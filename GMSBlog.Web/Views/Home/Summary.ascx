<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GMSBlog.Model.Entities.Post>" %>

<div class="blog-ui-summary">
    <a href="<%=Url.Action(MVC.Home.Post(Model.Id)) %>"><h2><%=Html.Encode(Model.Title) %></h2></a>
    <div class="blog-ui-summary-content">
    <p><%=Html.Encode(Model.Summary) %></p>
    <%Html.RenderPartial("BottomSection", Model); %>
    </div>
</div>