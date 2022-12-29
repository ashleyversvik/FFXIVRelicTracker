﻿using FFXIVRelicTracker.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FFXIVRelicTracker._03_HW.HWHelpers
{
    public static class HWInfo
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
               "BRD",
               "MCH",
               "BLM",
               "SMN"
            };

        public static List<string> StageListString = new List<string>()
        {
           "Animated",
           "Awoken",
           "Anima",
           "Hyperconductive",
           "Reconditioned",
           "Sharpened",
           "Complete",
           "Lux"
        };

        #region Methods
        public static ObservableCollection<string> LoadJobs(ObservableCollection<string> jobs, Character selectedCharacter, string stage)
        {
            var AvailableJobs = jobs;
            int StageIndex = HWInfo.StageListString.IndexOf(stage);
            if (jobs == null) AvailableJobs = new ObservableCollection<string>();
            foreach (HWJob job in selectedCharacter.HWModel.HWJobList)
            {
                if (!job.StageList[StageIndex] & !AvailableJobs.Contains(job.Name))
                {
                    HWInfo.ReloadJobList(AvailableJobs, job.Name);
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

            int jobIndex = HWInfo.JobListString.IndexOf(jobName);
            switch (tempList.Count)
            {
                case 0:
                    tempList.Add(jobName);
                    break;
                case 1:
                    if (HWInfo.JobListString.IndexOf(tempList[0]) > jobIndex) { tempList.Insert(0, jobName); }
                    else { tempList.Add(jobName); }
                    break;
                default:
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        if (HWInfo.JobListString.IndexOf(tempList[i]) > jobIndex)
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

        #region Animated

        public static string ReturnAnimatedWeaponName(string job)
        {
            return JobAnimatedWeapons[job];
        }

        private static Dictionary<string, string> JobAnimatedWeapons = new Dictionary<string, string>()
        {
            {"PLD", "Animated Hauteclaire and Animated Prytwen" },
            {"WAR", "Animated Parashu"},
            {"DRK", "Animated Deathbringer"},
            {"WHM", "Animated Seraph Cane"},
            {"SCH", "Animated Elements"},
            {"AST", "Animated Atlas"},
            {"MNK", "Animates Rising Suns"},
            {"DRG", "Animated Brionac"},
            {"NIN", "Animated Yukimitsu"},
            {"BRD", "Animated Berimbau"},
            {"MCH", "Animated Ferdinand"},
            {"BLM", "Animated Lunaris Rod"},
            {"SMN", "Animated Almandal"}
        };

        #endregion

        #region Awoken

        public static string ReturnAwokenWeaponName(string job)
        {
            return JobAwokenWeapons[job];
        }

        private static Dictionary<string, string> JobAwokenWeapons = new Dictionary<string, string>()
        {
            {"PLD", "Hauteclaire Awoken and Prytwen Awoken" },
            {"WAR", "Parashu Awoken"},
            {"DRK", "Deathbringer Awoken"},
            {"WHM", "Seraph Cane Awoken"},
            {"SCH", "Elements Awoken"},
            {"AST", "Atlas Awoken"},
            {"MNK", "Rising Suns Awoken"},
            {"DRG", "Brionac Awoken"},
            {"NIN", "Yukimitsu Awoken"},
            {"BRD", "Berimbau Awoken"},
            {"MCH", "Ferdinand Awoken"},
            {"BLM", "Lunaris Rod Awoken"},
            {"SMN", "Almandal Awoken"}
        };

        #endregion

        #region Anima

        public static string ReturnAnimaWeaponName(string job)
        {
            return JobAnimaWeapons[job];
        }

        private static Dictionary<string, string> JobAnimaWeapons = new Dictionary<string, string>()
        {
            {"PLD", "Almace and Ancile"},
            {"WAR", "Ukonvasara"},
            {"DRK", "Nothung"},
            {"WHM", "Majestas"},
            {"SCH", "Tetrabiblos"},
            {"AST", "Deneb"},
            {"MNK", "Verethragna"},
            {"DRG", "Rhongomiant"},
            {"NIN", "Kannagi"},
            {"BRD", "Gandiva"},
            {"MCH", "Armageddon"},
            {"BLM", "Hvergelmir"},
            {"SMN", "Draconomicon"}
        };

        #endregion    
        
        #region Complete

        public static string ReturnCompleteWeaponName(string job)
        {
            return JobCompleteWeapons[job];
        }

        private static Dictionary<string, string> JobCompleteWeapons = new Dictionary<string, string>()
        {
            {"PLD", "Aettir and Priven"},
            {"WAR", "Minos"},
            {"DRK", "Cronus"},
            {"WHM", "Sindri"},
            {"SCH", "Anabasis"},
            {"AST", "Canopus"},
            {"MNK", "Nyepel"},
            {"DRG", "Aredbhar"},
            {"NIN", "Sandung"},
            {"BRD", "Terpander"},
            {"MCH", "Deathlocke"},
            {"BLM", "Kalanda"},
            {"SMN", "Mimesis"}
        };

#endregion
    }
}
