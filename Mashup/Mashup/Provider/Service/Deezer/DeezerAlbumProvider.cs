//-----------------------------------------------------------------------
// <copyright file="DeezerAlbumProvider.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup.Provider.Service.Deezer
{
    using System.Globalization;
    using System.Threading.Tasks;
    using Entity;
    using Exception;
    /// <summary>
    /// Class who implements <see cref="DeezerProvider"/> and provide information about Music Album from $$(Deezer)$$ API
    /// </summary>
    internal class DeezerAlbumProvider : DeezerProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeezerAlbumProvider"/> class.
        /// </summary>
        public DeezerAlbumProvider() : base("https://api.deezer.com/album")
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
                case Identifier.Upc:
                    return string.Format(CultureInfo.InvariantCulture, "{0}/upc:{1}", this.Url, id);

                default:
                    throw new IdentifierUnsupportedException("This identifier is not supported by " + this.GetType());
            }
        }

        /// <summary>
        /// Gets the raw data from a request, in which you have to provide an identifier and its value.
        /// Example of $$(id)$$ : $$(UPC = 825646864836)$$
        /// </summary>
        /// <param name="id">The value</param>
        /// <param name="identifier">The identifier</param>
        /// <returns>The raw data returned by web services</returns>
        public override Task<string> GetRawData(string id, Identifier identifier)
        {
            return this.SendRequest(id, identifier);
        }
    }
}