<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"
    CodeBehind="wf30.aspx.cs" Inherits="SilverMoon.Workflow.wf30" %>

<%@ Register Src="~/Controls/Query.ascx" TagName="Query" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DropDownList ID="ddl_year" CssClass="ddlyear" runat="server">
    </asp:DropDownList>
    <asp:TextBox CssClass="txt ltsp" ID="txt_id" runat="server"></asp:TextBox>
    <input type="button" value="登录" id="btn_ck" class="btn" />
    <span id="sp_op">
        <asp:DropDownList ID="ddl_state" runat="server" CssClass="slct">
            <asp:ListItem Value="0">良品</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btn" runat="server" Text="确定" CssClass="btn" OnClick="btn_Click" />
    </span>
    <div class="data">
        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
            <HeaderTemplate>
                <table class="w100">
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
                            <th>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Eval("FullSerial") %>
                    </td>
                    <td>
                        <%# ShowState(Eval("State"))%>
                    </td>
                    <td>
                        <%# Eval("Remark")%>
                    </td>
                    <td>
                        <asp:LinkButton OnClientClick="return confirm('确定删除吗?');" ID="lbtn" CommandName="Del"
                            CommandArgument='<%# Eval("FullSerial") %>' runat="server">删除</asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody> </table>
            </FooterTemplate>
        </asp:Repeater>
        <webdiyer:AspNetPager CssClass="pager" runat="server" ID="AspNetPager1" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </div>
    <script type="text/javascript">
        $(function () {
            $('#btn_ck').click(getData);
            $('#sp_op').hide();
        });
        function getData() {
            var val = $('#<%=txt_id.ClientID %>').val().trim();
            if (val.length != 9) {
                alert('<asp:Localize runat="server">编号长度错误</asp:Localize>');
                return null;
            }
            $.post(AppPath + 'Handler/GetOne.ashx', {
                id: val
            },
            function (json) {
                var sid = json.split(',')[0];
                var sname = json.split(',')[1];
                //alert(sid);
                if (sid == '') {
                    alert('<asp:Localize runat="server">无此编号</asp:Localize>');
                    return null;
                }
                if (canUpdate(sid)) {
                    $('#sp_op').show();
                } else {
                    alert('<asp:Localize runat="server">不可改变的产品</asp:Localize>');
                    $('#sp_op').hide();
                }
            });
        }
        function canUpdate(st) {
            if (st == 0) {
                return true;
            }
            var arr = $('#<%=ddl_state.ClientID %>').children();
            for (var i = 0; i < arr.length; i++) {
                if (arr[i].value == st) {
                    return true;
                }
            }
            return false;
        }
    </script>
</asp:Content>
