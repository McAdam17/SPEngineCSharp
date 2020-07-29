namespace SPEngineCSharp
{
    public static class ConnectionDB
    {
        /// <summary>
        /// Is the default string connection
        /// </summary>
        public static string DefaultSringConnection { get; set; }
        /// <summary>
        /// Get the default connection, that was before configured
        /// </summary>
        /// <returns>Defalult connection</returns>
        public static System.Data.SqlClient.SqlConnection Conection()
        {
            return new System.Data.SqlClient.SqlConnection(DefaultSringConnection);
        }
        /// <summary>
        /// Get a new connection, that is now configured
        /// </summary>
        /// <param name="conectionString">Is a new String connection</param>
        /// <returns>A new connection based on conectionString</returns>
        public static System.Data.SqlClient.SqlConnection Conection(string conectionString)
        {
            return new System.Data.SqlClient.SqlConnection(conectionString);
        }
    }
}