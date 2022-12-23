using FFXIVRelicTracker.Helpers;

namespace FFXIVRelicTracker._05_Skysteel._02_BasePlus1
{
    public class BasePlus1Model : BaseStageModel
    {
        public string ToolName { get;  set; }
        public string FirstMat { get;  set; }
        public string SecondMat { get;  set; }
        public string CraftedMat { get;  set; }
        public int RemainingYellowScrips { get; set; }
        public string GatherLoc { get;  set; }

        public BasePlus1Model()
        {

        }
    }
}
