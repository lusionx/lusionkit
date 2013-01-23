using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alx.ORM.Core
{
    public interface IDefVal
    {
        object Val { get; }
    }

    public class Int0 : IDefVal
    {
        public object Val
        {
            get { return 0; }
        }
    }

    public class StringNull : IDefVal
    {
        public object Val
        {
            get { return null; }
        }
    }

    public class StringEmpty : IDefVal
    {
        public object Val
        {
            get { return string.Empty; }
        }
    }
}
