using System;

namespace IFeelGoodSalon.Data
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// As far as I can make out, a string would have worked just fine.
    /// 
    /// This is done for optimization purposes, Creating an empty class
    /// is cheaper and uses up less memory than generating a unique string.
    /// </remarks>
    internal class DbContextInstanceIdentifier : MarshalByRefObject
    {
    }
}
