<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"
    CodeBehind="wf10.aspx.cs" Inherits="SilverMoon.Workflow.wf10" %>

<%@ Register Src="~/Controls/Query.ascx" TagName="Query" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:Query ID="Query1" runat="server" />
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
                            <th>
                                说明
                            </th>
                            <th>
                                操作
                            </th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class='<%# Eval("State").ToString()=="0"?"":"orange" %>'>
                    <td>
                        <span class="dr">
                            <%# Eval("FullSerial") %></span>
                    </td>
                    <td>
                        <%# ShowState(Eval("State"))%><span class="dr nn"><%# Eval("State") %></span>
                    </td>
                    <td>
                        <span class="dr">
                            <%# Eval("Remark")%></span>
                    </td>
                    <td>
                        <a href="" class="edit" can="<%# CanUpdate(Eval("State")) %>">编辑</a>
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
    <div id="frm" class="sblock nn">
        <table class="w100">
            <tbody>
                <tr>
                    <th>
                        流水号
                    </th>
                    <td>
                        <asp:DropDownList ID="ddl_year" CssClass="ddlyear" runat="server">
                        </asp:DropDownList>
                        <asp:TextBox ID="txt_sno" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        状态
                    </th>
                    <td>
                        <asp:DropDownList ID="ddl_state" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        说明
                    </th>
                    <td>
                        <asp:TextBox ID="txt_remark" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
            <tfoot>
                <tr>
                    <th>
                    </th>
                    <td>
                        <asp:Button CssClass="btn" ID="btn_update" runat="server" Text="提交" OnClick="btn_update_Click" />
                        <input class="btn" type="button" value="取消" />
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
    <script type="text/javascript">
        $(function () {
            $('a.edit[can="False"]').attr("disabled", "disabled");
            $('a.edit[can="True"]').click(function () {
                var msg = $('#frm');
                var vs = $(this).parent().parent().find('span.dr');
                $('#<%=txt_sno.ClientID %>').val(vs.eq(0).text().trim()).attr('disabled', 'disabled');
                $('#<%=ddl_state.ClientID %>').val(vs.eq(1).text().trim());
                $('#<%=txt_remark.ClientID %>').val(vs.eq(2).text().trim());
                $.blockUI({
                    message: msg
                });
            });

            $('.sblock .btn').click(function () {
                $.unblockUI({
                    onUnblock: function () {
                        $('#<%=txt_sno.ClientID %>').removeAttr('disabled');
                    },
                    fadeOut: 0
                });
            });

        });
        
    </script>
</asp:Content>
