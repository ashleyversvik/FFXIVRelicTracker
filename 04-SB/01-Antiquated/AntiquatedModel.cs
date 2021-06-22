using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._04_SB._01_Antiquated
{
    public class AntiquatedModel: ObservableObject
    {
        public AntiquatedModel()
        {
        }

        public ObservableCollection<string> AvailableJobs { get; set; }
        public string SelectedJob { get; set; }
    }
}
