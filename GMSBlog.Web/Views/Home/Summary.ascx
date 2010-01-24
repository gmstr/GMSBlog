<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GMSBlog.Model.Entities.Post>" %>

<div class="blog-ui-summary">
    <h3><%=Html.Encode(Model.Title) %></h3>
    <p><%=Html.Encode(Model.Summary) %></p>
    <span><%=Html.Encode(Model.Comments.Count == 1 ? Model.Comments.Count + " comment" : Model.Comments.Count + " comments") %></span>
    <span>Posted on <%=Html.Encode(Model.DateCreated.ToString("dd MMMM yyyy h:mm tt")) %></span>
</div>