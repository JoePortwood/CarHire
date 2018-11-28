using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CarHireDBLibrary;

namespace CarHireWebApp.Account
{
    /// <summary>
    ///  Displays all the orders for the logged in customer.
    /// </summary>
    public partial class ViewOrders : System.Web.UI.Page
    {
        /// <summary>
        ///  Must be logged in as a customer.
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
                else if (Session["LoggedInType"].ToString() == "Company")
                {
                    Response.Redirect(Variables.REDIRECT, false);
                }
                generalErrorLbl.Text = "";
                AddHeaderRow();
                LoadOrders();
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Adds table header for orders table.
        /// </summary>
        private void AddHeaderRow()
        {
            TableHeaderRow tr = new TableHeaderRow();

            TableCell cell = new TableCell();
            cell.Text = "Image";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Vehicle";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Contact";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Times and Price";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Status";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            ordersTbl.Rows.Add(tr);
        }

        /// <summary>
        ///  Loads all the orders for logged in customer
        /// </summary>
        private void LoadOrders()
        {
            List<VehicleManager> vehicles = new List<VehicleManager>();
            List<LocationManager> locations = new List<LocationManager>();
            List<AddressManager> addresses = new List<AddressManager>();
            List<long> orderIDs = new List<long>();
            List<DateTime> hireStarts = new List<DateTime>();
            List<DateTime> hireEnds = new List<DateTime>();
            List<double> totalCosts = new List<double>();
            List<string> orderStatuses = new List<string>();
            List<string> phoneNos = new List<string>(); 
            List<string> emailAddresses = new List<string>();
            List<string> customerEmailAddresses = new List<string>();
            List<string> currencies = new List<string>();
            OrderManager.SelectOrderCustomer(ref vehicles, ref locations, ref addresses,
            ref orderIDs, ref hireStarts, ref hireEnds, ref totalCosts, ref orderStatuses,
            ref phoneNos, ref emailAddresses, ref customerEmailAddresses, ref currencies, Variables.GetUser(Session["UserID"].ToString()));

            SIPPCode sizeOfVehicleSIPPCode, noOfDoorsSIPPCode, transmissionAndDriveSIPPCode, fuelAndACSIPPCode;
            TableRow row;
            TableCell cell;
            for (int i = 0; i < orderIDs.Count; i++)
            {
                row = new TableRow();
                cell = new TableCell();

                Image vehicleImg = new Image();

                vehicleImg.AlternateText = vehicles[i].Manufacturer + " " + vehicles[i].Model;

                //make standard size of car images 270x150
                vehicleImg.Width = Variables.STANDARDWIDTH;
                vehicleImg.Height = Variables.STANDARDHEIGHT;
                if (vehicles[i].ImageLoc != "")
                {
                    vehicleImg.ImageUrl = vehicles[i].ImageLoc;
                }
                cell.Controls.Add(vehicleImg);

                row.Cells.Add(cell);

                cell = new TableCell();

                //Gets SIPP Code descriptions for all letters of SIPP code for this vehicle.
                sizeOfVehicleSIPPCode = SIPPCode.GetSIPPCodeDesc(Variables.SIZEOFVEHICLE, vehicles[i].SIPPCode[0].ToString());
                noOfDoorsSIPPCode = SIPPCode.GetSIPPCodeDesc(Variables.NOOFDOORS, vehicles[i].SIPPCode[1].ToString());
                transmissionAndDriveSIPPCode = SIPPCode.GetSIPPCodeDesc(Variables.TRANSMISSIONANDDRIVE, vehicles[i].SIPPCode[2].ToString());
                fuelAndACSIPPCode = SIPPCode.GetSIPPCodeDesc(Variables.FUELANDAC, vehicles[i].SIPPCode[3].ToString());

                cell.Text = vehicles[i].Manufacturer + " " + vehicles[i].Model + "<br /><br />" +
                    sizeOfVehicleSIPPCode.Description + "<br />" + noOfDoorsSIPPCode.Description + "<br />" +
                    transmissionAndDriveSIPPCode.Description + "<br />" + fuelAndACSIPPCode.Description + "<br />"
                    + "MPG: " + vehicles[i].MPG;

                row.Cells.Add(cell);

                cell = new TableCell();

                if (addresses[i].AddressID == 0)
                {
                    cell.Text = "Address: " + locations[i].GetAddressStr() + "<br />" +
                        "Phone No: " + phoneNos[i] + "<br />" + 
                        "Email Address: " + emailAddresses[i];
                }
                else
                {
                    cell.Text = addresses[i].GetAddressStr() + "<br />" +
                        "Phone No: " + phoneNos[i] + "<br />" +
                        "Email Address: " + emailAddresses[i];
                }

                row.Cells.Add(cell);

                cell = new TableCell();

                cell.Text = "Starts: " + hireStarts[i] + "<br />" + "Ends: " + hireEnds[i] + "<br /><br />" +
                    "Total Cost: " + currencies[i] + " " + totalCosts[i];

                row.Cells.Add(cell);

                cell = new TableCell();

                Label label = new Label();
                label.Text = "<b>Order ID: " + orderIDs[i] + "</br>Status: " + orderStatuses[i] + "</b><br />";
                cell.Controls.Add(label);

                if (orderStatuses[i] == "Pending")
                {
                    //Button to cancel the order
                    Button cancelBtn = new Button();
                    cancelBtn.ID = orderIDs[i] + "_" + customerEmailAddresses[i] + "_CancelBtn";
                    cancelBtn.Text = "Cancel Order";
                    cancelBtn.CssClass = "btn btn-primary";
                    cancelBtn.Click += new EventHandler(CancelBtn_Click);
                    //Warns user before they cancel their order giving the option to keep the order.
                    cancelBtn.OnClientClick = "return confirm('Are you sure want to cancel your order? Cancellation fees may apply');";
                    cell.Controls.Add(cancelBtn);
                }

                row.Cells.Add(cell);
                ordersTbl.Rows.Add(row);
            }
        }

        /// <summary>
        ///  Cancels the selected order
        /// </summary>
        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            try
            {
                long orderID;
                string emailAddress, subject, body;

                //Find the ID of the button clicked
                Button cancelBtn = (Button)sender;

                orderID = Convert.ToInt32(cancelBtn.ID.Split('_')[0]);
                emailAddress = cancelBtn.ID.Split('_')[1];
                subject = "Order " + orderID + " Cancelled";
                body = "This order has been cancelled. If you wish to hire this car please book it again";

                OrderManager.UpdateOrderStatus(orderID, "Cancelled");
                Response.Redirect(Request.RawUrl, false);

                Variables.Email(emailAddress, subject, body);
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }
    }
}