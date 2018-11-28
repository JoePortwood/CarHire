<%@ Page Title="Edit Location" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="EditLocation.aspx.cs" Inherits="CarHireWebApp.EditLocation" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript"
        src="https://maps.googleapis.com/maps/api/js?key=YOURKEY">
    </script>

    <script type="text/javascript">
        var geocoder;
        var map;
        function initialize() {
            geocoder = new google.maps.Geocoder();
            var mapOptions = {
                center: { lat: 53.553362, lng: -3.515624 },
                zoom: 6
            }
            map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
        }

        function codeAddress() {
            //Need to reinitialise map otherwise some of map is greyed out as not loaded properly
            initialize();
            var address = getAddress();

            geocoder.geocode({ 'address': address }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    map.setCenter(results[0].geometry.location);
                    document.getElementById('<%=longitudeTxt.ClientID%>').value = results[0].geometry.location.lng();
                    document.getElementById('<%=latitudeTxt.ClientID%>').value = results[0].geometry.location.lat();
                    var marker = new google.maps.Marker({
                        map: map,
                        position: results[0].geometry.location
                    });

                    var att = document.createAttribute("readonly"); //Create a "readonly" attribute
                    att.value = "true"; //Set the value of the readonly attribute
                    document.getElementById('<%=addressLine1Txt.ClientID%>').setAttributeNode(att);

                    att = document.createAttribute("readonly"); //Create a "readonly" attribute
                    att.value = "true"; //Set the value of the readonly attribute
                    document.getElementById('<%=addressLine2Txt.ClientID%>').setAttributeNode(att);

                    att = document.createAttribute("readonly"); //Create a "readonly" attribute
                    att.value = "true"; //Set the value of the readonly attribute
                    document.getElementById('<%=cityTxt.ClientID%>').setAttributeNode(att);

                    att = document.createAttribute("readonly"); //Create a "readonly" attribute
                    att.value = "true"; //Set the value of the readonly attribute
                    document.getElementById('<%=zipOrPostcodeTxt.ClientID%>').setAttributeNode(att);

                    att = document.createAttribute("readonly"); //Create a "readonly" attribute
                    att.value = "true"; //Set the value of the readonly attribute
                    document.getElementById('<%=countyStateProvinceTxt.ClientID%>').setAttributeNode(att);

                    document.getElementById("countryTxtVal").value = document.getElementById("countryTxt").value

                    att = document.createAttribute("disabled"); //Create a "disabled" attribute
                    att.value = "true"; //Set the value of the disabled attribute
                    document.getElementById("countryTxt").setAttributeNode(att);

                    //Sets lat and long checkbox to disabled and unchecked
                    document.getElementById('<%=enterManuallyChk.ClientID%>').disabled = true;
                    document.getElementById('<%=enterManuallyChk.ClientID%>').checked = false;
                    disableLatAndLong();

                    //Sets the centre of the map to the new location
                    var bounds = new google.maps.LatLngBounds();
                    var extendPoint1 = new google.maps.LatLng(place.geometry.location.lat() + 0.03, place.geometry.location.lng() + 0.03);
                    var extendPoint2 = new google.maps.LatLng(place.geometry.location.lat() - 0.03, place.geometry.location.lng() - 0.03);
                    bounds.extend(extendPoint1);
                    bounds.extend(extendPoint2);

                    map.fitBounds(bounds);

                } else {
                    alert('Geocode was not successful for the following reason: ' + status);
                }
            });
        }

        function getAddress() {
            var addressLine1 = document.getElementById('<%=addressLine1Txt.ClientID%>').value;
            var addressLine2 = document.getElementById('<%=addressLine2Txt.ClientID%>').value;
            var city = document.getElementById('<%=cityTxt.ClientID%>').value;
            var zipOrPostcode = document.getElementById('<%=zipOrPostcodeTxt.ClientID%>').value;
            var countyStateProvince = document.getElementById('<%=countyStateProvinceTxt.ClientID%>').value;
            var country = document.getElementById("countryTxt").value;
            var address;

            //Amends the address string to be geocoded depending on selections
            if (document.getElementById('<%=addressLine1Chk.ClientID%>').checked) {
                address = addressLine1;
            }
            if (document.getElementById('<%=addressLine2Chk.ClientID%>').checked) {
                address = address + ', ' + addressLine2;
            }
            if (document.getElementById('<%=cityChk.ClientID%>').checked) {
                address = address + ', ' + city;
            }
            if (document.getElementById('<%=zipOrPostcodeChk.ClientID%>').checked) {
                address = address + ', ' + zipOrPostcode;
            }
            if (document.getElementById('<%=countyStateProvinceChk.ClientID%>').checked) {
                address = address + ', ' + countyStateProvince;
            }
            if (document.getElementById('<%=countryChk.ClientID%>').checked) {
                address = address + ', ' + country;
            }
            return address;
        }

        //Cannot read text from read only textbox so only make textbox look read only
        function checkAddress(checkbox) {
            if (checkbox.checked) {
                document.getElementById('<%=longitudeTxt.ClientID%>').removeAttribute("readonly");
                document.getElementById('<%=latitudeTxt.ClientID%>').removeAttribute("readonly");
            }
            else {
                disableLatAndLong();
            }
        }

        function editAddressBtn_Click() {
            document.getElementById('<%=addressLine1Txt.ClientID%>').removeAttribute("readonly");
            document.getElementById('<%=addressLine2Txt.ClientID%>').removeAttribute("readonly");
            document.getElementById('<%=cityTxt.ClientID%>').removeAttribute("readonly");
            document.getElementById('<%=zipOrPostcodeTxt.ClientID%>').removeAttribute("readonly");
            document.getElementById('<%=countyStateProvinceTxt.ClientID%>').removeAttribute("readonly");
            document.getElementById("countryTxt").removeAttribute("disabled");

            document.getElementById('<%=longitudeTxt.ClientID%>').value = "";
            document.getElementById('<%=latitudeTxt.ClientID%>').value = "";

            document.getElementById('<%=enterManuallyChk.ClientID%>').disabled = false;
            disableLatAndLong();
        }

        //Set lat and long textboxes to readonly
        function disableLatAndLong() {
            var latTxt = document.getElementById('<%=latitudeTxt.ClientID%>');
            var longTxt = document.getElementById('<%=longitudeTxt.ClientID%>');

            var att = document.createAttribute("readonly"); // Create a "readonly" attribute
            att.value = "true"; // Set the value of the readonly attribute

            longTxt.setAttributeNode(att);

            att = document.createAttribute("readonly"); // Create a "readonly" attribute
            att.value = "true"; // Set the value of the readonly attribute

            latTxt.setAttributeNode(att);
        }

        function openModal() {
            $('#mapLocationModal').modal('show');

            //Event for after modal has loaded
            $('#mapLocationModal').on('shown.bs.modal', function (e) {
                codeAddress();
            })
        }

        google.maps.event.addDomListener(window, 'load', initialize);
    </script>

    <asp:Literal runat="server" ID="ErrorMessage" />
    <asp:Label runat="server" ID="generalErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="inputErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="locationSavedLbl" Font-Bold="true" ForeColor="Blue"></asp:Label>

    <div class="row">
        <div class="col-md-4">
            <div class="spacer"></div>
            <h3>Select Location</h3>
            <asp:DropDownList runat="server" ID="locationDdl" CssClass="btn btn-default dropdown-toggle" OnSelectedIndexChanged="LocationDdl_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            <h3>Location Details</h3>
            <div class="input-group">
                <label>*Location Name</label>
                <asp:TextBox runat="server" ID="locationNameTxt" CssClass="form-control input-xs" ToolTip="Location Name (Text)"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>*Owner Name</label>
                <asp:TextBox runat="server" ID="ownerNameTxt" CssClass="form-control input-xs" ToolTip="Owner Name (Text)"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>Capacity</label>
                <asp:TextBox runat="server" ID="capacityTxt" CssClass="form-control input-xs" ToolTip="Capacity (Number)"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>*Email Address</label>
                <asp:TextBox runat="server" ID="emailAddressTxt" CssClass="form-control input-xs" ToolTip="Email Address (Text)" TextMode="Email"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="emailAddressTxt"
                    CssClass="text-danger" ErrorMessage="<br />The email field is required." />
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>*Longitude</label>
                <asp:TextBox runat="server" ID="longitudeTxt" CssClass="form-control input-xs" ToolTip="Longitude (Decimal)"></asp:TextBox>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>*Latitude</label>
                <asp:TextBox runat="server" ID="latitudeTxt" CssClass="form-control input-xs" ToolTip="Latitude (Decimal)"></asp:TextBox>
                <div class="spacer"></div>
                <asp:CheckBox runat="server" ID="enterManuallyChk" Font-Size="Smaller" Text="Enter longitude and latitude manually" onclick="checkAddress(this)" />
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
                    var JSONLocation = '<%=new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(location)%>';
                    var locationJS = JSON.parse(JSONLocation);
                    $("#phoneNoTxt").intlTelInput("setNumber", locationJS.PhoneNo);
                </script>
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>Active</label>
                <div></div>
                <asp:DropDownList runat="server" ID="activeDdl" CssClass="dropdown-toggle"></asp:DropDownList>
            </div>
        </div>

        <div class="col-md-1">
        </div>
        
        <div class="col-md-4">

            <h3>Address</h3>
            <div class="input-group">
                <label>Address Line 1</label>
                <asp:TextBox runat="server" ID="addressLine1Txt" CssClass="form-control input-xs" ToolTip="Address Line 1 (Text)"></asp:TextBox>
                <div class="spacer"></div>
                <asp:CheckBox runat="server" ID="addressLine1Chk" Font-Size="Smaller" Text="Include in lat/long calc" Checked="true" />
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>Address Line 2</label>
                <asp:TextBox runat="server" ID="addressLine2Txt" CssClass="form-control input-xs" ToolTip="Address Line 2 (Text)"></asp:TextBox>
                <div class="spacer"></div>
                <asp:CheckBox runat="server" ID="addressLine2Chk" Font-Size="Smaller" Text="Include in lat/long calc" Checked="false" />
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>*City</label>
                <asp:TextBox runat="server" ID="cityTxt" CssClass="form-control input-xs" ToolTip="City (Text)"></asp:TextBox>
                <div class="spacer"></div>
                <asp:CheckBox runat="server" ID="cityChk" Font-Size="Smaller" Text="Include in lat/long calc" Checked="true" />
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>*Zip/Postcode</label>
                <asp:TextBox runat="server" ID="zipOrPostcodeTxt" CssClass="form-control input-xs" ToolTip="Zip/Postcode (Text)"></asp:TextBox>
                <div class="spacer"></div>
                <asp:CheckBox runat="server" ID="zipOrPostcodeChk" Font-Size="Smaller" Text="Include in lat/long calc" Checked="true" />
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>County/State</label>
                <asp:TextBox runat="server" ID="countyStateProvinceTxt" CssClass="form-control input-xs" ToolTip="County/State/Province (Text)"></asp:TextBox>
                <div class="spacer"></div>
                <asp:CheckBox runat="server" ID="countyStateProvinceChk" Font-Size="Smaller" Text="Include in lat/long calc" Checked="false" />
            </div>
            <div class="spacer"></div>
            <div class="input-group">
                <label>*Country</label>
                <input id="countryTxtVal" name="countryTxtVal" type="hidden"></input>
                <select id="countryTxt" name="countryTxt" class="form-control"></select>
                <script>
                    //Set the dropdown to the same country as the one selected in the phone section
                    // get the country data from the plugin
                    var countryData = $.fn.intlTelInput.getCountryData(),
                      telInput = $("#phoneNoTxt"),
                      addressDropdown = $("#countryTxt");

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
                <%--<asp:TextBox runat="server" ID="countryTxt" CssClass="form-control input-xs" ToolTip="Country"></asp:TextBox>--%>
                <div class="spacer"></div>
                <asp:CheckBox runat="server" ID="countryChk" Font-Size="Smaller" Text="Include in lat/long calc" Checked="true" />
            </div>
            <div id="panel">
                <asp:Label runat="server" Text="*Gets longitude and latitude based on address entered" ForeColor="Blue" Font-Size="X-Small"></asp:Label>
                <input id="getLocationBtn" type="button" value="Get Longitude and Latitude" onclick="openModal()" title="Must have address entered">
            </div>
            <div class="spacer"></div>
            <input id="editAddressBtn" type="button" value="Edit Address" onclick="editAddressBtn_Click()" title="Edit previously entered address">
            <div class="spacer"></div>
            <asp:Button runat="server" ID="updateLocationBtn" Text="Update Location" type="button" class="btn btn-primary" OnClick="LocationUpdateBtn_Click"></asp:Button>
        
        </div>
    </div>

    <!-- Modal -->
    <div class="modal fade bs-example-modal-lg" id="mapLocationModal" tabindex="-1" role="dialog" aria-labelledby="getLocationBtn" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title" id="SIPPCodeModalLabel">Location Selected</h4>
                </div>
                <div class="modal-body">
                    <div id="map-canvas"></div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
