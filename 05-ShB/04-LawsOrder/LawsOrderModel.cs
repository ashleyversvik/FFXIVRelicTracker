using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._05_ShB._04_LawsOrder
{
    public class LawsOrderModel : ObservableObject
    {
        #region Constructor
        public LawsOrderModel()
        {

        }
        #endregion

        public ObservableCollection<string> AvailableJobs { get; set; }
        public string SelectedJob { get; set; }
        public int MemoryCount { get; set; }
    }
}
