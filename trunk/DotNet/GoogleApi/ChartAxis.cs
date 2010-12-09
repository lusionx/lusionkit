using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleApi
{

    /// <summary>
    /// 轴标签chxt 和 chxl
    /// </summary>
    public class ChartAxis : ChartPropertyBase
    {
        public ChartAxis()
        {
            this.AxisX = new List<string>();
            this.AxisY = new List<string>();
        }
        public override string GetParameter()
        {
            if (AxisX.Count != 0)
            {
                var sb = new StringBuilder();
                if (AxisY.Count != 0)
                {//X+Y
                    sb.Append("chxt=x,y");
                    sb.Append(ChartPropertyBase.ParameterSeparator);
                    sb.Append("chxl=0:|");
                    foreach (var x in AxisX)
                    {
                        sb.Append(x);
                        sb.Append(ChartPropertyBase.GroupSeparator);
                    }
                    sb.Append("1:");
                    foreach (var x in AxisY)
                    {                       
                        sb.Append(ChartPropertyBase.GroupSeparator);
                        sb.Append(x);
                    }
                }
                else
                {//X
                    sb.Append("chxt=x");
                    sb.Append(ChartPropertyBase.ParameterSeparator);
                    sb.Append("chxl=0:");
                    foreach (var x in AxisX)
                    {
                        sb.Append(ChartPropertyBase.GroupSeparator);
                        sb.Append(x);
                    }
                }
                return sb.ToString();
            }
            return string.Empty;
        }

        public List<string> AxisX { get; set; }
        public List<string> AxisY { get; set; }
    }
}
