using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using CarHireWebApp.Models;
using CarHireDBLibrary;
using System.Linq;

namespace CarHireWebApp.Account
{
    public partial class ForgotPassword : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Forgot(object sender, EventArgs e)
        {
            //Could technically have same email address and username. Need to make another forgot page
            CompanyManager company;
            company = CompanyManager.GetCompanies().Where(x => x.UserName == userNameTxt.Text && x.EmailAddress == emailTxt.Text).SingleOrDefault();

            CustomerManager customer;
            customer = CustomerManager.GetCustomers().Where(x => x.UserName == userNameTxt.Text && x.EmailAddress == emailTxt.Text).SingleOrDefault();

            if (company != null)
            {
                SendEmail(company.EmailAddress, UserAccess.UserType.company, company.CompanyID, company.UserName);
            }
            else if (customer != null)
            {
                SendEmail(customer.EmailAddress, UserAccess.UserType.customer, customer.CustomerID, customer.UserName);
            }
            else
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "User name or email incorrect";
            }
        }
        
        private void SendEmail(string emailAddress, UserAccess.UserType type, long id, string userName)
        {
            PasswordResetRequest.InsertNewRequest((int)type, id, userName);

            Variables.Email(emailAddress, "Password Reset", "<html><body><p>Click here to reset password: " + "<a href=\"" + Variables.URL + "Account/ResetPassword" +
            "?AccountType=" + (int)type + "&ResetByEmail=true&AccountID=" + id + "&UserName=" + userName + "\">Reset</a></p></body></html>");
            Response.Redirect("~/Account/InformUser.aspx?InfoString=An+email+has+been+sent.+Please+click+on+the+link+on+the+email+to+reset+your+password", false);
        }
    }
}