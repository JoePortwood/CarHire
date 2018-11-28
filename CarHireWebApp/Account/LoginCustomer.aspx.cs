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
    ///  Logins to a customer account that is already on the system.
    /// </summary>
    public partial class LoginCustomer : System.Web.UI.Page
    {
        /// <summary>
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///  Logins to a customer account that is already on the system.
        /// </summary>
        protected void CustomerLogInBtn(object sender, EventArgs e)
        {
            try
            {
                CustomerManager customer;

                customer = CustomerManager.GetCustomers().Where(x => x.UserName.Equals(userNameTxt.Text, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();

                if (customer != null)
                {
                    //Checks the hashed password in the textbox is the same as the hashed password in the database.
                    if (PasswordHash.ValidatePassword(passwordTxt.Text, customer.Access.Password))
                    {
                        Session["UserID"] = customer.CustomerID;
                        Session["UserName"] = userNameTxt.Text;
                        Session["LoggedInType"] = "Customer";
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
                            Response.Cookies["UserIDCookie"].Value = customer.CustomerID.ToString();
                            Response.Cookies["LoggedInTypeCookie"].Value = "Customer";

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