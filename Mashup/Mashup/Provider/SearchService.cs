//-----------------------------------------------------------------------
// <copyright file="SearchService.cs" company="Archimed">
//     Copyright LookFor.HK. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup.Provider
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.ServiceModel.Activation;
    using System.Threading.Tasks;
    using Archimed.ServiceModel.Web;
    using Entity;
    using Mashup.IO;
    using Util;

    /// <summary>
    /// Search web service
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [KnownType(typeof(ResultSetObject))]
    public class SearchService : ISearchService
    {
        /// <summary>
        /// Make a search with a send object
        /// </summary>
        /// <param name="sender">The send object</param>
        /// <returns>The response</returns>
        public InstanceServiceResponse<ResultSetObject> Search(SendObject sender)
        {
            ProviderManager manager = ProviderManager.GetInstance();
            InstanceServiceResponse<ResultSetObject> response = new InstanceServiceResponse<ResultSetObject>();

            try
            {
                Task<ResultSetObject> t = manager.SendAll(sender);
                t.Wait();
                response.Result = t.Result;
            }
            catch (System.Exception e)
            {
                response.AddError(e.InnerException);
            }

            return response;
        }

        /// <summary>
        /// Gets the identifiers
        /// </summary>
        /// <returns>The identifiers</returns>
        public InstanceServiceResponse<string[]> GetIdentifiers()
        {
            InstanceServiceResponse<string[]> response = new InstanceServiceResponse<string[]>();
            response.Result = Enum.GetNames(typeof(Identifier));
            return response;
        }

        /// <summary>
        /// Gets all providers
        /// </summary>
        /// <returns>The providers</returns>
        public InstanceServiceResponse<string[]> GetProviders()
        {
            InstanceServiceResponse<string[]> response = new InstanceServiceResponse<string[]>();
            IEnumerable<IProvider> providers = ProviderManager.GetInstance().GetProviders();
            List<string> strProviders = new List<string>();

            foreach (IProvider i in providers)
            {
                strProviders.Add(i.GetType().ToString());
            }

            response.Result = strProviders.ToArray();
            return response;
        }
    }
}
