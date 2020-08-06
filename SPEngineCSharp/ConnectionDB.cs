using System;

namespace SPEngineCSharp
{
    public static class ConnectionDB
    {
        public enum ConnectionType
        {
            Msql = 1,
            SqlServer = 2
        }

        public enum SPType
        {
            Select = 1,
            InsertUpdateDelete = 2
        }
        /// <summary>
        /// Is the default string connection
        /// </summary>
        /// <param name="value">Value of the StringConnection</param>
        private static string stringConnection;
        public static string StringConnection
        {
            get
            {
                return stringConnection;
            }
            set
            {
                stringConnection = value;
            }
        }
        /// <summary>
        /// Is the default string connection type
        /// </summary>
        /// /// <param name="value">Value of the ConnectionType, it could be 1 for MySQL or 2 for SQLServer</param>
        private static ConnectionType cType;
        public static ConnectionType CType
        {
            set
            {
                cType = value;
            }
            get
            {
                return cType;
            }
        }
        /// <summary>
        /// Get the default connection, that was before configured
        /// </summary>
        /// <returns>Defalult connection (IDbConnection)</returns>
        public static System.Data.IDbConnection Conection()
        {
            System.Data.IDbConnection newConnection = null;
            if (StringConnection != null)
                if (CType.Equals(ConnectionType.SqlServer))
                    newConnection = new System.Data.SqlClient.SqlConnection(StringConnection);
                else if (CType.Equals(ConnectionType.Msql))
                    newConnection = new MySql.Data.MySqlClient.MySqlConnection(StringConnection);
                else
                    throw new NullReferenceException("The argument ConnType does not satisfies the type of connection");
            else
                throw new NullReferenceException("String Connection Not Defined");
            return newConnection;
        }
    }
}