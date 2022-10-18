using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker._06_EW.EWHelpers
{
    public class EWJob : ObservableObject
    {
        #region Constructors
        public EWJob()
        {

        }

        public EWJob(string name)
        {
            this.name = name;
            Manderville = new EWProgress("Manderville",Name);
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

        public List<EWProgress> StageList = new List<EWProgress>();
        private EWProgress manderville;

        public EWProgress Manderville
        {
            get { return manderville; }
            set
            {
                manderville = value;
                OnPropertyChanged(nameof(Manderville));
            }
        }

        #endregion

        #region Methods
        public void CheckObject()
        {
            //This method is used to fix the situation where an existing character is loaded and a new stage was added since the character was initially created
            //Without checking and replacing the Progress lists and objects, the Progress object is null, regardless of the initiator being in the class constructor
            //  or in the field

            List<EWProgress> tempList = new List<EWProgress>();

            for (int stageIndex = 0; stageIndex < EWInfo.StageListString.Count; stageIndex++)
            {
                EWProgress EWProgress = new EWProgress();
                if (stageIndex < StageList.Count && StageList[stageIndex] != null) { EWProgress = StageList[stageIndex]; }
                if (EWProgress.Name == null)
                {
                    EWProgress tempProgress = new EWProgress(EWInfo.StageListString[stageIndex], name);

                    tempList.Add(tempProgress);

                    switch (stageIndex)
                    {
                        case 0:
                            Manderville = tempProgress;
                            break;

                    }
                }
                else { tempList.Add(EWProgress); }
            }

            StageList = tempList;
        }
        #endregion
    }
}
