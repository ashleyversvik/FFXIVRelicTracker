using FFXIVRelicTracker.Helpers;
using System.Collections.ObjectModel;

namespace FFXIVRelicTracker._04_SB._03_Elemental
{
    public class ElementalModel : BaseStageModel
    {
        public ElementalModel()
        {

        }

        public ObservableCollection<bool> WeaponState { get; set; }
        public bool BuyIce { get; set; }
        public int PagosCount { get;  set; }
        public int FrostedProteanCount { get; set; }
        public int IceCount { get;  set; }
    }
}
