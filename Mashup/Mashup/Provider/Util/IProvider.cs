//-----------------------------------------------------------------------
// <copyright file="IProvider.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup.Provider.Util
{
    using System.Globalization;
    using System.Threading.Tasks;
    using Entity;
    using System.Collections.Generic;

    /// <summary>
    /// Provider Interface
    /// </summary>
    internal interface IProvider
    {
        /// <summary>
        /// Gets or sets the type of media
        /// </summary>
        Media MediaType { get; set; }

        Method MethodType { get; set; }

        /// <summary>
        /// Gets or sets the url
        /// </summary>
        string Url { get; set; }

        /// <summary>
        /// Gets or sets the api key
        /// </summary>
        string ApiKey { get; set; }

        /// <summary>
        /// Build a request from an Identifier and its associated value. Use The API Key stored if you need it.
        /// </summary>
        /// <param name="id">The value</param>
        /// <param name="identifier">The identifier</param>
        /// <param name="culture">The culture</param>
        /// <returns>The response server</returns>
        string RequestBuilder(Dictionary<Identifier, string> identifiers, CultureInfo culture);

        /// <summary>
        /// Gets the raw data from a request, in which you have to provide an identifier and its value.
        /// </summary>
        /// <param name="id">The value</param>
        /// <param name="identifier">The identifier</param>
        /// <param name="culture">The culture</param>
        /// <returns>The raw data returned by web services</returns>
        Task<string> GetRawData(Dictionary<Identifier, string> identifiers, CultureInfo culture);

        /// <summary>
        /// Get the data as an object T from a request, in which you have to provide an identifier and its value. 
        /// </summary>
        /// <param name="id">The value</param>
        /// <param name="identifier">The identifier</param>
        /// <param name="culture">The culture</param>
        /// <returns>The object data returned by web services</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Méthode jamais appelée, ne présente aucun danger")]
        Task<object> GetObjectData(Dictionary<Identifier, string> identifiers, CultureInfo culture);

        object DeserializeData(string rawData);

        /// <summary>
        /// Send a request with an Identifier and its value then get the response
        /// </summary>
        /// <param name="id">The value</param>
        /// <param name="identifier">The identifier</param>
        /// <param name="culture">The culture</param>
        /// <returns>The response</returns>
        Task<string> SendRequest(Dictionary<Identifier, string> identifiers, CultureInfo culture);
    }
}
