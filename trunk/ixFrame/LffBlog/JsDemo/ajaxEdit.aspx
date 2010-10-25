<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true"
    CodeBehind="ajaxEdit.aspx.cs" Inherits="LffBlog.JsDemo.ajaxEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_context" runat="server">
    <span>ajaxEdit-update ajaxEdit-cancel ajaxEdit-hidden ajaxEdit-error</span>
    <table>
        <tr>
            <td>
                name
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" reg="\S" CssClass="ajaxEdit" Text="Label">xxx</asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                age
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" CssClass="ajaxEdit" Text="Label">12</asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                sex
            </td>
            <td>
                <asp:Label ID="Label3" runat="server" CssClass="ajaxEdit" Text="Label">男</asp:Label>
            </td>
            <td>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        $(function() {
            $('.ajaxEdit').ajaxEdit(function(jdom, val) {
                alert(jdom[0].id);
                jdom.html(val);
            });
        });
    </script>

</asp:Content>
