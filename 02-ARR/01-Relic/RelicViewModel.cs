﻿using FFXIVRelicTracker._02_ARR.ArrHelpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows;
using System.Windows.Input;

namespace FFXIVRelicTracker._02_ARR._01_Relic
{
    class RelicViewModel : ObservableObject, IPageViewModel
    {
        public string Name => "Relic";
        private Character selectedCharacter;
        private RelicModel relicModel;
        private IEventAggregator eventAggregator;
        private ObservableCollection<string> availableJobs;

        public RelicViewModel(IEventAggregator eventAggregator)
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

        #region Preoperties
        #region ViewModel Properties
        public RelicModel RelicModel
        {
            get { return relicModel; }
            set
            {
                relicModel = value;
                OnPropertyChanged(nameof(RelicModel));
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
                    RelicModel = selectedCharacter.ArrModel.RelicModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
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
        #endregion

        #region RelicModel Properties

        #region Strikethrough Bools
        public bool Stage1Complete { get { return relicModel.Stage1Complete; } set { relicModel.Stage1Complete = value; OnPropertyChanged(nameof(Stage1Complete)); } }
        public bool Stage2Complete { get { return relicModel.Stage2Complete; } set { relicModel.Stage2Complete = value; OnPropertyChanged(nameof(Stage2Complete)); } }
        public bool Stage3Complete { get { return relicModel.Stage3Complete; } set { relicModel.Stage3Complete = value; OnPropertyChanged(nameof(Stage3Complete)); } }
        public bool Stage4Complete { get { return relicModel.Stage4Complete; } set { relicModel.Stage4Complete = value; OnPropertyChanged(nameof(Stage4Complete)); } }
        public bool Stage5Complete { get { return relicModel.Stage5Complete; } set { relicModel.Stage5Complete = value;
                OnPropertyChanged(nameof(BeastMan1Binding));
                OnPropertyChanged(nameof(BeastMan2Binding));
                OnPropertyChanged(nameof(BeastMan3Binding));
                OnPropertyChanged(nameof(Stage5Complete)); } }
        public bool Stage6Complete { get { return relicModel.Stage6Complete; } set { relicModel.Stage6Complete = value; OnPropertyChanged(nameof(Stage6Complete)); } }
        public bool Stage7Complete { get { return relicModel.Stage7Complete; } set { relicModel.Stage7Complete = value; 
                OnPropertyChanged(nameof(Stage7Complete));
                OnPropertyChanged(nameof(ShivaBinding));
                OnPropertyChanged(nameof(GarudaBinding));
                OnPropertyChanged(nameof(IfritBinding)); } }
        public bool Stage8Complete { get { return relicModel.Stage8Complete; } set { relicModel.Stage8Complete = value; OnPropertyChanged(nameof(Stage8Complete)); } }

        public bool IfritComplete { get { return relicModel.IfritComplete; } set { relicModel.IfritComplete = value; OnPropertyChanged(nameof(IfritComplete)); OnPropertyChanged(nameof(IfritBinding)); } }
        public bool GarudaComplete { get { return relicModel.GarudaComplete; } set { relicModel.GarudaComplete = value; OnPropertyChanged(nameof(GarudaComplete)); OnPropertyChanged(nameof(GarudaBinding)); } }
        public bool ShivaComplete { get { return relicModel.ShivaComplete; } set { relicModel.ShivaComplete = value; OnPropertyChanged(nameof(ShivaComplete)); OnPropertyChanged(nameof(ShivaBinding)); } }

        public bool IfritBinding { get {return relicModel.Stage7Complete | relicModel.IfritComplete; } }
        public bool GarudaBinding { get {return relicModel.Stage7Complete | relicModel.GarudaComplete; } }
        public bool ShivaBinding { get {return relicModel.Stage7Complete | relicModel.ShivaComplete; } }

        public bool BeastMan1Complete { get { return relicModel.BeastMan1Complete; } set { relicModel.BeastMan1Complete = value; OnPropertyChanged(nameof(BeastMan1Complete)); OnPropertyChanged(nameof(BeastMan1Binding)); } }
        public bool BeastMan2Complete { get { return relicModel.BeastMan2Complete; } set { relicModel.BeastMan2Complete = value; OnPropertyChanged(nameof(BeastMan2Complete)); OnPropertyChanged(nameof(BeastMan2Binding)); } }
        public bool BeastMan3Complete { get { return relicModel.BeastMan3Complete; } set { relicModel.BeastMan3Complete = value; OnPropertyChanged(nameof(BeastMan3Complete)); OnPropertyChanged(nameof(BeastMan3Binding)); } }

        public bool BeastMan1Binding { get { return relicModel.Stage5Complete | relicModel.BeastMan1Complete; } }
        public bool BeastMan2Binding { get { return relicModel.Stage5Complete | relicModel.BeastMan2Complete; } }
        public bool BeastMan3Binding { get { return relicModel.Stage5Complete | relicModel.BeastMan3Complete; } }



        #endregion

        public string SelectedJob
        {
            get { return relicModel.SelectedJob; }
            set
            {
                if (relicModel.SelectedJob != value) {ResetBools();}

                relicModel.SelectedJob = value;

                if (value != null)
                {
                    RelicMap = ArrInfo.ReturnRelicMap(SelectedJob);
                    RelicDestination = ArrInfo.ReturnRelicDestination(SelectedJob);
                    RelicPoint = ArrInfo.ReturnRelicPoint(SelectedJob);
                    RelicMateria = ArrInfo.ReturnMateria(SelectedJob);
                    RelicClassWeapon = ArrInfo.ReturnClassWeapon(SelectedJob);

                    List<string> templist = ArrInfo.ReturnBeastmen(SelectedJob);

                    RelicBeastmen1 = templist[0];
                    RelicBeastmen2 = templist[1];
                    RelicBeastmen3 = templist[2];

                    RelicVisibility = Visibility.Visible;
                }
                else
                {
                    RelicVisibility = Visibility.Hidden;
                }

                OnPropertyChanged(nameof(SelectedJob));
                OnPropertyChanged(nameof(CompleteButton));
            }
        }
        public Visibility RelicVisibility
        {
            get { return relicModel.RelicVisibility; }
            set
            {
                relicModel.RelicVisibility = value;
                OnPropertyChanged(nameof(RelicVisibility));
            }
        }
        public string RelicDestination
        {
            get { return relicModel.RelicDestination; }
            set
            {
                relicModel.RelicDestination = value;
                OnPropertyChanged(nameof(RelicDestination));
            }
        }
        public string RelicClassWeapon
        {
            get { return relicModel.RelicClassWeapon; }
            set
            {
                relicModel.RelicClassWeapon = value;
                OnPropertyChanged(nameof(RelicClassWeapon));
            }
        }
        public string RelicBeastmen1
        {
            get { return relicModel.RelicBeastmen1; }
            set
            {
                relicModel.RelicBeastmen1 = value;
                OnPropertyChanged(nameof(RelicBeastmen1));
            }
        }
        public string RelicBeastmen2
        {
            get { return relicModel.RelicBeastmen2; }
            set
            {
                relicModel.RelicBeastmen2 = value;
                OnPropertyChanged(nameof(RelicBeastmen2));
            }
        }
        public string RelicBeastmen3
        {
            get { return relicModel.RelicBeastmen3; }
            set
            {
                relicModel.RelicBeastmen3 = value;
                OnPropertyChanged(nameof(RelicBeastmen3));
            }
        }
        public string RelicMap
        {
            get { return relicModel.RelicMap; }
            set
            {
                relicModel.RelicMap = value;
                OnPropertyChanged(nameof(RelicMap));
            }
        }
        public PointF RelicPoint
        {
            get { return relicModel.RelicPoint; }
            set
            {
                relicModel.RelicPoint = value;
                OnPropertyChanged(nameof(RelicPoint));
            }
        }
        public string RelicMateria
        {
            get { return relicModel.RelicMateria; }
            set
            {
                relicModel.RelicMateria = value;
                OnPropertyChanged(nameof(RelicMateria));
            }
        }
        #endregion
        #endregion

        #region Methods

        public void ResetBools()
        {
            Stage1Complete =false;
            Stage2Complete =false;
            Stage3Complete =false;
            Stage4Complete =false;
            Stage5Complete =false;
            Stage6Complete =false;
            Stage7Complete =false;
            Stage8Complete = false;

            IfritComplete = false;
            GarudaComplete = false;
            ShivaComplete = false;

            BeastMan1Complete = false;
            BeastMan2Complete = false;
            BeastMan3Complete = false;
                        
        }
        public void LoadAvailableJobs()
        {
            AvailableJobs = ArrInfo.LoadJobs(AvailableJobs, SelectedCharacter, Name);
        }
        #endregion

        #region CompleteRelic Command
        private ICommand _RelicButton;

        public ICommand CompleteButton
        {
            get
            {
                if (_RelicButton == null)
                {
                    _RelicButton = new RelayCommand(
                        param => this.RelicCommand(),
                        param => this.RelicCan()
                        );
                }
                return _RelicButton;
            }
        }

        private bool RelicCan() { return SelectedJob!=null; }
        private void RelicCommand()
        {
            ArrStageCompleter.ProgressClass(selectedCharacter, SelectedJob, Name);
            AvailableJobs.Remove(SelectedJob);
        }
        #endregion
    }
}
