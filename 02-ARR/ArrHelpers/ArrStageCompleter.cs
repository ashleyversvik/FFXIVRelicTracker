using FFXIVRelicTracker.Models;

namespace FFXIVRelicTracker._02_ARR.ArrHelpers
{
    public static class ArrStageCompleter
    {

        public static void ProgressClass(Character character, string job, string stage)
        {
            int StageIndex = ArrInfo.StageListString.IndexOf(stage);
            int JobIndex = ArrInfo.JobListString.IndexOf(job);

            ArrJob tempJob = character.ArrModel.ArrJobList[JobIndex];
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
            if (StageIndex == 4)
            {
                NovusCompleter(character, tempJob.Name);
            }
        }

        private static void InCompleteFollowingStages(ArrJob tempStage, int stageIndex)
        {
            for (int i = stageIndex; i < tempStage.StageList.Count; i++)
            {
                tempStage.StageList[i] = false;
            }
        }

        private static void CompletePreviousStages(ArrJob tempStage, int stageIndex)
        {
            for (int i = 0; i < stageIndex; i++)
            {
                tempStage.StageList[i] = true;
            }
        }

        private static void NovusCompleter(Character character, string Job)
        {
            if (character.ArrModel.NovusModel.SelectedJob == Job)
            {
                int subtractAlexandrite = 75;
                subtractAlexandrite -= character.ArrModel.NovusModel.MateriaShieldSum + character.ArrModel.NovusModel.MateriaSwordSum + character.ArrModel.NovusModel.MateriaSum;

                if (subtractAlexandrite >= character.ArrModel.NovusModel.AlexandriteCount) { character.ArrModel.NovusModel.AlexandriteCount = 0; }
                else { character.ArrModel.NovusModel.AlexandriteCount -= subtractAlexandrite; }

                character.ArrModel.NovusModel.SelectedJob = "";
            }
        }

    }
}
