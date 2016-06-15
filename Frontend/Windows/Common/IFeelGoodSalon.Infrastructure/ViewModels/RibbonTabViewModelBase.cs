using IFeelGoodSalon.Infrastructure.Base;
using Microsoft.Practices.Prism.Mvvm;
using System;

namespace IFeelGoodSalon.Infrastructure.ViewModels
{
    public abstract class RibbonTabViewModelBase : BindableBase, IRibbonTabViewModel, IHeaderInfoProvider<string>
    {
        #region Fields

        private bool _isSelected;

        #endregion

        public event EventHandler TabSelected;

        #region Properties

        public virtual string Header { get; }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                this._isSelected = value;

                if (value)
                {
                    OnTabSelected(EventArgs.Empty);
                }

                base.OnPropertyChanged(() => IsSelected);
            }
        }

        #endregion

        protected virtual void OnTabSelected(EventArgs e)
        {
            this.TabSelected?.Invoke(this, e);
        }
    }
}
