<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SilverMoon.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="context" style="width: 400px">
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        <br />
        <div style="background-color: #B2C3E1; border-color: #41519A; border-width: 1px;
            border-style: Solid; border-collapse: collapse; width: 400px;">
            <table cellpadding="0" border="0" style="margin: 5px; color: #333333; font-family: Verdana;
                width: 97%">
                <tr>
                    <td colspan="2" style="text-align: center; color: White; background-color: #41519A;
                        font-weight: bold; font-size: 12px">
                        登录
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <label for="Login1_UserName">
                            用户名:</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_name" Width="150px" runat="server">Admin</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <label for="Login1_Password">
                            密码:</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_pwd" Width="150px" TextMode="Password" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="Button1" Style="color: #41519A; background-color: White; border-color: #41519A;
                            border-width: 1px; border-style: Solid;" runat="server" Text="登陆" OnClick="Button1_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
