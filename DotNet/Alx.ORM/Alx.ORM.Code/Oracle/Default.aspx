<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Alx.ORM.Code.Oracle._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>模型生成-Oracle</title>
    <link href="/Css/default.css" rel="stylesheet" type="text/css" />
    <link href="/Css/page.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.4.1.min.js"></script>
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
        <p>
            <table>
                <caption>
                    oracle 数据库类型对应</caption>
                <thead>
                    <tr>
                        <th>
                            C#类型
                        </th>
                        <th>
                            DbType
                        </th>
                        <th>
                            oracle type
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            Guid
                        </td>
                        <td>
                            DbType.Binary
                        </td>
                        <td>
                            raw(16)
                        </td>
                    </tr>
                    <tr>
                        <td>
                            string
                        </td>
                        <td>
                            DbType.String
                        </td>
                        <td>
                            char,varchar2
                        </td>
                    </tr>
                    <tr>
                        <td>
                            decimal
                        </td>
                        <td>
                            DbType.Decimal
                        </td>
                        <td>
                            NUMBER(x,y)
                        </td>
                    </tr>
                    <tr>
                        <td>
                            datetime
                        </td>
                        <td>
                            DbType.datetime
                        </td>
                        <td>
                            date
                        </td>
                    </tr>
                </tbody>
            </table>
        </p>
        <p>
            工厂串<asp:TextBox Width="800px" runat="server" TextMode="MultiLine" ID="facstr">Oracle.DataAccess, Oracle.DataAccess.Client.OracleClientFactory,Oracle</asp:TextBox>
        </p>
        <p>
            连接串<asp:TextBox Width="800px" runat="server" TextMode="MultiLine" ID="constr">Data Source=guolei;User Id=HR_PPORTAL;Password=1qaz2wsx;</asp:TextBox>
        </p>
        <h4>
            表名 <i>
                <input type="checkbox" id="cb_all" />全选</i>
            <asp:Button Text="生成" OnClick="btn_g_Click" runat="server" ID="btn_g" />
        </h4>
        <p>
            <asp:Repeater runat="server" ID="rpt_tables">
                <ItemTemplate>
                    <label class="atab" title="<%#Eval("COMMENTS")%>">
                        <input type="checkbox" checked="checked" /><i>
                            <%#Eval("table_name")%>
                        </i><code>
                            <%#Eval("COMMENTS")%></code>
                    </label>
                </ItemTemplate>
            </asp:Repeater>
        </p>
        <div class="clr">
        </div>
        <asp:HiddenField ID="hf" runat="server" />
    </div>
    <xmp>
        <asp:Repeater runat="server" ID="rpt_class" OnItemDataBound="rpt_class_ItemDataBound">
            <ItemTemplate>
/// <summary>
/// <%# Eval("COMMENTS")%>
/// </summary>
[Tabel(Name = "<%# Eval("TABLE_NAME") %>")]
public class <%# Eval("TABLE_NAME")%> : TableBase
{    <asp:Repeater runat="server" id="rpt_pro">
        <ItemTemplate>
    private <%#Eval("systemtype")%> _<%#Eval("name")%>;
    /// <summary>
    /// <%# Eval("COMMENTS")%>
    /// </summary>
    [Column(Name = "<%#Eval("ColumnName")%>", DbType = DbType.String, Nullable = <%#Eval("nullable")%>)]
    public <%#Eval("systemtype")%> <%#Eval("proname")%> { get { return _<%#Eval("name")%>; } set { _<%#Eval("name")%> = value; } }
        </ItemTemplate></asp:Repeater>
}
            </ItemTemplate>
        </asp:Repeater>
    </xmp>
    </form>
</body>
</html>
