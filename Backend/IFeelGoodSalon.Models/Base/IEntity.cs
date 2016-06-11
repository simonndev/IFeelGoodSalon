namespace IFeelGoodSalon.Models.Base
{
    public interface IEntity<TKey>
    {
        TKey Id { get; }
    }
}
