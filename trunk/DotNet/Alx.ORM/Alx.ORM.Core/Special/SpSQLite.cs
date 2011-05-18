using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Alx.ORM.Core
{
    internal class SpSQLite : ISpecial
    {
        public string ParamPerFix
        {
            get
            {
                return "$";
            }
        }


        public object FixValue(object val)
        {
            return val;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableAttr"></param>
        /// <param name="where">name = ? and age = ?</param>
        /// <returns></returns>
        public string SqlSelect(TabelAttribute tableAttr, string where, List<string> parNames, int skip, int take)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select ");
            foreach (var a in tableAttr.Columns)
            {
                sb.Append(a.Name);
                sb.Append(", ");
            }
            sb.Append("rowid ");
            sb.Append("from ");
            sb.Append(tableAttr.Name);
            if (!string.IsNullOrEmpty(where))
            {
                sb.Append(" where ");

                //分析条件字段的类型
                string[] keywords = { " and ", " or ", " like " };
                //var columnReg = new  Regex(@" \w+ ");
                //where = columnReg.Replace(where, delegate(Match match)
                //{                    
                //    if (match.Groups[0].Value.ToLower().In(keywords))//是关键字
                //    {

                //    }

                //    return match.Groups[0].Value.ToLower();
                //});

                //替换参数
                var parreg = new Regex(@"\?");
                where = parreg.Replace(where, delegate(Match match)
                {
                    var s = ParamPerFix + "par" + match.Index;
                    parNames.Add(s);
                    return s;
                });
                sb.Append(where);
            }
            sb.Append(" limit " + skip + " " + take);
            return sb.ToString();
        }



        public object ChangeType(object value, Type conversionType)
        {
            return value;
        }
    }
}
