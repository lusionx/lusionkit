<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Alxf.WebDemo._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <%if (false)
      { %>
    <script type="text/javascript" src="Scripts/jquery-1.4.1-vsdoc.js"></script>
    <%} %>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <%= this.Request.ApplicationPath %>
    <script type="text/javascript">
        $(function () {
            $.post('/Controls/uc1.ascx', { a: 123 }, function (html) {
                alert(html);
            });
        });
    </script>
    <h2>
        Welcome to ASP.NET![Alxf.WebDemo]
    </h2>
    <p>
        To learn more about ASP.NET visit <a href="http://www.asp.net" title="ASP.NET Website">
            www.asp.net</a>.
    </p>
    <p>
        You can also find <a href="http://go.microsoft.com/fwlink/?LinkID=152368&amp;clcid=0x409"
            title="MSDN ASP.NET Docs">documentation on ASP.NET at MSDN</a>.
    </p>
</asp:Content>
