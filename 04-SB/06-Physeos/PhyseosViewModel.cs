using FFXIVRelicTracker._04_SB.SBHelpers;
using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace FFXIVRelicTracker._04_SB._06_Physeos
{
    public class PhyseosViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private PhyseosModel physeosModel;
        #endregion

        #region Constructor
        public PhyseosViewModel()
        {

        }
        public PhyseosViewModel(IEventAggregator eventAggregator)
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
        public string Name => "Physeos";
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    PhyseosModel = SelectedCharacter.SBModel.PhyseosModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }

        public PhyseosModel PhyseosModel
        {
            get { return physeosModel; }
            set
            {
                physeosModel = value;
                OnPropertyChanged(nameof(PhyseosModel));
            }
        }

        public string SelectedJob
        {
            get { return physeosModel.SelectedJob; }
            set
            {
                physeosModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
            }
        }

        public ObservableCollection<string> AvailableJobs
        {
            get { return physeosModel.AvailableJobs; }
            set
            {
                physeosModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }
        public int FragmentCount 
        { 
            get 
            { 
                if(physeosModel.FragmentCount < 0) { FragmentCount = 0; }
                return physeosModel.FragmentCount; 
            } 
            set 
            {
                if (value < 0) { physeosModel.FragmentCount = 0; }
                else { physeosModel.FragmentCount = value; }
                OnPropertyChanged(nameof(FragmentCount));
                OnPropertyChanged(nameof(FragmentNeeded));
            } 
        }

        public int FragmentNeeded 
        { 
            get 
            { 
                if (AvailableJobs == null) { LoadAvailableJobs(); } 
                return (AvailableJobs.Count * 100) - physeosModel.FragmentCount;
            } 
        }

        #endregion

        #region Methods
        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach (SBJob job in selectedCharacter.SBModel.SBJobList)
            {
                if (job.Physeos.Progress == BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.Physeos.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    SBInfo.ReloadJobList(AvailableJobs, job.Name);
                }
            }
            //Calculate remaining memories to acquire
            OnPropertyChanged(nameof(FragmentNeeded));
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

            SBStageCompleter.ProgressClass(selectedCharacter, tempJob.Physeos, true);

            LoadAvailableJobs();

            OnPropertyChanged(nameof(FragmentCount));
        }
        #endregion

        #region Increment Fragment
        private ICommand _FragmentButton;

        public ICommand FragmentButton
        {
            get
            {
                if (_FragmentButton == null)
                {
                    _FragmentButton = new RelayCommand(
                        param => this.FragmentCommand(param)
                        );
                }
                return _FragmentButton;
            }
        }

        private void FragmentCommand(object param)
        {
            FragmentCount += 1;
        }
        #endregion
        #endregion


    }
}
