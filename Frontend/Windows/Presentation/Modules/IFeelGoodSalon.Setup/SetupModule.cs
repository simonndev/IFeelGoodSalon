using IFeelGoodSalon.Infrastructure;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFeelGoodSalon.Setup
{
    [Module(ModuleName = "IFeelGoodSalon.Setup")]
    public class SetupModule : IModule
    {
        #region Fields

        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        #endregion

        public SetupModule(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this._regionManager = regionManager;
            this._eventAggregator = eventAggregator;
        }

        public void Initialize()
        {
            Uri workspaceUri = new Uri("Views/ServicesSetupView", UriKind.Relative);
            this._regionManager.RequestNavigate(RegionNames.MainContentRegion, workspaceUri);

            //Uri leftUri = new Uri("Views/SetupSelectionView", UriKind.Relative);
            //this._regionManager.RequestNavigate(RegionNames.LeftContentRegion, workspaceUri);
        }
    }
}
