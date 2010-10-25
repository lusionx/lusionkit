using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace Utility.DBHelper
{
    public class ExcelHelper
    {
        public static DataSet GetAllData(string xlspath, bool hashead)
        {
            DataSet ds = new DataSet();

            //“HDR=Yes;”指示第一行中包含列名，而不是数据，“IMEX=1;”通知驱动程序始终将“互混”数据列作为文本读取 

            string strConn = "Provider=Microsoft.Jet.OleDb.4.0;data source=" + xlspath + ";Extended Properties='Excel 8.0;HDR={0};IMEX=1'";
            if (hashead)
            {
                strConn = string.Format(strConn, "YES");
            }
            else
            {
                strConn = string.Format(strConn, "NO");
            }
            OleDbConnection objConn = new OleDbConnection(strConn);
            DataTable dt_sheet = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            objConn.Close();
            for (int i = 0; i < dt_sheet.Rows.Count; i += 2)
            {
                string sheepname = dt_sheet.Rows[i][2].ToString();
                string sql = string.Format("SELECT * FROM [{0}]", sheepname);
                OleDbDataAdapter myCommand = new OleDbDataAdapter(sql, objConn);
                myCommand.Fill(ds, sheepname);
            }
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
            return ds;
        }
    }
}
