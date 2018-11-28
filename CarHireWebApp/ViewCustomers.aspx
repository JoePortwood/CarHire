<%@ Page Title="View Customers" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ViewCustomers.aspx.cs" Inherits="CarHireWebApp.ViewCustomers" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label runat="server" ID="generalErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>

    <div class="spacer"></div>
    <h4>Customer Surname</h4>
    <asp:TextBox runat="server" ID="customerSurnameTxt" CssClass="form-control" ToolTip="Customer Surname (Text)"></asp:TextBox>
    <div class="spacer"></div>
    <asp:Button runat="server" ID="searchCustomersBtn" Text="Search" type="button" class="btn btn-primary" OnClick="SearchCustomersBtn_Click"></asp:Button>
    <div class="spacer"></div>
    <asp:Table runat="server" ID="CustomersTbl" CssClass="table table-striped table-bordered">
    </asp:Table>

</asp:Content>
