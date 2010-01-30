<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="blog-ui-header">
    <h1>
        <%=Html.ActionLink(GMSBlog.Web.MvcApplication.BlogTitle, MVC.Home.Index()) %></h1>
    <h2>
        <%=Html.Encode(GMSBlog.Web.MvcApplication.BlogSubtitle) %></h2>
</div>
