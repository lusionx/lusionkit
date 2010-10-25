using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;
using GoogleApi;
using GoogleApi.ChartType;
using Tool;
namespace SilverMoon.Handler
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Chart : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            Query.Data.Query q = HttpContext.Current.Session["TQuery"] as Query.Data.Query;
            context.Response.ContentType = "text/plain";
            var Day = context.Request.Form["Day"].Replace("合计", string.Empty);
            var total = context.Request.Form["Total"];
            var db = BAL.DB.Current;
            List<DAL.sm_biz_Workpiece> data = null;
            if (string.IsNullOrEmpty(Day))
            {
                data = (from x in db.sm_biz_Workpiece
                        join s in db.sm_biz_State on x.State equals s.ID
                        where x.Date >= q.TimeFrom && x.Date < q.TimeTo && s.Group == (int)q.Step
                        select x).ToList();
            }
            else
            {
                data = (from x in db.sm_biz_Workpiece
                        join s in db.sm_biz_State on x.State equals s.ID
                        where x.Date == Day.ToDateTime() && s.Group == (int)q.Step
                        select x).ToList();
            }
            var dic = new Dictionary<int, int>();

            var chd = (from x in data
                       group x by x.State into g
                       select new Group { Key = g.Key, Count = g.Count() }).ToList();

            var cht = new GoogleApi.Chart(new GoogleApi.ChartType.Pie(PieType.p3));
            cht.Size.Height = 200;
            cht.Size.Width = 500;
            foreach (var ee in chd)
            {
                cht.Lables.List.Add(ee.State + ee.Count + "/" + total);
            }

            cht.Data.Group.Add(new List<float>(chd.Select(x => (float)x.Count)));
            context.Response.Write(cht.ToString());
        }

        internal class Group
        {
            public int Key { get; set; }
            public string State
            {
                get
                {
                    return BAL.DB.Current.sm_biz_State.Single(x => x.ID == Key).Name;
                }
            }
            public int Count { get; set; }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
