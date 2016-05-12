//-----------------------------------------------------------------------
// <copyright file="SpotifyAlbumProvider.cs" company="Archimed">
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
    using Spotify.Model.AlbumSearch;
    using Spotify.Model.Album;
    using Entity;

    /// <summary>
    /// Class who implements <see cref="DeezerProvider"/> and provide information about Music Album from $$(Deezer)$$ API
    /// </summary>
    internal class SpotifyAlbumProvider : SpotifyProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeezerAlbumProvider"/> class.
        /// </summary>
        public SpotifyAlbumProvider() : base()
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
                    return string.Format(CultureInfo.InvariantCulture, "{0}albums/{1}", this.Url, id);
                case Identifier.Title:
                    return string.Format(CultureInfo.InvariantCulture, "{0}search?query={1}&limit=5&type=album", this.Url, id);
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
        /// <param name="culture">The culture</param>
        /// <returns>The raw data returned by web services</returns>
        public override Task<string> GetRawData(string id, Identifier identifier, CultureInfo culture)
        {
            switch(identifier)
            {
                case Identifier.Title:
                    Task<string> infoAlbum = SendRequest(id, identifier, culture);
                    infoAlbum.Wait();
                    SpotifyAlbumSearch d = JsonBuilder.DeserializeJSon<SpotifyAlbumSearch>(infoAlbum.Result);
                    if (d == null || d.Albums == null || d.Albums.Items == null || d.Albums.Items.Count == 0)
                    {
                        var taskSource = new TaskCompletionSource<string>();
                        taskSource.SetResult("");
                        return taskSource.Task;
                    }
                    return this.GetRawData(d.Albums.Items[0].Id, Identifier.Id, culture);
                
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
            return JsonBuilder.DeserializeJSon<SpotifyAlbum>(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public override object DeserializeData(string rawData)
        {
            return JsonBuilder.DeserializeJSon<SpotifyAlbum>(rawData);
        }
    }
}