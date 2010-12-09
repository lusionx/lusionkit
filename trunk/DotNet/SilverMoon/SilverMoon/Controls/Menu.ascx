<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="SilverMoon.Controls.Menu" %>
<dl class="func">
    <dt>
        <%=RootMapNodeTile%></dt>
    <dd>
        <div>
            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                <HeaderTemplate>
                    <ul>
                </HeaderTemplate>
                <ItemTemplate>
                    <li><a title="<%# Eval("title") %>" href="<%# Eval("href") %>">
                        <asp:Literal ID="Literal1" Text='<%# Eval("text") %>' runat="server"></asp:Literal></a></li>
                    <asp:Repeater ID="Repeater2" runat="server">
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li><a title="<%# Eval("title") %>" href="<%# Eval("href") %>">
                                <%# Eval("text") %></a></li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </dd>
</dl>
