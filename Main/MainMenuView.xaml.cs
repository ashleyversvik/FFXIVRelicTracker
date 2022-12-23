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
                if (viewModel.CharacterList.Count == 1)
                {
                    viewModel.SelectedCharacter = viewModel.CharacterList.First();
                }
            }
        }
    }
}
