<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Log On
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Log On</h2>
    <%using (Html.BeginForm("LogOn", "Account", FormMethod.Post))
      { %>
      <%=Html.Hidden("returnUrl", Request["returnUrl"]) %>
    <table>
        <tr>
            <td>
                Username:
            </td>
            <td>
                <%=Html.TextBox("username") %>
            </td>
        </tr>
        <tr>
            <td>
                Password:
            </td>
            <td>
                <%=Html.Password("password") %>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <input type="submit" value="Log In" />
            </td>
        </tr>
    </table>
    <% } %>
</asp:Content>
