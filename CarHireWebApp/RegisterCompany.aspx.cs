using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using CarHireWebApp.Models;
using CarHireDBLibrary;
using System.Collections.Generic;

namespace CarHireWebApp.Account
{
    /// <summary>
    ///  Registers a new company.
    /// </summary>
    public partial class RegisterCompany : Page
    {
        /// <summary>
        /// Must not be logged in with any account type.
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
                companySavedLbl.Text = "";
                ErrorMessage.Text = "";
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Adds a new company checking all fields have been entered correctly.
        /// </summary>
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            try
            {
                string userName, companyName, companyDescription, licensingDetails, phoneNo, emailAddress;
                bool insertCompany = true; //boolean to check all fields are entered correctly

                #region companyCheck

                if (userNameTxt.Text != "")
                {
                    userName = userNameTxt.Text;
                }
                else
                {
                    userName = "";
                    insertCompany = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a user name.";
                }

                if (companyNameTxt.Text != "")
                {
                    companyName = companyNameTxt.Text;
                }
                else
                {
                    companyName = "";
                    insertCompany = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a company name.";
                }

                phoneNo = Request["phoneNoTxt"];
                if (phoneNo == "")
                {
                    insertCompany = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a phone no.";
                }

                if (emailAddressTxt.Text != "")
                {
                    emailAddress = emailAddressTxt.Text;
                }
                else
                {
                    emailAddress = "";
                    insertCompany = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a email address.";
                }

                if (licensingDetailsTxt.Text != "")
                {
                    licensingDetails = licensingDetailsTxt.Text;
                }
                else
                {
                    licensingDetails = "";
                    insertCompany = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter licensing details.";
                }

                companyDescription = companyDescriptionTxt.Text;
                //addressLine1 = addressLine1Txt.Text;
                //addressLine2 = addressLine2Txt.Text;
                //addressLine3 = addressLine3Txt.Text;
                //addressLine4 = addressLine4Txt.Text;

                //if (Variables.CheckAlphaNumericCharacters(cityTxt.Text) && cityTxt.Text != "")
                //{
                //    city = cityTxt.Text;
                //}
                //else
                //{
                //    city = "";
                //    insertCompany = false;
                //    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a valid city.";
                //}

                //if (Variables.CheckAlphaNumericCharacters(zipOrPostcodeTxt.Text) == true && zipOrPostcodeTxt.Text != "")
                //{
                //    zipOrPostcode = zipOrPostcodeTxt.Text;
                //}
                //else
                //{
                //    zipOrPostcode = "";
                //    insertCompany = false;
                //    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Invalid zip or postcode.";
                //}

                //countyStateProvince = countyStateProvinceTxt.Text;

                //if (Request["countryDdl"] == "")
                //{
                //    insertCompany = false;
                //    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a country.";
                //}

                //otherAddressDetails = otherAddressDetailsTxt.Text;

                if (Variables.CheckPasswordValid(passwordTxt.Text) != true)
                {
                    insertCompany = false;
                    ErrorMessage.Text = "Passwords must contain at least 1 upper case letter, 1 lower case letter" +
                    ", 1 number or special character and be at least 6 characters in length";
                }

                #endregion

                if (insertCompany == true)
                {
                    string passwordEncrypt;
                    List<CompanyManager> companies = CompanyManager.GetCompanies();
                    passwordEncrypt = PasswordHash.CreateHash(passwordTxt.Text);

                    if (companies.Where(x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)).ToList().Count <= 0)
                    {
                        CompanyManager.AddNewCompany(userName, companyName, companyDescription, licensingDetails, phoneNo, emailAddress, passwordEncrypt);
                        companySavedLbl.Text = "Save successful";

                        CompanyManager company = CompanyManager.GetCompanies().Where(x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();

                        Session["LoggedInType"] = "Company";
                        Session["UserName"] = userName;
                        Session["UserID"] = company.CompanyID;

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