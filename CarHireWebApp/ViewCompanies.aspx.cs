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
    ///  View all the companies in the database and search by company name.
    /// </summary>
    public partial class ViewCompanies : System.Web.UI.Page
    {
        /// <summary>
        ///  Must be logged in.
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
                AddHeaderRow();
                if (!IsPostBack)
                {
                    LoadCompanies();
                }
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
            cell.Text = "Description";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Phone No";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Email";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            CompaniesTbl.Rows.Add(tr);
        }

        /// <summary>
        ///  Adds cells with text to company table.
        /// </summary>
        private void AddCell(TableRow row, string value)
        {
            TableCell cell = new TableCell();
            cell.Text = value;

            row.Cells.Add(cell);

            CompaniesTbl.Rows.Add(row);
        }

        private void AddCell(TableRow row, string value, long ID)
        {
            TableCell cell = new TableCell();
            cell.Text = value;
            cell.ID = ID.ToString();

            row.Cells.Add(cell);

            CompaniesTbl.Rows.Add(row);
        }

        private void LoadCompanies()
        {
            long prevID = 0;
            int addressCount = 0;
            List<CompanyManager> companies, filteredCompanies = new List<CompanyManager>();
            string searchText = companyNameTxt.Text;

            TableRow row = new TableRow();

            companies = CompanyManager.GetCompanies();

            if (searchText != "")
            {
                foreach (CompanyManager company in companies)
                {
                    //For if there is a small spelling mistake
                    int diff = FuzzySearching.LD(company.CompanyName, searchText);
                    if (diff < 3 || company.CompanyName.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    {
                        filteredCompanies.Add(company);
                    }
                }
                companies = filteredCompanies;
            }

            foreach (CompanyManager company in companies.OrderBy(x => x.CompanyID))
            {
                //Put all addresses for each company in the same cell
                if (company.CompanyID != prevID)
                {
                    row = new TableRow();

                    AddCell(row, company.CompanyName);
                    AddCell(row, company.CompanyDescription);
                    AddCell(row, company.PhoneNo);
                    AddCell(row, company.EmailAddress);
                    addressCount = 1;
                }
                else
                {
                    addressCount++;
                    TableCell cell = (TableCell)CompaniesTbl.FindControl(company.CompanyID.ToString());
                }
                prevID = company.CompanyID;
            }
        }

        /// <summary>
        ///  Refreshes page but with updated search term records will change.
        /// </summary>
        protected void SearchCompanyNameBtn_Click(object sender, EventArgs e)
        {
            try
            {
                LoadCompanies();
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }
    }
}