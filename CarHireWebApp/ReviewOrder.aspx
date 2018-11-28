<%@ Page Title="Review Order" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReviewOrder.aspx.cs" Inherits="CarHireWebApp.ReviewOrder" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <asp:Label runat="server" ID="generalErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="inputErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="orderConfirmedLbl" Font-Bold="true" ForeColor="Blue"></asp:Label>

    <h1 class="center">Review Order</h1>

    <asp:Image runat="server" CssClass="centerimage" ID="vehicleImg" />
    <div class="spacer"></div>
   
    <asp:Label runat="server" CssClass="center" ID="addressLbl" Font-Bold="true"></asp:Label>
    <div class="spacer"></div>

    <asp:Label runat="server" CssClass="center" ID="carInfoLbl"></asp:Label>
    <div class="spacer"></div>

    <asp:Label runat="server" CssClass="center" ID="dateTimeLbl"></asp:Label>
    <div class="spacer"></div>

    <asp:Label runat="server" ID="priceLbl" Font-Bold="true" CssClass="center" Font-Size="X-Large"></asp:Label>

    <div class="spacer"></div>
    <asp:Button runat="server" ID="PayPalBtn" Text="Go to PayPal" type="button" class="btn btn-primary centerimage squarebutton" OnClick="PayPalBtn_Click"></asp:Button>

    </asp:Content>
