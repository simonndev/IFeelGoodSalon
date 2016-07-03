﻿using System.Collections;
using System.Threading.Tasks;

namespace IFeelGoodSalon.Data.Base
{
    public interface IDbContextScopeAsync : IDbContextScope
    {
        /// <summary>
        /// The DbContext instances that this DbContextScope manages.
        /// Don't call SaveChanges() on the DbContext themselves!
        /// Save the scope instead.
        /// </summary>
        IDbContextManagerAsync Manager { get; }

        /// <summary>
        /// Saves the changes in all the DbContext instances that were created within this scope.
        /// This method can only be called once per scope.
        /// </summary>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Reloads the provided persistent entities from the data store
        /// in the DbContext instances managed by the parent scope.
        /// 
        /// If there is no parent scope (i.e. if this DbContextScope is the top-level scope), does nothing.
        /// 
        /// This is useful when you have forced the creation of a new DbContextScope
        /// and want to make sure that the parent scope
        /// (if any) is aware of the entities you've modified in the 
        /// inner scope.
        /// </summary>
        /// <param name="entities"></param>
        /// <remarks>
        /// Ihis is a pretty advanced feature that should be used with parsimony.
        /// </remarks>
        Task ReloadParentScopeAsync(IEnumerable entities);
    }
}
