using FFXIVRelicTracker.Helpers;
using System.Collections.Generic;

namespace FFXIVRelicTracker._06_EW.EWHelpers
{
    public class EWJob : BaseJob
    {
        #region Constructors
        public EWJob()
        {

        }

        public EWJob(string name)
        {
            this.Name = name;
            Manderville = false;
        }

        #endregion

        #region Properties
        public List<bool> StageList = new List<bool>();
        private bool manderville;

        public bool Manderville
        {
            get { return manderville; }
            set
            {
                manderville = value;
                OnPropertyChanged(nameof(Manderville));
            }
        }

        #endregion

        #region Methods
        public void CheckObject()
        {
            List<bool> tempList = new List<bool>();

            for (int stageIndex = 0; stageIndex < EWInfo.StageListString.Count; stageIndex++)
            {
                bool tempProgress = false;
                if (stageIndex < StageList.Count)
                {
                    tempProgress = StageList[stageIndex];
                    tempList.Add(tempProgress);
                }
                else
                {
                    tempProgress = false;
                    tempList.Add(tempProgress);
                }
                switch (stageIndex)
                {
                    case 0:
                        Manderville = tempProgress;
                        break;

                }
            }

            StageList = tempList;
        }

        public void RefreshJob()
        {
            Manderville = StageList[0];
        }
        #endregion
    }
}
