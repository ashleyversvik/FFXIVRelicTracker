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

namespace FFXIVRelicTracker._04_SB._01_Antiquated
{
    public class AntiquatedViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private AntiquatedModel antiquatedModel;
        #endregion

        #region Constructor
        public AntiquatedViewModel()
        {

        }
        public AntiquatedViewModel(IEventAggregator eventAggregator)
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
        public string Name => "Antiquated";

        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    AntiquatedModel = SelectedCharacter.SBModel.AntiquatedModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }

        public AntiquatedModel AntiquatedModel
        {
            get { return antiquatedModel; }
            set
            {
                antiquatedModel = value;
                OnPropertyChanged(nameof(AntiquatedModel));
            }
        }

        public string SelectedJob
        {
            get { return antiquatedModel.SelectedJob; }
            set
            {
                antiquatedModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
            }
        }

        public ObservableCollection<string> AvailableJobs
        {
            get { return antiquatedModel.AvailableJobs; }
            set
            {
                antiquatedModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }
        #endregion


        #region Methods
        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach( SBJob job in selectedCharacter.SBModel.SBJobList)
            {
                if(job.Antiquated.Progress==BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.Antiquated.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    SBInfo.ReloadJobList(AvailableJobs, job.Name);                 
                }
            }
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

            SBStageCompleter.ProgressClass(selectedCharacter, tempJob.Antiquated, true);

            LoadAvailableJobs();

        }
        #endregion

        #endregion
    }
}
