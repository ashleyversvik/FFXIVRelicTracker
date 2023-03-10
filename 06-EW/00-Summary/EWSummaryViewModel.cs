using FFXIVRelicTracker._06_EW.EWHelpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System.Collections.Generic;
using System.Windows.Input;

namespace FFXIVRelicTracker._06_EW._00_Summary
{
    public class EWSummaryViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private IEventAggregator eventAggregator;
        private EWSummaryModel summaryModel;
        private Character selectedCharacter;
        #endregion

        #region Constructor

        public EWSummaryViewModel(IEventAggregator eventAggregator)
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
        public EWSummaryModel SummaryModel
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
                SummaryModel = new EWSummaryModel(SelectedCharacter);
                OnPropertyChanged(nameof(SelectedCharacter));
            }
        }
        public string SummaryLayout
        {
            get { return selectedCharacter.EWLayout; }
            set { selectedCharacter.EWLayout = value; OnPropertyChanged(nameof(SummaryLayout)); }
        }
        public List<string> Summaries { get { return new List<string> { "Horizontal", "Vertical" }; } }

        #region Expose Job Objects to VM
        public EWJob PLD { get { return SelectedCharacter.EWModel.PLD; } }
        public EWJob WAR { get { return SelectedCharacter.EWModel.WAR; } }
        public EWJob DRK { get { return SelectedCharacter.EWModel.DRK; } }
        public EWJob GNB { get { return SelectedCharacter.EWModel.GNB; } }
        public EWJob WHM { get { return SelectedCharacter.EWModel.WHM; } }
        public EWJob SCH { get { return SelectedCharacter.EWModel.SCH; } }
        public EWJob AST { get { return SelectedCharacter.EWModel.AST; } }
        public EWJob SGE { get { return SelectedCharacter.EWModel.SGE; } }
        public EWJob MNK { get { return SelectedCharacter.EWModel.MNK; } }
        public EWJob DRG { get { return SelectedCharacter.EWModel.DRG; } }
        public EWJob NIN { get { return SelectedCharacter.EWModel.NIN; } }
        public EWJob SAM { get { return SelectedCharacter.EWModel.SAM; } }
        public EWJob RPR { get { return SelectedCharacter.EWModel.RPR; } }
        public EWJob BRD { get { return SelectedCharacter.EWModel.BRD; } }
        public EWJob MCH { get { return SelectedCharacter.EWModel.MCH; } }
        public EWJob DNC { get { return SelectedCharacter.EWModel.DNC; } }
        public EWJob BLM { get { return SelectedCharacter.EWModel.BLM; } }
        public EWJob SMN { get { return SelectedCharacter.EWModel.SMN; } }
        public EWJob RDM { get { return SelectedCharacter.EWModel.RDM; } }
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
                        param => this.EWCommand(param),
                        param => this.EWCan()
                        );
                }
                return _SummaryClick;
            }
        }

        private bool EWCan() { return true; }
        private void EWCommand(object param)
        {
            string[] values = ((string)param).Split(".");

            EWJob tempJob = (EWJob)SelectedCharacter.EWModel.GetType().GetProperty(values[0]).GetValue(SelectedCharacter.EWModel);
            EWStageCompleter.ProgressClass(SelectedCharacter, tempJob.Name, values[1]);
        }
        #endregion
    }
}
