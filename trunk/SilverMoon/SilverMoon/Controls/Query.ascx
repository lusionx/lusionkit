<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Query.ascx.cs" Inherits="SilverMoon.Controls.Query" %>
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
                <th>
                    模次
                </th>
                <td>
                    <asp:DropDownList ID="ddl_type" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    生产时间
                </th>
                <td>
                    <asp:TextBox onClick="WdatePicker()" ID="txt_data" runat="server"></asp:TextBox>
                </td>
                <th>
                    班次
                </th>
                <td>
                    <asp:DropDownList ID="ddl_shift" runat="server">
                    </asp:DropDownList>
                    D:白班/N:夜班
                </td>
            </tr>
            <tr class="nn">
                <th>
                    流水号起始
                </th>
                <td>
                    <asp:TextBox ID="txt_nof" runat="server">1</asp:TextBox>
                </td>
                <th>
                    流水号终止
                </th>
                <td>
                    <asp:TextBox ID="txt_not" runat="server">30</asp:TextBox>
                </td>
            </tr>
        </tbody>
        <tfoot>
            <tr>
                <th>
                </th>
                <td>
                </td>
                <td>
                    <asp:Button ID="btn_query" runat="server" Text="查询" OnClick="btn_query_Click" />
                </td>
                <td>
                </td>
            </tr>
        </tfoot>
    </table>
</fieldset>
