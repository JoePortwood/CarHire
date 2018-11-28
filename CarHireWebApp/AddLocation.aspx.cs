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
    /// Adds a new location to the database.
    /// </summary>
    public partial class AddLocations : System.Web.UI.Page
    {
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

                longitudeTxt.Attributes.Add("readonly", "readonly");
                latitudeTxt.Attributes.Add("readonly", "readonly");
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        /// Checks the location is valid then adds it to the database.
        /// </summary>
        protected void LocationAddBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string locationName, ownerName, addressLine1, addressLine2, city, zipOrPostcode,
                    countyStateProvince, countryCode, countryName, phoneNo, emailAddress;
                int capacity = 0, tryParseNumber, userID = 0;
                double longitude = 0, latitude = 0;
                bool insertLocation = true; //boolean to check all fields are entered correctly
                long locationID;

                #region locationCheck

                if (locationNameTxt.Text != "")
                {
                    locationName = locationNameTxt.Text;
                }
                else
                {
                    locationName = "";
                    insertLocation = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a location.";
                }

                if (ownerNameTxt.Text != "")
                {
                    ownerName = ownerNameTxt.Text;
                }
                else
                {
                    ownerName = "";
                    insertLocation = false;
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
                    insertLocation = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a valid city.";
                }

                if (Variables.CheckAlphaNumericCharacters(zipOrPostcodeTxt.Text) == true && zipOrPostcodeTxt.Text != "")
                {
                    zipOrPostcode = zipOrPostcodeTxt.Text;
                }
                else
                {
                    zipOrPostcode = "";
                    insertLocation = false;
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
                    insertLocation = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a country.";
                }

                phoneNo = Request["phoneNoTxt"];
                if (phoneNo == "")
                {
                    insertLocation = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a phone no.";
                }

                if (emailAddressTxt.Text != "")
                {
                    emailAddress = emailAddressTxt.Text;
                }
                else
                {
                    emailAddress = "";
                    insertLocation = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a email address.";
                }

                if (LocationManager.CheckLongitudeOrLatitudeValid(longitudeTxt.Text))
                {
                    longitude = Convert.ToDouble(longitudeTxt.Text);
                }
                else
                {
                    insertLocation = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Invalid longitude.";
                }
                if (LocationManager.CheckLongitudeOrLatitudeValid(latitudeTxt.Text))
                {
                    latitude = Convert.ToDouble(latitudeTxt.Text);
                }
                else
                {
                    insertLocation = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Invalid latitude.";
                }

                userID = Variables.GetUser(Session["UserID"].ToString());
                if (userID == 0)
                {
                    insertLocation = false;
                    inputErrorLbl.Text = "Not logged in. Please login to continue";
                }

                #endregion

                if (insertLocation == true)
                {
                    countryName = Variables.GetCountryByCode(countryCode);
                    LocationManager.AddNewLocation(locationName, ownerName, capacity, addressLine1, addressLine2,
                        city, zipOrPostcode, countyStateProvince, countryName, phoneNo, emailAddress, longitude, latitude, 
                        userID, (int)UserAccess.UserType.company);
                    locationSavedLbl.Text = "Save successful";

                    locationID = LocationManager.GetLastAddedLocation();
                    OpeningTime.InsertDefaultOpeningTimes(locationID);

                    //Proceed on to opening times page
                    Response.Redirect("AddOpeningTime.aspx?LocationID=" + locationID, false);
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }
    }
}