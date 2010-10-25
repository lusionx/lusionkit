<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"
    CodeBehind="App.aspx.cs" Inherits="SilverMoon.Admin.App" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="data">
        <table class="w100">
            <thead>
                <tr>
                    <th>
                        说明
                    </th>
                    <th>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        清理临时文件
                    </td>
                    <td>
                        <asp:LinkButton ID="btn_clr" runat="server" onclick="btn_clr_Click">确定</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        备份数据库
                    </td>
                    <td>
                        <asp:LinkButton ID="btn_bak" runat="server" onclick="btn_bak_Click">确定</asp:LinkButton>
                    </td>
                </tr>
                <tr class="nn">
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
