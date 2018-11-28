<%@ Page Title="View Customer Orders" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewCustomerOrders.aspx.cs" Inherits="CarHireWebApp.Account.ViewCustomerOrders" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label runat="server" ID="generalErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>

    <div class="spacer"></div>
    <asp:Table runat="server" ID="customerOrdersTbl" CssClass="table table-striped table-bordered">
            </asp:Table>

</asp:Content>
