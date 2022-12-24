using FFXIVRelicTracker._03_HW.Main;
using FFXIVRelicTracker._04_SB.Main;
using FFXIVRelicTracker._05_ShB.Main;
using FFXIVRelicTracker._05_Skysteel.Main;
using FFXIVRelicTracker._06_EW.Main;
using FFXIVRelicTracker.Main;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FFXIVRelicTracker.ViewModels
{
    class ApplicationViewModel : ObservableObject

    {
        #region Fields

        private ICommand _changePageCommand;
        private ICommand _MainMenuCommand;

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        private MainMenuViewModel _mainMenu;

        #endregion
        #region Constructors
        public ApplicationViewModel(IEventAggregator iEventAggregator)
        {
            LoadSettings();
            this.iEventAggregator = iEventAggregator;
            SubscriptionToken subscriptionToken =
                                    this
                                        .iEventAggregator
                                        .GetEvent<PubSubEvent<Character>>()
                                        .Subscribe((details) =>
                                        {
                                            this.SelectedCharacter = details;
                                        });

            // Add available pages
            _mainMenu = new MainMenuViewModel(Event.EventInstance.EventAggregator);
            MenuViewModels.Add(_mainMenu);
            PageViewModels.Add(new ArrViewModel(Event.EventInstance.EventAggregator));
            PageViewModels.Add(new HWViewModel(Event.EventInstance.EventAggregator));
            PageViewModels.Add(new SBViewModel(Event.EventInstance.EventAggregator));
            PageViewModels.Add(new ShBViewModel(Event.EventInstance.EventAggregator));
            PageViewModels.Add(new SkysteelViewModel(Event.EventInstance.EventAggregator));
            PageViewModels.Add(new EWViewModel(Event.EventInstance.EventAggregator));

            // Set starting page
            CurrentPageViewModel = MenuViewModels[0];

        }
        #endregion


        #region Properties / Commands
        private IEventAggregator iEventAggregator;
        private Character selectedCharacter;
        private List<IPageViewModel> _menuViewModels;

        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                selectedCharacter = value;
                OnPropertyChanged(nameof(SelectedCharacter));
            }
        }

        private void performMainCloseButtonCommand(object Parameter)
        {
            Window objWindow = Parameter as Window;
            objWindow.Close();
        }

        public ICommand MainMenuPageCommand
        {
            get
            {
                if (_MainMenuCommand == null)
                {
                    _MainMenuCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p));
                }

                return _MainMenuCommand;
            }
        }


        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel & SelectedCharacter!=null);
                }

                return _changePageCommand;
            }
        }

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        public List<IPageViewModel> MenuViewModels
        {
            get
            {
                if (_menuViewModels == null)
                    _menuViewModels = new List<IPageViewModel>();

                return _menuViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }
        #region Open Settings
        private ICommand _SettingsCommand;
        public ICommand SettingsCommand
        {
            get
            {
                if (_SettingsCommand == null)
                {
                    _SettingsCommand = new RelayCommand(
                        param => this.SettingsMenu(),
                        param => this.CanSettings()
                        );
                }
                return _SettingsCommand;
            }
        }

        private bool CanSettings()
        {
            return true;
        }
        private void SettingsMenu()
        {
            SettingsWindow SettingWindow = new SettingsWindow();
            SettingWindow.Closing += new CancelEventHandler(_Closing);
            SettingWindow.ShowInTaskbar = false;
            SettingWindow.Owner = Application.Current.MainWindow;
            SettingWindow.Show();

            SettingWindow.FontButton.Text = Application.Current.Windows[0].FontSize.ToString();
        }

        void _Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SettingsManager.Settings.FontSize = Application.Current.Windows[0].FontSize;
        }
        #endregion
        #region Open Settings
        private ICommand _SaveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                {
                    _SaveCommand = _mainMenu.SaveCommand;
                }
                return _SaveCommand;
            }
        }
        #endregion
        #endregion

        #region Methods

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        private void LoadSettings()
        {
            foreach (Window window in Application.Current.Windows)
            {
                window.FontSize = SettingsManager.Settings.FontSize;
            }
        }

        #endregion

    }
}
