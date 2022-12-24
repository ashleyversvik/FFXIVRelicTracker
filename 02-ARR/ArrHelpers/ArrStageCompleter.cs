using FFXIVRelicTracker.Models;

namespace FFXIVRelicTracker._02_ARR.ArrHelpers
{
    public static class ArrStageCompleter
    {

        public static void ProgressClass(Character character, string job, ArrProgress arrProgress, bool CompleteBool=false )
        {
            int StageIndex = ArrInfo.StageListString.IndexOf(arrProgress.Name);
            int JobIndex = ArrInfo.JobListString.IndexOf(job);

            ArrJob Job = character.ArrModel.ArrJobList[JobIndex];

            if (arrProgress.Progress == ArrProgress.States.NA)
            {
                CompletePreviousStages(Job, StageIndex);
            }
            else if (arrProgress.Progress == ArrProgress.States.Completed)
            {
                InCompleteFollowingStages(Job, StageIndex);
                return;
            }
            if (arrProgress.Progress == ArrProgress.States.Initiated | CompleteBool)
            {
                arrProgress.Progress = ArrProgress.States.Completed;
            }
            else
            {
                switch (StageIndex)
                {
                    case 0:
                    case 3:
                    case 6:
                    case 5:
                    case 7:
                        arrProgress.Progress++;
                        if (arrProgress.Progress == ArrProgress.States.Initiated) { IncompleteOtherJobs(character, Job, StageIndex); }
                        break;
                    case 1:
                    case 2:
                        arrProgress.Progress = ArrProgress.States.Completed;
                        break;
                    case 4:
                        NovusCompleter(character,Job.Name);
                        arrProgress.Progress = ArrProgress.States.Completed;
                        break;
                }
            }
        }



        private static void IncompleteOtherJobs(Character SelectedCharacter, ArrJob tempStage, int StageIndex)
        {
            foreach (ArrJob Job in SelectedCharacter.ArrModel.ArrJobList)
            {
                if (Job != tempStage)
                {
                    ArrProgress stage = Job.StageList[StageIndex];
                    if (stage.Progress == ArrProgress.States.Initiated)
                    {
                        stage.Progress = ArrProgress.States.NA;
                    }
                }
            }
        }

        private static void InCompleteFollowingStages(ArrJob tempStage, int stageIndex)
        {
            for (int i = stageIndex; i < tempStage.StageList.Count; i++)
            {
                tempStage.StageList[i].Progress = ArrProgress.States.NA;
            }
        }

        private static void CompletePreviousStages(ArrJob tempStage, int stageIndex)
        {
            for (int i = 0; i < stageIndex; i++)
            {
                tempStage.StageList[i].Progress = ArrProgress.States.Completed;
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
