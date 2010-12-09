using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAlx.Core.Models
{
    internal interface IModel<TDModel> where TDModel : new()
    {
        void InitFromEntityObject(TDModel dModel);
    }

}
