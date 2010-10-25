<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="updiv.aspx.cs" Inherits="LffBlog.test.updiv" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" src="/js/jquery.1.2.6.min.js"></script>

    <script type="text/javascript" src="/js/jstest.js"></script>

</head>
<body style="background-color: red;">
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" runat="server">
        </asp:TextBox><asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" /><asp:Label
            ID="Label1" runat="server" Text="Label"></asp:Label>
        <input id="Button2" type="button" class="btn_closePar" value="close" />
    </div>
    </form>
</body>
</html>
