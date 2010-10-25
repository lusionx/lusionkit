<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FenBuTu.aspx.cs" Inherits="SilverMoon.Query.FenBuTu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="data" id="context" style="width: 900px; margin: 0px;">
        <asp:Label ID="Label1" runat="server" Visible="false" Text="">请选择压铸机号和模次</asp:Label>
        <asp:Repeater ID="rpt_d" runat="server">
            <HeaderTemplate>
                <table class="w100">
                    <tr>
                        <td>
                            白班
                        </td>
            </HeaderTemplate>
            <ItemTemplate>
                <td class='<%# GetStateCss(Eval("State")) %>' title='<%# Eval("Serial").ToString()+ ShowState(Eval("State"))%>'>
                    &nbsp;
                </td>
            </ItemTemplate>
            <FooterTemplate>
                </tr></table>
            </FooterTemplate>
        </asp:Repeater>
        <br />
        <asp:Repeater ID="rpt_n" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <td>
                            夜班
                        </td>
            </HeaderTemplate>
            <ItemTemplate>
                <td class='<%# GetStateCss(Eval("State")) %>' title='<%# Eval("Serial").ToString()+ ShowState(Eval("State"))%>'>
                    &nbsp;
                </td>
            </ItemTemplate>
            <FooterTemplate>
                </tr></table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
