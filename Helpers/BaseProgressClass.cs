using FFXIVRelicTracker.Models.Helpers;
using System.Text.Json.Serialization;

namespace FFXIVRelicTracker.Helpers
{
    public class BaseProgressClass : ObservableObject
    {
        
        private string name;
        private States progress = States.NA;
        [JsonIgnore]
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public States Progress
        {
            get { return progress; }
            set
            {
                progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }
        public enum States
        {
            NA,
            Initiated,
            Completed
        }
    }
}
