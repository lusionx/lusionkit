﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Reflection;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using System.Linq.Dynamic;

namespace Alx.ORM.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class ObjectContext : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionStr"></param>
        public ObjectContext(string connectionStr)
            : this(connectionStr, System.Configuration.ConfigurationManager.AppSettings["DbFactory"])
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionStr">连接字符串</param>
        /// <param name="factoryStr">工厂字符串 eg:Oracle.DataAccess, Oracle.DataAccess.Client.OracleClientFactory,Oracle</param>
        public ObjectContext(string connectionStr, string factoryStr)
        {
            var s = factoryStr.Split(',').Select(a => a.Trim()).ToList();
            var ass = Assembly.Load(s[0]);
            Provider = ass.CreateInstance(s[1]) as DbProviderFactory;
            Connection = Provider.CreateConnection();
            Connection.ConnectionString = connectionStr;
            ass = Assembly.GetExecutingAssembly();
            Special = ass.CreateInstance(ass.FullName.Split(',')[0] + ".Sp" + s[2]) as ISpecial;
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual string ParamPerFix
        {
            get
            {
                return Special.ParamPerFix;
            }
        }

        private DbConnection Connection;
        private DbProviderFactory Provider;
        private ISpecial Special;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(TableBase model)
        {
            var tableType = model.GetType();
            var tableAttr = AttrCache.Get(tableType);
            var cmd = Provider.CreateCommand();
            cmd.CommandText = SqlInsert(tableAttr);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = Connection;

            DbParameter par = null;
            foreach (var column in tableAttr.Columns)
            {
                par = Provider.CreateParameter();
                par.Direction = ParameterDirection.Input;
                par.ParameterName = ParamPerFix + column.Name;
                par.Value = Special.FixValue(column.GetValue(model));
                par.DbType = column.DbType;
                cmd.Parameters.Add(par);
            }
            try
            {
                Connection.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (DbException ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }

        }

        private string SqlInsert(TabelAttribute tableAttr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into  ");
            sb.Append(tableAttr.Name);
            sb.Append(" ( ");
            foreach (var a in tableAttr.Columns)
            {
                sb.Append(a.Name);
                sb.Append(", ");
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append(" ) values ( ");
            foreach (var a in tableAttr.Columns)
            {
                sb.Append(ParamPerFix);
                sb.Append(a.Name);
                sb.Append(", ");
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append(" ) ");
            return sb.ToString();
        }

        public int Delete(TableBase model)
        {
            var tableType = model.GetType();
            var tableAttr = AttrCache.Get(tableType);
            var cmd = Provider.CreateCommand();
            cmd.CommandText = SqlUpdate(tableAttr);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = Connection;
            DbParameter par = null;
            var pk = tableAttr.Columns.Single(c => c.IsPrimary);
            par = Provider.CreateParameter();
            par.Direction = ParameterDirection.Input;
            par.ParameterName = ParamPerFix + pk.Name;
            par.DbType = pk.DbType;
            par.Value = Special.FixValue(pk.GetValue(model));
            cmd.Parameters.Add(par);
            try
            {
                Connection.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (DbException ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public int Update(TableBase model)
        {
            var tableType = model.GetType();
            var tableAttr = AttrCache.Get(tableType);
            var cmd = Provider.CreateCommand();
            cmd.CommandText = SqlUpdate(tableAttr);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = Connection;
            DbParameter par = null;
            foreach (var column in tableAttr.Columns.Where(c => c.IsPrimary == false))
            {
                par = Provider.CreateParameter();
                par.Direction = ParameterDirection.Input;
                par.ParameterName = ParamPerFix + column.Name;
                par.DbType = column.DbType;
                par.Value = Special.FixValue(column.GetValue(model));
                cmd.Parameters.Add(par);
            }
            var pk = tableAttr.Columns.Single(c => c.IsPrimary);
            par = Provider.CreateParameter();
            par.Direction = ParameterDirection.Input;
            par.ParameterName = ParamPerFix + pk.Name;
            par.DbType = pk.DbType;
            par.Value = Special.FixValue(pk.GetValue(model));
            cmd.Parameters.Add(par);
            try
            {
                Connection.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (DbException ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }

        }

        private string SqlUpdate(TabelAttribute tableAttr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("update ");
            sb.Append(tableAttr.Name);
            sb.Append(" set ");
            foreach (var a in tableAttr.Columns.Where(c => c.IsPrimary == false))
            {
                sb.Append(a.Name);
                sb.Append(" = ");
                sb.Append(ParamPerFix);
                sb.Append(a.Name);
                sb.Append(", ");
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append(" where ");
            var pk = tableAttr.Columns.Single(c => c.IsPrimary).Name;
            sb.Append(pk);
            sb.Append(" = ");
            sb.Append(ParamPerFix);
            sb.Append(pk);
            return sb.ToString();
        }

        private string SqlDelete(TabelAttribute tableAttr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("delete from ");
            sb.Append(tableAttr.Name);
            sb.Append(" where ");
            var pk = tableAttr.Columns.Single(c => c.IsPrimary).Name;
            sb.Append(pk);
            sb.Append(" = ");
            sb.Append(ParamPerFix);
            sb.Append(pk);
            return sb.ToString();
        }

        public IEnumerable<Model> Query<Model>(Func<Model, bool> predicate) where Model : TableBase, new()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Model> Query<Model>(string where, IEnumerable<object> pars) where Model : TableBase, new()
        {
            return Query<Model>(where, pars, 0, -1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="where">name = ? and age = ?</param>
        /// <param name="pars">Parameter.DbType 全按照字符串处理,请自行转换好</param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public IEnumerable<Model> Query<Model>(string where, IEnumerable<object> pars, int skip, int take) where Model : TableBase, new()
        {
            var tableType = typeof(Model);
            var tableAttr = AttrCache.Get(tableType);
            var parNames = new List<string>();
            var cmd = Provider.CreateCommand();
            cmd.CommandText = Special.SqlSelect(tableAttr, where, parNames, skip, take);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = Connection;

            DbParameter par = null;
            parNames.Action((name, i) =>
            {
                par = Provider.CreateParameter();
                par.Direction = ParameterDirection.Input;
                par.ParameterName = name;
                par.Value = Special.FixValue(pars.ElementAt(i));
                cmd.Parameters.Add(par);
            });
            try
            {
                Connection.Open();
                var reader = cmd.ExecuteReader();
                return ReaderBind<Model>(tableAttr, reader);
            }
            catch (DbException ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        /// <summary>
        /// 自定义查询
        /// </summary>
        /// <param name="sql">select a,b from tt where a= ? and b = ?</param>
        /// <param name="pars"></param>
        /// <param name="par"></param>
        /// <returns></returns>
        public DataSet Query(string sql, IEnumerable<object> pars)
        {
            var cmd = Provider.CreateCommand();
            var parNames = new List<string>();
            var parreg = new Regex(@"\?");
            sql = parreg.Replace(sql, delegate(Match match)
            {
                var s = ParamPerFix + "par" + match.Index;
                parNames.Add(s);
                return s;
            });
            cmd.CommandText = sql;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = Connection;
            DbParameter par = null;

            //测试 是否 为定义的DBtype new {new {dbtype.xxx  vvvv},...}
            Func<object, bool> f_istv = arr1 =>
                {
                    if (arr1 is IEnumerable)
                    {
                        var arr = arr1 as IEnumerable;
                        if (Enumerable_my.ToList(arr).Count == 2)
                        {
                            if (Enumerable_my.ToList(arr)[0] is DbType)
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                };
            parNames.Action((name, i) =>
            {
                par = Provider.CreateParameter();
                par.Direction = ParameterDirection.Input;
                par.ParameterName = name;
                if (f_istv(pars.ElementAt(i)))
                {
                    par.DbType = (DbType)Enumerable_my.ToList(pars.ElementAt(i) as IEnumerable)[0];
                    par.Value = Special.FixValue(Enumerable_my.ToList(pars.ElementAt(i) as IEnumerable)[1]);
                }
                else
                {
                    par.Value = Special.FixValue(pars.ElementAt(i));
                }
                cmd.Parameters.Add(par);
            });
            var ada = Provider.CreateDataAdapter();
            ada.SelectCommand = cmd;
            var ds = new DataSet();
            ada.Fill(ds);
            return ds;
        }

        public IEnumerable<Model> Query<Model>() where Model : TableBase, new()
        {
            return Query<Model>(null, null, 0, -1);
        }

        private IEnumerable<Model> ReaderBind<Model>(TabelAttribute tableAttr, DbDataReader reader) where Model : TableBase, new()
        {
            var result = new List<Model>();
            while (reader.Read())
            {
                var model = new Model();
                foreach (var column in tableAttr.Columns)
                {
                    var vv = reader[column.Name];
                    if (vv is DBNull)
                    {
                        //column.SetValue(model, column.DefaultValue);
                    }
                    else
                    {
                        var method = column.Property.GetSetMethod();
                        var v2 = Special.ChangeType(vv, column.Property.PropertyType);
                        column.SetValue(model, v2);
                    }
                }
                result.Add(model);
            }
            reader.Close();
            return result;
        }


        public void Dispose()
        {
            if (Connection != null && Connection.State == ConnectionState.Open)
            {
                Connection.Close();
            }
        }
    }
}
