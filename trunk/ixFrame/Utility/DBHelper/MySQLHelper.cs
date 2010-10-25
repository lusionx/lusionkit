using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Collections;

namespace Utility.DBHelper
{
    public class MySQLHelper
    {
        public static DataRow ExecuteDataRow(string connectionString,CommandType commandType, string commandText, params MySqlParameter[] parms)
        {
            DataSet ds = ExecuteDataset(connectionString,commandType, commandText, parms);
            if (ds == null) return null;
            if (ds.Tables.Count == 0) return null;
            if (ds.Tables[0].Rows.Count == 0) return null;
            return ds.Tables[0].Rows[0];
        }
        public static DataRow ExecuteDataRow(string connectionString, string commandText, params MySqlParameter[] parms)
        {
            return ExecuteDataRow(connectionString,CommandType.Text, commandText, parms);
        }

        public static DataSet ExecuteDataset(MySqlConnection connection, string commandText)
        {
            //return MySql.Data.MySqlClient.MySqlHelper.ExecuteDataset(connection, commandText);
            return ExecuteDataset(connection, commandText, null);
        }

        public static DataSet ExecuteDataset(string connectionString, string commandText)
        {
            //return MySql.Data.MySqlClient.MySqlHelper.ExecuteDataset(connectionString, commandText);
            return ExecuteDataset(connectionString, commandText, null);
        }

        public static DataSet ExecuteDataset(MySqlConnection connection, string commandText, params MySqlParameter[] commandParameters)
        {
            //return MySql.Data.MySqlClient.MySqlHelper.ExecuteDataset(connection, commandText, commandParameters);
            return ExecuteDataset(connection, CommandType.Text, commandText, commandParameters);
        }
        public static DataSet ExecuteDataset(string connectionString, string commandText, params MySqlParameter[] commandParameters)
        {
            //return MySql.Data.MySqlClient.MySqlHelper.ExecuteDataset(connectionString, commandText, commandParameters);
            return ExecuteDataset(connectionString, CommandType.Text, commandText, commandParameters);
        }

        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            //create & open a SqlConnection, and dispose of it after we are done.
            MySqlConnection cn = new MySqlConnection(connectionString);
            DataSet ds = null;
            try
            {
                cn.Open();
                //call the overload that takes a connection in place of the connection string
                ds = ExecuteDataset(cn, commandType, commandText, commandParameters);
            }
            finally
            {
                cn.Close();
                cn.Dispose();
            }
            return ds;
        }

        public static DataSet ExecuteDataset(MySqlConnection connection, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandTimeout = connection.ConnectionTimeout;
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;

            if (commandParameters != null)
                foreach (MySqlParameter p in commandParameters)
                    cmd.Parameters.Add(p);

            //create the DataAdapter & DataSet
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            //fill the DataSet using default values for DataTable names, etc.
            da.Fill(ds);

            // detach the MySqlParameters from the command object, so they can be used again.			
            cmd.Parameters.Clear();

            //return the dataset
            return ds;	
        }

        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params MySqlParameter[] parms)
        {
            //create & open a SqlConnection, and dispose of it after we are done.
            MySqlConnection cn = new MySqlConnection(connectionString);
            int result = 0;
            try
            {
                cn.Open();
                //call the overload that takes a connection in place of the connection string
                result = ExecuteNonQuery(cn, commandType, commandText, parms);
            }
            finally
            {
                cn.Close();
                cn.Dispose();
            }
            return result;            
        }

        public static int ExecuteNonQuery(MySqlConnection connection, CommandType commandType, string commandText, params MySqlParameter[] parms)
        {
            //create a command and prepare it for execution
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandTimeout = connection.ConnectionTimeout;
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;

            if (parms != null)
                foreach (MySqlParameter p in parms)
                    cmd.Parameters.Add(p);
            if (commandText == "" || commandText==null)
            {
                return 0;
            }
            int result = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            return result;
        }

        public static int ExecuteNonQuery(MySqlConnection connection, string commandText, params MySqlParameter[] commandParameters)
        {
            //return MySql.Data.MySqlClient.MySqlHelper.ExecuteNonQuery(connection, commandText, commandParameters);
            return ExecuteNonQuery(connection, CommandType.Text, commandText, commandParameters);
        }

        public static int ExecuteNonQuery(string connectionString, string commandText, params MySqlParameter[] parms)
        {
            if (!string.IsNullOrEmpty(commandText))
            {
                //return MySql.Data.MySqlClient.MySqlHelper.ExecuteNonQuery(connectionString, commandText, parms);
                return ExecuteNonQuery(connectionString, CommandType.Text, commandText, parms);
            }
            else
            {
                return 0;
            }
        }
        public static MySqlDataReader ExecuteReader(string connectionString, string commandText)
        {
            //return MySql.Data.MySqlClient.MySqlHelper.ExecuteReader(connectionString, commandText);
            return ExecuteReader(connectionString, CommandType.Text, commandText, null);
        }
        public static MySqlDataReader ExecuteReader(string connectionString, string commandText, params MySqlParameter[] commandParameters)
        {
            //return MySql.Data.MySqlClient.MySqlHelper.ExecuteReader(connectionString, commandText, commandParameters);
            return ExecuteReader(connectionString, CommandType.Text, commandText, commandParameters);
        }

        public static MySqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            //create & open a SqlConnection
            MySqlConnection cn = new MySqlConnection(connectionString);
            MySqlDataReader reader = null;
            try
            {
                cn.Open();
                //call the private overload that takes an internally owned connection in place of the connection string
                reader = ExecuteReader(cn, commandType, commandText, commandParameters);
            }
            catch
            {
                //if we fail to return the SqlDatReader, we need to close the connection ourselves
                cn.Close();
                cn.Dispose();
            }
            return reader;
        }

        public static MySqlDataReader ExecuteReader(MySqlConnection connection, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandTimeout = connection.ConnectionTimeout;
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;

            if (commandParameters != null)
                foreach (MySqlParameter p in commandParameters)
                    cmd.Parameters.Add(p);

            //create a reader
            MySqlDataReader dr;

            // call ExecuteReader with the appropriate CommandBehavior
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);            
            
            // detach the SqlParameters from the command object, so they can be used again.
            cmd.Parameters.Clear();

            return dr;
        }

        public static object ExecuteScalar(MySqlConnection connection, string commandText)
        {
            //return MySql.Data.MySqlClient.MySqlHelper.ExecuteScalar(connection, commandText);
            return ExecuteScalar(connection, commandText, null);
        }
        public static object ExecuteScalar(string connectionString, string commandText)
        {
            //return MySql.Data.MySqlClient.MySqlHelper.ExecuteScalar(connectionString, commandText);
            return ExecuteScalar(connectionString, commandText, null);
        }
        public static object ExecuteScalar(MySqlConnection connection, string commandText, params MySqlParameter[] commandParameters)
        {
            //return MySql.Data.MySqlClient.MySqlHelper.ExecuteScalar(connection, commandText, commandParameters);
            return ExecuteScalar(connection, CommandType.Text, commandText, commandParameters);
        }
        public static object ExecuteScalar(string connectionString, string commandText, params MySqlParameter[] commandParameters)
        {
            //return MySql.Data.MySqlClient.MySqlHelper.ExecuteScalar(connectionString, commandText, commandParameters);
            return ExecuteScalar(connectionString, CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string connectionString,CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            //create & open a SqlConnection, and dispose of it after we are done.
            MySqlConnection cn = new MySqlConnection(connectionString);
            object result = null;
            try
            {
                cn.Open();
                //call the overload that takes a connection in place of the connection string
                result = ExecuteScalar(cn, commandType, commandText, commandParameters);
            }
            finally
            {
                cn.Close();
                cn.Dispose();
            }
            return result;
        }

        public static object ExecuteScalar(MySqlConnection connection, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandTimeout = connection.ConnectionTimeout;
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;

            if (commandParameters != null)
                foreach (MySqlParameter p in commandParameters)
                    cmd.Parameters.Add(p);

            object result = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return result;
        }

        //public static void UpdateDataSet(string connectionString, string commandText, DataSet ds, string tablename)
        //{
        //    MySql.Data.MySqlClient.MySqlHelper.UpdateDataSet(connectionString, commandText, ds, tablename);
        //}

        /// <summary>
        /// 执行无参存储过程
        /// </summary>
        /// <param name="name">存储过程名称</param>
        /// <returns></returns>
        public static int ExecuteProcedure(string name)
        {
            MySqlConnection cn = new MySqlConnection();
            int result = 0;
            try
            {
                cn.Open();
                //call the overload that takes a connection in place of the connection string
                result = ExecuteNonQuery(cn, CommandType.StoredProcedure, name, null);
            }
            finally
            {
                cn.Close();
                cn.Dispose();
            }
            return result;
        }
    }
}
