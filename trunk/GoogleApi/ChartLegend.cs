using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleApi
{
    /// <summary>
    /// chdl=NASDAQ|FTSE100|DOW&chdlp=l
    /// </summary>
    public class ChartLegend : ChartPropertyBase
    {
        public ChartLegend()
        {
            Items = new List<string>();
        }

        public List<string> Items { set; get; }

        public PlaceType Place { set; get; }

        public override string GetParameter()
        {
            if (Items.Count > 0)
            {
                if (Place == PlaceType.r)
                {
                    return string.Format("chdl={0}",
                        string.Join(GroupSeparator, Items.ToArray()));
                }
                else
                {
                    return string.Format("chdl={0}{1}chdlp={2}",
                        string.Join(GroupSeparator, Items.ToArray()),
                        ParameterSeparator,
                       Place.ToString());
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }

    public enum PlaceType
    {
        b, t, r, l
    }
}
