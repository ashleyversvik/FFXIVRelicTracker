using FFXIVRelicTracker._06_EW._01_Manderville;
using FFXIVRelicTracker._06_EW.EWHelpers;
using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker._06_EW.Main
{
    public class EWModel : ObservableObject
    {


        #region Fields

        private EWJob pld = new EWJob("PLD");
        private EWJob war = new EWJob("WAR");
        private EWJob drk = new EWJob("DRK");
        private EWJob gnb = new EWJob("GNB");
        private EWJob whm = new EWJob("WHM");
        private EWJob sch = new EWJob("SCH");
        private EWJob ast = new EWJob("AST");
        private EWJob sge = new EWJob("SGE");
        private EWJob mnk = new EWJob("MNK");
        private EWJob drg = new EWJob("DRG");
        private EWJob nin = new EWJob("NIN");
        private EWJob sam = new EWJob("SAM");
        private EWJob rpr = new EWJob("RPR");
        private EWJob brd = new EWJob("BRD");
        private EWJob mch = new EWJob("MCH");
        private EWJob dnc = new EWJob("DNC");
        private EWJob blm = new EWJob("BLM");
        private EWJob smn = new EWJob("SMN");
        private EWJob rdm = new EWJob("RDM");

        private MandervilleModel mandervilleModel = new MandervilleModel();

        #endregion

        #region Constructors
        public EWModel()
        {
        }

        #endregion

        #region Properties
        public List<EWJob> EWJobList = new List<EWJob>();
        public EWJob PLD { get { return pld; } set { pld = value; OnPropertyChanged(nameof(PLD)); } }
        public EWJob WAR { get { return war; } set { war = value; OnPropertyChanged(nameof(WAR)); } }
        public EWJob DRK { get { return drk; } set { drk = value; OnPropertyChanged(nameof(DRK)); } }
        public EWJob GNB { get { return gnb; } set { gnb = value; OnPropertyChanged(nameof(GNB)); } }
        public EWJob WHM { get { return whm; } set { whm = value; OnPropertyChanged(nameof(WHM)); } }
        public EWJob SCH { get { return sch; } set { sch = value; OnPropertyChanged(nameof(SCH)); } }
        public EWJob AST { get { return ast; } set { ast = value; OnPropertyChanged(nameof(AST)); } }
        public EWJob SGE { get { return sge; } set { sge = value; OnPropertyChanged(nameof(SGE)); } }
        public EWJob MNK { get { return mnk; } set { mnk = value; OnPropertyChanged(nameof(MNK)); } }
        public EWJob DRG { get { return drg; } set { drg = value; OnPropertyChanged(nameof(DRG)); } }
        public EWJob NIN { get { return nin; } set { nin = value; OnPropertyChanged(nameof(NIN)); } }
        public EWJob SAM { get { return sam; } set { sam = value; OnPropertyChanged(nameof(SAM)); } }
        public EWJob RPR { get { return rpr; } set { rpr = value; OnPropertyChanged(nameof(rpr)); } }
        public EWJob BRD { get { return brd; } set { brd = value; OnPropertyChanged(nameof(BRD)); } }
        public EWJob MCH { get { return mch; } set { mch = value; OnPropertyChanged(nameof(MCH)); } }
        public EWJob DNC { get { return dnc; } set { dnc = value; OnPropertyChanged(nameof(DNC)); } }
        public EWJob BLM { get { return blm; } set { blm = value; OnPropertyChanged(nameof(BLM)); } }
        public EWJob SMN { get { return smn; } set { smn = value; OnPropertyChanged(nameof(SMN)); } }
        public EWJob RDM { get { return rdm; } set { rdm = value; OnPropertyChanged(nameof(RDM)); } }

        public MandervilleModel MandervilleModel { get { return mandervilleModel; } set { mandervilleModel = value; OnPropertyChanged(nameof(MandervilleModel)); } }
        #endregion

    }

}
