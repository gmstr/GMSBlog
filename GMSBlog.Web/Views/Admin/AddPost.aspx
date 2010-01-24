<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<GMSBlog.Model.Entities.Post>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Html.Encode(Model.Id>0 ? "Edit Post" : "Add Post") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%= Html.Encode(Model.Id > 0 ? "Edit Post" : "Add Post") %>
    </h2>
    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm())
       {%>
    <%= Html.Hidden("Id", Model.Id) %>
    <table class="blog-ui-update-table">
        <tr>
            <td>
                <label for="Title">
                    Title:</label>
            </td>
            <td>
                <%= Html.TextBox("Title", Model.Title) %>
                <%= Html.ValidationMessage("Title", "*") %>
            </td>
        </tr>
        <tr>
            <td>
                <label for="Summary">
                    Summary:</label>
            </td>
            <td>
                <%= Html.TextBox("Summary", Model.Summary, new { style = "width:835px;" })%>
                <%= Html.ValidationMessage("Summary", "*") %>
            </td>
        </tr>
        <tr>
            <td>
                <label for="Content">
                    Content:</label>
            </td>
            <td>
                <%= Html.TextArea("Content", Model.Content) %>
                <%= Html.ValidationMessage("Content", "*") %>
            </td>
        </tr>
        <tr>
            <td>
                <label for="IsPublished">
                    Is Published:</label>
            </td>
            <td>
                <%= Html.CheckBox("IsPublished", Model.IsPublished) %>
                <%= Html.ValidationMessage("IsPublished", "*") %>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <h3>Categories</h3>
                <ul class="blog-ui-checkbox-list">
                <%foreach(var category in ViewData["Categories"] as IList<GMSBlog.Model.Entities.Category>){ %>
                    <li><%=Html.CheckBox("Category-" + category.Id, Model.Categories.Select(x => x.Id).Contains(category.Id)) %> <%=Html.Encode(category.Name) %></li>
                <%} %>
                </ul>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <input type="submit" value="Save" />
            </td>
        </tr>
    </table>
    <% } %>
    <div>
        <%=Html.ActionLink("Back to List", "Posts") %>
    </div>
</asp:Content>
