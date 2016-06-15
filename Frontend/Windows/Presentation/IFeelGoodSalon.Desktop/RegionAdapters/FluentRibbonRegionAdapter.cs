using Fluent;
using Microsoft.Practices.Prism.Regions;

namespace IFeelGoodSalon.Desktop.RegionAdapters
{
    public class FluentRibbonRegionAdapter : RegionAdapterBase<Ribbon>
    {
        public FluentRibbonRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, Ribbon regionTarget)
        {
        }

        protected override void AttachBehaviors(IRegion region, Ribbon regionTarget)
        {
            base.AttachBehaviors(region, regionTarget);

            if (!region.Behaviors.ContainsKey(FluentRibbonRegionBehavior.BehaviorKey))
            {
                var regionBehavior = new FluentRibbonRegionBehavior { HostControl = regionTarget };
                region.Behaviors.Add(FluentRibbonRegionBehavior.BehaviorKey, regionBehavior);
            }
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }
    }
}
