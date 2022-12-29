using FFXIVRelicTracker.Models.Helpers;

namespace FFXIVRelicTracker.Helpers
{
    public abstract class BaseStageModel : ObservableObject
    {
        public string SelectedJob { get; set; }
    }
}
