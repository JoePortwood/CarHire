using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using CarHireDBLibrary;

namespace CarHireWebApp.Account
{
    public partial class ResetPasswordConfirmation : Page
    {
        /// <summary>
        ///  Puts a message onto the page using the query string.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            infoLbl.Text = Request.QueryString["InfoString"];

            bool logOut = Convert.ToBoolean(Request.QueryString["LogOut"]);
            if (logOut == true)
            {
                //Remove the cookies for login
                if (Request.Cookies["UserName"] != null)
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                }
                if (Request.Cookies["LoggedInType"] != null)
                {
                    Response.Cookies["LoggedInType"].Expires = DateTime.Now.AddDays(-1);
                }
                Session["UserName"] = null;
                Session["LoggedInType"] = null;
            }

            //Redirects to the home page after 5 seconds.
            HtmlMeta meta = new HtmlMeta();
            meta.HttpEquiv = "Refresh";
            meta.Content = "5;url=" + Variables.URL;
            this.Page.Controls.Add(meta);
            redirectLbl.Text = "You will now be redirected in 5 seconds. Click <a href=" + Variables.URL + ">here</a> if this fails.";
        }
    }
}