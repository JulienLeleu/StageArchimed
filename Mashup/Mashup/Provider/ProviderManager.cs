//-----------------------------------------------------------------------
// <copyright file="ProviderManager.cs" company="Archimed">
//     Copyright LookFor.HK. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup.Provider
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Mashup.IO;
    using Exception;
    using Util;
    using Entity;

    /// <summary>
    /// Main plugin Manager
    /// </summary>
    public class ProviderManager
    {
        /// <summary>
        /// Singleton Instance
        /// </summary>
        private static ProviderManager instance = null;

        /// <summary>
        /// List of providers existing in the Provider folder
        /// </summary>
        private List<IProvider> providers = new List<IProvider>();

        /// <summary>
        /// List the types of the models contained in folder Model of each Services (used by ResultSetObject)
        /// </summary>
        private IEnumerable<Type> modelsTypes;

        /// <summary>
        /// Prevents a default instance of the <see cref="ProviderManager"/> class from being created.
        /// </summary>
        private ProviderManager()
        {
            this.modelsTypes = from t in Assembly.GetExecutingAssembly().GetTypes()
                        where Regex.Match(t.Namespace.ToString(), "^Mashup.Provider.Service.[A-z\\d]+.Model((.)*[A-z\\d]*)$").Success
                        select t;
            var currentAssembly = this.GetType().GetTypeInfo().Assembly;
            var types = currentAssembly.DefinedTypes.Where(t => t.ImplementedInterfaces.Any(i => i == typeof(IProvider)) && !t.IsAbstract);

            foreach (var t in types)
            {
                this.providers.Add((IProvider)Activator.CreateInstance(t));
            }
        }

        /// <summary>
        /// Returns the single instance of <see cref="ProviderManager"/>
        /// </summary>
        /// <returns>Instance of <see cref="ProviderManager"/></returns>
        public static ProviderManager GetInstance()
        {
            return instance ?? (instance = new ProviderManager());
        }

        /// <summary>
        /// Sends a search object to all providers
        /// </summary>
        /// <param name="sendObject">The send object</param>
        /// <returns>The response object</returns>
        public async Task<ResultSetObject> GetObjectsDatasFromProviders(SendObject sendObject)
        {
            if (sendObject == null)
            {
                throw new NullReferenceException("L'objet de la classe " + sendObject.GetType() + " est null");
            }

            try
            {
                Media mediaType = GetMediaTypeFromString(sendObject.MediaType);
                Identifier identifierKey = GetIdentifierFromString(sendObject.IdentifierKey);
                CultureInfo culture = GetCultureInfoFromString(sendObject.FavoriteLanguage);
                Dictionary<string, Task<object>> tasksProvider = new Dictionary<string, Task<object>>();
                Dictionary<string, object> responsesProvider = new Dictionary<string, object>();

                foreach (IProvider p in this.GetProviders().Where(p => p.MediaType == mediaType))
                {
                    tasksProvider.Add(p.GetType().ToString(), p.GetObjectData(sendObject.IdentifierValue, identifierKey, culture));
                }

                await Task.WhenAll(tasksProvider.Values);

                foreach (string key in tasksProvider.Keys)
                {
                    if (tasksProvider[key].IsCompleted && tasksProvider[key].Result != null)
                    {
                        responsesProvider.Add(key, tasksProvider[key].Result);
                    }
                }

                return new ResultSetObject(responsesProvider);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Dictionary<string, string>> GetRawsDatasFromProviders(SendObject sendObject)
        {
            if (sendObject == null)
            {
                throw new NullReferenceException("L'objet de la classe " + sendObject.GetType() + " est null");
            }

            try
            {
                Media mediaType = GetMediaTypeFromString(sendObject.MediaType);
                Identifier identifierKey = GetIdentifierFromString(sendObject.IdentifierKey);
                CultureInfo culture = GetCultureInfoFromString(sendObject.FavoriteLanguage);
                Dictionary<string, Task<string>> tasksProvider = new Dictionary<string, Task<string>>();
                Dictionary<string, string> responsesProvider = new Dictionary<string, string>();

                foreach (IProvider p in this.GetProviders().Where(p => p.MediaType == mediaType))
                {
                    tasksProvider.Add(p.GetType().ToString(), p.GetRawData(sendObject.IdentifierValue, identifierKey, culture));
                }

                await Task.WhenAll(tasksProvider.Values);

                foreach (string key in tasksProvider.Keys)
                {
                    if (tasksProvider[key].IsCompleted && tasksProvider[key].Result != null)
                    {
                        responsesProvider.Add(key, tasksProvider[key].Result);
                    }
                }

                return responsesProvider;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Dictionary<string, string>> GetRawsDataFromProvider(SendDBObject sendDBObject)
        {
            List<Task<Dictionary<string, string>>> tasks;
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sendDBObject.Ean != null)
            {
                dict = dict.Concat(GetRawsDatasFromProviders(new SendObject(sendDBObject.MediaType, "Ean", sendDBObject.Ean, sendDBObject.Language)).Result).ToDictionary(e => e.Key, e => e.Value);
            }
            return null;
        }

        /// <summary>
        /// Gets identifier from a string
        /// </summary>
        /// <param name="identifier">The string identifier</param>
        /// <returns>The identifier from Enum</returns>
        internal static Media GetMediaTypeFromString(string mediaType)
        {
            foreach (Media i in Enum.GetValues(typeof(Media)))
            {
                if (mediaType.Equals(i.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            throw new IdentifierUnknownException("This media \"" + mediaType + "\" doesn't match any Enum medias");
        }

        /// <summary>
        /// Gets identifier from a string
        /// </summary>
        /// <param name="identifier">The string identifier</param>
        /// <returns>The identifier from Enum</returns>
        internal static Identifier GetIdentifierFromString(string identifier)
        {
            foreach (Identifier i in Enum.GetValues(typeof(Identifier)))
            {
                if (identifier.Equals(i.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            throw new IdentifierUnknownException("This identifier \"" + identifier + "\" doesn't match any Enum identifiers");
        }

        /// <summary>
        /// Gets the culture from an ISO format country
        /// </summary>
        /// <param name="flag">The ISO format country</param>
        /// <returns>The culture info corresponding</returns>
        internal static CultureInfo GetCultureInfoFromString(string flag)
        {
            foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.NeutralCultures))
            {
                if (flag.Equals(ci.TwoLetterISOLanguageName.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return ci;
                }
            }

            throw new CultureUnknownException("The culture \"" + flag + "\" does not exist");
        }

        /// <summary>
        /// Gets all the providers
        /// </summary>
        /// <returns>List of providers</returns>
        internal IEnumerable<IProvider> GetProviders()
        {
            return this.providers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal IProvider GetProviderByName(string name)
        {
            return this.providers.FirstOrDefault(p => p.GetType().ToString().Equals(name));
        }

        /// <summary>
        /// Gets all the providers
        /// </summary>
        /// <returns>List of providers</returns>
        internal IEnumerable<Type> GetModelsTypes()
        {
            return this.modelsTypes;
        }
    }
}