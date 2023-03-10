using FFXIVRelicTracker._05_ShB.ShBHelpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FFXIVRelicTracker._05_ShB._02_AugmentedResistance
{

    public class AugmentedResistanceViewModel : ObservableObject, IPageViewModel
    {
        public string Name => "Augmented Resistance";
        public string TagName => "AugmentedResistance";

        #region Fields
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private AugmentedResistanceModel augmentedResistanceModel;
        private ObservableCollection<string> availableJobs;
        #endregion

        #region Constructor
        public AugmentedResistanceViewModel()
        {

        }

        public AugmentedResistanceViewModel(IEventAggregator eventAggregator)
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
                    AugmentedResistanceModel = SelectedCharacter.ShBModel.AugmentedResistanceModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }

        public AugmentedResistanceModel AugmentedResistanceModel
        {
            get { return augmentedResistanceModel; }
            set
            {
                augmentedResistanceModel = value;
                OnPropertyChanged(nameof(AugmentedResistanceModel));
            }
        }

        public string SelectedJob
        {
            get { return augmentedResistanceModel.SelectedJob; }
            set
            {
                augmentedResistanceModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
            }
        }

        public ObservableCollection<string> AvailableJobs
        {
            get { return availableJobs; }
            set
            {
                availableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }

        public int HarrowingNeeded { get { if (AvailableJobs != null) { return (AvailableJobs.Count*20) - HarrowingCount; } else { return 0; }; } }
        public int TorturedNeeded { get { if (AvailableJobs != null) { return (AvailableJobs.Count * 20) - TorturedCount; } else { return 0; }; } }
        public int SorrowfulNeeded { get { if (AvailableJobs != null) { return (AvailableJobs.Count * 20) - SorrowfulCount; } else { return 0; }; } }
        public int HarrowingCount
        {
            get
            {
                if (augmentedResistanceModel.HarrowingCount < 0) { HarrowingCount = 0; }
                return augmentedResistanceModel.HarrowingCount;
            }
            set
            {
                if (value < 0) { augmentedResistanceModel.HarrowingCount = 0; }
                else { augmentedResistanceModel.HarrowingCount = value; }
                OnPropertyChanged(nameof(HarrowingCount));
                OnPropertyChanged(nameof(HarrowingNeeded));
            }
        }
        public int TorturedCount
        {
            get
            {
                if (augmentedResistanceModel.TorturedCount < 0) { TorturedCount = 0; }
                return augmentedResistanceModel.TorturedCount;
            }
            set
            {
                if (value < 0) { augmentedResistanceModel.TorturedCount = 0; }
                else { augmentedResistanceModel.TorturedCount = value; }
                OnPropertyChanged(nameof(TorturedCount));
                OnPropertyChanged(nameof(TorturedNeeded));
            }
        }
        public int SorrowfulCount
        {
            get
            {
                if (augmentedResistanceModel.SorrowfulCount < 0) { SorrowfulCount = 0; }
                return augmentedResistanceModel.SorrowfulCount;
            }
            set
            {
                if (value < 0) { augmentedResistanceModel.SorrowfulCount = 0; }
                else { augmentedResistanceModel.SorrowfulCount = value; }
                OnPropertyChanged(nameof(SorrowfulCount));
                OnPropertyChanged(nameof(SorrowfulNeeded));
            }
        }

        #endregion

        #region Methods
        public void LoadAvailableJobs()
        {
            AvailableJobs = ShBInfo.LoadJobs(AvailableJobs, SelectedCharacter, TagName);
            //Calculate remaining memories to acquire
            OnPropertyChanged(nameof(HarrowingNeeded));
            OnPropertyChanged(nameof(TorturedNeeded));
            OnPropertyChanged(nameof(SorrowfulNeeded));
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
            ShBStageCompleter.ProgressClass(selectedCharacter, SelectedJob, TagName);

            LoadAvailableJobs();
            OnPropertyChanged(nameof(HarrowingCount));
            OnPropertyChanged(nameof(TorturedCount)); 
            OnPropertyChanged(nameof(SorrowfulCount)); 

        }
        #endregion

        #region Increment Memory
        private ICommand _MemoryButton;

        public ICommand MemoryButton
        {
            get
            {
                if (_MemoryButton == null)
                {
                    _MemoryButton = new RelayCommand(
                        param => this.MemoryCommand(param)
                        );
                }
                return _MemoryButton;
            }
        }

        private void MemoryCommand(object param)
        {
            switch (param)
            {
                case "Harrowing":
                    HarrowingCount += 1;
                    break;
                case "Tortured":
                    TorturedCount += 1;
                    break;
                case "Sorrowful":
                    SorrowfulCount += 1;
                    break;
                default:
                    break;
            }
        }
        #endregion
        #endregion


    }
}
