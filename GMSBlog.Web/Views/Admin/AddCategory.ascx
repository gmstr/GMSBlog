<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GMSBlog.Model.Entities.Category>" %>
<%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>
<div class="blog-ui-update-holder" id="addCategoryHolder">
    <% using (Html.BeginForm("AddCategory", "Admin", FormMethod.Post, new { id = "addCategoryForm" }))
       {%>
    <%= Html.Hidden("Id", Model.Id)%>
    <h4>
        Add Category
    </h4>
    
    <table>
        <tbody>
            <tr>
                <td>
                    <label for="Name">Name: </label>
                </td>
                <td>
                    <%= Html.TextBox("Name", Model.Name)%>
                    <%= Html.ValidationMessage("Name", "*")%>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="submit" value="Save" />       
                </td>
                <td>
                    <a href="#" id="cancelAddCategory">Cancel</a>     
                </td>
            </tr>
        </tbody>
    </table>

    <script type="text/javascript">
        $(document).ready(function() {
            $("#cancelAddCategory").live('click', function() {
                $.get('<%=Url.Action(MVC.Admin.AddCategoryLink()) %>', function(response) {
                    $("#addCategoryHolder").replaceWith(response);
                    return false;
                });
                return false;
            });
        });
    </script>

    <% } %>
</div>
