using FFXIVRelicTracker.Models.Helpers;
using FFXIVRelicTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FFXIVRelicTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SettingsManager.Load();
            OldFiles.Convert();

            MainWindow app = new MainWindow();
            ApplicationViewModel context = new ApplicationViewModel(Event.EventInstance.EventAggregator);
            app.DataContext = context;
            app.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            SettingsManager.Save();
        }
    }
}
