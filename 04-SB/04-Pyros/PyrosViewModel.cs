using FFXIVRelicTracker._04_SB.SBHelpers;
using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FFXIVRelicTracker._04_SB._04_Pyros
{

    public class PyrosViewModel : ObservableObject, IPageViewModel
    {
        public string Name => "Pyros";

        #region Fields
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private PyrosModel pyrosModel;
        #endregion

        #region Constructor
        public PyrosViewModel()
        {

        }

        public PyrosViewModel(IEventAggregator eventAggregator)
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
                    PyrosModel = SelectedCharacter.SBModel.PyrosModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }

        public PyrosModel PyrosModel
        {
            get { return pyrosModel; }
            set
            {
                pyrosModel = value;
                OnPropertyChanged(nameof(PyrosModel));
            }
        }

        public string SelectedJob
        {
            get { return pyrosModel.SelectedJob; }
            set
            {
                if (value != null && value != pyrosModel.SelectedJob) ResetWeaponState();
                pyrosModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
            }
        }

        public ObservableCollection<string> AvailableJobs
        {
            get { return pyrosModel.AvailableJobs; }
            set
            {
                pyrosModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }
        public int PyrosNeeded
        {
            get
            {
                if (AvailableJobs != null)
                {
                    if (pyrosModel.BuyFlames) { return AvailableJobs.Count * 900; }
                    else { return AvailableJobs.Count * 650; }
                }
                else { return 0; };
            }
        }
        public int PyrosRemaining
        {
            get
            {
                if (AvailableJobs == null) { LoadAvailableJobs(); }
                return PyrosNeeded - PyrosCount - (FlamesCount*50);
            }
        }
        public int PyrosCount
        {
            get
            {
                if (pyrosModel.PyrosCount < 0) { PyrosCount = 0; }
                return pyrosModel.PyrosCount;
            }
            set
            {
                if (value < 0) { pyrosModel.PyrosCount = 0; }
                else { pyrosModel.PyrosCount = value; }
                OnPropertyChanged(nameof(PyrosCount));
                OnPropertyChanged(nameof(PyrosRemaining));
            }
        }

        public int LogosRemaining
        {
            get
            {
                if (AvailableJobs == null) { LoadAvailableJobs(); }
                return 30 - LogosCount;
            }
        }
        public int LogosCount
        {
            get
            {
                if (pyrosModel.LogosCount < 0) { LogosCount = 0; }
                return pyrosModel.LogosCount;
            }
            set
            {
                if (value < 0) { pyrosModel.LogosCount = 0; }
                else { pyrosModel.LogosCount = value; }
                OnPropertyChanged(nameof(LogosCount));
                OnPropertyChanged(nameof(LogosRemaining));
            }
        }

        public int FlamesNeeded { get { if (AvailableJobs != null) { return AvailableJobs.Count * 5; } else { return 0; } } }
        public int FlamesRemaining
        {
            get
            {
                if (AvailableJobs == null) { LoadAvailableJobs(); }
                return FlamesNeeded - FlamesCount;
            }
        }
        public int FlamesCount
        {
            get
            {
                if (pyrosModel.FlamesCount < 0) { FlamesCount = 0; }
                return pyrosModel.FlamesCount;
            }
            set
            {
                if (value < 0) { pyrosModel.FlamesCount = 0; }
                else { pyrosModel.FlamesCount = value; }
                OnPropertyChanged(nameof(FlamesCount));
                OnPropertyChanged(nameof(FlamesRemaining));
                OnPropertyChanged(nameof(PyrosRemaining));
            }
        }
        public bool BuyFlames
        {
            get
            {
                return pyrosModel.BuyFlames;
            }
            set
            {
                pyrosModel.BuyFlames = value;
                OnPropertyChanged(nameof(BuyFlames));
                OnPropertyChanged(nameof(PyrosNeeded));
                OnPropertyChanged(nameof(PyrosRemaining));
            }
        }
        private ObservableCollection<bool> WeaponState
        {
            get
            {
                if (pyrosModel.WeaponState == null)
                {
                    pyrosModel.WeaponState = new ObservableCollection<bool>()
                    {
                        false,
                        false
                    };
                }
                return pyrosModel.WeaponState;
            }
            set
            {
                pyrosModel.WeaponState = value;
                RecheckBools();
            }
        }

        public bool State00 { get { return WeaponState[00]; } set { WeaponState[00] = value; AdjustBools(00, value); } }
        public bool State01 { get { return WeaponState[01]; } set { WeaponState[01] = value; AdjustBools(01, value); } }
        #endregion

        #region Methods
        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach (SBJob job in selectedCharacter.SBModel.SBJobList)
            {
                if (job.Pyros.Progress == BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.Pyros.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    SBInfo.ReloadJobList(AvailableJobs, job.Name);
                }
            }
            OnPropertyChanged(nameof(PyrosNeeded));
            OnPropertyChanged(nameof(PyrosCount));
            OnPropertyChanged(nameof(FlamesNeeded));
            OnPropertyChanged(nameof(FlamesCount));
            OnPropertyChanged(nameof(LogosCount));
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
            pyrosModel.WeaponState = new ObservableCollection<bool>
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

            SBJob tempJob = selectedCharacter.SBModel.SBJobList[SBInfo.JobListString.IndexOf(SelectedJob)];

            SBStageCompleter.ProgressClass(selectedCharacter, SelectedJob, tempJob.Pyros, true);

            LoadAvailableJobs();
            OnPropertyChanged(nameof(PyrosCount));
            OnPropertyChanged(nameof(FlamesCount));
        }
        #endregion

        #region Increment Pyros
        private ICommand _PyrosButton;

        public ICommand PyrosButton
        {
            get
            {
                if (_PyrosButton == null)
                {
                    _PyrosButton = new RelayCommand(
                        param => this.PyrosCommand(param)
                        );
                }
                return _PyrosButton;
            }
        }

        private void PyrosCommand(object param)
        {
            PyrosCount += 1;
        }
        #endregion

        #region Increment Flames
        private ICommand _FlamesButton;

        public ICommand FlamesButton
        {
            get
            {
                if (_FlamesButton == null)
                {
                    _FlamesButton = new RelayCommand(
                        param => this.FlamesCommand(param)
                        );
                }
                return _FlamesButton;
            }
        }

        private void FlamesCommand(object param)
        {
            FlamesCount += 1;
        }
        #endregion

        #region Increment Logos
        private ICommand _LogosButton;

        public ICommand LogosButton
        {
            get
            {
                if (_LogosButton == null)
                {
                    _LogosButton = new RelayCommand(
                        param => this.LogosCommand(param)
                        );
                }
                return _LogosButton;
            }
        }

        private void LogosCommand(object param)
        {
            LogosCount+=1;
        }
        #endregion
        #endregion


    }
}
