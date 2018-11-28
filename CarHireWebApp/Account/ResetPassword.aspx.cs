using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using CarHireWebApp.Models;
using CarHireDBLibrary;

namespace CarHireWebApp.Account
{
    /// <summary>
    ///  Resets the password of an account that a password reset request has been sent to.
    /// </summary>
    public partial class ResetPassword : Page
    {
        string userName;
        long accountID, accountType;
        DateTime? lastRequested;
        
        protected string StatusMessage
        {
            get;
            private set;
        }

        //MAKE TEXTBOXES AND LABELS INVISIBLE IF THERE HAS NOT BEEN A REQUEST ENTERED RECENTLY
        protected void Page_Load()
        {
            try
            {
                accountType = Convert.ToInt32(Request.QueryString["AccountType"]);
                accountID = Convert.ToInt32(Request.QueryString["AccountID"]);

                lastRequested = PasswordResetRequest.GetLastRequestedTime(accountType, accountID);
                if (lastRequested != null)
                {
                    userName = Request.QueryString["UserName"];

                    userNameTxt.Text = userName;

                    var script = "document.getElementById('pageForm').hidden = 'false';";
                    ClientScript.RegisterStartupScript(typeof(string), "textvaluesetter", script, true);
                }
                else
                {
                    ErrorMessage.Text = "This request is out of date, please make another password reset request and try again";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Resets password as long as there is a request for this account made within the last 15 mins.
        /// </summary>
        protected void Reset_Click(object sender, EventArgs e)
        {
            try
            {
                lastRequested = PasswordResetRequest.GetLastRequestedTime(accountType, accountID);
                bool updatePassword = true;
                bool resetByEmail;

                resetByEmail = Convert.ToBoolean(Request.QueryString["ResetByEmail"]);

                if (resetByEmail == true)
                {
                    if (Variables.CheckPasswordValid(passwordTxt.Text) != true)
                    {
                        updatePassword = false;
                        ErrorMessage.Text = "Passwords must contain at least 1 upper case letter, 1 lower case letter" +
                        ", 1 number or special character and be at least 6 characters in length";
                    }

                    if (lastRequested == null)
                    {
                        updatePassword = false;
                        ErrorMessage.Text = "This request is out of date, please make another password reset request and try again";
                    }
                }

                if (updatePassword == true)
                {
                    string hashedPassword = PasswordHash.CreateHash(passwordTxt.Text);
                    UserAccess.UpdateAccess(accountType, accountID, hashedPassword);
                    Response.Redirect("~/Account/InformUser.aspx?InfoString=Password+has+been+changed.", false);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }
    }
}