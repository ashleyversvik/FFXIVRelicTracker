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

            relic = new ArrProgress("Relic");
            zenith = new ArrProgress("Zenith");
            atma = new ArrProgress("Atma");
            animus = new ArrProgress("Animus");
            novus = new ArrProgress("Novus");
            nexus = new ArrProgress("Nexus");
            braves = new ArrProgress("Braves");
            zeta = new ArrProgress("Zeta");
        }
        public List<ArrProgress> StageList = new List<ArrProgress>();

        private ArrProgress relic;
        private ArrProgress zenith;
        private ArrProgress atma;
        private ArrProgress animus;
        private ArrProgress novus;
        private ArrProgress nexus;
        private ArrProgress braves;
        private ArrProgress zeta;

        public ArrProgress Relic
        {
            get { return relic; }
            set
            {relic = value; OnPropertyChanged(nameof(Relic)); }
        }
        public ArrProgress Zenith
        {
            get { return zenith; }
            set { zenith = value; OnPropertyChanged(nameof(Zenith)); }
        }
        public ArrProgress Atma
        {
            get { return atma; }
            set { atma = value; OnPropertyChanged(nameof(Atma)); }
        }
        public ArrProgress Animus
        {
            get { return animus; }
            set { animus = value; OnPropertyChanged(nameof(Animus)); }
        }
        public ArrProgress Novus
        {
            get { return novus; }
            set { novus = value; OnPropertyChanged(nameof(Novus)); }
        }
        public ArrProgress Nexus
        {
            get { return nexus; }
            set { nexus = value; OnPropertyChanged(nameof(Nexus)); }
        }
        public ArrProgress Braves
        {
            get { return braves; }
            set { braves = value; OnPropertyChanged(nameof(Braves)); }
        }
        public ArrProgress Zeta
        {
            get { return zeta; }
            set { zeta = value; OnPropertyChanged(nameof(Zeta)); }
        }

        public void CheckObject()
        {
            //This method is used to fix the situation where an existing character is loaded and a new stage was added since the character was initially created
            //Without checking and replacing the Progress lists and objects, the Progress object is null, regardless of the initiator being in the class constructor
            //  or in the field

            List<ArrProgress> tempList = new List<ArrProgress>();

            for (int stageIndex = 0; stageIndex < ArrInfo.StageListString.Count; stageIndex++)
            {
                ArrProgress tempProgress = null;
                if (stageIndex < StageList.Count && StageList[stageIndex] != null)
                {
                    tempProgress = StageList[stageIndex];
                    tempProgress.Name = ArrInfo.StageListString[stageIndex];
                    tempList.Add(tempProgress);
                }
                else
                {
                    tempProgress = new ArrProgress(ArrInfo.StageListString[stageIndex]);

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
    }
}
