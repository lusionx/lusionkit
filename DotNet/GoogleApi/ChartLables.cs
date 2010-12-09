using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleApi
{
    /// <summary>
    /// chl=Hello|World
    /// </summary>
    public class ChartLables : ChartPropertyBase
    {
        public ChartLables()
        {
            List = new List<string>();
        }

        public List<string> List { get; set; }

        public override string GetParameter()
        {
            if (List.Count > 0)
            {
                return string.Format("chl={0}", string.Join(GroupSeparator, List.ToArray()));
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
