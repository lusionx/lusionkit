<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"
    CodeBehind="CreateSerial.aspx.cs" Inherits="SilverMoon.Workflow.CreateSerial" %>

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
                    <th>
                        型次
                    </th>
                    <td>
                        <asp:DropDownList ID="ddl_type" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        月份
                    </th>
                    <td>
                        <asp:DropDownList ID="ddl_month" runat="server">
                        </asp:DropDownList>
                    </td>
                    <th>
                        日期
                    </th>
                    <td>
                        <asp:DropDownList ID="dd_date" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        班次
                    </th>
                    <td>
                        <asp:DropDownList ID="ddl_shift" runat="server">
                        </asp:DropDownList>
                        D:白班/N:夜班
                    </td>
                    <th>
                        年份
                    </th>
                    <td>
                        <asp:DropDownList ID="ddl_year" CssClass="ddlyear" runat="server">                            
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        流水号起始
                    </th>
                    <td>
                        <asp:TextBox ID="txt_nof" runat="server"></asp:TextBox>
                    </td>
                    <th>
                        流水号终止
                    </th>
                    <td>
                        <asp:TextBox ID="txt_not" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
            <tfoot>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btn_create" runat="server" Text="生成" OnClick="btn_create_Click" />
                    </td>
                    <th>
                        <asp:Literal ID="lt_sum" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:Button ID="btn_save" runat="server" Text="保存" OnClick="btn_save_Click" />
                        <asp:Button ID="btn_del" runat="server" Text="删除" OnClick="btn_del_Click" />

                        <script type="text/javascript">
                            $(function() {
                                $(window).bind('beforeunload', function() {
                                    $(":submit").attr('disabled', 'true');
                                    return true;
                                });
                            });
                        </script>

                    </td>
                </tr>
            </tfoot>
        </table>
    </fieldset>
    <div class="data">
        <webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="AspNetPager1">
        </webdiyer:AspNetPager>
        <asp:Repeater ID="Repeater1" runat="server">
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
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody> </table>
            </FooterTemplate>
        </asp:Repeater>
        <webdiyer:AspNetPager CssClass="pager" runat="server" ID="AspNetPager1" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </div>
</asp:Content>
