using FFXIVRelicTracker.Helpers;
using System.Collections.ObjectModel;

namespace FFXIVRelicTracker._04_SB._04_Pyros
{
    public class PyrosModel : BaseStageModel
    {
        public PyrosModel()
        {

        }

        public ObservableCollection<bool> WeaponState { get; set; }
        public bool BuyFlames { get; set; }
        public int PyrosCount { get;  set; }
        public int LogosCount { get; set; }
        public int FlamesCount { get;  set; }

    }
}
