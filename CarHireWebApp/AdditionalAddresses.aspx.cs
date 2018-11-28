using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CarHireDBLibrary;

namespace CarHireWebApp
{
    public partial class AdditionalAddresses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string redirect = "~/Account/InformUser.aspx?InfoString=Unauthorised+access.+Please+login+to+an+account+with+correct+privileges.";
                if (Session["LoggedInType"] == null)
                {
                    Response.Redirect(redirect, false);
                }
                else if (Session["LoggedInType"].ToString() == "")
                {
                    Response.Redirect(redirect, false);
                }
                else if (Convert.ToBoolean(Request.QueryString["Customer"]) == true && Session["LoggedInType"].ToString() == "Company")
                {
                    Response.Redirect(redirect, false);
                }
                generalErrorLbl.Text = "";
                inputErrorLbl.Text = "";
                addressSavedLbl.Text = "";

                CheckAddressType();
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
            cell.Text = "Current Addresses";
            cell.CssClass = "h4";
            tr.Cells.Add(cell);

            AddressesTbl.Rows.Add(tr);
        }

        private void CheckAddressType()
        {
            long prevID = 0;

            customerDdl.Items.Clear();
            companyDdl.Items.Clear();

            if (Convert.ToBoolean(Request.QueryString["Company"]) == true)
            {
                companyDdl.Visible = true;
                typeLbl.Text = "Company";

                //Don't create duplicate items when page is reloaded
                if (companyDdl.Items.Count <= 0)
                {
                    companyDdl.Items.Add("");
                    List<CompanyManager> companies = CompanyManager.GetCompanies();

                    companies.OrderBy(x => x.CompanyID);

                    foreach (CompanyManager company in companies.OrderBy(x => x.CompanyID))
                    {
                        //Don't duplicate companies if they have multiple addresses
                        if (company.CompanyID != prevID)
                        {
                            string companyInfo = company.CompanyID.ToString() + ", " + company.CompanyName;
                            companyDdl.Items.Add(companyInfo);
                        }
                        prevID = company.CompanyID;
                    }
                }
            }
            else if (Convert.ToBoolean(Request.QueryString["Customer"]) == true)
            {
                customerDdl.Visible = true;
                typeLbl.Text = "Customer";

                //Don't create duplicate items when page is reloaded
                if (customerDdl.Items.Count <= 0)
                {
                    customerDdl.Items.Add("");
                    List<CustomerManager> customers = CustomerManager.GetCustomers();

                    foreach (CustomerManager customer in customers.OrderBy(x => x.CustomerID))
                    {
                        //Don't duplicate companies if they have multiple addresses
                        if (customer.CustomerID != prevID)
                        {
                            string customerInfo = customer.CustomerID.ToString() + ", " + customer.Surname + " : " + customer.Forename;
                            customerDdl.Items.Add(customerInfo);
                        }
                        prevID = customer.CustomerID;
                    }
                }
            }
            else
            {
                custRedirectBtn.Visible = true;
                compRedirectBtn.Visible = true;
                generalErrorLbl.Text = "Please select a type of address to enter";
            }
        }

        protected void CompanyRedirectBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string redirect = "~/Account/InformUser.aspx?InfoString=Unauthorised+access.+Please+login+to+an+account+with+correct+privileges.";
                if (Session["LoggedInType"].ToString() == "Customer")
                {
                    Response.Redirect(redirect, false);
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        protected void CustomerRedirectBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("AdditionalAddresses.aspx?Customer=true", false);
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Loads companies.
        /// </summary>
        protected void CompanyDdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TableRow row;
                TableCell cell;
                List<CompanyManager> companies = CompanyManager.GetCompanies();

                if (companyDdl.SelectedItem.Text != "")
                {
                    AddHeaderRow();

                    foreach (CompanyManager company in companies.Where(x => x.CompanyID == Convert.ToInt32(companyDdl.SelectedItem.Text.Split(',')[0])))
                    {
                        //string companyInfo = company.CompanyID.ToString() + ", " + company.CompanyName;
                        //companyDdl.Items.Add(companyInfo);

                        row = new TableRow();
                        cell = new TableCell();
                        //cell.Text = company.Address.GetAddressStr();
                        row.Cells.Add(cell);
                        AddressesTbl.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Loads customers.
        /// </summary>
        protected void CustomerDdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TableRow row;
                TableCell cell;
                List<CustomerManager> companies = CustomerManager.GetCustomers();

                if (customerDdl.SelectedItem.Text != "")
                {
                    AddHeaderRow();

                    foreach (CustomerManager customer in companies.Where(x => x.CustomerID == Convert.ToInt32(customerDdl.SelectedItem.Text.Split(',')[0])))
                    {
                        row = new TableRow();
                        cell = new TableCell();
                        //cell.Text = customer.Address.GetAddressStr();
                        row.Cells.Add(cell);
                        AddressesTbl.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        protected void AddressAddBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string addressLine1, addressLine2, addressLine3, addressLine4, city, zipOrPostcode,
                        countyStateProvince, country, otherAddressDetails;
                bool insertAddress = true; //boolean to check all fields are entered correctly

                #region addressCheck

                addressLine1 = addressLine1Txt.Text;
                addressLine2 = addressLine2Txt.Text;
                addressLine3 = addressLine3Txt.Text;
                addressLine4 = addressLine4Txt.Text;

                if (cityTxt.Text != "")
                {
                    city = cityTxt.Text;
                }
                else
                {
                    city = "";
                    insertAddress = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a city.";
                }

                if (Variables.CheckAlphaNumericCharacters(zipOrPostcodeTxt.Text) == true)
                {
                    zipOrPostcode = zipOrPostcodeTxt.Text;
                }
                else
                {
                    zipOrPostcode = "";
                    insertAddress = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Invalid zip or postcode.";
                }

                countyStateProvince = countyStateProvinceTxt.Text;

                if (countryTxt.Text != "")
                {
                    country = countryTxt.Text;
                }
                else
                {
                    country = "";
                    insertAddress = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a country.";
                }

                otherAddressDetails = otherAddressDetailsTxt.Text;

                #endregion

                if (insertAddress == true)
                {
                    if (Convert.ToBoolean(Request.QueryString["Company"]) == true)
                    {
                        long companyID;
                        companyID = Convert.ToInt32(companyDdl.SelectedItem.Text.Split(',')[0]);

                        //AddressManager.AddNewCompanyAddress(companyID, addressLine1, addressLine2,
                        //addressLine3, addressLine4, city, zipOrPostcode, countyStateProvince, country, otherAddressDetails);
                        addressSavedLbl.Text = "Save successful";
                    }
                    else if (Convert.ToBoolean(Request.QueryString["Customer"]) == true)
                    {
                        long customerID;
                        customerID = Convert.ToInt32(customerDdl.SelectedItem.Text.Split(',')[0]);

                        //AddressManager.addNewCompanyAddress(companyID, addressLine1, addressLine2,
                        //addressLine3, addressLine4, city, zipOrPostcode, countyStateProvince, country, otherAddressDetails);
                        //addressSavedLbl.Text = "Save successful";
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