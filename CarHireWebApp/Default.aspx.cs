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
    ///  This class shows locations and places them on a map.
    ///  A slideshow is also displayed using javascript in the aspx file.
    /// </summary>
    public partial class _Default : Page
    {
        public List<LocationManager> locations;
        public List<OpeningTime> openingTimes, holidayOpeningTimes;

        /// <summary>
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                generalErrorLbl.Text = "";

                //Load the locations and their opening times for using in the javascript
                locations = LocationManager.GetLocations().Where(x => x.Active == true).ToList();
                openingTimes = OpeningTime.GetOpeningTimes();
                holidayOpeningTimes = OpeningTime.GetHolidayOpeningTimes();

                if (!IsPostBack)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "CallhideMap", "hideMap()", true);
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }
    }
}