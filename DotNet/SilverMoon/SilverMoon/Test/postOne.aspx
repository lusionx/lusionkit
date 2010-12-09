<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="postOne.aspx.cs" Inherits="SilverMoon.Test.postOne" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="form">
        <p>
            <asp:TextBox ID="txtName" runat="server">lxing</asp:TextBox>
        </p>
        <p>
            <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
        </p>
        <p>
            <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server">remark</asp:TextBox>
        </p>
        <p>
            <asp:DropDownList ID="ddlAge" runat="server">
                <asp:ListItem>12</asp:ListItem>
                <asp:ListItem>13</asp:ListItem>
                <asp:ListItem>14</asp:ListItem>
            </asp:DropDownList>
        </p>
        <p>
            HiddenField
            <asp:HiddenField ID="hf" Value="hfvv" runat="server" />
        </p>
        <p>
            1<asp:CheckBox ID="cb1" Text="cb1" runat="server" /><asp:CheckBox ID="cb2" Text="cb2"
                Checked="true" runat="server" />
        </p>
        <p>
            2<asp:CheckBoxList ID="CheckBoxList1" runat="server">
                <asp:ListItem Selected="True">aaa</asp:ListItem>
                <asp:ListItem>bbb</asp:ListItem>
            </asp:CheckBoxList>
        </p>
        <p>
            3<asp:RadioButton ID="rb1" runat="server" GroupName="rrbb" Text="rb1" /><asp:RadioButton
                ID="rb2" Text="rb2" GroupName="rrbb" runat="server" />
        </p>
        <p>
            4<asp:RadioButtonList ID="rbl" runat="server">
                <asp:ListItem Selected="True">aaa</asp:ListItem>
                <asp:ListItem>bbb</asp:ListItem>
            </asp:RadioButtonList>
        </p>
        <p>
            <asp:ListBox ID="ListBox1" runat="server" SelectionMode="Multiple">
                <asp:ListItem Selected="True">aaa</asp:ListItem>
                <asp:ListItem>bbb</asp:ListItem>
                <asp:ListItem>ccc</asp:ListItem>
            </asp:ListBox>
        </p>
        <p>
            <%=this.Request.HttpMethod%></p>
    </div>
    </form>
</body>
</html>
