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
    ///  View all customers that have orders with logged in company.
    /// </summary>
    public partial class ViewCustomerOrders : System.Web.UI.Page
    {
        /// <summary>
        ///  Must be logged in as company.
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
            cell.Text = "Location";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Vehicle";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Customer";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Times";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Status";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            customerOrdersTbl.Rows.Add(tr);
        }

        /// <summary>
        ///  Load the orders into the table.
        /// </summary>
        private void LoadOrders()
        {
            List<VehicleManager> vehicles = new List<VehicleManager>();
            List<LocationManager> locations = new List<LocationManager>();
            List<AddressManager> addresses = new List<AddressManager>();
            List<CustomerManager> customers = new List<CustomerManager>();
            List<long> orderIDs = new List<long>();
            List<DateTime> hireStarts = new List<DateTime>();
            List<DateTime> hireEnds = new List<DateTime>();
            List<double> totalCosts = new List<double>();
            List<string> orderStatuses = new List<string>();
            List<string> phoneNos = new List<string>();
            List<string> emailAddresses = new List<string>();
            List<string> currencies = new List<string>();
            OrderManager.SelectOrdersByCompany(ref vehicles, ref locations, ref addresses, ref customers,
            ref orderIDs, ref hireStarts, ref hireEnds, ref totalCosts, ref orderStatuses,
            ref phoneNos, ref emailAddresses, ref currencies, Variables.GetUser(Session["UserID"].ToString()));

            SIPPCode sizeOfVehicleSIPPCode, noOfDoorsSIPPCode, transmissionAndDriveSIPPCode, fuelAndACSIPPCode;
            TableRow row;
            TableCell cell;
            bool changeableStatus = true;
            for (int i = 0; i < orderIDs.Count; i++)
            {
                row = new TableRow();

                cell = new TableCell();
                cell.Text = "ID: " + locations[i].LocationID + "<br />" +
                    "Name: " + locations[i].LocationName + "<br />";

                row.Cells.Add(cell);

                cell = new TableCell();

                //Gets SIPP Code descriptions for all letters of SIPP code for this vehicle.
                sizeOfVehicleSIPPCode = SIPPCode.GetSIPPCodeDesc(Variables.SIZEOFVEHICLE, vehicles[i].SIPPCode[0].ToString());
                noOfDoorsSIPPCode = SIPPCode.GetSIPPCodeDesc(Variables.NOOFDOORS, vehicles[i].SIPPCode[1].ToString());
                transmissionAndDriveSIPPCode = SIPPCode.GetSIPPCodeDesc(Variables.TRANSMISSIONANDDRIVE, vehicles[i].SIPPCode[2].ToString());
                fuelAndACSIPPCode = SIPPCode.GetSIPPCodeDesc(Variables.FUELANDAC, vehicles[i].SIPPCode[3].ToString());

                //Fill vehicle information cell with text.
                cell.Text = vehicles[i].Manufacturer + " " + vehicles[i].Model + "<br /><br />" +
                    sizeOfVehicleSIPPCode.Description + "<br />" + noOfDoorsSIPPCode.Description + "<br />" +
                    transmissionAndDriveSIPPCode.Description + "<br />" + fuelAndACSIPPCode.Description + "<br />"
                    + "MPG: " + vehicles[i].MPG;

                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = customers[i].CustomerID + " : " + customers[i].Surname + " " + customers[i].Forename + "<br />"
                    + "License: " + customers[i].LicenseNo + "<br />" + "Expiry Date: " + customers[i].ExpirationDate;

                //If the address id column in the address table is null then use the location address.
                if (addresses[i].AddressID == 0)
                {
                    cell.Text = cell.Text + "<br />" + locations[i].GetAddressStr();
                }
                else
                {
                    cell.Text = cell.Text + "<br />" + addresses[i].GetAddressStr() + "<br />";
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

                DropDownList statusDdl = new DropDownList();
                //Only allow complete to be selected if the status is accepted
                if (orderStatuses[i] != "Accepted")
                {
                    statusDdl.Items.Add("Pending");
                    statusDdl.Items.Add("Cancelled");
                    statusDdl.Items.Add("Accepted");
                    statusDdl.Items.Add("Declined");
                    statusDdl.Items.Add("Complete");
                    statusDdl.CssClass = "btn btn-default dropdown-toggle";
                }
                else
                {
                    statusDdl.Items.Add("Accepted");
                    statusDdl.Items.Add("Complete");
                }
                statusDdl.CssClass = "btn btn-default dropdown-toggle";
                statusDdl.DataBind();
                statusDdl.ID = orderIDs[i] + "_StatusDdl";
                statusDdl.Items.FindByValue(orderStatuses[i]).Selected = true;

                //Button to change the status
                Button statusBtn = new Button();

                //Reset value
                changeableStatus = true;

                if (statusDdl.SelectedValue == "Cancelled" || statusDdl.SelectedValue == "Complete" ||statusDdl.SelectedValue == "Declined")
                {
                    changeableStatus = false;
                }

                if (changeableStatus == true)
                {
                    statusDdl.Enabled = true;

                    statusBtn.ID = orderIDs[i] + "_" + customers[i].EmailAddress + "_" + emailAddresses[i] + "_StatusBtn";
                    statusBtn.Text = "Change Status";
                    statusBtn.CssClass = "btn btn-primary";
                    statusBtn.Click += new EventHandler(UpdateBtn_Click);
                    statusBtn.OnClientClick = "return confirm('Confirm status change?');";
                }
                else
                {
                    statusDdl.Enabled = false;
                }

                cell.Controls.Add(statusDdl);
                label = new Label();
                label.Text = "<br /><br />";
                cell.Controls.Add(label);

                //Add button after drop down list has been added for HCI
                if (changeableStatus == true)
                {
                    cell.Controls.Add(statusBtn);
                }

                row.Cells.Add(cell);
                customerOrdersTbl.Rows.Add(row);
            }
        }

        /// <summary>
        ///  Updates the status of the order in question to whichever option is selected in the drop down list.
        /// </summary>
        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                long orderID;
                string customerEmailAddress, locationEmailAddress, subject, body;
                CompanyManager company = CompanyManager.GetCompanies().Where(x => x.CompanyID == Convert.ToInt32(Session["UserID"])).SingleOrDefault();

                //Find the ID of the button clicked
                Button statusBtn = (Button)sender;
                //Gets the drop down list in relation to the button clicked
                DropDownList statusDdl = (DropDownList)customerOrdersTbl.FindControl(statusBtn.ID.Split('_')[0] + "_StatusDdl");

                orderID = Convert.ToInt32(statusBtn.ID.Split('_')[0]);
                //Email both the customer and the location of the status change - could email the location company and the customer's company too?
                customerEmailAddress = statusBtn.ID.Split('_')[1];
                locationEmailAddress = statusBtn.ID.Split('_')[2];
                subject = "Order " + orderID + " " + statusDdl.SelectedValue;
                body = "This order has been " + statusDdl.SelectedValue + ".";

                //Do not update if status is set to pending
                if (statusDdl.SelectedValue != "Pending")
                {
                    OrderManager.UpdateOrderStatus(orderID, statusDdl.SelectedValue);
                    Response.Redirect(Request.RawUrl, false);

                    Variables.Email(customerEmailAddress, subject, body);
                    Variables.Email(locationEmailAddress, subject, body);
                    Variables.Email(company.EmailAddress, subject, body);
                }
                else
                {
                    generalErrorLbl.Text = "Please selet a valid status";
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }
    }
}