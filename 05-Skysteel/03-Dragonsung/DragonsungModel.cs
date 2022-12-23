using FFXIVRelicTracker.Helpers;

namespace FFXIVRelicTracker._05_Skysteel._03_Dragonsung
{
    public class DragonsungModel : BaseStageModel
    {
        public DragonsungModel()
        {

        }

        public string ToolName { get;  set; }
        public string CraftedMat { get;  set; }
        public string FirstMat { get;  set; }
        public string SecondMat { get;  set; }
        public int RemainingYellowScrips { get; set; }
        public string TradedMat { get;  set; }
        public string GatherLoc { get;  set; }
    }
}
