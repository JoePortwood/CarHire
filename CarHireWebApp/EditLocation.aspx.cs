using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CarHireDBLibrary;

namespace CarHireWebApp
{
    /// <summary>
    /// This class lists all the locations the logged in company owns then once one of these locations is selected
    /// the current information in loaded into the textboxes for the user to be able to change.
    /// </summary>
    public partial class EditLocation : System.Web.UI.Page
    {
        //Location made public for use in javascript.
        public LocationManager location;

        /// <summary>
        /// Must be logged in as a company.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["LoggedInType"] == null)
                {
                    Response.Redirect(Variables.REDIRECT, false);
                }
                else if (Session["LoggedInType"].ToString() == "")
                {
                    Response.Redirect(Variables.REDIRECT, false);
                }
                else if (Session["LoggedInType"].ToString() == "Customer")
                {
                    Response.Redirect(Variables.REDIRECT, false);
                }
                generalErrorLbl.Text = "";
                inputErrorLbl.Text = "";
                locationSavedLbl.Text = "";
                updateLocationBtn.Visible = false;

                LoadLocations();

                longitudeTxt.Attributes.Add("readonly", "readonly");
                latitudeTxt.Attributes.Add("readonly", "readonly");
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        private void LoadLocations()
        {
            List<LocationManager> locations;
            LocationManager locationSelect;
            string locationString;

            if (activeDdl.Items.Count < 1)
            {
                activeDdl.Items.Add("Yes");
                activeDdl.Items.Add("No");
            }

            locations = LocationManager.GetLocations().Where(x => x.UserID == Variables.GetUser(Session["UserID"].ToString())).ToList();

            if (!locationDdl.Items.Contains(new ListItem("")))
            {
                locationDdl.Items.Add("");
            }

            foreach (LocationManager location in locations)
            {
                if (!locationDdl.Items.Contains(new ListItem(location.LocationID + ", " + location.LocationName + ", " + location.OwnerName)))
                {
                    locationDdl.Items.Add(location.LocationID + ", " + location.LocationName + ", " + location.OwnerName);
                }

            }

            if (Request.QueryString["LocationID"] != null && !IsPostBack)
            {
                //Gets the location information for the location id passed through the URL
                locationString = Request.QueryString["LocationID"];
                if (locationString != "")
                {
                    //Only returns one value
                    locations = locations.Where(x => x.LocationID == Convert.ToInt32(locationString)).ToList();

                    if (locations.Count >= 1)
                    {
                        locationSelect = locations[0];

                        //Sets the location to information passed through URL
                        locationString = locationString + ", " + locationSelect.LocationName + ", " + locationSelect.OwnerName;
                        locationDdl.Items.FindByText(locationString).Selected = true;

                        LoadLocation();
                    }
                    else
                    {
                        inputErrorLbl.Text = "Location either doesn't exist or belongs to another company";
                    }
                }
            }
            else if (IsPostBack)
            {
                //If page is reloaded any labels that are set are set back to default
                //Sets the new value of location id to the location currently selected
                var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                nameValues.Set("LocationID", locationDdl.SelectedValue.Split(',')[0]);
                if (Session["PreviousLocationID"] != null)
                {
                    //If current locationid is not the same as the last location id on the page load then reload with new query string
                    if (Session["PreviousLocationID"].ToString() != nameValues.ToString())
                    {
                        Session["PreviousLocationID"] = nameValues.ToString();
                        string url = Request.Url.AbsolutePath;
                        string updatedQueryString = "?" + nameValues.ToString();
                        Response.Redirect(url + updatedQueryString, false);
                    }
                }
                else
                {
                    //If this is the first time loading the data
                    string url = Request.Url.AbsolutePath;
                    string updatedQueryString = "?" + nameValues.ToString();
                    Response.Redirect(url + updatedQueryString, false);
                    Session["PreviousLocationID"] = "LocationID=" + locationDdl.SelectedValue.Split(',')[0];
                }
            }
        }

        /// <summary>
        ///  Loads the selected location.
        /// </summary>
        protected void LocationDdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadLocation();
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Loads the details of the selected location into all the textboxes.
        /// </summary>
        private void LoadLocation()
        {
            activeDdl.SelectedValue = "Yes";
            if (locationDdl.SelectedValue != "")
            {
                location = LocationManager.GetLocations().Where(x => x.LocationID.ToString() == locationDdl.SelectedValue.Split(',')[0]).SingleOrDefault();

                locationNameTxt.Text = location.LocationName;
                ownerNameTxt.Text = location.OwnerName;
                if (location.Capacity != 0)
                {
                    capacityTxt.Text = location.Capacity.ToString();
                }
                else
                {
                    capacityTxt.Text = "";
                }
                addressLine1Txt.Text = location.AddressLine1;
                addressLine2Txt.Text = location.AddressLine2;
                cityTxt.Text = location.City;
                zipOrPostcodeTxt.Text = location.ZipOrPostcode;
                countyStateProvinceTxt.Text = location.CountyStateProvince;

                //Manually enter the JavaScript code from here to set the phone number and country
                var script = "$('#phoneNoTxt').intlTelInput('selectCountry', '" + Variables.GetCodeByCountry(location.Country) + "');  \n";
                script = script + "addressDropdown.val('" + Variables.GetCodeByCountry(location.Country) + "')";
                ClientScript.RegisterStartupScript(typeof(string), "textvaluesetter", script, true);

                emailAddressTxt.Text = location.EmailAddress;
                longitudeTxt.Text = location.Longitude.ToString();
                latitudeTxt.Text = location.Latitude.ToString();

                if (location.Active == true)
                {
                    activeDdl.SelectedValue = "Yes";
                }
                else
                {
                    activeDdl.SelectedValue = "No";
                }

                updateLocationBtn.Visible = true;
            }
            else
            {
                locationNameTxt.Text = "";
                ownerNameTxt.Text = "";
                capacityTxt.Text = "";
                addressLine1Txt.Text = "";
                addressLine2Txt.Text = "";
                cityTxt.Text = "";
                zipOrPostcodeTxt.Text = "";
                countyStateProvinceTxt.Text = "";
                emailAddressTxt.Text = "";
                longitudeTxt.Text = "";
                latitudeTxt.Text = "";

                updateLocationBtn.Visible = false;
            }
        }

        protected void LocationUpdateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string locationName, ownerName, addressLine1, addressLine2, city, zipOrPostcode,
                    countyStateProvince, countryCode, countryName, phoneNo, emailAddress;
                int capacity = 0, tryParseNumber, userID = 0;
                double longitude = 0, latitude = 0;
                bool editLocation = true, active; //boolean to check all fields are entered correctly
                long locationID;

                #region locationCheck

                if (locationNameTxt.Text != "")
                {
                    locationName = locationNameTxt.Text;
                }
                else
                {
                    locationName = "";
                    editLocation = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a location.";
                }

                if (ownerNameTxt.Text != "")
                {
                    ownerName = ownerNameTxt.Text;
                }
                else
                {
                    ownerName = "";
                    editLocation = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter an owner.";
                }

                if (Int32.TryParse(capacityTxt.Text, out tryParseNumber) == true)
                {
                    capacity = Convert.ToInt32(capacityTxt.Text);
                }
                else
                {
                    capacity = 0;
                }

                addressLine1 = addressLine1Txt.Text;
                addressLine2 = addressLine2Txt.Text;

                if (Variables.CheckAlphaNumericCharacters(cityTxt.Text) && cityTxt.Text != "")
                {
                    city = cityTxt.Text;
                }
                else
                {
                    city = "";
                    editLocation = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a valid city.";
                }

                if (Variables.CheckAlphaNumericCharacters(zipOrPostcodeTxt.Text) == true && zipOrPostcodeTxt.Text != "")
                {
                    zipOrPostcode = zipOrPostcodeTxt.Text;
                }
                else
                {
                    zipOrPostcode = "";
                    editLocation = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Invalid zip or postcode.";
                }

                countyStateProvince = countyStateProvinceTxt.Text;

                //Get from this box as getting from dropdown when it is disabled returns null value
                countryCode = Request["countryTxtVal"];
                if (countryCode == "")
                {
                    //Otherwise if value is still enabled get value directly from dropdown
                    countryCode = Request["countryTxt"];
                }

                //If nothing on the dropdown is selected
                if (countryCode == "")
                {
                    editLocation = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a country.";
                }

                phoneNo = Request["phoneNoTxt"];
                if (phoneNo == "")
                {
                    editLocation = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a phone no.";
                }

                if (emailAddressTxt.Text != "")
                {
                    emailAddress = emailAddressTxt.Text;
                }
                else
                {
                    emailAddress = "";
                    editLocation = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a email address.";
                }

                if (LocationManager.CheckLongitudeOrLatitudeValid(longitudeTxt.Text))
                {
                    longitude = Convert.ToDouble(longitudeTxt.Text);
                }
                else
                {
                    editLocation = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Invalid longitude.";
                }
                if (LocationManager.CheckLongitudeOrLatitudeValid(latitudeTxt.Text))
                {
                    latitude = Convert.ToDouble(latitudeTxt.Text);
                }
                else
                {
                    editLocation = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Invalid latitude.";
                }

                userID = Variables.GetUser(Session["UserID"].ToString());
                if (userID == 0)
                {
                    editLocation = false;
                    inputErrorLbl.Text = "Not logged in. Please login to continue";
                }

                if (activeDdl.SelectedValue == "Yes")
                {
                    active = true;
                }
                else
                {
                    active = false;
                }

                #endregion

                if (editLocation == true)
                {
                    countryName = Variables.GetCountryByCode(countryCode);
                    LocationManager.UpdateLocation(Convert.ToInt32(locationDdl.SelectedValue.Split(',')[0]), locationName, 
                        ownerName, capacity, addressLine1, addressLine2, city, zipOrPostcode, countyStateProvince, countryName, 
                        phoneNo, emailAddress, longitude, latitude, active, userID, (int)UserAccess.UserType.company);
                    locationSavedLbl.Text = "Update successful";
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }
    }
}