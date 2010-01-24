<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<GMSBlog.Model.Entities.Post>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Posts
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Posts</h2>

    <table>
        <tr>
            <th>
                Title
            </th>
            <th>
                Summary
            </th>
            <th>
                Date Created
            </th>
            <th>
                Date Updated
            </th>
            <th>
                Published?
            </th>
            <th>
                Categories
            </th>
            <th>
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.Encode(item.Title) %>
            </td>
            <td>
                <%= Html.Encode(item.Summary) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.DateCreated)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.DateUpdated)) %>
            </td>
            <td>
                <%= Html.Encode(item.IsPublished) %>
            </td>
            <td>
                <%= Html.Encode(string.Join(", ", item.Categories.Select(x => x.Name).ToArray())) %>
            </td>
            <td>
                <%= Html.ActionLink("Edit", MVC.Admin.EditPost(item.Id)) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "AddPost") %>
    </p>

</asp:Content>

