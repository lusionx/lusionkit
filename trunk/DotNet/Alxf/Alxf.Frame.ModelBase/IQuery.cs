using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alxf.Frame.ModelBase
{
    public interface IQuery<TModel> where TModel : class, new()
    {
        IQueryable<TModel> Query();
    }
}
