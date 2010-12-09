<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"
    CodeBehind="wf60.aspx.cs" Inherits="SilverMoon.Workflow.wf60" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
test
    <asp:TextBox CssClass="txt" ID="txt_id" runat="server">11A01D00</asp:TextBox>
    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="slct">
    <asp:ListItem>AA</asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="Button1" runat="server" Text="Button" CssClass="btn" />

    <script type="text/javascript">
    var ov='';
    function GetData(val) {
        $.post(AppPath + 'Handler/GetOne.ashx', {
            id: val
        },
        function(json) {
            alert(json);
        });
    }
    $(function() {    
        $('#<%=txt_id.ClientID %>').keyup(function() {        
            if (this.value.length == 9) {
                if(ov!=this.value){
                    ov=this.value;
                    GetData(this.value);
                }
            }
        });
    });
    </script>

</asp:Content>
