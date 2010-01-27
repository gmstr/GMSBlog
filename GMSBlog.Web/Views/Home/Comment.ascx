<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GMSBlog.Model.Entities.Comment>" %>
<div class="blog-ui-comment">
    <% =Html.RenderMarkdown(Model.Content)%>
    <span><%= string.IsNullOrEmpty(Model.Website) 
              ? Html.Encode("Posted by " + Model.Name + " on " + Model.DateCreated.ToString("dd MMMM yyyy h:mm tt")) 
                        : "Posted by <a href=\"" + Model.Website + "\">" + Model.Name + "</a> on " + Model.DateCreated.ToString("dd MMMM yyyy h:mm tt") %></span>
</div>
