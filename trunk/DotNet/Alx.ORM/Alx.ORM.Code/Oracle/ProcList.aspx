<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcList.aspx.cs" Inherits="Alx.ORM.Code.Oracle.ProcList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>存储过程列表</title>
    <link href="../Css/default.css" rel="stylesheet" type="text/css" />
    <link href="../Css/page.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.5.2.min.js"></script>
</head>
<body>
    <script type="text/javascript">
        $(function () {
            $('a.op-proc').click(function () {
                var obj = { proc: $.trim($(this).text()), constr: $('#<%=constr.ClientID %>').val(), facstr: $('#<%=facstr.ClientID %>').val() };
                window.open('ProcGen.aspx?' + $.param(obj), '_blank');
            });
        });
    </script>
    <form id="form1" runat="server">
    <div>
        <p class="nn">
            工厂串<asp:TextBox runat="server" TextMode="MultiLine" Width="800px" ID="facstr" />
        </p>
        <p class="nn">
            连接串
            <asp:TextBox runat="server" ID="constr" TextMode="MultiLine" Width="800px" />
        </p>
        <div class="procs">
            <asp:Repeater runat="server" ID="rpt_procs">
                <ItemTemplate>
                    <p>
                        <a href="#" class="op-proc">
                            <%#Eval("object_name")%></a>
                    </p>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    </form>
</body>
</html>
