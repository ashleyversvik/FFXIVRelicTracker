﻿using FFXIVRelicTracker._05_ShB.ShBHelpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System.Collections.Generic;
using System.Windows.Input;

namespace FFXIVRelicTracker._05_ShB._00_Summary
{
    public class ShBSummaryViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private IEventAggregator eventAggregator;
        private ShBSummaryModel summaryModel;
        private Character selectedCharacter;
        #endregion

        #region Constructor

        public ShBSummaryViewModel(IEventAggregator eventAggregator)
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
        public ShBSummaryModel SummaryModel
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
                SummaryModel = new ShBSummaryModel(SelectedCharacter);
                OnPropertyChanged(nameof(SelectedCharacter));
            }
        }
        public string SummaryLayout
        {
            get { return selectedCharacter.ShBLayout; }
            set { selectedCharacter.ShBLayout = value; OnPropertyChanged(nameof(SummaryLayout)); }
        }
        public List<string> Summaries { get { return new List<string> { "Horizontal", "Vertical" }; } }

        #region Expose Job Objects to VM
        public ShBJob PLD { get { return SelectedCharacter.ShBModel.PLD; } }
        public ShBJob WAR { get { return SelectedCharacter.ShBModel.WAR; } }
        public ShBJob DRK { get { return SelectedCharacter.ShBModel.DRK; } }
        public ShBJob GNB { get { return SelectedCharacter.ShBModel.GNB; } }
        public ShBJob WHM { get { return SelectedCharacter.ShBModel.WHM; } }
        public ShBJob SCH { get { return SelectedCharacter.ShBModel.SCH; } }
        public ShBJob AST { get { return SelectedCharacter.ShBModel.AST; } }
        public ShBJob MNK { get { return SelectedCharacter.ShBModel.MNK; } }
        public ShBJob DRG { get { return SelectedCharacter.ShBModel.DRG; } }
        public ShBJob NIN { get { return SelectedCharacter.ShBModel.NIN; } }
        public ShBJob SAM { get { return SelectedCharacter.ShBModel.SAM; } }
        public ShBJob BRD { get { return SelectedCharacter.ShBModel.BRD; } }
        public ShBJob MCH { get { return SelectedCharacter.ShBModel.MCH; } }
        public ShBJob DNC { get { return SelectedCharacter.ShBModel.DNC; } }
        public ShBJob BLM { get { return SelectedCharacter.ShBModel.BLM; } }
        public ShBJob SMN { get { return SelectedCharacter.ShBModel.SMN; } }
        public ShBJob RDM { get { return SelectedCharacter.ShBModel.RDM; } }
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
                        param => this.ShBCommand(param),
                        param => this.ShBCan()
                        );
                }
                return _SummaryClick;
            }
        }

        private bool ShBCan() { return true; }
        private void ShBCommand(object param)
        {

            string[] values = ((string)param).Split(".");

            ShBJob tempJob = (ShBJob)SelectedCharacter.ShBModel.GetType().GetProperty(values[0]).GetValue(SelectedCharacter.ShBModel);
            ShBProgress tempProgress = tempJob.StageList.Find(x => x.Name == values[1]);

            ShBStageCompleter.ProgressClass(SelectedCharacter, tempJob.Name, tempProgress);

        }
        #endregion
    }
}
