using FFXIVRelicTracker._02_Arr;
using FFXIVRelicTracker._03_HW.Main;
using FFXIVRelicTracker._04_SB.Main;
using FFXIVRelicTracker._05_ShB.Main;
using FFXIVRelicTracker._05_Skysteel.Main;
using FFXIVRelicTracker._06_EW.Main;
using FFXIVRelicTracker.PVP.Main;
using FFXIVRelicTracker.Models.Helpers;
using System.Reflection;


namespace FFXIVRelicTracker.Models
{


    public class Character : ObservableObject
    {
        private string name;
        private string server;
        private ArrModel arrModel;
        private ShBModel shbModel;
        private SkysteelModel skysteelModel;
        private HWModel hwModel;
        private SBModel sbModel;
        private EWModel ewModel;
        private PVPModel pvpModel;

        #region Properties

        public string ArrLayout { get; set; }
        public string HWLayout { get; set; }
        public string SBLayout { get; set; }
        public string ShBLayout { get; set; }
        public string SkysteelLayout { get; set; }
        public string EWLayout { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                if(name!=value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        public string Server
        {
            get { return server; }
            set
            {
                if (server != value)
                {
                    server = value;
                    OnPropertyChanged(nameof(Server));
                }
            }
        }
        public ArrModel ArrModel
        {
            get { return arrModel; }
            set
            {
                if (value != null)
                {
                    arrModel = value;
                    OnPropertyChanged(nameof(ArrModel));
                }
            }
        }
        public HWModel HWModel
        {
            get { return hwModel; }
            set
            {
                if (value != null)
                {
                    hwModel = value;
                    OnPropertyChanged(nameof(HWModel));
                }
            }
        }
        public SBModel SBModel
        {
            get { return sbModel; }
            set
            {
                if (value != null)
                {
                    sbModel = value;
                    OnPropertyChanged(nameof(SBModel));
                }
            }
        }
        public ShBModel ShBModel
        {
            get { return shbModel; }
            set
            {
                if (value != null)
                {
                    shbModel = value;
                    OnPropertyChanged(nameof(ShBModel));
                }
            }
        }
        public SkysteelModel SkysteelModel
        {
            get { return skysteelModel; }
            set
            {
                if (value != null)
                {
                    skysteelModel = value;
                    OnPropertyChanged(nameof(SkysteelModel));
                }
            }
        }
        public EWModel EWModel
        {
            get { return ewModel; }
            set
            {
                if (value != null)
                {
                    ewModel = value;
                    OnPropertyChanged(nameof(EWModel));
                }
            }
        }
        public PVPModel PVPModel
        {
            get { return pvpModel; }
            set
            {
                if (value != null)
                {
                    pvpModel = value;
                    OnPropertyChanged(nameof(PVPModel));
                }
            }
        }
        #endregion
        public Character()
        {
            Name = "Default Name";
            Server = "Default Server";

            ArrModel = new ArrModel();
            ShBModel = new ShBModel();
            SkysteelModel = new SkysteelModel();
            HWModel = new HWModel();
            SBModel = new SBModel();
            EWModel = new EWModel();
            PVPModel = new PVPModel();
        }

        public Character(string name, string server )
        {
            Name = name;
            Server = server;
            ArrModel = new ArrModel();
            ShBModel = new ShBModel();
            HWModel = new HWModel();
            SBModel = new SBModel();
            SkysteelModel = new SkysteelModel();
            EWModel = new EWModel();
            PVPModel = new PVPModel();

            ArrLayout = "Horizontal";
            HWLayout = "Horizontal";
            SBLayout = "Horizontal";
            ShBLayout = "Horizontal";
            SkysteelLayout = "Horizontal";
            EWLayout = "Horizontal";

        }

        public Character(Character oldCharacter)
            :this()
        {
            foreach (PropertyInfo propertyInfo in oldCharacter.GetType().GetProperties())
            {

                if (propertyInfo.GetValue(oldCharacter) != null)
                {
                    propertyInfo.SetValue(this, propertyInfo.GetValue(oldCharacter));
                }
            }

            if (string.IsNullOrEmpty(ArrLayout)) { ArrLayout = "Horizontal"; }
            if (string.IsNullOrEmpty(HWLayout)) { HWLayout = "Horizontal"; }
            if (string.IsNullOrEmpty(SBLayout)) { SBLayout = "Horizontal"; }
            if (string.IsNullOrEmpty(ShBLayout)) { ShBLayout = "Horizontal"; }
            if (string.IsNullOrEmpty(SkysteelLayout)) { SkysteelLayout = "Horizontal"; }
            if (string.IsNullOrEmpty(EWLayout)) { EWLayout = "Horizontal"; }

        }

        public override string ToString()
        {
            return name + " : " + server;
        }
    }
}
