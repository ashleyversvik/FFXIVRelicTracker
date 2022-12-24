using FFXIVRelicTracker.Helpers;

namespace FFXIVRelicTracker._06_EW.EWHelpers
{
    public class EWProgress : BaseProgressClass
    {

        #region Constructors
        public EWProgress()
        {
            Progress = States.NA;
        }

        public EWProgress(string name)
            : this()
        {
            this.Name = name;
        }

        #endregion
    }
}
