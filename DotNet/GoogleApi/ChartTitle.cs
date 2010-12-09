using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GoogleApi
{
    public class ChartTitle : ChartPropertyBase
    {
        public string Text { set; get; }

        public Color Color { set; get; }

        /// <summary>
        /// 默认20
        /// </summary>
        public int FontSize { set; get; }

        public ChartTitle()
        {
            Text = string.Empty;
            FontSize = 20;
            Color = Color.Black;
        }

        public override string GetParameter()
        {
            if (Text != string.Empty)
            {
                var sb = new StringBuilder(Text);
                sb.Insert(0, "chtt=");
                sb.Replace("\r\n", GroupSeparator);
                sb.Replace("  ", " ");
                sb.Replace(" ", "+");
                sb.Append(ParameterSeparator);
                sb.AppendFormat("chts={0}{1}{2}",
                    Color.ToArgb().ToString("X").Substring(2)
                    , ItemSeparator, FontSize.ToString());


                return sb.ToString();
            }
            else
            {
                return Text;
            }

        }
    }
}
