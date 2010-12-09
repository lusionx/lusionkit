using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;

namespace SLApp
{
    public partial class NewsLink : UserControl
    {
        private IEnumerable<XElement> items;

        public NewsLink()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(NewsLink_Loaded);
        }

        void NewsLink_Loaded(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            //HtmlDocument dom = HtmlPage.Document;

            //items = XDocument.Load("~/NewsImages/Config.xml").Descendants("item");
            string path = "NewsImages/Config.xml";
            Uri root = new Uri(ConfigHelper.GetSetting("host") + path, UriKind.Absolute);
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
            wc.DownloadStringAsync(root);
        }

        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                items = XDocument.Parse(e.Result).Descendants("item");
                int count = items.Count();
                for (int i = 0; i < count; i++)
                {
                    Button btn = new Button();
                    btn.Width = 20;
                    btn.Height = 20;
                    btn.HorizontalAlignment = HorizontalAlignment.Right;
                    btn.Content = i + 1;
                    btn.Click += new RoutedEventHandler(btn_Click);
                    this.foot.Children.Add(btn);
                }
                
                UpdateShow(null);
            }
        }

        void btn_Click(object sender, RoutedEventArgs e)
        {
            UpdateShow(sender as Button);
        }

        protected void UpdateShow(Button btn)
        {
            int i = 0;
            if (btn != null)
            {
                i = int.Parse(btn.Content.ToString());
                i--;
            }
            //加载第i个
            XElement xe = this.items.ToArray()[i];
            // XElement xe = this.items.Skip(i).Take(1).Single();
            string path = ConfigHelper.GetSetting("host") + "NewsImages/";

            this.hlb_show.NavigateUri = new Uri(xe.Element("href").Value, UriKind.Absolute);
            this.img_show.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(path + xe.Element("path").Value, UriKind.Absolute));
        }
    }
}
