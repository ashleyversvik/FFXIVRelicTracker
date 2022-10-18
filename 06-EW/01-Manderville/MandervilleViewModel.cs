using FFXIVRelicTracker._06_EW.EWHelpers;
using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace FFXIVRelicTracker._06_EW._01_Manderville
{
    public class MandervilleViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private MandervilleModel mandervilleModel;
        #endregion

        #region Constructor
        public MandervilleViewModel()
        {

        }
        public MandervilleViewModel(IEventAggregator eventAggregator)
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
        public string Name => "Manderville";

        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    MandervilleModel = SelectedCharacter.EWModel.MandervilleModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }

        public MandervilleModel MandervilleModel
        {
            get { return mandervilleModel; }
            set
            {
                mandervilleModel = value;
                OnPropertyChanged(nameof(MandervilleModel));
            }
        }

        public string CurrentManderville
        {
            get { return mandervilleModel.CurrentManderville; }
            set
            {
                mandervilleModel.CurrentManderville = value;
                OnPropertyChanged(nameof(CurrentManderville));
            }
        }
        public int CurrentMeteorites
        {
            get 
            {
                if (mandervilleModel.CurrentMeteorites < 0) { CurrentMeteorites = 0; }
                return mandervilleModel.CurrentMeteorites; 
            }
            set
            {
                if(value>=0 & value < 60)
                {
                    mandervilleModel.CurrentMeteorites = value;
                    OnPropertyChanged(nameof(CurrentMeteorites));
                    OnPropertyChanged(nameof(MeteoritesCost));
                }
            }
        }
        public int NeededMeteorites { get { if (AvailableJobs == null) { LoadAvailableJobs(); } return Math.Min(19, AvailableJobs.Count) * 3; } }
        public int MeteoritesCost => (NeededMeteorites - CurrentMeteorites) * 500;

        public ObservableCollection<string> AvailableJobs
        {
            get { return mandervilleModel.AvailableJobs; }
            set
            {
                mandervilleModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }
        public bool CompletedFirstManderville { get { return AvailableJobs.Count < EWInfo.JobListString.Count; } }
        #endregion


        #region Methods
        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach( EWJob job in selectedCharacter.EWModel.EWJobList)
            {
                if(job.Manderville.Progress==BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.Manderville.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    EWInfo.ReloadJobList(AvailableJobs, job.Name);                 
                }
            }
            OnPropertyChanged(nameof(CompletedFirstManderville));
            OnPropertyChanged(nameof(NeededMeteorites));
            OnPropertyChanged(nameof(MeteoritesCost));
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

        private bool CompleteCan() { return CurrentManderville != null; }
        private void CompleteCommand()
        {

            EWJob tempJob = selectedCharacter.EWModel.EWJobList[EWInfo.JobListString.IndexOf(CurrentManderville)];

            EWStageCompleter.ProgressClass(selectedCharacter, tempJob.Manderville, true);

            LoadAvailableJobs();

        }
        #endregion

        #region Increment Meteorites
        private ICommand _MeteoritesButton;

        public ICommand MeteoritesButton
        {
            get
            {
                if (_MeteoritesButton == null)
                {
                    _MeteoritesButton = new RelayCommand(
                        param => this.MeteoritesCommand(param)
                        );
                }
                return _MeteoritesButton;
            }
        }

        private void MeteoritesCommand(object param)
        {
            CurrentMeteorites += 1;
        }
        #endregion
        #endregion
    }
}
