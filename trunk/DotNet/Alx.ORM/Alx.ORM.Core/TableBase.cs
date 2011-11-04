using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alx.ORM.Core
{
    public abstract class TableBase
    {
        protected List<string> ChangeColumn = new List<string>();
        public bool IsChangeColumn(string columnName)
        {   
            foreach (var a in ChangeColumn)
            {
                if (a.Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
