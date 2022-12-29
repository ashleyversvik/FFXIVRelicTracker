using FFXIVRelicTracker._04_SB.SBHelpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FFXIVRelicTracker._04_SB._01_Antiquated
{
    public class AntiquatedViewModel : ObservableObject, IPageViewModel
    {
        public string Name => "Antiquated";

        #region Fields
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private AntiquatedModel antiquatedModel;
        private ObservableCollection<string> availableJobs;
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
            get { return availableJobs; }
            set
            {
                availableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }
        #endregion


        #region Methods
        public void LoadAvailableJobs()
        {
            AvailableJobs = SBInfo.LoadJobs(AvailableJobs, SelectedCharacter, Name);
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
            SBStageCompleter.ProgressClass(SelectedCharacter, SelectedJob, Name);
            LoadAvailableJobs();
        }
        #endregion

        #endregion
    }
}
