<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="GenerateBLL.aspx.cs" Inherits="Alxf.DevTool.GenerateBLL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <table>
            <tr>
                <td colspan="2">
                    从数据模型生成业务模型,此工具只是减少部分手工代码书写
                </td>
            </tr>
            <tr>
                <th>
                    上传DLL
                </th>
                <td>
                    <asp:FileUpload runat="server" ID="fileup" Width="500px" />
                </td>
            </tr>
            <tr>
                <th>
                    类型
                </th>
                <td>
                    <asp:TextBox runat="server" ID="txt_typpe" Width="500px">Alxf.DAL.Demo.Video.VideoObj</asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                </th>
                <td>
                    <asp:Button runat="server" ID="btn_submit" Text="生成代码" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Literal runat="server" ID="lt_cs"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
