<%@ Page Title="Vehicles" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Vehicles.aspx.cs" Inherits="CarHireWebApp.Vehicles" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="spacer"></div>
    <asp:UpdatePanel ID="UpdatePanel1"
        UpdateMode="Conditional"
        runat="server">
        <ContentTemplate>
            <asp:Label runat="server" ID="generalErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>

            <div class="row">

                <div class="col-md-2">
                    <h4>Manufacturer</h4>
                    <asp:DropDownList runat="server" ID="manufacturerDdl" CssClass="btn btn-default dropdown-toggle" OnSelectedIndexChanged="ManufacturerDdl_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </div>

                <div class="col-md-2">
                    <h4>Model</h4>
                    <asp:DropDownList runat="server" ID="modelDdl" CssClass="btn btn-default dropdown-toggle" AutoPostBack="true"></asp:DropDownList>
                </div>
                <div class="bigspacer"></div>
                <div class="col-md-1">
                    <asp:CheckBox runat="server" CssClass="input-group-addon" BackColor="#00ff00" ID="ecoFriendlyChk" Text="Eco-Friendly" AutoPostBack="true"/>
                </div>
            </div>
            <div class="spacer"></div>
            <asp:Table runat="server" ID="VehiclesTbl" CssClass="table table-striped table-bordered">
            </asp:Table>

            <asp:Button runat="server" ID="revealCodeBtn" Text="SIPP Code" CssClass="button" data-toggle="modal" data-target="#SIPPCodeModal"></asp:Button>

        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Modal -->
    <div class="modal fade bs-example-modal-lg" id="SIPPCodeModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title" id="SIPPCodeModalLabel">SIPP Codes</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Table runat="server" ID="SIPPCodesSizeOfVehicleTbl" CssClass="table table-bordered"></asp:Table>
                                </div>
                                <div class="col-md-3">
                                    <asp:Table runat="server" ID="SIPPCodesNoOfDoorsTbl" CssClass="table table-bordered"></asp:Table>
                                </div>
                                <div class="col-md-3">
                                    <asp:Table runat="server" ID="SIPPCodesTransmissionAndDriveTbl" CssClass="table table-bordered"></asp:Table>
                                </div>
                                <div class="col-md-3">
                                    <asp:Table runat="server" ID="SIPPCodesFuelAndAirConTbl" CssClass="table table-bordered"></asp:Table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
