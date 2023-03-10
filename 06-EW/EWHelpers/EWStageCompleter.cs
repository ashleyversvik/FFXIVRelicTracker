using FFXIVRelicTracker.Models;

namespace FFXIVRelicTracker._06_EW.EWHelpers
{
    public static class EWStageCompleter
    {
        public static void ProgressClass(Character character, string job, string stage)
        {
            int StageIndex = EWInfo.StageListString.IndexOf(stage);
            int JobIndex = EWInfo.JobListString.IndexOf(job);

            EWJob tempJob = character.EWModel.EWJobList[JobIndex];
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

        private static void InCompleteFollowingStages(EWJob tempStage, int stageIndex)
        {
            for (int i = stageIndex; i < tempStage.StageList.Count; i++)
            {
                tempStage.StageList[i] = false;
            }
        }
        private static void CompletePreviousStages(Character character, EWJob tempStage, int stageIndex)
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
                case 0:
                    DecreaseMeteorites(character);
                    break;
                case 1:
                    DecreateChondrites(character);
                    break;
                default:
                    break;
            }
        }

        private static void DecreaseMeteorites(Character character)
        {
            if (character.EWModel.MandervilleModel.MeteoritesCount <= 3) { character.EWModel.MandervilleModel.MeteoritesCount = 0; }
            else { character.EWModel.MandervilleModel.MeteoritesCount -= 3; }
        }

        private static void DecreateChondrites(Character character)
        {
            if (character.EWModel.AmazingModel.ChondritesCount <= 3) { character.EWModel.AmazingModel.ChondritesCount = 0; }
            else { character.EWModel.AmazingModel.ChondritesCount -= 3; }
        }

    }
}
