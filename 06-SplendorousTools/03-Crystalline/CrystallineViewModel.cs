using FFXIVRelicTracker._06_SplendorousTools.Splendorous_Helpers;
using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FFXIVRelicTracker._06_SplendorousTools._03_Crystalline
{
    public class CrystallineViewModel : ObservableObject, IPageViewModel, IIncompleteViewModel
    {
        public string Name => "Crystalline";
        #region Fields
        private CrystallineModel augmentedSplendorousModel;
        private Character selectedCharacter;
        private IEventAggregator eventAggregator;
        private Tuple<string, string, string, string, string, string> jobInfo;
        private ObservableCollection<string> availableJobs;
        #endregion

        #region Constructors
        public CrystallineViewModel(IEventAggregator eventAggregator)
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
        public CrystallineModel CrystallineModel
        {
            get { return augmentedSplendorousModel; }
            set
            {
                augmentedSplendorousModel = value;
                OnPropertyChanged(nameof(CrystallineModel));
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
            get { return CrystallineModel.SelectedJob; }
            set
            {
                CrystallineModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
                OnPropertyChanged(nameof(IsGatherer));
                OnPropertyChanged(nameof(IsFSH));
                OnPropertyChanged(nameof(DisplayInfo));
                SetGatherLoc();

                if (value != null)
                {
                    jobInfo = SplendorousToolsInfo.ReturnCrystallineTuple(SelectedJob);

                    ToolName = jobInfo.Item1;
                    TradedMat = jobInfo.Item2;
                    CraftedMat = jobInfo.Item3;
                    FirstMat = jobInfo.Item4;
                    SecondMat = jobInfo.Item5;
                    ThirdMat = jobInfo.Item6;
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

        public string ToolName { get { return augmentedSplendorousModel.ToolName; } set { augmentedSplendorousModel.ToolName = value; OnPropertyChanged(nameof(ToolName)); } }
        public string TradedMat { get { return augmentedSplendorousModel.TradedMat; } set { augmentedSplendorousModel.TradedMat = value; OnPropertyChanged(nameof(TradedMat)); } }
        public string CraftedMat { get { return augmentedSplendorousModel.CraftedMat; } set { augmentedSplendorousModel.CraftedMat = value; OnPropertyChanged(nameof(CraftedMat)); } }
        public string FirstMat { get { return augmentedSplendorousModel.FirstMat; } set { augmentedSplendorousModel.FirstMat = value; OnPropertyChanged(nameof(FirstMat)); } }
        public string SecondMat { get { return augmentedSplendorousModel.SecondMat; } set { augmentedSplendorousModel.SecondMat = value; OnPropertyChanged(nameof(SecondMat)); } }
        public string ThirdMat { get { return augmentedSplendorousModel.ThirdMat; } set { augmentedSplendorousModel.ThirdMat = value; OnPropertyChanged(nameof(ThirdMat)); } }
        public string GatherLoc { get { return augmentedSplendorousModel.GatherLoc; } set { augmentedSplendorousModel.GatherLoc = value; OnPropertyChanged(nameof(GatherLoc)); } }

        public string SelectedToolType { get { return SplendorousToolsInfo.ReturnToolName(SelectedJob); } }
        public bool DisplayInfo { get { return SelectedJob != null; } }
        public bool IsGatherer { get { return (SelectedJob == "MIN" | SelectedJob == "BTN" | SelectedJob == "FSH"); } }
        public bool IsFSH { get { return SelectedJob == "FSH"; } }
        public int MinRemainingScrips { get { return augmentedSplendorousModel.MinRemainingScrips; } set { augmentedSplendorousModel.MinRemainingScrips = value; OnPropertyChanged(nameof(MinRemainingScrips)); } }
        public int MaxRemainingScrips { get { return augmentedSplendorousModel.MaxRemainingScrips; } set { augmentedSplendorousModel.MaxRemainingScrips = value; OnPropertyChanged(nameof(MaxRemainingScrips)); } }


        #endregion

        #region Methods
        public void CheckModelExists()
        {
            if (CrystallineModel == null)
            {
                CrystallineModel = new CrystallineModel();
                selectedCharacter.SplendorousToolsModel.CrystallineModel = CrystallineModel;
            }
            else { CrystallineModel = selectedCharacter.SplendorousToolsModel.CrystallineModel; }
        }
        public void SetGatherLoc()
        {
            switch (SelectedJob)
            {
                case "MIN":
                    GatherLoc = " (Amh Araeng)";
                    break;
                case "BTN":
                    GatherLoc = " (Lakeland)";
                    break;
                default:
                    GatherLoc = "";
                    break;
            }
        }

        public void LoadAvailableJobs()
        {
            AvailableJobs = SplendorousToolsInfo.LoadJobs(AvailableJobs, SelectedCharacter, Name);
            int tempCount = AvailableJobs.Count;

            if (AvailableJobs.Contains("MIN")) { tempCount -= 1; }
            if (AvailableJobs.Contains("BTN")) { tempCount -= 1; }
            if (AvailableJobs.Contains("FSH")) { tempCount -= 1; }

            MinRemainingScrips = tempCount * 30 * 50;
            MaxRemainingScrips = tempCount * 90 * 50;
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
