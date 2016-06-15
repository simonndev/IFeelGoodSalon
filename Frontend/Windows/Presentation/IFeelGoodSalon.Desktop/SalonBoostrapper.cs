using IFeelGoodSalon.Infrastructure.Views;
using Microsoft.Practices.Prism.UnityExtensions;
using System;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using IFeelGoodSalon.Desktop.Views;
using Microsoft.Practices.Prism.Regions;
using IFeelGoodSalon.Desktop.RegionAdapters;
using Fluent;

namespace IFeelGoodSalon.Desktop
{
    public class SalonBoostrapper : UnityBootstrapper
    {
        protected override IModuleCatalog CreateModuleCatalog()
        {
            string moduleCatelogUrl = "/IFeelGoodSalon.Desktop;component/ModuleCatalog.xaml";
            Uri moduleCatelogUri = new Uri(moduleCatelogUrl, UriKind.Relative);

            return Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(moduleCatelogUri);
        }

        protected override DependencyObject CreateShell()
        {
            // keep in mind that Views/Shell implements IShell
            return this.Container.Resolve<IShell>() as DependencyObject;
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            this.Container.RegisterType<IShell, ShellView>(new ContainerControlledLifetimeManager());
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            var mappings = base.ConfigureRegionAdapterMappings();
            if (mappings != null)
            {
                mappings.RegisterMapping(typeof(Ribbon), Container.Resolve<FluentRibbonRegionAdapter>());
                mappings.RegisterMapping(typeof(RibbonTabItem), Container.Resolve<FluentRibbonTabItemRegionAdapter>());
            }

            return mappings;
        }
    }
}
