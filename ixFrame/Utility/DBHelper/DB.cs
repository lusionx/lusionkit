using System;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using Utility.DBHelper;

namespace Utility
{
    public static class DB
    {
        #region 执行一条SQL语句，如Delete ,Update ,Insert
        public const string ConnectionKey = "ConnectionKey";

        public static int ExecuteCommand(string sqlstring)
        {
            string connectionString = ConfigurationManager.AppSettings[ConnectionKey].ToString();

            return SQLHelper.ExecuteNonQuery(connectionString, CommandType.Text, sqlstring);
        }

        public static int ExecuteCommand(string sqlstring, string connectionString)
        {
            return SQLHelper.ExecuteNonQuery(connectionString, CommandType.Text, sqlstring);
        }

        public static int ExecuteCommand(string sqlstring, SqlParameter[] sqlParameter)
        {
            string connectionString = ConfigurationManager.AppSettings[ConnectionKey].ToString();

            return SQLHelper.ExecuteNonQuery(connectionString, CommandType.Text, sqlstring, sqlParameter);
        }

        public static int ExecuteCommand(string sqlstring, SqlParameter[] sqlParameter, string connectionString)
        {
            return SQLHelper.ExecuteNonQuery(connectionString, CommandType.Text, sqlstring, sqlParameter);
        }
        #endregion

        #region  执行一个存储过程
        public static int ExecuteProcedure(string procedureName)
        {
            string connectionString = ConfigurationManager.AppSettings[ConnectionKey].ToString();

            return SQLHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, procedureName);
        }

        public static int ExecuteProcedure(string procedureName, string connectionString)
        {
            return SQLHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, procedureName);
        }

        public static int ExecuteProcedure(string procedureName, SqlParameter[] sqlParameter)
        {
            string connectionString = ConfigurationManager.AppSettings[ConnectionKey].ToString();

            return SQLHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, procedureName, sqlParameter);
        }

        public static int ExecuteProcedure(string procedureName, SqlParameter[] sqlParameter, string connectionString)
        {

            return SQLHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, procedureName, sqlParameter);
        }
        #endregion

        #region 执行一条SQL语句,返回一个DataSet
        public static DataSet GetCommandDataSet(string sqlstring)
        {
            string connectionString = ConfigurationManager.AppSettings[ConnectionKey].ToString();

            return SQLHelper.ExecuteDataset(connectionString, CommandType.Text, sqlstring);
        }

        public static DataSet GetCommandDataSet(string sqlstring, string connectionString)
        {
            return SQLHelper.ExecuteDataset(connectionString, CommandType.Text, sqlstring);
        }

        public static DataSet GetCommandDataSet(string sqlstring, SqlParameter[] sqlParameter)
        {
            string connectionString = ConfigurationManager.AppSettings[ConnectionKey].ToString();

            return SQLHelper.ExecuteDataset(connectionString, CommandType.Text, sqlstring, sqlParameter);
        }

        public static DataSet GetCommandDataSet(string sqlstring, SqlParameter[] sqlParameter, string connectionString)
        {
            return SQLHelper.ExecuteDataset(connectionString, CommandType.Text, sqlstring, sqlParameter);
        }
        #endregion

        #region 执行一个存储过程,返回一个DataSet
        public static DataSet GetProcedureDataSet(string procedureName)
        {
            string connectionString = ConfigurationManager.AppSettings[ConnectionKey].ToString();

            return SQLHelper.ExecuteDataset(connectionString, procedureName, (SqlParameter[])null);
        }

        public static DataSet GetProcedureDataSet(string procedureName, string connectionString)
        {
            return SQLHelper.ExecuteDataset(connectionString, procedureName, (SqlParameter[])null);
        }

        public static DataSet GetProcedureDataSet(string procedureName, SqlParameter[] sqlParameter)
        {
            string connectionString = ConfigurationManager.AppSettings[ConnectionKey].ToString();

            return SQLHelper.ExecuteDataset(connectionString, procedureName, sqlParameter);
        }

        public static DataSet GetProcedureDataSet(string procedureName, SqlParameter[] sqlParameter, string connectionString)
        {
            return SQLHelper.ExecuteDataset(connectionString, procedureName, sqlParameter);
        }
        #endregion

        #region 执行SQL语句，返回第一行第一列的值
        public static object GetCommandScalar(string sqlstring)
        {
            string connectionString = ConfigurationManager.AppSettings[ConnectionKey].ToString();

            return SQLHelper.ExecuteScalar(connectionString, CommandType.Text, sqlstring);

        }

        public static object GetCommandScalar(string sqlstring, string connectionString)
        {
            return SQLHelper.ExecuteScalar(connectionString, CommandType.Text, sqlstring);
        }

        public static object GetCommandScalar(string sqlstring, SqlParameter[] sqlParameter)
        {
            string connectionString = ConfigurationManager.AppSettings[ConnectionKey].ToString();

            return SQLHelper.ExecuteScalar(connectionString, CommandType.Text, sqlstring, sqlParameter);

        }

        public static object GetCommandScalar(string sqlstring, SqlParameter[] sqlParameter, string connectionString)
        {
            return SQLHelper.ExecuteScalar(connectionString, CommandType.Text, sqlstring, sqlParameter);
        }
        #endregion

        #region  执行一个存储过程，返回第一行第一列的值
        public static object GetProcedureScalar(string procedureName)
        {
            string connectionString = ConfigurationManager.AppSettings[ConnectionKey].ToString();

            return SQLHelper.ExecuteScalar(connectionString, procedureName, (SqlParameter[])null);
        }

        public static object GetProcedureScalar(string procedureName, string connectionString)
        {
            return SQLHelper.ExecuteScalar(connectionString, procedureName, (SqlParameter[])null);
        }

        public static object GetProcedureScalar(string procedureName, SqlParameter[] sqlParameter)
        {
            string connectionString = ConfigurationManager.AppSettings[ConnectionKey].ToString();

            return SQLHelper.ExecuteScalar(connectionString, procedureName, sqlParameter);
        }

        public static object GetProcedureScalar(string procedureName, SqlParameter[] sqlParameter, string connectionString)
        {
            return SQLHelper.ExecuteScalar(connectionString, procedureName, sqlParameter);
        }
        #endregion

        #region 执行一个SQL语句,返回一个DataReader
        public static SqlDataReader GetCommandReader(string sqlstring)
        {
            string connectionString = ConfigurationManager.AppSettings[ConnectionKey].ToString();

            return SQLHelper.ExecuteReader(connectionString, CommandType.Text, sqlstring);
        }

        public static SqlDataReader GetCommandReader(string sqlstring, string connectionString)
        {
            return SQLHelper.ExecuteReader(connectionString, CommandType.Text, sqlstring);
        }

        public static SqlDataReader GetCommandReader(string sqlstring, SqlParameter[] sqlParameter)
        {
            string connectionString = ConfigurationManager.AppSettings[ConnectionKey].ToString();

            return SQLHelper.ExecuteReader(connectionString, CommandType.Text, sqlstring, sqlParameter);
        }

        public static SqlDataReader GetCommandReader(string sqlstring, SqlParameter[] sqlParameter, string connectionString)
        {
            return SQLHelper.ExecuteReader(connectionString, CommandType.Text, sqlstring, sqlParameter);
        }
        #endregion

        #region 执行一个存储过程,返回一个DataReader
        public static SqlDataReader GetProcedureReader(string procedureName)
        {
            string connectionString = ConfigurationManager.AppSettings[ConnectionKey].ToString();

            return SQLHelper.ExecuteReader(connectionString, procedureName, (SqlParameter[])null);
        }

        public static SqlDataReader GetProcedureReader(string procedureName, string connectionString)
        {
            return SQLHelper.ExecuteReader(connectionString, procedureName, (SqlParameter[])null);
        }

        public static SqlDataReader GetProcedureReader(string procedureName, SqlParameter[] sqlParameter)
        {
            string connectionString = ConfigurationManager.AppSettings[ConnectionKey].ToString();

            return SQLHelper.ExecuteReader(connectionString, procedureName, sqlParameter);
        }

        public static SqlDataReader GetProcedureReader(string procedureName, SqlParameter[] sqlParameter, string connectionString)
        {
            return SQLHelper.ExecuteReader(connectionString, procedureName, sqlParameter);
        }
        #endregion

        #region  执行一个SQL语句,返回第一条记录的DataRow对象
        public static DataRow GetCommandRow(string sqlstring)
        {
            string connectionString = ConfigurationManager.AppSettings[ConnectionKey].ToString();

            DataSet dsResult = SQLHelper.ExecuteDataset(connectionString, CommandType.Text, sqlstring);

            return (dsResult.Tables[0].Rows.Count > 0) ? dsResult.Tables[0].Rows[0] : (DataRow)null;
        }

        public static DataRow GetCommandRow(string sqlstring, string connectionString)
        {
            DataSet dsResult = SQLHelper.ExecuteDataset(connectionString, CommandType.Text, sqlstring);

            return (dsResult.Tables[0].Rows.Count > 0) ? dsResult.Tables[0].Rows[0] : (DataRow)null;
        }

        public static DataRow GetCommandRow(string sqlstring, SqlParameter[] sqlParameter)
        {
            string connectionString = ConfigurationManager.AppSettings[ConnectionKey].ToString();

            DataSet dsResult = SQLHelper.ExecuteDataset(connectionString, CommandType.Text, sqlstring, sqlParameter);

            return (dsResult.Tables[0].Rows.Count > 0) ? dsResult.Tables[0].Rows[0] : (DataRow)null;
        }

        public static DataRow GetCommandRow(string sqlstring, SqlParameter[] sqlParameter, string connectionString)
        {
            DataSet dsResult = SQLHelper.ExecuteDataset(connectionString, CommandType.Text, sqlstring, sqlParameter);

            return (dsResult.Tables[0].Rows.Count > 0) ? dsResult.Tables[0].Rows[0] : (DataRow)null;
        }
        #endregion
    }
}
