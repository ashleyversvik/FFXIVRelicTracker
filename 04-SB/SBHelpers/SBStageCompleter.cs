using FFXIVRelicTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker._04_SB.SBHelpers
{
    public static class SBStageCompleter
    {
        public static void ProgressClass(Character character, SBProgress SBProgress, bool CompleteBool = false)
        {
            int StageIndex = SBInfo.StageListString.IndexOf(SBProgress.Name);
            int JobIndex = SBInfo.JobListString.IndexOf(SBProgress.Job);

            SBJob tempJob = character.SBModel.SBJobList[JobIndex];

            if (SBProgress.Progress == SBProgress.States.NA)
            {
                CompletePreviousStages(character, tempJob, StageIndex);
            }
            else if (SBProgress.Progress == SBProgress.States.Completed)
            {
                InCompleteFollowingStages(tempJob, StageIndex);
                return;
            }

            if (SBProgress.Progress == SBProgress.States.Initiated | CompleteBool)
            {
                SBProgress.Progress = SBProgress.States.Completed;
                AlterCounts(character, StageIndex);
            }
            else
            {
                IncompleteOtherJobs(character, StageIndex);
                switch (StageIndex)
                {
                    default:
                        SBProgress.Progress = SBProgress.States.Completed;
                        break;
                }
                AlterCounts(character, StageIndex);
            }

        }
        private static void IncompleteOtherJobs(Character SelectedCharacter, int StageIndex)
        {
            foreach (SBJob Job in SelectedCharacter.SBModel.SBJobList)
            {
                SBProgress stage = Job.StageList[StageIndex];
                if (stage.Progress == SBProgress.States.Initiated)
                {
                    stage.Progress = SBProgress.States.NA;
                }
            }
        }
        private static void InCompleteFollowingStages(SBJob tempStage, int stageIndex)
        {
            for (int i = stageIndex; i < tempStage.StageList.Count; i++)
            {
                tempStage.StageList[i].Progress = SBProgress.States.NA;
            }
        }
        private static void CompletePreviousStages(Character character, SBJob tempStage, int stageIndex)
        {
            for (int i = 0; i < stageIndex; i++)
            {
                if (tempStage.StageList[i].Progress != SBProgress.States.Completed)
                {
                    AlterCounts(character, i);
                    tempStage.StageList[i].Progress = SBProgress.States.Completed;
                }
            }
        }

        private static void AlterCounts(Character character, int stageIndex)
        {
            switch (stageIndex)
            {
                case 1:
                    DecreaseAnemosCounts(character);
                    break;
                case 2:
                    DecreaseElementalAmounts(character);
                    break;
                case 3:
                    DecreasePyrosAmounts(character);
                    break;
                case 4:
                    DecreaseEurekaAmounts(character);
                    break;
                case 5:
                    DecreasePhyseosAmounts(character);
                    break;
                default:
                    break;
            }
        }

        private static void DecreaseAnemosCounts(Character character)
        {
            int protean = 1300;
            if (character.SBModel.AnemosModel.BuyFeathers) protean = 2200;

            if (character.SBModel.AnemosModel.ProteanCount <= protean) { character.SBModel.AnemosModel.ProteanCount = 0; }
            else { character.SBModel.AnemosModel.ProteanCount -= protean; }

            if (character.SBModel.AnemosModel.FeatherCount <= 3) { character.SBModel.AnemosModel.FeatherCount = 0; }
            else { character.SBModel.AnemosModel.FeatherCount -= 3; }

        }
        private static void DecreaseElementalAmounts (Character character)
        {
            int pagos = 500;
            if (character.SBModel.ElementalModel.BuyIce) pagos = 750;

            if (character.SBModel.ElementalModel.FrostedProteanCount <= 31) { character.SBModel.ElementalModel.FrostedProteanCount = 0; }
            else { character.SBModel.ElementalModel.FrostedProteanCount -= 31; }
            if (character.SBModel.ElementalModel.IceCount <= 5) { character.SBModel.ElementalModel.IceCount = 0; }
            else { character.SBModel.ElementalModel.IceCount -= 5; }
            if (character.SBModel.ElementalModel.PagosCount <= pagos) { character.SBModel.ElementalModel.PagosCount = 0; }
            else { character.SBModel.ElementalModel.PagosCount -= pagos; }
        }

        private static void DecreasePyrosAmounts(Character character)
        {
            int pyros = 650;
            if (character.SBModel.PyrosModel.BuyFlames) pyros = 900;

            if (character.SBModel.PyrosModel.PyrosCount <= pyros) { character.SBModel.PyrosModel.PyrosCount = 0; }
            else { character.SBModel.PyrosModel.PyrosCount -= pyros; }
            if (character.SBModel.PyrosModel.FlamesCount <= 5) { character.SBModel.PyrosModel.FlamesCount = 0; }
            else { character.SBModel.PyrosModel.FlamesCount -= 5; }
        }
        private static void DecreaseEurekaAmounts(Character character)
        {
            if (character.SBModel.EurekaModel.HydatosCount <= 350) { character.SBModel.EurekaModel.HydatosCount = 0; }
            else { character.SBModel.EurekaModel.HydatosCount -= 350; }

            if (character.SBModel.EurekaModel.ScaleCount <= 5) { character.SBModel.EurekaModel.ScaleCount = 0; }
            else { character.SBModel.EurekaModel.ScaleCount -= 5; }
        }

        private static void DecreasePhyseosAmounts(Character character)
        {
            if (character.SBModel.PhyseosModel.FragmentCount <= 100) { character.SBModel.PhyseosModel.FragmentCount = 0; }
            else { character.SBModel.PhyseosModel.FragmentCount -= 100; }
        }
        
    }
}
