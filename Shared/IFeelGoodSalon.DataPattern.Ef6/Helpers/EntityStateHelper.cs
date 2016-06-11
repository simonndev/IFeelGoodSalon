using IFeelGoodSalon.DataPattern.Ef6.Base;
using System;
using System.Data.Entity;

namespace IFeelGoodSalon.DataPattern.Ef6.Helpers
{
    public class EntityStateHelper
    {
        public static EntityState ConvertState(EntityModelState state)
        {
            switch (state)
            {
                case EntityModelState.Added:
                    return EntityState.Added;

                case EntityModelState.Modified:
                    return EntityState.Modified;

                case EntityModelState.Deleted:
                    return EntityState.Deleted;

                default:
                    return EntityState.Unchanged;
            }
        }

        public static EntityModelState ConvertState(EntityState state)
        {
            switch (state)
            {
                case EntityState.Detached:
                    return EntityModelState.Unchanged;

                case EntityState.Unchanged:
                    return EntityModelState.Unchanged;

                case EntityState.Added:
                    return EntityModelState.Added;

                case EntityState.Deleted:
                    return EntityModelState.Deleted;

                case EntityState.Modified:
                    return EntityModelState.Modified;

                default:
                    throw new ArgumentOutOfRangeException("state");
            }
        }
    }
}
