<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Func.Master" AutoEventWireup="true"
    CodeBehind="EditWidget.aspx.cs" Inherits="Perpor.Web.Func.EditWidget" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:DataList ID="dl_wdg" CssClass="wdgadd" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
        ShowFooter="False" ShowHeader="False" HorizontalAlign="Center">
        <ItemStyle Width="200px" CssClass="blk" />
        <ItemTemplate>
            <h3>
                <%# Eval("Title")%>
            </h3>
            <br />
            <p>
                <%# Eval("Description")%>
            </p>
            <br />
            <input type="button" class="btn" arg="<%# Eval("FileName") %>" value="�������" />
        </ItemTemplate>
    </asp:DataList>

    <script type="text/javascript">
        $(function() {
            $('.wdgadd .btn').click(add);
        });
        function add() {
            var el = $(this);
            el.hide().after('<h3/>').next().text('�������...');
            $.post(appPath + 'Handlers/AddWidget.ashx', { name: el.attr('arg') }, function(result) {
                if (result == 1) {
                    el.next().text('��ӳɹ�');
                } else {
                    el.next().text('���ʧ��');
                }
            });
        }
    </script>

</asp:Content>
