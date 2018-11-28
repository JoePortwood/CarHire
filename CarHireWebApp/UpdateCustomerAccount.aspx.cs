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
    ///  Updates the current logged in customer.
    /// </summary>
    public partial class UpdateCustomerAccount : System.Web.UI.Page
    {
        List<CompanyManager> companies;
        public CustomerManager customer;
        //PHONE PLUGIN DOES NOT WORK INSIDE A FOLDER SO NEEDS TO BE TOP LEVEL
        /// <summary>
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                generalErrorLbl.Text = "";
                inputErrorLbl.Text = "";
                customerSavedLbl.Text = "";

                LoadDdls();
                if (!IsPostBack)
                {
                    LoadCustomerInfo();
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        private void LoadDdls()
        {
            if (titleDdl.Items.Count <= 0)
            {
                titleDdl.Items.Add("Title");
                titleDdl.Items.Add("Mr");
                titleDdl.Items.Add("Miss/Ms");
                titleDdl.Items.Add("Mrs");
                titleDdl.Items.Add("Rev");
                titleDdl.Items.Add("Dr");
                titleDdl.Items.Add("Prof");
            }

            LoadDays();
            LoadMonths();
            LoadIssueDateYears();
            LoadExpirationDateYears();
            LoadDateOfBirthYears();

            companies = CompanyManager.GetCompanies();
            companyDdl.Items.Add("");

            long prevID = 0;

            foreach (CompanyManager company in companies.OrderBy(x => x.CompanyID))
            {
                //Don't duplicated companies
                if (company.CompanyID != prevID)
                {
                    companyDdl.Items.Add(company.CompanyID + ", " + company.CompanyName);
                }
                prevID = company.CompanyID;
            }
        }

        private void LoadDays()
        {
            //Add more if conditions for better selection
            if (issueDaysDdl.Items.Count <= 0)
            {
                issueDaysDdl.Items.Add("");
                expirationDaysDdl.Items.Add("");
                dateOfBirthDaysDdl.Items.Add("");
                for (int i = 1; i <= 31; i++)
                {
                    ListItem selectItem;
                    selectItem = new ListItem();
                    selectItem.Value = i.ToString();
                    selectItem.Text = i.ToString();
                    issueDaysDdl.Items.Add(selectItem);
                    expirationDaysDdl.Items.Add(selectItem);
                    dateOfBirthDaysDdl.Items.Add(selectItem);
                }
            }
        }

        private void LoadMonths()
        {
            //Add more if conditions for better selection
            if (issueMonthsDdl.Items.Count <= 0)
            {
                issueMonthsDdl.Items.Add("");
                expirationMonthsDdl.Items.Add("");
                dateOfBirthMonthsDdl.Items.Add("");

                DateTime dt = new DateTime(2000, 1, 1);
                for (int i = 1; i++ <= 12; dt = dt.AddMonths(1))
                {
                    issueMonthsDdl.Items.Add(dt.ToString("MMM"));
                    expirationMonthsDdl.Items.Add(dt.ToString("MMM"));
                    dateOfBirthMonthsDdl.Items.Add(dt.ToString("MMM"));
                }
            }
        }

        private void LoadIssueDateYears()
        {
            if (issueYearsDdl.Items.Count <= 0)
            {
                issueYearsDdl.Items.Add("");
                for (int i = DateTime.Now.Year - 100; i <= DateTime.Now.Year; i++)
                {
                    ListItem selectItem;
                    selectItem = new ListItem();
                    selectItem.Value = i.ToString();
                    selectItem.Text = i.ToString();
                    issueYearsDdl.Items.Add(selectItem);
                }
            }
        }

        private void LoadExpirationDateYears()
        {
            if (expirationYearsDdl.Items.Count <= 0)
            {
                expirationYearsDdl.Items.Add("");
                for (int i = DateTime.Now.Year; i <= DateTime.Now.Year + 100; i++)
                {
                    ListItem selectItem;
                    selectItem = new ListItem();
                    selectItem.Value = i.ToString();
                    selectItem.Text = i.ToString();
                    expirationYearsDdl.Items.Add(selectItem);
                }
            }
        }

        private void LoadDateOfBirthYears()
        {
            if (dateOfBirthYearsDdl.Items.Count <= 0)
            {
                dateOfBirthYearsDdl.Items.Add("");
                //Lowest legal driving age in any country is 14
                for (int i = DateTime.Now.Year - 100; i <= DateTime.Now.Year - 14; i++)
                {
                    ListItem selectItem;
                    selectItem = new ListItem();
                    selectItem.Value = i.ToString();
                    selectItem.Text = i.ToString();
                    dateOfBirthYearsDdl.Items.Add(selectItem);
                }
            }
        }

        private void LoadCustomerInfo()
        {
            string days, months, years;
            customer = CustomerManager.GetCustomers().Where(x => x.CustomerID == Variables.GetUser(Session["UserID"].ToString())).SingleOrDefault();

            surnameTxt.Text = customer.Surname;
            forenameTxt.Text = customer.Forename;
            titleDdl.SelectedValue = customer.Title;
            if (customer.CompanyID != 0)
            {
                companyDdl.SelectedValue = customer.CompanyID + ", " + customer.CompanyName;
            }
            else
            {
                companyDdl.SelectedValue = "";
            }
            emailAddressTxt.Text = customer.EmailAddress;
            licenseNoTxt.Text = customer.LicenseNo;

            //Issue Date
            days = customer.IssueDate.Day.ToString();
            months = customer.IssueDate.ToString("MMM");
            years = customer.IssueDate.Year.ToString();
            issueDaysDdl.SelectedValue = days;
            issueMonthsDdl.SelectedValue = months;
            issueYearsDdl.SelectedValue = years;

            //Expiration Date
            days = customer.ExpirationDate.Day.ToString();
            months = customer.ExpirationDate.ToString("MMM");
            years = customer.ExpirationDate.Year.ToString();
            expirationDaysDdl.SelectedValue = days;
            expirationMonthsDdl.SelectedValue = months;
            expirationYearsDdl.SelectedValue = years;

            //Date of Birth
            days = customer.DateOfBirth.Day.ToString();
            months = customer.DateOfBirth.ToString("MMM");
            years = customer.DateOfBirth.Year.ToString();
            dateOfBirthDaysDdl.SelectedValue = days;
            dateOfBirthMonthsDdl.SelectedValue = months;
            dateOfBirthYearsDdl.SelectedValue = years;
        }

        /// <summary>
        ///  Update the customer in the database assuming all fields entered are correct.
        /// </summary>
        protected void UpdateUser_Click(object sender, EventArgs e)
        {
            try
            {
                string surname, forename, title, licenseNo, companyName, phoneNo, mobileNo, emailAddress;
                bool updateCustomer = true; //boolean to check all fields are entered correctly
                long companyID;
                DateTime issueDate, expirationDate, dateOfBirth;

                #region customerCheck

                if (companyDdl.SelectedValue != "")
                {
                    companyID = Convert.ToInt32(companyDdl.SelectedValue.Split(',')[0]);
                    companyName = companyDdl.SelectedValue.Split(',')[1];
                }
                else
                {
                    companyID = 0;
                    companyName = "";
                }

                if (Variables.CheckAlphabetCharacters(surnameTxt.Text) && surnameTxt.Text != "")
                {
                    surname = surnameTxt.Text;
                }
                else
                {
                    surname = "";
                    updateCustomer = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a surname with only letters.";
                }

                if (Variables.CheckAlphabetCharacters(forenameTxt.Text) && forenameTxt.Text != "")
                {
                    forename = forenameTxt.Text;
                }
                else
                {
                    forename = "";
                    updateCustomer = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a forename with only letters.";
                }

                if (titleDdl.SelectedValue != "Title")
                {
                    title = titleDdl.SelectedValue;
                }
                else
                {
                    title = "";
                    updateCustomer = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a title.";
                }

                if (Variables.CheckAlphaNumericCharacters(licenseNoTxt.Text) && licenseNoTxt.Text != "")
                {
                    licenseNo = licenseNoTxt.Text;
                }
                else
                {
                    licenseNo = "";
                    updateCustomer = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a valid driving license number.";
                }

                if (issueDaysDdl.SelectedValue != "" && issueMonthsDdl.SelectedValue != "" && issueYearsDdl.SelectedValue != "")
                {
                    issueDate = Convert.ToDateTime(issueDaysDdl.SelectedValue + "/" + issueMonthsDdl.SelectedValue + "/" + issueYearsDdl.SelectedValue);
                }
                else
                {
                    issueDate = DateTime.Now;
                    updateCustomer = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter an issue date for your license.";
                }

                if (expirationDaysDdl.SelectedValue != "" && expirationMonthsDdl.SelectedValue != "" && expirationYearsDdl.SelectedValue != "")
                {
                    expirationDate = Convert.ToDateTime(expirationDaysDdl.SelectedValue + "/" + expirationMonthsDdl.SelectedValue + "/" + expirationYearsDdl.SelectedValue);
                }
                else
                {
                    expirationDate = DateTime.Now;
                    updateCustomer = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter an expiration date for your license.";
                }

                if (dateOfBirthDaysDdl.SelectedValue != "" && dateOfBirthMonthsDdl.SelectedValue != "" && dateOfBirthYearsDdl.SelectedValue != "")
                {
                    dateOfBirth = Convert.ToDateTime(dateOfBirthDaysDdl.SelectedValue + "/" + dateOfBirthMonthsDdl.SelectedValue + "/" + dateOfBirthYearsDdl.SelectedValue);
                }
                else
                {
                    dateOfBirth = DateTime.Now;
                    updateCustomer = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter an date of birth";
                }

                phoneNo = Request["phoneNoTxt"];
                if (phoneNo == "")
                {
                    updateCustomer = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a phone no.";
                }

                mobileNo = Request["mobileNoTxt"];

                if (emailAddressTxt.Text != "")
                {
                    emailAddress = emailAddressTxt.Text;
                }
                else
                {
                    emailAddress = "";
                    updateCustomer = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a email address.";
                }

                #endregion

                if (updateCustomer == true)
                {
                    CustomerManager.UpdateCustomer(Variables.GetUser(Session["UserID"].ToString()), companyID, surname, forename, title, licenseNo, issueDate, expirationDate,
                        dateOfBirth, phoneNo, mobileNo, emailAddress);
                    customerSavedLbl.Text = "Save successful";
                }

                //Reload because 3rd party phone controller clears itself on postback
                LoadCustomerInfo();
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }
    }
}