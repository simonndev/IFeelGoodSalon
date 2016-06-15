using IFeelGoodSalon.Infrastructure;
using IFeelGoodSalon.Infrastructure.ViewModels;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFeelGoodSalon.Setup.ViewModels
{
    public class SetupRibbonTabViewModel : RibbonTabViewModelBase
    {
        #region Fields

        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        #endregion

        public SetupRibbonTabViewModel(IRegionManager regionManager, IEventAggregator eventAggregator )
        {
            this._regionManager = regionManager;
            this._eventAggregator = eventAggregator;
        }

        public override string Header
        {
            get
            {
                return "Setup";
            }
        }

        protected override void OnTabSelected(EventArgs e)
        {
            
        }
    }
}
