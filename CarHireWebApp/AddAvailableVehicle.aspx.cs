using CarHireDBLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarHireWebApp
{
    /// <summary>
    ///  Adds a new vehicle that is linked to a location and listed as available.
    /// </summary>
    public partial class AddAvailableVehicle : System.Web.UI.Page
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
                vehiclesAvailableTbl.Visible = false;
                LoadCurrencies();
                LoadLocations();
                LoadVehicles();
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

            locations = LocationManager.GetLocations().Where(x => x.UserID == Variables.GetUser(Session["UserID"].ToString())).ToList();

            //Adds a blank row to the dropdown
            if (!locationDdl.Items.Contains(new ListItem("")))
            {
                locationDdl.Items.Add("");
            }

            //Add the locations to the dropdown
            foreach (LocationManager location in locations)
            {
                if (!locationDdl.Items.Contains(new ListItem(location.LocationID + ", " + location.LocationName + ", " + location.OwnerName)))
                {
                    locationDdl.Items.Add(location.LocationID + ", " + location.LocationName + ", " + location.OwnerName);
                }
            }
        }

        /// <summary>
        ///  Load all the vehicles that the logged in company has ownership of.
        /// </summary>
        private void LoadVehicles()
        {
            List<VehicleManager> vehicles;

            vehicles = VehicleManager.GetVehicles().Where(x => x.UserID == Variables.GetUser(Session["UserID"].ToString())).ToList();

            //Adds a blank row to the dropdown
            if (!vehicleDdl.Items.Contains(new ListItem("")))
            {
                vehicleDdl.Items.Add("");
            }

            //Add the vehicles to the dropdown
            foreach (VehicleManager vehicle in vehicles)
            {
                if (!vehicleDdl.Items.Contains(new ListItem(vehicle.VehicleID + ", " + vehicle.Manufacturer + " " + vehicle.Model)))
                {
                    vehicleDdl.Items.Add(vehicle.VehicleID + ", " + vehicle.Manufacturer + " " + vehicle.Model);
                }
            }
        }

        /// <summary>
        ///  Displays the image of the vehicle once a vehicle has been selected in the drop down list.
        /// </summary>
        protected void VehicleDdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (vehicleDdl.SelectedValue != "")
                {
                    List<VehicleManager> vehicles;
                    vehicles = VehicleManager.GetVehicles().Where(x => x.VehicleID.ToString() == vehicleDdl.SelectedValue.Split(',')[0]).ToList();

                    if (vehicles.Count > 0)
                    {
                        vehicleImg.ImageUrl = vehicles[0].ImageLoc;
                        vehicleImg.AlternateText = vehicles[0].Manufacturer + " " + vehicles[0].Model;

                        //make standard size of car images 270x150
                        vehicleImg.Width = Variables.STANDARDWIDTH;
                        vehicleImg.Height = Variables.STANDARDHEIGHT;
                    }
                }
                else
                {
                    vehicleImg.ImageUrl = null;
                    vehicleImg.AlternateText = null;

                    vehicleImg.Width = 0;
                    vehicleImg.Height = 0;
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Adds a new vehicle to be listed against this location as available to book.
        /// </summary>
        protected void AddAvailableVehiclesBtn_Click(object sender, EventArgs e)
        {
            try
            {
                List<VehicleManager> vehiclesAvailable = new List<VehicleManager>();
                bool validEntries = true;
                long vehicleID = 0;
                long locationID = 0;
                int totalVehicles = 0;
                int userID = 0;
                double basePrice = 0;
                string currency;

                #region checkValidity
                if (locationDdl.SelectedValue != "")
                {
                    locationID = Convert.ToInt32(locationDdl.SelectedValue.Split(',')[0]);
                    vehiclesAvailable = VehicleManager.GetAvailableVehicles(locationID);
                }
                else
                {
                    validEntries = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "Please select a location. <br />";
                }
                if (vehicleDdl.SelectedValue != "")
                {
                    vehicleID = Convert.ToInt32(vehicleDdl.SelectedValue.Split(',')[0]);
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

                foreach (VehicleManager vehicle in vehiclesAvailable)
                {
                    if (vehicle.VehicleID == vehicleID)
                    {
                        validEntries = false;
                        inputErrorLbl.Text = inputErrorLbl.Text + "This vehicle is already assigned to this location. Please select another. <br />";
                    }
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
                #endregion

                if (validEntries == true)
                {
                    VehicleManager.InsertNewAvailableVehicle(vehicleID, locationID, totalVehicles, currency, basePrice, userID, (int)UserAccess.UserType.company);
                    vehicleAddedLbl.Text = "New available vehicle added.";
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }
    }
}