<%@ Page Title="Edit Company" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateCompanyAccount.aspx.cs" Inherits="CarHireWebApp.UpdateCompany" %>

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

                    var JSONCompany = '<%=new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(company)%>';
                    var companyJS = JSON.parse(JSONCompany);
                    $("#phoneNoTxt").intlTelInput("setNumber", companyJS.PhoneNo);
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
            <div class="spacer"></div>
            <asp:Button runat="server" OnClick="UpdateUser_Click" Text="Update" CssClass="btn btn-default" />
        </div>
    </div>
</asp:Content>

