﻿using FFXIVRelicTracker._05_ShB.ShBHelpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FFXIVRelicTracker._05_ShB._05_AugmentedLawsOrder
{
    class AugmentedLawsOrderViewModel : ObservableObject, IPageViewModel
    {
        public string Name => "Augmented Law's Order";
        public string TagName => "AugmentedLawsOrder";

        #region Fields
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private AugmentedLawsOrderModel augmentedLawsOrderModel;
        private ObservableCollection<string> availableJobs;
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
            get { return availableJobs; }
            set
            {
                availableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }
        public int ArtifactCount
        {
            get
            {
                if (augmentedLawsOrderModel.ArtifactCount < 0) { ArtifactCount = 0; }
                return augmentedLawsOrderModel.ArtifactCount;
            }
            set
            {
                if (value < 0) { augmentedLawsOrderModel.ArtifactCount = 0; }
                else { augmentedLawsOrderModel.ArtifactCount = value; }
                OnPropertyChanged(nameof(ArtifactCount));
                OnPropertyChanged(nameof(ArtifactNeeded));
            }
        } 
        public int YellowCount
        {
            get
            {
                if (augmentedLawsOrderModel.YellowMemoryCount < 0) { YellowCount = 0; }
                return augmentedLawsOrderModel.YellowMemoryCount;
            }
            set
            {
                if (value < 0) { augmentedLawsOrderModel.YellowMemoryCount = 0; }
                else if (value > 17) { augmentedLawsOrderModel.YellowMemoryCount = 18; }
                else { augmentedLawsOrderModel.YellowMemoryCount = value; }
                OnPropertyChanged(nameof(YellowCount));
            }
        }
        public int PurpleCount
        {
            get
            {
                if (augmentedLawsOrderModel.PurpleMemoryCount < 0) { PurpleCount = 0; }
                return augmentedLawsOrderModel.PurpleMemoryCount;
            }
            set
            {
                if (value < 0) { augmentedLawsOrderModel.PurpleMemoryCount = 0; }
                else if (value > 17) { augmentedLawsOrderModel.PurpleMemoryCount = 18; }
                else { augmentedLawsOrderModel.PurpleMemoryCount = value; }
                OnPropertyChanged(nameof(PurpleCount));
            }
        }

        public int ArtifactNeeded
        {
            get
            {
                if (AvailableJobs == null) { LoadAvailableJobs(); }
                return (AvailableJobs.Count * 15) - augmentedLawsOrderModel.ArtifactCount;
            }
        }

        public bool CompletedOnce
        {
            get
            {
                if (AvailableJobs == null)
                    return false;
                else if (AvailableJobs.Count == ShBHelpers.ShBInfo.JobListString.Count)
                    return false;
                else return true;
            }
        }
                

        #endregion

        #region Methods
        public void LoadAvailableJobs()
        {
            AvailableJobs = ShBInfo.LoadJobs(AvailableJobs, SelectedCharacter, TagName);
            //Calculate remaining memories to acquire
            OnPropertyChanged(nameof(ArtifactCount));
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
            ShBStageCompleter.ProgressClass(selectedCharacter, SelectedJob, TagName);

            LoadAvailableJobs();

            OnPropertyChanged(nameof(ArtifactCount));
            OnPropertyChanged(nameof(ArtifactNeeded));
            OnPropertyChanged(nameof(CompletedOnce));
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
            string itemType = (string)param;

            switch (itemType)
            {
                case "Purple":
                    PurpleCount += 1;
                    break;
                case "Yellow":
                    YellowCount += 1;
                    break;
                default:
                    ArtifactCount += 1;
                    break;
                    
                        
            }
        }
        #endregion
        #endregion
    }
}
