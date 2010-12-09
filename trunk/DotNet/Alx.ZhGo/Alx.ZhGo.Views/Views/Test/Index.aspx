<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    test Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <li>
            <%= Html.ActionLink("测试", "Index", "Test")%></li>
        <li>
            <%= Html.ActionLink("测试", "Index2", "Test")%></li>
    </h2>
</asp:Content>
