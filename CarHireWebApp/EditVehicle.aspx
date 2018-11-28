<%@ Page Title="EditVehicles" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditVehicle.aspx.cs" Inherits="CarHireWebApp.EditVehicle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="Scripts/filepreview.js"></script>
    <script type="text/javascript">
        function previewFileInit() {
            var preview = document.querySelector('#<%=imgViewFile.ClientID %>');
            var file = document.querySelector('#<%=fileUpload.ClientID %>').files[0];
            previewFile(file, preview);
        }
    </script> 

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
            </div>
            <div class="spacer"></div>

            <asp:Table runat="server" ID="VehiclesTbl" CssClass="table table-striped table-bordered"></asp:Table>

            <asp:Button runat="server" ID="revealCodeBtnTbl" Text="SIPP Code" Visible="false" CssClass="button" data-toggle="modal" data-target="#SIPPCodeModal"></asp:Button>


            <div class="row">
                <div class="col-md-4">
                    <asp:Label runat="server" ID="pictureLbl" Visible="false" CssClass="h4" Text="Picture"></asp:Label>
                    <div class="spacer"></div>
                    <input id="fileUpload" type="file" visible="true" name="file" onchange="previewFileInit()" runat="server" />
                    <asp:Image ID="imgViewFile" Visible="false" runat="server" Width="270" Height="150" />
                    <asp:CheckBox runat="server" ID="noImageChk" Visible="false" CssClass="leftfloat" Text="Tick for blank image" />
                    <div class="spacer"></div>
                    <asp:Label runat="server" CssClass="leftfloat" ID="pictureErrorLbl" ForeColor="Red"></asp:Label>

                </div>

                <div class="col-md-3">
                    <asp:Label runat="server" ID="vehicleDetailsLbl" Visible="false" CssClass="h4" Text="Vehicle Details"></asp:Label>
                    <div class="spacer"></div>
                    <div class="input-group">
                        <asp:Label runat="server" ID="manufacturerLbl" Visible="false" Font-Bold="true" Text="Manufacturer"></asp:Label>
                        <asp:TextBox runat="server" ID="manufacturerTxt" Visible="false" CssClass="form-control" ToolTip="Manufacturer"></asp:TextBox>
                    </div>
                    <div class="spacer"></div>
                    <div class="input-group">
                        <asp:Label runat="server" ID="modelLbl" Visible="false" Font-Bold="true" Text="Model"></asp:Label>
                        <asp:TextBox runat="server" ID="modelTxt" Visible="false" CssClass="form-control" ToolTip="Model"></asp:TextBox>
                    </div>
                    <asp:Label runat="server" ID="manufacturerFailLbl" ForeColor="Red"></asp:Label>
                    <div class="spacer"></div>
                    <asp:Label runat="server" ID="modelFailLbl" ForeColor="Red"></asp:Label>

                </div>
                <div class="bigspacer"></div>

                <div class="col-md-3">
                    <div class="input-group">
                        <div class="sameline">
                            <asp:Label runat="server" ID="SIPPCodeLbl" Visible="false" Font-Bold="true" Text="SIPP Code"></asp:Label>
                            <asp:Button runat="server" ID="revealCodeBtn" Visible="false" Font-Size="X-Small" Text="Forgot Code?" CssClass="button" data-toggle="modal" data-target="#SIPPCodeModal"></asp:Button>
                        </div>
                        <asp:TextBox runat="server" ID="SIPPCodeTxt" Visible="false" CssClass="form-control" ToolTip="SIPP Code"></asp:TextBox>
                    </div>
                    <div class="spacer"></div>
                    <div class="input-group">
                        <asp:Label runat="server" ID="MPGLbl" Visible="false" Font-Bold="true" Text="MPG"></asp:Label>
                        <asp:TextBox runat="server" ID="MPGTxt" Visible="false" CssClass="form-control" ToolTip="MPG"></asp:TextBox>

                    </div>
                    <div class="spacer"></div>
                    <asp:CheckBox runat="server" ID="activeChk" Visible="false" CssClass="leftfloat" Text="Tick for vehicle to be displayed" />
                    <asp:Label runat="server" ID="MPGFailLbl" ForeColor="Red"></asp:Label>
                    <div class="spacer"></div>
                    <asp:Label runat="server" ID="SIPPCodeFailLbl" ForeColor="Red"></asp:Label>
                </div>

                <div class="col-md-2">
                    <div class="mediumspacer"></div>
                    <div class="btn-group">
                        <asp:Button runat="server" ID="editVehicleBtn" Visible="false" Text="Update" type="button" class="btn btn-primary" OnClick="VehicleUpdateBtn_Click"></asp:Button>
                    </div>
                    <asp:Label runat="server" CssClass="leftfloat" ID="updateCompleteLbl" ForeColor="Blue"></asp:Label>
                </div>
            </div>

            <asp:TextBox runat="server" ID="vehicleIDTxt" Visible="false"></asp:TextBox>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="editVehicleBtn" />
        </Triggers>
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
