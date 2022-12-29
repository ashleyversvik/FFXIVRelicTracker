using FFXIVRelicTracker._05_ShB.ShBHelpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FFXIVRelicTracker._05_ShB._01_Resistance
{
    public class ResistanceViewModel : ObservableObject, IPageViewModel
    {
        public string Name => "Resistance";

        #region Fields
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private ResistanceModel resistanceModel;
        private ObservableCollection<string> availableJobs;
        #endregion

        #region Constructor
        public ResistanceViewModel()
        {

        }
        public ResistanceViewModel(IEventAggregator eventAggregator)
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
                    ResistanceModel = SelectedCharacter.ShBModel.ResistanceModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }

        public ResistanceModel ResistanceModel
        {
            get { return resistanceModel; }
            set
            {
                resistanceModel = value;
                OnPropertyChanged(nameof(ResistanceModel));
            }
        }

        public string SelectedJob
        {
            get { return resistanceModel.SelectedJob; }
            set
            {
                resistanceModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
            }
        }
        public int ScalepowderCount
        {
            get 
            {
                if (resistanceModel.ScalepowderCount < 0) { ScalepowderCount = 0; }
                return resistanceModel.ScalepowderCount; 
            }
            set
            {
                if(value>=0 & value < 70)
                {
                    resistanceModel.ScalepowderCount = value;
                    OnPropertyChanged(nameof(ScalepowderCount));
                    OnPropertyChanged(nameof(ScalepowderCost));
                }
            }
        }
        public int NeededScalepowder { get { if (AvailableJobs == null) { LoadAvailableJobs(); } return (Math.Min(16, AvailableJobs.Count) * 4) - ScalepowderCount; } }
        public int ScalepowderCost => NeededScalepowder * 250;

        public ObservableCollection<string> AvailableJobs
        {
            get { return availableJobs; }
            set
            {
                availableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }
        public bool CompletedFirstResistance { get { return AvailableJobs.Count < ShBInfo.JobListString.Count; } }
        #endregion


        #region Methods
        public void LoadAvailableJobs()
        {
            AvailableJobs = ShBInfo.LoadJobs(AvailableJobs, SelectedCharacter, Name);
            OnPropertyChanged(nameof(CompletedFirstResistance));
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

        private bool CompleteCan() { return SelectedJob != null; }
        private void CompleteCommand()
        {

            ShBStageCompleter.ProgressClass(selectedCharacter, SelectedJob, Name);
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
            ScalepowderCount += 1;
            OnPropertyChanged(nameof(NeededScalepowder));
        }
        #endregion
        #endregion
    }
}
