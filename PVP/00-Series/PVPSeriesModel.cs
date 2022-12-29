using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;

namespace FFXIVRelicTracker.PVP._00_Series
{
    public class PVPSeriesModel : ObservableObject
    {
        public PVPSeriesModel()
        {
        }

        public int DesiredLevel { get; set; }
        public int CurrentLevel { get; set; }
        public int CurrentExp { get; set; }

    }
}
