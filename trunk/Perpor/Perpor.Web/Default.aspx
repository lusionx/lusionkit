<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Perpor.Web.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:Repeater ID="rptcln" runat="server" OnItemDataBound="rptcln_ItemDataBound">
        <ItemTemplate>
            <ul class="column">
                <asp:Repeater ID="rptwidget" runat="server" OnItemDataBound="rptwidget_ItemDataBound">
                    <ItemTemplate>
                        <pp:Widget ID="Widget1" runat="server" />
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
