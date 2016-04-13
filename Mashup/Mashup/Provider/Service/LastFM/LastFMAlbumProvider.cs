//-----------------------------------------------------------------------
// <copyright file="LastFMAlbumProvider.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup.Provider.Service.LastFM
{
    using System.Globalization;
    using System.Threading.Tasks;
    using Entity;
    using Exception;
    /// <summary>
    /// Class who implements <see cref="LastFMProvider"/> and provides information about Music Album from $$(Last>FM)$$ API
    /// </summary>
    internal class LastFMAlbumProvider : LastFMProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LastFMAlbumProvider"/> class.
        /// </summary>
        public LastFMAlbumProvider() : base("http://ws.audioscrobbler.com/2.0")
        {
        }

        /// <summary>
        /// Build a request from an Identifier and its associated value. Use The API Key stored if you need it.
        /// </summary>
        /// <param name="id">The value</param>
        /// <param name="identifier">The identifier</param>
        /// <returns>The response server</returns>
        public override string RequestBuilder(string id, Identifier identifier)
        {
            switch (identifier)
            {
                case Identifier.Mbid:
                    return string.Format(CultureInfo.InvariantCulture, "{0}?method=album.getinfo&api_key={1}&mbid={2}&format={3}", this.Url, this.ApiKey, id, "json");

                default:
                    throw new IdentifierUnsupportedException("This identifier is not supported by " + this.GetType());
            }
        }

        /// <summary>
        /// Gets the raw data from a request, in which you have to provide an identifier and its value.
        /// Example of $$(id)$$ : $$(MBID = fc981ea3-e846-48f8-946d-51e9ef4b7198)$$
        /// </summary>
        /// <param name="id">The value</param>
        /// <param name="identifier">The identifier</param>
        /// <returns>The raw data returned by web services</returns>
        public override async Task<string> GetRawData(string id, Identifier identifier)
        {
            return await this.SendRequest(id, identifier);
        }
    }
}