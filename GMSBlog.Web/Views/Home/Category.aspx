<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<GMSBlog.Model.Entities.Post>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% foreach (var post in Model)
	   { %>
	    <%Html.RenderPartial("Summary", post);%>	 
	<% } %>
    
    
</asp:Content>

