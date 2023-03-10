using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;

namespace FFXIVRelicTracker._06_SplendorousTools._00_Summary
{
    class SplendorousToolsSummaryModel : ObservableObject
    {
        private Character selectedCharacter;

        public SplendorousToolsSummaryModel(Character selectedCharacter)
        {
            this.selectedCharacter = selectedCharacter;
        }
    }
}
