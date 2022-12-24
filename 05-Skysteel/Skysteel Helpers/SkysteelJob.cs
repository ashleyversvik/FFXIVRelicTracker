using FFXIVRelicTracker.Helpers;
using System.Collections.Generic;

namespace FFXIVRelicTracker._05_Skysteel.Skysteel_Helpers
{
    public class SkysteelJob : BaseJob
    {
        #region Fields
        private SkysteelProgress baseTool;
        private SkysteelProgress basePlus1;
        private SkysteelProgress dragonsung;
        private SkysteelProgress augmentedDragonsung;
        private SkysteelProgress skysung;
        private SkysteelProgress skybuilders;
        #endregion


        #region Constructors
        public SkysteelJob()
        {
        }

        public SkysteelJob(string name)
        {
            this.Name = name;

            BaseTool = new SkysteelProgress("BaseTool");
            BasePlus1 = new SkysteelProgress("BasePlus1");
            Dragonsung = new SkysteelProgress("Dragonsung");
            AugmentedDragonsung = new SkysteelProgress("AugmentedDragonsung");
            Skysung = new SkysteelProgress("Skysung");
            skybuilders = new SkysteelProgress("Skybuilders");
        }
        #endregion

        #region Properties
        public List<SkysteelProgress> StageList = new List<SkysteelProgress>();

        public SkysteelProgress BaseTool
        {
            get { return baseTool; }
            set
            {
                baseTool = value;
                OnPropertyChanged(nameof(BaseTool));
            }
        }
        public SkysteelProgress BasePlus1
        {
            get { return basePlus1; }
            set
            {
                basePlus1 = value;
                OnPropertyChanged(nameof(BasePlus1));
            }
        }
        public SkysteelProgress Dragonsung
        {
            get { return dragonsung; }
            set
            {
                dragonsung = value;
                OnPropertyChanged(nameof(Dragonsung));
            }
        }
        public SkysteelProgress AugmentedDragonsung
        {
            get { return augmentedDragonsung; }
            set
            {
                augmentedDragonsung = value;
                OnPropertyChanged(nameof(AugmentedDragonsung));
            }
        }
        public SkysteelProgress Skysung
        {
            get { return skysung; }
            set
            {
                skysung = value;
                OnPropertyChanged(nameof(Skysung));
            }
        }

        public SkysteelProgress Skybuilders
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


            List<SkysteelProgress> tempList = new List<SkysteelProgress>();

            for (int stageIndex = 0; stageIndex < StageList.Count; stageIndex++)
            {
                SkysteelProgress tempProgress = null;
                if (stageIndex < StageList.Count && StageList[stageIndex] != null)
                {
                    tempProgress = StageList[stageIndex];
                    tempProgress.Name = SkysteelInfo.StageListString[stageIndex];
                    tempList.Add(tempProgress);
                }
                else
                {
                    tempProgress = new SkysteelProgress(SkysteelInfo.StageListString[stageIndex]);

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
        #endregion

    }
}
