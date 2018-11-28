<%@ Page Title="Checkout Complete" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckoutComplete.aspx.cs" Inherits="CarHireWebApp.CheckoutComplete" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3><%: Title %>.</h3>

    <asp:Label runat="server" ID="generalErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>

    <div class="spacer"></div>
    <asp:Label runat="server" Font-Bold="true" Text="Payment Transaction ID" ID="transactionIDLbl"></asp:Label>

    <div class="spacer"></div>
    <asp:Label runat="server" ID="transactionLbl"></asp:Label>

    <div class="spacer"></div>
    <asp:Label runat="server" Font-Bold="true" Text="Thank you" ID="thankyouLbl"></asp:Label>

    <div class="spacer"></div>
    <asp:Button runat="server" ID="continueBtn" Text="Look at other cars" type="button" class="btn" OnClick="ContinueBtn_Click"></asp:Button>
    
</asp:Content>
