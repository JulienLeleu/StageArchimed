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
        public ResultSetObject Search(SearchObject searchObject)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);
                    Tuple<int?, int?> data = getMediaId(con, transaction, searchObject);
                    int? idMedia = (data == null ? null : data.Item1);
                    int? status = (data == null ? null : data.Item2);
                    if (idMedia != null)
                    {
                        if (status == 1)
                        {
                            return getMediaData(con, transaction, (int)idMedia);
                        }
                        return null;
                    }
                    postMedia(con, transaction, searchObject);
                    scope.Complete();
                    return null;
                }
            }
        }

        /// <summary>
        /// Get the Id in database of a media
        /// </summary>
        /// <param name="searchObject">The search object</param>
        /// <returns>The Id with Status</returns>
        public Tuple<int?, int?> getMediaId(SqlConnection con, SqlTransaction trans, SearchObject searchObject)
        {
            string query = @"SELECT TOP 1 Id, Status 
                            FROM dbo.mashup_media 
                            WHERE (@title IS NULL OR Title=@title) 
                            AND (@author IS NULL OR Author=@author) 
                            AND (@ean IS NULL OR Ean=@ean) 
                            AND (@language IS NULL OR Language=@language)";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Transaction = trans;
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@title", (object)searchObject.Title ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@author", (object)searchObject.Author ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ean", (object)searchObject.Ean ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@language", (object)searchObject.Language ?? DBNull.Value);

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    return new Tuple<int?, int?>((int?)rdr["Id"], (int?)rdr["Status"]);
                }
                return null;
            }
        }

        /// <summary>
        /// Get the media datas
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns>The result set</returns>
        public ResultSetObject getMediaData(SqlConnection con, SqlTransaction trans, int id)
        {
            string query = @"SELECT p.Name as Provider, d.Data, m.Author 
                            FROM mashup_media as m 
                            INNER JOIN mashup_media_data as d ON m.Id = d.Id_media 
                            INNER JOIN mashup_provider as p ON d.Id_provider = p.Id 
                            WHERE m.Id=@Id ;";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Transaction = trans;
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
                        Console.WriteLine((string)rdr["Author"]);
                        results.Add((string)rdr["Provider"], provider.GetObjectData((string)rdr["Data"]));
                    }
                }
                return new ResultSetObject(results);
            }
        }

        /// <summary>
        /// Post in database a media 
        /// </summary>
        /// <param name="searchObject">The media</param>
        public void postMedia(SqlConnection con, SqlTransaction trans, SearchObject searchObject)
        {
            try
            {
                string query = @"INSERT INTO dbo.mashup_media 
                                (Title, Author, Ean, Language, Created_on, Updated_on, Status) 
                                VALUES 
                                (@title, @author, @ean, @language, GETDATE(), GETDATE(), @status)";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Transaction = trans;
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@title", (object)searchObject.Title ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@author", (object)searchObject.Author ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ean", (object)searchObject.Ean ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@language", (object)searchObject.Language ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@status", (int)Status.Waiting);
                cmd.ExecuteNonQuery();
                trans.Commit();
            } catch (Exception)
            {
                try
                {
                    trans.Rollback();
                }
                catch (SqlException)
                {
                    throw;
                }
                throw;
            }
        }
    }
}
