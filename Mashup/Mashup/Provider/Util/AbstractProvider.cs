//-----------------------------------------------------------------------
// <copyright file="AbstractProvider.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup.Provider.Util
{
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Threading.Tasks;
    using Entity;
    using System;

    /// <summary>
    /// Abstract class of a provider as $$(Deezer)$$, $$(Spotify)$$ ...
    /// </summary>
    internal abstract class AbstractProvider : IProvider
    {
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
        public AbstractProvider(string url)
        {
            this.Url = url;
            this.ApiKey = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractProvider"/> class.
        /// </summary>
        /// <param name="url">The API main URL</param>
        /// <param name="apiKey">The API key</param>
        public AbstractProvider(string url, string apiKey) : this(url)
        {
            this.ApiKey = apiKey;
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
        /// Deserialize data from JSON to C# object
        /// </summary>
        /// <typeparam name="T">The object returned</typeparam>
        /// <param name="jsonString">The JSON data incoming</param>
        /// <returns>The data object</returns>
        public static T DeserializeJSon<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            T newObject = default(T);
            MemoryStream stream = null;
            try
            {
                stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                newObject = (T)ser.ReadObject(stream);
            }
            catch (IOException e)
            {
                e.GetBaseException();
            }
            finally
            {
                stream.Dispose();
            }

            return newObject;
        }

        /// <summary>
        /// Build a request from an Identifier and its associated value. Use The API Key stored if you need it.
        /// </summary>
        /// <param name="id">The value</param>
        /// <param name="identifier">The identifier</param>
        /// <returns>The response server</returns>
        public abstract string RequestBuilder(string id, Identifier identifier);

        /// <summary>
        /// Gets the raw data from a request, in which you have to provide an identifier and its value.
        /// </summary>
        /// <param name="id">The value</param>
        /// <param name="identifier">The identifier</param>
        /// <returns>The raw data returned by web services</returns>
        public abstract Task<string> GetRawData(string id, Identifier identifier);

        /// <summary>
        /// Get the data as an object T from a request, in which you have to provide an identifier and its value. 
        /// </summary>
        /// <typeparam name="T">Type of object returned</typeparam>
        /// <param name="id">The value</param>
        /// <param name="identifier">The identifier</param>
        /// <returns>The object data returned by web services</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Méthode jamais appelée, ne présente aucun danger")]
        public async Task<T> GetObjectData<T>(string id, Identifier identifier)
        {
            string result = await this.GetRawData(id, identifier);
            return DeserializeJSon<T>(result);
        }

        /// <summary>
        /// Send a request with an Identifier and its value then get the response
        /// </summary>
        /// <param name="id">The value</param>
        /// <param name="identifier">The identifier</param>
        /// <returns>The response</returns>
        public async Task<string> SendRequest(string id, Identifier identifier)
        {
            var client = new WebClient() { Encoding = Encoding.UTF8 };
            try
            {
                Uri url = new Uri(this.RequestBuilder(id, identifier));
                return await client.DownloadStringTaskAsync(url.ToString());
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine(e);
                Console.ForegroundColor = ConsoleColor.White;
            }
            return null;
        }
    }
}