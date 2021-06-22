using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._04_SB._05_Eureka
{
    public class EurekaModel : ObservableObject
    {
        #region Fields

        #endregion

        #region Constructor
        public EurekaModel()
        {

        }

        public string SelectedJob { get; set; }
        public ObservableCollection<string> AvailableJobs { get;  set; }
        public ObservableCollection<bool> WeaponState { get; set; }
        public int HydatosCount { get;  set; }
        public int ScaleCount { get;  set; }
        #endregion

        #region Properties

        #endregion
    }
}
