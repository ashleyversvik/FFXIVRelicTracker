using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._06_EW._01_Manderville
{
    public class MandervilleModel: ObservableObject
    {
        public MandervilleModel()
        {
        }

        public ObservableCollection<string> AvailableJobs { get; set; }
        public string CurrentManderville{ get; set; }
        public int CurrentMeteorites { get; set; }
    }
}
