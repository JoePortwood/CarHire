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
    ///  Shows the order that was confirmed and saves the information to the database.
    /// </summary>
    public partial class OrderConfirmation : System.Web.UI.Page
    {
        /// <summary>
        ///  Need to be logged in to access this page.
        /// </summary>
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
                    //If order has not been entered
                else if (Session["Address"] == null ||
                        Session["VehicleAvailableID"] == null ||
                        Session["LocationID"] == null ||
                        Session["StartTime"] == null ||
                        Session["EndTime"] == null ||
                        Session["CustomerID"] == null)
                {
                    Response.Redirect("~/Account/InformUser.aspx?InfoString=Please+complete+ordering+process.", false);
                }
                if (!IsPostBack)
                {
                    DisplayInformation();
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Loads the information about the order from the session variables which were stored from the review order page.
        /// </summary>
        private void LoadInformation(ref VehicleManager vehicle, ref double totalDays, ref double totalCost, ref DateTime hireStart, 
            ref DateTime hireEnd, ref AddressManager address, ref CustomerManager customer, ref LocationManager location)
        {
            long vehicleAvailableID, locationID, customerID;
            bool useLocation;

            address = (AddressManager)Session["Address"];
            vehicleAvailableID = (long)Session["VehicleAvailableID"];
            locationID = (long)Session["LocationID"];
            hireStart = (DateTime)Session["StartTime"];
            hireEnd = (DateTime)Session["EndTime"];
            customerID = (long)Session["CustomerID"];
            useLocation = (bool)Session["UseLocation"];

            customer = CustomerManager.GetCustomers().Where(x => x.CustomerID == customerID).SingleOrDefault();
            vehicle = VehicleManager.GetAvailableVehicles(locationID).Where(x => x.VehicleAvailableID == vehicleAvailableID).SingleOrDefault();
            location = LocationManager.GetLocations().Where(x => x.LocationID == locationID).SingleOrDefault();

            totalDays = (hireEnd - hireStart).TotalDays;
            totalCost = totalDays * vehicle.BasePrice;
            totalCost = Math.Round(totalCost, 2); //Round to 2 dp
            
        }

        /// <summary>
        ///  Puts the information onto the page to display.
        /// </summary>
        private void DisplayInformation()
        {
            VehicleManager vehicle = null;
            DateTime hireStart = DateTime.Now, hireEnd = DateTime.Now;
            string manufacturer, model;
            double totalDays = 0, totalCost = 0;
            SIPPCode sizeOfVehicleSIPPCode, noOfDoorsSIPPCode, transmissionAndDriveSIPPCode, fuelAndACSIPPCode;
            AddressManager address = null;
            CustomerManager customer = null;
            LocationManager location = null;

            LoadInformation(ref vehicle, ref totalDays, ref totalCost, ref hireStart,
            ref hireEnd, ref address, ref customer, ref location);

            priceLbl.Text = "Total Price: £" + totalCost.ToString() + " for " + totalDays * 24 + " hours";

            addressLbl.Text = "Pick up from address: <br />" + address.GetAddressStr()
                + "<br />Location Contact: " + location.PhoneNo;

            vehicleImg.ImageUrl = vehicle.ImageLoc;
            vehicleImg.AlternateText = vehicle.Manufacturer + " " + vehicle.Model;

            //make standard size of car images 270x150
            vehicleImg.Width = Variables.STANDARDWIDTH;
            vehicleImg.Height = Variables.STANDARDHEIGHT;

            manufacturer = vehicle.Manufacturer;
            model = vehicle.Model;

            //Gets SIPP Code descriptions for all letters of SIPP code for this car
            sizeOfVehicleSIPPCode = SIPPCode.GetSIPPCodeDesc(Variables.SIZEOFVEHICLE, vehicle.SIPPCode[0].ToString());
            noOfDoorsSIPPCode = SIPPCode.GetSIPPCodeDesc(Variables.NOOFDOORS, vehicle.SIPPCode[1].ToString());
            transmissionAndDriveSIPPCode = SIPPCode.GetSIPPCodeDesc(Variables.TRANSMISSIONANDDRIVE, vehicle.SIPPCode[2].ToString());
            fuelAndACSIPPCode = SIPPCode.GetSIPPCodeDesc(Variables.FUELANDAC, vehicle.SIPPCode[3].ToString());

            carInfoLbl.Text = "<br /><b>" + manufacturer + " " + model + "</b><br />" + sizeOfVehicleSIPPCode.Description + "<br />"
                + noOfDoorsSIPPCode.Description + "<br />" + transmissionAndDriveSIPPCode.Description + "<br />"
                + fuelAndACSIPPCode.Description + "<br />";

            dateTimeLbl.Text = "<br /><b>Hire Start: " + hireStart.ToString("dd/MM/yyyy HH:mm tt") + "<br />"
                + "Hire End: " + hireEnd.ToString("dd/MM/yyyy HH:mm tt") + "</b>";
        }

        /// <summary>
        ///  Saves the order to the database and emails the appropriate accounts.
        /// </summary>
        private void SaveOrder()
        {
            VehicleManager vehicle = null;
            DateTime hireStart = DateTime.Now, hireEnd = DateTime.Now;
            string payerID = "", customerEmailAddress, locationEmailAddress, companyEmailAddress,
                subject = "", managerBody, body, orderInfo;
            double totalDays = 0, totalCost = 0;
            AddressManager address = null;
            CustomerManager customer = null;
            LocationManager location = null;

            LoadInformation(ref vehicle, ref totalDays, ref totalCost, ref hireStart,
            ref hireEnd, ref address, ref customer, ref location);

            bool useLocation = (bool)Session["UseLocation"];

            //Gets the customer, location and company email addresses to send the confirmation of the order to.
            customerEmailAddress = customer.EmailAddress;
            locationEmailAddress = location.EmailAddress;
            companyEmailAddress = CompanyManager.GetCompanyByLocation(location.LocationID);

            if (CompletePayment(totalCost, vehicle.Currency, ref payerID) == true)
            {
                //String that will be placed in email to summarise order.
                orderInfo = "<br /><br /><b>Vehicle:</b> " + vehicle.Manufacturer + " " + vehicle.Model + "<br /><b>SIPP Code:</b> " + vehicle.SIPPCode
                + "<br /><b>Total Cost:</b> " + vehicle.Currency + " " + totalCost + ". For " + totalDays + " days." +
                "<br /><b>Start:</b> " + hireStart.ToString() + ". <b>End:</b> " + hireEnd.ToString() + "<br /><b>Address:</b> " + address.GetAddressStr()
                + "<br /><br /><b>PayPal Payer ID:</b> " + payerID; 

                body = "<a href=" + Variables.URL + "Account/ViewOrders>Your Account </a>" + orderInfo;
                managerBody = "Complete this order by going to the <a href=" + Variables.URL + "Account/ViewCustomerOrders>customer orders page</a>" + orderInfo;
                if (useLocation == false)
                {
                    //If address is similar to address already on the system use that one
                    if (AddressManager.GetAddresses().Any(x => x.AddressID == address.AddressID))
                    {
                        OrderManager.InsertNewOrder(customer.CustomerID, address.AddressID, hireStart, hireEnd, vehicle.VehicleAvailableID, payerID);
                        confirmOrderNotify(customerEmailAddress, subject, body);
                        confirmOrderNotify(locationEmailAddress, subject, managerBody);
                        confirmOrderNotify(companyEmailAddress, subject, managerBody);
                    }
                    else
                    {
                        AddressManager.AddNewAddress(1, address.AddressLine1, address.AddressLine2, address.City
                            , address.ZipOrPostcode, address.CountyStateProvince, address.Country);
                        OrderManager.InsertNewOrder(customer.CustomerID, address.AddressID, hireStart, hireEnd, vehicle.VehicleAvailableID, payerID);
                        confirmOrderNotify(customerEmailAddress, subject, body);
                        confirmOrderNotify(locationEmailAddress, subject, managerBody);
                        confirmOrderNotify(companyEmailAddress, subject, managerBody);
                    }
                }
                else
                {
                    //Don't insert address ID as location address is used
                    OrderManager.InsertNewOrder(customer.CustomerID, 0, hireStart, hireEnd, vehicle.VehicleAvailableID, payerID);
                    confirmOrderNotify(customerEmailAddress, subject, body);
                    confirmOrderNotify(locationEmailAddress, subject, managerBody);
                    confirmOrderNotify(companyEmailAddress, subject, managerBody);
                }
            }
            else
            {
                Response.Redirect("~/Account/InformUser.aspx?InfoString=A+problem+has+occured+in+the+PayPal+payment.+Please+try+again.", false);
            }
        }

        /// <summary>
        ///  Emails the customer, location and company that an order has been added.
        /// </summary>
        private void confirmOrderNotify(string emailAddress, string subject, string body)
        {
            long orderID = 0;
            string PayPalPayerID = "";
            OrderManager.GetLastAddedOrder(ref orderID, ref PayPalPayerID);

            body = "Order " + orderID.ToString() + " Added. View on your account for more details. " + body;
            subject = "Order " + orderID.ToString() + " Added.";

            Variables.Email(emailAddress, subject, body);
        }

        /// <summary>
        ///  Completes the PayPal transaction.
        /// </summary>
        private bool CompletePayment(double totalCost, string currency, ref string payerID)
        {
            NVPAPICaller payPalCaller = new NVPAPICaller();

            string retMsg = "";
            string token = "";
            NVPCodec decoder = new NVPCodec();

            if (Session["token"] != null)
            {
                token = Session["token"].ToString();
            }

            bool ret = payPalCaller.GetCheckoutDetails(token, ref payerID, ref decoder, ref retMsg);

            if (ret == false)
            {
                Response.Redirect("~/Account/InformUser.aspx?InfoString=Please+complete+PayPal+payment.", false);
            }

            ret = payPalCaller.DoCheckoutPayment(totalCost.ToString(), token, payerID, ref decoder, ref retMsg, currency);

            return ret;
        }

        /// <summary>
        ///  Adds order to the database then goes to the checkout complete page to show payer id of PayPal.
        /// </summary>
        protected void ConfirmOrderBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SaveOrder();
                Response.Redirect("~/CheckoutComplete", false);
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }
    }
}