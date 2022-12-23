using FFXIVRelicTracker.ViewModels;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FFXIVRelicTracker.Views
{
    /// <summary>
    /// Interaction logic for MainMenuView.xaml
    /// </summary>
    public partial class MainMenuView : UserControl
    {
        public MainMenuView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainMenuViewModel)DataContext;
            if (viewModel.LoadCommand.CanExecute(null))
            {
                viewModel.LoadCommand.Execute(null);
                if (!string.IsNullOrEmpty(SettingsManager.Settings.LastOpenCharacter))
                    SelectCharacterFromSettings();
                else
                    SelectCharacterIfSingle();

            }
        }

        private void SelectCharacterFromSettings()
        {
            if (string.IsNullOrWhiteSpace(SettingsManager.Settings.LastOpenCharacter))
                return;

            var viewModel = (MainMenuViewModel)DataContext;
            var character = viewModel.CharacterList.FirstOrDefault(x => x.ToString() == SettingsManager.Settings.LastOpenCharacter);
            if(character != null)
            {
                viewModel.SelectedCharacter = character;
            }
        }

        private void SelectCharacterIfSingle()
        {
            var viewModel = (MainMenuViewModel)DataContext;
            if (viewModel.CharacterList.Count == 1)
            {
                viewModel.SelectedCharacter = viewModel.CharacterList.First();
            }
        }
    }
}
