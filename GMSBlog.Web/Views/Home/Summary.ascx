<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GMSBlog.Model.Entities.Post>" %>

<div class="blog-ui-summary">
    <a href="<%=Url.Action(MVC.Home.Post(Model.Id)) %>"><h2><%=Html.Encode(Model.Title) %></h2></a>
    <p><%=Html.Encode(Model.Summary) %></p>
    <span><%=Html.Encode(Model.Comments.Count == 1 ? Model.Comments.Count + " comment" : Model.Comments.Count + " comments") %></span>
    <span>Posted on <%=Html.Encode(Model.DateCreated.ToString("dd MMMM yyyy h:mm tt")) %></span>
</div>