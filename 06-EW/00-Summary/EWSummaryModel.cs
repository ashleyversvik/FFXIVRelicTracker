using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;

namespace FFXIVRelicTracker._06_EW._00_Summary
{
    public class EWSummaryModel : ObservableObject
    {

        public EWSummaryModel(Character SelectedCharacter)
        {
            this.SelectedCharacter = SelectedCharacter;
        }

        private Character selectedCharacter;
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }
    }
}
