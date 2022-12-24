﻿using FFXIVRelicTracker._05_Skysteel._01_BaseTool;
using FFXIVRelicTracker._05_Skysteel._02_BasePlus1;
using FFXIVRelicTracker._05_Skysteel._03_Dragonsung;
using FFXIVRelicTracker._05_Skysteel._04_AugmentedDragonsung;
using FFXIVRelicTracker._05_Skysteel._05_Skysung;
using FFXIVRelicTracker._05_Skysteel._06_Skybuilders;
using FFXIVRelicTracker._05_Skysteel.Skysteel_Helpers;
using FFXIVRelicTracker.Models.Helpers;
using System.Collections.Generic;

namespace FFXIVRelicTracker._05_Skysteel.Main
{
    public class SkysteelModel : ObservableObject
    {
        #region Fields

        private SkysteelJob crp = new SkysteelJob("CRP");
        private SkysteelJob bsm = new SkysteelJob("BSM");
        private SkysteelJob arm = new SkysteelJob("ARM");
        private SkysteelJob gsm = new SkysteelJob("GSM");
        private SkysteelJob ltw = new SkysteelJob("LTW");
        private SkysteelJob wvr = new SkysteelJob("WVR");
        private SkysteelJob alc = new SkysteelJob("ALC");
        private SkysteelJob cul = new SkysteelJob("CUL");

        private SkysteelJob min = new SkysteelJob("MIN");
        private SkysteelJob btn = new SkysteelJob("BTN");
        private SkysteelJob fsh = new SkysteelJob("FSH");

        private BaseToolModel baseToolModel = new BaseToolModel();
        private BasePlus1Model basePlus1Model = new BasePlus1Model();
        private DragonsungModel dragonsungModel = new DragonsungModel();
        private AugmentedDragonsungModel augmentedDragonsungModel = new AugmentedDragonsungModel();
        private SkysungModel skysungModel = new SkysungModel();
        private SkybuildersModel skybuildersModel = new SkybuildersModel();

        #endregion

        #region Constructor
        public SkysteelModel()
        {

        }

        #endregion

        #region Properties

        public List<SkysteelJob> SkysteelJobList = new List<SkysteelJob>();

        public SkysteelJob CRP {get{return crp ;} set{ crp=value; OnPropertyChanged(nameof(CRP));} }
        public SkysteelJob BSM {get{return bsm ;} set{ bsm=value; OnPropertyChanged(nameof(BSM));} }
        public SkysteelJob ARM {get{return arm ;} set{ arm=value; OnPropertyChanged(nameof(ARM));} }
        public SkysteelJob GSM {get{return gsm ;} set{ gsm=value; OnPropertyChanged(nameof(GSM));} }
        public SkysteelJob LTW {get{return ltw ;} set{ ltw=value; OnPropertyChanged(nameof(LTW));} }
        public SkysteelJob WVR {get{return wvr ;} set{ wvr=value; OnPropertyChanged(nameof(WVR));} }
        public SkysteelJob ALC {get{return alc ;} set{ alc=value; OnPropertyChanged(nameof(ALC));} }
        public SkysteelJob CUL {get{return cul ;} set{ cul=value; OnPropertyChanged(nameof(CUL));} }
                                                                                                   
        public SkysteelJob MIN {get{return min ;} set{ min=value; OnPropertyChanged(nameof(MIN));} }
        public SkysteelJob BTN { get{return btn ;} set{ btn = value; OnPropertyChanged(nameof(BTN));} }
        public SkysteelJob FSH { get { return fsh; } set { fsh = value; OnPropertyChanged(nameof(FSH)); } }

        public BaseToolModel BaseToolModel { get { return baseToolModel; } set { baseToolModel = value; OnPropertyChanged(nameof(BaseToolModel)); } }
        public BasePlus1Model BasePlus1Model { get { return basePlus1Model; } set { basePlus1Model = value; OnPropertyChanged(nameof(BasePlus1Model)); } }
        public DragonsungModel DragonsungModel { get { return dragonsungModel; } set { dragonsungModel = value; OnPropertyChanged(nameof(DragonsungModel)); } }
        public AugmentedDragonsungModel AugmentedDragonsungModel { get { return augmentedDragonsungModel; } set { augmentedDragonsungModel = value; OnPropertyChanged(nameof(AugmentedDragonsungModel)); } }
        public SkysungModel SkysungModel { get { return skysungModel; } set { skysungModel = value; OnPropertyChanged(nameof(SkysungModel)); } }
        public SkybuildersModel SkybuildersModel { get { return skybuildersModel; } set { skybuildersModel = value; OnPropertyChanged(nameof(SkybuildersModel)); } }

        #endregion
    }
}
