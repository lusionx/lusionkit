using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alxf.Frame.DataBase
{
    /// <summary>
    /// 数据模型工具
    /// </summary>
    public class ModelTool
    {
        /// <summary>
        /// 把模型中字段赋值为默认值为String=String.Empty,int=-1,Guid=Guid.Empty
        /// </summary>
        /// <param name="dm"></param>
        public static void SetDefault(object dm)
        {
            foreach (var p in dm.GetType().GetProperties(System.Reflection.BindingFlags.Public))
            {
                if (p.PropertyType == typeof(string))
                {
                    p.SetValue(dm, String.Empty, null);
                }
                else if (p.PropertyType == typeof(int))
                {
                    p.SetValue(dm, -1, null);
                }
                else
                {
                    p.SetValue(dm, Guid.Empty, null);
                }
            }
        }

        /// <summary>
        /// 截取超出数据库规定的字符串长度,
        /// 此操作不涉及业务逻辑,
        /// 目的是把截断二进制字符串的错误屏蔽掉
        /// </summary>
        /// <param name="dm"></param>
        public static void ValidateStringLength(object dm)
        {
            foreach (var p in dm.GetType().GetProperties(System.Reflection.BindingFlags.Public))
            {
                if (p.PropertyType == typeof(string))
                {
                    var attrs = p.GetCustomAttributes(typeof(System.Data.Linq.Mapping.ColumnAttribute), false);
                    var dbtype = (attrs[0] as System.Data.Linq.Mapping.ColumnAttribute).DbType;
                    int st = dbtype.IndexOf('(');
                    int ed = dbtype.IndexOf(')');
                    var length = Convert.ToInt32(dbtype.Substring(st + 1, ed - st));
                    var _str = p.GetValue(dm, null).ToString();
                    var str = _str.Length > length ? _str.Substring(0, length) : _str;
                    p.SetValue(dm, str, null);
                }
            }
        }
    }
}
