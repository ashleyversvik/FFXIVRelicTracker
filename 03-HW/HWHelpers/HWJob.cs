using FFXIVRelicTracker.Helpers;
using System.Collections.Generic;

namespace FFXIVRelicTracker._03_HW.HWHelpers
{
    public class HWJob : BaseJob
    {
        #region Constructors
        public HWJob()
        {

        }

        public HWJob(string name)
        {
            this.Name = name;

            Animated = false;
            Awoken = false;
            Anima = false;
            Hyperconductive = false;
            Reconditioned = false;
            Sharpened = false;
            Complete = false;
            Lux = false;
        }

        #endregion

        #region Fields
        private bool animated;
        private bool awoken;
        private bool anima;
        private bool hyperconductive;
        private bool reconditioned;
        private bool sharpened;
        private bool complete;
        private bool lux;
        #endregion

        #region Properties
        public List<bool> StageList = new List<bool>();

        public bool Animated
        {
            get { return animated; }
            set
            {
                animated = value;
                OnPropertyChanged(nameof(Animated));
            }
        }
        public bool Awoken
        {
            get { return awoken; }
            set
            {
                awoken = value;
                OnPropertyChanged(nameof(Awoken));
            }
        }
        public bool Anima
        {
            get { return anima; }
            set
            {
                anima = value;
                OnPropertyChanged(nameof(Anima));
            }
        }
        public bool Hyperconductive
        {
            get { return hyperconductive; }
            set
            {
                hyperconductive = value;
                OnPropertyChanged(nameof(Hyperconductive));
            }
        }
        public bool Reconditioned
        {
            get { return reconditioned; }
            set
            {
                reconditioned = value;
                OnPropertyChanged(nameof(Reconditioned));
            }
        }
        public bool Sharpened
        {
            get { return sharpened; }
            set
            {
                sharpened = value;
                OnPropertyChanged(nameof(Sharpened));
            }
        }
        public bool Complete
        {
            get { return complete; }
            set
            {
                complete = value;
                OnPropertyChanged(nameof(Complete));
            }
        }
        public bool Lux
        {
            get { return lux; }
            set
            {
                lux = value;
                OnPropertyChanged(nameof(Lux));
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

            for (int stageIndex = 0; stageIndex < HWInfo.StageListString.Count; stageIndex++)
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
                            Animated = tempProgress;
                            break;
                        case 1:
                            Awoken = tempProgress;
                            break;
                        case 2:
                            Anima = tempProgress;
                            break;
                        case 3:
                            Hyperconductive = tempProgress;
                            break;
                        case 4:
                            Reconditioned = tempProgress;
                            break;
                        case 5:
                            Sharpened = tempProgress;
                            break;
                        case 6:
                            Complete = tempProgress;
                            break;
                        case 7:
                            Lux = tempProgress;
                            break;
                    }
                }
            }

            StageList = tempList;

        }
        public void RefreshJob()
        {
            Animated = StageList[0];
            Awoken = StageList[1];
            Anima = StageList[2];
            Hyperconductive = StageList[3];
            Reconditioned = StageList[4];
            Sharpened = StageList[5];
            Complete = StageList[6];
            Lux = StageList[7];
        }
        #endregion
    }
}
