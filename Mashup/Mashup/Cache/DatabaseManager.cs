namespace Mashup.Cache
{
    using Entity;
    using Mashup.IO;
    using Provider;
    using Provider.Util;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Transactions;

    /// <summary>
    /// Main database manager
    /// </summary>
    class DatabaseManager
    {
        /// <summary>
        /// The connection string
        /// </summary>
        private string connectionString;
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public DatabaseManager()
        {
            connectionString = "Data Source=(local);Initial Catalog=STAGE;Integrated Security=SSPI;User ID=sa;Password=#botiq2016!";
        }

        /// <summary>
        /// Search an object, return it if found, else insert in database
        /// </summary>
        /// <param name="searchObject">The search object</param>
        /// <returns>The resultset (null if not found)</returns>
        public ResultSetObject Search(SendDBObject searchObject)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Tuple<int?, int?> data = getMediaId(searchObject);
                int? idMedia = (data == null ? null : data.Item1);
                int? status = (data == null ? null : data.Item2);
                if (idMedia != null)
                {
                    if (status == 1)
                    {
                        return getMediaData((int)idMedia);
                    }
                    return null;
                }
                postMedia(searchObject);
                scope.Complete();
                return null;
            }
        }

        /// <summary>
        /// Get the Id in database of a media
        /// </summary>
        /// <param name="searchObject">The search object</param>
        /// <returns>The Id with Status</returns>
        public Tuple<int?, int?> getMediaId(SendDBObject searchObject)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"SELECT TOP 1 Id, Status 
                            FROM dbo.mashup_media 
                            WHERE (@title IS NULL OR Title=@title) 
                            AND (@author IS NULL OR Id_author IN (SELECT Id FROM mashup_author WHERE Name=@author)) 
                            AND (@ean IS NULL OR Ean=@ean) 
                            AND (@language IS NULL OR Language=@language)
                            AND (@mediaType IS NULL OR MediaType=@mediaType)";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Prepare();
                cmd.Parameters.AddWithValue("@title", (object)searchObject.Title ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@author", (object)searchObject.Author ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ean", (object)searchObject.Ean ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@language", (object)searchObject.Language ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@mediaType", (object)searchObject.MediaType ?? DBNull.Value);

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        int v1 = (int)rdr["Id"];
                        int v2 = (int)rdr["Status"];
                        return new Tuple<int?, int?>((int?)rdr["Id"], (int?)rdr["Status"]);
                    }
                    return null;
                }
            }
        }

        /// <summary>
        /// Get the media datas
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns>The result set</returns>
        public ResultSetObject getMediaData(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                //SqlTransaction transaction = con.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);
                string query = @"SELECT p.Name as Provider, d.Data as Data, a.Name as Author 
                            FROM mashup_media as m 
                            INNER JOIN mashup_author as a ON m.Id_author = a.Id
                            INNER JOIN mashup_media_data as d ON m.Id = d.Id_media 
                            INNER JOIN mashup_provider as p ON d.Id_provider = p.Id 
                            WHERE m.Id=@Id ;";
                SqlCommand cmd = new SqlCommand(query, con);

                //cmd.Transaction = transaction;
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    ProviderManager providerManager = ProviderManager.GetInstance();
                    Dictionary<string, object> results = new Dictionary<string, object>();
                    while (rdr.Read())
                    {
                        IProvider provider = providerManager.GetProviderByName((string)rdr["Provider"]);
                        if (provider != null)
                        {
                            results.Add((string)rdr["Provider"], provider.DeserializeData((string)rdr["Data"]));
                        }
                    }
                    return new ResultSetObject(results);
                }
            }
        }

        public int? getAuthorId(string authorName)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = @"SELECT TOP 1 Id 
                            FROM dbo.mashup_author 
                            WHERE Name=@name";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Prepare();
                cmd.Parameters.AddWithValue("@name", (object)authorName ?? DBNull.Value);

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        return (int?)(rdr["Id"] == DBNull.Value ? null : rdr["Id"]);
                    }
                    return null;
                }
            }
        }

        /// <summary>
        /// Post in database a media 
        /// </summary>
        /// <param name="searchObject">The media</param>
        public void postMedia(SendDBObject searchObject)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                int? idAuthor = (string.IsNullOrEmpty(searchObject.Author) ? null : ((getAuthorId(searchObject.Author) ?? postAuthor(searchObject.Author))));
                con.Open();
                string query = @"INSERT INTO dbo.mashup_media 
                                (Title, Id_author, Ean, Language, MediaType, Created_on, Updated_on, Status) 
                                VALUES 
                                (@title, @id_author, @ean, @language, @mediaType, GETDATE(), GETDATE(), @mediaStatus);";

                // methode pour récup
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@title", (object)searchObject.Title ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@id_author", (object)idAuthor ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ean", (object)searchObject.Ean ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@language", (object)searchObject.Language ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@mediaType", (object)searchObject.MediaType ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@mediaStatus", (int)Status.Waiting);
                cmd.ExecuteNonQuery();
            }
        }

        public int? postAuthor(string authorName)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"INSERT INTO dbo.mashup_author
                                (Name, Created_on, Updated_on, Status)
                                OUTPUT INSERTED.Id
                                VALUES
                                (@authorName, GETDATE(), GETDATE(), @authorStatus);";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@authorName", (object)authorName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@authorStatus", (int)Status.Waiting);
                return (int?)cmd.ExecuteScalar();
            }
        }
    }
}
