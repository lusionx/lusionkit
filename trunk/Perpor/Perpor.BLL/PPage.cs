using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Perpor.BLL
{
    public class PPage
    {
        public PPage()
        {
            Column0 = new List<WidgetData>();
            Column1 = new List<WidgetData>();
            Column2 = new List<WidgetData>();
        }

        public List<WidgetData> Column0 { get; set; }
        public List<WidgetData> Column1 { get; set; }
        public List<WidgetData> Column2 { get; set; }

        public List<WidgetData> this[int i]
        {
            get
            {
                var clm = new List<WidgetData>[] { Column0, Column1, Column2 };
                return clm[i];
            }
        }

        public IEnumerable<WidgetData> AllWidgetData()
        {
            return Column0.Union(Column1).Union(Column2);
        }

        public void AddWidget(WidgetData wdt)
        {
            var clm = new List<WidgetData>[] { Column0, Column1, Column2 };
            var cc = new int[] { Column0.Count, Column1.Count, Column2.Count };
            var lt = clm.First(x => x.Count == cc.Min());
            lt.Add(wdt);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

