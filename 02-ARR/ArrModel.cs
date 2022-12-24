using FFXIVRelicTracker._02_ARR._04_Animus;
using FFXIVRelicTracker._02_ARR.ArrHelpers;
using FFXIVRelicTracker._02_ARR._03_Atma;
using FFXIVRelicTracker._02_ARR._07_Braves;
using FFXIVRelicTracker._02_ARR._06_Nexus;
using FFXIVRelicTracker._02_ARR._05_Novus;
using FFXIVRelicTracker._02_ARR._01_Relic;
using FFXIVRelicTracker._02_ARR._08_Zeta;
using FFXIVRelicTracker.Models.Helpers;
using System.Collections.Generic;

namespace FFXIVRelicTracker.Models
{
    public class ArrModel:ObservableObject
    {
        private RelicModel relicModel;
        private AtmaModel atmaModel;
        private AnimusModel animusModel;
        private NovusModel novusModel;
        private NexusModel nexusModel;
        private BravesModel bravesModel;
        private ZetaModel zetaModel;

        private ArrJob pld = new ArrJob("PLD");
        private ArrJob war = new ArrJob("WAR");
        private ArrJob whm = new ArrJob("WHM");
        private ArrJob sch = new ArrJob("SCH");
        private ArrJob mnk = new ArrJob("MNK");
        private ArrJob drg = new ArrJob("DRG");
        private ArrJob nin = new ArrJob("NIN");
        private ArrJob brd = new ArrJob("BRD");
        private ArrJob blm = new ArrJob("BLM");
        private ArrJob smn = new ArrJob("SMN");

        public ArrModel()
        {
            RelicModel = new RelicModel();
            AtmaModel = new AtmaModel();
            AnimusModel = new AnimusModel();
            NovusModel = new NovusModel();
            NexusModel = new NexusModel();
            BravesModel = new BravesModel();
            ZetaModel = new ZetaModel();
        }

        #region JobList
        public List<ArrJob> ArrJobList = new List<ArrJob>();
        public ArrJob PLD { get { return pld; } set { pld = value; OnPropertyChanged(nameof(PLD)); } }
        public ArrJob WAR { get { return war; } set { war = value; OnPropertyChanged(nameof(WAR)); } }
        public ArrJob WHM { get { return whm; } set { whm = value; OnPropertyChanged(nameof(WHM)); } }
        public ArrJob SCH { get { return sch; } set { sch = value; OnPropertyChanged(nameof(SCH)); } }
        public ArrJob MNK { get { return mnk; } set { mnk = value; OnPropertyChanged(nameof(MNK)); } }
        public ArrJob DRG { get { return drg; } set { drg = value; OnPropertyChanged(nameof(DRG)); } }
        public ArrJob NIN { get { return nin; } set { nin = value; OnPropertyChanged(nameof(NIN)); } }
        public ArrJob BRD { get { return brd; } set { brd = value; OnPropertyChanged(nameof(BRD)); } }
        public ArrJob BLM { get { return blm; } set { blm = value; OnPropertyChanged(nameof(BLM)); } }
        public ArrJob SMN { get { return smn; } set { smn = value; OnPropertyChanged(nameof(SMN)); } }
        #endregion


        #region Stage Models
        public RelicModel RelicModel
        {
            get { return relicModel; }
            set
            {
                if (value != null)
                {
                    relicModel = value;
                    OnPropertyChanged(nameof(RelicModel));
                }
            }
        }

        public AtmaModel AtmaModel
        {
            get { return atmaModel; }
            set
            {
                if (value != null)
                {
                    atmaModel = value;
                    OnPropertyChanged(nameof(AtmaModel));
                }
            }
        }

        public AnimusModel AnimusModel
        {
            get { return animusModel; }
            set
            {
                if (value != null)
                {
                    animusModel = value;
                    OnPropertyChanged(nameof(AnimusModel));
                }
            }
        }

        public NovusModel NovusModel
        {
            get { return novusModel; }
            set
            {
                if (value != null)
                {
                    novusModel = value;
                    OnPropertyChanged(nameof(NovusModel));
                }
            }
        }

        public NexusModel NexusModel
        {
            get { return nexusModel; }
            set
            {
                if (value != null)
                {
                    nexusModel = value;
                    OnPropertyChanged(nameof(NexusModel));
                }
            }
        }
        public BravesModel BravesModel
        {
            get { return bravesModel; }
            set
            {
                if (value != null)
                {
                    bravesModel = value;
                    OnPropertyChanged(nameof(BravesModel));
                }
            }
        }
        public ZetaModel ZetaModel
        {
            get { return zetaModel; }
            set
            {
                if (value != null)
                {
                    zetaModel = value;
                    OnPropertyChanged(nameof(ZetaModel));
                }
            }
        }

        #endregion
        
    }
}
