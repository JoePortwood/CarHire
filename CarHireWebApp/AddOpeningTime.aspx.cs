using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CarHireDBLibrary;
using System.Text.RegularExpressions;
using MKB.TimePicker;
using Ajaxified;

namespace CarHireWebApp
{
    /// <summary>
    ///  Adds opening times to selected location for each day of the week.
    /// </summary>
    public partial class AddOpeningTime : System.Web.UI.Page
    {
        /// <summary>
        ///  Must be logged in as a company.
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
                timeSavedLbl.Text = "";

                LoadTableControls();
                LoadLocations();
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Loads location either from url string or drop down list.
        /// </summary>
        private void LoadLocations()
        {
            List<LocationManager> locations;
            LocationManager locationSelect;
            string locationString;

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

                        LoadOpeningTimes();
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
                    //If this is the first time loading the table
                    string url = Request.Url.AbsolutePath;
                    string updatedQueryString = "?" + nameValues.ToString();
                    Response.Redirect(url + updatedQueryString, false);
                    Session["PreviousLocationID"] = "LocationID=" + locationDdl.SelectedValue.Split(',')[0];
                }
            }
        }

        /// <summary>
        ///  Loads the table to edit the opening times.
        /// </summary>
        private void LoadTableControls()
        {
            TableRow row = new TableRow();
            CheckBox closedChk;
            TimePicker timeSelected;
            TextBox timeTxt;
            TableCell cell;

            AddHeaderRow();

            //Add open cell
            cell = new TableCell();
            cell.Width = 110;
            cell.Text = "Open";
            cell.CssClass = "h4";
            row.Cells.Add(cell);

            //For when no times are in the database
            {
                for (int i = 1; i <= 7; i++)
                {
                    timeSelected = new TimePicker();
                    timeTxt = new TextBox();
                    timeTxt.CssClass = "form-control input-xs";
                    timeTxt.Width = 70;
                    timeTxt.ID = i + "_open";
                    timeSelected.TargetControlID = i + "_open";

                    cell = new TableCell();
                    cell.Width = 110;
                    cell.Controls.Add(timeTxt);
                    cell.Controls.Add(timeSelected);
                    row.Cells.Add(cell);
                }

                openingTimesTbl.Rows.Add(row);
                row = new TableRow();

                //Add closed cell
                cell = new TableCell();
                cell.Width = 110;
                cell.Text = "Close";
                cell.CssClass = "h4";
                row.Cells.Add(cell);

                for (int i = 1; i <= 7; i++)
                {
                    timeSelected = new TimePicker();
                    timeTxt = new TextBox();
                    timeTxt.CssClass = "form-control input-xs";
                    timeTxt.Width = 70;
                    timeTxt.ID = i + "_close";
                    timeSelected.TargetControlID = i + "_close";

                    closedChk = new CheckBox();
                    closedChk.Text = "Closed";
                    closedChk.ID = i + "_Chk";
                    closedChk.Attributes.Add("onclick", "JavaScript:clearText(" + i + ");");
                    closedChk.Checked = true;

                    cell = new TableCell();
                    cell.Width = 110;
                    cell.Controls.Add(timeTxt);
                    cell.Controls.Add(timeSelected);
                    cell.Controls.Add(closedChk);
                    row.Cells.Add(cell);
                }
                openingTimesTbl.Rows.Add(row);
            }
        }

        /// <summary>
        ///  Adds table header for opening times table.
        /// </summary>
        private void AddHeaderRow()
        {
            TableHeaderRow tr = new TableHeaderRow();

            TableCell cell = new TableCell();
            cell.Text = "Status";
            cell.ID = "Status";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Monday";
            cell.ID = "Monday";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Tuesday";
            cell.ID = "Tuesday";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Wednesday";
            cell.ID = "Wednesday";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Thursday";
            cell.ID = "Thursday";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Friday";
            cell.ID = "Friday";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Saturday";
            cell.ID = "Saturday";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Sunday";
            cell.ID = "Sunday";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);
            openingTimesTbl.Rows.Add(tr);
        }

        /// <summary>
        ///  Loads opening times.
        /// </summary>
        protected void LocationDdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            { 
                LoadOpeningTimes();
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Loads the opening times for this location.
        ///  If closed is set to true then no times will be displayed.
        /// </summary>
        private void LoadOpeningTimes()
        {
            List<OpeningTime> openingTimes, holidayOpeningTimes;
            long locationID = 0;
            TextBox openTimeTxt, closeTimeTxt;
            CheckBox timeSelectChk;

            if (locationDdl.SelectedValue != "")
            {
                locationID = Convert.ToInt32(Regex.Match(locationDdl.SelectedValue, @"\d+").Value);
                openingTimesTbl.Visible = true;
                updateTimesBtn.Visible = true;
            }
            else
            {
                openingTimesTbl.Visible = false;
                updateTimesBtn.Visible = false;
            }

            openingTimes = OpeningTime.GetOpeningTimesByLocationID(locationID);
            holidayOpeningTimes = OpeningTime.GetHolidayOpeningTimesByLocationID(locationID);

            foreach (OpeningTime holidayOpeningTime in holidayOpeningTimes)
            {
                TableRow row = new TableRow();
                TableCell cell = new TableCell();
                cell.Text = holidayOpeningTime.HolidayStartDateStr;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = holidayOpeningTime.HolidayEndDateStr;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = holidayOpeningTime.OpenTimeStr;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = holidayOpeningTime.CloseTimeStr;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = holidayOpeningTime.DayOfWeek;
                row.Cells.Add(cell);
                //holidayOpeningTimesTbl.Rows.Add(row);

                cell = new TableCell();
                if (holidayOpeningTime.Closed == true)
                {
                    cell.Text = "Closed";
                }
                else
                {
                    cell.Text = "Open";
                }
                row.Cells.Add(cell);
            }

            for (int i = 1; i <= 7; i++)
            {
                openTimeTxt = (TextBox)openingTimesTbl.FindControl(i + "_open");
                closeTimeTxt = (TextBox)openingTimesTbl.FindControl(i + "_close");
                timeSelectChk = (CheckBox)openingTimesTbl.FindControl(i + "_Chk");

                //Reset to default values if drop down changed
                openTimeTxt.Text = "";
                closeTimeTxt.Text = "";
                timeSelectChk.Checked = true;
            }

            if (openingTimes.Count > 0)
            {
                foreach (OpeningTime openingTime in openingTimes)
                {
                    //WHEN FINDING CONTROL ":" CANNOT BE USED OTHERWISE IT WON'T FIND IT
                    openTimeTxt = (TextBox)openingTimesTbl.FindControl(openingTime.DayOfWeekNum + "_open");
                    closeTimeTxt = (TextBox)openingTimesTbl.FindControl(openingTime.DayOfWeekNum + "_close");
                    timeSelectChk = (CheckBox)openingTimesTbl.FindControl(openingTime.DayOfWeekNum + "_Chk");

                    if (openingTime.Closed == false)
                    {
                        if (openingTime.OpenTimeStr != "00:00")
                        {
                            openTimeTxt.Text = openingTime.OpenTime.ToString("hh:mm tt");
                        }

                        if (openingTime.CloseTimeStr != "00:00")
                        {
                            closeTimeTxt.Text = openingTime.CloseTime.ToString("hh:mm tt");
                        }

                        timeSelectChk.Checked = false;
                    }
                }
            }
        }

        /// <summary>
        ///  Updates the database with all the times currently in the table for the selected location.
        /// </summary>
        protected void AddUpdateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                List<OpeningTime> openingTimes;
                long locationID;
                TextBox openTimeTxt, closeTimeTxt;
                CheckBox closedChk;
                DateTime? openTimeChk = null, closeTimeChk = null;
                DateTime openTime = DateTime.Now, closeTime = DateTime.Now;
                bool validDates = true;

                openingTimesTbl.Visible = true;

                locationID = Convert.ToInt32(Regex.Match(locationDdl.SelectedValue, @"\d+").Value);

                openingTimes = OpeningTime.GetOpeningTimesByLocationID(locationID);

                foreach (OpeningTime openingTime in openingTimes)
                {
                    openTimeTxt = (TextBox)openingTimesTbl.FindControl(openingTime.DayOfWeekNum + "_open");
                    closeTimeTxt = (TextBox)openingTimesTbl.FindControl(openingTime.DayOfWeekNum + "_close");
                    closedChk = (CheckBox)openingTimesTbl.FindControl(openingTime.DayOfWeekNum + "_Chk");

                    validDates = true;
                    if (OpeningTime.CheckTimeValid(openTimeTxt.Text) == true)
                    {
                        openTime = Convert.ToDateTime(openTimeTxt.Text);
                        openTimeChk = openTime;
                    }
                    else if (openTimeTxt.Text == "")
                    {
                        openTimeChk = null;
                    }
                    else
                    {
                        validDates = false;
                        inputErrorLbl.Text = inputErrorLbl.Text + "Invalid opening date on " + openingTime.DayOfWeek + "<br />";
                    }

                    if (OpeningTime.CheckTimeValid(closeTimeTxt.Text) == true)
                    {
                        closeTime = Convert.ToDateTime(closeTimeTxt.Text);
                        closeTimeChk = closeTime;
                    }
                    else if (closeTimeTxt.Text == "")
                    {
                        closeTimeChk = null;
                    }
                    else
                    {
                        validDates = false;
                        inputErrorLbl.Text = inputErrorLbl.Text + "Invalid closing date on " + openingTime.DayOfWeek + "<br />";
                    }

                    if (openTimeChk > closeTimeChk)
                    {
                        validDates = false;
                        inputErrorLbl.Text = inputErrorLbl.Text + "End time must be after start time for " + openingTime.DayOfWeek + "<br />";
                    }

                    if (validDates == true)
                    {
                        //Check if date has been updated and set this day to closed
                        if (closeTimeChk != null && openTimeChk != null)
                        {
                            OpeningTime.UpdateOpeningTimes(openingTime.LocationID, openingTime.DayOfWeekNum, openTime, closeTime, false);
                            closedChk.Checked = false;
                            timeSavedLbl.Text = timeSavedLbl.Text + "Save successful for " + openingTime.DayOfWeek + "<br />";
                        }
                        //If only closed date has been entered return error
                        else if (openTimeChk != null)
                        {
                            inputErrorLbl.Text = inputErrorLbl.Text + "Only opening date entered on " + openingTime.DayOfWeek + "<br />";
                        }
                        //If only open date has been entered return error
                        else if (closeTimeChk != null)
                        {
                            inputErrorLbl.Text = inputErrorLbl.Text + "Only closing date entered on " + openingTime.DayOfWeek + "<br />";
                        }
                        else
                        {
                            OpeningTime.UpdateOpeningTimes(openingTime.LocationID, openingTime.DayOfWeekNum, openTime, closeTime, true);
                            timeSavedLbl.Text = timeSavedLbl.Text + "Save successful for " + openingTime.DayOfWeek + "<br />";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            } 
        }
    }
}