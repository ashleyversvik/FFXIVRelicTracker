using FFXIVRelicTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FFXIVRelicTracker._05_Skysteel.Skysteel_Helpers
{
    public static class SkysteelInfo
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
           "BaseTool",
           "BasePlus1",
           "Dragonsung",
           "AugmentedDragonsung",
           "Skysung",
           "Skybuilders"
        };

        private static Dictionary<string, string> JobToToolDict= new Dictionary<string, string>
        {
            {"CRP", "Saw" },
            {"BSM", "Cross-pein Hammer" },
            {"ARM", "Raising Hammer"},
            {"GSM", "Lapidary Hammer"},
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
            int StageIndex = SkysteelInfo.StageListString.IndexOf(stage);
            if (jobs == null) AvailableJobs = new ObservableCollection<string>();
            foreach (SkysteelJob job in selectedCharacter.SkysteelModel.SkysteelJobList)
            {
                if (!job.StageList[StageIndex] & !AvailableJobs.Contains(job.Name))
                {
                    SkysteelInfo.ReloadJobList(AvailableJobs, job.Name);
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

            int jobIndex = SkysteelInfo.JobListString.IndexOf(jobName);
            switch (tempList.Count)
            {
                case 0:
                    tempList.Add(jobName);
                    break;
                case 1:
                    if (SkysteelInfo.JobListString.IndexOf(tempList[0]) > jobIndex) { tempList.Insert(0, jobName); }
                    else { tempList.Add(jobName); }
                    break;
                default:
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        if (SkysteelInfo.JobListString.IndexOf(tempList[i]) > jobIndex)
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
            int StageIndex = SkysteelInfo.StageListString.IndexOf(stage);
            int JobIndex = SkysteelInfo.JobListString.IndexOf(job);

            SkysteelJob tempJob = character.SkysteelModel.SkysteelJobList[JobIndex];
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
        private static void InCompleteFollowingStages(SkysteelJob tempStage, int stageIndex)
        {
            for (int i = stageIndex; i < tempStage.StageList.Count; i++)
            {
                tempStage.StageList[i] = false;
            }
        }
        private static void CompletePreviousStages(SkysteelJob tempStage, int stageIndex)
        {
            for (int i = 0; i < stageIndex; i++)
            {
                tempStage.StageList[i] = true;
            }
        }

        #endregion

        #endregion

        #region BasePlus1

        public static Tuple<string, string, string, string> ReturnBasePlus1Tuple(string job)
        {
            return new Tuple<string, string, string, string>(JobToToolDict[job], JobToPlus1Craft[job], JobToFirstPlus1Mat[job], JobToSecondPlus1Mat[job]);
        }

        private static Dictionary<string, string> JobToPlus1Craft = new Dictionary<string, string>
        {
            {"CRP", "Oddly Specific Petrified Orb" },
            {"BSM", "Oddly Specific Rivets" },
            {"ARM", "Oddly Specific Wire"},
            {"GSM", "Oddly Specific Whetstone"},
            {"LTW", "Oddly Specific Leather"},
            {"WVR", "Oddly Specific Moonbeam Silk"},
            {"ALC", "Oddly Specific Synthetic Resin"},
            {"CUL", "Oddly Specific Seed Extract"},
            {"MIN", "" },
            {"BTN", ""},
            {"FSH", ""}
        };

        private static Dictionary<string, string> JobToFirstPlus1Mat = new Dictionary<string, string>
        {
            {"CRP", "Oddly Specific Petrified Log" },
            {"BSM", "Oddly Specific Iron Sand" },
            {"ARM", "Oddly Specific Iron Ore"},
            {"GSM", "Oddly Specific Uncut Gemstone"},
            {"LTW", "Oddly Specific Skin"},
            {"WVR", "Oddly Specific Cotton"},
            {"ALC", "Oddly Specific Quartz"},
            {"CUL", "Oddly Specific Seeds"},
            {"MIN", "Oddly Specific Obsidian" },
            {"BTN", "Oddly Specific Latex"},
            {"FSH", "Thinker's Coral"}
        };

        private static Dictionary<string, string> JobToSecondPlus1Mat = new Dictionary<string, string>
        {
            {"CRP", "White Ash Log" },
            {"BSM", "Manasilver Sand" },
            {"ARM", "Manasilver Sand"},
            {"GSM", "Manasilver Sand"},
            {"LTW", "Atrociraptor Skin"},
            {"WVR", "Pixie Floss Boll"},
            {"ALC", "Vampire Vine Sap"},
            {"CUL", "Highland Spring Water"},
            {"MIN", "Oddly Specific Mineral Sand" },
            {"BTN", "Oddly Specific Fossil Dust)"},
            {"FSH", ""}
        };

        #endregion

        #region Dragonsung

        public static Tuple<string, string, string, string> ReturnDragonsungTuple(string job)
        {
            return new Tuple<string, string, string, string>(JobToToolDict[job], JobToDragonsungCraft[job], JobToFirstDragonsungMat[job], JobToSecondDragonsungMat[job]);
        }

        private static Dictionary<string, string> JobToDragonsungCraft = new Dictionary<string, string>
        {
            {"CRP", "Oddly Specific Shaft"},
            {"BSM", "Oddly Specific Fitting"},
            {"ARM", "Oddly Specific Chainmail Sheet"},
            {"GSM", "Oddly Specific Gemstone"},
            {"LTW", "Oddly Specific Vellum"},
            {"WVR", "Oddly Specific Velvet"},
            {"ALC", "Oddly Specific Glass"},
            {"CUL", "Oddly Specific Seed Flour"},
            {"MIN", "" },
            {"BTN", ""},
            {"FSH", ""}
        };

        private static Dictionary<string, string> JobToFirstDragonsungMat = new Dictionary<string, string>
        {
            {"CRP", "Oddly Specific Petrified Log" },
            {"BSM", "Oddly Specific Iron Sand" },
            {"ARM", "Oddly Specific Iron Ore"},
            {"GSM", "Oddly Specific Uncut Gemstone"},
            {"LTW", "Oddly Specific Skin"},
            {"WVR", "Oddly Specific Cotton"},
            {"ALC", "Oddly Specific Quartz"},
            {"CUL", "Oddly Specific Seeds"},
            {"MIN", "Oddly Specific Dark Matter" },
            {"BTN", "Oddly Specific Amber"},
            {"FSH", "Dragonspine"}
        };

        private static Dictionary<string, string> JobToSecondDragonsungMat = new Dictionary<string, string>
        {
            {"CRP", "Sandteak Lumber"},
            {"BSM", "Titanbronze Ingot"},
            {"ARM", "Titanbronze Ingot"},
            {"GSM", "Tuff Whetstone"},
            {"LTW", "Zonure Leather"},
            {"WVR", "Ovim Wool"},
            {"ALC", "Refined Natron"},
            {"CUL", "Night Vinegar"},
            {"MIN", "Oddly Specific Striking Stone"},
            {"BTN", "Oddly Specific Bauble"},
            {"FSH", ""}
        };

        #endregion

        #region AugmentedDragonsung

        public static Tuple<string, string, string, string, string> ReturnAugmentedDragonsungTuple(string job)
        {
            return new Tuple<string, string, string, string, string>(JobToToolDict[job], JobNameListString[JobListString.IndexOf(job)], JobToAugmentedDragonsungCraft[job], JobToFirstAugmentedDragonsungMat[job], JobToSecondAugmentedDragonsungMat[job]);
        }

        private static Dictionary<string, string> JobToAugmentedDragonsungCraft = new Dictionary<string, string>
        {
            {"CRP", "Oddly Specific Cedar Lumber"},
            {"BSM", "Oddly Specific Iron Nails"},
            {"ARM", "Oddly Specific Mythril Rings"},
            {"GSM", "Oddly Specific Silver Nuggets"},
            {"LTW", "Oddly Specific Gagana Syrup"},
            {"WVR", "Oddly Specific Cloth"},
            {"ALC", "Oddly Specific Cedar Glue"},
            {"CUL", "Oddly Specific Cedar Oil"},
            {"MIN", "" },
            {"BTN", ""},
            {"FSH", ""}
        };

        private static Dictionary<string, string> JobToFirstAugmentedDragonsungMat = new Dictionary<string, string>
        {
            {"CRP", "Oddly Specific Cedar Log" },
            {"BSM", "Oddly Specific Coerthan Iron Ore" },
            {"ARM", "Oddly Specific Mythrite Sand"},
            {"GSM", "Oddly Specific Silver Ore"},
            {"LTW", "Oddly Specific Gagana Skin"},
            {"WVR", "Oddly Specific Fleece"},
            {"ALC", "Oddly Specific Sap"},
            {"CUL", "Oddly Specific Aloe"},
            {"MIN", "Oddly Specific Schorl" },
            {"BTN", "Oddly Specific Dark Chestnut Log"},
            {"FSH", "Petal Shell"}
        };

        private static Dictionary<string, string> JobToSecondAugmentedDragonsungMat = new Dictionary<string, string>
        {
            {"CRP", "Lignum Vitae Log"},
            {"BSM", "Dimythrite Ore"},
            {"ARM", "Dimythrite Ore"},
            {"GSM", "Dimythrite Sand"},
            {"LTW", "Yellow Alumen"},
            {"WVR", "Dwarven Cotton Boll"},
            {"ALC", "Vampire Cup Vine"},
            {"CUL", "Frantoio"},
            {"MIN", "Oddly Specific Landborne Aethersand"},
            {"BTN", "Oddly Specific Leafborne Aethersand"},
            {"FSH", ""}
        };

        #endregion

        #region Skysung

        public static Tuple<string, string, string, string, string> ReturnSkysungTuple(string job)
        {
            return new Tuple<string, string, string, string, string>(JobToToolDict[job], JobNameListString[JobListString.IndexOf(job)], JobToSkysungCraft[job], JobToFirstSkysungMat[job], JobToSecondSkysungMat[job]);
        }

        private static Dictionary<string, string> JobToSkysungCraft = new Dictionary<string, string>
        {
            {"CRP", "Oddly Specific Cedar Lumber"},
            {"BSM", "Oddly Specific Iron Ingots"},
            {"ARM", "Oddly Specific Mythril PLate"},
            {"GSM", "Oddly Specific Silver Ingot"},
            {"LTW", "Oddly Specific Gagana Leather"},
            {"WVR", "Oddly Specific Undyed Woolen Cloth"},
            {"ALC", "Oddly Specific Rubber"},
            {"CUL", "Oddly Specific Paste"},
            {"MIN", "" },
            {"BTN", ""},
            {"FSH", ""}
        };

        private static Dictionary<string, string> JobToFirstSkysungMat = new Dictionary<string, string>
        {
            {"CRP", "Oddly Specific Cedar Log" },
            {"BSM", "Oddly Specific Coerthan Iron Ore" },
            {"ARM", "Oddly Specific Mythrite Sand"},
            {"GSM", "Oddly Specific Silver Ore"},
            {"LTW", "Oddly Specific Gagana Skin"},
            {"WVR", "Oddly Specific Fleece"},
            {"ALC", "Oddly Specific Sap"},
            {"CUL", "Oddly Specific Aloe"},
            {"MIN", "Oddly Specific Primordial Ore" },
            {"BTN", "Oddly Specific Primordial Log"},
            {"FSH", "Allagan Hunter"}
        };

        private static Dictionary<string, string> JobToSecondSkysungMat = new Dictionary<string, string>
        {
            {"CRP", "Lignum Vitae Lumber"},
            {"BSM", "Dwarven Mythril Ingot"},
            {"ARM", "Dwarven Mythril Ingot"},
            {"GSM", "Dwarven Mythril Nugget"},
            {"LTW", "Sea Swallow Leather"},
            {"WVR", "Dwarven Cotton"},
            {"ALC", "Refined Natron"},
            {"CUL", "Night Vinegar"},
            {"MIN", "Oddly Specific Primordial Asphaltum"},
            {"BTN", "Oddly Specific Primordial Resin"},
            {"FSH", ""}
        };

        #endregion

        #region Skysung

        public static Tuple<string, string, string, string, string, string> ReturnSkybuildersTuple(string job)
        {
            return new Tuple<string, string, string, string, string, string>(JobToToolDict[job], JobNameListString[JobListString.IndexOf(job)], JobToSkybuildersCraft[job], JobToFirstSkybuildersMat[job], JobToSecondSkybuildersMat[job], JobToThirdSkybuildersMat[job]);
        }

        private static Dictionary<string, string> JobToSkybuildersCraft = new Dictionary<string, string>
        {
            {"CRP", "Oddly Delicate Pine Lumber"},
            {"BSM", "Oddly Delicate Silver Gear"},
            {"ARM", "Oddly Delicate Wolfram Square"},
            {"GSM", "Oddly Delicate Celestine"},
            {"LTW", "Oddly Delicate Gazelle Leather"},
            {"WVR", "Oddly Delicate Rhea Cloth"},
            {"ALC", "Oddly Delicate Holy Water"},
            {"CUL", "Oddly Delicate Shark Oil"},
            {"MIN", "" },
            {"BTN", ""},
            {"FSH", ""}
        };

        private static Dictionary<string, string> JobToFirstSkybuildersMat = new Dictionary<string, string>
        {
            {"CRP", "Oddly Delicate Ping Log" },
            {"BSM", "Oddly Delicate Silvergrace Ore" },
            {"ARM", "Oddly Delicate Scheelite"},
            {"GSM", "Oddly Delicate Raw Celestine"},
            {"LTW", "Oddly Delicate Gazelle Hide"},
            {"WVR", "Oddly Delicate Rhea"},
            {"ALC", "Oddly Delicate Mistletoe"},
            {"CUL", "Oddly Delicate Hammerhead Shark"},
            {"MIN", "Oddly Delicate Adamantite Ore" },
            {"BTN", "Oddly Delicate Feather"},
            {"FSH", "Flintstrike"}
        };

        private static Dictionary<string, string> JobToSecondSkybuildersMat = new Dictionary<string, string>
        {
            {"CRP", "Approved Grade 4 Artisanal Skybuilders' Log"},
            {"BSM", "Approved Grade 4 Artisanal Skybuilders' Cloudstone"},
            {"ARM", "Approved Grade 4 Artisanal Skybuilders' Cloudstone"},
            {"GSM", "Approved Grade 4 Artisanal Skybuilders' Silex"},
            {"LTW", "Approved Grade 4 Artisanal Skybuilders' Log"},
            {"WVR", "Approved Grade 4 Artisanal Skybuilders' Cocoon"},
            {"ALC", "Approved Grade 4 Artisanal Skybuilders' Spring Water"},
            {"CUL", "Approved Grade 4 Artisanal Skybuilders' Spring Water"},
            {"MIN", "Oddly Delicate Raw Jade"},
            {"BTN", "Oddly Delicate Birch Log"},
            {"FSH", "Pickled Pom"}
        };

        private static Dictionary<string, string> JobToThirdSkybuildersMat = new Dictionary<string, string>
        {
            {"CRP", "Approved Grade 4 Artisanal Skybuilders' Barbgrass"},
            {"BSM", "Approved Grade 4 Artisanal Skybuilders' Silex"},
            {"ARM", "Approved Grade 4 Artisanal Skybuilders' Prismstone"},
            {"GSM", "Approved Grade 4 Artisanal Skybuilders' Barbgrass"},
            {"LTW", "Approved Grade 4 Artisanal Skybuilders' Caiman"},
            {"WVR", "Approved Grade 4 Artisanal Skybuilders' Ice Stalagmite"},
            {"ALC", "Approved Grade 4 Artisanal Skybuilders' Raspberry"},
            {"CUL", "Approved Grade 4 Artisanal Skybuilders' Ice Stalagmite"},
            {"MIN", ""},
            {"BTN", ""},
            {"FSH", ""}
        };
        #endregion
    }
}
