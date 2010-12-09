using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GoogleApi
{
    /// <summary>
    /// chco=ff0000,00ff00,0000ff
    /// </summary>
    public class ChartColor : ChartPropertyBase
    {
        public ChartColor()
        {
            this.Items = new List<Color>();
        }

        public List<Color> Items { get; set; }

        public override string GetParameter()
        {
            if (Items.Count > 0)
            {
                return string.Format("chco={0}", string.Join(ItemSeparator,
                    (from e in Items 
                     select e.ToArgb().ToString("X").Substring(2))
                     .ToArray()));
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
