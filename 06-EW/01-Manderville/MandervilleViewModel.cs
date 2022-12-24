using FFXIVRelicTracker._06_EW.EWHelpers;
using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FFXIVRelicTracker._06_EW._01_Manderville
{
    public class MandervilleViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        public string Name => "Manderville";
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private MandervilleModel mandervilleModel;
        #endregion

        #region Constructor
        public MandervilleViewModel()
        {

        }
        public MandervilleViewModel(IEventAggregator eventAggregator)
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
                    MandervilleModel = SelectedCharacter.EWModel.MandervilleModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }

        public MandervilleModel MandervilleModel
        {
            get { return mandervilleModel; }
            set
            {
                mandervilleModel = value;
                OnPropertyChanged(nameof(MandervilleModel));
            }
        }

        public string SelectedJob
        {
            get { return mandervilleModel.SelectedJob; }
            set
            {
                mandervilleModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
            }
        }
        public int MeteoritesCount
        {
            get 
            {
                if (mandervilleModel.MeteoritesCount < 0) { MeteoritesCount = 0; }
                return mandervilleModel.MeteoritesCount;
            }
            set
            {
                if(value>=0 & value < 60)
                {
                    mandervilleModel.MeteoritesCount = value;
                    OnPropertyChanged(nameof(MeteoritesCount));
                    OnPropertyChanged(nameof(MeteoritesCost));
                    OnPropertyChanged(nameof(NeededMeteorites));
                }
            }
        }
        public int NeededMeteorites { get { if (AvailableJobs == null) { LoadAvailableJobs(); } return (AvailableJobs.Count * 3) - MeteoritesCount; } }
        public int MeteoritesCost => NeededMeteorites * 500;

        public ObservableCollection<string> AvailableJobs
        {
            get { return mandervilleModel.AvailableJobs; }
            set
            {
                mandervilleModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }
        public bool CompletedFirstManderville { get { return AvailableJobs.Count < EWInfo.JobListString.Count; } }
        #endregion


        #region Methods
        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach( EWJob job in selectedCharacter.EWModel.EWJobList)
            {
                if(job.Manderville.Progress==BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.Manderville.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    EWInfo.ReloadJobList(AvailableJobs, job.Name);                 
                }
            }
            OnPropertyChanged(nameof(CompletedFirstManderville));
            OnPropertyChanged(nameof(NeededMeteorites));
            OnPropertyChanged(nameof(MeteoritesCost));
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

            EWJob tempJob = selectedCharacter.EWModel.EWJobList[EWInfo.JobListString.IndexOf(SelectedJob)];

            EWStageCompleter.ProgressClass(selectedCharacter, SelectedJob, tempJob.Manderville, true);

            LoadAvailableJobs();
            OnPropertyChanged(nameof(MeteoritesCount));
            OnPropertyChanged(nameof(NeededMeteorites));
        }
        #endregion

        #region Increment Meteorites
        private ICommand _MeteoritesButton;

        public ICommand MeteoritesButton
        {
            get
            {
                if (_MeteoritesButton == null)
                {
                    _MeteoritesButton = new RelayCommand(
                        param => this.MeteoritesCommand(param)
                        );
                }
                return _MeteoritesButton;
            }
        }

        private void MeteoritesCommand(object param)
        {
            MeteoritesCount += 1;
        }
        #endregion
        #endregion
    }
}
