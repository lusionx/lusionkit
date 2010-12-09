<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"
    CodeBehind="wf70.aspx.cs" Inherits="SilverMoon.Workflow.wf70" %>

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
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <th>
                        类型
                    </th>
                    <td>
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <th>
                        月份
                    </th>
                    <td>
                        <asp:DropDownList ID="DropDownList3" runat="server">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <th>
                        日期
                    </th>
                    <td>
                        <asp:DropDownList ID="DropDownList4" runat="server">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <th>
                        班次
                    </th>
                    <td>
                        <asp:DropDownList ID="DropDownList5" runat="server">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <th>
                        流水号
                    </th>
                    <td>
                        起始<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        终止<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
            <tfoot>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="Button2" runat="server" Text="查询" />
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
                <table>
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
                        11H04D003
                    </td>
                    <td>
                        合格
                    </td>
                    <td>
                        ....
                    </td>
                    <td>
                        <a href="" class="edit">编辑</a>
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
    <div class="nn sblock">
        <table>
            <tbody>
                <tr>
                    <th>
                        流水号
                    </th>
                    <td>
                        <asp:TextBox CssClass="txtView" ID="TextBox3" runat="server">11H04D003</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        状态
                    </th>
                    <td>
                        <asp:DropDownList ID="DropDownList6" runat="server">
                            <asp:ListItem>状态A</asp:ListItem>
                            <asp:ListItem>状态B</asp:ListItem>
                            <asp:ListItem>状态C</asp:ListItem>
                            <asp:ListItem>状态D</asp:ListItem>
                            <asp:ListItem>状态E</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        说明
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
            <tfoot>
                <tr>
                    <th>
                    </th>
                    <td>
                        <input class="submit btn" type="button" value="提交" />
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>

    <script type="text/javascript">
        $(function() {
            $('a.edit').click(function() {
                $.blockUI({
                    message: $('.sblock').eq(0)
                });
                $('.blockOverlay').attr('title', '点击背景取消').click($.unblockUI);
            });

            $('.sblock .submit').click(function() {
                $('#<%=Button1.ClientID %>').click();
            });
        });
       
    
    </script>

    <asp:Button CssClass="nn" ID="Button1" runat="server" Text="提交" />
</asp:Content>
