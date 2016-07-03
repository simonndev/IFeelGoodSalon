using System;

namespace IFeelGoodSalon.Data.Base
{
    /// <summary>
    /// A read-only DbContextScope. Refer to the comments for IDbContextScope
    /// for more details.
    /// </summary>
    public interface IReadOnlyDbContextScope : IDisposable
    {
        /// <summary>
        /// The DbContext instances that this DbContextScope manages.
        /// </summary>
        IDbContextManagerAsync Manager { get; }
    }
}
