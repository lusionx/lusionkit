using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleApi
{
    /// <summary>
    /// chds=-80,140
    /// </summary>
    public class ChartFix : ChartPropertyBase
    {
        public ChartFix()
        {
            Items = new List<float>();
        }

        public List<float> Items { set; get; }

        public override string GetParameter()
        {
            if (Items.Count > 0)
            {
                return string.Format("chds={0}",
                    string.Join(ItemSeparator,
                    (from e in Items 
                     select e.ToString("f1")).ToArray()));
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
