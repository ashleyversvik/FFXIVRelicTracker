using FFXIVRelicTracker.Helpers;
using System.Collections.Generic;

namespace FFXIVRelicTracker._02_ARR.ArrHelpers
{
    public class ArrJob : BaseJob
    {
        public ArrJob()
        {
        }


        public ArrJob(string name)
        {
            this.Name = name;

            relic = false;
            zenith = false;
            atma = false;
            animus = false;
            novus = false;
            nexus = false;
            braves = false;
            zeta = false;
        }
        public List<bool> StageList = new List<bool>();

        private bool relic;
        private bool zenith;
        private bool atma;
        private bool animus;
        private bool novus;
        private bool nexus;
        private bool braves;
        private bool zeta;

        public bool Relic
        {
            get { return relic; }
            set
            {relic = value; OnPropertyChanged(nameof(Relic)); }
        }
        public bool Zenith
        {
            get { return zenith; }
            set { zenith = value; OnPropertyChanged(nameof(Zenith)); }
        }
        public bool Atma
        {
            get { return atma; }
            set { atma = value; OnPropertyChanged(nameof(Atma)); }
        }
        public bool Animus
        {
            get { return animus; }
            set { animus = value; OnPropertyChanged(nameof(Animus)); }
        }
        public bool Novus
        {
            get { return novus; }
            set { novus = value; OnPropertyChanged(nameof(Novus)); }
        }
        public bool Nexus
        {
            get { return nexus; }
            set { nexus = value; OnPropertyChanged(nameof(Nexus)); }
        }
        public bool Braves
        {
            get { return braves; }
            set { braves = value; OnPropertyChanged(nameof(Braves)); }
        }
        public bool Zeta
        {
            get { return zeta; }
            set { zeta = value; OnPropertyChanged(nameof(Zeta)); }
        }

        public void CheckObject()
        {
            //This method is used to fix the situation where an existing character is loaded and a new stage was added since the character was initially created
            //Without checking and replacing the Progress lists and objects, the Progress object is null, regardless of the initiator being in the class constructor
            //  or in the field

            List<bool> tempList = new List<bool>();

            for (int stageIndex = 0; stageIndex < ArrInfo.StageListString.Count; stageIndex++)
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
                            Relic = tempProgress;
                            break;
                        case 1:
                            Zenith = tempProgress;
                            break;
                        case 2:
                            Atma = tempProgress;
                            break;
                        case 3:
                            Animus = tempProgress;
                            break;
                        case 4:
                            Novus = tempProgress;
                            break;
                        case 5:
                            Nexus = tempProgress;
                            break;
                        case 6:
                            Braves = tempProgress;
                            break;
                        case 7:
                            Zeta = tempProgress;
                            break;
                    }
                }
            }

            StageList = tempList;

        }
        public void RefreshJob()
        {
            Relic = StageList[0];
            Zenith = StageList[1];
            Atma = StageList[2];
            Animus = StageList[3];
            Novus = StageList[4];
            Nexus = StageList[5];
            Braves = StageList[6];
            Zeta = StageList[7];
        }
    }
}
