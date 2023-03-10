using FFXIVRelicTracker._05_ShB.ShBHelpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FFXIVRelicTracker._05_ShB._04_LawsOrder
{
    public class LawsOrderViewModel : ObservableObject, IPageViewModel
    {
        public string Name => "Law's Order";
        public string TagName => "LawsOrder";

        #region Fields
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private LawsOrderModel lawsOrderModel;
        private ObservableCollection<string> availableJobs;
        #endregion

        #region Constructor
        public LawsOrderViewModel()
        {

        }
        public LawsOrderViewModel(IEventAggregator eventAggregator)
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
                    LawsOrderModel = SelectedCharacter.ShBModel.LawsOrderModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }

        public LawsOrderModel LawsOrderModel
        {
            get { return lawsOrderModel; }
            set
            {
                lawsOrderModel = value;
                OnPropertyChanged(nameof(LawsOrderModel));
            }
        }

        public string SelectedJob
        {
            get { return lawsOrderModel.SelectedJob; }
            set
            {
                lawsOrderModel.SelectedJob = value;
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
        public int MemoryCount
        {
            get
            {
                if (lawsOrderModel.MemoryCount < 0) { MemoryCount = 0; }
                return lawsOrderModel.MemoryCount;
            }
            set
            {
                if (value < 0) { lawsOrderModel.MemoryCount = 0; }
                else { lawsOrderModel.MemoryCount = value; }
                OnPropertyChanged(nameof(MemoryCount));
                OnPropertyChanged(nameof(MemoryNeeded));
            }
        }
        
        public int MemoryNeeded
        {
            get
            {
                if (AvailableJobs == null) { LoadAvailableJobs(); }
                return (AvailableJobs.Count * 15) - lawsOrderModel.MemoryCount;
            }
        }

        #endregion

        #region Methods
        public void LoadAvailableJobs()
        {
            AvailableJobs = ShBInfo.LoadJobs(AvailableJobs, SelectedCharacter, TagName);
            //Calculate remaining memories to acquire
            OnPropertyChanged(nameof(MemoryNeeded));
            OnPropertyChanged(nameof(MemoryCount));
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

            OnPropertyChanged(nameof(MemoryCount));
            OnPropertyChanged(nameof(MemoryNeeded));
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
            MemoryCount += 1;
        }
        #endregion
        #endregion


    }
}
