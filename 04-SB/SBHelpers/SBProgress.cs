using FFXIVRelicTracker.Helpers;

namespace FFXIVRelicTracker._04_SB.SBHelpers
{
    public class SBProgress : BaseProgressClass
    {

        #region Constructors
        public SBProgress()
        {
            Progress = States.NA;
        }

        public SBProgress(string name)
            : this()
        {
            this.Name = name;
        }

        #endregion
    }
}
