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
    using System.Collections.Generic;

    /// <summary>
    /// Class who implements <see cref="LastFMProvider"/> and provides information about Music Album from $$(Last>FM)$$ API
    /// </summary>
    internal class LastFMAlbumProvider : LastFMProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LastFMAlbumProvider"/> class.
        /// </summary>
        public LastFMAlbumProvider() : base(Method.Media)
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
            if (identifiers.ContainsKey(Identifier.Mbid))
            {
                return string.Format(CultureInfo.InvariantCulture, "{0}?method=album.getinfo&api_key={1}&mbid={2}&lang={3}&format={4}", this.Url, this.ApiKey, identifiers[Identifier.Mbid], culture.TwoLetterISOLanguageName, "json");
            }
            if (identifiers.ContainsKey(Identifier.Title))
            {
                if (identifiers.ContainsKey(Identifier.Author))
                {
                    return string.Format(CultureInfo.InvariantCulture, "{0}?method=album.search&api_key={1}&album={2}&artist={3}&lang={4}&format={5}&limit=5", this.Url, this.ApiKey, identifiers[Identifier.Title], identifiers[Identifier.Author], culture.TwoLetterISOLanguageName, "json");
                }
                return string.Format(CultureInfo.InvariantCulture, "{0}?method=album.search&api_key={1}&album={2}&lang={3}&format={4}&limit=5", this.Url, this.ApiKey, identifiers[Identifier.Title], culture.TwoLetterISOLanguageName, "json");
            }
            throw new IdentifierUnsupportedException("This identifier is not supported by " + this.GetType());
        }

        /// <summary>
        /// Gets the raw data from a request, in which you have to provide an identifier and its value.
        /// Example of $$(id)$$ : $$(MBID = fc981ea3-e846-48f8-946d-51e9ef4b7198)$$
        /// </summary>
        /// <param name="id">The value</param>
        /// <param name="identifier">The identifier</param>
        /// <param name="culture">The culture</param>
        /// <returns>The raw data returned by web services</returns>
        public override Task<string> GetRawData(Dictionary<Identifier, string> identifiers, CultureInfo culture)
        {
            if (identifiers.ContainsKey(Identifier.Title))
            {
                Task<string> infoAlbum = SendRequest(identifiers, culture);
                infoAlbum.Wait();
                LastFMAlbumSearch d = JsonBuilder.DeserializeJSon<LastFMAlbumSearch>(infoAlbum.Result);
                if (d == null || d.Results == null || d.Results.Albummatches == null || d.Results.Albummatches.Album == null || d.Results.Albummatches.Album.Count == 0)
                {
                    var taskSource = new TaskCompletionSource<string>();
                    taskSource.SetResult("");
                    return taskSource.Task;
                }
                return this.GetRawData(new Dictionary<Identifier, string>() {{ Identifier.Mbid, d.Results.Albummatches.Album[0].Mbid }}, culture);
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