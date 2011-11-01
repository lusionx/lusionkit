<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenCode.aspx.cs" Inherits="Alx.ORM.Code.Oracle.GenCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div>
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
    private <%#Eval("systemtype")%><%#Eval("dbnull")%> _<%#Eval("name")%>;
    /// <summary>
    /// <%# Eval("COMMENTS")%>
    /// </summary>
    [Column(Name = "<%#Eval("ColumnName")%>", DbType = <%#Eval("dbtype")%>, Nullable = <%#Eval("nullable")%><%#Eval("pk") %>)]
    public <%#Eval("systemtype")%><%#Eval("dbnull")%> <%#Eval("proname")%> { get { return _<%#Eval("name")%>; } set { _<%#Eval("name")%> = value; } }
        </ItemTemplate></asp:Repeater>
}
            </ItemTemplate>
        </asp:Repeater>
    </xmp>
    </div>
</body>
</html>
