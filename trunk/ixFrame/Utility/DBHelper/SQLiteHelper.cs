
using System;
using System.Data.SQLite;
using System.Data;


namespace Utility.DBHelper
{
    /// <summary>
    /// 啊啦星的SQLiteHelper类
    /// 随意转载和修改
    /// </summary>
    public sealed class SQLiteHelper
    {
        public SQLiteHelper() { }

        private static void PrepareCommand(SQLiteCommand command, SQLiteConnection connection, string commandText, SQLiteParameter[] commandParameters, out bool mustCloseConnection)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }

            // Associate the connection with the command
            command.Connection = connection;
            command.CommandText = commandText;

            // Attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }

        private static void AttachParameters(SQLiteCommand command, SQLiteParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (SQLiteParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }

        private static object Execute(SQLiteConnection connection, string commandText, SQLiteParameter[] commandParameters, ExecuteType execType)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            // Create a command and prepare it for execution
            SQLiteCommand cmd = new SQLiteCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, commandText, commandParameters, out mustCloseConnection);

            // Finally, execute the command
            object retval = null;
            switch (execType)
            {
                case ExecuteType.ExecuteNonQuery:
                    retval = cmd.ExecuteNonQuery();
                    break;
                case ExecuteType.ExecuteReader:
                    retval = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    mustCloseConnection = false;
                    break;
                case ExecuteType.ExecuteScalar:
                    retval = cmd.ExecuteScalar();
                    break;
                case ExecuteType.ExecuteDataSet:
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        retval = ds;
                    }
                    break;
            }

            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();
            if (mustCloseConnection)
            {
                connection.Close();
            }
            return retval;
        }

        public static int ExecuteNonQuery(string connectionString, string commandText, SQLiteParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }

            // Create & open a SqlConnection, and dispose of it after we are done
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                // Call the overload that takes a connection in place of the connection string
                return (int)Execute(connection, commandText, commandParameters, ExecuteType.ExecuteNonQuery);
            }
        }

        public static int ExecuteNonQuery(string connectionString, string commandText)
        {
            return ExecuteNonQuery(connectionString, commandText, (SQLiteParameter[])null);
        }

        public static object ExecuteScalar(string connectionString, string commandText)
        {
            return ExecuteScalar(connectionString, commandText, (SQLiteParameter[])null);
        }

        public static object ExecuteScalar(string connectionString, string commandText, SQLiteParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }

            // Create & open a SqlConnection, and dispose of it after we are done
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                // Call the overload that takes a connection in place of the connection string
                return Execute(connection, commandText, commandParameters, ExecuteType.ExecuteScalar);
            }
        }

        public static SQLiteDataReader ExecuteReader(string connectionString, string commandText)
        {
            return ExecuteReader(connectionString, commandText, (SQLiteParameter[])null);
        }

        public static SQLiteDataReader ExecuteReader(string connectionString, string commandText, SQLiteParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }

            // Create & open a SqlConnection, and dispose of it after we are done
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                // Call the overload that takes a connection in place of the connection string
                return Execute(connection, commandText, commandParameters, ExecuteType.ExecuteReader) as SQLiteDataReader;
            }
        }

        public static DataSet ExecuteDataset(string connectionString, string commandText)
        {
            return ExecuteDataset(connectionString, commandText, (SQLiteParameter[])null);
        }

        public static DataSet ExecuteDataset(string connectionString, string commandText, SQLiteParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }

            // Create & open a SqlConnection, and dispose of it after we are done
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                // Call the overload that takes a connection in place of the connection string
                return Execute(connection, commandText, commandParameters, ExecuteType.ExecuteDataSet) as DataSet;
            }
        }
    }

    internal enum ExecuteType
    {
        ExecuteNonQuery,
        ExecuteReader,
        ExecuteScalar,
        ExecuteDataSet
    }
}
