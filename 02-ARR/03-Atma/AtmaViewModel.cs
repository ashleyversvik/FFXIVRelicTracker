using FFXIVRelicTracker._02_ARR.ArrHelpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FFXIVRelicTracker._02_ARR._03_Atma
{
    class AtmaViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private ArrWeapon arrWeapon;
        private AtmaModel atmaModel;
        private ICommand _CompleteButton;
        #endregion

        public AtmaViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            SubscriptionToken subscriptionToken =this.eventAggregator.GetEvent<PubSubEvent<Character>>().Subscribe((details) =>{this.SelectedCharacter = details;});
            eventAggregator.GetEvent<PubSubEvent<ArrWeapon>>().Subscribe((details) => { this.ArrWeapon = details; });

        }

        #region Properties
        public string Name => "Atma";
        public ArrWeapon ArrWeapon
        {
            get { return arrWeapon; }
            set
            {
                if (value != null)
                {
                    arrWeapon = value;
                    CountAtma();
                    OnPropertyChanged(nameof(ArrWeapon));
                }
            }
        }
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    AtmaModel = SelectedCharacter.ArrProgress.AtmaModel;
                    ArrWeapon = SelectedCharacter.ArrProgress.ArrWeapon;
                }
            }
        }
        public AtmaModel AtmaModel
        {
            get { return atmaModel; }
            set
            {
                atmaModel = value;
                OnPropertyChanged(nameof(AtmaModel));
            }
        }
        public int NeededAtmas
        {
            get { return AtmaModel.NeededAtmas; }
            set
            {
                atmaModel.NeededAtmas = value;
                OnPropertyChanged(nameof(NeededAtmas));
            }
        }


        public ObservableCollection<string> AvailableJobs
        {
            get { return atmaModel.AvailableJobs; }
            set
            {
                atmaModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }

        public string SelectedJob
        {
            get { return atmaModel.SelectedJob; }
            set
            {
                atmaModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
            }
        }

        #endregion
        public void LoadAvailableJobs()
        {
            CountAtma();
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach (ArrJobs job in ArrWeapon.JobList)
            {
                if (job.Atma.Progress != ArrProgress.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Add(job.Name);
                }
                if (job.Atma.Progress == ArrProgress.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
            }
        }

        private void CountAtma()
        {
            int atmaCount = 0;
            foreach (ArrJobs job in arrWeapon.JobList)
            {
                if (job.Atma.Progress != ArrProgress.States.Completed)
                {
                    atmaCount++;
                }
            }
            NeededAtmas = atmaCount;
        }

        #region Complete Command

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

            ArrJobs tempJob = ArrWeapon.JobList[ArrInfo.JobListString.IndexOf(SelectedJob)];
            ArrStageCompleter.ProgressClass(selectedCharacter, tempJob.Atma, true);
            LoadAvailableJobs();
        }
        #endregion
    }
}
