<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"
    CodeBehind="DelData.aspx.cs" Inherits="SilverMoon.Admin.DelData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset class="query">
        <legend>规则</legend>
        <table>
            <tbody>
                <tr>
                    <th>
                        起始时间
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="txt_tf" onfocus="WdatePicker()" />
                    </td>
                    <th>
                        终止时间
                    </th>
                    <td>
                        <asp:TextBox runat="server" onfocus="WdatePicker()" ID="txt_tt" />
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
                    <th colspan="2">
                        为防止数据丢失,删除数据前请备份数据库,或导出数据
                    </th>
                    <td>
                    </td>
                    <th>
                        <asp:Button ID="btn_de" OnClientClick="return checkAll();" runat="server" 
                            Text="删除" onclick="btn_de_Click" />
                    </th>
                </tr>
            </tfoot>
        </table>
    </fieldset>
    <script type="text/javascript">
        var checkAll = function () {
            var jd = $('#<%=txt_tf.ClientID%>');
            if (jd.val() == '') {
                alert("起始时间不能为空");
                jd.focus();
                return false;
            }
            jd=$('#<%=txt_tt.ClientID%>');
            if (jd.val() == '') {
                alert("终止时间不能为空");
                jd.focus();
                return false;
            }

            return confirm("确认删除吗?");
        };
    </script>
</asp:Content>
