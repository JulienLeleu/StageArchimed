//-----------------------------------------------------------------------
// <copyright file="LastFMFactory.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace Mashup.Provider.Service.LastFM
{
    using Mashup.Provider.Entity;
    using Util;

    /// <summary>
    /// Class who implements <see cref="IFactory"/> and provide instances of <see cref="LastFMProvider"/> 
    /// </summary>
    internal class LastFMFactory : IFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LastFMFactory"/> class.
        /// </summary>
        public LastFMFactory()
        {
        }

        /// <summary>
        /// Gets a <see cref="LastFMProvider"/> from a type of category
        /// </summary>
        /// <param name="category">The kind of category</param>
        /// <returns>The new instance</returns>
        public IProvider GetProvider(Category category)
        {
            switch (category)
            {
                case Category.Album:
                    return new LastFMAlbumProvider();

                case Category.Artist:
                    return new LastFMArtistProvider();

                default:
                    return null;
            }
        }
    }
}