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
            Manderville = new EWProgress("Manderville");
        }

        #endregion

        #region Properties
        public List<EWProgress> StageList = new List<EWProgress>();
        private EWProgress manderville;

        public EWProgress Manderville
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
            //This method is used to fix the situation where an existing character is loaded and a new stage was added since the character was initially created
            //Without checking and replacing the Progress lists and objects, the Progress object is null, regardless of the initiator being in the class constructor
            //  or in the field

            List<EWProgress> tempList = new List<EWProgress>();

            for (int stageIndex = 0; stageIndex < EWInfo.StageListString.Count; stageIndex++)
            {
                EWProgress tempProgress = null;
                if (stageIndex < StageList.Count && StageList[stageIndex] != null)
                {
                    tempProgress = StageList[stageIndex];
                    tempProgress.Name = EWInfo.StageListString[stageIndex];
                    tempList.Add(tempProgress);
                }
                else
                {
                    tempProgress = new EWProgress(EWInfo.StageListString[stageIndex]);

                    tempList.Add(tempProgress);

                    switch (stageIndex)
                    {
                        case 0:
                            Manderville = tempProgress;
                            break;

                    }
                }
            }

            StageList = tempList;
        }
        #endregion
    }
}
