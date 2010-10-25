<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true"
    CodeBehind="game.aspx.cs" Inherits="SLWeb.game" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Silverlight ID="Xaml1" runat="server" Source="~/ClientBin/SLApp.xap" MinimumVersion="2.0.31005.0"
        Width="100%" Height="100%">
    </asp:Silverlight>
</asp:Content>
