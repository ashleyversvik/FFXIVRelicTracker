﻿using FFXIVRelicTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker._06_EW.EWHelpers
{
    public static class EWStageCompleter
    {
        public static void ProgressClass(Character character, EWProgress shbProgress, bool CompleteBool = false)
        {
            int StageIndex = EWInfo.StageListString.IndexOf(shbProgress.Name);
            int JobIndex = EWInfo.JobListString.IndexOf(shbProgress.Job);

            EWJob tempJob = character.EWModel.EWJobList[JobIndex];

            if (shbProgress.Progress == EWProgress.States.NA)
            {
                CompletePreviousStages(character, tempJob, StageIndex);
            }
            else if (shbProgress.Progress == EWProgress.States.Completed)
            {
                InCompleteFollowingStages(tempJob, StageIndex);
                return;
            }

            if (shbProgress.Progress == EWProgress.States.Initiated | CompleteBool)
            {
                shbProgress.Progress = EWProgress.States.Completed;
                AlterCounts(character, StageIndex);
            }
            else
            {
                IncompleteOtherJobs(character, StageIndex);
                switch (StageIndex)
                {
                    default:
                        shbProgress.Progress = EWProgress.States.Completed;
                        break;
                }
                AlterCounts(character, StageIndex);
            }

        }
        private static void IncompleteOtherJobs(Character SelectedCharacter, int StageIndex)
        {
            foreach (EWJob Job in SelectedCharacter.EWModel.EWJobList)
            {
                EWProgress stage = Job.StageList[StageIndex];
                if (stage.Progress == EWProgress.States.Initiated)
                {
                    stage.Progress = EWProgress.States.NA;
                }
            }
        }
        private static void InCompleteFollowingStages(EWJob tempStage, int stageIndex)
        {
            for (int i = stageIndex; i < tempStage.StageList.Count; i++)
            {
                tempStage.StageList[i].Progress = EWProgress.States.NA;
            }
        }
        private static void CompletePreviousStages(Character character, EWJob tempStage, int stageIndex)
        {
            for (int i = 0; i < stageIndex; i++)
            {
                if (tempStage.StageList[i].Progress != EWProgress.States.Completed)
                {
                    AlterCounts(character, i);
                    tempStage.StageList[i].Progress = EWProgress.States.Completed;
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

                default:
                    break;
            }
        }

        private static void DecreaseScalePowder(Character character)
        {
            //Decrease Scalepowder outside of resistance model so that changes to progress that occur outside of Resistance view still impact scalepowder

            if (character.EWModel.PlaceholderModel.CurrentScalepowder <= 4) { character.EWModel.PlaceholderModel.CurrentScalepowder = 0; }
            else { character.EWModel.PlaceholderModel.CurrentScalepowder -= 4; }
        }
        
    }
}