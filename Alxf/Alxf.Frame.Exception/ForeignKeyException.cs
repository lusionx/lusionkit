using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alxf.Frame.Exception
{
    public class ForeignKeyException : AlxfException
    {
        public ForeignKeyException()
            : base("违反外键约束")
        {
            //this.InnerException;
        }
    }
}
