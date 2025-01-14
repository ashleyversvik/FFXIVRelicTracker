﻿using FFXIVRelicTracker._05_ShB.ShBHelpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FFXIVRelicTracker._05_ShB._06_Blades
{
    class BladesViewModel : ObservableObject, IPageViewModel
    {
        public string Name => "Blades of Gunnhildr";
        public string TagName => "Blades";

        #region Fields
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private BladesModel bladesModel;
        private ObservableCollection<string> availableJobs;
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
            get { return availableJobs; }
            set
            {
                availableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }
        public int Compact1
        {
            get
            {
                if (bladesModel.Compact1 < 0) { Compact1 = 0; }
                return bladesModel.Compact1;
            }
            set
            {
                if (value < 0) { bladesModel.Compact1 = 0; }
                else if (value > 29) { bladesModel.Compact1 = 30; }
                else { bladesModel.Compact1 = value; }
                OnPropertyChanged(nameof(Compact1));
            }
        }
        public int Compact2
        {
            get
            {
                if (bladesModel.Compact2 < 0) { Compact2 = 0; }
                return bladesModel.Compact2;
            }
            set
            {
                if (value < 0) { bladesModel.Compact2 = 0; }
                else if (value > 29) { bladesModel.Compact2 = 30; }
                else { bladesModel.Compact2 = value; }
                OnPropertyChanged(nameof(Compact2));
            }
        }
        
        public int Book1
        {
            get
            {
                if (bladesModel.Book1 < 0) { Book1 = 0; }
                return bladesModel.Book1;
            }
            set
            {
                if (value < 0) { bladesModel.Book1 = 0; }
                else if (value > 29) { bladesModel.Book1 = 30; }
                else { bladesModel.Book1 = value; }
                OnPropertyChanged(nameof(Book1));
            }
        }
        public int Book2
        {
            get
            {
                if (bladesModel.Book2 < 0) { Book2 = 0; }
                return bladesModel.Book2;
            }
            set
            {
                if (value < 0) { bladesModel.Book2 = 0; }
                else if (value > 29) { bladesModel.Book2 = 30; }
                else { bladesModel.Book2 = value; }
                OnPropertyChanged(nameof(Book2));
            }
        }
        
        public int Memory1
        {
            get
            {
                if (bladesModel.Memory1 < 0) { Memory1 = 0; }
                return bladesModel.Memory1;
            }
            set
            {
                if (value < 0) { bladesModel.Memory1 = 0; }
                else if (value > 29) { bladesModel.Memory1 = 30; }
                else { bladesModel.Memory1 = value; }
                OnPropertyChanged(nameof(Memory1));
            }
        }
        public int Memory2
        {
            get
            {
                if (bladesModel.Memory2 < 0) { Memory2 = 0; }
                return bladesModel.Memory2;
            }
            set
            {
                if (value < 0) { bladesModel.Memory2 = 0; }
                else if (value > 29) { bladesModel.Memory2 = 30; }
                else { bladesModel.Memory2 = value; }
                OnPropertyChanged(nameof(Memory2));
            }
        }
        
        public int EmotionCount
        {
            get
            {
                if (bladesModel.EmotionCount < 0) { EmotionCount = 0; }
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
            OnPropertyChanged(nameof(EmotionCount));
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
            ShBStageCompleter.ProgressClass(selectedCharacter, SelectedJob, TagName);

            LoadAvailableJobs();

            OnPropertyChanged(nameof(EmotionCount));
            OnPropertyChanged(nameof(EmotionNeeded));
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
                case "Compact1":
                    Compact1 += 1;
                    break;
                case "Compact2":
                    Compact2 += 1;
                    break;
                case "Book1":
                    Book1 += 1;
                    break;
                case "Book2":
                    Book2 += 1;
                    break;
                case "Memory1":
                    Memory1 += 1;
                    break;
                case "Memory2":
                    Memory2 += 1;
                    break;
                default:
                    EmotionCount += 1;
                    break;
            }
        }
        #endregion
        #endregion


    }
}
