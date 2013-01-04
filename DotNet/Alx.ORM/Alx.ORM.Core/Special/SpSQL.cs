using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alx.ORM.Core
{
    public class SpSQL : ISpecial
    {
        public string ParamPerFix
        {
            get { return "@"; }
        }

        public object FixValue(object val)
        {
            return val;
        }

        public string SqlSelect(TabelAttribute tableAttr, string where, List<string> parNames, int skip, int take)
        {
            throw new NotImplementedException();
        }

        public object ChangeType(object value, Type conversionType)
        {
            throw new NotImplementedException();
        }
    }
}
