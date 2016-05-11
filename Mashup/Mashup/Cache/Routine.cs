using Mashup.IO;
using Mashup.Provider;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Mashup.Cache.Entity;

namespace Mashup.Cache
{
    class Routine
    {
        private string connectionString;

        private int delay;

        public Routine()
        {
            connectionString = "Data Source=(local);Initial Catalog=STAGE;Integrated Security=SSPI;User ID=sa;Password=#botiq2016!";
            delay = 10000;
        }

        public void process()
        {
            // While True :
            //  Requête SQL : SELECT Title, Author, Ean, Language FROM mashup_media WHERE Status <> 1
            //  Pour chaque ligne, on requête auprès du ProviderManager pour chaque information, 
            //  On insert ou on update les données reçues dans la table mashup_media_data
            //  Update la valeur de status
            //  On Commit
            // End While

            // transaction useless ?
            // traiter seulement une 100 aine de lignes à la fois
            // Boucle toutes les n secondes
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"SELECT TOP 100 Id, Title, Author, Ean, Language FROM mashup_media WHERE Status <> 1";
                SqlCommand cmd = new SqlCommand(query, con);

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    ProviderManager manager = ProviderManager.GetInstance();
                    while (rdr.Read())
                    {
                        ResultSetObject res = new ResultSetObject();
                        if (!rdr["Ean"].Equals(DBNull.Value))
                        {
                            res.Add(manager.SendAll(new SendObject("Ean", (string)rdr["Ean"])).Result.JsonDatas);
                        }
                        if (!rdr["Title"].Equals(DBNull.Value))
                        {
                            res.Add(manager.SendAll(new SendObject("Title", (string)rdr["Title"])).Result.JsonDatas);
                        }
                        if (!rdr["Author"].Equals(DBNull.Value))
                        {
                            res.Add(manager.SendAll(new SendObject("Author", (string)rdr["Author"])).Result.JsonDatas);
                        }
                        postMediaData(con, (int)rdr["Id"], res);
                        updateMediaStatus(con, (int)rdr["Id"], Status.Success);
                    }
                }
            }
        }

        private void postMediaData(SqlConnection con, int id_media, ResultSetObject res)
        {
            string query = @"INSERT INTO mashup_media_data 
                                (Id_media, Id_provider, data) 
                                SELECT Id, '@idMedia', '@data' 
                                FROM mashup_provider 
                                WHERE Name=@provider";
            SqlCommand cmd;
            foreach (string provider in res.JsonDatas.Keys)
            {
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@idMedia", id_media);
                cmd.Parameters.AddWithValue("@data", res.JsonDatas[provider]);
                cmd.Parameters.AddWithValue("@provider", provider);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        private void updateMediaStatus(SqlConnection con, int id_media, Status status)
        {
            string query = @"UPDATE mashup_media SET Status=@status";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@status", (int)status);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }
}
