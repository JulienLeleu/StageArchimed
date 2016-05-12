//-----------------------------------------------------------------------
// <copyright file="AbstractProvider.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup.Provider.Util
{
    using System;
    using System.Globalization;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Entity;
    
    /// <summary>
    /// Abstract class of a provider as $$(Deezer)$$, $$(Spotify)$$ ...
    /// </summary>
    internal abstract class AbstractProvider : IProvider
    {
        /// <summary>
        /// The type of media
        /// </summary>
        private Media mediaType;

        /// <summary>
        /// API main URL
        /// </summary>
        private string url;

        /// <summary>
        /// API access key who allows to use their services
        /// </summary>
        private string apiKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractProvider"/> class.
        /// </summary>
        /// <param name="url">The API main URL</param>
        internal AbstractProvider(Media mediaType, string url)
        {
            this.MediaType = mediaType;
            this.Url = url;
            this.ApiKey = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractProvider"/> class.
        /// </summary>
        /// <param name="url">The API main URL</param>
        /// <param name="apiKey">The API key</param>
        internal AbstractProvider(Media mediaType, string url, string apiKey) : this(mediaType, url)
        {
            this.ApiKey = apiKey;
        }

        /// <summary>
        /// Gets or sets the type of media
        /// </summary>
        public Media MediaType
        {
            get
            {
                return mediaType;
            }

            set
            {
                mediaType = value;
            }
        }

        /// <summary>
        /// Gets or sets the Url
        /// </summary>
        public string Url
        {
            get
            {
                return this.url;
            }

            set
            {
                this.url = value;
            }
        }

        /// <summary>
        /// Gets or sets the API Key
        /// </summary>
        public string ApiKey
        {
            get
            {
                return this.apiKey;
            }

            set
            {
                this.apiKey = value;
            }
        }

        /// <summary>
        /// Build a request from an Identifier and its associated value. Use The API Key stored if you need it.
        /// </summary>
        /// <param name="id">The value</param>
        /// <param name="identifier">The identifier</param>
        /// <param name="culture">The culture</param>
        /// <returns>The response server</returns>
        public abstract string RequestBuilder(string id, Identifier identifier, CultureInfo culture);

        /// <summary>
        /// Gets the raw data from a request, in which you have to provide an identifier and its value.
        /// </summary>
        /// <param name="id">The value</param>
        /// <param name="identifier">The identifier</param>
        /// <param name="culture">The culture</param>
        /// <returns>The raw data returned by web services</returns>
        public abstract Task<string> GetRawData(string id, Identifier identifier, CultureInfo culture);

        /// <summary>
        /// Get the data as an object T from a request, in which you have to provide an identifier and its value. 
        /// </summary>
        /// <param name="id">The value</param>
        /// <param name="identifier">The identifier</param>
        /// <param name="culture">The culture</param>
        /// <returns>The object data returned by web services</returns>
        public abstract Task<object> GetObjectData(string id, Identifier identifier, CultureInfo culture);

        /// <summary>
        /// Send a request with an Identifier and its value then get the response
        /// </summary>
        /// <param name="id">The value</param>
        /// <param name="identifier">The identifier</param>
        /// <param name="culture">The culture</param>
        /// <returns>The response</returns>
        public async Task<string> SendRequest(string id, Identifier identifier, CultureInfo culture)
        {
            var client = new WebClient() { Encoding = Encoding.UTF8 };
            try
            {
                Uri url = new Uri(this.RequestBuilder(id, identifier, culture));
                return await client.DownloadStringTaskAsync(url.ToString());
            }
            catch (Exception e)
            {
                /*Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine(e);
                Console.ForegroundColor = ConsoleColor.White;*/
            }

            return null;
        }

        public abstract object DeserializeData(string rawData);
    }
}