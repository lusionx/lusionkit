<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="MyAlx.Core.Models" %>
<%@ Import Namespace="MyAlx.Tools" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%: ViewData["Message"] %></h2>
    <p>
        To learn more about ASP.NET MVC visit <a href="http://asp.net/mvc" title="ASP.NET MVC Website">
            http://asp.net/mvc</a>.
        <%=ConstString.EntityKeyOfViewData%>
    </p>
    <p>
        <% foreach (var a in ViewData[ConstString.EntityKeyOfViewData] as IEnumerable<WorkLog>)
           { %>
        <li>
            <%= a.Log %>
        </li>
        <% } %>
    </p>
</asp:Content>
