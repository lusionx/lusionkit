<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListByDate.aspx.cs" Inherits="SilverMoon.Query.ListByDate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="data" id="context" style="width: 670px; margin: 0px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <HeaderTemplate>
                <table style="margin: 0px;" class="w100">
                    <thead>
                        <tr>
                            <th>
                                流水号
                            </th>
                            <th>
                                状态
                            </th>
                            <th>
                                说明
                            </th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class='<%# Eval("State").ToString()=="0"?"":"orange" %>'>
                    <td>
                        <%# Eval("FullSerial") %>
                    </td>
                    <td>
                        <%# ShowState(Eval("State"))%>
                    </td>
                    <td>
                        <%# Eval("Remark")%>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class='<%# Eval("State").ToString()=="0"?"alrow":"orange alrow" %>'>
                    <td>
                        <%# Eval("FullSerial") %>
                    </td>
                    <td>
                        <%# ShowState(Eval("State"))%>
                    </td>
                    <td>
                        <%# Eval("Remark")%>
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                </tbody> </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
