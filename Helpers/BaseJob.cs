using FFXIVRelicTracker.Models.Helpers;
using System.Text.Json.Serialization;

namespace FFXIVRelicTracker.Helpers
{
    public class BaseJob : ObservableObject
    {

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }
}
