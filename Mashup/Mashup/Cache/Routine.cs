using Mashup.IO;
using Mashup.Provider;
using System;
using System.Data.SqlClient;
using Mashup.Cache.Entity;
using System.Collections.Generic;
using System.Linq;
using Mashup.Provider.Util;

namespace Mashup.Cache
{
    class Routine
    {
        private static Routine instance = null;

        private string connectionString;

        public Routine()
        {
            connectionString = "Data Source=(local);Initial Catalog=STAGE;Integrated Security=SSPI;User ID=sa;Password=#botiq2016!";
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
                    string query = @"INSERT INTO mashup_provider (Name) SELECT @name WHERE NOT EXISTS (SELECT 1 FROM mashup_provider AS p WHERE Name = @name)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@name", provider.GetType().ToString());
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
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"SELECT TOP 100 Id, Title, Author, Ean, Language, MediaType 
                                FROM mashup_media 
                                WHERE Status=@status";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@status", (int)Status.Waiting);

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    ProviderManager manager = ProviderManager.GetInstance();
                    Dictionary<string, string> dictProviderData = new Dictionary<string, string>();
                    // Données reçus dans Dictionary<int, Task<Dictionary<string, string>> avec Id et données reçues
                    while (rdr.Read())
                    {
                        dictProviderData.Clear();

                        if (!rdr["Ean"].Equals(DBNull.Value))
                        {
                            dictProviderData = dictProviderData.Concat(manager.GetRawsDatasFromProviders(new SendObject((string)rdr["MediaType"], "Ean", (string)rdr["Ean"], (string)rdr["Language"])).Result).ToDictionary(e => e.Key, e => e.Value);
                            Console.WriteLine("Informations from Ean reiceved.");
                        }
                        else if (!rdr["Title"].Equals(DBNull.Value))
                        {
                            dictProviderData = dictProviderData.Concat(manager.GetRawsDatasFromProviders(new SendObject((string)rdr["MediaType"], "Title", (string)rdr["Title"], (string)rdr["Language"])).Result).ToDictionary(e => e.Key, e => e.Value);
                            Console.WriteLine("Informations from Title reiceved.");
                        }
                        if (!rdr["Author"].Equals(DBNull.Value))
                        {
                            dictProviderData = dictProviderData.Concat(manager.GetRawsDatasFromProviders(new SendObject((string)rdr["MediaType"], "Author", (string)rdr["Author"], (string)rdr["Language"])).Result).ToDictionary(e => e.Key, e => e.Value);
                            Console.WriteLine("Informations from Author reiceved.");
                        }
                        postMediaData((int)rdr["Id"], dictProviderData);
                        updateMediaStatus((int)rdr["Id"], Status.Success);
                    }
                }
            }
            Console.WriteLine("Process done");
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
                string query = @"UPDATE mashup_media SET Status=@status";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@status", (int)status);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
