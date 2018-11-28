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
    ///  Registers a new customer.
    /// </summary>
    public partial class RegisterCustomer : System.Web.UI.Page
    {
        /// <summary>
        ///  Must not be logged in with any account type.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["LoggedInType"] != null)
                {
                    if (Session["LoggedInType"].ToString() == "Customer")
                    {
                        Response.Redirect(Variables.REDIRECT, false);
                    }
                    else if (Session["LoggedInType"].ToString() == "Company")
                    {
                        Response.Redirect(Variables.REDIRECT, false);
                    }
                }
                generalErrorLbl.Text = "";
                inputErrorLbl.Text = "";
                customerSavedLbl.Text = "";

                LoadDdls();
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Loads data for all the drop down lists.
        /// </summary>
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

            List<CompanyManager> companies;
            companies = CompanyManager.GetCompanies();
            companyDdl.Items.Add("");

            long prevID = 0;

            foreach (CompanyManager company in companies.OrderBy(x => x.CompanyID))
            {
                //Don't display duplicated companies
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

        /// <summary>
        ///  Registers a new company assuming all the fields are entered correctly.
        /// </summary>
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            try
            {
                string userName, surname, forename, title, licenseNo, companyName, phoneNo, mobileNo, emailAddress;
                bool insertCustomer = true; //boolean to check all fields are entered correctly
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

                if (userNameTxt.Text != "")
                {
                    userName = userNameTxt.Text;
                }
                else
                {
                    userName = "";
                    insertCustomer = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a user name.";
                }

                if (Variables.CheckAlphabetCharacters(surnameTxt.Text) && surnameTxt.Text != "")
                {
                    surname = surnameTxt.Text;
                }
                else
                {
                    surname = "";
                    insertCustomer = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a surname with only letters.";
                }

                if (Variables.CheckAlphabetCharacters(forenameTxt.Text) && forenameTxt.Text != "")
                {
                    forename = forenameTxt.Text;
                }
                else
                {
                    forename = "";
                    insertCustomer = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a forename with only letters.";
                }

                if (titleDdl.SelectedValue != "Title")
                {
                    title = titleDdl.SelectedValue;
                }
                else
                {
                    title = "";
                    insertCustomer = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a title.";
                }

                if (Variables.CheckAlphaNumericCharacters(licenseNoTxt.Text) && licenseNoTxt.Text != "")
                {
                    licenseNo = licenseNoTxt.Text;
                }
                else
                {
                    licenseNo = "";
                    insertCustomer = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a valid driving license number.";
                }

                if (issueDaysDdl.SelectedValue != "" && issueMonthsDdl.SelectedValue != "" && issueYearsDdl.SelectedValue != "")
                {
                    issueDate = Convert.ToDateTime(issueDaysDdl.SelectedValue + "/" + issueMonthsDdl.SelectedValue + "/" + issueYearsDdl.SelectedValue);
                }
                else
                {
                    issueDate = DateTime.Now;
                    insertCustomer = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter an issue date for your license.";
                }

                if (expirationDaysDdl.SelectedValue != "" && expirationMonthsDdl.SelectedValue != "" && expirationYearsDdl.SelectedValue != "")
                {
                    expirationDate = Convert.ToDateTime(expirationDaysDdl.SelectedValue + "/" + expirationMonthsDdl.SelectedValue + "/" + expirationYearsDdl.SelectedValue);
                }
                else
                {
                    expirationDate = DateTime.Now;
                    insertCustomer = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter an expiration date for your license.";
                }

                if (dateOfBirthDaysDdl.SelectedValue != "" && dateOfBirthMonthsDdl.SelectedValue != "" && dateOfBirthYearsDdl.SelectedValue != "")
                {
                    dateOfBirth = Convert.ToDateTime(dateOfBirthDaysDdl.SelectedValue + "/" + dateOfBirthMonthsDdl.SelectedValue + "/" + dateOfBirthYearsDdl.SelectedValue);
                }
                else
                {
                    dateOfBirth = DateTime.Now;
                    insertCustomer = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter an date of birth";
                }

                phoneNo = Request["phoneNoTxt"];
                if (phoneNo == "")
                {
                    insertCustomer = false;
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
                    insertCustomer = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a email address.";
                }

                if (Variables.CheckPasswordValid(passwordTxt.Text) != true)
                {
                    insertCustomer = false;
                    ErrorMessage.Text = "Passwords must contain at least 1 upper case letter, 1 lower case letter" +
                    ", 1 number or special character and be at least 6 characters in length";
                }

                #endregion

                if (insertCustomer == true)
                {
                    string passwordEncrypt;
                    List<CustomerManager> customers = CustomerManager.GetCustomers();
                    passwordEncrypt = PasswordHash.CreateHash(passwordTxt.Text);

                    if (customers.Where(x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)).ToList().Count <= 0)
                    {
                        CustomerManager.AddNewCustomer(companyID, userName, surname, forename, title, licenseNo, issueDate, expirationDate,
                            dateOfBirth, phoneNo, mobileNo, emailAddress, passwordEncrypt);
                        customerSavedLbl.Text = "Save successful";

                        CustomerManager customer = CustomerManager.GetCustomers().Where(x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();

                        Session["LoggedInType"] = "Customer";
                        Session["UserName"] = userName;
                        Session["UserID"] = customer.CustomerID;

                        //Return to the home page
                        Response.Redirect("~/", false);
                    }
                    else
                    {
                        inputErrorLbl.Text = "An account with that username already exists. Please enter a different one.";
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