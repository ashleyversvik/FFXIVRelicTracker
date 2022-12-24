using FFXIVRelicTracker.Helpers;

namespace FFXIVRelicTracker._02_ARR.ArrHelpers
{

    public class ArrProgress : BaseProgressClass
    {
        
        public ArrProgress()
        {
            Progress = States.NA;
        }
        public ArrProgress(string name)
            :this()
        {
            this.Name = name;
        }

        public ArrProgress(ArrProgress arrProgress, string name)
        {
            this.Name = name;
            this.Progress = arrProgress.Progress;
        }

    }
}
