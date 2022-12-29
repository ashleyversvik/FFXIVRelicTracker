using FFXIVRelicTracker._04_SB.SBHelpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System.Collections.Generic;
using System.Windows.Input;

namespace FFXIVRelicTracker._04_SB._00_Summary
{
    public class SBSummaryViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private IEventAggregator eventAggregator;
        private SBSummaryModel summaryModel;
        private Character selectedCharacter;
        #endregion

        #region Constructor

        public SBSummaryViewModel(IEventAggregator eventAggregator)
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
        public SBSummaryModel SummaryModel
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
                SummaryModel = new SBSummaryModel(SelectedCharacter);
                OnPropertyChanged(nameof(SelectedCharacter));
            }
        }
        public string SummaryLayout
        {
            get { return selectedCharacter.SBLayout; }
            set { selectedCharacter.SBLayout = value; OnPropertyChanged(nameof(SummaryLayout)); }
        }
        public List<string> Summaries { get { return new List<string> { "Horizontal", "Vertical" }; } }

        #region Expose Job Objects to VM
        public SBJob PLD { get { return SelectedCharacter.SBModel.PLD; } }
        public SBJob WAR { get { return SelectedCharacter.SBModel.WAR; } }
        public SBJob DRK { get { return SelectedCharacter.SBModel.DRK; } }
        public SBJob WHM { get { return SelectedCharacter.SBModel.WHM; } }
        public SBJob SCH { get { return SelectedCharacter.SBModel.SCH; } }
        public SBJob AST { get { return SelectedCharacter.SBModel.AST; } }
        public SBJob MNK { get { return SelectedCharacter.SBModel.MNK; } }
        public SBJob DRG { get { return SelectedCharacter.SBModel.DRG; } }
        public SBJob NIN { get { return SelectedCharacter.SBModel.NIN; } }
        public SBJob SAM { get { return SelectedCharacter.SBModel.SAM; } }
        public SBJob BRD { get { return SelectedCharacter.SBModel.BRD; } }
        public SBJob MCH { get { return SelectedCharacter.SBModel.MCH; } }
        public SBJob BLM { get { return SelectedCharacter.SBModel.BLM; } }
        public SBJob SMN { get { return SelectedCharacter.SBModel.SMN; } }
        public SBJob RDM { get { return SelectedCharacter.SBModel.RDM; } }
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
                        param => this.SBCommand(param),
                        param => this.SBCan()
                        );
                }
                return _SummaryClick;
            }
        }

        private bool SBCan() { return true; }
        private void SBCommand(object param)
        {
            string[] values = ((string)param).Split(".");

            SBJob tempJob = (SBJob)SelectedCharacter.SBModel.GetType().GetProperty(values[0]).GetValue(SelectedCharacter.SBModel);
            SBStageCompleter.ProgressClass(SelectedCharacter, tempJob.Name, values[1]);
        }
        #endregion
    }
}
