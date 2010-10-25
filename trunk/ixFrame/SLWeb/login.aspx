<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true"
    CodeBehind="login.aspx.cs" Inherits="SLWeb.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        $(function() {
            $('#id').val('');            
        })
    </script>

    <asp:TextBox ID="TextBox1" CssClass="jev-reqf" runat="server"></asp:TextBox>
    <asp:Button ID="btn_login" runat="server" Text="登录" OnClick="btn_login_Click" />
</asp:Content>
