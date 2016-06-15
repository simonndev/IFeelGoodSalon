namespace IFeelGoodSalon.Infrastructure.Base
{
    public interface IHeaderInfoProvider<T>
    {
        T Header { get; }
    }
}
