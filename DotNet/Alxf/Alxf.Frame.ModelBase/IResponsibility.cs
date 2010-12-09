using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alxf.Frame.ModelBase
{
    //主要是定义接口
    public interface IResponsibility<TModel> : IQuery<TModel> where TModel : class, new()
    {
        void Insert(TModel model);

        void Update(TModel model);

        void Delete(TModel model);

        bool SaveChanges();

    }
}
