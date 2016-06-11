using IFeelGoodSalon.Models.Base;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace IFeelGoodSalon.DataAccess.Maps.Base
{
    public abstract class EntityMapBase<TEntity, TKey> : EntityTypeConfiguration<TEntity> where TEntity : EntityBase<TKey>
    {
        public EntityMapBase()
        {
            this.HasKey(entity => entity.Id);
        }
    }

    public abstract class IdentityEntityMapBase<TEntity> : EntityMapBase<TEntity, int> where TEntity : EntityBase
    {
        public IdentityEntityMapBase()
            : base()
        {
            this.Property(entity => entity.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
