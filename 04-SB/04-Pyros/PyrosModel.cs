using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._04_SB._04_Pyros
{
    public class PyrosModel : ObservableObject
    {
        #region Fields

        #endregion

        #region Constructor
        public PyrosModel()
        {

        }

        public string SelectedJob { get; set; }
        public ObservableCollection<string> AvailableJobs { get;  set; }
        public ObservableCollection<bool> WeaponState { get; set; }
        public bool BuyFlames { get; set; }
        public int PyrosCount { get;  set; }
        public int LogosCount { get; set; }
        public int FlamesCount { get;  set; }
        #endregion

        #region Properties

        #endregion
    }
}
