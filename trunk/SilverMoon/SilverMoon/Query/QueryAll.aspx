<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"
    CodeBehind="QueryAll.aspx.cs" Inherits="SilverMoon.Query.QueryAll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset class="query">
        <legend>规则</legend>
        <table>
            <tbody>
                <tr>
                    <th>
                        压铸机号
                    </th>
                    <td>
                        <asp:DropDownList ID="ddl_mid" runat="server">
                        </asp:DropDownList>
                    </td>
                    <th>
                        模次
                    </th>
                    <td>
                        <asp:DropDownList ID="ddl_type" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        起始时间
                    </th>
                    <td>
                        <asp:TextBox onClick="WdatePicker()" ID="txt_tf" runat="server"></asp:TextBox>
                    </td>
                    <th>
                        终止时间
                    </th>
                    <td>
                        <asp:TextBox onClick="WdatePicker()" ID="txt_tt" runat="server"></asp:TextBox>
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
                        所属部门
                    </th>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_step" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_step_SelectedIndexChanged">                                    
                                </asp:DropDownList>
                                &nbsp;
                                <asp:DropDownList ID="ddl_st" runat="server">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </tbody>
            <tfoot>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btn_query" runat="server" Text="查询" OnClick="btn_query_Click" />
                    </td>
                    <td>
                    </td>
                </tr>
            </tfoot>
        </table>
    </fieldset>
    <div class="data">
        <p>
            <asp:Localize ID="Localize1" runat="server">不良总数</asp:Localize>
            <asp:Label ID="lb_badt" runat="server" Text=""></asp:Label></p>
        <asp:Repeater ID="Repeater1" runat="server">
            <HeaderTemplate>
                <table class="w100">
                    <thead>
                        <tr>
                            <th>
                                流水号
                            </th>
                            <th>
                                状态
                            </th>
                            <th>
                                说明
                            </th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Eval("FullSerial") %>
                    </td>
                    <td>
                        <%# ShowState(Eval("State"))%>
                    </td>
                    <td>
                        <%# Eval("Remark")%>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody> </table>
            </FooterTemplate>
        </asp:Repeater>
        <webdiyer:AspNetPager CssClass="pager" runat="server" ID="AspNetPager1" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </div>
</asp:Content>
