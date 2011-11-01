using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Alx.ORM.Core
{
    internal class SpOracle : ISpecial
    {
        public string ParamPerFix
        {
            get { return ":"; }
        }


        public object FixValue(object val)
        {
            if (val is Guid)
            {
                Guid g = (Guid)val;
                return g.ToByteArray();
            }
            return val;
        }


        public string SqlSelect(TabelAttribute tableAttr, string where, List<string> parNames, int skip, int take)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select ");
            foreach (var a in tableAttr.Columns)
            {
                sb.Append(a.Name);
                sb.Append(", ");
            }
            sb.Append("rownum rn ");
            sb.Append("from ");
            sb.Append(tableAttr.Name);
            if (!string.IsNullOrEmpty(where))
            {
                sb.Append(" where ");
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

            sb.Insert(0, "with pager as ( ");
            sb.Append(" ) select * from pager where rn > " + skip);
            if (take > 0)
            {
                sb.Append(" and rn <= " + (skip + take));
            }

            //sb.Append(" limit " + skip + " " + take);
            return sb.ToString();
        }

        /// <summary>
        /// oracle 类型转换 raw => byte[] => Guid
        /// </summary>
        /// <param name="value"></param>
        /// <param name="conversionType"></param>
        /// <returns></returns>
        public object ChangeType(object value, Type conversionType)
        {    
            var rels = new List<ConverRel>();
            rels.Add(new ConverRel
            {
                Original = typeof(byte[]),
                Target = typeof(Guid),
                Conver = (v1) =>
                {
                    var b = (byte[])v1;
                    return new Guid(b);
                }
            });
            var rel = rels.FirstOrDefault(a => a.Original == value.GetType() && a.Target == conversionType);
            if (rel != null)
            {
                return rel.Conver(value);
            }
            return Convert.ChangeType(value, conversionType);
        }
    }

    internal class ConverRel
    {
        public Type Original { get; set; }
        public Type Target { get; set; }
        public Func<object, object> Conver { get; set; }
    }
}
