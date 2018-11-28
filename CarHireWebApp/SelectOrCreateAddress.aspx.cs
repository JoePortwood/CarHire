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
    ///  This class allows for previous addresses used by that customer to be used again or create a new address.
    ///  The address of the location can be used instead of an address the customer has entered.
    /// </summary>
    public partial class SelectOrCreateAddresses : System.Web.UI.Page
    {
        /// <summary>
        ///  Struct to check whether the address entered is already in the database.
        /// </summary>
        private struct AddressValid
        {
            public List<AddressManager> addresses;
            public AddressManager existingAddress;
            public bool addOrder;
            public string addressLine1;
            public string addressLine2;
            public string city;
            public string zipOrPostcode;
            public string countyStateProvince;
            public string country;
        }

        /// <summary>
        ///  Need to be logged in to access this page
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
                else if (Session["LoggedInType"].ToString() == "Company")
                {
                    customersInCompanyDdl.Visible = true;

                    CompanyManager company = CompanyManager.GetCompanies().Where(x => x.UserName == Session["UserName"].ToString()).SingleOrDefault();
                    List<CustomerManager> customers = CustomerManager.GetCustomers().Where(x => x.CompanyID == company.CompanyID).ToList();

                    if (customers.Count != 0)
                    {
                        //Put all the customers that are part of this company into the drop down list.
                        if (!customersInCompanyDdl.Items.Contains(new ListItem("")))
                        {
                            customersInCompanyDdl.Items.Add("");
                        }
                        foreach (CustomerManager customer in customers)
                        {
                            if (!customersInCompanyDdl.Items.Contains(new ListItem(customer.CustomerID + ", " + customer.Surname)))
                            {
                                customersInCompanyDdl.Items.Add(customer.CustomerID + ", " + customer.Surname);
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Account/InformUser.aspx?InfoString=No+customers+for+your+company.+Please+make+sure+a+customer+is+a+member+of+your+company.", false);
                    }
                }

                orderCreatedLbl.Text = "";
                generalErrorLbl.Text = "";
                inputErrorLbl.Text = "";

                if (customersInCompanyDdl.SelectedValue != "" || Session["LoggedInType"].ToString() == "Customer")
                {
                    AddHeaderRow();
                    LoadAddresses();
                }
                if (IsPostBack)
                {
                    GetAddress(true);
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Adds table header for vehicle table.
        /// </summary>
        private void AddHeaderRow()
        {
            TableHeaderRow tr = new TableHeaderRow();

            TableCell cell = new TableCell();
            cell.Text = "Previous Delivery Addresses";
            cell.CssClass = "h4";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "";
            cell.CssClass = "h4";
            tr.Cells.Add(cell);

            addressesTbl.Rows.Add(tr);

            tr = new TableHeaderRow();
            cell = new TableCell();
            cell.Text = "Office Address";
            cell.CssClass = "h4";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "";
            cell.CssClass = "h4";
            tr.Cells.Add(cell);

            locationAddressTbl.Rows.Add(tr);
        }

        private void AddSimilarHeaderRow()
        {
            TableHeaderRow tr = new TableHeaderRow();

            TableCell cell = new TableCell();
            cell.Text = "Similar Addresses";
            cell.CssClass = "h4";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "";
            cell.CssClass = "h4";
            tr.Cells.Add(cell);

            similarAddressesTbl.Rows.Add(tr);
        }

        private void LoadAddresses()
        {
            TableRow row;
            TableCell cell;
            List<AddressManager> addresses;
            CustomerManager customer = null;

            //If the customer is logged in then use the customer otherwise the company selects a customer and that customer's addresses are loaded.
            if (Session["LoggedInType"].ToString() == "Customer")
            {
                customer = CustomerManager.GetCustomers().Where(x => x.UserName.Equals(Session["UserName"].ToString(), StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            }
            else if (Session["LoggedInType"].ToString() == "Company")
            {
                CompanyManager company;
                company = CompanyManager.GetCompanies().Where(x => x.UserName.Equals(Session["UserName"].ToString(), StringComparison.OrdinalIgnoreCase)).SingleOrDefault();

                customer = CustomerManager.GetCustomers().Where(x => x.CustomerID.ToString() == customersInCompanyDdl.SelectedValue.Split(',')[0]).SingleOrDefault();
            }

            row = new TableRow();
            cell = new TableCell();
            cell.Text = GetLocation().GetAddressStr();
            row.Cells.Add(cell);

            //Book button to use the address
            Button locationAddressBtn = new Button();
            //locationAddressBtn.ID = "Location_Button" + "_" + customer.CustomerID + "_BookBtn";
            locationAddressBtn.Text = "Use Location";
            locationAddressBtn.CssClass = "btn btn-primary";
            locationAddressBtn.Click += new EventHandler(UseLocationAddressBtn_Click);
            cell = new TableCell();
            cell.Controls.Add(locationAddressBtn);
            row.Cells.Add(cell);
            locationAddressTbl.Rows.Add(row);

            addresses = AddressManager.GetAddressesByCustomer(customer.CustomerID);

            if (addresses.Count > 0)
            {
                foreach (AddressManager address in addresses)
                {
                    row = new TableRow();
                    cell = new TableCell();
                    cell.Text = address.GetAddressStr();
                    row.Cells.Add(cell);

                    //Book button to use the address
                    Button useAddressBtn = new Button();
                    useAddressBtn.ID = address.AddressID + "_" + customer.CustomerID + "_BookBtn";
                    useAddressBtn.Text = "Use";
                    useAddressBtn.CssClass = "btn btn-primary";
                    useAddressBtn.Click += new EventHandler(UseAddressBtn_Click);
                    cell = new TableCell();
                    cell.Controls.Add(useAddressBtn);
                    row.Cells.Add(cell);
                    addressesTbl.Rows.Add(row);
                }
            }
            else
            {
                row = new TableRow();
                cell = new TableCell();
                cell.Text = "No previous addresses";
                row.Cells.Add(cell);
                addressesTbl.Rows.Add(row);
            }
        }

        private void LoadAddresses(List<AddressManager> similarAddresses)
        {
            TableRow row;
            TableCell cell;
            CustomerManager customer = null;

            if (Session["LoggedInType"].ToString() == "Customer")
            {
                customer = CustomerManager.GetCustomers().Where(x => x.UserName.Equals(Session["UserName"].ToString(), StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            }
            else if (Session["LoggedInType"].ToString() == "Company")
            {
                CompanyManager company;
                company = CompanyManager.GetCompanies().Where(x => x.UserName.Equals(Session["UserName"].ToString(), StringComparison.OrdinalIgnoreCase)).SingleOrDefault();

                customer = CustomerManager.GetCustomers().Where(x => x.CustomerID.ToString() == customersInCompanyDdl.SelectedValue.Split(',')[0]).SingleOrDefault();
            }

            foreach (AddressManager address in similarAddresses)
            {
                row = new TableRow();
                cell = new TableCell();
                cell.Text = address.GetAddressStr();
                row.Cells.Add(cell);

                //Book button to use the address
                Button useAddressBtn = new Button();
                useAddressBtn.ID = address.AddressID + "_" + customer.CustomerID + "_SimilarBookBtn";
                useAddressBtn.Text = "Use";
                useAddressBtn.CssClass = "btn btn-primary";
                useAddressBtn.Click += new EventHandler(UseAddressBtn_Click);
                cell = new TableCell();
                cell.Controls.Add(useAddressBtn);
                row.Cells.Add(cell);
                similarAddressesTbl.Rows.Add(row);
            }
        }

        /// <summary>
        ///  To force a postback when a customer is selected.
        /// </summary>
        protected void CustomersInCompanyDdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Uses a previously entered address.
        /// </summary>
        protected void UseAddressBtn_Click(object sender, EventArgs e)
        {
            try
            {
                long addressID, customerID;

                //Find the ID of the button clicked
                Button bookBtn = (Button)sender;

                //Only gets numbers at the start of the button ID
                addressID = Convert.ToInt32(bookBtn.ID.Split('_')[0]);
                customerID = Convert.ToInt32(bookBtn.ID.Split('_')[1]);
                CreateOrderInformation(addressID, customerID, null);
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Uses the location of the vehicle's address.
        /// </summary>
        protected void UseLocationAddressBtn_Click(object sender, EventArgs e)
        {
            try
            {
                long customerID;
                LocationManager location;

                //Find the ID of the button clicked
                Button bookBtn = (Button)sender;

                location = GetLocation();

                AddressManager address = new AddressManager(0, 1, location.AddressLine1, location.AddressLine2, location.City
                    , location.ZipOrPostcode, location.CountyStateProvince, location.Country);

                if (Session["LoggedInType"].ToString() == "Company")
                {
                    //Only gets numbers at the start of the button ID
                    customerID = Convert.ToInt32(customersInCompanyDdl.SelectedValue.Split(',')[0]);
                }
                else
                {
                    customerID = Variables.GetUser(Session["UserID"].ToString());
                }
                CreateOrderInformation(0, customerID, address);
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Adds a new address to be used for this order but does not save it to the database until the order is completed
        ///  to prevent unused records from being added.
        /// </summary>
        protected void AddOrderBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string addressLine1, addressLine2, city, zipOrPostcode, countyStateProvince, country;
                bool addOrder = true;
                long vehicleAvailableID, locationID, addressID;
                DateTime hireStart, hireEnd;
                CustomerManager customer = null;
                AddressManager address;

                addressLine1 = addressLine1Txt.Text;
                addressLine2 = addressLine2Txt.Text;

                if (Variables.CheckAlphaNumericCharacters(cityTxt.Text) && cityTxt.Text != "")
                {
                    city = cityTxt.Text;
                }
                else
                {
                    city = "";
                    addOrder = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a valid city.";
                }

                if (Variables.CheckAlphaNumericCharacters(zipOrPostcodeTxt.Text) == true && zipOrPostcodeTxt.Text != "")
                {
                    zipOrPostcode = zipOrPostcodeTxt.Text;
                }
                else
                {
                    zipOrPostcode = "";
                    addOrder = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Invalid zip or postcode.";
                }

                countyStateProvince = countyStateProvinceTxt.Text;

                country = Variables.GetCountryByCode(Request["countryDdl"]);
                if (country == "")
                {
                    addOrder = false;
                    inputErrorLbl.Text = inputErrorLbl.Text + "<br />" + "Please enter a country.";
                }

                AddressValid addressSuccess = GetAddress(false);

                if (addressSuccess.addOrder == true && addOrder == true)
                {
                    vehicleAvailableID = Convert.ToInt32(Request.QueryString["VehicleAvailableID"]);
                    locationID = Convert.ToInt32(Request.QueryString["LocationID"]);
                    hireStart = Convert.ToDateTime(Request.QueryString["StartDateTime"]);
                    hireEnd = Convert.ToDateTime(Request.QueryString["EndDateTime"]);

                    //AddressManager.AddNewAddress(1, addressSuccess.addressLine1, addressSuccess.addressLine2, addressSuccess.city
                    //    , addressSuccess.zipOrPostcode, addressSuccess.countyStateProvince, addressSuccess.country);

                    if (customersInCompanyDdl.SelectedValue != "")
                    {
                        if (Session["LoggedInType"].ToString() == "Customer")
                        {
                            customer = CustomerManager.GetCustomers().Where(x => x.UserName.Equals(Session["UserName"].ToString(), StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
                        }
                        else if (Session["LoggedInType"].ToString() == "Company")
                        {
                            CompanyManager company;
                            company = CompanyManager.GetCompanies().Where(x => x.UserName.Equals(Session["UserName"].ToString(), StringComparison.OrdinalIgnoreCase)).SingleOrDefault();

                            customer = CustomerManager.GetCustomers().Where(x => x.CustomerID.ToString() == customersInCompanyDdl.SelectedValue.Split(',')[0]).SingleOrDefault();
                        }

                        if (addressSuccess.existingAddress == null)
                        {
                            addressID = AddressManager.GetLastAddedAddress();

                            address = new AddressManager(addressID + 1, 1, addressSuccess.addressLine1, addressSuccess.addressLine2, addressSuccess.city
                                , addressSuccess.zipOrPostcode, addressSuccess.countyStateProvince, addressSuccess.country);
                        }
                        else
                        {
                            address = addressSuccess.existingAddress;
                        }

                        StoreSessionVariables(address, vehicleAvailableID, locationID, hireStart, hireEnd, customer.CustomerID, false);
                        Response.Redirect("~/ReviewOrder", false);

                        orderCreatedLbl.Text = "Order Created";
                    }
                    else
                    {
                        inputErrorLbl.Text = "Please select a customer to book for";
                    }
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Checks whether the added address is similar to an address already in the database and if not
        ///  store this address in a session variable to be added when the order is completed.
        /// </summary>
        private AddressValid GetAddress(bool createTable)
        {
            List<AddressManager> similarAddresses = new List<AddressManager>();
            List<AddressManager> currentAddresses = AddressManager.GetAddresses();
            AddressValid addressSuccess = new AddressValid();
            bool addOrder = true;
            string addressLine1, addressLine2, city, zipOrPostcode, countyStateProvince, country;

            addressLine1 = addressLine1Txt.Text;
            addressSuccess.addressLine1 = addressLine1;
            addressLine2 = addressLine2Txt.Text;
            addressSuccess.addressLine2 = addressLine2;

            city = cityTxt.Text;
            addressSuccess.city = city;

            zipOrPostcode = zipOrPostcodeTxt.Text;
            addressSuccess.zipOrPostcode = zipOrPostcode;

            countyStateProvince = countyStateProvinceTxt.Text;
            addressSuccess.countyStateProvince = countyStateProvince;

            country = Variables.GetCountryByCode(Request["countryDdl"]);
            addressSuccess.country = country;

            string addeddAddress = addressLine1 + addressLine2 + city + zipOrPostcode + countyStateProvince + country;

            foreach (AddressManager address in currentAddresses)
            {
                int diff = FuzzySearching.LD(addeddAddress, address.GetAddressStrWithoutBreaks());
                if (diff == 0)
                {
                    addressSuccess.existingAddress = address;
                    addOrder = true;
                }
                //Only get addresses that have less than 5 characters different    
                else if (diff < 5)
                {
                    similarAddresses.Add(address);
                    addOrder = false;
                    addressesTbl.Visible = false;
                    similarAddressesTbl.Visible = true;
                }
            }

            if (similarAddresses.Count > 0 && createTable == true)
            {
                AddSimilarHeaderRow();
                LoadAddresses(similarAddresses);
            }

            addressSuccess.addresses = similarAddresses;
            addressSuccess.addOrder = addOrder;
            return addressSuccess;
        }

        /// <summary>
        ///  Creates the address information needed for the order and forwards the user onto the review order page.
        /// </summary>
        private void CreateOrderInformation(long addressID, long customerID, AddressManager locationAddress)
        {
            long vehicleAvailableID, locationID, orderID;
            DateTime hireStart, hireEnd;
            AddressManager address;

            vehicleAvailableID = Convert.ToInt32(Request.QueryString["VehicleAvailableID"]);
            locationID = Convert.ToInt32(Request.QueryString["LocationID"]);
            hireStart = Convert.ToDateTime(Request.QueryString["StartDateTime"]);
            hireEnd = Convert.ToDateTime(Request.QueryString["EndDateTime"]);

            //Check minimum amount of hours is 12 to continue
            if ((hireEnd - hireStart).TotalHours >= 12)
            {
                //Checks whether the location address is being used or not - if not used it is null
                if (locationAddress == null)
                {
                    address = AddressManager.GetAddresses().Where(x => x.AddressID == addressID).SingleOrDefault();
                    StoreSessionVariables(address, vehicleAvailableID, locationID, hireStart, hireEnd, customerID, false);
                }
                else
                {
                    address = locationAddress;
                    StoreSessionVariables(address, vehicleAvailableID, locationID, hireStart, hireEnd, customerID, true);
                }
                Response.Redirect("~/ReviewOrder", false);

                orderCreatedLbl.Text = "Order Created";
            }
            else
            {
                inputErrorLbl.Text = "Hours to hire less than 12. Please select more.";
            }
        }

        private void StoreSessionVariables(AddressManager address, long vehicleAvailableID, long locationID, DateTime hireStart, 
            DateTime hireEnd, long customerID, bool useLocation)
        {
            Session["Address"] = address;
            Session["VehicleAvailableID"] = vehicleAvailableID;
            Session["LocationID"] = locationID;
            Session["StartTime"] = hireStart;
            Session["EndTime"] = hireEnd;
            Session["CustomerID"] = customerID;
            Session["OrderConfirmed"] = false;
            Session["UseLocation"] = useLocation;
        }

        private LocationManager GetLocation()
        {
            long locationID = Convert.ToInt32(Request.QueryString["LocationID"]);
            LocationManager location;
            location = LocationManager.GetLocations().Where(x => x.LocationID == locationID).SingleOrDefault();

            return location;
        }
    }
}