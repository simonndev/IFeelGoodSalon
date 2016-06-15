using IFeelGoodSalon.Infrastructure.Base;
using Microsoft.Practices.Prism.Mvvm;
using System;

namespace IFeelGoodSalon.Infrastructure.ViewModels
{
    public abstract class ViewModelBase : BindableBase, IViewModel
    {
        private bool _isBusy;
        private string _busyMessage;

        public event EventHandler Loaded;
        public event EventHandler Unloaded;

        #region properties

        public bool IsBusy
        {
            get { return this._isBusy; }
            set
            {
                this._isBusy = value;
                base.OnPropertyChanged(() => IsBusy);
            }
        }

        public string BusyMessage
        {
            get { return this._busyMessage; }
            set
            {
                this._busyMessage = value;
                base.OnPropertyChanged(() => BusyMessage);
            }
        }

        #endregion

        public virtual void OnLoaded(EventArgs e)
        {
            this.Loaded?.Invoke(this, e);
        }

        public virtual void OnUnloaded(EventArgs e)
        {
            this.Unloaded?.Invoke(this, e);
        }
    }
}
