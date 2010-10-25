using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace Tool
{
    public class OpenDataSource
    {

        public static DataSet FromExcel(string xlspath)
        {
            string cnstr = string.Format("Provider=Microsoft.Jet.OleDb.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\";", xlspath);

            DataSet ds = new DataSet();

            OleDbDataAdapter da = new OleDbDataAdapter(string.Empty, cnstr);

            var cn = da.SelectCommand.Connection;

            cn.Open();

            DataTable dt_sheet = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            //List<string> sheets = new List<string>();

            foreach (DataRow dr in dt_sheet.Rows)
            {
                //sheets.Add(dr[2].ToString());
                da.SelectCommand.CommandText += string.Format("select * from [{0}];", dr[2]);
            }

            cn.Close();

            da.Fill(ds);

            return ds;
        }

        public static DataSet FromAccess(string mdbpath, string tableName)
        {
            //"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\mydatabase.mdb;User Id=admin;Password=;";
            string cnstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbpath;

            DataSet ds = new DataSet();

            OleDbDataAdapter da = new OleDbDataAdapter("select * from " + tableName, cnstr);

            da.Fill(ds);

            return ds;
        }
    }
}
