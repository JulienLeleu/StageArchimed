using Mashup.Provider;
using System;
using System.Linq;
using System.Data.SqlClient;
using Mashup.Cache.Entity;
using System.Collections.Generic;
using Mashup.Provider.Util;
using System.Threading.Tasks;
using Mashup.IO;
using Mashup.Provider.Service.Deezer;
using Mashup.Provider.Service.Deezer.Model.Album;
using Mashup.Provider.Service.LastFM.Model.Album;
using Mashup.Provider.Service.LastFM;
using Mashup.Provider.Service.Spotify.Model.Album;
using Mashup.Provider.Service.Spotify;
using System.Data;

namespace Mashup.Cache
{
    class Routine
    {
        private static Routine instance = null;

        private string connectionString;

        private DatabaseManager databaseManager;

        private ProviderManager providerManager;

        public Routine()
        {
            connectionString = "Data Source=(local);Initial Catalog=STAGE;Integrated Security=SSPI;User ID=sa;Password=#botiq2016!";
            databaseManager = new DatabaseManager();
            providerManager = ProviderManager.GetInstance();
            initProviders();
        }

        public void initProviders()
        {
            ProviderManager manager = ProviderManager.GetInstance();
            foreach (IProvider provider in manager.GetProviders())
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"INSERT INTO mashup_provider (Name, MethodType) SELECT @name, @methodType WHERE NOT EXISTS (SELECT 1 FROM mashup_provider AS p WHERE Name = @name)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@name", provider.GetType().ToString());
                    cmd.Parameters.AddWithValue("@methodType", provider.MethodType.ToString());
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static Routine GetInstance()
        {
            return instance ?? (instance = new Routine());
        }

        public void process()
        {
            Console.WriteLine("Processing ...");
            processMedia();
            processAuthor();
            Console.WriteLine("Process done");
        }

        /*
            Attention, ceci est la partie "lourde" mais obligatoire du projet, âme sensible s'abstenir
        */
        private void processMedia()
        {
            Console.WriteLine("Processing media ...");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"SELECT TOP 100 m.Id, Title, a.Name as Author, Ean, Language, MediaType 
                                FROM mashup_media as m LEFT JOIN mashup_author as a ON m.Id_author = a.Id 
                                WHERE m.Status=@status";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@status", (int)Status.Waiting);

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(rdr);
                    Dictionary<int, Task<Dictionary<string, string>>> dictMediaData = new Dictionary<int, Task<Dictionary<string, string>>>();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        Dictionary<string, string> parameters = new Dictionary<string, string>();
                        if (row["Ean"] != DBNull.Value)
                            parameters.Add("Ean", (string)row["Ean"]);
                        if (row["Title"] != DBNull.Value)
                            parameters.Add("Title", (string)row["Title"]);
                        if (row["Author"] != DBNull.Value)
                            parameters.Add("Author", (string)row["Author"]);

                        Task<Dictionary<string, string>> reiceved = providerManager.GetRawsDatasFromProviders(new SendObject((string)row["MediaType"], "Media", parameters, (string)row["Language"]));
                        dictMediaData.Add((int)row["Id"], reiceved);
                    }

                    Task.WaitAll(dictMediaData.Values.ToArray());

                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (dictMediaData.ContainsKey((int)row["Id"]) && dictMediaData[(int)row["Id"]].IsCompleted && dictMediaData[(int)row["Id"]].Result != null)
                        {
                            if (row["Author"] == DBNull.Value)
                            {
                                foreach (string key in dictMediaData[(int)row["Id"]].Result.Keys)
                                {
                                    if (!string.IsNullOrEmpty(dictMediaData[(int)row["Id"]].Result[key]))
                                    {
                                        switch (key)
                                        {
                                            case "Mashup.Provider.Service.Deezer.DeezerAlbumProvider":
                                                DeezerAlbum d = (DeezerAlbum)new DeezerAlbumProvider().DeserializeData(dictMediaData[(int)row["Id"]].Result[key]);
                                                if (d != null && d.Artist != null && d.Artist.Name != null && databaseManager.getAuthorId(d.Artist.Name) == null)
                                                    databaseManager.postAuthor(d.Artist.Name);
                                                break;
                                            case "Mashup.Provider.Service.LastFM.LastFMAlbumProvider":
                                                LastFMAlbum l = (LastFMAlbum)new LastFMAlbumProvider().DeserializeData(dictMediaData[(int)row["Id"]].Result[key]);
                                                if (l != null && l.Album != null && l.Album.Artist != null && databaseManager.getAuthorId(l.Album.Artist) == null)
                                                    databaseManager.postAuthor(l.Album.Artist);
                                                break;
                                            case "Mashup.Provider.Service.Spotify.SpotifyAlbumProvider":
                                                SpotifyAlbum s = (SpotifyAlbum)new SpotifyAlbumProvider().DeserializeData(dictMediaData[(int)row["Id"]].Result[key]);
                                                if (s != null && s.Artists != null && s.Artists[0].Name != null && databaseManager.getAuthorId(s.Artists[0].Name) == null)
                                                    databaseManager.postAuthor(s.Artists[0].Name);
                                                break;
                                        }
                                    }
                                }
                            }

                            postMediaData((int)row["Id"], dictMediaData[(int)row["Id"]].Result);
                            updateMediaStatus((int)row["Id"], Status.Success);
                        }
                    }
                }
            }
            Console.WriteLine("Process media done");
        }

        private void processAuthor()
        {
            Console.WriteLine("Processing author ...");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"SELECT TOP 100 Id, Name FROM mashup_author
                                WHERE Status=@status";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@status", (int)Status.Waiting);
                Console.WriteLine("Process author done");
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(rdr);

                    Dictionary<int, Task<Dictionary<string, string>>> dictAuthorData = new Dictionary<int, Task<Dictionary<string, string>>>();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        Dictionary<string, string> parameters = new Dictionary<string, string>() { { "Author", (string)row["Name"] } };
                        Task<Dictionary<string, string>> reiceved = providerManager.GetRawsDatasFromProviders(new SendObject("Music", "Artist", parameters, "FR"));
                        dictAuthorData.Add((int)row["Id"], reiceved);
                    }

                    Task.WaitAll(dictAuthorData.Values.ToArray());

                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (dictAuthorData.ContainsKey((int)row["Id"]) && dictAuthorData[(int)row["Id"]].IsCompleted && dictAuthorData[(int)row["Id"]].Result != null)
                        {
                            postAuthorData((int)row["Id"], dictAuthorData[(int)row["Id"]].Result);
                            updateAuthorStatus((int)row["Id"], Status.Success);
                        }
                    }
                }
            }
            Console.WriteLine("Process media done");
        }

        private async Task<Dictionary<string, string>> getMediaData(string title, string author, string ean, string language, string mediaType)
        {
            
            var taskSource = new TaskCompletionSource<Dictionary <string, string>> ();
            taskSource.SetResult(null);
            return await taskSource.Task;
        }

        private void postMediaData(int id_media, Dictionary<string, string> dictProviderData)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"INSERT INTO mashup_media_data 
                                (Id_media, Id_provider, Data) 
                                SELECT @idMedia, Id, @data 
                                FROM mashup_provider 
                                WHERE Name=@provider";
                SqlCommand cmd;
                foreach (string provider in dictProviderData.Keys)
                {
                    cmd = new SqlCommand(query, con);
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@idMedia", id_media);
                    cmd.Parameters.AddWithValue("@data", dictProviderData[provider]);
                    cmd.Parameters.AddWithValue("@provider", provider);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void updateMediaStatus(int id_media, Status status)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"UPDATE mashup_media SET Status=@status WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@status", (int)status);
                cmd.Parameters.AddWithValue("@Id", id_media);
                cmd.ExecuteNonQuery();
            }
        }

        private void postAuthorData(int id_author, Dictionary<string, string> dictProviderData)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"INSERT INTO mashup_author_data 
                                (Id_author, Id_provider, Data) 
                                SELECT @idAuthor, Id, @data 
                                FROM mashup_provider 
                                WHERE Name=@provider";
                SqlCommand cmd;
                foreach (string provider in dictProviderData.Keys)
                {
                    cmd = new SqlCommand(query, con);
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@idAuthor", id_author);
                    cmd.Parameters.AddWithValue("@data", dictProviderData[provider]);
                    cmd.Parameters.AddWithValue("@provider", provider);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void updateAuthorStatus(int id_author, Status status)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"UPDATE mashup_author SET Status=@status WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@status", (int)status);
                cmd.Parameters.AddWithValue("@Id", id_author);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
