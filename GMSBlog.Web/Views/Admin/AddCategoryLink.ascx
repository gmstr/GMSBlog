<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<p id="addCategory">
    <a href="#" id="addCategoryLink">Add a category</a>
</p>

<script type="text/javascript">
    $(document).ready(function() {
        $("#addCategoryLink").live('click', function() {
            $.get('<% =Url.Action(MVC.Admin.AddCategory()) %>', function(response) {
                $("#addCategory").replaceWith(response);
                return false;
            });
            return false;
        });
    });
</script>

