<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="gv.aspx.cs" Inherits="SLWeb.Test.gv" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridView1" runat="server" 
        onrowdatabound="GridView1_RowDataBound">
    </asp:GridView>
    <script type="text/javascript">
    //Sys.Serialization.JavaScriptSerializer.serialize();
    </script>
</asp:Content>
