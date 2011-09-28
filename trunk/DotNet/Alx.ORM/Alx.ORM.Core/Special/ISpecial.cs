using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


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

        /// <summary>
        /// 读取数据的时候,改变数据库类型为C# 类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="conversionType"></param>
        /// <returns></returns>
        object ChangeType(object value, Type conversionType);

        /// <summary>
        /// 根据约定 从C# 类型 推导 DbType
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //DbType GetDbType(object value);
    }

}
