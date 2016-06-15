using Fluent;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Regions.Behaviors;
using System;
using System.Collections.Specialized;

namespace IFeelGoodSalon.Desktop.RegionAdapters
{
    public class FluentRibbonRegionBehavior : RegionBehavior, IHostAwareRegionBehavior
    {
        #region Fields

        public const string BehaviorKey = "FluentRibbonRegionBehavior";
        private Ribbon _hostControl;

        #endregion Fields

        public System.Windows.DependencyObject HostControl
        {
            get { return this._hostControl; }
            set
            {
                var ribbon = value as Ribbon;
                if (ribbon == null)
                {
                    throw new InvalidOperationException("Host control should be a Fluent Ribbon control.");
                }

                this._hostControl = ribbon;
            }
        }

        protected override void OnAttach()
        {
            if (this._hostControl == null)
            {
                throw new InvalidOperationException("Host control cannot be null.");
            }

            SynchronizeItems();

            this._hostControl.SelectedTabChanged += HostControlOnTabSelectionChanged;
            Region.ActiveViews.CollectionChanged += ActiveViewsOnCollectionChanged;
            Region.Views.CollectionChanged += ViewsOnCollectionChanged;
        }

        private void SynchronizeItems()
        {
            // TODO: initial item synchronization: copy from region to ribbon and vice-versa
        }

        private void ViewsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var newItem in e.NewItems)
                {
                    this._hostControl.Tabs.Add((RibbonTabItem)newItem);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var oldItem in e.OldItems)
                {
                    this._hostControl.Tabs.Remove((RibbonTabItem)oldItem);
                }
            }
        }

        private void ActiveViewsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Add)
            {
                this._hostControl.SelectedTabItem = (RibbonTabItem)notifyCollectionChangedEventArgs.NewItems[0];
            }
            else if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Remove
                     && this._hostControl.SelectedTabItem != null
                     && notifyCollectionChangedEventArgs.OldItems.Contains(this._hostControl.SelectedTabItem))
            {
                this._hostControl.SelectedTabItem = null;
            }
        }

        private void HostControlOnTabSelectionChanged(object sender, EventArgs eventArgs)
        {
            if (this._hostControl.SelectedTabItem == null)
            {
                return;
            }

            Region.Activate(this._hostControl.SelectedTabItem);
        }
    }
}