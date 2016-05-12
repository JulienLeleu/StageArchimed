//-----------------------------------------------------------------------
// <copyright file="LastFMAlbumProvider.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup.Provider.Service.LastFM
{
    using System.Globalization;
    using System.Threading.Tasks;
    using IO;
    using Exception;
    using Model.Album;
    using Model.AlbumSearch;
    using Entity;

    /// <summary>
    /// Class who implements <see cref="LastFMProvider"/> and provides information about Music Album from $$(Last>FM)$$ API
    /// </summary>
    internal class LastFMAlbumProvider : LastFMProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LastFMAlbumProvider"/> class.
        /// </summary>
        public LastFMAlbumProvider() : base()
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
                case Identifier.Mbid:
                    return string.Format(CultureInfo.InvariantCulture, "{0}?method=album.getinfo&api_key={1}&mbid={2}&lang={3}&format={4}", this.Url, this.ApiKey, id, culture.TwoLetterISOLanguageName, "json");
                case Identifier.Title:
                    return string.Format(CultureInfo.InvariantCulture, "{0}?method=album.search&api_key={1}&album={2}&lang={3}&format={4}&limit=5", this.Url, this.ApiKey, id, culture.TwoLetterISOLanguageName, "json");
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
        /// <param name="culture">The culture</param>
        /// <returns>The raw data returned by web services</returns>
        public override Task<string> GetRawData(string id, Identifier identifier, CultureInfo culture)
        {
            switch (identifier)
            {
                case Identifier.Title:
                    Task<string> infoAlbum = SendRequest(id, identifier, culture);
                    infoAlbum.Wait();
                    LastFMAlbumSearch d = JsonBuilder.DeserializeJSon<LastFMAlbumSearch>(infoAlbum.Result);
                    if (d == null || d.Results == null || d.Results.Albummatches == null || d.Results.Albummatches.Album == null || d.Results.Albummatches.Album.Count == 0)
                    {
                        var taskSource = new TaskCompletionSource<string>();
                        taskSource.SetResult("");
                        return taskSource.Task;
                    }
                    return this.GetRawData(d.Results.Albummatches.Album[0].Mbid, Identifier.Mbid, culture);

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
            return JsonBuilder.DeserializeJSon<LastFMAlbum>(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public override object DeserializeData(string rawData)
        {
            return JsonBuilder.DeserializeJSon<LastFMAlbum>(rawData);
        }
    }
}