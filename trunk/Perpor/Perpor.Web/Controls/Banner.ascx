<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Banner.ascx.cs" Inherits="Perpor.Web.Controls.Banner" %>
<div class="banner">
    <ul>
        <li>
            <asp:HyperLink ID="HyperLink6" NavigateUrl="~/Default.aspx" runat="server">首页</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Func/EditWidget.aspx" runat="server">编辑模块</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink ID="HyperLink2" runat="server">注册</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink ID="HyperLink3" runat="server">登录</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink ID="HyperLink4" runat="server">开发</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink ID="HyperLink5" NavigateUrl="~/Login.aspx" runat="server">退出</asp:HyperLink>
        </li>
    </ul>
</div>
