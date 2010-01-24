<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<GMSBlog.Model.Entities.Category>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Categories
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>
        Categories
    </h3>
    <div class="blog-ui-table-holder">
    <%if (Model.Any())
      { %>
    <table>
        <thead>
            <tr>
                <th>
                    Name
                </th>
                <th>
                </th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var item in Model)
               { %>
            <tr>
                <td>
                    <%= Html.Encode(item.Name)%>
                </td>
                <td>
                    <%using (Html.BeginForm("DeleteCategory", "Admin", FormMethod.Post, new { @class = "deleteForm" }))
                      { %>
                    <%=Html.Hidden("Id", item.Id)%><a href="#" class="deleteCategory">Delete</a>
                    <%} %>
                </td>
            </tr>
            <% } %></tbody>
    </table>
    <%} else { %>
    <p>
        There are currently no categories.
    </p>
    <%} %>
    </div>
    <% Html.RenderPartial(MVC.Admin.Views.AddCategoryLink); %>
    
    <p>
        <%=Html.ActionLink("Back to Index", MVC.Admin.Index()) %>
    </p>

    <script>
        $(document).ready(function() {
            $(".deleteForm").submit(function() {
                if (confirm("Are you sure you want to delete this category?")) { return true; }
                else { return false; }
            });

            $(".deleteCategory").click(function() {
                $(this).parent('form').submit();
            });
        });
    </script>

</asp:Content>
