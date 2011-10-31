<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Alx.ORM.Code.Oracle._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>模型生成-Oracle</title>
    <link href="../Css/default.css" rel="stylesheet" type="text/css" />
    <link href="../Css/page.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.5.2.min.js"></script>
</head>
<body>
    <form runat="server" id="form1">
    <script type="text/javascript">
        $(function () {
            var gettabs = function () {
                var names = $.map($('.atab :checked'), function (e) {
                    return "'" + $.trim($(e).next().text()) + "'";
                });
                names = names.join(',');
                $('#<%=hf.ClientID %>').val(names);
                return names;
            };
            $('#btn').click(function () {
                var obj = { constr: $('#<%=constr.ClientID %>').val(), facstr: $('#<%=facstr.ClientID %>').val(), tabs: gettabs() };
                window.open('GenCode.aspx?' + $.param(obj), '_blank');
            });
            $('#cb_all').click(function () {
                var ck = this.checked;
                $('.atab :checkbox').each(function () {
                    this.checked = ck;
                });
            });
            $('#cb_rall').click(function () {
                $('.atab :checkbox').each(function () {
                    this.checked = !this.checked;
                });
            });
            $('a.op-gena').click(function () {
                var obj = { constr: $('#<%=constr.ClientID %>').val(), facstr: $('#<%=facstr.ClientID %>').val(), tabs: "'" + $.trim($(this).text()) + "'" };
                window.open('GenCode.aspx?' + $.param(obj), '_blank');
            });
        });
    </script>
    <div class="tables">
        <asp:HiddenField ID="hf" runat="server" />
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
            工厂串<asp:TextBox runat="server" TextMode="MultiLine" Width="800px" Text="Oracle.DataAccess, Oracle.DataAccess.Client.OracleClientFactory,Oracle"
                ID="facstr" />
        </p>
        <p>
            连接串
            <asp:TextBox runat="server" ID="constr" TextMode="MultiLine" Width="800px" Text="Data Source=guolei;User Id=HR_PPORTAL;Password=1qaz2wsx;" />
        </p>
    </form>
    <h4>
        表名 <i>
            <input type="checkbox" id="cb_all" />全选<input type="checkbox" id="cb_rall" />反选</i>
        <input type="button" id="btn" value="生成" />
    </h4>
    <p>
        <asp:Repeater runat="server" ID="rpt_tables">
            <ItemTemplate>
                <label class="atab" title="<%#Eval("COMMENTS")%>">
                    <input type="checkbox" /><i><a href="#" class="op-gena">
                        <%#Eval("table_name")%></a> </i><code>
                            <%#Eval("COMMENTS")%></code>
                </label>
            </ItemTemplate>
        </asp:Repeater>
    </p>
    </div> </form>
</body>
</html>
