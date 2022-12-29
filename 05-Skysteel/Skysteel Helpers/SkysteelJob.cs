using FFXIVRelicTracker.Helpers;
using System.Collections.Generic;

namespace FFXIVRelicTracker._05_Skysteel.Skysteel_Helpers
{
    public class SkysteelJob : BaseJob
    {
        #region Fields
        private bool baseTool;
        private bool basePlus1;
        private bool dragonsung;
        private bool augmentedDragonsung;
        private bool skysung;
        private bool skybuilders;
        #endregion


        #region Constructors
        public SkysteelJob()
        {
        }

        public SkysteelJob(string name)
        {
            this.Name = name;

            BaseTool = false;
            BasePlus1 = false;
            Dragonsung = false;
            AugmentedDragonsung = false;
            Skysung = false;
            skybuilders = false;
        }
        #endregion

        #region Properties
        public List<bool> StageList = new List<bool>();

        public bool BaseTool
        {
            get { return baseTool; }
            set
            {
                baseTool = value;
                OnPropertyChanged(nameof(BaseTool));
            }
        }
        public bool BasePlus1
        {
            get { return basePlus1; }
            set
            {
                basePlus1 = value;
                OnPropertyChanged(nameof(BasePlus1));
            }
        }
        public bool Dragonsung
        {
            get { return dragonsung; }
            set
            {
                dragonsung = value;
                OnPropertyChanged(nameof(Dragonsung));
            }
        }
        public bool AugmentedDragonsung
        {
            get { return augmentedDragonsung; }
            set
            {
                augmentedDragonsung = value;
                OnPropertyChanged(nameof(AugmentedDragonsung));
            }
        }
        public bool Skysung
        {
            get { return skysung; }
            set
            {
                skysung = value;
                OnPropertyChanged(nameof(Skysung));
            }
        }

        public bool Skybuilders
        {
            get { return skybuilders; }
            set
            {
                skybuilders = value;
                OnPropertyChanged(nameof(Skybuilders));
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

            for (int stageIndex = 0; stageIndex < StageList.Count; stageIndex++)
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
                            BaseTool = tempProgress;
                            break;
                        case 1:
                            BasePlus1 = tempProgress;
                            break;
                        case 2:
                            Dragonsung = tempProgress;
                            break;
                        case 3:
                            AugmentedDragonsung = tempProgress;
                            break;
                        case 4:
                            Skysung = tempProgress;
                            break;
                        case 5:
                            Skybuilders = tempProgress;
                            break;
                    }
                }
            }
            StageList = tempList;

        }

        public void RefreshJob()
        {
            BaseTool = StageList[0];
            BasePlus1 = StageList[1];
            Dragonsung = StageList[2];
            AugmentedDragonsung = StageList[3];
            Skysung = StageList[4];
            Skybuilders = StageList[5];
        }
        #endregion

    }
}
