<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<GMSBlog.Model.Entities.Post>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EditPost
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>EditPost</h2>

    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="Id">Id:</label>
                <%= Html.TextBox("Id", Model.Id) %>
                <%= Html.ValidationMessage("Id", "*") %>
            </p>
            <p>
                <label for="Title">Title:</label>
                <%= Html.TextBox("Title", Model.Title) %>
                <%= Html.ValidationMessage("Title", "*") %>
            </p>
            <p>
                <label for="Summary">Summary:</label>
                <%= Html.TextBox("Summary", Model.Summary) %>
                <%= Html.ValidationMessage("Summary", "*") %>
            </p>
            <p>
                <label for="Content">Content:</label>
                <%= Html.TextBox("Content", Model.Content) %>
                <%= Html.ValidationMessage("Content", "*") %>
            </p>
            <p>
                <label for="DateCreated">DateCreated:</label>
                <%= Html.TextBox("DateCreated", String.Format("{0:g}", Model.DateCreated)) %>
                <%= Html.ValidationMessage("DateCreated", "*") %>
            </p>
            <p>
                <label for="DateUpdated">DateUpdated:</label>
                <%= Html.TextBox("DateUpdated", String.Format("{0:g}", Model.DateUpdated)) %>
                <%= Html.ValidationMessage("DateUpdated", "*") %>
            </p>
            <p>
                <label for="IsPublished">IsPublished:</label>
                <%= Html.TextBox("IsPublished", Model.IsPublished) %>
                <%= Html.ValidationMessage("IsPublished", "*") %>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

