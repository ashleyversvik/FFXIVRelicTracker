using FFXIVRelicTracker.Helpers;
using System.Collections.ObjectModel;

namespace FFXIVRelicTracker._03_HW._02_Awoken
{
    public class AwokenModel : BaseStageModel
    {
        public AwokenModel()
        {
        }

        public ObservableCollection<bool> DungeonBools { get;  set; }
    }
}
