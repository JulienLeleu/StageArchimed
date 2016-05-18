//-----------------------------------------------------------------------
// <copyright file="SpotifyProvider.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup.Provider.Service.Spotify
{
    using Entity;
    using Mashup.Provider.Util;

    /// <summary>
    /// Class who implements <see cref="AbstractProvider"/>
    /// </summary>
    internal abstract class SpotifyProvider : AbstractProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeezerProvider"/> class.
        /// </summary>
        /// <param name="url">The main $$(Spotify)$$ url</param>
        public SpotifyProvider(Method methodType) : base(Media.Music, methodType, "https://api.spotify.com/v1/", null)
        {
        }
    }
}