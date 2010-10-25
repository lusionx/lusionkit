using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data.SqlClient;

namespace Utility.Extensions
{
    public static class LocalizeHelper
    {
        public static void Localize(this GridView gridview, LangResource resourceManager)
        {
            foreach (DataControlField field in gridview.Columns)
            {
                field.HeaderText = resourceManager.GetString(field.HeaderText);
            }
        }

        public static void Localize(this GridViewRow gvrow, int[] columns, LangResource resourceManager)
        {
            if (gvrow.RowType == DataControlRowType.DataRow)
            {
                foreach (int i in columns)
                {
                    if (i < gvrow.Cells.Count)
                    {
                        if (gvrow.Cells[i].Controls.Count == 0)
                        {
                            gvrow.Cells[i].Text = resourceManager.GetString(gvrow.Cells[i].Text);
                        }
                        else
                        {
                            foreach (Control cc in gvrow.Cells[i].Controls)
                            {
                                cc.Localize(resourceManager);
                            }
                        }
                    }
                }
            }
        }

        public static void Localize(this DropDownList ddl, LangResource resourceManager)
        {
            foreach (ListItem item in ddl.Items)
            {
                item.Text = resourceManager.GetString(item.Text);
            }
        }

        public static void Localize(this CheckBoxList cbl, LangResource resourceManager)
        {
            foreach (ListItem item in cbl.Items)
            {
                item.Text = resourceManager.GetString(item.Text);
            }
        }

        public static void Localize(this RadioButtonList rbl, LangResource resourceManager)
        {
            foreach (ListItem item in rbl.Items)
            {
                item.Text = resourceManager.GetString(item.Text);
            }
        }

        public static void Localize(this Control control, LangResource resourceManager)
        {
            if (control.HasControls())
            {
                foreach (Control subctrl in control.Controls)
                {
                    subctrl.Localize(resourceManager);
                }
            }
            else
            {
                //简单显示
                if (control is Localize)
                {
                    ((Localize)control).Text = resourceManager.GetString(((Localize)control).Text);
                }
                //按钮
                else if (control is Button)
                {
                    ((Button)control).Text = resourceManager.GetString(((Button)control).Text);
                }
                else if (control is LinkButton)
                {
                    (control as LinkButton).Text = resourceManager.GetString((control as LinkButton).Text);
                }
                //链接
                else if (control is HyperLink)
                {
                    (control as HyperLink).Text = resourceManager.GetString((control as HyperLink).Text);
                }
                //列表，list类型需要手动调用
                else if (control is RadioButton)
                {
                    (control as RadioButton).Text = resourceManager.GetString((control as RadioButton).Text);
                }
                else if (control is CheckBox)
                {
                    (control as CheckBox).Text = resourceManager.GetString((control as CheckBox).Text);
                }
            }
        }

        public static void Localize(this ValidatorCollection collection, LangResource resoourceManager)
        {
            foreach (BaseValidator validator in collection)
            {
                validator.ErrorMessage = resoourceManager.GetString(validator.ErrorMessage);
            }
        }
    }
}
