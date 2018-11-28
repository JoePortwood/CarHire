<%@ Page  Title="Add Address" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AdditionalAddresses.aspx.cs" Inherits="CarHireWebApp.AdditionalAddresses" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label runat="server" ID="generalErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="inputErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="addressSavedLbl" Font-Bold="true" ForeColor="Blue"></asp:Label>

    <div class="spacer"></div>
    <asp:Button runat="server" ID="custRedirectBtn" Text="Customer" Visible="false" type="button" class="btn" OnClick="CustomerRedirectBtn_Click"></asp:Button>
    <asp:Button runat="server" ID="compRedirectBtn" Text="Company" Visible="false" type="button" class="btn" OnClick="CompanyRedirectBtn_Click"></asp:Button>
    <asp:Label runat="server" id="typeLbl" CssClass="h4"></asp:Label>
    <asp:DropDownList runat="server" ID="companyDdl" Visible="false" CssClass="btn btn-default dropdown-toggle" OnSelectedIndexChanged="CompanyDdl_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    <asp:DropDownList runat="server" ID="customerDdl" Visible="false" CssClass="btn btn-default dropdown-toggle" OnSelectedIndexChanged="CustomerDdl_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

    <div class="row">

        <div class="col-md-5">
            <div class="spacer"></div>
            <asp:Table runat="server" ID="AddressesTbl" CssClass="table table-striped table-bordered">
                </asp:Table>
        </div>

        <div class="col-md-3">

            <h3>Address</h3>
            <div class="input-group">
                <label>Address Line 1</label>
                <asp:TextBox runat="server" ID="addressLine1Txt" CssClass="form-control input-xs" ToolTip="Address Line 1 (Text)"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>Address Line 2</label>
                <asp:TextBox runat="server" ID="addressLine2Txt" CssClass="form-control input-xs" ToolTip="Address Line 2 (Text)"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>Address Line 3</label>
                <asp:TextBox runat="server" ID="addressLine3Txt" CssClass="form-control input-xs" ToolTip="Address Line 3 (Text)"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>Address Line 4</label>
                <asp:TextBox runat="server" ID="addressLine4Txt" CssClass="form-control input-xs" ToolTip="Address Line 4 (Text)"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>*City</label>
                <asp:TextBox runat="server" ID="cityTxt" CssClass="form-control input-xs" ToolTip="City (Text)"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-3">

            <h3>Address</h3>
            <div class="input-group">
                <label>*Zip/Postcode</label>
                <asp:TextBox runat="server" ID="zipOrPostcodeTxt" CssClass="form-control input-xs" ToolTip="Zip/Postcode (Text)"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>County/State/Province</label>
                <asp:TextBox runat="server" ID="countyStateProvinceTxt" CssClass="form-control input-xs" ToolTip="County/State/Province (Text)"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>*Country</label>
                <asp:TextBox runat="server" ID="countryTxt" CssClass="form-control input-xs" ToolTip="Country"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>Other Address Details</label>
                <asp:TextBox runat="server" ID="otherAddressDetailsTxt" CssClass="form-control input-xs" ToolTip="Other Address Details (Text)"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <asp:Button runat="server" ID="addAddressBtn" Text="Add Address" type="button" class="btn btn-primary" OnClick="AddressAddBtn_Click"></asp:Button>
        </div>
    </div>

</asp:Content>
