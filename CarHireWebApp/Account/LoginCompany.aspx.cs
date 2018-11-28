using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using CarHireWebApp.Models;
using CarHireDBLibrary;
using System.Collections.Generic;
using System.Linq;

namespace CarHireWebApp.Account
{
    /// <summary>
    ///  Login to a company account that is on the system.
    /// </summary>
    public partial class LoginCompany : Page
    {
        /// <summary>
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///  Login to a company account that is on the system.
        /// </summary>
        protected void CompanyLogInBtn(object sender, EventArgs e)
        {
            try
            {
                CompanyManager company;
                company = CompanyManager.GetCompanies().Where(x => x.UserName.Equals(userNameTxt.Text, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();

                if (company != null)
                {
                    //Checks the hashed password in the textbox is the same as the hashed password in the database.
                    if (PasswordHash.ValidatePassword(passwordTxt.Text, company.Access.Password))
                    {
                        Session["UserID"] = company.CompanyID;
                        Session["UserName"] = userNameTxt.Text;
                        Session["LoggedInType"] = "Company";
                        ErrorMessage.Visible = false;

                        //Reset all previous login data to prevent completing an order as someone else for another user
                        Session["Address"] = null;
                        Session["VehicleAvailableID"] = null;
                        Session["LocationID"] = null;
                        Session["StartTime"] = null;
                        Session["EndTime"] = null;
                        Session["CustomerID"] = null;
                        Session["OrderConfirmed"] = null;
                        if (rememberMeChk.Checked == true)
                        {
                            Response.Cookies["UserNameCookie"].Value = userNameTxt.Text;
                            Response.Cookies["UserIDCookie"].Value = company.CompanyID.ToString();
                            Response.Cookies["LoggedInTypeCookie"].Value = "Company";

                            //Cookies never expire so user is always remembered until they logout.
                            Response.Cookies["UserNameCookie"].Expires = DateTime.MaxValue;
                            Response.Cookies["UserIDCookie"].Expires = DateTime.MaxValue;
                            Response.Cookies["LoggedInTypeCookie"].Expires = DateTime.MaxValue;
                        }

                        Response.Redirect("~/", false);
                    }
                    else
                    {
                        PasswordFail();
                    }
                }
                else
                {
                    PasswordFail();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        private void PasswordFail()
        {
            ErrorMessage.Visible = true;
            Session["UserName"] = null;
            Session["LoggedInType"] = null;
            FailureText.Text = "Password or username incorrect";
        }
    }
}