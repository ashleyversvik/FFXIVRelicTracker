using FFXIVRelicTracker._05_Skysteel.Skysteel_Helpers;
using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace FFXIVRelicTracker._05_Skysteel._06_Skybuilders
{
    class SkybuildersViewModel : ObservableObject, IPageViewModel, IIncompleteViewModel
    {
        #region Fields
        private SkybuildersModel skybuildersModel;
        private Character selectedCharacter;
        private IEventAggregator eventAggregator;
        private Tuple<string, string, string, string, string, string> jobInfo;
        #endregion

        #region Constructors
        public SkybuildersViewModel(IEventAggregator eventAggregator)
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
        public string Name => "Skybuilders";
        public SkybuildersModel SkybuildersModel
        {
            get { return skybuildersModel; }
            set
            {
                skybuildersModel = value;
                OnPropertyChanged(nameof(SkybuildersModel));
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
            get { return SkybuildersModel.SelectedJob; }
            set
            {
                SkybuildersModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
                OnPropertyChanged(nameof(IsGatherer));
                OnPropertyChanged(nameof(IsFSH));
                OnPropertyChanged(nameof(DisplayInfo));
                SetGatherLoc();

                if (value != null)
                {
                    jobInfo = SkysteelInfo.ReturnSkybuildersTuple(SelectedJob);


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
            get { return SkybuildersModel.AvailableJobs; }
            set
            {
                SkybuildersModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }

        public string ToolName { get { return SkybuildersModel.ToolName; } set { SkybuildersModel.ToolName = value; OnPropertyChanged(nameof(ToolName)); } }
        public string TradedMat { get { return SkybuildersModel.TradedMat; } set { SkybuildersModel.TradedMat = value; OnPropertyChanged(nameof(TradedMat)); } }
        public string CraftedMat { get { return SkybuildersModel.CraftedMat; } set { SkybuildersModel.CraftedMat = value; OnPropertyChanged(nameof(CraftedMat)); } }
        public string FirstMat { get { return SkybuildersModel.FirstMat; } set { SkybuildersModel.FirstMat = value; OnPropertyChanged(nameof(FirstMat)); } }
        public string SecondMat { get { return SkybuildersModel.SecondMat; } set { SkybuildersModel.SecondMat = value; OnPropertyChanged(nameof(SecondMat)); } }
        public string ThirdMat { get { return SkybuildersModel.ThirdMat; } set { SkybuildersModel.ThirdMat = value; OnPropertyChanged(nameof(ThirdMat)); } }
        public string GatherLoc { get { return SkybuildersModel.GatherLoc; } set { SkybuildersModel.GatherLoc = value; OnPropertyChanged(nameof(GatherLoc)); } }


        public string SelectedToolType { get { return SkysteelInfo.ReturnToolName(SelectedJob); } }
        public bool DisplayInfo { get { return SelectedJob != null; } }
        public bool IsGatherer { get { return (SelectedJob == "MIN" | SelectedJob == "BTN" | SelectedJob == "FSH"); } }
        public bool IsFSH { get { return SelectedJob == "FSH"; } }
        public int MinRemainingYellowScrips { get { return SkybuildersModel.MinRemainingYellowScrips; } set { SkybuildersModel.MinRemainingYellowScrips = value; OnPropertyChanged(nameof(MinRemainingYellowScrips)); } }
        public int MaxRemainingYellowScrips { get { return SkybuildersModel.MaxRemainingYellowScrips; } set { SkybuildersModel.MaxRemainingYellowScrips = value; OnPropertyChanged(nameof(MaxRemainingYellowScrips)); } }


        #endregion

        #region Methods
        public void CheckModelExists()
        {
            if (SkybuildersModel == null)
            {
                SkybuildersModel = new SkybuildersModel();
                selectedCharacter.SkysteelModel.SkybuildersModel = SkybuildersModel;
            }
            else { SkybuildersModel = selectedCharacter.SkysteelModel.SkybuildersModel; }
        }

        public void SetGatherLoc()
        {
            switch (SelectedJob)
            {
                case "MIN":
                    GatherLoc = " (Diadem)";
                    break;
                case "BTN":
                    GatherLoc = " (Diadem)";
                    break;
                default:
                    GatherLoc = "";
                    break;
            }
        }
        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach (SkysteelJob job in selectedCharacter.SkysteelModel.SkysteelJobList)
            {
                if (job.Skybuilders.Progress == BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.Skybuilders.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    SkysteelInfo.ReloadJobList(AvailableJobs, job.Name);
                }
            }
            int tempCount = AvailableJobs.Count;

            if (AvailableJobs.Contains("MIN")) { tempCount -= 1; }
            if (AvailableJobs.Contains("BTN")) { tempCount -= 1; }
            if (AvailableJobs.Contains("FSH")) { tempCount -= 1; }

            MinRemainingYellowScrips = tempCount * 20 * 90;
            MaxRemainingYellowScrips = tempCount * 60 * 90;
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

            SkysteelJob tempJob = selectedCharacter.SkysteelModel.SkysteelJobList[SkysteelInfo.JobListString.IndexOf(SelectedJob)];

            SkysteelInfo.ProgressClass(selectedCharacter, tempJob.Skybuilders, true);

            LoadAvailableJobs();

        }
        #endregion

        #endregion
    }
}
