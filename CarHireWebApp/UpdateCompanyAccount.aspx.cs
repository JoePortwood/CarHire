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
    ///  Updates the current logged in company.
    /// </summary>
    public partial class UpdateCompany : System.Web.UI.Page
    {
        public CompanyManager company;

        /// <summary>
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                generalErrorLbl.Text = "";
                inputErrorLbl.Text = "";
                companySavedLbl.Text = "";
                ErrorMessage.Text = "";

                if (!IsPostBack)
                {
                    LoadCompany();
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        private void LoadCompany()
        {
            try
            {
                company = CompanyManager.GetCompanies().Where(x => x.CompanyID == Convert.ToInt32(Session["UserID"])).SingleOrDefault();

                companyNameTxt.Text = company.CompanyName;
                companyDescriptionTxt.Text = company.CompanyDescription;
                licensingDetailsTxt.Text = company.LicensingDetails;
                emailAddressTxt.Text = company.EmailAddress;
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Update the company in the database assuming all fields entered are correct.
        /// </summary>
        protected void UpdateUser_Click(object sender, EventArgs e)
        {
            try
            {
                string companyName, companyDescription, licensingDetails, phoneNo, emailAddress;
                bool updateCompany = true; //boolean to check all fields are entered correctly

                #region companyCheck

                if (companyNameTxt.Text != "")
                {
                    companyName = companyNameTxt.Text;
                }
                else
                {
                    companyName = "";
                    updateCompany = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a company name.";
                }

                phoneNo = Request["phoneNoTxt"];
                if (phoneNo == "")
                {
                    updateCompany = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a phone no.";
                }

                if (emailAddressTxt.Text != "")
                {
                    emailAddress = emailAddressTxt.Text;
                }
                else
                {
                    emailAddress = "";
                    updateCompany = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a email address.";
                }

                if (licensingDetailsTxt.Text != "")
                {
                    licensingDetails = licensingDetailsTxt.Text;
                }
                else
                {
                    licensingDetails = "";
                    updateCompany = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter licensing details.";
                }

                companyDescription = companyDescriptionTxt.Text;

                #endregion

                if (updateCompany == true)
                {
                    CompanyManager.UpdateCompany(Convert.ToInt32(Session["UserID"]), companyName, companyDescription, licensingDetails, phoneNo, emailAddress);
                    companySavedLbl.Text = "Save successful";
                }

                //Refresh values
                LoadCompany();
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }
    }
}