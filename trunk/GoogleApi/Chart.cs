using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using GoogleApi.ChartType;

namespace GoogleApi
{
    public class Chart
    {
        public const string Host = "http://chart.apis.google.com/chart?";

        public Chart(ChartTypeBase type)
        {
            Axis = new ChartAxis();
            Color = new ChartColor();
            Data = new ChartData();
            Fix = new ChartFix();
            Lables = new ChartLables();
            Legend = new ChartLegend();
            Size = new ChartSize();
            Title = new ChartTitle();
            Type = type;
            _properties = new List<ChartPropertyBase>{
                Axis,
                Color,
                Data,
                Fix,
                Lables,
                Legend,
                Size,
                Title,
                Type
            };
        }

        private List<ChartPropertyBase> _properties;

        public ChartAxis Axis { set; get; }
        public ChartColor Color { set; get; }
        public ChartData Data { set; get; }
        public ChartFix Fix { set; get; }
        public ChartLables Lables { set; get; }
        public ChartLegend Legend { set; get; }
        public ChartSize Size { set; get; }
        public ChartTitle Title { set; get; }
        public ChartTypeBase Type { set; get; }
        

        /// <summary>
        /// 返回图片请求的url
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Host);

            sb.Append(string.Join(ChartPropertyBase.ParameterSeparator,
                (from e in _properties
                 select e.GetParameter()).ToArray()));

            return sb.ToString();
        }

        /// <summary>
        /// 下载到本地
        /// </summary>
        /// <param name="dirpath">下载目录路径,以/结尾</param>
        /// <returns>本地路径</returns>
        public string Download(string dirpath)
        {
            var fn = dirpath + System.Guid.NewGuid().ToString() + ".png";
            using (var wc = new WebClient())
            {
                wc.DownloadFile(this.ToString(), fn);
            }
            return fn;
        }

        /// <summary>
        /// return img alt="" src=""
        /// </summary>
        public string Html(bool download)
        {
            //if (download)
            //{
            //    return string.Format(@"<img style=""border: solid 1px #d3d3d3;"" alt="""" src=""{0}"" />", this.LocalImagePath);
            //}
            //else
            //{
            return string.Format(@"<img style=""border: solid 1px #d3d3d3;"" alt="""" src=""{0}"" />", this.ToString());
            //}

        }
    }
}
