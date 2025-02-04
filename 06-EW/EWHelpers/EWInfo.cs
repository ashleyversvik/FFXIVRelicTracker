﻿using FFXIVRelicTracker.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FFXIVRelicTracker._06_EW.EWHelpers
{
    public static class EWInfo
    {
        #region Common
        public static List<string> JobListString = new List<string>
            {
               "PLD",
               "WAR",
               "DRK",
               "GNB",
               "WHM",
               "SCH",
               "AST",
               "SGE",
               "MNK",
               "DRG",
               "NIN",
               "SAM",
               "RPR",
               "BRD",
               "MCH",
               "DNC",
               "BLM",
               "SMN",
               "RDM"
            };

        public static List<string> StageListString = new List<string>()
        {
           "Manderville",
           "Amazing",
        };

        #region Methods
        public static ObservableCollection<string> LoadJobs(ObservableCollection<string> jobs, Character selectedCharacter, string stage)
        {
            var AvailableJobs = jobs;
            int StageIndex = EWInfo.StageListString.IndexOf(stage);
            if (jobs == null) AvailableJobs = new ObservableCollection<string>();
            foreach (EWJob job in selectedCharacter.EWModel.EWJobList)
            {
                if (!job.StageList[StageIndex] & !AvailableJobs.Contains(job.Name))
                {
                    EWInfo.ReloadJobList(AvailableJobs, job.Name);
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

            int jobIndex = EWInfo.JobListString.IndexOf(jobName);
            switch (tempList.Count)
            {
                case 0:
                    tempList.Add(jobName);
                    break;
                case 1:
                    if (EWInfo.JobListString.IndexOf(tempList[0]) > jobIndex) { tempList.Insert(0, jobName); }
                    else { tempList.Add(jobName); }
                    break;
                default:
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        if (EWInfo.JobListString.IndexOf(tempList[i]) > jobIndex)
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
