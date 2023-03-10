using FFXIVRelicTracker._06_SplendorousTools.Splendorous_Helpers;
using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FFXIVRelicTracker._06_SplendorousTools._01_Splendorous
{
    public class SplendorousViewModel : ObservableObject, IPageViewModel, IIncompleteViewModel
    {
        public string Name => "Splendorous";

        #region Fields
        private SplendorousModel splendorousModel;
        private Character selectedCharacter;
        private IEventAggregator eventAggregator;
        private ObservableCollection<string> availableJobs;

        #endregion

        #region Constructors
        public SplendorousViewModel(IEventAggregator eventAggregator)
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

        public SplendorousModel SplendorousModel
        {
            get { return splendorousModel; }
            set
            {
                splendorousModel = value;
                OnPropertyChanged(nameof(SplendorousModel));
            }
        }
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    CheckModelExists();
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
                
            }
        }

        public string SelectedJob
        {
            get { return splendorousModel.SelectedJob; }
            set
            {
                splendorousModel.SelectedJob = value;
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

        public bool CompletedFirstTool { get { return AvailableJobs.Count < SplendorousToolsInfo.JobListString.Count; } }
        public int RemainingScrips { get { return AvailableJobs.Count * 1500;  } }
        #endregion

        #region Methods
        public void CheckModelExists()
        {
            if (SplendorousModel == null)
            {
                SplendorousModel = new SplendorousModel();
                selectedCharacter.SplendorousToolsModel.SplendorousModel = SplendorousModel;
            }
            else { SplendorousModel = selectedCharacter.SplendorousToolsModel.SplendorousModel; }
        }


        public void LoadAvailableJobs()
        {
            AvailableJobs = SplendorousToolsInfo.LoadJobs(AvailableJobs, SelectedCharacter, Name);
            OnPropertyChanged(nameof(AvailableJobs));
            OnPropertyChanged(nameof(CompletedFirstTool));
            OnPropertyChanged(nameof(RemainingScrips));
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
            SplendorousToolsInfo.ProgressClass(SelectedCharacter, SelectedJob, Name);
            LoadAvailableJobs();
        }
        #endregion

       
        #endregion
    }
}
