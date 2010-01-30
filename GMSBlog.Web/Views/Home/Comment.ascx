<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GMSBlog.Model.Entities.Comment>" %>
<div class="blog-ui-comment">
    <% =Html.RenderMarkdown(Model.Content)%>
    <span><%= Html.GetCommentDescriptionString(Model) %></span>
</div>
