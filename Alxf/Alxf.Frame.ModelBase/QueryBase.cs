using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace Alxf.Frame.ModelBase
{
    public abstract class QueryBase<TModel, TDataContext> :
         IQuery<TModel>
        where TModel : class, new()
        where TDataContext : DataContext
    {
        public abstract IQueryable<TModel> Query();
    }
}
