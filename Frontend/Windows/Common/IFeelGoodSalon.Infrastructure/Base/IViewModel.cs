using System;

namespace IFeelGoodSalon.Infrastructure.Base
{
    public interface IViewModel
    {
        void OnLoaded(EventArgs e);
        void OnUnloaded(EventArgs e);
    }
}
