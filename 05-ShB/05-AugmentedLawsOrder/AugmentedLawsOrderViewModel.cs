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

namespace FFXIVRelicTracker._05_ShB._05_AugmentedLawsOrder
{
    public class AugmentedLawsOrderViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private AugmentedLawsOrderModel augmentedLawsOrderModel;
        #endregion

        #region Constructor
        public AugmentedLawsOrderViewModel()
        {

        }
        public AugmentedLawsOrderViewModel(IEventAggregator eventAggregator)
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
        public string Name => "Augmented Law's Order";
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    AugmentedLawsOrderModel = SelectedCharacter.ShBModel.AugmentedLawsOrderModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }

        public AugmentedLawsOrderModel AugmentedLawsOrderModel
        {
            get { return augmentedLawsOrderModel; }
            set
            {
                augmentedLawsOrderModel = value;
                OnPropertyChanged(nameof(AugmentedLawsOrderModel));
            }
        }

        public string SelectedJob
        {
            get { return augmentedLawsOrderModel.SelectedJob; }
            set
            {
                augmentedLawsOrderModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
            }
        }

        public ObservableCollection<string> AvailableJobs
        {
            get { return augmentedLawsOrderModel.AvailableJobs; }
            set
            {
                augmentedLawsOrderModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }
        public int MemoryCount 
        { 
            get 
            { 
                if(augmentedLawsOrderModel.MemoryCount < 0) { MemoryCount = 0; }
                return augmentedLawsOrderModel.MemoryCount; 
            } 
            set 
            {
                if (value < 0) { augmentedLawsOrderModel.MemoryCount = 0; }
                else { augmentedLawsOrderModel.MemoryCount = value; }
                OnPropertyChanged(nameof(MemoryCount));
                OnPropertyChanged(nameof(MemoryNeeded));
            } 
        }

        public int MemoryNeeded 
        { 
            get 
            { 
                if (AvailableJobs == null) { LoadAvailableJobs(); } 
                return (AvailableJobs.Count * 15) - augmentedLawsOrderModel.MemoryCount;
            } 
        }

        #endregion

        #region Methods
        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach (ShBJob job in selectedCharacter.ShBModel.ShbJobList)
            {
                if (job.AugmentedLawsOrder.Progress == BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.AugmentedLawsOrder.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    ShBInfo.ReloadJobList(AvailableJobs, job.Name);
                }
            }
            //Calculate remaining memories to acquire
            OnPropertyChanged(nameof(MemoryNeeded));
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

            ShBStageCompleter.ProgressClass(selectedCharacter, tempJob.AugmentedLawsOrder, true);

            LoadAvailableJobs();

            OnPropertyChanged(nameof(MemoryCount));
        }
        #endregion

        #region Increment Memory
        private ICommand _MemoryButton;

        public ICommand MemoryButton
        {
            get
            {
                if (_MemoryButton == null)
                {
                    _MemoryButton = new RelayCommand(
                        param => this.MemoryCommand(param)
                        );
                }
                return _MemoryButton;
            }
        }

        private void MemoryCommand(object param)
        {
            MemoryCount += 1;
        }
        #endregion
        #endregion


    }
}
