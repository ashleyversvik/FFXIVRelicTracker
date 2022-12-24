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

            Animated = new HWProgress("Animated");
            Awoken = new HWProgress("Awoken");
            Anima = new HWProgress("Anima");
            Hyperconductive = new HWProgress("Hyperconductive");
            Reconditioned = new HWProgress("Reconditioned");
            Sharpened = new HWProgress("Sharpened");
            Complete = new HWProgress("Complete");
            Lux = new HWProgress("Lux");
        }

        #endregion

        #region Fields
        private HWProgress animated;
        private HWProgress awoken;
        private HWProgress anima;
        private HWProgress hyperconductive;
        private HWProgress reconditioned;
        private HWProgress sharpened;
        private HWProgress complete;
        private HWProgress lux;
        #endregion

        #region Properties
        public List<HWProgress> StageList = new List<HWProgress>();

        public HWProgress Animated
        {
            get { return animated; }
            set
            {
                animated = value;
                OnPropertyChanged(nameof(Animated));
            }
        }
        public HWProgress Awoken
        {
            get { return awoken; }
            set
            {
                awoken = value;
                OnPropertyChanged(nameof(Awoken));
            }
        }
        public HWProgress Anima
        {
            get { return anima; }
            set
            {
                anima = value;
                OnPropertyChanged(nameof(Anima));
            }
        }
        public HWProgress Hyperconductive
        {
            get { return hyperconductive; }
            set
            {
                hyperconductive = value;
                OnPropertyChanged(nameof(Hyperconductive));
            }
        }
        public HWProgress Reconditioned
        {
            get { return reconditioned; }
            set
            {
                reconditioned = value;
                OnPropertyChanged(nameof(Reconditioned));
            }
        }
        public HWProgress Sharpened
        {
            get { return sharpened; }
            set
            {
                sharpened = value;
                OnPropertyChanged(nameof(Sharpened));
            }
        }
        public HWProgress Complete
        {
            get { return complete; }
            set
            {
                complete = value;
                OnPropertyChanged(nameof(Complete));
            }
        }
        public HWProgress Lux
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

            List<HWProgress> tempList = new List<HWProgress>();

            for (int stageIndex = 0; stageIndex < HWInfo.StageListString.Count; stageIndex++)
            {
                HWProgress tempProgress = null;
                if (stageIndex < StageList.Count && StageList[stageIndex] != null)
                {
                    tempProgress = StageList[stageIndex];
                    tempProgress.Name = HWInfo.StageListString[stageIndex];
                    tempList.Add(tempProgress);
                }
                else
                {
                    tempProgress = new HWProgress(HWInfo.StageListString[stageIndex]);

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
        #endregion
    }
}
