using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker._04_SB.SBHelpers
{
    public class SBJob : ObservableObject
    {
        #region Constructors
        public SBJob()
        {

        }

        public SBJob(string name)
        {
            this.name = name;
            Antiquated = new SBProgress("Antiquated", Name);
            Anemos = new SBProgress("Anemos", Name);
            Elemental = new SBProgress("Elemental", Name);
            Pyros = new SBProgress("Pyros", Name);
            Eureka = new SBProgress("Eureka", Name);
            Physeos = new SBProgress("Physeos", Name);
        }

        #endregion

        #region Fields
        private string name;
        #endregion

        #region Properties
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

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
                SBProgress SBProgress = new SBProgress();
                if (stageIndex < StageList.Count && StageList[stageIndex] != null) { SBProgress = StageList[stageIndex]; }
                if (SBProgress.Name == null)
                {
                    SBProgress tempProgress = new SBProgress(SBInfo.StageListString[stageIndex], name);

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
                else { tempList.Add(SBProgress); }
            }

            StageList = tempList;
        }
        #endregion
    }
}
