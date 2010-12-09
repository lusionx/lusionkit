<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="SLWeb.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <asp:Button runat="server" ID="btn" Text="SignOut" />
    <asp:MultiView ID="MultiView1" runat="server">
    </asp:MultiView>
    <asp:BulletedList ID="BulletedList1" runat="server">
    </asp:BulletedList>
    <asp:Substitution ID="Substitution1" runat="server" />
    <asp:View ID="View1" runat="server">
    </asp:View>
</asp:Content>
