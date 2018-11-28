<%@ Page Title="Add Company" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AddCompany.aspx.cs" Inherits="CarHireWebApp.AddCompany" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

        <asp:Label runat="server" ID="generalErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="inputErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="companySavedLbl" Font-Bold="true" ForeColor="Blue"></asp:Label>

    <div class="row">
        <div class="col-md-3">
            <h3>Company Details</h3>
            <div class="input-group">
                <label>*User Name</label>
                <asp:TextBox runat="server" ID="userNameTxt" CssClass="form-control input-xs" ToolTip="Company Name (Text)"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>*Company Name</label>
                <asp:TextBox runat="server" ID="companyNameTxt" CssClass="form-control input-xs" ToolTip="Company Name (Text)"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>Company Description</label>
                <asp:TextBox runat="server" ID="companyDescriptionTxt" CssClass="form-control input-xs" ToolTip="Company Description (Text)"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>*Phone No.</label>
                <asp:TextBox runat="server" ID="phoneNoTxt" CssClass="form-control input-xs" ToolTip="Phone No (Text)"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>*Email Address</label>
                <asp:TextBox runat="server" ID="emailAddressTxt" CssClass="form-control input-xs" ToolTip="Email Address (Text)"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-2">
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
            <asp:Button runat="server" ID="addCompanyBtn" Text="Add Company" type="button" class="btn btn-primary" OnClick="CompanyAddBtn_Click"></asp:Button>
        </div>
    </div>

</asp:Content>
