using FFXIVRelicTracker.Models;
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
        public static ObservableCollection<string> LoadJobs(ObservableCollection<string> jobs, Character selectedCharacter, string stage)
        {
            var AvailableJobs = jobs;
            int StageIndex = SBInfo.StageListString.IndexOf(stage);
            if (jobs == null) AvailableJobs = new ObservableCollection<string>();
            foreach (SBJob job in selectedCharacter.SBModel.SBJobList)
            {
                if (!job.StageList[StageIndex] & !AvailableJobs.Contains(job.Name))
                {
                    SBInfo.ReloadJobList(AvailableJobs, job.Name);
                }
                if (job.StageList[StageIndex] & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
            }
            return AvailableJobs;
        }
        private static void ReloadJobList(ObservableCollection<string> tempList, string jobName)
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
