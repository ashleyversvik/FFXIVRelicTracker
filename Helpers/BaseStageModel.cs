using FFXIVRelicTracker.Models.Helpers;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace FFXIVRelicTracker.Helpers
{
    public abstract class BaseStageModel : ObservableObject
    {

        [JsonIgnore]
        public ObservableCollection<string> AvailableJobs { get; set; }
        public string SelectedJob { get; set; }
    }
}
