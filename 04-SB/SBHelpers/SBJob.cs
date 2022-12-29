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
            Antiquated = false;
            Anemos = false;
            Elemental = false;
            Pyros = false;
            Eureka = false;
            Physeos = false;
        }

        #endregion

        #region Properties
        public List<bool> StageList = new List<bool>();
        private bool antiquated;
        private bool anemos;
        private bool elemental;
        private bool pyros;
        private bool eureka;
        private bool physeos;

        public bool Antiquated
        {
            get { return antiquated; }
            set
            {
                antiquated = value;
                OnPropertyChanged(nameof(Antiquated));
            }
        }

        public bool Anemos
        {
            get { return anemos; }
            set
            {
                anemos = value;
                OnPropertyChanged(nameof(Anemos));
            }
        }

        public bool Elemental
        {
            get { return elemental; }
            set
            {
                elemental = value;
                OnPropertyChanged(nameof(Elemental));
            }
        }

        public bool Pyros
        {
            get { return pyros; }
            set
            {
                pyros = value;
                OnPropertyChanged(nameof(Pyros));
            }
        }

        public bool Eureka
        {
            get { return eureka; }
            set
            {
                eureka = value;
                OnPropertyChanged(nameof(Eureka));
            }
        }

        public bool Physeos
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

            List<bool> tempList = new List<bool>();

            for (int stageIndex = 0; stageIndex < SBInfo.StageListString.Count; stageIndex++)
            {
                bool tempProgress = false;
                if (stageIndex < StageList.Count) 
                {
                    tempProgress = StageList[stageIndex];
                    tempList.Add(tempProgress);
                }
                else
                {
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

        public void RefreshJob()
        {
            Antiquated = StageList[0];
            Anemos = StageList[1];
            Elemental = StageList[2];
            Pyros = StageList[3];
            Eureka = StageList[4];
            Physeos = StageList[5];
        }
        #endregion
    }
}
