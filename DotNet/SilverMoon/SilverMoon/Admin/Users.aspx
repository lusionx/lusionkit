<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"
    CodeBehind="Users.aspx.cs" Inherits="SilverMoon.Admin.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        $(function() {
            $('a.showfrm').click(function() {
                var frm = $('#sfrm');
                frm.find("input:password").val('');
                var input = frm.find('input:first').val('');
                frm.hide().show('slow');
                var name = $(this).parent().prev().text().trim();
                if (name == '') {
                    input.attr('contenteditable', 'true');
                } else {
                    input.val(name).attr('contenteditable', 'false');
                }
            });
        });
    </script>

    <div class="data">
        <a class="showfrm" href="">新建用户</a>
        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
            <HeaderTemplate>
                <table class="w100">
                    <thead>
                        <tr>
                            <th>
                                用户名
                            </th>
                            <th>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%#Eval("UserName") %>
                    </td>
                    <td>
                        <a class="showfrm" href="">修改密码</a>
                        <asp:LinkButton ID="btn_del" OnClientClick="return confirm('确定吗?');" CommandName="Delete"
                            CommandArgument='<%#Eval("UserName") %>' runat="server">删除</asp:LinkButton>
                        <asp:LinkButton ID="btn_role" CommandName="Role" CommandArgument='<%#Eval("UserName") %>'
                            runat="server">编辑角色</asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody> </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <asp:Panel ID="pl_role" runat="server">
        <fieldset id="Fieldset1" class="query">
            <legend>
                <asp:Literal ID="lt_u" runat="server"></asp:Literal>&nbsp;角色列表</legend>
            <table>
                <tbody>
                    <tr>
                        <td>
                            <asp:CheckBox Text="Admin" ID="CheckBox5" runat="server" />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox Text="DC" ID="CheckBox1" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox Text="FI" ID="CheckBox2" runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox Text="JCH" ID="CheckBox3" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox Text="TFTE" ID="CheckBox4" runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btn_role" runat="server" Text="确认" onclick="btn_role_Click" />
                            <input onclick="$('#Fieldset1').hide('slow');" class="btn" type="button" value="取消" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </tfoot>
            </table>
        </fieldset>
    </asp:Panel>
    <fieldset id="sfrm" class="query nn">
        <legend>账户信息</legend>
        <table>
            <tbody>
                <tr>
                    <th>
                        用户名
                    </th>
                    <td>
                        <asp:TextBox Width="150px" ID="txt_name" ValidationGroup="Add" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="Add" ID="RequiredFieldValidator1" runat="server"
                            ControlToValidate="txt_name" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th>
                        原始密码
                    </th>
                    <td>
                        <asp:TextBox Width="150px" CssClass="txt" ValidationGroup="Add" TextMode="Password"
                            ID="txt_opwd" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;新建用户可为空
                    </td>
                </tr>
                <tr>
                    <th>
                        密码
                    </th>
                    <td>
                        <asp:TextBox Width="150px" CssClass="txt" ValidationGroup="Add" TextMode="Password"
                            ID="txt_pwd" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="Add" ID="RequiredFieldValidator2" runat="server"
                            ControlToValidate="txt_pwd" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th>
                        密码确认
                    </th>
                    <td>
                        <asp:TextBox Width="150px" CssClass="txt" ValidationGroup="Add" TextMode="Password"
                            ID="txt_pwd2" runat="server"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" ValidationGroup="Add" runat="server"
                            ControlToCompare="txt_pwd" ControlToValidate="txt_pwd2" ErrorMessage="两次密码输入不同"></asp:CompareValidator>
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
