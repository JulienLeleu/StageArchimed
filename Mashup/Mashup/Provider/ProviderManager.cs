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
        /// <param name="sender">The send object</param>
        /// <returns>The response object</returns>
        public async Task<ResultSetObject> SendAll(SendObject sender)
        {
            if (sender == null)
            {
                throw new NullReferenceException("L'objet de la classe " + sender.GetType() + " est null");
            }

            try
            {
                Identifier identifierKey = GetIdentifierFromString(sender.IdentifierKey);
                CultureInfo culture = GetCultureInfoFromString(sender.FavoriteLanguage);
                Dictionary<string, Task<object>> tasksProvider = new Dictionary<string, Task<object>>();
                Dictionary<string, object> responsesProvider = new Dictionary<string, object>();

                foreach (IProvider p in this.GetProviders())
                {
                    tasksProvider.Add(p.GetType().ToString(), p.GetObjectData(sender.IdentifierValue, identifierKey, culture));
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
            catch (IdentifierUnknownException)
            {
                throw;
            }
            catch (CultureUnknownException)
            {
                throw;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        // public Dictionary<string, string> GetRawsDatasFromProviders(SendOject sendObject) {
        // 
        // }

        // public ResultSetObject GetObjectsDatasFromProviders(SendObject sendObject) {
        //
        // }
        
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