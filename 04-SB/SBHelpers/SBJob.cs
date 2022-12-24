using FFXIVRelicTracker.Helpers;
using System.Collections.Generic;

namespace FFXIVRelicTracker._04_SB.SBHelpers
{
    public class SBJob : BaseJob
    {
        #region Constructors
        public SBJob()
        {

        }

        public SBJob(string name)
        {
            this.Name = name;
            Antiquated = new SBProgress("Antiquated");
            Anemos = new SBProgress("Anemos");
            Elemental = new SBProgress("Elemental");
            Pyros = new SBProgress("Pyros");
            Eureka = new SBProgress("Eureka");
            Physeos = new SBProgress("Physeos");
        }

        #endregion

        #region Properties
        public List<SBProgress> StageList = new List<SBProgress>();
        private SBProgress antiquated;
        private SBProgress anemos;
        private SBProgress elemental;
        private SBProgress pyros;
        private SBProgress eureka;
        private SBProgress physeos;

        public SBProgress Antiquated
        {
            get { return antiquated; }
            set
            {
                antiquated = value;
                OnPropertyChanged(nameof(Antiquated));
            }
        }

        public SBProgress Anemos
        {
            get { return anemos; }
            set
            {
                anemos = value;
                OnPropertyChanged(nameof(Anemos));
            }
        }

        public SBProgress Elemental
        {
            get { return elemental; }
            set
            {
                elemental = value;
                OnPropertyChanged(nameof(Elemental));
            }
        }

        public SBProgress Pyros
        {
            get { return pyros; }
            set
            {
                pyros = value;
                OnPropertyChanged(nameof(Pyros));
            }
        }

        public SBProgress Eureka
        {
            get { return eureka; }
            set
            {
                eureka = value;
                OnPropertyChanged(nameof(Eureka));
            }
        }

        public SBProgress Physeos
        {
            get { return physeos; }
            set
            {
                physeos = value;
                OnPropertyChanged(nameof(Physeos));
            }
        }        
        #endregion

        #region Methods
        public void CheckObject()
        {
            //This method is used to fix the situation where an existing character is loaded and a new stage was added since the character was initially created
            //Without checking and replacing the Progress lists and objects, the Progress object is null, regardless of the initiator being in the class constructor
            //  or in the field

            List<SBProgress> tempList = new List<SBProgress>();

            for (int stageIndex = 0; stageIndex < SBInfo.StageListString.Count; stageIndex++)
            {
                SBProgress tempProgress = null;
                if (stageIndex < StageList.Count && StageList[stageIndex] != null) 
                {
                    tempProgress = StageList[stageIndex];
                    tempProgress.Name = SBInfo.StageListString[stageIndex];
                    tempList.Add(tempProgress);
                }
                else
                {
                    tempProgress = new SBProgress(SBInfo.StageListString[stageIndex]);

                    tempList.Add(tempProgress);

                    switch (stageIndex)
                    {
                        case 0:
                            Antiquated = tempProgress;
                            break;
                        case 1:
                            Anemos = tempProgress;
                            break;
                        case 2:
                            Elemental = tempProgress;
                            break;
                        case 3:
                            Pyros = tempProgress;
                            break;
                        case 4:
                            Eureka = tempProgress;
                            break;
                        case 5:
                            Physeos = tempProgress;
                            break;
                    }
                }
            }

            StageList = tempList;
        }
        #endregion
    }
}
