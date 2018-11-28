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
    ///  This page contains the paypal payer id and the order id so if the customer has any problems they can contact
    ///  the company and/or paypal to sort them out.
    /// </summary>
    public partial class CheckoutComplete : System.Web.UI.Page
    {
        /// <summary>
        ///  Need to be logged in to access this page. The last added order is listed on this page to verify the transaction.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string PayPalPayerID = "";
                long orderID = 0;
                if (Session["LoggedInType"] == null)
                {
                    Response.Redirect(Variables.REDIRECT, false);
                }
                else if (Session["LoggedInType"].ToString() == "")
                {
                    Response.Redirect(Variables.REDIRECT, false);
                }

                OrderManager.GetLastAddedOrder(ref orderID, ref PayPalPayerID);

                transactionLbl.Text = "Order ID: " + orderID.ToString() + "<br />PayPal Payer ID: " + PayPalPayerID;
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Exits to home page.
        /// </summary>
        protected void ContinueBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/", false);
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }
    }
}