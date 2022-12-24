using FFXIVRelicTracker.Helpers;
using System.Collections.ObjectModel;

namespace FFXIVRelicTracker._03_HW._08_Lux
{
    public class LuxModel:BaseStageModel
    {
        public LuxModel()
        {
        }

        public ObservableCollection<bool> DungeonBools { get;  set; }
        public int Ink { get;  set; }
    }
}
