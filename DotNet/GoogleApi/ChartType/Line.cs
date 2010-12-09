using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleApi.ChartType
{
    public class Line : ChartTypeBase
    {
        public Line(LineType t)
        {
            this.Type = t;
        }

        public LineType Type { set; get; }

        public override string GetParameter()
        {
            return string.Format("cht={0}", this.Type.ToString());
        }
    }
    public enum LineType
    {
        /// <summary>
        /// 只有Y轴
        /// </summary>
        lc, 
        /// <summary>
        /// XY轴
        /// </summary>
        lxy
    }
}
