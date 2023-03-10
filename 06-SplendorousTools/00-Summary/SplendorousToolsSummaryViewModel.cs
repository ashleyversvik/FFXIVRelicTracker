using FFXIVRelicTracker._06_SplendorousTools.Splendorous_Helpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System.Collections.Generic;
using System.Windows.Input;

namespace FFXIVRelicTracker._06_SplendorousTools._00_Summary
{
    class SplendorousToolsSummaryViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private SplendorousToolsSummaryModel summaryModel;
        private Character selectedCharacter;
        private IEventAggregator eventAggregator;

        #endregion

        #region Constructors
        public SplendorousToolsSummaryViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            SubscriptionToken subscriptionToken =
                        this
                            .eventAggregator
                            .GetEvent<PubSubEvent<Character>>()
                            .Subscribe((details) =>
                            {
                                this.SelectedCharacter = details;
                            });

        }
        #endregion

        #region Properties
        public string Name => "Summary";
        public SplendorousToolsSummaryModel SummaryModel
        {
            get { return summaryModel; }
            set
            {
                summaryModel = value;
                OnPropertyChanged(nameof(SummaryModel));
            }
        }
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                selectedCharacter = value;
                SummaryModel = new SplendorousToolsSummaryModel(SelectedCharacter);
                OnPropertyChanged(nameof(SelectedCharacter));
            }
        }
        public string SummaryLayout
        {
            get { return selectedCharacter.SplendorousLayout; }
            set { selectedCharacter.SplendorousLayout = value; OnPropertyChanged(nameof(SummaryLayout)); }
        }
        public List<string> Summaries { get { return new List<string> { "Horizontal", "Vertical" }; } }

        #region Expose Jobs to VM
        public SplendorousToolsJob CRP{ get { return SelectedCharacter.SplendorousToolsModel.CRP; } }
        public SplendorousToolsJob BSM{ get { return SelectedCharacter.SplendorousToolsModel.BSM; } }
        public SplendorousToolsJob ARM{ get { return SelectedCharacter.SplendorousToolsModel.ARM; } }
        public SplendorousToolsJob GSM{ get { return SelectedCharacter.SplendorousToolsModel.GSM; } }
        public SplendorousToolsJob LTW{ get { return SelectedCharacter.SplendorousToolsModel.LTW; } }
        public SplendorousToolsJob WVR{ get { return SelectedCharacter.SplendorousToolsModel.WVR; } }
        public SplendorousToolsJob ALC{ get { return SelectedCharacter.SplendorousToolsModel.ALC; } }
        public SplendorousToolsJob CUL{ get { return SelectedCharacter.SplendorousToolsModel.CUL; } }
        public SplendorousToolsJob MIN{ get { return SelectedCharacter.SplendorousToolsModel.MIN; } }
        public SplendorousToolsJob BTN{ get { return SelectedCharacter.SplendorousToolsModel.BTN; } }
        public SplendorousToolsJob FSH{ get { return SelectedCharacter.SplendorousToolsModel.FSH; } }

        #endregion
        #endregion

        #region Methods
        public void LoadAvailableJobs()
        {
        }
        #endregion

        #region Commands
        private ICommand _SummaryClick;
        public ICommand SummaryClick
        {
            get
            {
                if (_SummaryClick == null)
                {
                    _SummaryClick = new RelayCommand(
                        param => this.SplendorousToolsCommand(param),
                        param => this.SplendorousToolsCan()
                        );
                }
                return _SummaryClick;
            }
        }

        private bool SplendorousToolsCan() { return true; }
        private void SplendorousToolsCommand(object param)
        {

            string[] values = ((string)param).Split(".");

            SplendorousToolsJob tempJob = (SplendorousToolsJob)SelectedCharacter.SplendorousToolsModel.GetType().GetProperty(values[0]).GetValue(SelectedCharacter.SplendorousToolsModel);
            SplendorousToolsInfo.ProgressClass(SelectedCharacter, tempJob.Name, values[1]);

        }
        #endregion


    }
}
