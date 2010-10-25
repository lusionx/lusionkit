using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace SilverMoon.Test
{
    public class AjaxPageBase : Page
    {
        protected sealed override void OnLoad(EventArgs e)
        {
            switch (Request.HttpMethod)
            {
                case "GET":
                    {
                        this.RequestGET();
                        break;
                    }
                case "POST":
                    {
                        this.RequestPOST();
                        break;
                    }

            }

            base.OnLoad(e);
        }

        protected virtual void RequestGET()
        {

        }

        protected virtual void RequestPOST()
        {

        }

        protected virtual string PostValueString(Control ctr)
        {
            var form = Request.Form;
            string val = form[this.ControlName(ctr)];
            return val;
        }

        protected virtual bool PostValueBoolean(Control ctr)
        {
            var form = Request.Form;
            if (ctr is RadioButton)
            {
                var rb = ctr as RadioButton;
                var val = form[rb.GroupName];
                if (val != null)
                {
                    return val.Equals(rb.Text);
                }
                else
                {
                    return false;
                }
            }
            else if (ctr is CheckBox)
            {
                string val = form[this.ControlName(ctr)];
                return val != null;
            }
            else
            {
                throw new Exception();
            }

        }
        /// <summary>
        /// 从post过来的数据得到控件的值
        /// </summary>
        /// <typeparam name="TSource">bool,string,IList</typeparam>
        /// <param name="ctr"></param>
        /// <returns></returns>
        protected virtual IList<string> PostValueList(Control ctr)
        {
            var form = Request.Form;
            if (ctr is ListBox)
            {
                return form.GetValues(this.ControlName(ctr)).ToList();
            }
            else if (ctr is CheckBoxList)
            {
                string val = null;
                var cbl = ctr as CheckBoxList;
                List<string> lt = new List<string>();
                for (int i = 0; i < cbl.Items.Count; i++)
                {
                    val = form[this.ControlName(ctr) + "$" + i.ToString()];
                    if (val == null)
                    {
                        cbl.Items[i].Selected = false;
                    }
                    else
                    {
                        cbl.Items[i].Selected = true;
                        lt.Add(cbl.Items[i].Value);
                    }
                }
                return lt;
            }
            else
            {
                throw new Exception();
            }
            #region 注释掉的
            //switch (ctr.GetType())
            //{
            //    case (typeof(HiddenField)):
            //        {
            //            return val;
            //        }
            //    case (typeof(TextBox)):
            //        {
            //            return val;
            //        }
            //    case (typeof(DropDownList)):
            //        {
            //            return val;
            //        }
            //}
            #endregion

        }



        private string ControlName(Control ctr)
        {
            return ctr.ClientID.Replace("_", "$");
        }
    }
}

//public class myTextBox : TextBox
//{

//}
//public class myDropDownList : DropDownList
//{

//}
//public class myHiddenField : HiddenField
//{

//}
//public class myCheckBox : CheckBox
//{

//}
//public class myCheckBoxList : CheckBoxList
//{

//}
//public class myRadioButton : RadioButton
//{

//}
//public class myRadioButtonList : RadioButtonList
//{

//}
//public class myListBox : ListBox
//{

//}

