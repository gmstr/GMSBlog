<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Admin
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>
        Tasks
    </h3>
    <ul>
        <li>
            <%=Html.ActionLink("View Categories", MVC.Admin.Categories()) %>
        </li>
        <li>
            <%=Html.ActionLink("View Posts", "Posts") %>
        </li>
    </ul>
</asp:Content>
