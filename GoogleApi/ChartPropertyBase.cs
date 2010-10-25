using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleApi
{
    public abstract class ChartPropertyBase
    {
        /// <summary>
        /// is ","
        /// </summary>
        public const string ItemSeparator = ",";

        /// <summary>
        /// is "|"
        /// </summary>
        public const string GroupSeparator = "|";

        /// <summary>
        /// is and char
        /// </summary>
        public const string ParameterSeparator = "&";

        public abstract string GetParameter();
    }
}
