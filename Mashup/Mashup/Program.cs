//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Mashup.Provider.Service.Deezer;
    using Provider.Service.LastFM;
    using Provider.Service.LastFM.Model.Artist;
    using Provider.Entity;
    using Provider.Util;
    using Provider;
    using Newtonsoft.Json.Linq;
    using Entity;    /// <summary>
                     /// The program launcher
                     /// </summary>
    internal class Program
    {
        /// <summary>
        /// Number of request sent
        /// </summary>
        private const int NbRequests = 10;

        /// <summary>
        /// Launch an asynchronous method to test performances and provider services
        /// </summary>
        public async void Run()
        {
            //// Tasks who contains data received
            List<Task<string>> tasks = new List<Task<string>>();
            //// Create providers
            DeezerAlbumProvider provider = (DeezerAlbumProvider)new DeezerFactory().GetProvider(Category.Album);
            LastFMArtistProvider provider2 = (LastFMArtistProvider)new LastFMFactory().GetProvider(Category.Artist);

            //// Running NbRequest times requests
            for (int i = 0; i < NbRequests; i++)
            {
                tasks.Add(provider.GetRawData("825646864836", Identifier.Upc));
                tasks.Add(provider2.GetRawData("9c9f1380-2516-4fc9-a3e6-f9f61941d090", Identifier.Mbid));
            }

            //// We are waiting for all responses
            await Task.WhenAll(tasks);

            //// Display all results
            foreach (Task<string> t in tasks)
            {
                LastFMArtist artist = AbstractProvider.DeserializeJSon<LastFMArtist>(t.Result);
                Console.WriteLine(artist.Artist.Name);
            }
        }

        public async void Manage()
        {
            ProviderManager manager = ProviderManager.GetInstance();
            List<Task<string>> tasks = new List<Task<string>>();

            foreach (IProvider p in manager.GetProviders())
            {
                tasks.Add(p.SendRequest("", Identifier.Mbid));
                Console.WriteLine("provider : " + p);
            }

            await Task.WhenAll(tasks);

            Console.WriteLine(tasks);

            int i = 0;
            //// Display all results
            foreach (Task<string> t in tasks)
            {
                Console.WriteLine(i++ + " " + t.Result);
            }
        }

        /// <summary>
        /// Method main
        /// </summary>
        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ProviderManager manager = ProviderManager.GetInstance();
            Task<ResultSetObject> t = manager.sendAll(new SendObject("UPC", "724384960650"));
            try
            {
                ResultSetObject b = t.Result;
                Console.WriteLine(b.getJSON());
            } catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine(e);
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.ReadKey();
        }
    }
}