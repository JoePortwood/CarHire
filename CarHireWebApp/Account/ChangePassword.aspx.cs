using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using CarHireWebApp.Models;
using CarHireDBLibrary;
using System.Net.Mail;

namespace CarHireWebApp.Account
{
    public partial class Manage : System.Web.UI.Page
    {
        protected string SuccessMessage
        {
            get;
            private set;
        }

        /// <summary>
        ///  Hashes the entered password if the password and confirmed password are the same then saves this hashed password to the database.
        /// </summary>
        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                //Checks whether logged in account is company or customer then changes the password of the account in question.
                if (Session["LoggedInType"].ToString() == "Company")
                {
                    CompanyManager company;

                    company = CompanyManager.GetCompanies().Where(x => x.UserName.Equals(Session["UserName"].ToString(), StringComparison.OrdinalIgnoreCase)).Single();
                    if (Variables.CheckPasswordValid(newPasswordTxt.Text) == true)
                    {
                        if (PasswordHash.ValidatePassword(currentPasswordTxt.Text, company.Access.Password))
                        {
                            string hashedPassword = PasswordHash.CreateHash(newPasswordTxt.Text);
                            UserAccess.UpdateAccess(2, company.CompanyID, hashedPassword);
                            SendEmail(company.EmailAddress, company.UserName);
                            Response.Redirect("~/Account/InformUser.aspx?InfoString=Password+change+successful.");
                        }
                    }
                }
                else if (Session["LoggedInType"].ToString() == "Customer")
                {
                    CustomerManager customer;

                    customer = CustomerManager.GetCustomers().Where(x => x.UserName.Equals(Session["UserName"].ToString(), StringComparison.OrdinalIgnoreCase)).Single();
                    if (Variables.CheckPasswordValid(newPasswordTxt.Text) == true)
                    {
                        if (PasswordHash.ValidatePassword(currentPasswordTxt.Text, customer.Access.Password))
                        {
                            string hashedPassword = PasswordHash.CreateHash(newPasswordTxt.Text);
                            UserAccess.UpdateAccess(4, customer.CustomerID, hashedPassword);
                            SendEmail(customer.EmailAddress, customer.UserName);
                            Response.Redirect("~/Account/InformUser.aspx?InfoString=Password+change+successful.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        private void SendEmail(string emailAddress, string userName)
        {
            Variables.Email(emailAddress, "Password Reset", "<html><body><p>Dear " + userName + ",<br />" +
                            "Your password has been reset. Please reply to us if this was not you who " +
                            "changed the password. <br /> Regards, Portwood Car Hire </p></body></html>");
        }
    }
}