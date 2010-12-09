using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Reflection;

namespace Alxf.Frame.ModelBase
{
    public abstract class ResponsibilityBase<TModel, TDataContext> :
        IResponsibility<TModel>
        where TModel : class, new()
        where TDataContext : DataContext, new()
    {
        #region IResponsibility<TModel> 成员

        public abstract void Insert(TModel model);

        public abstract void Update(TModel model);

        public abstract void Delete(TModel model);

        public abstract IQueryable<TModel> Query();

        public virtual bool SaveChanges()
        {
            try
            {
                DB.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        private TDataContext _db;

        public TDataContext DB
        {
            get
            {
                if (_db == null)
                {
                    _db = new TDataContext();
                }
                return _db;
            }
        }

        protected void CopyObjectProperty(object tSource, object tDestination)
        {
            //获得所有property的信息
            PropertyInfo[] properties = tSource.GetType().GetProperties();

            foreach (PropertyInfo p in properties)
            {
                p.SetValue(tDestination, p.GetValue(tSource, null), null);//设置tDestination的属性值              
            }
        }
    }
}
