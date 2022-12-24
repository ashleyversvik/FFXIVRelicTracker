﻿using FFXIVRelicTracker._03_HW.HWHelpers;
using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FFXIVRelicTracker._03_HW._03_Anima
{
    public class AnimaViewModel : ObservableObject, IPageViewModel
    {
        public string Name => "Anima";
        #region Fields
        private AnimaModel animaModel;
        private Character selectedCharacter;
        private IEventAggregator eventAggregator;
        #endregion

        #region Constructor
        public AnimaViewModel()
        {
        }
        public AnimaViewModel(IEventAggregator eventAggregator)
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
                    AnimaModel = selectedCharacter.HWModel.AnimaModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }

            }
        }

        public string SelectedJob
        {
            get { return AnimaModel.SelectedJob; }
            set
            {
                AnimaModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
            }
        }

        public ObservableCollection<string> AvailableJobs
        {
            get { return AnimaModel.AvailableJobs; }
            set
            {
                AnimaModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }

        public AnimaModel AnimaModel
        {
            get { return animaModel; }
            set
            {
                if (value != null)
                {
                    animaModel = value;
                    OnPropertyChanged(nameof(AnimaModel));
                }
            }
        }

        #region Token 1
        public int UnidentifiableBone { get { if (animaModel.UnidentifiableBone < 0) { UnidentifiableBone = 0; } return animaModel.UnidentifiableBone; } set { if (value >= 0) { animaModel.UnidentifiableBone = value; RecalcBones(); }  }}
        public int UnidentifiableShell { get { if (animaModel.UnidentifiableShell < 0) { UnidentifiableShell = 0; } return animaModel.UnidentifiableShell; } set { if (value >= 0) { animaModel.UnidentifiableShell = value; } RecalcShells(); }}
        public int UnidentifiableOre { get { if (animaModel.UnidentifiableOre < 0) { UnidentifiableOre = 0; } return animaModel.UnidentifiableOre; } set { if (value >= 0) { animaModel.UnidentifiableOre = value; } RecalcOres(); }}
        public int UnidentifiableSeeds { get { if (animaModel.UnidentifiableSeeds < 0) { UnidentifiableSeeds = 0; } return animaModel.UnidentifiableSeeds; } set { if (value >= 0) { animaModel.UnidentifiableSeeds = value; } RecalcSeeds(); }}
        
        public int NeededBone { get { if (AvailableJobs == null) { LoadAvailableJobs(); } { return Math.Max(0,AvailableJobs.Count * 10 - UnidentifiableBone); } } }
        public int NeededShell { get { if (AvailableJobs == null) { LoadAvailableJobs(); } { return Math.Max(0,AvailableJobs.Count * 10 - UnidentifiableShell); } } }
        public int NeededOre { get { if (AvailableJobs == null) { LoadAvailableJobs(); } { return Math.Max(0,AvailableJobs.Count * 10 - UnidentifiableOre); } } }
        public int NeededSeeds { get { if (AvailableJobs == null) { LoadAvailableJobs(); } { return Math.Max(0,AvailableJobs.Count * 10 - UnidentifiableSeeds); } } }

        public int BonePoetics { get { return NeededBone * 150; } }
        public int BoneTokens { get { return NeededBone * 6; } }

        public int ShellPoetics { get { return NeededShell * 150; } }
        public int ShellTokens { get { return NeededShell * 6; } }

        public int OrePoetics { get { return NeededOre * 150; } }
        public int OreTokens { get { return NeededOre * 6; } }

        public int SeedsPoetics { get { return NeededSeeds * 150; } }
        public int SeedsTokens { get { return NeededSeeds * 6; } }
        #endregion

        #region Token 2
        public int Francesca { get { if (animaModel.Francesca < 0) { Francesca = 0; } return animaModel.Francesca; } set { if (value >= 0) { animaModel.Francesca = value; } RecalcFrancesca(); } }
        public int Mirror { get { if (animaModel.Mirror < 0) { Mirror = 0; } return animaModel.Mirror; } set { if (value >= 0) { animaModel.Mirror = value; } RecalcMirror(); } }
        public int Arrow { get { if (animaModel.Arrow < 0) { Arrow = 0; } return animaModel.Arrow; } set { if (value >= 0) { animaModel.Arrow = value; } RecalcArrow(); } }
        public int Kingcake { get { if (animaModel.Kingcake < 0) { Kingcake = 0; } return animaModel.Kingcake; } set { if (value >= 0) { animaModel.Kingcake = value; } RecalcKingcake(); } }

        public int NeededFrancesca { get { if (AvailableJobs == null) { LoadAvailableJobs(); } { return Math.Max(0, AvailableJobs.Count * 4 - Francesca); } } }
        public int NeededMirror { get { if (AvailableJobs == null) { LoadAvailableJobs(); } { return Math.Max(0, AvailableJobs.Count * 4 - Mirror); } } }
        public int NeededArrow { get { if (AvailableJobs == null) { LoadAvailableJobs(); } { return Math.Max(0, AvailableJobs.Count * 4 - Arrow); } } }
        public int NeededKingcake { get { if (AvailableJobs == null) { LoadAvailableJobs(); } { return Math.Max(0, AvailableJobs.Count * 4 - Kingcake); } } }

        public int FrancescaSeals { get { return NeededFrancesca * 5000; } }
        public int MirrorSeals { get { return NeededMirror * 5000; } }
        public int ArrowSeals { get { return NeededArrow * 5000; } }
        public int KingcakeSeals { get { return NeededKingcake * 5000; } }
        #endregion

        #region Totals
        public int Poetics { get { return BonePoetics + ShellPoetics + OrePoetics + SeedsPoetics; } }
        public int Tokens { get { return BoneTokens + ShellTokens + OreTokens + SeedsTokens; } }
        public int Seals { get { return FrancescaSeals + MirrorSeals + ArrowSeals + KingcakeSeals; } }

        #endregion

        #endregion

        #region Methods
        #region Recalculate Items
        private void RecalcTotals()
        {
            OnPropertyChanged(nameof(Poetics));
            OnPropertyChanged(nameof(Tokens));
            OnPropertyChanged(nameof(Seals));
        }
        private void RecalcBones()
        {
            OnPropertyChanged(nameof(UnidentifiableBone));
            OnPropertyChanged(nameof(NeededBone));
            OnPropertyChanged(nameof(BonePoetics));
            OnPropertyChanged(nameof(BoneTokens));

            RecalcTotals();
        }
        private void RecalcShells()
        {
            OnPropertyChanged(nameof(UnidentifiableShell));
            OnPropertyChanged(nameof(NeededShell));
            OnPropertyChanged(nameof(ShellPoetics));
            OnPropertyChanged(nameof(ShellTokens));
            RecalcTotals();
        }
        private void RecalcOres()
        {
            OnPropertyChanged(nameof(UnidentifiableOre));
            OnPropertyChanged(nameof(NeededOre));
            OnPropertyChanged(nameof(OrePoetics));
            OnPropertyChanged(nameof(OreTokens));
            RecalcTotals();
        }
        private void RecalcSeeds()
        {
            OnPropertyChanged(nameof(UnidentifiableSeeds));
            OnPropertyChanged(nameof(NeededSeeds));
            OnPropertyChanged(nameof(SeedsPoetics));
            OnPropertyChanged(nameof(SeedsTokens));
            RecalcTotals();
        }
        private void RecalcFrancesca()
        {
            OnPropertyChanged(nameof(Francesca));
            OnPropertyChanged(nameof(NeededFrancesca));
            OnPropertyChanged(nameof(FrancescaSeals));
            RecalcTotals();
        }
        private void RecalcMirror()
        {
            OnPropertyChanged(nameof(Mirror));
            OnPropertyChanged(nameof(NeededMirror));
            OnPropertyChanged(nameof(MirrorSeals));
            RecalcTotals();
        }
        private void RecalcArrow()
        {
            OnPropertyChanged(nameof(Arrow));
            OnPropertyChanged(nameof(NeededArrow));
            OnPropertyChanged(nameof(ArrowSeals));
            RecalcTotals();
        }
        private void RecalcKingcake()
        {
            OnPropertyChanged(nameof(Kingcake));
            OnPropertyChanged(nameof(NeededKingcake));
            OnPropertyChanged(nameof(KingcakeSeals));
            RecalcTotals();
        }

        private void RecalcAll()
        {
            RecalcBones();
            RecalcShells();
            RecalcOres();
            RecalcSeeds();
            RecalcFrancesca();
            RecalcMirror();
            RecalcArrow();
            RecalcKingcake();
        }
        #endregion
        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach (HWJob job in selectedCharacter.HWModel.HWJobList)
            {
                if (job.Anima.Progress == BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.Anima.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    HWInfo.ReloadJobList(AvailableJobs, job.Name);
                }
            }
            OnPropertyChanged(nameof(AvailableJobs));
            RecalcAll();
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

            HWJob tempJob = selectedCharacter.HWModel.HWJobList[HWInfo.JobListString.IndexOf(SelectedJob)];

            HWStageCompleter.ProgressClass(selectedCharacter, SelectedJob, tempJob.Anima, true);

            LoadAvailableJobs();
            RecalcAll();
        }
        #endregion

        #region Add Materials

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
            string materialType = (string)param;
            switch (materialType)
            {
                case "Bone":
                    UnidentifiableBone += 1;
                    break;
                case "Shell":
                    UnidentifiableShell += 1;
                    break;
                case "Ore":
                    UnidentifiableOre += 1;
                    break;
                case "Seeds":
                    UnidentifiableSeeds += 1;
                    break;
                case "Francesca":
                    Francesca += 1;
                    break;
                case "Mirror":
                    Mirror += 1;
                    break;
                case "Arrow":
                    Arrow += 1;
                    break;
                case "Kingcake":
                    Kingcake += 1;
                    break;
            }
        }

        #endregion
        #endregion
    }
}
