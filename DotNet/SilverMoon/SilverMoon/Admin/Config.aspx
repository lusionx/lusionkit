<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"
    CodeBehind="Config.aspx.cs" Inherits="SilverMoon.Admin.Config" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        $(function() {
            $('a.edit').click(function() {
                var frm = $('#sfrm');
                var input = frm.find("input:text").val('');
                frm.hide().show('slow');
                input.eq(0).val($(this).attr('id'));
                input.eq(1).val($(this).attr('name'));
            });
        });
    </script>

    <div class="data">
        <asp:DropDownList ID="ddl_group" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_group_SelectedIndexChanged">
            <asp:ListItem Value="10">DC</asp:ListItem>
            <asp:ListItem Value="20">FI浇口</asp:ListItem>
            <asp:ListItem Value="30">FI外观</asp:ListItem>
            <asp:ListItem Value="40">检查系</asp:ListItem>
            <asp:ListItem Value="50">TFTE</asp:ListItem>
        </asp:DropDownList>
        <asp:Repeater ID="Repeater1" runat="server">
            <HeaderTemplate>
                <table class="w100">
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Eval("Name")%>
                    </td>
                    <td>
                        <a href="" class="edit" name="<%# Eval("Name")%>" id="<%# Eval("ID")%>">编辑</a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody> </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <fieldset id="sfrm" class="query nn">
        <legend>修改名称</legend>
        <table>
            <tbody>
                <tr>
                    <th>
                        新名称<asp:TextBox ID="txt_id" CssClass="nn" runat="server"></asp:TextBox>
                    </th>
                    <td>
                        <asp:TextBox Width="150px" CssClass="txt" ValidationGroup="Add" ID="txt_newst" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
            <tfoot>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btn_ok" ValidationGroup="Add" runat="server" Text="确认" OnClick="btn_ok_Click" />
                        <input onclick="$('#sfrm').hide('slow');" class="btn" type="button" value="取消" />
                    </td>
                </tr>
            </tfoot>
        </table>
    </fieldset>
</asp:Content>
