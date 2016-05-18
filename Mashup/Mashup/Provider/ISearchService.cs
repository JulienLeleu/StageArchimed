//-----------------------------------------------------------------------
// <copyright file="ISearchService.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup.Provider
{
    using System.ServiceModel;
    using System.ServiceModel.Web;
    using Archimed.ServiceModel.Web;
    using Mashup.IO;
    using System.Collections.Generic;
    /// <summary>
    /// Interface of search web service
    /// </summary>
    [ServiceContract]
    public interface ISearchService
    {
        /// <summary>
        /// Search method
        /// </summary>
        /// <param name="sender">The send object</param>
        /// <returns>Result set object</returns>
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        InstanceServiceResponse<ResultSetObject> Search(SendObject sender);

        /// <summary>
        /// Search method
        /// </summary>
        /// <returns>The identifiers</returns>
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        InstanceServiceResponse<string[]> GetIdentifiers();

        /// <summary>
        /// Search method
        /// </summary>
        /// <returns>The providers</returns>
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        InstanceServiceResponse<string[]> GetProviders();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        InstanceServiceResponse<string[]> GetMediaTypes();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        InstanceServiceResponse<string[]> GetMethodTypes();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        InstanceServiceResponse<Dictionary<string, string>> GetDict();
    }
}
