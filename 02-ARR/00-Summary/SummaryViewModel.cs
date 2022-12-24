using FFXIVRelicTracker._02_ARR.ArrHelpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System.Collections.Generic;
using System.Windows.Input;

namespace FFXIVRelicTracker._02_ARR._00_Summary
{
    class SummaryViewModel : ObservableObject, IPageViewModel
    {
        private IEventAggregator eventAggregator;

        public SummaryViewModel(IEventAggregator eventAggregator)
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

        private SummaryModel summaryModel;
        public SummaryModel SummaryModel
        {
            get { return summaryModel; }
            set
            {
                summaryModel = value;
                OnPropertyChanged(nameof(SummaryModel));
            }
        }

        private Character selectedCharacter;
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                selectedCharacter = value;
                SummaryModel = new SummaryModel(SelectedCharacter);
                OnPropertyChanged(nameof(SelectedCharacter));
            }
        }

        public string SummaryLayout
        {
            get { return selectedCharacter.ArrLayout; }
            set { selectedCharacter.ArrLayout = value; OnPropertyChanged(nameof(SummaryLayout)); }
        }
        public List<string> Summaries { get { return new List<string> { "Horizontal", "Vertical" }; } }

        public string Name {get{return "Summary";}}

        #region Expose Job Objects to VM
        public ArrJob PLD { get { return SelectedCharacter.ArrModel.PLD; } }
        public ArrJob WAR { get { return SelectedCharacter.ArrModel.WAR; } }
        public ArrJob WHM { get { return SelectedCharacter.ArrModel.WHM; } }
        public ArrJob SCH { get { return SelectedCharacter.ArrModel.SCH; } }
        public ArrJob MNK { get { return SelectedCharacter.ArrModel.MNK; } }
        public ArrJob DRG { get { return SelectedCharacter.ArrModel.DRG; } }
        public ArrJob NIN { get { return SelectedCharacter.ArrModel.NIN; } }
        public ArrJob BRD { get { return SelectedCharacter.ArrModel.BRD; } }
        public ArrJob BLM { get { return SelectedCharacter.ArrModel.BLM; } }
        public ArrJob SMN { get { return SelectedCharacter.ArrModel.SMN; } }
        #endregion

        #region ArrButton Command
        private ICommand _SummaryClick;
        public ICommand SummaryClick
        {
            get
            {
                if (_SummaryClick == null)
                {
                    _SummaryClick = new RelayCommand(
                        param => this.ArrCommand(param),
                        param => this.ArrCan()
                        );
                }
                return _SummaryClick;
            }
        }

        private bool ArrCan() { return true; }
        private void ArrCommand(object param)
        {
            string[] values = ((string)param).Split(".");

            ArrJob tempJob = (ArrJob)SelectedCharacter.ArrModel.GetType().GetProperty(values[0]).GetValue(SelectedCharacter.ArrModel);
            ArrProgress tempProgress = tempJob.StageList.Find(x => x.Name == values[1]);

            ArrStageCompleter.ProgressClass(SelectedCharacter, tempJob.Name, tempProgress);
        }

        public void LoadAvailableJobs()
        {
        }

        #endregion
    }
}
