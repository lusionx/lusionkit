<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Perpor.Web.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

    <script type="text/javascript" src="Script/jquery-1.2.6.min.js">
    </script>

    <script type="text/javascript" src="Script/json2.js"></script>

    <script type="text/javascript">
        $(function() {
            var oo = new Object();
            oo.name = 'xxx';
            oo.arr = [1, 2, 3, 4];
            alert(JSON.stringify(oo));
        });
       

    </script>

    登录any lxing uu1
    <table class="w100">
        <tr>
            <td>
                <table cellpadding="0" border="0">
                    <tr>
                        <td colspan="2">
                            登录
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            用户名:
                        </td>
                        <td>
                            <asp:TextBox ID="txt_name" Width="150px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            密码:
                        </td>
                        <td>
                            <asp:TextBox ID="txt_pwd" Width="150px" TextMode="Password" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btn_go" runat="server" Text="登陆" OnClick="btn_go_Click" />
                            <asp:Button ID="btn_any" runat="server" Text="匿名" CausesValidation="false" OnClick="btn_any_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table cellpadding="0" border="0">
                    <tr>
                        <td colspan="2">
                            注册
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            用户名:
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" Width="150px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            密码:
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox2" Width="150px" TextMode="Password" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            确认:
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox3" Width="150px" TextMode="Password" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            邮箱:
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox4" Width="150px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btn_reg" runat="server" Text="注册" OnClick="btn_reg_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
