using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace IFeelGoodSalon.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

#if (DEBUG)
            RunInDebugMode();
#else
            RunInReleaseMode();
#endif
            this.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        private static void RunInDebugMode()
        {
            SalonBoostrapper bootstrapper = new SalonBoostrapper();
            bootstrapper.Run();
        }

        private static void RunInReleaseMode()
        {
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
            try
            {
                SalonBoostrapper bootstrapper = new SalonBoostrapper();
                bootstrapper.Run();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        private static void HandleException(Exception ex)
        {
            if (ex == null)
            {
                return;
            }

            //ExceptionPolicy.HandleException(ex, "Default Policy");
            MessageBox.Show(ex.Message, "Unhandled Error Occurred");
            Environment.Exit(1);
        }
    }
}
