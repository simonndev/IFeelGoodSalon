using IFeelGoodSalon.DataPattern.Ef6;
using System.ComponentModel.DataAnnotations;

namespace IFeelGoodSalon.Models.Base
{
    public abstract class EntityBase<TKey> : IEntity<TKey>
    {
        public TKey Id { get; }
    }
}
