using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alx.ORM.Core;
using System.Data;

namespace Alx.ORM.Code.Oracle
{
    public partial class ProcGen : System.Web.UI.Page
    {
        private DataTable dt = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            InitRules();
            ObjectContext db = new ObjectContext(Request.Params["constr"], Request.Params["facstr"]);
            dt = db.Query(@"select argument_name, data_type, in_out 
            from user_arguments arg where arg.OBJECT_name= ? order by position",
                new List<object> { Request.Params["proc"] }).Tables[0];
            rpt_pars.DataSource = from a in dt.AsEnumerable()
                                  where GetCsType(a.Field<string>("data_type")) != null
                                  select new
                                      {
                                          argument_name = a.Field<string>("argument_name"),
                                          data_type = a.Field<string>("data_type"),
                                          in_out = a.Field<string>("in_out")
                                      };
            rpt_pars.DataBind();
        }

        /// <summary>
        /// 是否返回 DataSet
        /// </summary>
        public string VorDs
        {
            get
            {
                if (dt.AsEnumerable().Any(a => GetCsType(a.Field<string>("data_type")) == null))
                {
                    return "System.Data.DataSet";
                }
                else
                {
                    return "void";
                }
            }
        }



        public string MethodParams
        {
            get
            {
                var ps = new List<string>();

                foreach (var a in dt.AsEnumerable())
                {
                    var ss = "";
                    if (a.Field<string>("in_out") == "OUT")
                    {
                        ss += "out ";
                    }
                    if (string.IsNullOrEmpty(GetCsType(a.Field<string>("data_type"))))
                    {
                        continue;
                    }
                    ss += GetCsType(a.Field<string>("data_type"));
                    ss += " " + a.Field<string>("argument_name");
                    ps.Add(ss);
                }
                return string.Join(", ", ps.ToArray());
            }
        }

        private List<List<string>> Rules = new List<List<string>>();
        private void InitRules()
        {
            Rules.Clear();
            Rules.Add(new List<string> { "VARCHAR2", "System.String", "DbType.String" });
            Rules.Add(new List<string> { "CHAR", "System.String", "DbType.String" });
            Rules.Add(new List<string> { "DATE", "System.Date", "DbType.Date" });
            Rules.Add(new List<string> { "RAW", "System.Guid", "DbType.Binary" });
            Rules.Add(new List<string> { "NUMBER", "System.Decimal", "DbType.Decimal" });
        }


        public string GetCsType(string oracleType)
        {
            if (oracleType == "REF CURSOR")
            {
                return null;
            }
            if (Rules.Any(a => a[0] == oracleType))
            {
                return Rules.First(a => a[0] == oracleType)[1];
            }
            else
            {
                throw new Exception("没有找到对应c#类型:" + oracleType);
            }
        }

        public string GetDbType(string oracleType)
        {
            if (oracleType == "REF CURSOR")
            {
                return null;
            }
            if (Rules.Any(a => a[0] == oracleType))
            {
                return Rules.First(a => a[0] == oracleType)[1];
            }
            else
            {
                throw new Exception("没有找到对应DbType类型:" + oracleType);
            }
        }


    }
}