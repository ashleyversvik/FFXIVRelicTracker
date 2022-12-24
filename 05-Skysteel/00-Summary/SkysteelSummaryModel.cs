using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;

namespace FFXIVRelicTracker._05_Skysteel._00_Summary
{
    class SkysteelSummaryModel : ObservableObject
    {
        private Character selectedCharacter;

        public SkysteelSummaryModel(Character selectedCharacter)
        {
            this.selectedCharacter = selectedCharacter;
        }
    }
}
