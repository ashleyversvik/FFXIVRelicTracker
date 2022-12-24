using FFXIVRelicTracker.Helpers;

namespace FFXIVRelicTracker._05_Skysteel.Skysteel_Helpers
{
    public class SkysteelProgress : BaseProgressClass
    {
        #region Constructors
        public SkysteelProgress()
        {
            Progress = States.NA;
        }

        public SkysteelProgress(string name)
            : this()
        {
            this.Name = name;
        }
        #endregion
    }
}
