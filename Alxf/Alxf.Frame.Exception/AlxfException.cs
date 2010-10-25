using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alxf.Frame.Exception
{
    public class AlxfException : System.Exception
    {
        public AlxfException(string msg)
        {
            this._msg = msg;
        }

        public override string Message
        {
            get
            {
                return this._msg;
            }

        }

        private string _msg;
    }
}
