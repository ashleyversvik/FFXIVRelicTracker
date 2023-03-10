using FFXIVRelicTracker.Helpers;

namespace FFXIVRelicTracker._06_SplendorousTools._03_Crystalline
{
    public class CrystallineModel : BaseStageModel
    {
        public CrystallineModel()
        {

        }

        public string ToolName { get; set; }
        public string CraftedMat { get; set; }
        public string FirstMat { get; set; }
        public string SecondMat { get; set; }
        public string ThirdMat { get; set; }
        public string TradedMat { get; set; }
        public int MinRemainingScrips { get; set; }
        public int MaxRemainingScrips { get; set; }
        public string GatherLoc { get; set; }

    }
}
