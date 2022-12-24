﻿using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;

namespace FFXIVRelicTracker._05_ShB._00_Summary
{
    public class ShBSummaryModel : ObservableObject
    {

        public ShBSummaryModel(Character SelectedCharacter)
        {
            this.SelectedCharacter = SelectedCharacter;
        }

        private Character selectedCharacter;
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }
    }
}
