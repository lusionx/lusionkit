using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleApi
{
    public class ChartException : System.Exception
    {
        public ChartException(string msg)
            : base(msg)
        {

        }
    }
}
