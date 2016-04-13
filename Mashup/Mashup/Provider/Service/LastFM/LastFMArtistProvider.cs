//-----------------------------------------------------------------------
// <copyright file="LastFMArtistProvider.cs" company="Archimed">
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
    /// Class who implements <see cref="LastFMProvider"/> and provides information about Music Album from $$(LastFM)$$ API
    /// </summary>
    internal class LastFMArtistProvider : LastFMProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LastFMArtistProvider"/> class.
        /// </summary>
        public LastFMArtistProvider() : base("http://ws.audioscrobbler.com/2.0")
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
                    return string.Format(CultureInfo.InvariantCulture, "{0}?method=artist.getinfo&api_key={1}&mbid={2}&format={3}", this.Url, this.ApiKey, id, "json");

                default:
                    throw new IdentifierUnsupportedException("This identifier is not supported by " + this.GetType());
            }
        }

        /// <summary>
        /// Gets the raw data from a request, in which you have to provide an identifier and its value.
        /// Example of $$(id)$$ : $$(MBID = 9c9f1380-2516-4fc9-a3e6-f9f61941d090)$$
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