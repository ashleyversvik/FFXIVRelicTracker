using FFXIVRelicTracker._05_ShB.ShBHelpers;
using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace FFXIVRelicTracker._05_ShB._06_Blades
{
    public class BladesViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private BladesModel bladesModel;
        #endregion

        #region Constructor
        public BladesViewModel()
        {

        }
        public BladesViewModel(IEventAggregator eventAggregator)
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
        public string Name => "Blade's";
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    BladesModel = SelectedCharacter.ShBModel.BladesModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }

        public BladesModel BladesModel
        {
            get { return bladesModel; }
            set
            {
                bladesModel = value;
                OnPropertyChanged(nameof(BladesModel));
            }
        }

        public string SelectedJob
        {
            get { return bladesModel.SelectedJob; }
            set
            {
                bladesModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
            }
        }

        public ObservableCollection<string> AvailableJobs
        {
            get { return bladesModel.AvailableJobs; }
            set
            {
                bladesModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }
        public int EmotionCount 
        { 
            get 
            { 
                if(bladesModel.EmotionCount < 0) { EmotionCount = 0; }
                return bladesModel.EmotionCount; 
            } 
            set 
            {
                if (value < 0) { bladesModel.EmotionCount = 0; }
                else { bladesModel.EmotionCount = value; }
                OnPropertyChanged(nameof(EmotionCount));
                OnPropertyChanged(nameof(EmotionNeeded));
            } 
        }

        public int EmotionNeeded 
        { 
            get 
            { 
                if (AvailableJobs == null) { LoadAvailableJobs(); } 
                return (AvailableJobs.Count * 15) - bladesModel.EmotionCount;
            } 
        }

        #endregion

        #region Methods
        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach (ShBJob job in selectedCharacter.ShBModel.ShbJobList)
            {
                if (job.Blades.Progress == BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.Blades.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    ShBInfo.ReloadJobList(AvailableJobs, job.Name);
                }
            }
            //Calculate remaining memories to acquire
            OnPropertyChanged(nameof(EmotionNeeded));
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

            ShBJob tempJob = selectedCharacter.ShBModel.ShbJobList[ShBInfo.JobListString.IndexOf(SelectedJob)];

            ShBStageCompleter.ProgressClass(selectedCharacter, tempJob.Blades, true);

            LoadAvailableJobs();

            OnPropertyChanged(nameof(EmotionCount));
        }
        #endregion

        #region Increment Emotion
        private ICommand _EmotionButton;

        public ICommand EmotionButton
        {
            get
            {
                if (_EmotionButton == null)
                {
                    _EmotionButton = new RelayCommand(
                        param => this.EmotionCommand(param)
                        );
                }
                return _EmotionButton;
            }
        }

        private void EmotionCommand(object param)
        {
            EmotionCount += 1;
        }
        #endregion
        #endregion


    }
}
