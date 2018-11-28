using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CarHireDBLibrary;

namespace CarHireWebApp
{
    public partial class ViewCustomers : System.Web.UI.Page
    {
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
                AddHeaderRow();
                LoadCustomers("");
                generalErrorLbl.Text = "";
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Adds table header for vehicle table.
        /// </summary>
        private void AddHeaderRow()
        {
            TableHeaderRow tr = new TableHeaderRow();

            TableCell cell = new TableCell();
            cell.Text = "Name";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Company";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Phone No & Email";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Address";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            CustomersTbl.Rows.Add(tr);
        }

        /// <summary>
        ///  Adds cells with text to company table.
        /// </summary>
        private void AddCell(TableRow row, string value)
        {
            TableCell cell = new TableCell();
            cell.Text = value;

            row.Cells.Add(cell);

            CustomersTbl.Rows.Add(row);
        }

        private void AddCell(TableRow row, string value, long ID)
        {
            TableCell cell = new TableCell();
            cell.Text = value;
            cell.ID = ID.ToString();

            row.Cells.Add(cell);

            CustomersTbl.Rows.Add(row);
        }

        private void LoadCustomers(string searchName)
        {
            long prevID = 0;
            int addressCount = 0;
            List<CustomerManager> customers;

            TableRow row = new TableRow();

            customers = CustomerManager.GetCustomers();

            if (searchName != "")
            {
                customers = customers.Where(x => x.Surname.Contains(searchName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            foreach (CustomerManager customer in customers.OrderBy(x => x.CompanyID))
            {
                //Put all addresses for each company in the same cell
                if (customer.CustomerID != prevID)
                {
                    row = new TableRow();

                    AddCell(row, customer.Surname + ", " + customer.Forename + ". Gender: " + customer.Title);
                    if (customer.CompanyID != 0)
                    {
                        AddCell(row, customer.CustomerID + " : " + customer.CompanyName);
                    }
                    else
                    {
                        AddCell(row, "No company assigned");
                    }
                    if (customer.MobileNo != "")
                    {
                        AddCell(row, "Phone no: " + customer.PhoneNo + ", Mobile:" + customer.MobileNo + ", " + customer.EmailAddress);
                    }
                    else
                    {
                        AddCell(row, "Phone no: " + customer.PhoneNo + ", Mobile: N/A, " + customer.EmailAddress);
                    }
                    //AddCell(row, "Address 1: " + customer.Address.GetAddressStr(), customer.CustomerID);
                    addressCount = 1;
                }
                else
                {
                    addressCount++;
                    TableCell cell = (TableCell)CustomersTbl.FindControl(customer.CustomerID.ToString());
                    //cell.Text = cell.Text + "<br /><br />Address " + addressCount + ": " + customer.Address.GetAddressStr();
                }
                prevID = customer.CompanyID;
            }
        }

        protected void SearchCustomersBtn_Click(object sender, EventArgs e)
        {
            try
            {
                LoadCustomers(customerSurnameTxt.Text);
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }
    }
}