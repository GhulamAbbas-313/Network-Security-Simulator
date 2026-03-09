using System;
using System.Windows;
using NetworkSecuritySimulator.UI.ViewModels;

namespace NetworkSecuritySimulator.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // TODO: Application startup logic
            // - Initialize singleton services
            // - Load configuration
            // - Set up dependency injection (optional)
            // - Show main window
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // TODO: Application cleanup
            // - Save state
            // - Close database connections
            // - Cleanup resources

            base.OnExit(e);
        }
    }
}
