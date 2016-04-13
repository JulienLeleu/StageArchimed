//-----------------------------------------------------------------------
// <copyright file="ProviderManager.cs" company="Archimed">
//     Copyright LookFor.HK. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup.Provider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Mashup.Provider.Util;
    using Mashup.Entity;
    using Entity;
    using Exception;
    using System.Threading.Tasks;

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
        /// List of all providers
        /// </summary>
        private Dictionary<string, IProvider> providers = new Dictionary<string, IProvider>();

        /// <summary>
        /// Prevents a default instance of the <see cref="ProviderManager"/> class from being created.
        /// </summary>
        private ProviderManager()
        {
            var currentAssembly = this.GetType().GetTypeInfo().Assembly;
            var types = currentAssembly.DefinedTypes.Where(t => t.ImplementedInterfaces.Any(i => i == typeof(IProvider)) && !t.IsAbstract);

            foreach (var t in types)
            {
                this.providers.Add(t.Name
                    , (IProvider)Activator.CreateInstance(t));
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
        /// Gets all the providers
        /// </summary>
        /// <returns>List of providers</returns>
        internal IEnumerable<IProvider> GetProviders()
        {
            return this.providers.Select(p => p.Value);
        }

        private Identifier getIdentifierFromString(string identifier)
        {
            foreach (Identifier i in Enum.GetValues(typeof(Identifier)))
            {
                if (identifier.Equals(i.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    return i;
                }
            }
            throw new IdentifierUnknownException("This identifier \"" + identifier + "\" doesn't match any Enum identifiers");
        }

        public async Task<ResultSetObject> sendAll(SendObject sender)
        {
            try
            {
                Identifier identifierKey = getIdentifierFromString(sender.IdentifierKey);
                List<Task<string>> tasks = new List<Task<string>>();
                List<string> rawsDatas = new List<string>();
                foreach (IProvider p in GetProviders())
                {
                    tasks.Add(p.GetRawData(sender.IdentifierValue, identifierKey));
                }
                await Task.WhenAll(tasks);
                foreach (Task<string> t in tasks)
                {
                    if(t.IsCompleted && t.Result != "")
                    {
                        rawsDatas.Add(t.Result);
                    }
                }
                return new ResultSetObject(rawsDatas);
            } catch (System.Exception e)
            {
                /*Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine(e);
                Console.ForegroundColor = ConsoleColor.White;*/
                throw;
            }
        }
    }
}