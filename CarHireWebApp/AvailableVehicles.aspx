<%@ Page Title="Available Vehicles" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AvailableVehicles.aspx.cs" Inherits="CarHireWebApp.AvailableVehicles" %>

<%@ Register TagPrefix="Ajaxified" Assembly="Ajaxified" Namespace="Ajaxified" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css">
    <script src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script>
        $(function () {
            $("[id$=hireStartDateTxt]").datepicker({
                dateFormat: "dd/mm/yy",
                minDate: 1,
            });
        });
        $(function () {
            $("[id$=hireEndDateTxt]").datepicker({
                dateFormat: "dd/mm/yy",
                minDate: 1,
            });
        });
  </script>

    <asp:Label runat="server" ID="generalErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="inputErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>

    <div class="spacer"></div>
    <div class="row">
        <div class="col-md-4">
            <label>Location</label>
                <asp:DropDownList runat="server" ID="locationDdl" CssClass="btn btn-default dropdown-toggle"></asp:DropDownList>
            </div>
        <div class="col-md-1">
            </div>
        <div class="col-md-2">
            <label>Start Date</label>
                <asp:TextBox runat="server" ID="hireStartDateTxt" CssClass="form-control"></asp:TextBox>
            <label>Start Time</label>
                <asp:TextBox runat="server" ID="hireStartTimeTxt" CssClass="form-control"></asp:TextBox>
                <Ajaxified:TimePicker ID="hireStartTimeTimePicker" runat="server" TargetControlID="hireStartTimeTxt">
                </Ajaxified:TimePicker>	
            </div>
        <div class="col-md-1">
            </div>
        <div class="col-md-2">
            <label>End Date</label>
                <asp:TextBox runat="server" ID="hireEndDateTxt" CssClass="form-control"></asp:TextBox>
            <label>End Time</label>
                <asp:TextBox runat="server" ID="hireEndTimeTxt" CssClass="form-control"></asp:TextBox>
                <Ajaxified:TimePicker ID="hireEndTimeTimePicker" runat="server" TargetControlID="hireEndTimeTxt">
                </Ajaxified:TimePicker>	
            </div>
        </div>
    <div class="spacer"></div>
    <asp:Button runat="server" ID="searchAvailableVehiclesBtn" Text="Search" type="button" class="btn btn-primary" OnClick="SearchAvailableVehiclesBtn_Click"></asp:Button>
    <div class="spacer"></div>
    <asp:Table runat="server" ID="vehiclesAvailableTbl" Visible="false" CssClass="table table-striped table-bordered">
    </asp:Table>

</asp:Content>
