namespace IFeelGoodSalon.Infrastructure.Base
{
    public interface IBusyInfoProvider
    {
        bool IsBusy { get; set; }
        string BusyMessage { get; set; }
    }
}
