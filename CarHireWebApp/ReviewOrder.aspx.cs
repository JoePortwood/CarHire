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
    ///  Displays the current information of the order to give the user the option to cancel if it is incorrect.
    /// </summary>
    public partial class ReviewOrder : System.Web.UI.Page
    {
        /// <summary>
        ///  Must be logged in to view this page.
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
                //If order has not been entered
                else if (Session["Address"] == null ||
                        Session["VehicleAvailableID"] == null ||
                        Session["LocationID"] == null ||
                        Session["StartTime"] == null ||
                        Session["EndTime"] == null ||
                        Session["CustomerID"] == null)
                {
                    Response.Redirect("~/Account/InformUser.aspx?InfoString=Please+complete+ordering+process.", false);
                }
                LoadOrderInfo();
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        private void LoadOrderInfo()
        {
            VehicleManager vehicle;
            long vehicleAvailableID, locationID;
            DateTime hireStart, hireEnd;
            TableRow row;
            string manufacturer, model;
            double totalDays, totalCost;
            SIPPCode sizeOfVehicleSIPPCode, noOfDoorsSIPPCode, transmissionAndDriveSIPPCode, fuelAndACSIPPCode;
            AddressManager address;

            address = (AddressManager)Session["Address"];
            vehicleAvailableID = (long)Session["VehicleAvailableID"];
            locationID = (long)Session["LocationID"];
            hireStart = (DateTime)Session["StartTime"];
            hireEnd = (DateTime)Session["EndTime"];
            
            vehicle = VehicleManager.GetAvailableVehicles(locationID).Where(x => x.VehicleAvailableID == vehicleAvailableID).SingleOrDefault();

            totalDays = (hireEnd - hireStart).TotalDays;
            totalCost = totalDays * vehicle.BasePrice;
            totalCost = Math.Round(totalCost, 2); //Round to 2 dp
            priceLbl.Text = "Total Price: £" + totalCost.ToString() + " for " + totalDays * 24 + " hours";

            addressLbl.Text = "Pick up from address: <br />" + address.GetAddressStr();

            row = new TableRow();

            TableCell cell = new TableCell();

            vehicleImg.ImageUrl = vehicle.ImageLoc;
            vehicleImg.AlternateText = vehicle.Manufacturer + " " + vehicle.Model;

            //make standard size of car images 270x150
            vehicleImg.Width = Variables.STANDARDWIDTH;
            vehicleImg.Height = Variables.STANDARDHEIGHT;

            manufacturer = vehicle.Manufacturer;
            model = vehicle.Model;
            cell = new TableCell();

            //Gets SIPP Code descriptions for all letters of SIPP code for this car
            sizeOfVehicleSIPPCode = SIPPCode.GetSIPPCodeDesc(Variables.SIZEOFVEHICLE, vehicle.SIPPCode[0].ToString());
            noOfDoorsSIPPCode = SIPPCode.GetSIPPCodeDesc(Variables.NOOFDOORS, vehicle.SIPPCode[1].ToString());
            transmissionAndDriveSIPPCode = SIPPCode.GetSIPPCodeDesc(Variables.TRANSMISSIONANDDRIVE, vehicle.SIPPCode[2].ToString());
            fuelAndACSIPPCode = SIPPCode.GetSIPPCodeDesc(Variables.FUELANDAC, vehicle.SIPPCode[3].ToString());

            carInfoLbl.Text = "<br /><b>" + manufacturer + " " + model + "</b><br />" + sizeOfVehicleSIPPCode.Description + "<br />"
                + noOfDoorsSIPPCode.Description + "<br />" + transmissionAndDriveSIPPCode.Description + "<br />"
                + fuelAndACSIPPCode.Description + "<br />";

            dateTimeLbl.Text = "<br /><b>Hire Start: " + hireStart.ToString("dd/MM/yyyy HH:mm tt") + "<br />"
                + "Hire End: " + hireEnd.ToString("dd/MM/yyyy HH:mm tt") + "</b>";

            row.Cells.Add(cell);
        }

        /// <summary>
        ///  Stores the information and proceeds to PayPal payment page.
        /// </summary>
        protected void PayPalBtn_Click(object sender, EventArgs e)
        {
            try
            {
                AddressManager address;
                long vehicleAvailableID, locationID, customerID;
                DateTime hireStart, hireEnd;
                NVPAPICaller payPalCaller = new NVPAPICaller();
                string retMsg = "";
                string token = "";
                double totalDays, totalCost;

                address = (AddressManager)Session["Address"];
                vehicleAvailableID = (long)Session["VehicleAvailableID"];
                locationID = (long)Session["LocationID"];
                hireStart = (DateTime)Session["StartTime"];
                hireEnd = (DateTime)Session["EndTime"];
                customerID = (long)Session["CustomerID"];

                VehicleManager vehicle = VehicleManager.GetAvailableVehicles(locationID).Where(x => x.VehicleAvailableID == vehicleAvailableID).SingleOrDefault();

                totalDays = (hireEnd - hireStart).TotalDays;
                totalCost = totalDays * vehicle.BasePrice;
                totalCost = Math.Round(totalCost, 2); //Round to 2 dp

                bool ret = payPalCaller.ShortcutExpressCheckout(totalCost.ToString(), ref token, ref retMsg, vehicle.Manufacturer + " " + vehicle.Model, vehicle.Currency);
                if (ret)
                {
                    Session["token"] = token;
                    Response.Redirect(retMsg, false);
                }

                orderConfirmedLbl.Text = "Order Created";
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }
    }
}