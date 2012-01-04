<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcGen.aspx.cs" Inherits="Alx.ORM.Code.Oracle.ProcGen" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <xmp>
    public <%=VorDs%> <%=Request.Params["proc"]%>(<%= MethodParams%>){
            var db = HR.Substructure.DB.HR;
            DbCommand dbCommand = db.GetStoredProcCommand("<%=Request.Params["proc"]%>");
            <asp:Repeater runat="server" ID="rpt_pars">
            <ItemTemplate>
            db.Add<%#Eval("in_out").ToString()[0].ToString() + Eval("in_out").ToString().ToLower().Substring(1)%>Parameter(dbCommand, "<%#Eval("argument_name")%>", <%#GetDbType(Eval("data_type").ToString())%>, <%#Eval("argument_name")%>);</ItemTemplate>
            </asp:Repeater>

            DataSet ds = db.ExecuteDataSet(dbCommand);
            pagesum = Int32.Parse(dbCommand.Parameters[":pagesum"].Value.ToString());
            return db.ExecuteDataSet(dbCommand); 
    }
    </xmp>
       
    </div>
    </form>
</body>
</html>
