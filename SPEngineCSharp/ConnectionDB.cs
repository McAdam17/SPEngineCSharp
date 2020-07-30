using System;

namespace SPEngineCSharp
{
    public static class ConnectionDB
    {
        /// <summary>
        /// Is the default string connection
        /// </summary>
        public static string StringConnection { get; set; }
        /// <summary>
        /// Is the default string connection type
        /// </summary>
        public static ConnectionType ConnectionType { set; get; }
        /// <summary>
        /// Get the default connection, that was before configured
        /// </summary>
        /// <returns>Defalult connection</returns>
        public static System.Data.IDbConnection Conection()
        {
            System.Data.IDbConnection newConnection = null;
            if (StringConnection != null)
                if (ConnectionType == ConnectionType.Msql)
                    newConnection = new System.Data.SqlClient.SqlConnection(StringConnection);
                else if (ConnectionType == ConnectionType.SqlServer)
                    newConnection = new MySql.Data.MySqlClient.MySqlConnection(StringConnection);
                else
                    throw new ConnectionTypeNotFound();
            else
                throw new StringConnectionNotDefinedException("String Connection Not Defined");
            return newConnection;
        }
    }
}