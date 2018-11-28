<%@ Page Title="Finish Booking" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SelectOrCreateAddress.aspx.cs" Inherits="CarHireWebApp.SelectOrCreateAddresses" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <script>
        function show() {
            $("#newAddressForm").toggle(); // if currently visible
        }
    </script>

    <asp:Label runat="server" ID="generalErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="inputErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="orderCreatedLbl" Font-Bold="true" ForeColor="Blue"></asp:Label>

    <div class="spacer"></div>

        <div class="col-md-5">
    <div class="spacer"></div>
            <asp:DropDownList runat="server" ID="customersInCompanyDdl" OnSelectedIndexChanged="CustomersInCompanyDdl_SelectedIndexChanged" CssClass="btn btn-default dropdown-toggle" Visible="false" AutoPostBack="true"></asp:DropDownList>
            <div class="spacer"></div>
            <asp:Table runat="server" ID="locationAddressTbl" CssClass="table table-striped table-bordered">
                </asp:Table>
            <div class="spacer"></div>
            <asp:Table runat="server" ID="addressesTbl" CssClass="table table-striped table-bordered">
                </asp:Table>
            <asp:Table runat="server" ID="similarAddressesTbl" Visible="false" CssClass="table table-striped table-bordered">
                </asp:Table>

            <input id="addNewAddress" class="squarebutton" onclick="show()" type="button" value="Use new address" />
            </div>

    <div class="spacer"></div>
        <div id="newAddressForm" hidden="hidden" class="col-md-5">         
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
                <label>*City</label>
                <asp:TextBox runat="server" ID="cityTxt" CssClass="form-control input-xs" ToolTip="City (Text)"></asp:TextBox>
            </div>

            <div class="input-group">
            <div class="spacer"></div>
                <label>*Zip/Postcode</label>
                <asp:TextBox runat="server" ID="zipOrPostcodeTxt" CssClass="form-control input-xs" ToolTip="Zip/Postcode (Text)"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>County/State/Province</label>
                <asp:TextBox runat="server" ID="countyStateProvinceTxt" CssClass="form-control input-xs" ToolTip="County/State/Province (Text)"></asp:TextBox>
            </div>
            <div class="input-group">
                <input id="phoneNoTxt" name="phoneNoTxt" class="form-control input-xs" type="hidden">
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
    <asp:Button runat="server" ID="addAddressBtn" Text="Add New Address" type="button" class="btn btn-primary" OnClick="AddOrderBtn_Click"></asp:Button>
            </div>
    <div class="spacer"></div>

</asp:Content>
