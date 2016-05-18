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
        private IEnumerable<System.Type> modelsTypes;

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
                Method methodType = GetMethodTypeFromString(sendObject.MethodType);
                CultureInfo culture = GetCultureInfoFromString(sendObject.Language);
                Dictionary<Identifier, string> identifiers = GetIdentifiersFromString(sendObject.Identifiers);
                Dictionary<string, Task<object>> tasksProvider = new Dictionary<string, Task<object>>();
                Dictionary<string, object> responsesProvider = new Dictionary<string, object>();

                foreach (IProvider p in this.GetProviders().Where(p => p.MediaType == mediaType && p.MethodType == methodType))
                {
                    tasksProvider.Add(p.GetType().ToString(), p.GetObjectData(identifiers, culture));
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
                Method methodType = GetMethodTypeFromString(sendObject.MethodType);
                CultureInfo culture = GetCultureInfoFromString(sendObject.Language);
                Dictionary<Identifier, string> identifiers = GetIdentifiersFromString(sendObject.Identifiers);
                Dictionary<string, Task<string>> tasksProvider = new Dictionary<string, Task<string>>();
                Dictionary<string, string> responsesProvider = new Dictionary<string, string>();

                foreach (IProvider p in this.GetProviders().Where(p => p.MediaType == mediaType && p.MethodType == methodType))
                {
                    tasksProvider.Add(p.GetType().ToString(), p.GetRawData(identifiers, culture));
                }

                await Task.WhenAll(tasksProvider.Values);

                foreach (string key in tasksProvider.Keys)
                {
                    if (tasksProvider[key].IsCompleted && !string.IsNullOrEmpty(tasksProvider[key].Result))
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

        internal static Method GetMethodTypeFromString(string methodType)
        {
            foreach (Method i in Enum.GetValues(typeof(Method)))
            {
                if (methodType.Equals(i.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            throw new IdentifierUnknownException("This method \"" + methodType + "\" doesn't match any Enum methods");
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

        internal static Dictionary<Identifier, string> GetIdentifiersFromString(Dictionary<string, string> identifiers)
        {
            Dictionary<Identifier, string> dict = new Dictionary<Identifier, string>();
            foreach (string key in identifiers.Keys)
            {
                dict.Add(GetIdentifierFromString(key), identifiers[key]);
            }
            return dict;
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
        internal IEnumerable<System.Type> GetModelsTypes()
        {
            return this.modelsTypes;
        }
    }
}