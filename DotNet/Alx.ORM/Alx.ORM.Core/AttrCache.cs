using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace Alx.ORM.Core
{
    internal static class AttrCache
    {
        static AttrCache()
        {
            TableAttrs = new Dictionary<Type, TabelAttribute>();
        }
        private static Dictionary<Type, TabelAttribute> TableAttrs { get; set; }

        public static TabelAttribute Get(Type tableType)
        {
            if (TableAttrs.ContainsKey(tableType))
            {
                return TableAttrs[tableType];
            }
            else
            {
                var tableAttr = tableType.GetCustomAttributes(false)
                            .Single(a => a is TabelAttribute) as TabelAttribute;
                var columnsAttr = new List<ColumnAttribute>();
                tableAttr.Columns = columnsAttr;
                var tableProperties = tableType.GetProperties();
                foreach (var property in tableProperties)
                {
                    var col = property.GetCustomAttributes(false).FirstOrDefault(a => a is ColumnAttribute) as ColumnAttribute;
                    if (col == null)
                    {
                        continue;
                    }
                    col.Property = property;
                    //缓存 2个 委托
                    col.SetAct = SetDelegate(property.GetSetMethod());
                    col.GetFun = GetDelegate(property.GetGetMethod());
                    if (col.DefaultValue != null)
                    {
                        col.DefaultVal = (System.Activator.CreateInstance(col.DefaultValue) as IDefultVal).GetVal();
                    }

                    columnsAttr.Add(col);
                }
                TableAttrs.Add(tableType, tableAttr);
                return tableAttr;
            }
        }

        /// <summary>
        /// 生成Set委托
        /// </summary>
        /// <param name="method"></param>
        private static Action<object, object[]> SetDelegate(MethodInfo methodInfo)
        {

            ParameterExpression instanceParameter =
               Expression.Parameter(typeof(object), "instance");
            ParameterExpression parametersParameter =
                Expression.Parameter(typeof(object[]), "parameters");
            // build parameter list
            List<Expression> parameterExpressions = new List<Expression>();
            ParameterInfo[] paramInfos = methodInfo.GetParameters();
            for (int i = 0; i < paramInfos.Length; i++)
            {
                // (Ti)parameters[i]
                BinaryExpression valueObj = Expression.ArrayIndex(
                    parametersParameter, Expression.Constant(i));
                UnaryExpression valueCast = Expression.Convert(
                    valueObj, paramInfos[i].ParameterType);
                parameterExpressions.Add(valueCast);
            }
            // (TInstance)instance
            Expression instanceCast = Expression.Convert(instanceParameter, methodInfo.ReflectedType);

            // static invoke or ((TInstance)instance).Method
            MethodCallExpression methodCall = Expression.Call(
                instanceCast, methodInfo, parameterExpressions);

            Expression<Action<object, object[]>> lambda =
                    Expression.Lambda<Action<object, object[]>>(
                        methodCall, instanceParameter, parametersParameter);

            Action<object, object[]> execute = lambda.Compile();
            return execute;
        }

        /// <summary>
        /// 生成Get委托
        /// </summary>
        /// <param name="method"></param>
        private static Func<object, object[], object> GetDelegate(MethodInfo methodInfo)
        {

            ParameterExpression instanceParameter = Expression.Parameter(typeof(object), "instance");
            ParameterExpression parametersParameter = Expression.Parameter(typeof(object[]), "parameters");

            // (TInstance)instance
            Expression instanceCast = Expression.Convert(instanceParameter, methodInfo.ReflectedType);

            // static invoke or ((TInstance)instance).Method
            MethodCallExpression methodCall = Expression.Call(instanceCast, methodInfo);
            UnaryExpression castMethodCall = Expression.Convert(methodCall, typeof(object));
            var lambda = Expression.Lambda<Func<object, object[], object>>(castMethodCall, instanceParameter, parametersParameter);

            var execute = lambda.Compile();
            return execute;
        }


    }

}
