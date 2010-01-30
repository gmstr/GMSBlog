<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GMSBlog.Web.Models.PostComment>" %>
<div class="blog-ui-add-comment">
<h3>Add Comment</h3>
<%using (Html.BeginForm("AddComment", "Home", FormMethod.Post))
  { %>
  <%=Html.Hidden("postId", Model.PostId) %>
<table class="blog-ui-comment-table">
    <tr>
        <td>
            Name:
        </td>
        <td>
            <%=Html.TextBox("Name", Model.Comment.Name)%>
        </td>
    </tr>
    <tr>
        <td>
            Website (optional):
        </td>
        <td>
            <%=Html.TextBox("Website", Model.Comment.Website)%>
        </td>
    </tr>
    <tr>
        <td>
            Comment:
        </td>
        <td>
            <%=Html.TextArea("Content", Model.Comment.Content)%>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <input type="submit" value="Add Comment" />
        </td>
    </tr>
</table>
<%} %>
</div>
