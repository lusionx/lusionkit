<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="SLWeb.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">

        $(function() {
            $('#hello').text("Welcome " + $('#id').val());
        })

        $(function() {
            GetMsg();
        })

        function GetMsg() {
            var userid = $('#id').val();
            //SLWeb.WS.Message.GetInvite(userid, ShowInvite);
            $.postJSON("{to:'" + userid + "'}", "/WS/Message.asmx/GetInvite", ShowInvite);
            setTimeout(GetMsg, 5000);
        }

        function ShowInvite(msg) {
            $('#inf').html('');
            $.each(msg.d, function() {
                var al = new Array();
                for (ee in this) {
                    al.push(ee);
                }
                var jq = $('#inf');
                jq.append('<br/>');
                jq.append(this[al[0]]);
                var msgid = this[al[1]];
                jq.append(String.format('<input type="button" value="接受" onclick="alert(\'{0}\')" />', msgid));                
            })
        }
    </script>

    <input id="Button1" type="button" value="button" onclick="alert('aa')" />
    <div id="hello">
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeader="False"
        OnSelectedIndexChanging="GridView1_SelectedIndexChanging">
        <Columns>
            <asp:BoundField DataField="UserID" />
            <asp:CheckBoxField DataField="Leisure" />
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select"
                        Text="邀请" CommandArgument='<%#Eval("UserID") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div id="inf">
    </div>
</asp:Content>
