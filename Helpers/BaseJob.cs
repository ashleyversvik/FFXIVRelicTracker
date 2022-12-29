using FFXIVRelicTracker.Models.Helpers;

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
