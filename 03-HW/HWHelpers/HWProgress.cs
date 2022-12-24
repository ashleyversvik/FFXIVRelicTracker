using FFXIVRelicTracker.Helpers;

namespace FFXIVRelicTracker._03_HW.HWHelpers
{
    public class HWProgress : BaseProgressClass
    {

        #region Constructors
        public HWProgress()
        {
            Progress = States.NA;
        }

        public HWProgress(string name)
            : this()
        {
            this.Name = name;
        }

        #endregion
    }
}
