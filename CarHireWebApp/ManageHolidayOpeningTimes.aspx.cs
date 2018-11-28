using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CarHireDBLibrary;
using System.Text.RegularExpressions;
using Ajaxified;
using System.Globalization;

namespace CarHireWebApp
{
    /// <summary>
    ///  This class shows holiday times in the database.
    /// </summary>
    /// <remarks>
    /// Session variables used for error and status text due to page redirect.
    /// </remarks>
    public partial class ManageHolidayOpeningTimes : System.Web.UI.Page
    {
        /// <summary>
        /// List of holiday dates to be transfered to javascript for use in the jquery calendar
        /// </summary>
        public List<string> holidayDates = new List<string>();

        /// <summary>
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
                AddHeaderRow();
                AddHolidayTimeRow();
                LoadLocations();
                LoadOpeningTimes();
            }
            catch (Exception ex)
            {
                Session["GeneralError"] = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Loads all the locations in the database to be listed in the dropdown.
        /// </summary>
        private void LoadLocations()
        {
            List<LocationManager> locations;
            LocationManager locationSelect;
            string locationString;

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
                    }
                    else
                    {
                        Session["InputFailed"] = "Location either doesn't exist or belongs to another company";
                    }
                }
            }
            else if (IsPostBack)
            {
                //Sets the new value of location id to the location currently selected
                var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                nameValues.Set("LocationID", locationDdl.SelectedValue.Split(',')[0]);

                //Reload page with new values
                RefreshPage(nameValues.ToString());
            }
        }

        /// <summary>
        ///  Performs page refresh to load holiday times for selected location and sets the labels to empty strings.
        /// </summary>
        protected void LocationDdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Session["HolidayStatus"] = "";
                Session["InputFailed"] = "";
            }
            catch (Exception ex)
            {
                Session["GeneralError"] = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Adds the header row the the holiday opening times table
        /// </summary>
        private void AddHeaderRow()
        {
            //Holiday opening times
            TableHeaderRow tr = new TableHeaderRow();

            TableCell cell = new TableCell();
            cell.Text = "Holiday Start";
            cell.ID = "HolidayStart";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Alt Opening Time";
            cell.ID = "AltOpenTime";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Alt Closing Time";
            cell.ID = "AltCloseTime";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Closed";
            cell.ID = "Closed";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Command";
            cell.ID = "Command";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            holidayOpeningTimesTbl.Rows.Add(tr);
        }

        /// <summary>
        ///  Adds the row that can add new holiday opening times
        /// </summary>
        private void AddHolidayTimeRow()
        {
            TableRow row = new TableRow();
            TextBox holidayStartTxt = new TextBox();
            TextBox altOpeningTimeTxt = new TextBox();
            TextBox altClosingTimeTxt = new TextBox();
            CheckBox closedChk = new CheckBox();
            Button addHolidayOpeningTimeBtn = new Button();
            TimePicker timeSelected;

            //Textbox for the start date
            TableCell cell = new TableCell();
            holidayStartTxt.CssClass = "input-xs";
            holidayStartTxt.ID = "HolidayStartAdd";
            cell.Controls.Add(holidayStartTxt);
            row.Cells.Add(cell);

            //Textbox for the opening time with a timepicker control
            cell = new TableCell();
            timeSelected = new TimePicker();
            altOpeningTimeTxt.CssClass = "input-xs";
            altOpeningTimeTxt.ID = "AltOpenTimeAdd";
            timeSelected.TargetControlID = "AltOpenTimeAdd";
            cell.Controls.Add(altOpeningTimeTxt);
            cell.Controls.Add(timeSelected);
            row.Cells.Add(cell);

            //Textbox for the closing time with a timepicker control
            cell = new TableCell();
            timeSelected = new TimePicker();
            altClosingTimeTxt.CssClass = "input-xs";
            altClosingTimeTxt.ID = "AltCloseTimeAdd";
            timeSelected.TargetControlID = "AltCloseTimeAdd";
            cell.Controls.Add(altClosingTimeTxt);
            cell.Controls.Add(timeSelected);
            row.Cells.Add(cell);

            //Checkbox which when clicked clears the text in opening and closing times
            cell = new TableCell();
            closedChk.ID = "ClosedAdd";
            closedChk.Attributes.Add("onclick", "JavaScript:clearText();");
            cell.Controls.Add(closedChk);
            row.Cells.Add(cell);

            //Button to add the new holiday opening time
            cell = new TableCell();
            addHolidayOpeningTimeBtn.Text = "Add";
            addHolidayOpeningTimeBtn.ID = "AddBtn";
            addHolidayOpeningTimeBtn.CssClass = "btn btn-primary";
            addHolidayOpeningTimeBtn.Click += new EventHandler(HolidayTimeAddBtn_Click);
            cell.Controls.Add(addHolidayOpeningTimeBtn);
            row.Cells.Add(cell);

            holidayOpeningTimesTbl.Rows.Add(row);
        }

        /// <summary>
        ///  Load holiday opening times and dates for location in dropdown box
        /// </summary>
        private void LoadOpeningTimes()
        {
            List<OpeningTime> holidayOpeningTimes;
            long locationID = 0;
            TableRow row;
            TableCell cell;
            CheckBox closedChk;
            Button deleteBtn;

            //Put text into the status labels if there is a session variable saved
            if (Session["HolidayStatus"] != null)
            {
                timeSavedLbl.Text = Session["HolidayStatus"].ToString();
                Session["HolidayStatus"] = null;
            }
            else
            {
                timeSavedLbl.Text = "";
            }

            if (Session["GeneralError"] != null)
            {
                generalErrorLbl.Text = Session["GeneralError"].ToString();
                Session["GeneralError"] = null;
            }
            else
            {
                generalErrorLbl.Text = "";
            }

            if (Session["InputFailed"] != null)
            {
                inputErrorLbl.Text = Session["InputFailed"].ToString();
                Session["InputFailed"] = null;
            }
            else
            {
                inputErrorLbl.Text = "";
            }


            if (locationDdl.SelectedValue != "")
            {
                //Gets the location id from the dropdown
                locationID = Convert.ToInt32(Regex.Match(locationDdl.SelectedValue, @"\d+").Value);
                holidayOpeningTimesTbl.Visible = true;

                holidayOpeningTimes = OpeningTime.GetHolidayOpeningTimesByLocationID(locationID);

                foreach (OpeningTime holidayOpeningTime in holidayOpeningTimes)
                {
                    //Add current holiday dates to disable in jquery datepicker
                    holidayDates.Add(holidayOpeningTime.HolidayStartDate.ToString("d-M-yyyy"));

                    row = new TableRow();
                    cell = new TableCell();
                    cell.ID = holidayOpeningTime.LocationID + "_" + holidayOpeningTime.HolidayStartDate + "_StartDate";
                    cell.Text = holidayOpeningTime.HolidayStartDate.ToString("dd/MM/yyyy");
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    //If there is a opening time in the database then display on holiday date record
                    if (holidayOpeningTime.Closed == false && holidayOpeningTime.HolidayOpenTime != null)
                    {
                        cell.Text = holidayOpeningTime.HolidayOpenTime.Value.ToString("HH:mm");                       
                    }
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    //If there is a closing time in the database then display on holiday date record
                    if (holidayOpeningTime.Closed == false && holidayOpeningTime.HolidayCloseTime != null)
                    {
                        cell.Text = holidayOpeningTime.HolidayCloseTime.Value.ToString("HH:mm");  
                    }
                    row.Cells.Add(cell);

                    closedChk = new CheckBox();
                    cell = new TableCell();
                    if (holidayOpeningTime.Closed == true)
                    {
                        closedChk.Checked = true;
                    }
                    else
                    {
                        closedChk.Checked = false;
                    }
                    closedChk.Enabled = false;
                    cell.Controls.Add(closedChk);
                    row.Cells.Add(cell);

                    //Delete button to delete the row the delete button is on
                    deleteBtn = new Button();
                    deleteBtn.ID = holidayOpeningTime.LocationID + "_" + holidayOpeningTime.HolidayStartDate.ToString("dd.MM.yyyy") + "_DelBtn";
                    deleteBtn.Text = "Delete";
                    deleteBtn.CssClass = "btn btn-primary";
                    deleteBtn.Click += new EventHandler(DeleteHolidayBtn_Click);
                    cell = new TableCell();
                    cell.Controls.Add(deleteBtn);
                    row.Cells.Add(cell);
                    holidayOpeningTimesTbl.Rows.Add(row);
                }
            }
            else
            {
                holidayOpeningTimesTbl.Visible = false;
            }
        }

        /// <summary>
        ///  Adds a new holiday opening time record to the selected location
        /// </summary>
        protected void HolidayTimeAddBtn_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox holidayStartTxt;
                TextBox altOpeningTimeTxt;
                TextBox altClosingTimeTxt;
                CheckBox closedChk;
                DateTime? openTimeChk = null, closeTimeChk = null;
                DateTime openTime = DateTime.Now, closeTime = DateTime.Now;
                DateTime holidayStartDate;
                bool validDates = true;

                holidayStartTxt = (TextBox)holidayOpeningTimesTbl.FindControl("HolidayStartAdd");
                altOpeningTimeTxt = (TextBox)holidayOpeningTimesTbl.FindControl("altOpenTimeAdd");
                altClosingTimeTxt = (TextBox)holidayOpeningTimesTbl.FindControl("altCloseTimeAdd");
                closedChk = (CheckBox)holidayOpeningTimesTbl.FindControl("ClosedAdd");

                //Checks the start date is in a dd/mm/yyyy format
                validDates = DateTime.TryParseExact(holidayStartTxt.Text, "dd/MM/yyyy",
                                    CultureInfo.InvariantCulture,
                                    DateTimeStyles.None,
                                    out holidayStartDate);

                if (closedChk.Checked == false)
                {
                    if (OpeningTime.CheckTimeValid(altOpeningTimeTxt.Text) == true)
                    {
                        openTime = Convert.ToDateTime(altOpeningTimeTxt.Text);
                        openTimeChk = openTime;
                    }
                    else if (altOpeningTimeTxt.Text == "")
                    {
                        openTimeChk = null;
                    }
                    else
                    {
                        validDates = false;
                        Session["InputFailed"] = Session["InputFailed"] + "Invalid opening date entered <br />";
                    }

                    if (OpeningTime.CheckTimeValid(altClosingTimeTxt.Text) == true)
                    {
                        closeTime = Convert.ToDateTime(altClosingTimeTxt.Text);
                        closeTimeChk = closeTime;
                    }
                    else if (altClosingTimeTxt.Text == "")
                    {
                        closeTimeChk = null;
                    }
                    else
                    {
                        validDates = false;
                        Session["InputFailed"] = Session["InputFailed"] + "Invalid closing date entered <br />";
                    }

                    if (openTimeChk > closeTimeChk)
                    {
                        validDates = false;
                        Session["InputFailed"] = Session["InputFailed"] + "End time must be after start time <br />";
                    }
                }

                if (validDates == true)
                {
                    Session["HolidayStatus"] = "Holiday time saved";
                    Session["InputFailed"] = "";

                    if (closeTimeChk != null && openTimeChk != null)
                    {
                        OpeningTime.InsertHolidayOpeningTimes(Convert.ToInt32(Request.QueryString["LocationID"]), holidayStartDate, openTimeChk,
                            closeTimeChk, closedChk.Checked);
                    }
                    //No times entered so closed selected
                    else
                    {
                        OpeningTime.InsertHolidayOpeningTimes(Convert.ToInt32(Request.QueryString["LocationID"]), holidayStartDate, openTimeChk,
                            closeTimeChk, true);
                    }
                }
                else
                {
                    //Session["InputFailed"] = "Time or date entered was not in the correct format. Please try again.";
                    Session["HolidayStatus"] = "";
                }

                //Reload dates to include new holiday that has just been added
                RefreshPage("LocationID=" + Request.QueryString["LocationID"]);
            }
            catch (Exception ex)
            {
                Session["GeneralError"] = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Deletes the selected row
        /// </summary>
        protected void DeleteHolidayBtn_Click(object sender, EventArgs e)
        {
            try
            {
                long locationID;
                DateTime holidayStart;

                //Find the ID of the button clicked
                Button deleteBtn = (Button)sender;

                //Only gets numbers at the start of the button ID
                locationID = Convert.ToInt32(deleteBtn.ID.Split('_')[0]);
                holidayStart = Convert.ToDateTime(deleteBtn.ID.Split('_')[1]);

                OpeningTime.DeleteHolidayOpeningTime(locationID, holidayStart);

                //Refresh the page without deleted record
                RefreshPage("LocationID=" + locationID.ToString());

                Session["InputFailed"] = "";
                Session["HolidayStatus"] = "Holiday time deleted";
            }
            catch (Exception ex)
            {
                Session["GeneralError"] = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
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
    }
}