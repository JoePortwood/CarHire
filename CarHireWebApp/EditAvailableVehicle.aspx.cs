using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CarHireDBLibrary;
using System.Text.RegularExpressions;

namespace CarHireWebApp
{
    /// <summary>
    /// Loads all the locations that the current user owns, loads all available vehicles for a selected location
    /// then shows the information about that available vehicle to edit.
    /// </summary>
    public partial class EditAvailableVehicle : System.Web.UI.Page
    {
        /// <summary>
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                inputErrorLbl.Text = "";
                generalErrorLbl.Text = "";
                vehicleAddedLbl.Text = "";
                editAvailableVehicleBtn.Visible = false;
                LoadCurrencies();
                LoadLocations();
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        private void LoadCurrencies()
        {
            List<string> currencies = new List<string> { "", "GBP", "AUD", "EUR", "USD", "CNY", "CAD" };
            //Add the currencies to the dropdown
            foreach (string currency in currencies)
            {
                if (!currencyDdl.Items.Contains(new ListItem(currency)))
                {
                    currencyDdl.Items.Add(currency);
                }
            }
        }

        /// <summary>
        ///  Loads all the locations in the database for logged in user to be listed in the dropdown.
        /// </summary>
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

                        LoadAvailableVehicles(locationSelect.LocationID);
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
        ///  Loads the selected available vehicle for this location.
        /// </summary>
        private void LoadAvailableVehicle(long vehicleID)
        {
            activeDdl.SelectedValue = "Yes";
            if (vehiclesAvailableDdl.SelectedValue != "")
            {
                VehicleManager availableVehicle;
                List<VehicleManager> availableVehicles;
                availableVehicles = VehicleManager.GetAvailableVehicles(Convert.ToInt32(locationDdl.SelectedValue.Split(',')[0])).ToList();
                availableVehicle = availableVehicles.Where(x => x.VehicleID == vehicleID).SingleOrDefault();

                //vehicleDdl.SelectedValue = availableVehicle.VehicleID + ", " + availableVehicle.Manufacturer + " " + availableVehicle.Model;
                totalVehiclesTxt.Text = availableVehicle.TotalVehicles.ToString();
                currencyDdl.SelectedValue = availableVehicle.Currency;
                basePriceTxt.Text = availableVehicle.BasePrice.ToString();

                vehicleImg.ImageUrl = availableVehicle.ImageLoc;
                vehicleImg.AlternateText = availableVehicle.Manufacturer + " " + availableVehicle.Model;

                //make standard size of car images 270x150
                vehicleImg.Width = Variables.STANDARDWIDTH;
                vehicleImg.Height = Variables.STANDARDHEIGHT;

                if (availableVehicle.Active == true)
                {
                    activeDdl.SelectedValue = "Yes";
                }
                else
                {
                    activeDdl.SelectedValue = "No";
                }

                editAvailableVehicleBtn.Visible = true;
            }
            else
            {
                //vehicleDdl.SelectedValue = "";
                vehicleImg.ImageUrl = null;
                vehicleImg.AlternateText = null;
                vehicleImg.Width = 0;
                vehicleImg.Height = 0;

                totalVehiclesTxt.Text = "";
                currencyDdl.SelectedValue = "";
                basePriceTxt.Text = "";

                editAvailableVehicleBtn.Visible = false;
            }
        }

        private void LoadAvailableVehicles(long locationID)
        {
            List<VehicleManager> availableVehicles;
            availableVehicles = VehicleManager.GetAvailableVehicles(locationID).ToList();
            
            if (!vehiclesAvailableDdl.Items.Contains(new ListItem("")))
            {
                vehiclesAvailableDdl.Items.Add("");
            }

            foreach (VehicleManager availableVehicle in availableVehicles)
            {
                if (!vehiclesAvailableDdl.Items.Contains(new ListItem(availableVehicle.VehicleID + ", " + availableVehicle.Manufacturer + " " + availableVehicle.Model)))
                {
                    vehiclesAvailableDdl.Items.Add(availableVehicle.VehicleID + ", " + availableVehicle.Manufacturer + " " + availableVehicle.Model);
                }
            }
        }

        /// <summary>
        ///  Loads all vehicles for this location
        /// </summary>
        protected void LocationDdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (locationDdl.SelectedValue != "")
                {
                    LoadAvailableVehicles(Convert.ToInt32(locationDdl.SelectedValue.Split(',')[0]));
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Shows the information for the available vehicle selected.
        /// </summary>
        protected void VehiclesAvailableDdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (locationDdl.SelectedValue != "")
                {
                    LoadAvailableVehicle(Convert.ToInt32(vehiclesAvailableDdl.SelectedValue.Split(',')[0]));
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Updates the selected available vehicle with the new values entered in the text fields.
        /// </summary>
        protected void EditAvailableVehiclesBtn_Click(object sender, EventArgs e)
        {
            try
            {
                bool validEntries = true;
                long vehicleID = 0;
                long locationID = 0;
                int totalVehicles = 0;
                int userID = 0;
                double basePrice = 0;
                string currency;
                bool active;

                #region checkVehicle
                if (locationDdl.SelectedValue != "")
                {
                    locationID = Convert.ToInt32(locationDdl.SelectedValue.Split(',')[0]);
                }
                else
                {
                    validEntries = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "Please select a location. <br />";
                }
                if (vehiclesAvailableDdl.SelectedValue != "")
                {
                    vehicleID = Convert.ToInt32(vehiclesAvailableDdl.SelectedValue.Split(',')[0]);
                }
                else
                {
                    validEntries = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "Please select a vehicle. <br />";
                }

                if (Regex.IsMatch(totalVehiclesTxt.Text, @"^[0-9]*$"))
                {
                    totalVehicles = Convert.ToInt32(totalVehiclesTxt.Text);
                }
                else
                {
                    validEntries = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "Please enter a whole number for total vehicles. <br />";
                }

                if (currencyDdl.SelectedValue != "")
                {
                    currency = currencyDdl.SelectedValue;
                }
                else
                {
                    currency = "";
                    validEntries = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "Please enter a currency. <br />";
                }
                if (Variables.CheckDecimal(basePriceTxt.Text))
                {
                    basePrice = Convert.ToDouble(basePriceTxt.Text);
                }
                else
                {
                    validEntries = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "Please enter a base price in currency format. <br />";
                }

                if (Response.Cookies["UserID"].Value != null)
                {
                    userID = Convert.ToInt32(Response.Cookies["UserID"].Value);
                }
                else if (Session["UserID"] != null)
                {
                    userID = Convert.ToInt32(Session["UserID"]);
                }
                else
                {
                    validEntries = false;
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

                if (validEntries == true)
                {
                    VehicleManager.UpdateAvailableVehicle(vehicleID, locationID, totalVehicles, currency, basePrice, active, userID, (int)UserAccess.UserType.company);
                    vehicleAddedLbl.Text = "Available vehicle updated.";
                }
                else
                {
                    editAvailableVehicleBtn.Visible = true;
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }
    }
}