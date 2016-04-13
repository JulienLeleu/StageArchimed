//-----------------------------------------------------------------------
// <copyright file="DeezerFactory.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace Mashup.Provider.Service.Deezer
{
    using Mashup.Provider.Entity;
    using Util;

    /// <summary>
    /// Class who implements <see cref="DeezerFactory"/> and provide instances of <see cref="DeezerProvider"/> 
    /// </summary>
    internal class DeezerFactory : IFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeezerFactory"/> class.
        /// </summary>
        public DeezerFactory()
        {
        }

        /// <summary>
        /// Gets a <see cref="DeezerProvider"/> from a type of category
        /// </summary>
        /// <param name="category">The kind of category</param>
        /// <returns>The new instance</returns>
        public IProvider GetProvider(Category category)
        {
            switch (category)
            {
                case Category.Album:
                    return new DeezerAlbumProvider();

                case Category.Artist:
                    return new DeezerArtistProvider();

                default:
                    return null;
            }
        }
    }
}