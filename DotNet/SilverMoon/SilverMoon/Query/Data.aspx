<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"
    CodeBehind="Data.aspx.cs" Inherits="SilverMoon.Query.Data" %>

<%@ Import Namespace="SilverMoon.BAL" %>
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
                        模次
                    </th>
                    <td>
                        <asp:DropDownList ID="ddl_type" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        起始时间
                    </th>
                    <td>
                        <asp:TextBox onClick="WdatePicker()" ID="txt_tf" runat="server"></asp:TextBox>
                    </td>
                    <th>
                        终止时间
                    </th>
                    <td>
                        <asp:TextBox onClick="WdatePicker()" ID="txt_tt" runat="server"></asp:TextBox>
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
                    </th>
                    <td>
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
                        <asp:Button ID="btn_query" runat="server" Text="查询" OnClick="btn_query_Click" />
                    </td>
                    <th>
                        <asp:Button ID="btn_down" runat="server" OnClick="btn_Click" Text="下载" />
                    </th>
                </tr>
            </tfoot>
        </table>
    </fieldset>
    <div class="data">
        <webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="AspNetPager1">
        </webdiyer:AspNetPager>
        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
            <HeaderTemplate>
                <table class="w100">
                    <thead>
                        <tr>
                            <th>
                                日期(<asp:Label ID="lb_1" runat="server" Text=""></asp:Label>)
                            </th>
                            <th>
                                生产总数
                            </th>
                            <th>
                                良品数
                            </th>
                            <th>
                                不良品数
                            </th>
                            <th>
                                不良率
                            </th>
                            <th>
                                比例
                            </th>
                            <th>
                                分布图
                            </th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <a href="" class="list">
                            <%# Eval("Date","{0:yyyy-MM-dd}")%></a>
                    </td>
                    <td>
                        <%# Eval("ZongShu")%>
                    </td>
                    <td>
                        <%# Eval("Good")%>
                    </td>
                    <td>
                        <%# Eval("Bad")%>
                    </td>
                    <td>
                        <%# (Convert.ToSingle(Eval("Bad"))*100 / Convert.ToSingle(Eval("ZongShu"))).ToString("f2")%>%
                    </td>
                    <td>
                        <a href="" class="edit">比例</a>
                    </td>
                    <td>
                        <a href="" class="fenbu">分布图</a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                <tr>
                    <td>
                        合计
                    </td>
                    <td>
                        <asp:Label ID="lb_1" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lb_2" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lb_3" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lb_4" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:HyperLink CssClass="edit" Visible="false" ID="hlk_1" runat="server">比例</asp:HyperLink>
                    </td>
                    <td>
                    </td>
                </tr>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <webdiyer:AspNetPager CssClass="pager" runat="server" ID="AspNetPager1" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </div>
    <img alt="" id="imgpie" class="nn" style="width: 500px; height: 200px" src="" />
    <div id="wplist" style="width: 690px; height: 400px; overflow: scroll;" class="nn">
    </div>
    <div id="fenbutu" style="width: 950px; height: 150px; overflow: auto;" class="nn">
    </div>

    <script type="text/javascript">
        $(function() {
            $('a.edit').click(function() {
                var msg = $('#imgpie');
                var date = $(this).parent().parent().children('td').eq(0).text().trim();
                var total = $(this).parent().parent().children('td').eq(1).text().trim();
                $.post(AppPath + 'Handler/Chart.ashx',
                {
                    Day: date,
                    Total: total
                },
                 function(str) {
                     msg.attr('src', str);
                 });
                $.blockUI({
                    message: msg,
                    css: {
                        top: ($(window).height() - msg.height()) / 2 + 'px',
                        left: ($(window).width() - msg.width()) / 2 + 'px',
                        width: msg.width(),
                        height: msg.height(),
                        cursor: 'default'
                    }
                });
                $('.blockOverlay').attr('title', '点击背景取消').click(function() {
                    $.unblockUI();
                    msg.attr('src', '');
                });
            });
        });      
    
    </script>

    <script type="text/javascript">
        $(function() {
            $('a.list').click(function() {
                var msg = $('#wplist');
                var date = $(this).parent().parent().children('td').eq(0).text().trim();
                $.get('ListByDate.aspx',
                {
                    time: date,
                    xx: new Date()
                },
                 function(page) {
                     msg.append($('#context', page));
                 });
                $.blockUI({
                    message: msg,
                    css: {
                        top: ($(window).height() - msg.height()) / 2 + 'px',
                        left: ($(window).width() - msg.width()) / 2 + 'px',
                        width: msg.width(),
                        height: msg.height(),
                        cursor: 'default'
                    }
                });
                $('.blockOverlay').attr('title', '点击背景取消').click(function() {
                    $.unblockUI();
                    msg.empty();
                });
            });
        });      
    
    </script>

    <script type="text/javascript">
        $(function() {
            $('a.fenbu').click(function() {
                var msg = $('#fenbutu');
                var date = $(this).parent().parent().children('td').eq(0).text().trim();
                $.get('FenBuTu.aspx',
                {
                    time: date,
                    xx: new Date()
                },
                 function(page) {
                     msg.append($('#context', page));
                 });
                $.blockUI({
                    message: msg,
                    css: {
                        top: ($(window).height() - msg.height()) / 2 + 'px',
                        left: ($(window).width() - msg.width()) / 2 + 'px',
                        width: msg.width(),
                        height: msg.height(),
                        cursor: 'default'
                    }
                });
                $('.blockOverlay').attr('title', '点击背景取消').click(function() {
                    $.unblockUI();
                    msg.empty();
                });
            });
        });      
    
    </script>

</asp:Content>
