<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"
    CodeBehind="Report.aspx.cs" Inherits="SilverMoon.Query.Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset class="query">
        <legend>规则</legend>
        <table>
            <tbody>
                <tr>
                    <th>
                        压铸机号
                    </th>
                    <td>
                        <asp:DropDownList ID="ddl_mid" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        所属部门
                    </th>
                    <td>
                        <asp:DropDownList ID="ddl_step" runat="server">                             
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        时间
                    </th>
                    <td>
                        <asp:DropDownList ID="ddl_year" runat="server">
                            <asp:ListItem>2009</asp:ListItem>
                            <asp:ListItem>2010</asp:ListItem>
                            <asp:ListItem>2011</asp:ListItem>
                        </asp:DropDownList>
                        年
                        <asp:DropDownList ID="ddl_month" runat="server">                            
                        </asp:DropDownList>
                        月
                    </td>
                </tr>
            </tbody>
            <tfoot>
                <tr>
                    <td style="width: 20%">
                    </td>
                    <td>
                        <asp:Button ID="btn_down" runat="server" Text="导出" onclick="btn_down_Click" />
                    </td>
                </tr>
            </tfoot>
        </table>
    </fieldset>
</asp:Content>
