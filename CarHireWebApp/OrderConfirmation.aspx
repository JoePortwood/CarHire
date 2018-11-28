<%@ Page Title="Order Confirmed" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderConfirmation.aspx.cs" Inherits="CarHireWebApp.OrderConfirmation" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <asp:Label runat="server" ID="generalErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>

    <h1 class="center">Complete Order</h1>

    <asp:Image runat="server" CssClass="centerimage" ID="vehicleImg" />
    <div class="spacer"></div>

    <asp:Label runat="server" CssClass="center" ID="addressLbl" Font-Bold="true"></asp:Label>
    <div class="spacer"></div>

    <asp:Label runat="server" CssClass="center" ID="carInfoLbl"></asp:Label>
    <div class="spacer"></div>

    <asp:Label runat="server" CssClass="center" ID="dateTimeLbl"></asp:Label>
    <div class="spacer"></div>

    <asp:Label runat="server" ID="priceLbl" Font-Size="X-Large" Font-Bold="true" CssClass="center"></asp:Label>

    <div class="spacer"></div>
    <asp:Button runat="server" ID="confirmOrderBtn" Text="Complete Order" type="button" class="btn btn-primary centerimage squarebutton" OnClick="ConfirmOrderBtn_Click"></asp:Button>

</asp:Content>
