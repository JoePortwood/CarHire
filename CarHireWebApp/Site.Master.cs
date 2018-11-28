using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using CarHireDBLibrary;

namespace CarHireWebApp
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            //Check login needs to be here so that the login is checked before other pages are loaded.
            CheckLogin(true);

            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut();
        }

        protected void LogOut(object sender, EventArgs e)
        {
            Session["UserName"] = null;
            Session["LoggedInType"] = null;
            Session["UserID"] = null;
            Response.Cookies["UserNameCookie"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["LoggedInTypeCookie"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["UserIDCookie"].Expires = DateTime.Now.AddDays(-1);  
            CheckLogin(false);
            Response.Redirect(Variables.URL, false);
        }

        //Cookie valid checks whether the user has clicked logout and the cookie has expired
        private void CheckLogin(bool cookieValid)
        {
            if (Request.Cookies["UserNameCookie"] != null && cookieValid == true)
            {
                Session["UserName"] = Request.Cookies["UserNameCookie"].Value;
            }
            else if (Session["UserName"] != null)
            {
                //Logged in without creating cookie
            }
            else
            {
                Session["UserName"] = null;
            }

            if (Request.Cookies["LoggedInTypeCookie"] != null && cookieValid == true)
            {
                Session["LoggedInType"] = Request.Cookies["LoggedInTypeCookie"].Value;
            }
            else if (Session["LoggedInType"] != null)
            {
                //Logged in without creating cookie
            }
            else
            {
                Session["LoggedInType"] = null;
            }

            //COOKIES CAN'T GO ON THEIR OWN PAGE FOR SOME REASON SO STORE IN SESSION VARIABLE HERE
            if (Request.Cookies["UserIDCookie"] != null && cookieValid == true)
            {
                Session["UserID"] = Request.Cookies["UserIDCookie"].Value;
            }
            else if (Session["UserID"] != null)
            {
                //Logged in without creating cookie
            }
            else
            {
                Session["UserID"] = null;
            }

            string userType;
            if (Session["LoggedInType"] != null)
            {
                userType = Session["LoggedInType"].ToString();
            }
            else
            {
                userType = "";
            }

            string urlPath = HttpContext.Current.Request.Url.AbsolutePath;

            //Check session variables haven't expired
            if (Session["UserName"] == null && !urlPath.Contains("InformUser"))
            {
                if (urlPath.Contains("default") || urlPath.Contains("Vehicles") || urlPath.Contains("ViewLocations")
                    || urlPath.Contains("AvailableVehicles") || urlPath.Contains("Register") || urlPath.Contains("Login"))
                {
                    //Currently on page that anyone can look at.
                }
                else
                {
                    //Return to the home page.
                    Response.Redirect("~/Account/InformUser.aspx?InfoString=Session+timeout.+Please+redo+action.", false);
                }
            }

            //Hides tabs or pages so the user cannot access them unless they have appropriate permissions
            if (userType == "Customer" || userType == "")
            {
                Page.Master.FindControl("locationAdd").Visible = false;
                Page.Master.FindControl("locationEdit").Visible = false;
                Page.Master.FindControl("openingTimes").Visible = false;
                Page.Master.FindControl("manageHolidayTimes").Visible = false;
                Page.Master.FindControl("vehicleAdd").Visible = false;
                Page.Master.FindControl("vehicleEdit").Visible = false;
                Page.Master.FindControl("companyAdd").Visible = false;
                Page.Master.FindControl("companyAddressAdd").Visible = false;
                Page.Master.FindControl("customerAdd").Visible = false;
                Page.Master.FindControl("locationDiv1").Visible = false;
                Page.Master.FindControl("locationDiv2").Visible = false;
                Page.Master.FindControl("vehicleDiv").Visible = false;
                Page.Master.FindControl("companyDiv").Visible = false;
                Page.Master.FindControl("bookableDiv").Visible = false;
                Page.Master.FindControl("bookingAdd").Visible = false;
                Page.Master.FindControl("ViewCustomerOrders").Visible = false;
                Page.Master.FindControl("updateCompany").Visible = false;
                Page.Master.FindControl("bookingEdit").Visible = false;
            }

            if (userType == "") 
            {
                Page.Master.FindControl("customerView").Visible = false;
                Page.Master.FindControl("customerAddressAdd").Visible = false;
                Page.Master.FindControl("customerDdl").Visible = false;
                Page.Master.FindControl("companyDdl").Visible = false;
                
            }
            if (userType == "Company") 
            {
                Page.Master.FindControl("customerAddressAdd").Visible = false;
                Page.Master.FindControl("viewOrders").Visible = false;
                Page.Master.FindControl("updateCustomer").Visible = false;
            }

            Page.Master.FindControl("customerDdl").Visible = false;
            Page.Master.FindControl("companyAdd").Visible = false;
            Page.Master.FindControl("companyDiv").Visible = false;

            var accountItem = Page.Master.FindControl("accountDdl");
            var loginItem = Page.Master.FindControl("loginDdl");
            var registerItem = Page.Master.FindControl("registerDdl");

            //Not using the address here
            Page.Master.FindControl("customerAddressAdd").Visible = false;
            Page.Master.FindControl("companyAddressAdd").Visible = false;

            if (Session["UserName"] == null)
            {
                accountItem.Visible = false;
                logout.Visible = false;
                loginItem.Visible = true;
                registerItem.Visible = true;
            }
            else
            {
                accountItem.Visible = true;
                logout.Visible = true;
                loginItem.Visible = false;
                registerItem.Visible = false;
            }
        }
    }

}