using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleApi
{
    /// <summary>
    /// chs=300x400
    /// </summary>
    public class ChartSize : ChartPropertyBase
    {
        public int Width { get; set; }
        public int Height { get; set; }

        /// <summary>
        /// chs=250x100
        /// </summary>
        /// <returns></returns>
        public override string GetParameter()
        {
            return string.Format("chs={0}x{1}", Width, Height);
        }
    }
}
