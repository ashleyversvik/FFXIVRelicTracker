using FFXIVRelicTracker._05_ShB.ShBHelpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FFXIVRelicTracker._05_ShB._03_Recollection
{
    public class RecollectionViewModel : ObservableObject, IPageViewModel
    {
        public string Name => "Recollection";

        #region Fields
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private RecollectionModel recollectionModel;
        private ObservableCollection<string> availableJobs;
        #endregion

        #region Constructor
        public RecollectionViewModel()
        {

        }
        public RecollectionViewModel(IEventAggregator eventAggregator)
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
                    RecollectionModel = SelectedCharacter.ShBModel.RecollectionModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }

        public RecollectionModel RecollectionModel
        {
            get { return recollectionModel; }
            set
            {
                recollectionModel = value;
                OnPropertyChanged(nameof(RecollectionModel));
            }
        }

        public string SelectedJob
        {
            get { return recollectionModel.SelectedJob; }
            set
            {
                recollectionModel.SelectedJob = value;
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
                if(recollectionModel.MemoryCount < 0) { MemoryCount = 0; }
                return recollectionModel.MemoryCount; 
            } 
            set 
            {
                if (value < 0) { recollectionModel.MemoryCount = 0; }
                else { recollectionModel.MemoryCount = value; }
                OnPropertyChanged(nameof(MemoryCount));
                OnPropertyChanged(nameof(MemoryNeeded));
            } 
        }

        public int MemoryNeeded 
        { 
            get 
            { 
                if (AvailableJobs == null) { LoadAvailableJobs(); } 
                return (AvailableJobs.Count * 6) - recollectionModel.MemoryCount;
            } 
        }

        #endregion

        #region Methods
        public void LoadAvailableJobs()
        {
            AvailableJobs = ShBInfo.LoadJobs(AvailableJobs, SelectedCharacter, Name);
            //Calculate remaining memories to acquire
            OnPropertyChanged(nameof(MemoryNeeded));
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

            OnPropertyChanged(nameof(MemoryCount));
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
