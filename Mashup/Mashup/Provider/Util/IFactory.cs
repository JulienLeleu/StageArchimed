//-----------------------------------------------------------------------
// <copyright file="IFactory.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace Mashup.Provider.Util
{
    using Mashup.Provider.Entity;

    /// <summary>
    /// Interface that each factory have to implements
    /// </summary>
    internal interface IFactory
    {
        /// <summary>
        /// Gets an instance of a service provider thanks to category parameter
        /// </summary>
        /// <param name="category">The category</param>
        /// <returns>The provider associated</returns>
        IProvider GetProvider(Category category);
    }
}