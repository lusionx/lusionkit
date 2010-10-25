﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tool;
using SilverMoon.BAL;

namespace SilverMoon.Workflow
{
    public partial class wf50 : PageBase
    {
        protected WorkStep ws = WorkStep.TFTE;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Serial.BindYear(ddl_year);
                Serial.Year = Convert.ToInt32(this.ddl_year.SelectedValue);

                this.AspNetPager1.PageSize = System.Configuration.ConfigurationManager.AppSettings["PageSize"].ToInt32();
                this.ddl_state.Items.AddRange(
    (from x in DB.Current.sm_biz_State
     where x.Group == (int)ws
     select new ListItem(x.Name, x.ID.ToString())).ToArray());
                DataBind();
            }
        }

        public override void DataBind()
        {
            int st = (int)ws;
            var q = DB.Current.sm_biz_Workpiece.Where(x => x.State > st * 10 && x.State < st * 10 + 100);
            q = q.OrderByDescending(x => x.Date);
            this.AspNetPager1.RecordCount = q.Count();
            this.Repeater1.DataSource = q.Skip(AspNetPager1.StartRecordIndex - 1).Take(AspNetPager1.PageSize);
            this.Repeater1.DataBind();
            base.DataBind();
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            DataBind();
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            var db = BAL.DB.Current;
            var obj = db.GetWorkpiece(this.txt_id.Text);
            obj.Remark = Request.QueryString["type"] ?? string.Empty;
            obj.Remark = GetRemark(obj.Remark);
            obj.sm_biz_State = db.sm_biz_State.First(x => x.ID == this.ddl_state.SelectedValue.ToInt32());
            db.SubmitChanges();
            DataBind();
        }

        protected string GetRemark(string str)
        {
            switch (str)
            {
                case "baiban":
                    {
                        return "白班";

                    }
                case "huangban":
                    {
                        return "黄班";
                    }
                default:
                    {
                        return string.Empty;
                    }

            }

        }

        protected bool CanUpdate(object state)
        {
            int stat = (int)state;
            if (stat == 0)
            {
                return true;
            }

            int grp = (int)ws;
            return stat > grp * 10 && stat < grp * 10 + 100;
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var arg = e.CommandArgument.ToString();
            switch (e.CommandName)
            {
                case "Del":
                    {
                        var db = DB.Current;
                        var obj = db.GetWorkpiece(arg);
                        if (obj != null)
                        {
                            obj.sm_biz_State = db.sm_biz_State.First(x => x.ID == 0);
                            db.SubmitChanges();
                        }
                        break;
                    }
            }
            DataBind();
        }
    }
}
