<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="postList.aspx.cs" Inherits="Alxf.WebDemo.postList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>

    <script type="text/javascript" src="../Js/jquery-1.3.2.min.js"></script>

    <script type="text/javascript">
    
        //var ss=$(".form").find('input:text,input:password,input:checkbox,input:radio,input:hiden,textarea,select');
    </script>

</head>
<body>
    <form id="form1" runat="server">

    <script type="text/javascript">
    $(function(){
        $.get('postOne.aspx',{},function(page){
            var dd = $('#form').append($('#form',page).children());
        });
        $('#sub').click(function(){         
           var obj = $('#form').postObj();
           $.post('postOne.aspx',obj,function(page){
            
           });
        });
    });
    </script>

    <div id="form">
    </div>
    <input type="button" id="sub" value="ajaxsubmit" />
    <fieldset>
        <legend>Bind Mode </legend>
        <p>
            <asp:TextBox ID="tf_Name" runat="server"></asp:TextBox>
            <asp:DropDownList ID="tf_Age" runat="server">
                <asp:ListItem>12</asp:ListItem>
                <asp:ListItem>22</asp:ListItem>
            </asp:DropDownList>
            <asp:HiddenField ID="tf_Xx" runat="server" />
            
            <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
        </p>
    </fieldset>
    </form>
</body>
</html>
