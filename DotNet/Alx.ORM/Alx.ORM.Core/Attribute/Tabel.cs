using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Alx.ORM.Core
{
    public class TabelAttribute : System.Attribute
    {
        public string Name { get; set; }
        public IEnumerable<ColumnAttribute> Columns { get; set; }
    }
}
