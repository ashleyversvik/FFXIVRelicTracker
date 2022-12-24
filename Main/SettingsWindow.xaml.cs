using System.Windows;
using System.Windows.Input;

namespace FFXIVRelicTracker.Main
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            this.DataContext = MainWindow.DataContextProperty;

            this.FontSize = Application.Current.Windows[0].FontSize;
        }

        private void ContinueBtn_Click(object sender, RoutedEventArgs e)
        {

            double FontSetting = Application.Current.Windows[0].FontSize;
            double.TryParse(this.FontButton.Text, out FontSetting);

            if (FontSetting <= 0) { FontSetting = Application.Current.Windows[0].FontSize; }

            foreach (Window window in Application.Current.Windows)
            {
                window.FontSize = FontSetting;
            }
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            FontButton.Text = Application.Current.Windows[0].FontSize.ToString(); 
            this.Close();
        }
    }
}
