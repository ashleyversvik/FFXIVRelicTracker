using FFXIVRelicTracker.Helpers;
using System.Collections.Generic;

namespace FFXIVRelicTracker._06_SplendorousTools.Splendorous_Helpers
{
    public class SplendorousToolsJob : BaseJob
    {
        #region Fields
        private bool splendorous;
        private bool augmentedSplendorous;
        private bool crystalline;
        #endregion


        #region Constructors
        public SplendorousToolsJob()
        {
        }

        public SplendorousToolsJob(string name)
        {
            this.Name = name;

            Splendorous = false;
        }
        #endregion

        #region Properties
        public List<bool> StageList = new List<bool>();

        public bool Splendorous
        {
            get { return splendorous; }
            set
            {
                splendorous = value;
                OnPropertyChanged(nameof(Splendorous));
            }
        }

        public bool AugmentedSplendorous
        {
            get { return augmentedSplendorous; }
            set
            {
                augmentedSplendorous = value;
                OnPropertyChanged(nameof(AugmentedSplendorous));
            }
        }

        public bool Crystalline
        {
            get { return crystalline; }
            set
            {
                crystalline = value;
                OnPropertyChanged(nameof(Crystalline));
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
                            Splendorous = tempProgress;
                            break;
                        case 1:
                            AugmentedSplendorous = tempProgress;
                            break;
                        case 2:
                            Crystalline = tempProgress;
                            break;
                    }
                }
            }
            StageList = tempList;

        }

        public void RefreshJob()
        {
            Splendorous = StageList[0];
            AugmentedSplendorous = StageList[1];
            Crystalline = StageList[2];
        }
        #endregion

    }
}
