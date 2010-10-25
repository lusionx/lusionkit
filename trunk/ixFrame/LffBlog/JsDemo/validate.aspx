<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true"
    CodeBehind="validate.aspx.cs" Inherits="LffBlog.JsDemo.validate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_context" runat="server">
<span>span class=jv-errmsg</span>
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
    </table>
    <asp:Button ID="Button1" runat="server" Text="提交" />
</asp:Content>
