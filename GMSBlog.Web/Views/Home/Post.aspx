<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<GMSBlog.Model.Entities.Post>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Post
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="blog-ui-post">
    <h2><%=Html.Encode(Model.Title) %></h2>
    <div class="blog-ui-post-content">
    <p><em><%=Html.Encode(Model.Summary) %></em></p>
    
    <%=Html.RenderMarkdown(Model.Content) %>
    <%Html.RenderPartial("BottomSection", Model); %>
    </div>
    </div>
    <%Html.RenderPartial("AddComment", new GMSBlog.Web.Models.PostComment(Model.Id, new GMSBlog.Model.Entities.Comment())); %>
    <h3>Comments</h3>
    <% foreach (var comment in Model.Comments)
       { %>
    <%Html.RenderPartial("Comment", comment);%>
    <% } %>
    

</asp:Content>
