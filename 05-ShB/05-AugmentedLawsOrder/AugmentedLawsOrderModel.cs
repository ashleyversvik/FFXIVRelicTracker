using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._05_ShB._05_AugmentedLawsOrder
{
    public class AugmentedLawsOrderModel : ObservableObject
    {
        #region Constructor
        public AugmentedLawsOrderModel()
        {

        }
        #endregion

        public ObservableCollection<string> AvailableJobs { get; set; }
        public string SelectedJob { get; set; }
        public int MemoryCount { get; set; }
    }
}
