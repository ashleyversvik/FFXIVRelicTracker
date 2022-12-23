﻿using FFXIVRelicTracker._02_ARR.ArrHelpers;
using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;
using System.Windows;

namespace FFXIVRelicTracker._02_ARR._01_Relic
{
    
    public class RelicModel : BaseStageModel
    {

        public RelicModel()
        {
        }

        public Visibility relicVisibility = Visibility.Hidden;
        public List<ArrProgress> JobRelics{ get; set;}
        public int RelicIndex{ get; set;}
        public string RelicDestination{ get; set;}
        public string RelicClassWeapon{ get; set;}
        public string RelicBeastmen1{ get; set;}
        public string RelicBeastmen2{ get; set;}
        public string RelicBeastmen3{ get; set;}
        public string RelicMap{ get; set;}
        public PointF RelicPoint{ get; set;}
        public string RelicMateria{ get; set;}
        public Visibility RelicVisibility { get { return relicVisibility; } set { relicVisibility = value; OnPropertyChanged(nameof(RelicVisibility)); } }
        public bool Stage1Complete { get;  set; }
        public bool Stage2Complete { get;  set; }
        public bool Stage3Complete { get;  set; }
        public bool Stage4Complete { get;  set; }
        public bool Stage5Complete { get;  set; }
        public bool Stage6Complete { get;  set; }
        public bool Stage7Complete { get;  set; }
        public bool Stage8Complete { get;  set; }
        public bool IfritComplete { get;  set; }
        public bool GarudaComplete { get;  set; }
        public bool ShivaComplete { get;  set; }
        public bool BeastMan1Complete { get;  set; }
        public bool BeastMan2Complete { get;  set; }
        public bool BeastMan3Complete { get;  set; }

    }
}
