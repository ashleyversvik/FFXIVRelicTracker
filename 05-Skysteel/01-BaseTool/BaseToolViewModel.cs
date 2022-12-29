using FFXIVRelicTracker._05_Skysteel.Skysteel_Helpers;
using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FFXIVRelicTracker._05_Skysteel._01_BaseTool
{
    public class BaseToolViewModel : ObservableObject, IPageViewModel, IIncompleteViewModel
    {
        public string Name => "Base Tool";

        #region Fields
        private BaseToolModel baseToolModel;
        private Character selectedCharacter;
        private IEventAggregator eventAggregator;
        private ObservableCollection<string> availableJobs;

        #endregion

        #region Constructors
        public BaseToolViewModel(IEventAggregator eventAggregator)
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

        public BaseToolModel BaseToolModel
        {
            get { return baseToolModel; }
            set
            {
                baseToolModel = value;
                OnPropertyChanged(nameof(BaseToolModel));
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
            get { return baseToolModel.SelectedJob; }
            set
            {
                baseToolModel.SelectedJob = value;
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

        public bool CompletedFirstTool { get { return AvailableJobs.Count < SkysteelInfo.JobListString.Count; } }
        #endregion

        #region Methods
        public void CheckModelExists()
        {
            if (BaseToolModel == null)
            {
                BaseToolModel = new BaseToolModel();
                selectedCharacter.SkysteelModel.BaseToolModel = BaseToolModel;
            }
            else { BaseToolModel = selectedCharacter.SkysteelModel.BaseToolModel; }
        }


        public void LoadAvailableJobs()
        {
            AvailableJobs = SkysteelInfo.LoadJobs(AvailableJobs, SelectedCharacter, Name);
            OnPropertyChanged(nameof(AvailableJobs));
            OnPropertyChanged(nameof(CompletedFirstTool));
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
            SkysteelInfo.ProgressClass(SelectedCharacter, SelectedJob, Name);
            LoadAvailableJobs();
        }
        #endregion

       
        #endregion
    }
}
