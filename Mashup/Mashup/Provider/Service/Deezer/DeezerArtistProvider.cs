//-----------------------------------------------------------------------
// <copyright file="DeezerArtistProvider.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup.Provider.Service.Deezer
{
    using System.Globalization;
    using System.Threading.Tasks;
    using IO;
    using Exception;
    using Model.Artist;
    using Model.Search;
    using Entity;

    /// <summary>
    /// Class who implements <see cref="DeezerProvider"/> and provide information about Music Artist from $$(Deezer)$$ API
    /// </summary>
    internal class DeezerArtistProvider : DeezerProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeezerArtistProvider"/> class.
        /// </summary>
        public DeezerArtistProvider() : base()
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
                    return string.Format(CultureInfo.InvariantCulture, "{0}artist/{1}", this.Url, id);
                case Identifier.Author:
                    return string.Format(CultureInfo.InvariantCulture, "{0}search?q={1}", this.Url, (string.IsNullOrEmpty(id) ? "" : "artist:" + id));

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
                    DeezerSearch d = JsonBuilder.DeserializeJSon<DeezerSearch>(infoArtist.Result);
                    if (d == null || d.Data == null || d.Data.Count == 0)
                    {
                        var taskSource = new TaskCompletionSource<string>();
                        taskSource.SetResult("");
                        return taskSource.Task;
                    }
                    return this.GetRawData(d.Data[0].Artist.Id, Identifier.Id, culture);

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
            return JsonBuilder.DeserializeJSon<DeezerArtist>(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public override object DeserializeData(string rawData)
        {
            return JsonBuilder.DeserializeJSon<DeezerArtist>(rawData);
        }
    }
}