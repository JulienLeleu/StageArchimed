//-----------------------------------------------------------------------
// <copyright file="SpotifyArtistProvider.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup.Provider.Service.Spotify
{
    using System.Globalization;
    using System.Threading.Tasks;
    using IO;
    using Exception;
    using Spotify;
    using Spotify.Model.ArtistSearch;
    using Spotify.Model.Artist;
    using Entity;
    using System.Collections.Generic;
    /// <summary>
    /// Class who implements <see cref="DeezerProvider"/> and provide information about Music Artist from $$(Deezer)$$ API
    /// </summary>
    internal class SpotifyArtistProvider : SpotifyProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeezerArtistProvider"/> class.
        /// </summary>
        public SpotifyArtistProvider() : base(Method.Artist)
        {
        }

        /// <summary>
        /// Build a request from an Identifier and its associated value. Use The API Key stored if you need it.
        /// </summary>
        /// <param name="id">The value</param>
        /// <param name="identifier">The identifier</param>
        /// <param name="culture">The culture</param>
        /// <returns>The response server</returns>
        public override string RequestBuilder(Dictionary<Identifier, string> identifiers, CultureInfo culture)
        {
            if (identifiers.ContainsKey(Identifier.Id_Author))
            {
                return string.Format(CultureInfo.InvariantCulture, "{0}artists/{1}", this.Url, identifiers[Identifier.Id_Author]);
            }
            if (identifiers.ContainsKey(Identifier.Author))
            {
                return string.Format(CultureInfo.InvariantCulture, "{0}search?query={1}&limit=5&type=artist", this.Url, identifiers[Identifier.Author]);
            }
            throw new IdentifierUnsupportedException("This identifier is not supported by " + this.GetType());
        }

        /// <summary>
        /// Gets the raw data from a request, in which you have to provide an identifier and its value.
        /// Example of $$(id)$$ : $$(ID = 705)$$
        /// </summary>
        /// <param name="id">The value</param>
        /// <param name="identifier">The identifier</param>
        /// <param name="culture">The culture</param>
        /// <returns>The raw data returned by web services</returns>
        public override Task<string> GetRawData(Dictionary<Identifier, string> identifiers, CultureInfo culture)
        {
            if (identifiers.ContainsKey(Identifier.Author))
            {
                Task<string> infoArtist = SendRequest(identifiers, culture);
                infoArtist.Wait();
                SpotifyArtistSearch d = JsonBuilder.DeserializeJSon<SpotifyArtistSearch>(infoArtist.Result);
                if (d == null || d.Artists == null || d.Artists.Items == null || d.Artists.Items.Count == 0)
                {
                    var taskSource = new TaskCompletionSource<string>();
                    taskSource.SetResult("");
                    return taskSource.Task;
                }
                return this.GetRawData(new Dictionary<Identifier, string>() { { Identifier.Id_Author, d.Artists.Items[0].Id } }, culture);
            }
            return this.SendRequest(identifiers, culture);
        }

        /// <summary>
        /// Get the data as an object T from a request, in which you have to provide an identifier and its value. 
        /// </summary>
        /// <param name="id">The value</param>
        /// <param name="identifier">The identifier</param>
        /// <param name="culture">The culture</param>
        /// <returns>The object data returned by web services</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Méthode jamais appelée, ne présente aucun danger")]
        public override async Task<object> GetObjectData(Dictionary<Identifier, string> identifiers, CultureInfo culture)
        {
            string result = await this.GetRawData(identifiers, culture);
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