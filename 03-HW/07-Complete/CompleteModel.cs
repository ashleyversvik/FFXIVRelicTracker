using FFXIVRelicTracker.Helpers;
using System.Collections.ObjectModel;

namespace FFXIVRelicTracker._03_HW._07_Complete
{
    public class CompleteModel:BaseStageModel
    {
        public CompleteModel()
        {
        }

        public ObservableCollection<bool> DungeonBools { get;  set; }
        public int Light { get; set; }
        public int CurrentPneumite { get; set; }
    }
}
