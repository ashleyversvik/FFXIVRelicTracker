using FFXIVRelicTracker.Models.Helpers;
using FFXIVRelicTracker.PVP._00_Series;

namespace FFXIVRelicTracker.PVP.Main
{
    public class PVPModel : ObservableObject
    {
        #region Fields

        private PVPSeriesModel pvpSeriesModel = new PVPSeriesModel();

        #endregion

        #region Constructors
        public PVPModel()
        {
        }

        #endregion

        #region Properties
        public PVPSeriesModel PVPSeriesModel { get { return pvpSeriesModel; } set { pvpSeriesModel = value; OnPropertyChanged(nameof(PVPSeriesModel)); } }
        #endregion

    }

}
