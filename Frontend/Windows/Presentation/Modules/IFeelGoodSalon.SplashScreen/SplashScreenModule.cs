using IFeelGoodSalon.Infrastructure.Events;
using IFeelGoodSalon.Infrastructure.Views;
using IFeelGoodSalon.SplashScreen.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Threading;
using System.Windows.Threading;

namespace IFeelGoodSalon.SplashScreen
{
    [Module(ModuleName = "IFeelGoodSalon.SplashScreen")]
    public class SplashScreenModule : IModule
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IShell _shell;

        private AutoResetEvent _shellLoadedEvent;

        public SplashScreenModule(IShell shell, IEventAggregator eventAggregator)
        {
            this._shell = shell;
            this._eventAggregator = eventAggregator;
        }

        public void Initialize()
        {
            Dispatcher.CurrentDispatcher.BeginInvoke(
                (Action)(() =>
                {
                    this._shell.Show();
                    this._eventAggregator.GetEvent<CloseSplashScreenEvent>
                        ().Publish(new CloseSplashScreenEvent());
                }));

            this._shellLoadedEvent = new AutoResetEvent(false);

            ThreadStart showSplashScreenThread =
                () =>
                {
                    Dispatcher.CurrentDispatcher.BeginInvoke(
                        (Action)(() =>
                        {
                            var splashscreen = ServiceLocator.Current.GetInstance<SplashScreenView>();

                            this._eventAggregator.GetEvent<CloseSplashScreenEvent>().Subscribe(
                                        e => splashscreen.Dispatcher.BeginInvoke((Action)splashscreen.Close),
                                        ThreadOption.PublisherThread,
                                        true);

                            splashscreen.Show();

                            this._shellLoadedEvent.Set();
                        }));

                    Dispatcher.Run();
                };

            var thread = new Thread(showSplashScreenThread)
            {
                Name = "ShowSplashScreenThread",
                IsBackground = true
            };

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            this._shellLoadedEvent.WaitOne();
        }
    }
}
