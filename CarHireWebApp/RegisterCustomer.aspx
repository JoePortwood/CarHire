<%@ Page Title="Register Customer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterCustomer.aspx.cs" Inherits="CarHireWebApp.Account.RegisterCustomer" %>

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

            <%--<h3>Address</h3>
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
            </div>--%>
       </div>

        <div class="col-md-4">

            <%--<h3>Address</h3>
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
                <select id="countryDdl" name="countryDdl" class="dropdown-toggle"></select>
                <script>
                    //Set the dropdown to the same country as the one selected in the phone section
                    // get the country data from the plugin
                    var countryData = $.fn.intlTelInput.getCountryData(),
                      telInput = $("#phoneNoTxt"),
                      addressDropdown = $("#countryDdl");

                    // init plugin
                    telInput.intlTelInput({
                        utilsScript: "Scripts/PhoneInput/utils.js" // just for formatting/placeholders etc
                    });

                    // populate the country dropdown
                    $.each(countryData, function (i, country) {
                        addressDropdown.append($("<option></option>").attr("value", country.iso2).text(country.name));
                    });

                    // listen to the telephone input for changes
                    telInput.change(function () {
                        var countryCode = telInput.intlTelInput("getSelectedCountryData").iso2;
                        addressDropdown.val(countryCode);
                    });

                    // trigger a fake "change" event now, to trigger an initial sync
                    telInput.change();

                    // listen to the address dropdown for changes
                    addressDropdown.change(function () {
                        var countryCode = $(this).val();
                        telInput.intlTelInput("selectCountry", countryCode);
                    });
                </script>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>Other Address Details</label>
                <asp:TextBox runat="server" ID="otherAddressDetailsTxt" CssClass="form-control input-xs" ToolTip="Other Address Details (Text)"></asp:TextBox>
            </div>
            <div class="bigspacer"></div>--%>
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

    <%--<div class="form-horizontal">
        <h4>Create a new account.</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                    CssClass="text-danger" ErrorMessage="The email field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                    CssClass="text-danger" ErrorMessage="The password field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Confirm password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn btn-default" />
            </div>
        </div>
    </div>--%>
</asp:Content>

