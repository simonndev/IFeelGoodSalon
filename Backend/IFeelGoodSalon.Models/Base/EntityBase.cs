using IFeelGoodSalon.DataPattern.Ef6;

namespace IFeelGoodSalon.Models.Base
{
    public abstract class EntityBase<TKey> : ObservableEntity, IEntity<TKey>
    {
        public TKey Id { get; }
    }
}
