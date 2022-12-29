using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using FFXIVRelicTracker.PVP.PVPHelpers;
using Prism.Events;
using System;
using System.Windows.Input;

namespace FFXIVRelicTracker.PVP._00_Series
{
    public class PVPSeriesViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private IEventAggregator eventAggregator;
        private PVPSeriesModel pvpSeriesModel;
        private Character selectedCharacter;
        #endregion

        #region Constructor

        public PVPSeriesViewModel(IEventAggregator eventAggregator)
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
        public string Name => "Series";
        public PVPSeriesModel PVPSeriesModel
        {
            get { return pvpSeriesModel; }
            set
            {
                pvpSeriesModel = value;
                OnPropertyChanged(nameof(PVPSeriesModel));
            }
        }

        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                selectedCharacter = value;
                PVPSeriesModel = SelectedCharacter.PVPModel.PVPSeriesModel;
                OnPropertyChanged(nameof(SelectedCharacter));
            }
        }

        public int CurrentLevel
        {
            get
            {
                if (pvpSeriesModel.CurrentLevel > PVPInfo.MAX_LEVEL) { pvpSeriesModel.CurrentLevel = PVPInfo.MAX_LEVEL; }
                else if (pvpSeriesModel.CurrentLevel < 1) { pvpSeriesModel.CurrentLevel = 1; }
                return pvpSeriesModel.CurrentLevel;
            }
            set
            {
                if (value >= 1 & value <= PVPInfo.MAX_LEVEL)
                {
                    pvpSeriesModel.CurrentLevel = value;
                    UpdateDisplays();
                }
            }
        }
        public int DesiredLevel
        {
            get
            {
                if (pvpSeriesModel.DesiredLevel < 1 || pvpSeriesModel.DesiredLevel > PVPInfo.MAX_LEVEL) { pvpSeriesModel.DesiredLevel = PVPInfo.MAX_LEVEL; }
                return pvpSeriesModel.DesiredLevel;
            }
            set
            {
                if (value >= 1 & value <= PVPInfo.MAX_LEVEL)
                {
                    pvpSeriesModel.DesiredLevel = value;
                    UpdateDisplays();
                }
            }
        }
        public int CurrentExp
        {
            get
            {
                return pvpSeriesModel.CurrentExp;
            }
            set
            {
                if (value < 0)
                {
                    CurrentExp = 0;
                }
                else if (PVPInfo.ExpPerLevel[CurrentLevel] <= value)
                {
                    pvpSeriesModel.CurrentExp = value - PVPInfo.ExpPerLevel[CurrentLevel];
                    CurrentLevel++;
                } else {
                    pvpSeriesModel.CurrentExp = value;
                }

                UpdateDisplays();
            }
        }

        public int CurrentLevelExp => PVPInfo.ExpPerLevel[CurrentLevel];

        public decimal NeededFL1st => Math.Ceiling((decimal)(PVPInfo.ExpGraph[DesiredLevel] - PVPInfo.ExpGraph[CurrentLevel] - CurrentExp) / (int)MatchExp.Frontline_Win);
        public decimal NeededFL2nd => Math.Ceiling((decimal)(PVPInfo.ExpGraph[DesiredLevel] - PVPInfo.ExpGraph[CurrentLevel] - CurrentExp) / (int)MatchExp.Frontline_2nd);
        public decimal NeededFL3rd => Math.Ceiling((decimal)(PVPInfo.ExpGraph[DesiredLevel] - PVPInfo.ExpGraph[CurrentLevel] - CurrentExp) / (int)MatchExp.Frontline_3rd);
        public decimal NeededCCWins => Math.Ceiling((decimal)(PVPInfo.ExpGraph[DesiredLevel] - PVPInfo.ExpGraph[CurrentLevel] - CurrentExp) / (int)MatchExp.CC_Win);
        public decimal NeededCCLosses => Math.Ceiling((decimal)(PVPInfo.ExpGraph[DesiredLevel] - PVPInfo.ExpGraph[CurrentLevel] - CurrentExp) / (int)MatchExp.CC_Loss);

        #endregion

        #region Methods

        public void LoadAvailableJobs()
        {
        }

        public void UpdateDisplays()
        {
            OnPropertyChanged(nameof(CurrentLevel));
            OnPropertyChanged(nameof(DesiredLevel));
            OnPropertyChanged(nameof(CurrentExp));
            OnPropertyChanged(nameof(CurrentLevelExp));
            OnPropertyChanged(nameof(NeededFL1st));
            OnPropertyChanged(nameof(NeededFL2nd));
            OnPropertyChanged(nameof(NeededFL3rd));
            OnPropertyChanged(nameof(NeededCCWins));
            OnPropertyChanged(nameof(NeededCCLosses));
        }
        #endregion

        #region Add Exp
        private ICommand _IncrementButton;

        public ICommand IncrementButton
        {
            get
            {
                if (_IncrementButton == null)
                {
                    _IncrementButton = new RelayCommand(
                        param => this.IncrementCommand(param)
                        );
                }
                return _IncrementButton;
            }
        }
        private void IncrementCommand(object param)
        {
            int exp = int.Parse((string)param);
            switch (exp)
            {
                case 1:
                    CurrentLevel++;
                    break;
                case 2:
                    CurrentExp += (int)MatchExp.Frontline_Win;
                    break;
                case 3:
                    CurrentExp += (int)MatchExp.Frontline_2nd;
                    break;
                case 4:
                    CurrentExp += (int)MatchExp.Frontline_3rd;
                    break;
                case 5:
                    CurrentExp += (int)MatchExp.CC_Win;
                    break;
                case 6:
                    CurrentExp += (int)MatchExp.CC_Loss;
                    break;
                default:
                    CurrentExp += exp;
                    break;
            }
        }
        #endregion
    }
}
