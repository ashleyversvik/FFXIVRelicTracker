using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker._06_EW.EWHelpers
{
    public class EWProgress : BaseProgressClass
    {

        #region Constructors
        public EWProgress()
        {
            Progress = States.NA;
        }

        public EWProgress(string name, string job)
            : this()
        {
            this.Name = name;
            this.Job = job;
        }

        #endregion

        #region Fields
        private string name;
        private string job;
        #endregion

        #region Properties
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Job
        {
            get { return job; }
            set
            {
                job = value;
                OnPropertyChanged(nameof(Job));
            }
        }
        #endregion
    }
}
