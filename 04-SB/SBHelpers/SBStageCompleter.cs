using FFXIVRelicTracker.Models;

namespace FFXIVRelicTracker._04_SB.SBHelpers
{
    public static class SBStageCompleter
    {
        public static void ProgressClass(Character character, string job, string stage)
        {
            int StageIndex = SBInfo.StageListString.IndexOf(stage);
            int JobIndex = SBInfo.JobListString.IndexOf(job);

            SBJob tempJob = character.SBModel.SBJobList[JobIndex];
            var progress = tempJob.StageList[StageIndex];

            if (!progress)
            {
                CompletePreviousStages(character, tempJob, StageIndex);
            }
            else if (progress)
            {
                InCompleteFollowingStages(tempJob, StageIndex);
                tempJob.RefreshJob();
                return;
            }

            tempJob.StageList[StageIndex] = true;
            tempJob.RefreshJob();
            AlterCounts(character, StageIndex);
        }

        private static void InCompleteFollowingStages(SBJob tempStage, int stageIndex)
        {
            for (int i = stageIndex; i < tempStage.StageList.Count; i++)
            {
                tempStage.StageList[i] = false;
            }
        }
        private static void CompletePreviousStages(Character character, SBJob tempStage, int stageIndex)
        {
            for (int i = 0; i < stageIndex; i++)
            {
                if (!tempStage.StageList[i])
                {
                    AlterCounts(character, i);
                    tempStage.StageList[i] = true;
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
