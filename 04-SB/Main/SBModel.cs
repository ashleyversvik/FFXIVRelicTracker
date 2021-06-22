using FFXIVRelicTracker._04_SB._01_Antiquated;
using FFXIVRelicTracker._04_SB._02_Anemos;
using FFXIVRelicTracker._04_SB._03_Elemental;
using FFXIVRelicTracker._04_SB._04_Pyros;
using FFXIVRelicTracker._04_SB._05_Eureka;
using FFXIVRelicTracker._04_SB._06_Physeos;
using FFXIVRelicTracker._04_SB.SBHelpers;
using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker._04_SB.Main
{
    public class SBModel : ObservableObject
    {
        #region Fields
        private SBJob pld = new SBJob("PLD");
        private SBJob war = new SBJob("WAR");
        private SBJob drk = new SBJob("DRK");
        private SBJob whm = new SBJob("WHM");
        private SBJob sch = new SBJob("SCH");
        private SBJob ast = new SBJob("AST");
        private SBJob mnk = new SBJob("MNK");
        private SBJob drg = new SBJob("DRG");
        private SBJob nin = new SBJob("NIN");
        private SBJob sam = new SBJob("SAM");
        private SBJob brd = new SBJob("BRD");
        private SBJob mch = new SBJob("MCH");
        private SBJob blm = new SBJob("BLM");
        private SBJob smn = new SBJob("SMN");
        private SBJob rdm = new SBJob("RDM");


        private AntiquatedModel antiquatedModel = new AntiquatedModel();
        private AnemosModel anemosModel = new AnemosModel();
        private ElementalModel elementalModel = new ElementalModel();
        private PyrosModel pyrosModel = new PyrosModel();
        private EurekaModel eurekaModel = new EurekaModel();
        private PhyseosModel physeosModel = new PhyseosModel();
        #endregion

        #region Constructor
        public SBModel()
        {
        }
        #endregion

        #region Properties
        public List<SBJob> SBJobList = new List<SBJob>();
        public SBJob PLD { get { return pld; } set { pld = value; OnPropertyChanged(nameof(PLD)); } }
        public SBJob WAR { get { return war; } set { war = value; OnPropertyChanged(nameof(WAR)); } }
        public SBJob DRK { get { return drk; } set { drk = value; OnPropertyChanged(nameof(DRK)); } }
        public SBJob WHM { get { return whm; } set { whm = value; OnPropertyChanged(nameof(WHM)); } }
        public SBJob SCH { get { return sch; } set { sch = value; OnPropertyChanged(nameof(SCH)); } }
        public SBJob AST { get { return ast; } set { ast = value; OnPropertyChanged(nameof(AST)); } }
        public SBJob MNK { get { return mnk; } set { mnk = value; OnPropertyChanged(nameof(MNK)); } }
        public SBJob DRG { get { return drg; } set { drg = value; OnPropertyChanged(nameof(DRG)); } }
        public SBJob NIN { get { return nin; } set { nin = value; OnPropertyChanged(nameof(NIN)); } }
        public SBJob SAM { get { return sam; } set { sam = value; OnPropertyChanged(nameof(SAM)); } }
        public SBJob BRD { get { return brd; } set { brd = value; OnPropertyChanged(nameof(BRD)); } }
        public SBJob MCH { get { return mch; } set { mch = value; OnPropertyChanged(nameof(MCH)); } }
        public SBJob BLM { get { return blm; } set { blm = value; OnPropertyChanged(nameof(BLM)); } }
        public SBJob SMN { get { return smn; } set { smn = value; OnPropertyChanged(nameof(SMN)); } }
        public SBJob RDM { get { return rdm; } set { rdm = value; OnPropertyChanged(nameof(RDM)); } }

        public AntiquatedModel AntiquatedModel { get { return antiquatedModel; } set { antiquatedModel = value; OnPropertyChanged(nameof(AntiquatedModel)); } }
        public AnemosModel AnemosModel { get { return anemosModel; } set { anemosModel = value; OnPropertyChanged(nameof(AnemosModel)); } }
        public ElementalModel ElementalModel { get { return elementalModel; } set { elementalModel = value; OnPropertyChanged(nameof(ElementalModel)); } }
        public PyrosModel PyrosModel { get { return pyrosModel; } set { pyrosModel = value; OnPropertyChanged(nameof(PyrosModel)); } }
        public EurekaModel EurekaModel { get { return eurekaModel; } set { eurekaModel = value; OnPropertyChanged(nameof(EurekaModel)); } }
        public PhyseosModel PhyseosModel { get { return physeosModel; } set { physeosModel = value; OnPropertyChanged(nameof(PhyseosModel)); } }
        #endregion
    }
}
