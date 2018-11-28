<%@ Page Title="Register Company" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterCompany.aspx.cs" Inherits="CarHireWebApp.Account.RegisterCompany" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3><%: Title %>.</h3>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <asp:Label runat="server" ID="generalErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="inputErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="companySavedLbl" Font-Bold="true" ForeColor="Blue"></asp:Label>

    <div class="row">
        <div class="col-md-4">
            <h3>Company Details</h3>
            <div class="input-group">
                <label>*Company Name</label>
                <asp:TextBox runat="server" ID="companyNameTxt" CssClass="form-control input-xs" ToolTip="Display Name (Text)"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>Company Description</label>
                <asp:TextBox runat="server" ID="companyDescriptionTxt" CssClass="form-control input-xs" ToolTip="Company Description (Text)"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>*Licensing Details</label>
                <asp:TextBox runat="server" ID="licensingDetailsTxt" TextMode="MultiLine" Rows="3" CssClass="form-control input-xs" ToolTip="Licensing for your company (Text)"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>*Phone No.</label>
                <div></div>
                <input id="phoneNoTxt" name="phoneNoTxt" class="form-control input-xs" type="tel">
                <script>
                    $("#phoneNoTxt").intlTelInput({
                        //autoFormat: false,
                        //autoHideDialCode: false,
                        //defaultCountry: "auto",
                        //ipinfoToken: "yolo",
                        //nationalMode: true,
                        //numberType: "MOBILE",
                        //onlyCountries: ['us', 'gb', 'ch', 'ca', 'do'],
                        //preferredCountries: ['cn', 'jp'],
                        //preventInvalidNumbers: true,
                        //responsiveDropdown: true,
                        utilsScript: "Scripts/PhoneInput/utils.js"
                    });
                    $("#phoneNoTxt").intlTelInput("selectCountry", "gb");
                </script>
            </div>
            <div class="spacer"></div>
            <div class="form-group">
                <label>*Email Address</label>
                <asp:TextBox runat="server" ID="emailAddressTxt" CssClass="form-control input-xs" ToolTip="Email Address (Text)" TextMode="Email"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="emailAddressTxt"
                    CssClass="text-danger" ErrorMessage="The email field is required." />
            </div>
        </div>

        <div class="col-md-1">
        </div>

        <div class="col-md-4">
            <h3>Login Details</h3>
            <div class="input-group">
                <label>*User Name</label>
                <asp:TextBox runat="server" ID="userNameTxt" CssClass="form-control input-xs" ToolTip="Required for login"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <asp:ValidationSummary runat="server" Visible="false" CssClass="text-danger" />
        <div class="form-group">
            <label>*Password</label>
            <asp:TextBox runat="server" ID="passwordTxt" TextMode="Password" CssClass="form-control input-xs" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="passwordTxt"
                    CssClass="text-danger" ErrorMessage="The password field is required." />
        </div> 
        <div class="form-group">
            <label>*Confirm Password</label>
            <asp:TextBox runat="server" ID="confirmPasswordTxt" TextMode="Password" CssClass="form-control input-xs" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="confirmPasswordTxt"
                    CssClass="text-danger" ErrorMessage="The password field is required." />
                <asp:CompareValidator runat="server" ControlToCompare="passwordTxt" ControlToValidate="confirmPasswordTxt"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
        </div>
            <div class="spacer"></div>
            <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn btn-default" />
        </div>
    </div>
</asp:Content>
