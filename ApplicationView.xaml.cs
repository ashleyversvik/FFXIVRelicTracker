using FFXIVRelicTracker.Models;
using System.Windows;
using System.Windows.Input;

namespace FFXIVRelicTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
        }

        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }


        private ICommand _MinimizeCommand;
        public ICommand MinimizeCommandHandler
        {
            get
            {
                if (_MinimizeCommand == null)
                {
                    _MinimizeCommand = new RelayCommand(p => this.WindowState = WindowState.Minimized);
                }
                return _MinimizeCommand;
            }
        }
        private ICommand _ResizeCommand;
        public ICommand ResizeCommandHandler
        {
            get
            {
                if (_ResizeCommand == null)
                {
                    _ResizeCommand = new RelayCommand(p => ResizeMethod());
                }
                return _ResizeCommand;
            }
        }
        private void ResizeMethod()
        {
            if (this.WindowState == WindowState.Maximized) this.WindowState = WindowState.Normal;
            else this.WindowState = WindowState.Maximized;
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

    }
}
