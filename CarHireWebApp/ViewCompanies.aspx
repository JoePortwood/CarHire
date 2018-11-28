<%@ Page Title="View Companies" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ViewCompanies.aspx.cs" Inherits="CarHireWebApp.ViewCompanies" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label runat="server" ID="generalErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>

    <div class="spacer"></div>
    <h4>Company Name</h4>
    <asp:TextBox runat="server" ID="companyNameTxt" CssClass="form-control" ToolTip="Company Name (Text)"></asp:TextBox>
    <div class="spacer"></div>
    <asp:Button runat="server" ID="searchCompanyNameBtn" Text="Search" type="button" class="btn btn-primary" OnClick="SearchCompanyNameBtn_Click"></asp:Button>
    <div class="spacer"></div>
    <asp:Table runat="server" ID="CompaniesTbl" CssClass="table table-striped table-bordered">
    </asp:Table>

</asp:Content>
