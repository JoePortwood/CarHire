<%@ Page Title="Manage Your Account" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateCustomerAccount.aspx.cs" Inherits="CarHireWebApp.Account.UpdateCustomerAccount" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3><%: Title %>.</h3>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <asp:Label runat="server" ID="generalErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="inputErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="customerSavedLbl" Font-Bold="true" ForeColor="Blue"></asp:Label>

    <div class="row">
        <div class="col-md-4">
            <h3>Customer Details</h3>
            <div class="input-group">
                <label>*Surname</label>
                <asp:TextBox runat="server" ID="surnameTxt" CssClass="form-control input-xs" ToolTip="Display Name (Text)"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>*Forename</label>
                <asp:TextBox runat="server" ID="forenameTxt" CssClass="form-control input-xs" ToolTip="Company Description (Text)"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>*Title</label>
                <div></div>
                <asp:DropDownList runat="server" ID="titleDdl" CssClass="dropdown-toggle"></asp:DropDownList>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>Company</label>
                <div></div>
                <asp:DropDownList runat="server" ID="companyDdl" CssClass="dropdown-toggle"></asp:DropDownList>
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

                    //Sets the phone number to the current one in the database
                    var JSONCustomer = '<%=new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(customer)%>';
                    var CustomerJS = JSON.parse(JSONCustomer);
                    $("#phoneNoTxt").intlTelInput("setNumber", CustomerJS.PhoneNo);
                </script>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>Mobile No.</label>
                <div></div>
                <input id="mobileNoTxt" name="mobileNoTxt" class="form-control input-xs" type="tel">
                <script>
                    $("#mobileNoTxt").intlTelInput({
                        //autoFormat: false,
                        //autoHideDialCode: false,
                        //defaultCountry: "auto",
                        //ipinfoToken: "yolo",
                        //nationalMode: true,
                        numberType: "MOBILE",
                        //onlyCountries: ['us', 'gb', 'ch', 'ca', 'do'],
                        //preferredCountries: ['cn', 'jp'],
                        //preventInvalidNumbers: true,
                        //responsiveDropdown: true,
                        utilsScript: "Scripts/PhoneInput/utils.js"
                    });
                    $("#mobileNoTxt").intlTelInput("selectCountry", "gb");

                    var JSONCustomer = '<%=new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(customer)%>';
                    var CustomerJS = JSON.parse(JSONCustomer);
                    if (CustomerJS.MobileNo != "") {
                        $("#mobileNoTxt").intlTelInput("setNumber", CustomerJS.MobileNo);
                    }
                </script>
            </div>
            <div class="spacer"></div>
            <div class="form-group">
                <label>*Email Address</label>
                <asp:TextBox runat="server" ID="emailAddressTxt" CssClass="form-control input-xs" ToolTip="Email Address (Text)" TextMode="Email"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="emailAddressTxt"
                    CssClass="text-danger" ErrorMessage="The email field is required." />
            </div>
            <div class="spacer"></div>
        </div>

        <div class="col-md-4">

            <h3>Driving License</h3>
            <div class="input-group">
                <label>*Driving License No</label>
                <asp:TextBox runat="server" ID="licenseNoTxt" CssClass="form-control input-xs" ToolTip="Driving License Number (Text)"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>*Issue Date</label>
                <div></div>
                <asp:DropDownList runat="server" ID="issueDaysDdl" CssClass="dropdown-toggle"></asp:DropDownList>
                <asp:DropDownList runat="server" ID="issueMonthsDdl" CssClass="dropdown-toggle"></asp:DropDownList>
                <asp:DropDownList runat="server" ID="issueYearsDdl" CssClass="dropdown-toggle"></asp:DropDownList>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>*Expiration Date</label>
                <div></div>
                <asp:DropDownList runat="server" ID="expirationDaysDdl" CssClass="dropdown-toggle"></asp:DropDownList>
                <asp:DropDownList runat="server" ID="expirationMonthsDdl" CssClass="dropdown-toggle"></asp:DropDownList>
                <asp:DropDownList runat="server" ID="expirationYearsDdl" CssClass="dropdown-toggle"></asp:DropDownList>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>*Date of Birth</label>
                <div></div>
                <asp:DropDownList runat="server" ID="dateOfBirthDaysDdl" CssClass="dropdown-toggle"></asp:DropDownList>
                <asp:DropDownList runat="server" ID="dateOfBirthMonthsDdl" CssClass="dropdown-toggle"></asp:DropDownList>
                <asp:DropDownList runat="server" ID="dateOfBirthYearsDdl" CssClass="dropdown-toggle"></asp:DropDownList>
            </div>
            <div class="spacer"></div>
            <asp:Button runat="server" OnClick="UpdateUser_Click" Text="Update" CssClass="btn btn-default" />
       </div>
    </div>
</asp:Content>
