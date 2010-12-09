using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Reflection;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Web.UI.WebControls;

namespace Alxf.Frame.Page
{
    public static class PageModel
    {
        /// <summary>
        /// 将值绑定到控件上
        /// </summary>
        /// <param name="pg"></param>
        /// <param name="model"></param>
        public static void BindModel(this System.Web.UI.Page pg, object model)
        {
            BindModel(FindForm(pg), model);
        }

        private static HtmlForm FindForm(System.Web.UI.Page pg)
        {
            var form = pg.Controls.Cast<Control>().First(aa => aa is HtmlForm);

            if (form != null)
            {
                return form as HtmlForm;
            }
            else
            {
                throw new Exception("此页面没有form");
            }
        }

        private static void BindModel(HtmlForm form, object model)
        {
            var protys = model.GetType().GetProperties();

            foreach (var x in MatchControl(form.Controls, protys))
            {
                SetControlValue(x.Control, x.Property.GetValue(model, null));
            }
        }

        private static void SetControlValue(Control ctr, object val)
        {
            if (ctr is TextBox)
            {
                (ctr as TextBox).Text = val.ToString();
            }
            else if (ctr is HiddenField)
            {
                (ctr as HiddenField).Value = val.ToString();
            }
            else if (ctr is DropDownList)
            {
                (ctr as DropDownList).SelectedValue = val.ToString();
            }
            else
            {

            }
        }

        private static string GetControlValue(Control ctr)
        {
            if (ctr is TextBox)
            {
                return (ctr as TextBox).Text;
            }
            else if (ctr is HiddenField)
            {
                return (ctr as HiddenField).Value;
            }
            else if (ctr is DropDownList)
            {
                return (ctr as DropDownList).SelectedValue;
            }
            else
            {
                return string.Empty;
            }
        }

        public static TModel GetModel<TModel>(this System.Web.UI.Page pg) where TModel : new()
        {
            return GetModel<TModel>(FindForm(pg));
        }

        public static TModel GetModel<TModel>(HtmlForm form) where TModel : new()
        {
            var protys = typeof(TModel).GetProperties();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("{");

            string format = "\"{0}\":\"{1}\",";

            foreach (var x in MatchControl(form.Controls, protys))
            {
                //从控件中取得值为  string    //需要处理
                //x.Property.SetValue(model, GetControlValue(x.Control), null);

                //采用json的方式                
                sb.AppendFormat(format, x.Property.Name, GetControlValue(x.Control));
            }

            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();

            return jss.Deserialize<TModel>(sb.ToString(0, sb.Length - 1) + "}");
        }

        private static IEnumerable<BoxCP> MatchControl(IEnumerable ctrs, IEnumerable<PropertyInfo> pros)
        {
            foreach (Control ctr in ctrs)
            {
                foreach (var p in pros)
                {
                    if (ctr.ID == "tf_" + p.Name)
                    {
                        yield return new BoxCP { Control = ctr, Property = p };
                    }
                }
            }
        }

        internal class BoxCP
        {
            internal Control Control { set; get; }

            internal PropertyInfo Property { set; get; }
        }
    }
}
