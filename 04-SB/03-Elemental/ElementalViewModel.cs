using FFXIVRelicTracker._04_SB.SBHelpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FFXIVRelicTracker._04_SB._03_Elemental
{

    public class ElementalViewModel : ObservableObject, IPageViewModel
    {
        public string Name => "Elemental";


        #region Fields
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private ElementalModel elementalModel;
        private ObservableCollection<string> availableJobs;
        #endregion

        #region Constructor
        public ElementalViewModel()
        {

        }

        public ElementalViewModel(IEventAggregator eventAggregator)
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
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    ElementalModel = SelectedCharacter.SBModel.ElementalModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }

        public ElementalModel ElementalModel
        {
            get { return elementalModel; }
            set
            {
                elementalModel = value;
                OnPropertyChanged(nameof(ElementalModel));
            }
        }

        public string SelectedJob
        {
            get { return elementalModel.SelectedJob; }
            set
            {
                if (value != null && value != elementalModel.SelectedJob) ResetWeaponState();
                elementalModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
            }
        }

        public ObservableCollection<string> AvailableJobs
        {
            get { return availableJobs; }
            set
            {
                availableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }
        public int FrostedProteanNeeded
        {
            get
            {
                if (AvailableJobs != null)
                {
                    return AvailableJobs.Count * 31; 
                }
                else { return 0; };
            }
        }
        public int FrostedProteanRemaining
        {
            get
            {
                if (AvailableJobs == null) { LoadAvailableJobs(); }
                return FrostedProteanNeeded - FrostedProteanCount;
            }
        }
        public int FrostedProteanCount
        {
            get
            {
                if (elementalModel.FrostedProteanCount < 0) { FrostedProteanCount = 0; }
                return elementalModel.FrostedProteanCount;
            }
            set
            {
                if (value < 0) { elementalModel.FrostedProteanCount = 0; }
                else { elementalModel.FrostedProteanCount = value; }
                OnPropertyChanged(nameof(FrostedProteanCount));
                OnPropertyChanged(nameof(FrostedProteanRemaining));
            }
        }
        public int PagosNeeded
        {
            get
            {
                if (AvailableJobs != null)
                {
                    if (elementalModel.BuyIce) { return AvailableJobs.Count * 750; }
                    else { return AvailableJobs.Count * 500; }
                }
                else { return 0; };
            }
        }
        public int PagosRemaining
        {
            get
            {
                if (AvailableJobs == null) { LoadAvailableJobs(); }
                return PagosNeeded - PagosCount - (IceCount*50);
            }
        }
        public int PagosCount
        {
            get
            {
                if (elementalModel.PagosCount < 0) { PagosCount = 0; }
                return elementalModel.PagosCount;
            }
            set
            {
                if (value < 0) { elementalModel.PagosCount = 0; }
                else { elementalModel.PagosCount = value; }
                OnPropertyChanged(nameof(PagosCount));
                OnPropertyChanged(nameof(PagosRemaining));
            }
        }
        public int IceNeeded { get { if (AvailableJobs != null) { return AvailableJobs.Count * 5; } else { return 0; } } }
        public int IceRemaining
        {
            get
            {
                if (AvailableJobs == null) { LoadAvailableJobs(); }
                return IceNeeded - IceCount;
            }
        }
        public int IceCount
        {
            get
            {
                if (elementalModel.IceCount < 0) { IceCount = 0; }
                return elementalModel.IceCount;
            }
            set
            {
                if (value < 0) { elementalModel.IceCount = 0; }
                else { elementalModel.IceCount = value; }
                OnPropertyChanged(nameof(IceCount));
                OnPropertyChanged(nameof(IceRemaining));
                OnPropertyChanged(nameof(PagosRemaining));
            }
        }

        public bool BuyIce
        {
            get
            {
                return elementalModel.BuyIce;
            }
            set
            {
                elementalModel.BuyIce = value;
                OnPropertyChanged(nameof(BuyIce));
                OnPropertyChanged(nameof(PagosNeeded));
                OnPropertyChanged(nameof(PagosRemaining));
            }
        }
        private ObservableCollection<bool> WeaponState
        {
            get
            {
                if (elementalModel.WeaponState == null)
                {
                    elementalModel.WeaponState = new ObservableCollection<bool>()
                    {
                        false,
                        false                        
                    };
                }
                return elementalModel.WeaponState;
            }
            set
            {
                elementalModel.WeaponState = value;
                RecheckBools();
            }
        }

        public bool State00 { get { return WeaponState[00]; } set { WeaponState[00] = value; AdjustBools(00, value); } }
        public bool State01 { get { return WeaponState[01]; } set { WeaponState[01] = value; AdjustBools(01, value); } }
        #endregion

        #region Methods
        public void LoadAvailableJobs()
        {
            AvailableJobs = SBInfo.LoadJobs(AvailableJobs, SelectedCharacter, Name);
            OnPropertyChanged(nameof(FrostedProteanNeeded));
            OnPropertyChanged(nameof(FrostedProteanCount));
            OnPropertyChanged(nameof(IceNeeded));
            OnPropertyChanged(nameof(IceCount));
        }

        private void AdjustBools(int BoolIndex, bool value)
        {
            if (value)
            {
                for (int index = 0; index <= BoolIndex; index++)
                {
                    WeaponState[index] = true;
                }
            }
            else
            {
                for (int index = BoolIndex; index < WeaponState.Count; index++)
                {
                    WeaponState[index] = false;
                }
            }
            RecheckBools();
        }
        private void RecheckBools()
        {
            OnPropertyChanged(nameof(State00));
            OnPropertyChanged(nameof(State01));
        }
        public void ResetWeaponState()
        {
            elementalModel.WeaponState = new ObservableCollection<bool>
            {
                false, false
            };
            RecheckBools();
        }
        #endregion

        #region Commands
        #region Complete Button
        private ICommand _CompleteButton;

        public ICommand CompleteButton
        {
            get
            {
                if (_CompleteButton == null)
                {
                    _CompleteButton = new RelayCommand(
                        param => this.CompleteCommand(),
                        param => this.CompleteCan()
                        );
                }
                return _CompleteButton;
            }
        }

        private bool CompleteCan() { return SelectedJob != null; }
        private void CompleteCommand()
        {
            SBStageCompleter.ProgressClass(selectedCharacter, SelectedJob, Name);
            LoadAvailableJobs();
            OnPropertyChanged(nameof(FrostedProteanCount));
            OnPropertyChanged(nameof(IceCount));
        }
        #endregion

        #region Increment FrostedProtean
        private ICommand _FrostedProteanButton;

        public ICommand FrostedProteanButton
        {
            get
            {
                if (_FrostedProteanButton == null)
                {
                    _FrostedProteanButton = new RelayCommand(
                        param => this.FrostedProteanCommand(param)
                        );
                }
                return _FrostedProteanButton;
            }
        }

        private void FrostedProteanCommand(object param)
        {
            FrostedProteanCount += 1;
        }
        #endregion

        #region Increment Ice
        private ICommand _IceButton;

        public ICommand IceButton
        {
            get
            {
                if (_IceButton == null)
                {
                    _IceButton = new RelayCommand(
                        param => this.IceCommand(param)
                        );
                }
                return _IceButton;
            }
        }

        private void IceCommand(object param)
        {
            IceCount += 1;
        }
        #endregion

        #region Increment Pagos
        private ICommand _PagosButton;

        public ICommand PagosButton
        {
            get
            {
                if (_PagosButton == null)
                {
                    _PagosButton = new RelayCommand(
                        param => this.PagosCommand(param)
                        );
                }
                return _PagosButton;
            }
        }

        private void PagosCommand(object param)
        {
            PagosCount+=1;
        }
        #endregion
        #endregion


    }
}
