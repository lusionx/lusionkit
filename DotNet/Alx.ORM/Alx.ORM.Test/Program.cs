using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alx.ORM.Core;

namespace Alx.ORM.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ITest t = new TestOracle();
            t.Test();
        }
    }

    public interface ITest
    {
        void Test();
    }
}
