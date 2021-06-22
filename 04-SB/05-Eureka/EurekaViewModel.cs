using FFXIVRelicTracker._04_SB.SBHelpers;
using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace FFXIVRelicTracker._04_SB._05_Eureka
{

    public class EurekaViewModel : ObservableObject, IPageViewModel
    {

        #region Fields
        public string Name => "Eureka";
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private EurekaModel eurekaModel;
        #endregion

        #region Constructor
        public EurekaViewModel()
        {

        }

        public EurekaViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            SubscriptionToken subscriptionToken =
                        this
                            .eventAggregator
                            .GetEvent<PubSubEvent<Character>>()
                            .Subscribe((details) =>
                            {
                                this.SelectedCharacter = details;
                            });
        }
        #endregion

        #region Properties
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    EurekaModel = SelectedCharacter.SBModel.EurekaModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }

        public EurekaModel EurekaModel
        {
            get { return eurekaModel; }
            set
            {
                eurekaModel = value;
                OnPropertyChanged(nameof(EurekaModel));
            }
        }

        public string SelectedJob
        {
            get { return eurekaModel.SelectedJob; }
            set
            {
                if (value != null && value != eurekaModel.SelectedJob) ResetWeaponState();
                eurekaModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
            }
        }

        public ObservableCollection<string> AvailableJobs
        {
            get { return eurekaModel.AvailableJobs; }
            set
            {
                eurekaModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }
        public int HydatosNeeded 
        { 
            get 
            { 
                if (AvailableJobs != null) 
                { 
                    return AvailableJobs.Count * 350;
                } 
                else { return 0; }; 
            } 
        }
        public int HydatosRemaining
        {
            get
            {
                if (AvailableJobs == null) { LoadAvailableJobs(); }
                return HydatosNeeded - HydatosCount;
            }
        }
        public int HydatosCount
        {
            get
            {
                if (eurekaModel.HydatosCount < 0) { HydatosCount = 0; }
                return eurekaModel.HydatosCount;
            }
            set
            {
                if (value < 0) { eurekaModel.HydatosCount = 0; }
                else { eurekaModel.HydatosCount = value; }
                OnPropertyChanged(nameof(HydatosCount));
                OnPropertyChanged(nameof(HydatosRemaining));
            }
        }
        public int ScaleNeeded { get { if (AvailableJobs != null) { return AvailableJobs.Count * 5; } else { return 0; } } }
        public int ScaleRemaining
        {
            get
            {
                if (AvailableJobs == null) { LoadAvailableJobs(); }
                return ScaleNeeded - ScaleCount;
            }
        }
        public int ScaleCount
        {
            get
            {
                if (eurekaModel.ScaleCount < 0) { ScaleCount = 0; }
                return eurekaModel.ScaleCount;
            }
            set
            {
                if (value < 0) { eurekaModel.ScaleCount = 0; }
                else { eurekaModel.ScaleCount = value; }
                OnPropertyChanged(nameof(ScaleCount));
                OnPropertyChanged(nameof(ScaleRemaining));
            }
        }
        private ObservableCollection<bool> WeaponState
        {
            get
            {
                if (eurekaModel.WeaponState == null)
                {
                    eurekaModel.WeaponState = new ObservableCollection<bool>()
                    {
                        false,
                        false,
                        false
                    };
                }
                return eurekaModel.WeaponState;
            }
            set
            {
                eurekaModel.WeaponState = value;
                RecheckBools();
            }
        }

        public bool State00 { get { return WeaponState[00]; } set { WeaponState[00] = value; AdjustBools(00, value); } }
        public bool State01 { get { return WeaponState[01]; } set { WeaponState[01] = value; AdjustBools(01, value); } }
        public bool State02 { get { return WeaponState[02]; } set { WeaponState[02] = value; AdjustBools(02, value); } }
        #endregion

        #region Methods
        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach (SBJob job in selectedCharacter.SBModel.SBJobList)
            {
                if (job.Eureka.Progress == BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.Eureka.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    SBInfo.ReloadJobList(AvailableJobs, job.Name);
                }
            }
            OnPropertyChanged(nameof(HydatosNeeded));
            OnPropertyChanged(nameof(HydatosCount));
            OnPropertyChanged(nameof(ScaleNeeded));
            OnPropertyChanged(nameof(ScaleCount));
        }
        private void AdjustBools(int BoolIndex, bool value)
        {
            if (value)
            {
                for (int index = 0; index <= BoolIndex; index++)
                {
                    WeaponState[index] = true;
                }
            }
            else
            {
                for (int index = BoolIndex; index < WeaponState.Count; index++)
                {
                    WeaponState[index] = false;
                }
            }
            RecheckBools();
        }
        private void RecheckBools()
        {
            OnPropertyChanged(nameof(State00));
            OnPropertyChanged(nameof(State01));
            OnPropertyChanged(nameof(State02));
        }
        public void ResetWeaponState()
        {
            eurekaModel.WeaponState = new ObservableCollection<bool>
            {
                false, false, false
            };
            RecheckBools();
        }
        #endregion

        #region Commands
        #region Complete Button
        private ICommand _CompleteButton;

        public ICommand CompleteButton
        {
            get
            {
                if (_CompleteButton == null)
                {
                    _CompleteButton = new RelayCommand(
                        param => this.CompleteCommand(),
                        param => this.CompleteCan()
                        );
                }
                return _CompleteButton;
            }
        }

        private bool CompleteCan() { return SelectedJob != null; }
        private void CompleteCommand()
        {

            SBJob tempJob = selectedCharacter.SBModel.SBJobList[SBInfo.JobListString.IndexOf(SelectedJob)];

            SBStageCompleter.ProgressClass(selectedCharacter, tempJob.Eureka, true);

            LoadAvailableJobs();
            OnPropertyChanged(nameof(HydatosCount));
            OnPropertyChanged(nameof(ScaleCount)); 
        }
        #endregion

        #region Increment Hydatos
        private ICommand _HydatosButton;

        public ICommand HydatosButton
        {
            get
            {
                if (_HydatosButton == null)
                {
                    _HydatosButton = new RelayCommand(
                        param => this.HydatosCommand(param)
                        );
                }
                return _HydatosButton;
            }
        }

        private void HydatosCommand(object param)
        {
            HydatosCount += 1;
        }
        #endregion

        #region Increment Scale
        private ICommand _ScaleButton;

        public ICommand ScaleButton
        {
            get
            {
                if (_ScaleButton == null)
                {
                    _ScaleButton = new RelayCommand(
                        param => this.ScaleCommand(param)
                        );
                }
                return _ScaleButton;
            }
        }

        private void ScaleCommand(object param)
        {
            ScaleCount += 1;
        }
        #endregion
        #endregion


    }
}
