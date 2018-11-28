<%@ Page Title="Info Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InformUser.aspx.cs" Inherits="CarHireWebApp.Account.ResetPasswordConfirmation" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <%--<h2><%: Title %>.</h2>--%>
    <div class="spacer"></div>
    <div>
        <asp:Label runat="server" ID="infoLbl" CssClass="h4"></asp:Label>
        <div class="spacer"></div>
        <asp:Label runat="server" ID="redirectLbl" CssClass="h4"></asp:Label>
    </div>
</asp:Content>
