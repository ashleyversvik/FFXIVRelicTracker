using FFXIVRelicTracker.Helpers;
using System.Collections.Generic;

namespace FFXIVRelicTracker._05_ShB.ShBHelpers
{
    public class ShBJob : BaseJob
    {
        #region Constructors
        public ShBJob()
        { }
        public ShBJob(string name)
        {
            this.Name = name;
            Resistance = false;
            AugmentedResistance = false;
            Recollection = false;
            LawsOrder = false;
            AugmentedLawsOrder = false;
            Blades = false;
        }

        #endregion

        #region Properties
        public List<bool> StageList = new List<bool>();
        private bool resistance;
        private bool augmentedResistance;
        private bool recollection;
        private bool lawsOrder;
        private bool augmentedLawsOrder;
        private bool blades;

        public bool Resistance
        {
            get { return resistance; }
            set
            {
                resistance = value;
                OnPropertyChanged(nameof(Resistance));
            }
        }

        public bool AugmentedResistance
        {
            get { return augmentedResistance; }
            set
            {
                augmentedResistance = value;
                OnPropertyChanged(nameof(AugmentedResistance));
            }
        }

        public bool Recollection
        {
            get { return recollection; }
            set
            {
                recollection = value;
                OnPropertyChanged(nameof(Recollection));
            }
        }

        public bool LawsOrder
        {
            get { return lawsOrder; }
            set
            {
                lawsOrder = value;
                OnPropertyChanged(nameof(LawsOrder));
            }
        }

        public bool AugmentedLawsOrder
        {
            get { return augmentedLawsOrder; }
            set
            {
                augmentedLawsOrder = value;
                OnPropertyChanged(nameof(AugmentedLawsOrder));
            }
        }

        public bool Blades
        {
            get { return blades; }
            set
            {
                blades = value;
                OnPropertyChanged(nameof(Blades));
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

            for (int stageIndex = 0; stageIndex < ShBInfo.StageListString.Count; stageIndex++)
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

                    switch (stageIndex)
                    {
                        case 0:
                            Resistance = tempProgress;
                            break;
                        case 1:
                            AugmentedResistance = tempProgress;
                            break;
                        case 2:
                            Recollection = tempProgress;
                            break;
                        case 3:
                            LawsOrder = tempProgress;
                            break;
                        case 4:
                            AugmentedLawsOrder = tempProgress;
                            break;
                        case 5:
                            Blades = tempProgress;
                            break;
                    }
                }
            }

            StageList = tempList;
        }

        public void RefreshJob()
        {
            Resistance = StageList[0];
            AugmentedResistance = StageList[1];
            Recollection = StageList[2];
            LawsOrder = StageList[3];
            AugmentedLawsOrder = StageList[4];
            Blades = StageList[5];
        }
        #endregion
    }
}
