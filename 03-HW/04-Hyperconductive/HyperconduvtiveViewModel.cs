using FFXIVRelicTracker._03_HW.HWHelpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FFXIVRelicTracker._03_HW._04_Hyperconductive
{
    public class HyperconduvtiveViewModel : ObservableObject, IPageViewModel
    {
        public string Name => "Hyperconductive";

        #region Fields
        private HyperconductiveModel hyperconductiveModel;
        private Character selectedCharacter;
        private IEventAggregator eventAggregator;
        private ObservableCollection<string> availableJobs;
        #endregion

        #region Constructor
        public HyperconduvtiveViewModel()
        {
        }
        public HyperconduvtiveViewModel(IEventAggregator eventAggregator)
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
                    HyperconductiveModel = selectedCharacter.HWModel.HyperconductiveModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }

            }
        }

        public string SelectedJob
        {
            get { return HyperconductiveModel.SelectedJob; }
            set
            {
                HyperconductiveModel.SelectedJob = value;
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

        public HyperconductiveModel HyperconductiveModel
        {
            get { return hyperconductiveModel; }
            set
            {
                if (value != null)
                {
                    hyperconductiveModel = value;
                    OnPropertyChanged(nameof(HyperconductiveModel));
                }
            }
        }
        
        public int OilCount 
        { 
            get 
            {
                if (hyperconductiveModel.OilCount < 0) { OilCount = 0; }
                return hyperconductiveModel.OilCount; 
            } 
            set 
            {
                if (value < 0) { hyperconductiveModel.OilCount = 0; }
                else { hyperconductiveModel.OilCount = value; }
                OilChange(); 
            } 
        }
        public int NeededOil { get { if (AvailableJobs == null) { LoadAvailableJobs(); }  return Math.Max(0,AvailableJobs.Count * 5 - OilCount); } }
        public int Poetics { get { return NeededOil * 350; } }
        public string QuestName { get { if (AvailableJobs != null) { if (AvailableJobs.Count != 13) { return "Finding Your Voice"; } else { return "Soul Without Life"; } } else { return "Finding Your Voice"; } } }
        #endregion

        #region Methods

        private void OilChange()
        {
            OnPropertyChanged(nameof(OilCount));
            OnPropertyChanged(nameof(NeededOil));
            OnPropertyChanged(nameof(Poetics));
        }

        public void LoadAvailableJobs()
        {
            AvailableJobs = HWInfo.LoadJobs(AvailableJobs, SelectedCharacter, Name);
            OnPropertyChanged(nameof(AvailableJobs));
            OilChange();
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
            HWStageCompleter.ProgressClass(SelectedCharacter, SelectedJob, Name);
            LoadAvailableJobs();
        }
        #endregion

        #region Add Oil

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
            OilCount += 1;
        }

        #endregion
        #endregion


    }
}
