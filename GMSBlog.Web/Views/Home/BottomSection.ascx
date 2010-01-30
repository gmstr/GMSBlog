<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GMSBlog.Model.Entities.Post>" %>
<div class="blog-ui-bottom-section">
    <span class="blog-ui-comments">
        <%=Html.Encode(Model.Comments.Count == 1 ? Model.Comments.Count + " comment" : Model.Comments.Count + " comments") %></span>
    <span class="blog-ui-postedon">Posted on
        <%=Html.Encode(Model.DateCreated.ToString("dd MMMM yyyy h:mm tt")) %></span>
</div>
