using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace SPEngineCSharp
{
    public static class SPEngine
    {
        /// <summary>
        /// Execute the Stored Procedure with the defalt connection
        /// This method is for insert, update and delete db operations
        /// </summary>
        /// <param name="sp">Stored Procedure</param>
        public static void ExecuteSP(StoreProcedure sp)
        {
            SqlCommand cmd;
            SqlConnection connection = ConnectionDB.Conection();
            connection.Open();
            try
            {
                cmd = new SqlCommand(sp.Name, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                if (sp.Parameters != null)
                {
                    foreach (Parameter parametro in sp.Parameters)
                        if (parametro.Direction == ParameterDirection.Input)
                            cmd.Parameters.AddWithValue(parametro.Name, parametro.Value);
                        else if (parametro.Direction == ParameterDirection.Output)
                            cmd.Parameters.Add(parametro.Name, parametro.DataType, parametro.Size).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    for (int i = 0; i < sp.Parameters.Count; i++)
                        if (sp.Parameters[i].Direction == ParameterDirection.Input)
                            sp.Parameters[i].Value = cmd.Parameters[i].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// Execute the Stored Procedure with a new connection
        /// This method is for insert, update and delete db operations
        /// </summary>
        /// <param name="sp">Stored Procedure</param>
        public static void ExecuteSP(StoreProcedure sp, SqlConnection connection)
        {
            SqlCommand cmd;
            connection.Open();
            try
            {
                cmd = new SqlCommand(sp.Name, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                if (sp.Parameters != null)
                {
                    foreach (Parameter parametro in sp.Parameters)
                        if (parametro.Direction == ParameterDirection.Input)
                            cmd.Parameters.AddWithValue(parametro.Name, parametro.Value);
                        else if (parametro.Direction == ParameterDirection.Output)
                            cmd.Parameters.Add(parametro.Name, parametro.DataType, parametro.Size).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    for (int i = 0; i < sp.Parameters.Count; i++)
                        if (sp.Parameters[i].Direction == ParameterDirection.Input)
                            sp.Parameters[i].Value = cmd.Parameters[i].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Execute a consult Store Procedure with the default connection
        /// </summary>
        /// <param name="sp">Store Procedure</param>
        /// <returns>The result content</returns>
        public static DataTable ExecuteConsultSP(StoreProcedure sp)
        {
            DataTable tableResult = new DataTable();
            SqlDataAdapter adapter;

            SqlConnection connection = ConnectionDB.Conection();
            connection.Open();
            try
            {
                adapter = new SqlDataAdapter(sp.Name, connection);
                if (sp.Parameters != null)
                    foreach (Parameter parametro in sp.Parameters)
                        adapter.SelectCommand.Parameters.AddWithValue(parametro.Name, parametro.Value);
                adapter.Fill(tableResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return tableResult;
        }

        /// <summary>
        /// Execute a consult Store Procedure with a new connection
        /// </summary>
        /// <param name="sp">Store Procedure</param>
        /// <returns>The result content</returns>
        public static DataTable ExecuteConsultSP(StoreProcedure sp, SqlConnection connection)
        {
            DataTable tableResult = new DataTable();
            SqlDataAdapter adapter;

            connection.Open();
            try
            {
                adapter = new SqlDataAdapter(sp.Name, connection);
                if (sp.Parameters != null)
                    foreach (Parameter parametro in sp.Parameters)
                        adapter.SelectCommand.Parameters.AddWithValue(parametro.Name, parametro.Value);
                adapter.Fill(tableResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return tableResult;
        }
    }
}