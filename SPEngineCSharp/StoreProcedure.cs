using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SPEngineCSharp
{
    public abstract class StoreProcedure
    {
        public string Name { get; set; }
        public System.Collections.Generic.List<Parameter> Parameters { get; set; }
        public bool Response { get; set; }
        public DataTable TableResponse { get; set; }
        /// <summary>
        /// Execute the Stored Procedure with the defalt connection
        /// This method is for insert, update and delete db operations
        /// </summary>
        /// <param name="sp">Stored Procedure</param>
        public Boolean ExecuteBool(SPEngine lst)
        {
            SqlCommand cmd;
            IDbConnection connection = ConnectionDB.Conection();
            connection.Open();
            try
            {
                cmd = new SqlCommand(lst.Name, connection as SqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (lst.Parameters != null)
                {
                    foreach (Parameter parametro in lst.Parameters)
                        if (parametro.Direction == ParameterDirection.Input)
                            cmd.Parameters.AddWithValue(parametro.Name, parametro.Value);
                        else if (parametro.Direction == ParameterDirection.Output)
                            cmd.Parameters.Add(parametro.Name, parametro.DataType, parametro.Size).Direction = ParameterDirection.Output;
                    Response = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    for (int i = 0; i < lst.Parameters.Count; i++)
                        if (lst.Parameters[i].Direction == ParameterDirection.Input)
                            lst.Parameters[i].Value = cmd.Parameters[i].Value.ToString();
                }
                else
                {
                    //Debería existir un mensaje de error
                    Response = false;
                }
            }
            catch (Exception ex)
            {
                Response = false;
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return Response;
        }

        /// <summary>
        /// Execute a consult Store Procedure with the default connection
        /// </summary>
        /// <param name="sp">Store Procedure</param>
        /// <param name="lst">Parameters List</param>
        /// <returns>The result content</returns>
        public DataTable ExecuteDataT(SPEngine lst)
        {
            DataTable tableResult = new DataTable();
            //El adapter es como una carcasa mas grande que puede contener multiples SqlCommands
            SqlCommand _Command;
            SqlDataReader reader;
            using (IDbConnection connection = ConnectionDB.Conection())
            {
                connection.Open();
                try
                {
                    _Command = new SqlCommand(lst.Name, connection as SqlConnection);
                    _Command.CommandText = lst.Name;
                    _Command.CommandType = CommandType.StoredProcedure;
                    //Si existe lista de parámetros
                    //Si no existe
                    if (lst.Parameters != null)
                    {
                        foreach (Parameter item in lst.Parameters)
                        {
                            _Command.Parameters.AddWithValue(item.Name, item.Value);
                        }
                    }
                    else
                    {
                        _Command.Parameters.Clear();
                    }
                    //El reader guarda el resultado del comando ejecutado
                    reader = _Command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        tableResult.Load(reader);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return tableResult;
        }

        public void Execute(ConnectionDB.SPType sPType, SPEngine sp)
        {
            if (sPType.Equals(ConnectionDB.SPType.InsertUpdateDelete))
            {
                Response = ExecuteBool(sp);
            }
            else if (sPType.Equals(ConnectionDB.SPType.Select))
            {
                TableResponse = ExecuteDataT(sp);
            }
        }
    }
}