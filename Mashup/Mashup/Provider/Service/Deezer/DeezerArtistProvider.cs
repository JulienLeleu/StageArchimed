//-----------------------------------------------------------------------
// <copyright file="DeezerArtistProvider.cs" company="Archimed">
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
    /// Class who implements <see cref="DeezerProvider"/> and provide information about Music Artist from $$(Deezer)$$ API
    /// </summary>
    internal class DeezerArtistProvider : DeezerProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeezerArtistProvider"/> class.
        /// </summary>
        public DeezerArtistProvider() : base("https://api.deezer.com/artist")
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
                case Identifier.Id:
                    return string.Format(CultureInfo.InvariantCulture, "{0}/{1}", this.Url, id);

                default:
                    throw new IdentifierUnsupportedException("This identifier is not supported by " + this.GetType());
            }
        }

        /// <summary>
        /// Gets the raw data from a request, in which you have to provide an identifier and its value.
        /// Example of $$(id)$$ : $$(ID = 705)$$
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