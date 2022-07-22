using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._06_EW._01_Placeholder
{
    public class PlaceholderModel: ObservableObject
    {
        public PlaceholderModel()
        {
        }

        public ObservableCollection<string> AvailableJobs { get; set; }
        public string CurrentPlaceholder{ get; set; }
        public int CurrentScalepowder { get; set; }
    }
}
