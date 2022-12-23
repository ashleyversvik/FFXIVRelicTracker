using FFXIVRelicTracker.Helpers;
using System.Collections.ObjectModel;

namespace FFXIVRelicTracker._04_SB._05_Eureka
{
    public class EurekaModel : BaseStageModel
    {
        public EurekaModel()
        {

        }

        public ObservableCollection<bool> WeaponState { get; set; }
        public int HydatosCount { get;  set; }
        public int ScaleCount { get;  set; }
    }
}
