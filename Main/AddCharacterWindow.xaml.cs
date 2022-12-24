using System.Windows;
using System.Windows.Input;

namespace FFXIVRelicTracker.Views
{
    /// <summary>
    /// Interaction logic for AddCharacterWindow.xaml
    /// </summary>
    public partial class AddCharacterWindow : Window
    {
        public AddCharacterWindow()
        {
            InitializeComponent();
            this.DataContext = MainWindow.DataContextProperty;
            this.FontSize = Application.Current.Windows[0].FontSize;
        }

        private void ContinueBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            NewServer.Text = "";
            this.Close();
        }
    }
}
