using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;

namespace Alx.ORM.Core
{
    public class ColumnAttribute : System.Attribute
    {
        public string Name { get; set; }
        public DbType DbType { get; set; }
        public bool IsPrimary { get; set; }
        public bool Nullable { get; set; }
        public Type DefaultValue { get; set; }

        internal object DefVal { get; set; }

        /// <summary>
        /// 本Attr修饰的属性
        /// </summary>
        internal PropertyInfo Property { get; set; }

        internal Action<object, object[]> SetAct;

        internal void SetValue(object instance, object val)
        {
            object[] v = { val };
            SetAct(instance, v);
        }

        internal Func<object, object[], object> GetFun;

        internal object GetValue(object instance)
        {
            return GetFun(instance, null);
        }

        public ColumnAttribute()
        {
            Name = string.Empty;
            IsPrimary = false;
            Nullable = false;
        }
    }
}
