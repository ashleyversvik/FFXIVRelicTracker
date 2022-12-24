using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FFXIVRelicTracker._04_SB.SBHelpers
{
    public static class SBInfo
    {
        #region Common
        public static List<string> JobListString = new List<string>
            {
               "PLD",
               "WAR",
               "DRK",
               "WHM",
               "SCH",
               "AST",
               "MNK",
               "DRG",
               "NIN",
               "SAM",
               "BRD",
               "MCH",
               "BLM",
               "SMN",
               "RDM"
            };

        public static List<string> StageListString = new List<string>()
        {
           "Antiquated",
           "Anemos",
           "Elemental",
           "Pyros",
           "Eureka",
           "Physeos"
        };

        #region Methods

        

        public static void ReloadJobList(ObservableCollection<string> tempList, string jobName)
        {
            //This method should be called from LoadAvailableJobs methods to add jobs back into the list to preserve their order

            int jobIndex = SBInfo.JobListString.IndexOf(jobName);
            switch (tempList.Count)
            {
                case 0:
                    tempList.Add(jobName);
                    break;
                case 1:
                    if (SBInfo.JobListString.IndexOf(tempList[0]) > jobIndex) { tempList.Insert(0, jobName); }
                    else { tempList.Add(jobName); }
                    break;
                default:
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        if (SBInfo.JobListString.IndexOf(tempList[i]) > jobIndex)
                        {
                            tempList.Insert(i, jobName);
                            break;
                        }
                    }
                    if (!tempList.Contains(jobName)) { tempList.Add(jobName); }
                    break;
            }
        }

        #endregion

        #endregion

    }
}
