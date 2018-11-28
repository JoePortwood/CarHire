<%@ Page Title="Add Bookable Vehicle" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddAvailableVehicle.aspx.cs" Inherits="CarHireWebApp.AddAvailableVehicle" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <asp:Label runat="server" ID="generalErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="inputErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="vehicleAddedLbl" Font-Bold="true" ForeColor="Blue"></asp:Label>

    <div class="spacer"></div>
    <div class="row">
        <div class="col-md-2">
            <label>Location</label>
                <asp:DropDownList runat="server" ID="locationDdl" CssClass="btn btn-default dropdown-toggle"></asp:DropDownList>
            </div>
        <div class="col-md-1">
            </div>
        <div class="col-md-2">
            <label>Vehicle</label>
                <asp:DropDownList runat="server" ID="vehicleDdl" CssClass="btn btn-default dropdown-toggle" OnSelectedIndexChanged="VehicleDdl_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            <div class="spacer"></div>
                <asp:Image runat="server" ID="vehicleImg" />
            </div>
        <div class="col-md-1">
            </div>
        <div class="col-md-2">
            <label>Total Vehicles</label>
                <asp:TextBox runat="server" ID="totalVehiclesTxt" CssClass="form-control"></asp:TextBox>
            </div>
        <div class="col-md-1">
            </div>
        <div class="col-md-2">
            <label>Currency</label>
            <div></div>
                <asp:DropDownList runat="server" ID="currencyDdl" CssClass="btn btn-default dropdown-toggle"></asp:DropDownList>
            <div class="spacer"></div>
            <label>BasePrice</label>
                <asp:TextBox runat="server" ID="basePriceTxt" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    <div class="spacer"></div>
    <asp:Button runat="server" ID="addAvailableVehicleBtn" Text="Add" type="button" class="btn btn-primary" OnClick="AddAvailableVehiclesBtn_Click"></asp:Button>
    <div class="spacer"></div>
    <asp:Table runat="server" ID="vehiclesAvailableTbl" Visible="false" CssClass="table table-striped table-bordered">
    </asp:Table>

</asp:Content>
