using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using SilverMoon.BAL;
using Tool;

namespace SilverMoon.Admin
{
    public partial class Users : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.pl_role.Visible = true;
            if (!IsPostBack)
            {
                this.pl_role.Visible = false;
                DataBind();
            }
        }

        public override void DataBind()
        {
            this.Repeater1.DataSource = Membership.GetAllUsers();//[""].UserName;
            this.Repeater1.DataBind();
        }

        protected void btn_ok_Click(object sender, EventArgs e)
        {
            string name = this.txt_name.Text.Trim();
            string oldpwd = this.txt_opwd.Text;
            string pwd = this.txt_pwd.Text;
            if (pwd.Length < 6)
            {
                Alert("密码至少6位");
                return;
            }
            var uu = Membership.GetUser(name);
            if (uu == null)
            {
                MembershipCreateStatus mcs;
                Membership.CreateUser(name, pwd, "a@a.a", "?", "!", true, out mcs);
                Alert(mcs.ToString());
            }
            else
            {
                //update
                if (oldpwd != string.Empty && uu.ChangePassword(oldpwd, pwd))
                {
                    Alert("修改成功");
                }
                else
                {
                    Alert("原始密码错误");
                }
            }
            DataBind();
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //"Delete"
            string name = e.CommandArgument.ToString();
            var useroles = Roles.GetRolesForUser(name);
            switch (e.CommandName)
            {
                case "Delete":
                    {
                        if (!useroles.Contains(System.Configuration.ConfigurationManager.AppSettings["AdminRole"]))
                        {
                            Membership.DeleteUser(name);
                        }
                        else
                        {
                            Alert("不能删除管理员");
                        }
                        break;
                    }
                case "Role":
                    {
                        var cbs = pl_role.Controls.Cast<Control>().Where(x => x is CheckBox).Select(x => x as CheckBox);
                        foreach (var ee in cbs)
                        {
                            if (ee.Text.In(useroles))
                            {
                                ee.Checked = true;
                            }
                            else
                            {
                                ee.Checked = false;
                            }
                        }
                        this.lt_u.Text = name;
                        break;
                    }
            }
            DataBind();
        }

        protected void btn_role_Click(object sender, EventArgs e)
        {
            var name = this.lt_u.Text;
            var cbs = pl_role.Controls.Cast<Control>().Where(x => x is CheckBox)
                .Select(x => x as CheckBox)
                .Where(x => x.Checked)
                .Select(x => x.Text);
            string[] roles = Roles.GetRolesForUser(name);
            if (roles.Length > 0)
            {
                Roles.RemoveUserFromRoles(name, roles);
            }
            roles = cbs.ToArray();
            if (roles.Length > 0)
            {
                Roles.AddUserToRoles(name, roles);
            }
        }
    }
}
