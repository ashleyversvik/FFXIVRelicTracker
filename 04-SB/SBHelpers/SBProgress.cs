﻿using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker._04_SB.SBHelpers
{
    public class SBProgress : BaseProgressClass
    {

        #region Constructors
        public SBProgress()
        {
            Progress = States.NA;
        }

        public SBProgress(string name, string job)
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
