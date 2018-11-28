<%@ Page Title="AddVehicle" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AddVehicle.aspx.cs" Inherits="CarHireWebApp.AddVehicle" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="Scripts/filepreview.js"></script>
    <script type="text/javascript">
        function previewFileInit() {
            var preview = document.querySelector('#<%=imgViewFile.ClientID %>');
            var file = document.querySelector('#<%=fileUpload.ClientID %>').files[0]; 
            previewFile(file, preview);
        }
    </script> 

    <asp:Label runat="server" ID="generalErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>
    <div class="row">
        <div class="col-md-4">
            <h4>Picture</h4>
            <input id="fileUpload" type="file" name="file" onchange="previewFileInit()" runat="server" />
            <asp:Image ID="imgViewFile" runat="server" />
            <asp:CheckBox runat="server" ID="noImageChk" CssClass="leftfloat" Text="Tick for blank image" />
            <div class="spacer"></div>
            <asp:Label runat="server" CssClass="leftfloat" ID="pictureErrorLbl" ForeColor="Red"></asp:Label>

        </div>

        <div class="col-md-3">
            <h4>Vehicle Details</h4>
            <div class="input-group">
                <label>Manufacturer</label>
                <asp:TextBox runat="server" ID="manufacturerTxt" CssClass="form-control" ToolTip="Manufacturer"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>Model</label>
                <asp:TextBox runat="server" ID="modelTxt" CssClass="form-control" ToolTip="Model"></asp:TextBox>
            </div>
            <asp:Label runat="server" ID="manufacturerFailLbl" ForeColor="Red"></asp:Label>
            <div class="spacer"></div>
            <asp:Label runat="server" ID="modelFailLbl" ForeColor="Red"></asp:Label>
        </div>
        <div class="bigspacer"></div>

        <div class="col-md-3">
            <div class="input-group">
                <div class="sameline">
                    <label>SIPP Code </label>
                    <asp:Button runat="server" ID="revealCodeBtn" Font-Size="X-Small" Text="Forgot Code?" CssClass="button" data-toggle="modal" data-target="#myModal"></asp:Button>
                </div>
                <asp:TextBox runat="server" ID="SIPPCodeTxt" CssClass="form-control" ToolTip="SIPP Code"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>MPG</label>
                <asp:TextBox runat="server" ID="MPGTxt" CssClass="form-control" ToolTip="MPG"></asp:TextBox>
            </div>
            <asp:Label runat="server" ID="MPGFailLbl" ForeColor="Red"></asp:Label>
            <div class="spacer"></div>
            <asp:Label runat="server" ID="SIPPCodeFailLbl" ForeColor="Red"></asp:Label>
        </div>

        <div class="col-md-2">
            <div class="mediumspacer"></div>
            <div class="btn-group">
                <asp:Button runat="server" ID="addVehicleBtn" Text="Add Car" type="button" class="btn btn-primary" OnClick="VehicleAddBtn_Click"></asp:Button>
            </div>
            <asp:Label runat="server" CssClass="leftfloat" ID="addCompleteLbl" ForeColor="Blue"></asp:Label>
        </div>
    </div>

    <!-- Modal -->
    <div class="modal fade bs-example-modal-lg" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title" id="myModalLabel">SIPP Codes</h4>
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
