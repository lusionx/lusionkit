<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Widget.ascx.cs" Inherits="Perpor.Web.Controls.Widget" %>
<li class="<%=this.LiClass %>" ctr="<%=this.ControlName %>">
    <div class="widget-head">
        <h3>
            <%=this.H3 %></h3>
    </div>
    <div class="widget-content">
        <p>
            <asp:PlaceHolder ID="content" runat="server"></asp:PlaceHolder>
        </p>
    </div>
</li>
