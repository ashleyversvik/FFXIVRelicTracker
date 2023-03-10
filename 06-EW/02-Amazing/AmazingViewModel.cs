using FFXIVRelicTracker._06_EW.EWHelpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FFXIVRelicTracker._06_EW._02_Amazing
{
    public class AmazingViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        public string Name => "Amazing";
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private AmazingModel amazingModel;
        private ObservableCollection<string> availableJobs;
        #endregion

        #region Constructor
        public AmazingViewModel()
        {

        }
        public AmazingViewModel(IEventAggregator eventAggregator)
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
                    AmazingModel = SelectedCharacter.EWModel.AmazingModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }

        public AmazingModel AmazingModel
        {
            get { return amazingModel; }
            set
            {
                amazingModel = value;
                OnPropertyChanged(nameof(AmazingModel));
            }
        }

        public string SelectedJob
        {
            get { return amazingModel.SelectedJob; }
            set
            {
                amazingModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
            }
        }
        public int ChondritesCount
        {
            get 
            {
                if (amazingModel.ChondritesCount < 0) { ChondritesCount = 0; }
                return amazingModel.ChondritesCount;
            }
            set
            {
                if(value>=0 & value < 60)
                {
                    amazingModel.ChondritesCount = value;
                    OnPropertyChanged(nameof(ChondritesCount));
                    OnPropertyChanged(nameof(ChondritesCost));
                    OnPropertyChanged(nameof(NeededChondrites));
                }
            }
        }
        public int NeededChondrites { get { if (AvailableJobs == null) { LoadAvailableJobs(); } return (AvailableJobs.Count * 3) - ChondritesCount; } }
        public int ChondritesCost => NeededChondrites * 500;

        public ObservableCollection<string> AvailableJobs
        {
            get { return availableJobs; }
            set
            {
                availableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }
        public bool CompletedFirstAmazing { get { return AvailableJobs.Count < EWInfo.JobListString.Count; } }
        #endregion


        #region Methods
        public void LoadAvailableJobs()
        {
            AvailableJobs = EWInfo.LoadJobs(AvailableJobs, SelectedCharacter, Name);
            OnPropertyChanged(nameof(CompletedFirstAmazing));
            OnPropertyChanged(nameof(NeededChondrites));
            OnPropertyChanged(nameof(ChondritesCost));
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
            EWStageCompleter.ProgressClass(selectedCharacter, SelectedJob, Name);

            LoadAvailableJobs();
            OnPropertyChanged(nameof(ChondritesCount));
            OnPropertyChanged(nameof(NeededChondrites));
        }
        #endregion

        #region Increment Chondrites
        private ICommand _ChondritesButton;

        public ICommand ChondritesButton
        {
            get
            {
                if (_ChondritesButton == null)
                {
                    _ChondritesButton = new RelayCommand(
                        param => this.ChondritesCommand(param)
                        );
                }
                return _ChondritesButton;
            }
        }

        private void ChondritesCommand(object param)
        {
            ChondritesCount += 1;
        }
        #endregion
        #endregion
    }
}
