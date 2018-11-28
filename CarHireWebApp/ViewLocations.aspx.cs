using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CarHireDBLibrary;
using System.Web.Script.Serialization;

namespace CarHireWebApp
{
    /// <summary>
    ///  View all the locations in the database by placing their location on Google maps.
    /// </summary>
    public partial class View_Locations : System.Web.UI.Page
    {
        //Variables for use in javascript.
        public List<LocationManager> locations;
        public List<OpeningTime> openingTimes, holidayOpeningTimes;

        /// <summary>
        ///  Loads the locations, opening times and holidays opening times for use in javascript.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                generalErrorLbl.Text = "";

                locations = LocationManager.GetLocations().Where(x => x.Active == true).ToList();
                openingTimes = OpeningTime.GetOpeningTimes();
                holidayOpeningTimes = OpeningTime.GetHolidayOpeningTimes();

                foreach (LocationManager location in locations)
                {
                    TableRow row = new TableRow();

                    TableCell cell = new TableCell();
                    cell.Text = "Phone No:";
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = location.PhoneNo;
                    row.Cells.Add(cell);

                    LocationsTbl.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }
    }
}