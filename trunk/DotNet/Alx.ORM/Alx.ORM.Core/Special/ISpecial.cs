using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alx.ORM.Core
{
    internal interface ISpecial
    {
        string ParamPerFix { get; }

        /// <summary>
        /// 提取类属性值,经过此方法转换,传给DbParameter.Value
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        object FixValue(object val);

        /// <summary>
        /// 单表查询的时候,拼接select
        /// </summary>
        /// <param name="tableAttr"></param>
        /// <param name="where"></param>
        /// <param name="parNames"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        string SqlSelect(TabelAttribute tableAttr, string where, List<string> parNames, int skip, int take);

        object ChangeType(object value, Type conversionType);
    }

}
