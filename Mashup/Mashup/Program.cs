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
    using Mashup.Provider;
    using Mashup.Provider.Service.Deezer;
    using Mashup.Provider.Service.Deezer.Model;

    /// <summary>
    /// 
    /// </summary>
    internal class Program
    {
        // Nombre de requêtes envoyées au serveur
        private const int NbRequests = 100;

        public async void Run()
        {
            // Liste de tâches, qui contiendront les données renvoyées
            List<Task<string>> tasks = new List<Task<string>>();
            // Création d'un fournisseur
            DeezerAlbumProvider provider = (DeezerAlbumProvider)new DeezerFactory().getProvider(Category.Album);

            // On lance NB_REQUETES fois la requête
            for (int i = 0; i < NbRequests; i++)
            {
                tasks.Add(provider.getRawData("825646864836", Identifier.Upc));
            }

            // On attends la fin des requêtes
            await Task.WhenAll(tasks);

            // Affichage des données reçues
            foreach (Task<string> t in tasks)
            {
                DeezerAlbum album = AbstractProvider.DeserializeJSon<DeezerAlbum>(t.Result);
                Console.WriteLine(album.Title);
            }
        }

        private static void Main()
        {
            new Program().Run();
            Console.ReadKey();
        }
    }
}