using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._05_ShB._06_Blades
{
    public class BladesModel : ObservableObject
    {
        #region Constructor
        public BladesModel()
        {

        }
        #endregion

        public ObservableCollection<string> AvailableJobs { get; set; }
        public string SelectedJob { get; set; }
        public int EmotionCount { get; set; }
    }
}
