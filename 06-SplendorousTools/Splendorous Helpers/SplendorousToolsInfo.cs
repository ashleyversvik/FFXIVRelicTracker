using FFXIVRelicTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FFXIVRelicTracker._06_SplendorousTools.Splendorous_Helpers
{
    public static class SplendorousToolsInfo
    {
        public static List<string> JobListString = new List<string>
            {
               "CRP",
               "BSM",
               "ARM",
               "GSM",
               "LTW",
               "WVR",
               "ALC",
               "CUL",
               "MIN",
               "BTN",
               "FSH"
            };

        public static List<string> JobNameListString = new List<string>
            {
               "Carpenter",
               "Blacksmith",
               "Armorer",
               "Goldsmith",
               "Leatherworker",
               "Weaver",
               "Alchemist",
               "Culinarian",
               "Miner",
               "Botanist",
               "Fisher"
            };

        public static List<string> StageListString = new List<string>()
        {
           "Splendorous",
           "AugmentedSplendorous",
           "Crystalline"
        };

        private static Dictionary<string, string> JobToToolDict= new Dictionary<string, string>
        {
            {"CRP", "Saw" },
            {"BSM", "Cross-pein Hammer" },
            {"ARM", "Raising Hammer"},
            {"GSM", "Mallet"},
            {"LTW", "Round Knife"},
            {"WVR", "Needle"},
            {"ALC", "Alembic"},
            {"CUL", "Frypan"},
            {"MIN", "Pickaxe" },
            {"BTN", "Hatchet"},
            {"FSH", "Fishing Rod"}
        };

        #region Methods
        public static ObservableCollection<string> LoadJobs(ObservableCollection<string> jobs, Character selectedCharacter, string stage)
        {
            var AvailableJobs = jobs;
            int StageIndex = SplendorousToolsInfo.StageListString.IndexOf(stage);
            if (jobs == null) AvailableJobs = new ObservableCollection<string>();
            foreach (SplendorousToolsJob job in selectedCharacter.SplendorousToolsModel.SplendorousToolsJobList)
            {
                if (!job.StageList[StageIndex] & !AvailableJobs.Contains(job.Name))
                {
                    SplendorousToolsInfo.ReloadJobList(AvailableJobs, job.Name);
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

            int jobIndex = SplendorousToolsInfo.JobListString.IndexOf(jobName);
            switch (tempList.Count)
            {
                case 0:
                    tempList.Add(jobName);
                    break;
                case 1:
                    if (SplendorousToolsInfo.JobListString.IndexOf(tempList[0]) > jobIndex) { tempList.Insert(0, jobName); }
                    else { tempList.Add(jobName); }
                    break;
                default:
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        if (SplendorousToolsInfo.JobListString.IndexOf(tempList[i]) > jobIndex)
                        {
                            tempList.Insert(i, jobName);
                            break;
                        }
                    }
                    if (!tempList.Contains(jobName)) { tempList.Add(jobName); }
                    break;
            }
        }
        public static string ReturnToolName(string job)
        {
            return JobToToolDict[job];
        }

        #region CompleteStages
        public static void ProgressClass(Character character, string job, string stage)
        {
            int StageIndex = SplendorousToolsInfo.StageListString.IndexOf(stage);
            int JobIndex = SplendorousToolsInfo.JobListString.IndexOf(job);

            SplendorousToolsJob tempJob = character.SplendorousToolsModel.SplendorousToolsJobList[JobIndex];
            var progress = tempJob.StageList[StageIndex];

            if (!progress)
            {
                CompletePreviousStages(tempJob, StageIndex);
            }
            else if (progress)
            {
                InCompleteFollowingStages(tempJob, StageIndex);
                tempJob.RefreshJob();
                return;
            }
            tempJob.StageList[StageIndex] = true;
            tempJob.RefreshJob();
        }
        private static void InCompleteFollowingStages(SplendorousToolsJob tempStage, int stageIndex)
        {
            for (int i = stageIndex; i < tempStage.StageList.Count; i++)
            {
                tempStage.StageList[i] = false;
            }
        }
        private static void CompletePreviousStages(SplendorousToolsJob tempStage, int stageIndex)
        {
            for (int i = 0; i < stageIndex; i++)
            {
                tempStage.StageList[i] = true;
            }
        }

        #endregion

        #endregion

        #region AugmentedSplendorous

        public static Tuple<string, string, string, string, string> ReturnAugmentedSplendorousTuple(string job)
        {
            return new Tuple<string, string, string, string, string>(JobToToolDict[job], JobNameListString[JobListString.IndexOf(job)], JobToAugmentedCraft[job], JobToFirstAugmentedMat[job], JobToSecondAugmentedMat[job]);
        }

        private static Dictionary<string, string> JobToAugmentedCraft = new Dictionary<string, string>
        {
            {"CRP", "Connoisseur's Chair" },
            {"BSM", "Connoisseur's Wall Lantern" },
            {"ARM", "Connoisseur's Ornate Door"},
            {"GSM", "Connoisseur's Vanity Mirror"},
            {"LTW", "Connoisseur's Rug"},
            {"WVR", "Connoisseur's Swag Valance"},
            {"ALC", "Connoisseur's Cosmetics Box"},
            {"CUL", "Connoisseur's Pixieberry Tea"},
            {"MIN", "" },
            {"BTN", ""},
            {"FSH", ""}
        };

        private static Dictionary<string, string> JobToFirstAugmentedMat = new Dictionary<string, string>
        {
            {"CRP", "Select Ironwood Lumber" },
            {"BSM", "Select Manganese Ingot" },
            {"ARM", "Select Titanium Plate"},
            {"GSM", "Select Crystal Glass"},
            {"LTW", "Select Smilodon Leather"},
            {"WVR", "Select Scarlet Moko Cloth"},
            {"ALC", "Select Hoptrap Leaf"},
            {"CUL", "Select Pixieberry"},
            {"MIN", "Connoisseur's Prismstone" },
            {"BTN", "Connoisseur's Wattle Petribark"},
            {"FSH", "Platinum Seahorse"}
        };

        private static Dictionary<string, string> JobToSecondAugmentedMat = new Dictionary<string, string>
        {
            {"CRP", "Ironwood Log" },
            {"BSM", "Phrygian Gold Ore" },
            {"ARM", "Ironwood Log"},
            {"GSM", "Bismuth Ore"},
            {"LTW", "Almasty Fur"},
            {"WVR", "Almasty Fur"},
            {"ALC", "Petalouda Scales"},
            {"CUL", "Sideritis Leaves"},
            {"MIN", "Splendorous Water Shard" },
            {"BTN", "Splendorous Earth Shard"},
            {"FSH", "Clavekeeper"}
        };

        #endregion

        #region Crystalline

        public static Tuple<string, string, string, string, string, string> ReturnCrystallineTuple(string job)
        {
            return new Tuple<string, string, string, string, string, string>(JobToToolDict[job], JobNameListString[JobListString.IndexOf(job)], JobToCrystallineCraft[job], JobToFirstCrystallineMat[job], JobToSecondCrystallineMat[job], JobToThirdCrystallineMat[job]);
        }

        private static Dictionary<string, string> JobToCrystallineCraft = new Dictionary<string, string>
        {
            {"CRP", "Connoisseur's Marimba" },
            {"BSM", "Connoisseur's Coffee Brewer" },
            {"ARM", "Connoisseur's Bench"},
            {"GSM", "Connoisseur's Astroscope"},
            {"LTW", "Connoisseur's Leather Sofa"},
            {"WVR", "Connoisseur's Red Carpet"},
            {"ALC", "Connoisseur's Elixir Bottle"},
            {"CUL", "Connoisseur's Pixieberry Tart"},
            {"MIN", "" },
            {"BTN", ""},
            {"FSH", ""}
        };

        private static Dictionary<string, string> JobToFirstCrystallineMat = new Dictionary<string, string>
        {
            {"CRP", "Select Ironwood Lumber" },
            {"BSM", "Select Manganese Ingot" },
            {"ARM", "Select Titanium Plate"},
            {"GSM", "Select Crystal Glass"},
            {"LTW", "Select Smilodon Leather"},
            {"WVR", "Select Scarlet Moko Cloth"},
            {"ALC", "Select Hoptrap Leaf"},
            {"CUL", "Select Pixieberry"},
            {"MIN", "Connoisseur's Red Malachite" },
            {"BTN", "Connoisseur's Levin Mint"},
            {"FSH", "Mirror Image"}
        };

        private static Dictionary<string, string> JobToSecondCrystallineMat = new Dictionary<string, string>
        {
            {"CRP", "Integral Lumber"},
            {"BSM", "Star Quartz"},
            {"ARM", "Integral Lumber"},
            {"GSM", "Star Quartz"},
            {"LTW", "Integral Lumber"},
            {"WVR", "AR-Caean Velvet"},
            {"ALC", "Grade 5 Vitality Alkahest"},
            {"CUL", "Dark Rye Flour"},
            {"MIN", "Adaptive Fire Crystal"},
            {"BTN", "Adaptive Lightning Crystal"},
            {"FSH", "Spangled Pirarucu"}
        };

        private static Dictionary<string, string> JobToThirdCrystallineMat = new Dictionary<string, string>
        {
            {"CRP", "Chondrite Ingot" },
            {"BSM", "Chondrite Ingot"},
            {"ARM", "Chondrite Ingot"},
            {"GSM", "Chondrite Ingot"},
            {"LTW", "AR-Caean Velvet"},
            {"WVR", "Ophiotauros Leather"},
            {"ALC", "Grade 5 Mind Alkahest"},
            {"CUL", "Palm Sugar"},
            {"MIN", ""},
            {"BTN", ""},
            {"FSH", ""}
        };

        #endregion

    }
}
