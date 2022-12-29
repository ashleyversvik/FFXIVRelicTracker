using FFXIVRelicTracker.Models;

namespace FFXIVRelicTracker._03_HW.HWHelpers
{
    public static class HWStageCompleter
    {
        #region CompleteStages
        public static void ProgressClass(Character character, string job, string stage)
        {
            int StageIndex = HWInfo.StageListString.IndexOf(stage);
            int JobIndex = HWInfo.JobListString.IndexOf(job);

            HWJob tempJob = character.HWModel.HWJobList[JobIndex];
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
            SelectStage(character, StageIndex);
        }

        private static void InCompleteFollowingStages(HWJob tempStage, int stageIndex)
        {
            for (int i = stageIndex; i < tempStage.StageList.Count; i++)
            {
                tempStage.StageList[i] = false;
            }
        }
        private static void CompletePreviousStages(Character character, HWJob tempStage, int stageIndex)
        {
            for (int i = 0; i < stageIndex; i++)
            {
                if (!tempStage.StageList[i])
                {
                    tempStage.StageList[i] = true;
                    SelectStage(character, i);
                }
                
            }
        }
        #endregion

        #region Stage Specifics
        private static void SelectStage(Character character, int stageInt)
        {
            switch (stageInt)
            {
                case 0: //Animated Stage
                    AnimatedStage(character);
                    break;
                case 1: //Awoken Stage
                    break;
                case 2: //Anima Stage
                    AnimaStage(character);
                    break;
                case 3: //Hyperconductive Stage
                    HyperconductiveStage(character);
                    break;
                case 4: //Reconditioned Stage
                    ReconditionedStage(character);
                    break;
                case 5: //Sharpened Stage
                    SharpenedStage(character);
                    break;
                case 6: //Complete Stage
                    CompleteStage(character);
                    break;
                case 7: //Lux Stage
                    LuxStage(character);
                    break;
                default:
                    break;
            }
        }
        private static void AnimatedStage(Character character)
        {
            character.HWModel.AnimatedModel.WindCount -= 1;
            character.HWModel.AnimatedModel.FireCount -= 1;
            character.HWModel.AnimatedModel.LightningCount -= 1;
            character.HWModel.AnimatedModel.IceCount -= 1;
            character.HWModel.AnimatedModel.EarthCount -= 1;
            character.HWModel.AnimatedModel.WaterCount -= 1;
        }

        private static void AnimaStage(Character character)
        {
            character.HWModel.AnimaModel.UnidentifiableBone -= 10;
            character.HWModel.AnimaModel.UnidentifiableShell -= 10;
            character.HWModel.AnimaModel.UnidentifiableOre -= 10;
            character.HWModel.AnimaModel.UnidentifiableSeeds -= 10;

            character.HWModel.AnimaModel.Francesca -= 4;
            character.HWModel.AnimaModel.Mirror -= 4;
            character.HWModel.AnimaModel.Arrow -= 4;
            character.HWModel.AnimaModel.Kingcake -= 4;

        }

        private static void HyperconductiveStage(Character character)
        {
            character.HWModel.HyperconductiveModel.OilCount -= 5;
        }

        private static void ReconditionedStage(Character character)
        {
            character.HWModel.ReconditionedModel.CurrentPoints = 0;
        }

        private static void SharpenedStage(Character character)
        {
            character.HWModel.SharpenedModel.Currentcluster -= 50;
        }

        private static void CompleteStage(Character character)
        {
            character.HWModel.CompleteModel.CurrentPneumite -= 15;
            character.HWModel.CompleteModel.Light = 0;
        }
        #endregion
        private static void LuxStage(Character character)
        {
            character.HWModel.LuxModel.Ink = -1;
        }
    }
}
