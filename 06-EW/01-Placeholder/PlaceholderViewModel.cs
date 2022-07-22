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

namespace FFXIVRelicTracker._06_EW._01_Placeholder
{
    public class PlaceholderViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private PlaceholderModel placeholderModel;
        #endregion

        #region Constructor
        public PlaceholderViewModel()
        {

        }
        public PlaceholderViewModel(IEventAggregator eventAggregator)
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
        public string Name => "Placeholder";

        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    PlaceholderModel = SelectedCharacter.EWModel.PlaceholderModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }

        public PlaceholderModel PlaceholderModel
        {
            get { return placeholderModel; }
            set
            {
                placeholderModel = value;
                OnPropertyChanged(nameof(PlaceholderModel));
            }
        }

        public string CurrentPlaceholder
        {
            get { return placeholderModel.CurrentPlaceholder; }
            set
            {
                placeholderModel.CurrentPlaceholder = value;
                OnPropertyChanged(nameof(CurrentPlaceholder));
            }
        }
        public int CurrentScalepowder
        {
            get 
            {
                if (placeholderModel.CurrentScalepowder < 0) { CurrentScalepowder = 0; }
                return placeholderModel.CurrentScalepowder; 
            }
            set
            {
                if(value>=0 & value < 70)
                {
                    placeholderModel.CurrentScalepowder = value;
                    OnPropertyChanged(nameof(CurrentScalepowder));
                    OnPropertyChanged(nameof(ScalepowderCost));
                }
            }
        }
        public int NeededScalepowder { get { if (AvailableJobs == null) { LoadAvailableJobs(); } return Math.Min(16, AvailableJobs.Count) * 4; } }
        public int ScalepowderCost => (NeededScalepowder - CurrentScalepowder) * 250;

        public ObservableCollection<string> AvailableJobs
        {
            get { return placeholderModel.AvailableJobs; }
            set
            {
                placeholderModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }
        public bool CompletedFirstPlaceholder { get { return AvailableJobs.Count < EWInfo.JobListString.Count; } }
        #endregion


        #region Methods
        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach( EWJob job in selectedCharacter.EWModel.EWJobList)
            {
                if(job.Placeholder.Progress==BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.Placeholder.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    EWInfo.ReloadJobList(AvailableJobs, job.Name);                 
                }
            }
            OnPropertyChanged(nameof(CompletedFirstPlaceholder));
            OnPropertyChanged(nameof(NeededScalepowder));
            OnPropertyChanged(nameof(ScalepowderCost));
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

        private bool CompleteCan() { return CurrentPlaceholder != null; }
        private void CompleteCommand()
        {

            EWJob tempJob = selectedCharacter.EWModel.EWJobList[EWInfo.JobListString.IndexOf(CurrentPlaceholder)];

            EWStageCompleter.ProgressClass(selectedCharacter, tempJob.Placeholder, true);

            LoadAvailableJobs();

        }
        #endregion

        #region Increment Scalepowder
        private ICommand _ScalepowderButton;

        public ICommand ScalepowderButton
        {
            get
            {
                if (_ScalepowderButton == null)
                {
                    _ScalepowderButton = new RelayCommand(
                        param => this.ScalepowderCommand(param)
                        );
                }
                return _ScalepowderButton;
            }
        }

        private void ScalepowderCommand(object param)
        {
            CurrentScalepowder += 1;
        }
        #endregion
        #endregion
    }
}
