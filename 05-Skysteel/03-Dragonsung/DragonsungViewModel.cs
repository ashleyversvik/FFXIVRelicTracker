using FFXIVRelicTracker._05_Skysteel.Skysteel_Helpers;
using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FFXIVRelicTracker._05_Skysteel._03_Dragonsung
{
    public class DragonsungViewModel : ObservableObject, IPageViewModel, IIncompleteViewModel
    {
        public string Name => "Dragonsung";

        #region Fields
        private DragonsungModel dragonsung1Model;
        private Character selectedCharacter;
        private IEventAggregator eventAggregator;
        private Tuple<string, string, string, string> jobInfo;
        private ObservableCollection<string> availableJobs;
        #endregion

        #region Constructors
        public DragonsungViewModel(IEventAggregator eventAggregator)
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
        public DragonsungModel DragonsungModel
        {
            get { return dragonsung1Model; }
            set
            {
                dragonsung1Model = value;
                OnPropertyChanged(nameof(DragonsungModel));
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
            get { return DragonsungModel.SelectedJob; }
            set
            {
                DragonsungModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
                OnPropertyChanged(nameof(IsGatherer));
                OnPropertyChanged(nameof(IsFSH));
                OnPropertyChanged(nameof(DisplayInfo));
                SetGatherLoc();

                if (value != null)
                {
                    jobInfo = SkysteelInfo.ReturnDragonsungTuple(SelectedJob);


                    ToolName = jobInfo.Item1;
                    CraftedMat = jobInfo.Item2;
                    FirstMat = jobInfo.Item3;
                    SecondMat = jobInfo.Item4;
                }
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

        public string ToolName { get { return dragonsung1Model.ToolName; } set { dragonsung1Model.ToolName = value; OnPropertyChanged(nameof(ToolName)); } }
        public string CraftedMat { get { return dragonsung1Model.CraftedMat; } set { dragonsung1Model.CraftedMat = value; OnPropertyChanged(nameof(CraftedMat)); } }
        public string FirstMat { get { return dragonsung1Model.FirstMat; } set { dragonsung1Model.FirstMat = value; OnPropertyChanged(nameof(FirstMat)); } }
        public string SecondMat { get { return dragonsung1Model.SecondMat; } set { dragonsung1Model.SecondMat = value; OnPropertyChanged(nameof(SecondMat)); } }
        public string GatherLoc { get { return dragonsung1Model.GatherLoc; } set { dragonsung1Model.GatherLoc = value; OnPropertyChanged(nameof(GatherLoc)); } }

        public string SelectedToolType { get { return SkysteelInfo.ReturnToolName(SelectedJob); } }
        public bool DisplayInfo { get { return SelectedJob != null; } }
        public bool IsGatherer { get { return (SelectedJob == "MIN" | SelectedJob == "BTN" | SelectedJob == "FSH"); } }
        public bool IsFSH { get { return SelectedJob == "FSH"; } }
        public int RemainingYellowScrips { get { return dragonsung1Model.RemainingYellowScrips; } set { dragonsung1Model.RemainingYellowScrips = value; OnPropertyChanged(nameof(RemainingYellowScrips)); } }


        #endregion

        #region Methods
        public void CheckModelExists()
        {
            if (DragonsungModel == null)
            {
                DragonsungModel = new DragonsungModel();
                selectedCharacter.SkysteelModel.DragonsungModel = DragonsungModel;
            }
            else { DragonsungModel = selectedCharacter.SkysteelModel.DragonsungModel; }
        }
        public void SetGatherLoc()
        {
            switch (SelectedJob)
            {
                case "MIN":
                    GatherLoc = " (The Dravanian Forelands | The Dravanian Hinterlands)";
                    break;
                case "BTN":
                    GatherLoc = " (Coerthas Western Highlands | The Churning Mists)";
                    break;
                default:
                    GatherLoc = "";
                    break;
            }
        }

        public void LoadAvailableJobs()
        {
            AvailableJobs = SkysteelInfo.LoadJobs(AvailableJobs, SelectedCharacter, Name);
            int tempCount = AvailableJobs.Count;

            if (AvailableJobs.Contains("MIN")) { tempCount -= 1; }
            if (AvailableJobs.Contains("BTN")) { tempCount -= 1; }
            if (AvailableJobs.Contains("FSH")) { tempCount -= 1; }

            RemainingYellowScrips = tempCount * 30 * 50;
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
