<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Alx.ORM.Code._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>模型生成-Oracle</title>
    <link href="./Css/default.css" rel="stylesheet" type="text/css" />
    <link href="./Css/page.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="./Scripts/jquery-1.4.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        $(function () {
            var gettabs = function () {
                var names = $.map($('.atab :checked'), function (e) {
                    return "'" + $.trim($(e).next().text()) + "'";
                });
                $('#<%=hf.ClientID %>').val(names.join(','));
            };
            gettabs();

            $('.atab :checkbox').click(gettabs);
        });
    </script>
    <div class="tables">
        <h4>
            表名 <i>
                <input type="checkbox" id="cb_all" />全选</i>
            <asp:Button Text="生成" OnClick="btn_g_Click" runat="server" ID="btn_g" />
        </h4>
        <asp:Repeater runat="server" ID="rpt_tables">
            <ItemTemplate>
                <label class="atab">
                    <input type="checkbox" checked="checked" /><i>
                        <%#Eval("table_name")%>
                    </i>
                </label>
            </ItemTemplate>
        </asp:Repeater>
        <div class="clr">
        </div>
        <asp:HiddenField ID="hf" runat="server" />
    </div>
    <xmp>
        <asp:Repeater runat="server" ID="rpt_class" OnItemDataBound="rpt_class_ItemDataBound">
            <ItemTemplate>
[Tabel(Name = "<%# Container.DataItem %>")]
public class <%# Container.DataItem%> : TableBase
{    <asp:Repeater runat="server" id="rpt_pro">
        <ItemTemplate>
    private <%#Eval("systemtype")%> _<%#Eval("name")%>;
    /// <summary>
    /// 
    /// </summary>
    [Column(Name = "<%#Eval("ColumnName")%>", DbType = DbType.String, Nullable = false, IsPrimary = false)]
    public <%#Eval("systemtype")%> <%#Eval("proname")%> { get { return _<%#Eval("name")%>; } set { _<%#Eval("name")%> = value; } }
        </ItemTemplate></asp:Repeater>
}
            </ItemTemplate>
        </asp:Repeater>
    </xmp>
    </form>
</body>
</html>
