using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleApi.ChartType
{
    /// <summary>
    /// cht=p3
    /// </summary>
    public class Pie : ChartTypeBase
    {
        public Pie(PieType t)
        {
            this.Type = t;
        }

        public PieType Type { set; get; }

        public override string GetParameter()
        {
            return string.Format("cht={0}", this.Type.ToString());
        }
    }

    public enum PieType
    {
        /// <summary>
        /// Pie
        /// </summary>
        p,
        /// <summary>
        /// 3D Pie
        /// </summary>
        p3
    }
}
