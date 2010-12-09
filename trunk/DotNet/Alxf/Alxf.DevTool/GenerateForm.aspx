<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="GenerateForm.aspx.cs" Inherits="Alxf.DevTool.GenerateForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <table>
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
                    <asp:TextBox runat="server" ID="txt_typpe" Width="500px">HR.BusinessLayer.Organization</asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                </th>
                <td>
                    <asp:Button runat="server" ID="btn_set" Text="设置" />
                    <asp:Button runat="server" ID="btn_submit" Text="生成代码" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <asp:Repeater runat="server" ID="rpt_pname">
            <ItemTemplate>
                <p>
                    <%# Eval("name") %>
                    <asp:HiddenField runat="server" ID="hf_name" Value='<%# Eval("name") %>' />
                    <asp:RadioButtonList runat="server" ID="rbl_ctype" RepeatLayout="Flow" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">TextBox</asp:ListItem>
                        <asp:ListItem>HiddenField</asp:ListItem>
                        <asp:ListItem>DropDownList</asp:ListItem>
                        <asp:ListItem>RadioButtonList</asp:ListItem>
                    </asp:RadioButtonList>
                </p>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <hr />
    <div>
        <p title="生成的*.aspx">
            <asp:Literal ID="lt_aspx" runat="server"></asp:Literal>
        </p>
        <hr />
        <p>
            <asp:Literal ID="lt_js" runat="server"></asp:Literal>
        </p>
        <hr />
        <p title="生成的*.aspx.cs">
            <asp:Literal ID="lt_cs" runat="server"></asp:Literal>
        </p>
    </div>
</asp:Content>
