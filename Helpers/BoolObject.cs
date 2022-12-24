using FFXIVRelicTracker.Models.Helpers;

namespace FFXIVRelicTracker.Helpers
{

    public class BoolObject : ObservableObject
    {
        private bool internalBool;
        public BoolObject()
        {
            internalBool = false;
        }
        public bool Bool
        {
            get { return internalBool; }
            set
            {
                internalBool = value;
                OnPropertyChanged(nameof(Bool));
            }
        }
    }
}
