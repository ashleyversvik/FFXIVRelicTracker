using FFXIVRelicTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker._05_ShB.ShBHelpers
{
    public static class ShBStageCompleter
    {
        public static void ProgressClass(Character character, ShBProgress shbProgress, bool CompleteBool = false)
        {
            int StageIndex = ShBInfo.StageListString.IndexOf(shbProgress.Name);
            int JobIndex = ShBInfo.JobListString.IndexOf(shbProgress.Job);

            ShBJob tempJob = character.ShBModel.ShbJobList[JobIndex];

            if (shbProgress.Progress == ShBProgress.States.NA)
            {
                CompletePreviousStages(character, tempJob, StageIndex);
            }
            else if (shbProgress.Progress == ShBProgress.States.Completed)
            {
                InCompleteFollowingStages(tempJob, StageIndex);
                return;
            }

            if (shbProgress.Progress == ShBProgress.States.Initiated | CompleteBool)
            {
                shbProgress.Progress = ShBProgress.States.Completed;
                AlterCounts(character, StageIndex);
            }
            else
            {
                IncompleteOtherJobs(character, StageIndex);
                switch (StageIndex)
                {
                    default:
                        shbProgress.Progress = ShBProgress.States.Completed;
                        break;
                }
                AlterCounts(character, StageIndex);
            }

        }
        private static void IncompleteOtherJobs(Character SelectedCharacter, int StageIndex)
        {
            foreach (ShBJob Job in SelectedCharacter.ShBModel.ShbJobList)
            {
                ShBProgress stage = Job.StageList[StageIndex];
                if (stage.Progress == ShBProgress.States.Initiated)
                {
                    stage.Progress = ShBProgress.States.NA;
                }
            }
        }
        private static void InCompleteFollowingStages(ShBJob tempStage, int stageIndex)
        {
            for (int i = stageIndex; i < tempStage.StageList.Count; i++)
            {
                tempStage.StageList[i].Progress = ShBProgress.States.NA;
            }
        }
        private static void CompletePreviousStages(Character character, ShBJob tempStage, int stageIndex)
        {
            for (int i = 0; i < stageIndex; i++)
            {
                if (tempStage.StageList[i].Progress != ShBProgress.States.Completed)
                {
                    AlterCounts(character, i);
                    tempStage.StageList[i].Progress = ShBProgress.States.Completed;
                }
            }
        }

        private static void AlterCounts(Character character, int stageIndex)
        {
            switch (stageIndex)
            {
                case 0:
                    DecreaseScalePowder(character);
                    break;
                case 1:
                    DecreaseMemoryCount(character);
                    break;
                case 2:
                    DecreaseBitterMemoryCount(character);
                    break;
                case 3:
                    DecreaseLoathsomeMemoryCount(character);
                    break;
                case 4:
                    DecreaseTimewornArtifactCount(character);
                    break;
                case 5:
                    DecreaseRawEmotionCount(character);
                    break;
                default:
                    break;
            }
        }

        private static void DecreaseScalePowder(Character character)
        {
            //Decrease Scalepowder outside of resistance model so that changes to progress that occur outside of Resistance view still impact scalepowder

            if (character.ShBModel.ResistanceModel.CurrentScalepowder <= 4) { character.ShBModel.ResistanceModel.CurrentScalepowder = 0; }
            else { character.ShBModel.ResistanceModel.CurrentScalepowder -= 4; }
        }

        private static void DecreaseMemoryCount(Character character)
        {
            //Decrease Count of Memory items outside of AugmentedResistance model so that changes to progress that occur outside of AugmentedResistance view still impact Memory Counts

            if (character.ShBModel.AugmentedResistanceModel.HarrowingCount <= 20) { character.ShBModel.AugmentedResistanceModel.HarrowingCount = 0; }
            else { character.ShBModel.AugmentedResistanceModel.HarrowingCount -= 20; }

            if (character.ShBModel.AugmentedResistanceModel.TorturedCount <= 20) { character.ShBModel.AugmentedResistanceModel.TorturedCount = 0; }
            else { character.ShBModel.AugmentedResistanceModel.TorturedCount -= 20; }

            if (character.ShBModel.AugmentedResistanceModel.SorrowfulCount <= 20) { character.ShBModel.AugmentedResistanceModel.SorrowfulCount = 0; }
            else { character.ShBModel.AugmentedResistanceModel.SorrowfulCount -= 20; }
        }
        private static void DecreaseBitterMemoryCount(Character character)
        {
            //Decrease Count of Memory items outside of AugmentedResistance model so that changes to progress that occur outside of AugmentedResistance view still impact Memory Counts

            if (character.ShBModel.RecollectionModel.MemoryCount <= 6) { character.ShBModel.RecollectionModel.MemoryCount = 0; }
            else { character.ShBModel.RecollectionModel.MemoryCount -= 6; }
        }

        private static void DecreaseLoathsomeMemoryCount(Character character)
        {
            //Decrease Count of Memory items outside of LawsOrder model so that changes to progress that occur outside of LawsOrder view still impact Memory Counts

            if (character.ShBModel.LawsOrderModel.MemoryCount <= 15) { character.ShBModel.RecollectionModel.MemoryCount = 0; }
            else { character.ShBModel.LawsOrderModel.MemoryCount -= 15; }
        }
        private static void DecreaseTimewornArtifactCount(Character character)
        {
            //Decrease Count of Memory items outside of AugmentedLawsOrderModel so that changes to progress that occur outside of AugmentedLawsOrder view still impact Memory Counts

            if (character.ShBModel.AugmentedLawsOrderModel.MemoryCount <= 15) { character.ShBModel.AugmentedLawsOrderModel.MemoryCount = 0; }
            else { character.ShBModel.AugmentedLawsOrderModel.MemoryCount -= 15; }
        }

        private static void DecreaseRawEmotionCount(Character character)
        {
            //Decrease Count of Emotion items outside of BladesModel so that changes to progress that occur outside of BladesModel view still impact Emotion Counts

            if (character.ShBModel.BladesModel.EmotionCount <= 15) { character.ShBModel.BladesModel.EmotionCount = 0; }
            else { character.ShBModel.BladesModel.EmotionCount -= 15; }
        }
        
    }
}
