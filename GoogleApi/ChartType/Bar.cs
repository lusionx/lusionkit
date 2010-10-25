using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleApi.ChartType
{
    public class Bar : ChartTypeBase
    {
        public Bar(BarType t)
        {
            this.Type = t;
        }

        public BarType Type { set; get; }
        public override string GetParameter()
        {
            return string.Format("cht={0}", this.Type.ToString());
        }
    }

    public enum BarType
    {
        /// <summary>
        /// 单数据集独柱图
        /// </summary>
        bvs
    }
}
