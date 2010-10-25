using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alxf.Frame.ModelBase
{
    public interface IB2D<DModel> where DModel : class, new()
    {
        DModel DataModel { get; set; }
    }
}
