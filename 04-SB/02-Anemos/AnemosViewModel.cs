using FFXIVRelicTracker._04_SB.SBHelpers;
using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FFXIVRelicTracker._04_SB._02_Anemos
{

    public class AnemosViewModel : ObservableObject, IPageViewModel
    {
        public string Name => "Anemos";

        #region Fields
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private AnemosModel anemosModel;
        #endregion

        #region Constructor
        public AnemosViewModel()
        {

        }

        public AnemosViewModel(IEventAggregator eventAggregator)
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
                    AnemosModel = SelectedCharacter.SBModel.AnemosModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }

        public AnemosModel AnemosModel
        {
            get { return anemosModel; }
            set
            {
                anemosModel = value;
                OnPropertyChanged(nameof(AnemosModel));
            }
        }

        public string SelectedJob
        {
            get { return anemosModel.SelectedJob; }
            set
            {
                if (value != null && value != anemosModel.SelectedJob) ResetWeaponState();
                anemosModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
            }
        }

        public ObservableCollection<string> AvailableJobs
        {
            get { return anemosModel.AvailableJobs; }
            set
            {
                anemosModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }

        public int ProteanNeeded 
        { 
            get 
            { 
                if (AvailableJobs != null) 
                { 
                    if (anemosModel.BuyFeathers) { return AvailableJobs.Count * 2200; } 
                    else { return AvailableJobs.Count * 1300; }
                } 
                else { return 0; }; 
            } 
        }
        public int ProteanRemaining
        {
            get
            {
                if (AvailableJobs == null) { LoadAvailableJobs(); }
                return ProteanNeeded - ProteanCount - (FeatherCount*300);
            }
        }
        public int ProteanCount
        {
            get
            {
                if (anemosModel.ProteanCount < 0) { ProteanCount = 0; }
                return anemosModel.ProteanCount;
            }
            set
            {
                if (value < 0) { anemosModel.ProteanCount = 0; }
                else { anemosModel.ProteanCount = value; }
                OnPropertyChanged(nameof(ProteanCount));
                OnPropertyChanged(nameof(ProteanRemaining));
            }
        }
        public int FeatherNeeded { get { if (AvailableJobs != null) { return AvailableJobs.Count * 3; } else { return 0; } } }
        public int FeatherRemaining
        {
            get
            {
                if (AvailableJobs == null) { LoadAvailableJobs(); }
                return FeatherNeeded - FeatherCount;
            }
        }
        public int FeatherCount
        {
            get
            {
                if (anemosModel.FeatherCount < 0) { FeatherCount = 0; }
                return anemosModel.FeatherCount;
            }
            set
            {
                if (value < 0) { anemosModel.FeatherCount = 0; }
                else { anemosModel.FeatherCount = value; }
                OnPropertyChanged(nameof(FeatherCount));
                OnPropertyChanged(nameof(FeatherRemaining));
                OnPropertyChanged(nameof(ProteanRemaining));
            }
        }

        public bool BuyFeathers
        {
            get
            {
                return anemosModel.BuyFeathers;
            }
            set
            {
                anemosModel.BuyFeathers = value;
                OnPropertyChanged(nameof(BuyFeathers));
                OnPropertyChanged(nameof(ProteanNeeded));
                OnPropertyChanged(nameof(ProteanRemaining));
            }
        }

        private ObservableCollection<bool> WeaponState
        {
            get
            {
                if (anemosModel.WeaponState == null)
                {
                    anemosModel.WeaponState = new ObservableCollection<bool>()
                    {
                        false,
                        false,
                        false
                    };
                }
                return anemosModel.WeaponState;
            }
            set
            {
                anemosModel.WeaponState = value;
                RecheckBools();
            }
        }

        public bool State00 { get { return WeaponState[00]; } set { WeaponState[00] = value; AdjustBools(00, value); } }
        public bool State01 { get { return WeaponState[01]; } set { WeaponState[01] = value; AdjustBools(01, value); } }
        public bool State02 { get { return WeaponState[02]; } set { WeaponState[02] = value; AdjustBools(02, value); } }
        #endregion

        #region Methods
        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach (SBJob job in selectedCharacter.SBModel.SBJobList)
            {
                if (job.Anemos.Progress == BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.Anemos.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    SBInfo.ReloadJobList(AvailableJobs, job.Name);
                }
            }
            OnPropertyChanged(nameof(ProteanNeeded));
            OnPropertyChanged(nameof(ProteanCount));
            OnPropertyChanged(nameof(FeatherNeeded));
            OnPropertyChanged(nameof(FeatherCount));
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
            OnPropertyChanged(nameof(State02));
        }
        public void ResetWeaponState()
        {
            anemosModel.WeaponState = new ObservableCollection<bool>
            {
                false, false, false
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

            SBJob tempJob = selectedCharacter.SBModel.SBJobList[SBInfo.JobListString.IndexOf(SelectedJob)];

            SBStageCompleter.ProgressClass(selectedCharacter, SelectedJob, tempJob.Anemos, true);

            LoadAvailableJobs();
            OnPropertyChanged(nameof(ProteanCount));
            OnPropertyChanged(nameof(FeatherCount)); 
        }
        #endregion

        #region Increment Protean
        private ICommand _ProteanButton;

        public ICommand ProteanButton
        {
            get
            {
                if (_ProteanButton == null)
                {
                    _ProteanButton = new RelayCommand(
                        param => this.ProteanCommand(param)
                        );
                }
                return _ProteanButton;
            }
        }

        private void ProteanCommand(object param)
        {
            ProteanCount += 1;
        }
        #endregion

        #region Increment Feather
        private ICommand _FeatherButton;

        public ICommand FeatherButton
        {
            get
            {
                if (_FeatherButton == null)
                {
                    _FeatherButton = new RelayCommand(
                        param => this.FeatherCommand(param)
                        );
                }
                return _FeatherButton;
            }
        }

        private void FeatherCommand(object param)
        {
            FeatherCount += 1;
        }
        #endregion

        #endregion


    }
}
