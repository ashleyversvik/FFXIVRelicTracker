using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._04_SB._03_Elemental
{
    public class ElementalModel : ObservableObject
    {
        #region Fields

        #endregion

        #region Constructor
        public ElementalModel()
        {

        }

        public string SelectedJob { get; set; }
        public ObservableCollection<string> AvailableJobs { get;  set; }
        public ObservableCollection<bool> WeaponState { get; set; }
        public bool BuyIce { get; set; }
        public int PagosCount { get;  set; }
        public int FrostedProteanCount { get; set; }
        public int IceCount { get;  set; }
        #endregion

        #region Properties

        #endregion
    }
}
