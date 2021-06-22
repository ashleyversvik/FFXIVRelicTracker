using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._04_SB._02_Anemos
{
    public class AnemosModel : ObservableObject
    {
        #region Fields

        #endregion

        #region Constructor
        public AnemosModel()
        {

        }

        public string SelectedJob { get; set; }
        public ObservableCollection<string> AvailableJobs { get;  set; }
        public ObservableCollection<bool> WeaponState { get; set; }
        public bool BuyFeathers { get; set; }
        public int ProteanCount { get;  set; }
        public int FeatherCount { get;  set; }
        #endregion

        #region Properties

        #endregion
    }
}
