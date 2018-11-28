using CarHireDBLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarHireWebApp
{
    /// <summary>
    ///  This class shows the available vehicles by location.
    ///  The user can then select the date that they wish book the vehicle for.
    ///  Once they have selected the time, assuming it is in the future, the user can continue with the order.
    /// </summary>
    public partial class AvailableVehicles : System.Web.UI.Page
    {
        /// <summary>
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                inputErrorLbl.Text = "";
                generalErrorLbl.Text = "";
                vehiclesAvailableTbl.Visible = false;
                AddHeaderRow();
                LoadLocations();
                LoadAvailableVehicles();

                if (!IsPostBack)
                {
                    //Set up dates for default values
                    hireStartDateTxt.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
                    hireStartTimeTxt.Text = "10:00 am";

                    hireEndDateTxt.Text = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy");
                    hireEndTimeTxt.Text = "10:00 am";
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Loads all the locations in the database to be listed in the dropdown.
        /// </summary>
        private void LoadLocations()
        {
            List<LocationManager> locations;

            locations = LocationManager.GetLocations();

            //Adds a blank row to the dropdown
            if (!locationDdl.Items.Contains(new ListItem("")))
            {
                locationDdl.Items.Add("");
            }

            long locationID = Convert.ToInt32(Request.QueryString["LocationID"]);

            //Add the locations to the dropdown
            foreach (LocationManager location in locations)
            {
                if (!locationDdl.Items.Contains(new ListItem(location.LocationID + ", " + location.LocationName + ", " + location.OwnerName)))
                {
                    locationDdl.Items.Add(location.LocationID + ", " + location.LocationName + ", " + location.OwnerName);

                    //Select value if location ID entered in query string
                    if (locationID == location.LocationID)
                    {
                        locationDdl.SelectedValue = location.LocationID + ", " + location.LocationName + ", " + location.OwnerName.ToString();
                        vehiclesAvailableTbl.Visible = true;
                    }
                }
            }
        }

        private void LoadAvailableVehicles()
        {
            List<VehicleManager> vehiclesAvailable = new List<VehicleManager>();
            string manufacturer, model, SIPPCodeStr, currency;
            double MPG, basePrice;
            long locationID = 0;
            SIPPCode sizeOfVehicleSIPPCode, noOfDoorsSIPPCode, transmissionAndDriveSIPPCode, fuelAndACSIPPCode;
            Button bookVehicle;
            TableRow row;

            if (locationDdl.SelectedValue != "")
            {
                locationID = Convert.ToInt32(locationDdl.SelectedValue.Split(',')[0]);

                //Filter by using the ID from the location ddl
                vehiclesAvailable = VehicleManager.GetAvailableVehicles(locationID).Where(x => x.Active == true).ToList();
            }

            foreach (VehicleManager vehicle in vehiclesAvailable)
            {
                row = new TableRow();
                Image vehicleImg = new Image();

                TableCell cell = new TableCell();

                vehicleImg.ImageUrl = vehicle.ImageLoc;
                vehicleImg.AlternateText = vehicle.Manufacturer + " " + vehicle.Model;

                //make standard size of car images 270x150
                vehicleImg.Width = Variables.STANDARDWIDTH;
                vehicleImg.Height = Variables.STANDARDHEIGHT;

                cell.Controls.Add(vehicleImg);

                row.Cells.Add(cell);
                vehiclesAvailableTbl.Rows.Add(row);

                manufacturer = vehicle.Manufacturer;
                model = vehicle.Model;
                MPG = vehicle.MPG;
                SIPPCodeStr = vehicle.SIPPCode;
                currency = vehicle.Currency;
                AddCell(row, manufacturer + " " + model + "<br />MPG: " + MPG.ToString() + "<br />SIPP: " + SIPPCodeStr);

                //Gets SIPP Code descriptions for all letters of SIPP code for this car
                sizeOfVehicleSIPPCode = SIPPCode.GetSIPPCodeDesc(Variables.SIZEOFVEHICLE, vehicle.SIPPCode[0].ToString());
                noOfDoorsSIPPCode = SIPPCode.GetSIPPCodeDesc(Variables.NOOFDOORS, vehicle.SIPPCode[1].ToString());
                transmissionAndDriveSIPPCode = SIPPCode.GetSIPPCodeDesc(Variables.TRANSMISSIONANDDRIVE, vehicle.SIPPCode[2].ToString());
                fuelAndACSIPPCode = SIPPCode.GetSIPPCodeDesc(Variables.FUELANDAC, vehicle.SIPPCode[3].ToString());

                cell = new TableCell();
                Label label = new Label();
                label.Text = sizeOfVehicleSIPPCode.Description + "<br />";
                cell.Controls.Add(label);

                label = new Label();
                label.Text = noOfDoorsSIPPCode.Description + "<br />";
                cell.Controls.Add(label);

                label = new Label();
                label.Text = transmissionAndDriveSIPPCode.Description + "<br />";
                cell.Controls.Add(label);

                label = new Label();
                label.Text = fuelAndACSIPPCode.Description + "<br />";
                cell.Controls.Add(label);

                row.Cells.Add(cell);

                basePrice = vehicle.BasePrice;
                AddCell(row, currency + ": " + basePrice.ToString());

                //Book button to book selected car
                bookVehicle = new Button();
                bookVehicle.ID = locationID + "_" + vehicle.VehicleAvailableID + "_BookBtn";
                bookVehicle.Text = "Book";
                bookVehicle.CssClass = "btn btn-primary";
                bookVehicle.Click += new EventHandler(BookVehicleBtn_Click);
                if (Session["LoggedInType"] != null)
                {
                    if (Session["LoggedInType"].ToString() == "")
                    {
                        bookVehicle.OnClientClick = "return alert('Please login to continue');";
                    }
                }
                else
                {
                    bookVehicle.OnClientClick = "return alert('Please login to continue');";
                }
                cell = new TableCell();
                cell.Controls.Add(bookVehicle);
                row.Cells.Add(cell);

                vehiclesAvailableTbl.Rows.Add(row);
            }
        }

        /// <summary>
        ///  Adds the header row the the vehicles available table.
        /// </summary>
        private void AddHeaderRow()
        {
            //Holiday opening times
            TableHeaderRow tr = new TableHeaderRow();

            TableCell cell = new TableCell();
            cell.Text = "Vehicles";
            //cell.ID = "HolidayStart";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Description";
            //cell.ID = "AltOpenTime";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Features";
            //cell.ID = "AltCloseTime";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Price";
            //cell.ID = "Closed";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            //cell.Text = "Command";
            cell.ID = "Command";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            vehiclesAvailableTbl.Rows.Add(tr);
        }

        /// <summary>
        ///  When button is pressed make the table visible.
        /// </summary>
        protected void SearchAvailableVehiclesBtn_Click(object sender, EventArgs e)
        {
            try
            {
                vehiclesAvailableTbl.Visible = true;
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Create a booking against selected car if possible.
        /// </summary>
        protected void BookVehicleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //SEND DATETIME WANTED TO BOOK FROM AND TO. ALSO SEND TIME BOOKING MADE
                long locationID, vehicleAvailableID;
                List<DateTime> dates;

                //Find the ID of the button clicked
                Button bookBtn = (Button)sender;

                dates = CheckDatesAndTimes();

                //Only gets numbers at the start of the button ID
                locationID = Convert.ToInt32(bookBtn.ID.Split('_')[0]);
                vehicleAvailableID = Convert.ToInt32(bookBtn.ID.Split('_')[1]);

                if (Session["LoggedInType"] != null)
                {
                    if (Session["LoggedInType"].ToString() == "")
                    {
                        Response.Redirect("~/Account/LoginCustomer", false);
                    }
                    else
                    {
                        if (inputErrorLbl.Text == "")
                        {
                            Response.Redirect("~/SelectOrCreateAddress?VehicleAvailableID=" + vehicleAvailableID + "&LocationID=" + locationID + "&StartDateTime=" + dates[0] +
                            "&EndDateTime=" + dates[1], false);
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Account/LoginCustomer", false);
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        private List<DateTime> CheckDatesAndTimes()
        {
            DateTime hireStartDate, hireEndDate, startTime, endTime, hireStartDateTime, hireEndDateTime;
            bool validDates;
            List<DateTime> dates = new List<DateTime>();

            validDates = DateTime.TryParseExact(hireStartDateTxt.Text, "dd/MM/yyyy",
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None,
                                out hireStartDate);

            validDates = DateTime.TryParseExact(hireEndDateTxt.Text, "dd/MM/yyyy",
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None,
                                out hireEndDate);

            if (OpeningTime.CheckTimeValid(hireStartTimeTxt.Text) == true)
            {
                startTime = Convert.ToDateTime(hireStartTimeTxt.Text);
            }
            else
            {
                startTime = DateTime.Now;
                validDates = false;
                inputErrorLbl.Text = inputErrorLbl.Text + "Invalid start date entered <br />";
            }

            if (OpeningTime.CheckTimeValid(hireEndTimeTxt.Text) == true)
            {
                endTime = Convert.ToDateTime(hireEndTimeTxt.Text);
            }
            else
            {
                endTime = DateTime.Now;
                validDates = false;
                inputErrorLbl.Text = inputErrorLbl.Text + "Invalid end date entered <br />";
            }

            hireStartDateTime = hireStartDate.Date + startTime.TimeOfDay;
            hireEndDateTime = hireEndDate.Date + endTime.TimeOfDay;

            //Check minimum amount of hours is 12 to continue
            if ((hireEndDateTime - hireStartDateTime).TotalHours <= 12)
            {
                validDates = false;
                inputErrorLbl.Text = inputErrorLbl.Text + "End date must be after start date <br />";
            }

            dates.Add(hireStartDateTime);
            dates.Add(hireEndDateTime);
            return dates;
        }

        /// <summary>
        ///  Adds cells with text to vehicles available table.
        /// </summary>
        private void AddCell(TableRow row, string value)
        {
            TableCell cell = new TableCell();
            cell.Text = value;

            row.Cells.Add(cell);

            vehiclesAvailableTbl.Rows.Add(row);
        }

        /// <summary>
        ///  Refreshes the page to update the query string
        /// </summary>
        private void RefreshPage(string locationID)
        {
            string url = Request.Url.AbsolutePath;
            string updatedQueryString = "?" + locationID;
            Response.Redirect(url + updatedQueryString, false);
        }

        private LocationManager GetLocation()
        {
            long locationID = Convert.ToInt32(Request.QueryString["LocationID"]);
            LocationManager location;
            location = LocationManager.GetLocations().Where(x => x.LocationID == locationID).SingleOrDefault();

            return location;
        }
    }
}