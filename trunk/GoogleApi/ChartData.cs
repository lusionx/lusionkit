using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleApi
{
    /// <summary>
    /// chd=t:10.0,58.0,95.0|30.0,8.0,63.0
    /// </summary>
    public class ChartData : ChartPropertyBase
    {
        public ChartData()
        {
            Group = new List<List<float>>();
        }
        public List<List<float>> Group { set; get; }
        public override string GetParameter()
        {
            //CheckData();
            var q = from e in Group.ToArray()
                    select string.Join(ItemSeparator,
                        (from i in e.ToArray() 
                         select i.ToString("f1")).ToArray());

            return string.Format("chd=t:{0}", string.Join(GroupSeparator, q.ToArray()));
        }

        /// <summary>
        /// 对于超过100的数据进行等比例缩小
        /// 负数只许有-1
        /// </summary>
        private void CheckData()
        {
            var temp = new List<float>();
            Group.ForEach(e => temp.AddRange(e));
            if (temp.Where(e => e < 0 && e != -1).Count() > 0)
            {
                throw new ChartException("负数只许有-1");
            }

            float max = temp.Max();
            if (max > 100)
            {
                float fs = max / 100;
                Group.ForEach(e => e.Where(o => o > 0).ToList().ForEach(a => a = a / fs));
            }
        }
    }
}
