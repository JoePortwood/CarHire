using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CarHireDBLibrary;

namespace CarHireWebApp
{
    public partial class AddCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["LoggedInType"] == null)
                {
                    Response.Redirect(Variables.REDIRECT, false);
                }
                else if (Session["LoggedInType"].ToString() == "")
                {
                    Response.Redirect(Variables.REDIRECT, false);
                }
                else if (Session["LoggedInType"].ToString() == "Customer")
                {
                    Response.Redirect(Variables.REDIRECT, false);
                }
                generalErrorLbl.Text = "";
                inputErrorLbl.Text = "";
                companySavedLbl.Text = "";
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        protected void CompanyAddBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string userName, companyName, companyDescription, phoneNo, emailAddress;
                string addressLine1, addressLine2, addressLine3, addressLine4, city, zipOrPostcode,
                        countyStateProvince, country, otherAddressDetails;
                bool insertCompany = true; //boolean to check all fields are entered correctly
                long companyID;

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

                if (phoneNoTxt.Text != "")
                {
                    phoneNo = phoneNoTxt.Text;
                }
                else
                {
                    phoneNo = "";
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

                companyDescription = companyDescriptionTxt.Text;
                addressLine1 = addressLine1Txt.Text;
                addressLine2 = addressLine2Txt.Text;
                addressLine3 = addressLine3Txt.Text;
                addressLine4 = addressLine4Txt.Text;

                if (cityTxt.Text != "")
                {
                    city = cityTxt.Text;
                }
                else
                {
                    city = "";
                    insertCompany = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a city.";
                }

                if (Variables.CheckAlphaNumericCharacters(zipOrPostcodeTxt.Text) == true)
                {
                    zipOrPostcode = zipOrPostcodeTxt.Text;
                }
                else
                {
                    zipOrPostcode = "";
                    insertCompany = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Invalid zip or postcode.";
                }

                countyStateProvince = countyStateProvinceTxt.Text;

                if (countryTxt.Text != "")
                {
                    country = countryTxt.Text;
                }
                else
                {
                    country = "";
                    insertCompany = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a country.";
                }

                otherAddressDetails = otherAddressDetailsTxt.Text;

                #endregion

                if (insertCompany == true)
                {
                    //CompanyManager.AddNewCompany(userName, companyName, companyDescription, phoneNo, emailAddress, addressLine1, addressLine2, 
                    //addressLine3, addressLine4, city, zipOrPostcode, countyStateProvince, country, otherAddressDetails);
                    companySavedLbl.Text = "Save successful";

                    //locationID = LocationManager.getLastAddedLocation();
                    //OpeningTime.insertDefaultOpeningTimes(locationID);

                    //Proceed on to adding addresses page
                    Response.Redirect("AdditionalAddresses.aspx?Company=true", false);
                }

            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }
    }
}