using FFXIVRelicTracker.Helpers;
using System.Collections.ObjectModel;

namespace FFXIVRelicTracker._04_SB._02_Anemos
{
    public class AnemosModel : BaseStageModel
    {

        public AnemosModel()
        {

        }

        public ObservableCollection<bool> WeaponState { get; set; }
        public bool BuyFeathers { get; set; }
        public int ProteanCount { get;  set; }
        public int FeatherCount { get;  set; }

    }
}
