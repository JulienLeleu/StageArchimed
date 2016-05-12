//-----------------------------------------------------------------------
// <copyright file="SpotifyArtistProvider.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup.Provider.Service.Deezer
{
    using System.Globalization;
    using System.Threading.Tasks;
    using IO;
    using Exception;
    using Spotify;
    using Spotify.Model.ArtistSearch;
    using Spotify.Model.Artist;
    using Entity;

    /// <summary>
    /// Class who implements <see cref="DeezerProvider"/> and provide information about Music Artist from $$(Deezer)$$ API
    /// </summary>
    internal class SpotifyArtistProvider : SpotifyProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeezerArtistProvider"/> class.
        /// </summary>
        public SpotifyArtistProvider() : base()
        {
        }

        /// <summary>
        /// Build a request from an Identifier and its associated value. Use The API Key stored if you need it.
        /// </summary>
        /// <param name="id">The value</param>
        /// <param name="identifier">The identifier</param>
        /// <param name="culture">The culture</param>
        /// <returns>The response server</returns>
        public override string RequestBuilder(string id, Identifier identifier, CultureInfo culture)
        {
            switch (identifier)
            {
                case Identifier.Id:
                    return string.Format(CultureInfo.InvariantCulture, "{0}artists/{1}", this.Url, id);
                case Identifier.Author:
                    return string.Format(CultureInfo.InvariantCulture, "{0}search?query={1}&limit=5&type=artist", this.Url, id);

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
        /// <param name="culture">The culture</param>
        /// <returns>The raw data returned by web services</returns>
        public override Task<string> GetRawData(string id, Identifier identifier, CultureInfo culture)
        {
            switch (identifier)
            {
               case Identifier.Author:
                    Task<string> infoArtist = SendRequest(id, identifier, culture);
                    infoArtist.Wait();
                    SpotifyArtistSearch d = JsonBuilder.DeserializeJSon<SpotifyArtistSearch>(infoArtist.Result);
                    if (d == null || d.Artists == null || d.Artists.Items == null || d.Artists.Items.Count == 0)
                    {
                        var taskSource = new TaskCompletionSource<string>();
                        taskSource.SetResult("");
                        return taskSource.Task;
                    }
                    return this.GetRawData(d.Artists.Items[0].Id, Identifier.Id, culture);

                default:
                    return this.SendRequest(id, identifier, culture);
            }
        }

        /// <summary>
        /// Get the data as an object T from a request, in which you have to provide an identifier and its value. 
        /// </summary>
        /// <param name="id">The value</param>
        /// <param name="identifier">The identifier</param>
        /// <param name="culture">The culture</param>
        /// <returns>The object data returned by web services</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Méthode jamais appelée, ne présente aucun danger")]
        public override async Task<object> GetObjectData(string id, Identifier identifier, CultureInfo culture)
        {
            string result = await this.GetRawData(id, identifier, culture);
            return JsonBuilder.DeserializeJSon<SpotifyArtist>(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public override object DeserializeData(string rawData)
        {
            return JsonBuilder.DeserializeJSon<SpotifyArtist>(rawData);
        }
    }
}