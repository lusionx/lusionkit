<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="LffBlog.Default" %>

<%@ Register Src="~/UControls/Links.ascx" TagName="Links" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        input
        {
            width: 200px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cleftbar" runat="server">
    <uc:Links ID="Links1" runat="server" />
    <asp:Localize ID="Localize1" runat="server">Welcome</asp:Localize>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cmain" runat="server">
    main
    <%= DateTime.Now %>
    <table style="text-align: left;">
        <tr>
            <td>
                非空
            </td>
            <td>
                <input id="Text1" type="text" reg="\S" />
            </td>
        </tr>
        <tr>
            <td>
                6+位数字
            </td>
            <td>
                <input id="Text2" type="text" reg="\d{6,}" errmsg="6+位数字" />
            </td>
        </tr>
        <tr>
            <td>
                一般密码
            </td>
            <td>
                <input id="Text3" type="text" reg="[qw]{2,}" errmsg="一般密码" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <input id="Submit1" type="submit" value="submit" />
            </td>
        </tr>
    </table>
    <table style="width: 100%;">
        <tr>
            <td>
                <input id="Text4" type="text" />
            </td>
            <td>
                <input id="Text5" type="text" />
            </td>
            <td>
                <button onclick="queryString()">
                    queryString</button>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        $(function() {
            //alert( $.queryString('q1'));
        })
        function queryString() {
            var par = $('#Text4').val();
            var val = $.queryString(par);            
            $('#Text5').val(val);
        }
    </script>

</asp:Content>
