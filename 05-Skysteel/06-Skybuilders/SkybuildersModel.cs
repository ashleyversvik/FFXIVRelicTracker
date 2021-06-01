using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._05_Skysteel._06_Skybuilders
{
    public class SkybuildersModel
    {
        public SkybuildersModel()
        {

        }
        public string SelectedJob { get; set; }
        public ObservableCollection<string> AvailableJobs { get; set; }
        public string ToolName { get; set; }
        public string CraftedMat { get; set; }
        public string FirstMat { get; set; }
        public string SecondMat { get; set; }
        public string ThirdMat { get; set; }
        public string TradedMat { get;  set; }
        public int MinRemainingYellowScrips { get;  set; }
        public int MaxRemainingYellowScrips { get;  set; }
        public string GatherLoc { get;  set; }
    }
}
