using FFXIVRelicTracker._06_SplendorousTools._00_Summary;
using FFXIVRelicTracker._06_SplendorousTools._01_Splendorous;
using FFXIVRelicTracker._06_SplendorousTools._02_AugmentedSplendorous;
using FFXIVRelicTracker._06_SplendorousTools._03_Crystalline;
using FFXIVRelicTracker._06_SplendorousTools.Splendorous_Helpers;
using FFXIVRelicTracker.Models.Helpers;
using System.Collections.Generic;

namespace FFXIVRelicTracker._06_SplendorousTools.Main
{
    public class SplendorousToolsModel : ObservableObject
    {
        #region Fields

        private SplendorousToolsJob crp = new SplendorousToolsJob("CRP");
        private SplendorousToolsJob bsm = new SplendorousToolsJob("BSM");
        private SplendorousToolsJob arm = new SplendorousToolsJob("ARM");
        private SplendorousToolsJob gsm = new SplendorousToolsJob("GSM");
        private SplendorousToolsJob ltw = new SplendorousToolsJob("LTW");
        private SplendorousToolsJob wvr = new SplendorousToolsJob("WVR");
        private SplendorousToolsJob alc = new SplendorousToolsJob("ALC");
        private SplendorousToolsJob cul = new SplendorousToolsJob("CUL");

        private SplendorousToolsJob min = new SplendorousToolsJob("MIN");
        private SplendorousToolsJob btn = new SplendorousToolsJob("BTN");
        private SplendorousToolsJob fsh = new SplendorousToolsJob("FSH");

        private SplendorousModel splendorousModel = new SplendorousModel();
        private AugmentedSplendorousModel augmentedSplendorousModel = new AugmentedSplendorousModel();
        private CrystallineModel crystallineModel = new CrystallineModel();
        #endregion

        #region Constructor
        public SplendorousToolsModel()
        {

        }

        #endregion

        #region Properties

        public List<SplendorousToolsJob> SplendorousToolsJobList = new List<SplendorousToolsJob>();

        public SplendorousToolsJob CRP {get{return crp ;} set{ crp=value; OnPropertyChanged(nameof(CRP));} }
        public SplendorousToolsJob BSM {get{return bsm ;} set{ bsm=value; OnPropertyChanged(nameof(BSM));} }
        public SplendorousToolsJob ARM {get{return arm ;} set{ arm=value; OnPropertyChanged(nameof(ARM));} }
        public SplendorousToolsJob GSM {get{return gsm ;} set{ gsm=value; OnPropertyChanged(nameof(GSM));} }
        public SplendorousToolsJob LTW {get{return ltw ;} set{ ltw=value; OnPropertyChanged(nameof(LTW));} }
        public SplendorousToolsJob WVR {get{return wvr ;} set{ wvr=value; OnPropertyChanged(nameof(WVR));} }
        public SplendorousToolsJob ALC {get{return alc ;} set{ alc=value; OnPropertyChanged(nameof(ALC));} }
        public SplendorousToolsJob CUL {get{return cul ;} set{ cul=value; OnPropertyChanged(nameof(CUL));} }
                                                                                                   
        public SplendorousToolsJob MIN {get{return min ;} set{ min=value; OnPropertyChanged(nameof(MIN));} }
        public SplendorousToolsJob BTN { get{return btn ;} set{ btn = value; OnPropertyChanged(nameof(BTN));} }
        public SplendorousToolsJob FSH { get { return fsh; } set { fsh = value; OnPropertyChanged(nameof(FSH)); } }

        public SplendorousModel SplendorousModel { get { return splendorousModel; } set { splendorousModel = value; OnPropertyChanged(nameof(SplendorousModel)); } }
        public AugmentedSplendorousModel AugmentedSplendorousModel { get { return augmentedSplendorousModel; } set { augmentedSplendorousModel = value; OnPropertyChanged(nameof(AugmentedSplendorousModel)); } }
        public CrystallineModel CrystallineModel { get { return crystallineModel; } set { crystallineModel = value; OnPropertyChanged(nameof(CrystallineModel)); } }

        #endregion
    }
}
