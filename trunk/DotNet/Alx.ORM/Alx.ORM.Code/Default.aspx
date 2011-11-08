<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Alx.ORM.Code._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>模型生成</title>
</head>
<body>
    <form action="./Oracle/" method="get">
    <div>
        <p>
            工厂串
            <textarea name="facstr" rows="2" cols="20" id="facstr" style="width: 800px;">Oracle.DataAccess,Oracle.DataAccess.Client.OracleClientFactory,Oracle</textarea>
        </p>
        <p>
            连接串
            <textarea name="constr" rows="2" cols="20" id="constr" style="width: 800px;">Data Source=dev;User Id=HR_PPORTAL;Password=1qaz2wsx;</textarea>
        </p>
        <p>
            <input type="submit" value="Oracle" />
        </p>
    </div>
    </form>
</body>
</html>
