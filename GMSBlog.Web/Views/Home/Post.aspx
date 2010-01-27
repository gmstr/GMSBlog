<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<GMSBlog.Model.Entities.Post>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Post
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Html.Encode(Model.Title) %></h2>
    <p><em><%=Html.Encode(Model.Summary) %></em></p>
    
    <%=Html.RenderMarkdown(Model.Content) %>
    
    <h3>Comments</h3>
    <% foreach (var comment in Model.Comments)
       { %>
    <%Html.RenderPartial("Comment", comment);%>
    <% } %>
    

</asp:Content>
