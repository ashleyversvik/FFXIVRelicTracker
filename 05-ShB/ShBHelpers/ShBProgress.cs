using FFXIVRelicTracker.Helpers;

namespace FFXIVRelicTracker._05_ShB.ShBHelpers
{
    public class ShBProgress : BaseProgressClass
    {

        #region Constructors
        public ShBProgress()
        {
            Progress = States.NA;
        }

        public ShBProgress(string name)
            : this()
        {
            this.Name = name;
        }

        #endregion
    }
}
