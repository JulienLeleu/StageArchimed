//-----------------------------------------------------------------------
// <copyright file="IProvider.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace Mashup.Provider.Util
{
    using System.Threading.Tasks;
    using Mashup.Provider.Entity;

    interface IProvider
    {
        /// <summary>
        /// Build a request from an Identifier and its associated value. Use The API Key stored if you need it.
        /// </summary>
        /// <param name="id">The value</param>
        /// <param name="identifier">The identifier</param>
        /// <returns>The response server</returns>
        string RequestBuilder(string id, Identifier identifier);

        /// <summary>
        /// Gets the raw data from a request, in which you have to provide an identifier and its value.
        /// </summary>
        /// <param name="id">The value</param>
        /// <param name="identifier">The identifier</param>
        /// <returns>The raw data returned by web services</returns>
        Task<string> GetRawData(string id, Identifier identifier);

        /// <summary>
        /// Get the data as an object T from a request, in which you have to provide an identifier and its value. 
        /// </summary>
        /// <typeparam name="T">Type of object returned</typeparam>
        /// <param name="id">The value</param>
        /// <param name="identifier">The identifier</param>
        /// <returns>The object data returned by web services</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Méthode jamais appelée, ne présente aucun danger")]
        Task<T> GetObjectData<T>(string id, Identifier identifier);

        /// <summary>
        /// Send a request with an Identifier and its value then get the response
        /// </summary>
        /// <param name="id">The value</param>
        /// <param name="identifier">The identifier</param>
        /// <returns>The response</returns>
        Task<string> SendRequest(string id, Identifier identifier);
    }
}
