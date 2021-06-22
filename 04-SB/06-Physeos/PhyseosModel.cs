using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._04_SB._06_Physeos
{
    public class PhyseosModel : ObservableObject
    {
        #region Constructor
        public PhyseosModel()
        {

        }
        #endregion

        public ObservableCollection<string> AvailableJobs { get; set; }
        public string SelectedJob { get; set; }
        public int FragmentCount { get; set; }
    }
}
