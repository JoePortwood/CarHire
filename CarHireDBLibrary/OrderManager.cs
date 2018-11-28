using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarHireDBLibrary
{
    public class OrderManager
    {
        private long m_OrderID;
        private long m_CustomerID;
        private long m_AddressID;
        private DateTime m_HireStart;
        private DateTime m_HireEnd;
        private DateTime m_BookingCreated;
        private string m_OrderStatus;

        public long OrderID
        {
            get { return m_OrderID; }
        }

        public long CustomerID
        {
            get { return m_CustomerID; }
        }

        public long AddressID
        {
            get { return m_AddressID; }
        }

        public DateTime HireStart
        {
            get { return m_HireStart; }
        }

        public DateTime HireEnd
        {
            get { return m_HireEnd; }
        }

        public DateTime BookingCreated
        {
            get { return m_BookingCreated; }
        }

        public string OrderStatusStr
        {
            get { return m_OrderStatus; }
        }

        public enum OrderStatus
        {
            Pending = 1,
            Accepted = 2,
            Declined = 3,
            Complete = 4,
            Cancelled = 8
        };

        /// <summary>
        /// Constructor for vehicle.
        /// </summary>
        public OrderManager(long orderID, long customerID, long addressID, DateTime hireStart, DateTime hireEnd, DateTime bookingCreated)
        {
            m_OrderID = orderID;
            m_CustomerID = customerID;
            m_AddressID = addressID;
            m_HireStart = hireStart;
            m_HireEnd = hireEnd;
            m_BookingCreated = bookingCreated;
            m_OrderStatus = OrderStatus.Pending.ToString();
        }

        /// <summary>
        /// Inserting new order.
        /// </summary>
        public static void InsertNewOrder(long customerID, long addressID, DateTime hireStart, DateTime hireEnd, long vehicleAvailableID, string payerID)
        {
            try
            {
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("INSERT_Order", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@CustomerID", SqlDbType.BigInt).Value = customerID;
                        if (addressID != 0)
                        {
                            myCommand.Parameters.Add("@AddressID", SqlDbType.BigInt).Value = addressID;
                        }
                        myCommand.Parameters.Add("@HireStart", SqlDbType.DateTime).Value = hireStart;
                        myCommand.Parameters.Add("@HireEnd", SqlDbType.DateTime).Value = hireEnd;
                        myCommand.Parameters.Add("@VehicleAvailableID", SqlDbType.BigInt).Value = vehicleAvailableID;
                        myCommand.Parameters.Add("@PayPalPayerID", SqlDbType.VarChar).Value = payerID;

                        myConnection.Open();
                        myCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public static void SelectOrderCustomer(ref List<VehicleManager> vehicles, ref List<LocationManager> locations, ref List<AddressManager> addresses,
            ref List<long> orderIDs, ref List<DateTime> hireStarts, ref List<DateTime> hireEnds, ref List<double> totalCosts, ref List<string> orderStatuses,
            ref List<string> phoneNos, ref List<string> emailAddresses, ref List<string> customerEmailAddresses, ref List<string> currencies, long customerID)
        {
            try
            {
                //long orderID, companyID;
                //DateTime hireStart, hireEnd;
                //OrderStatus orderStatus;
                double totalDays, basePrice, totalCost;
                string addressLine1 = "", addressLine2 = "", countyStateProvince = "", imageLoc = "";
                int capacity;

                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("SELECT_OrderInfo", myConnection))
                    {
                        myConnection.Open();

                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@CustomerID", SqlDbType.BigInt).Value = customerID;

                        SqlDataReader myReader = null;

                        myReader = myCommand.ExecuteReader();
                        while (myReader.Read())
                        {
                            orderIDs.Add((long)(myReader["OrderID"]));
                            //companyID = (long)(myReader["CompanyID"]);
                            hireStarts.Add((DateTime)(myReader["HireStart"]));
                            hireEnds.Add((DateTime)(myReader["HireEnd"]));
                            basePrice = (double)(myReader["BasePrice"]);
                            orderStatuses.Add((myReader["OrderStatus"].ToString()));

                            //Either get the location address or customer entered address
                            if (!myReader.IsDBNull(myReader.GetOrdinal("AddressID")))
                            {
                                if (!myReader.IsDBNull(myReader.GetOrdinal("Line1")))
                                {
                                    addressLine1 = (myReader["Line1"].ToString());
                                }
                                if (!myReader.IsDBNull(myReader.GetOrdinal("Line2")))
                                {
                                    addressLine2 = (myReader["Line2"].ToString());
                                }
                                if (!myReader.IsDBNull(myReader.GetOrdinal("CountyStateProvince")))
                                {
                                    countyStateProvince = (myReader["CountyStateProvince"].ToString());
                                }
                                addresses.Add(new AddressManager((long)(myReader["AddressID"]), (int)(myReader["AddressType"]), addressLine1
                                    , addressLine2, (myReader["City"].ToString()), (myReader["ZipOrPostcode"].ToString()),
                                    countyStateProvince, (myReader["Country"].ToString())));

                                locations.Add(new LocationManager());
                            }
                            else
                            {
                                if (!myReader.IsDBNull(myReader.GetOrdinal("LocationAddressLine1")))
                                {
                                    addressLine1 = (myReader["LocationAddressLine1"].ToString());
                                }
                                if (!myReader.IsDBNull(myReader.GetOrdinal("LocationAddressLine2")))
                                {
                                    addressLine2 = (myReader["LocationAddressLine2"].ToString());
                                }
                                if (!myReader.IsDBNull(myReader.GetOrdinal("LocationCountyStateProvince")))
                                {
                                    countyStateProvince = (myReader["LocationCountyStateProvince"].ToString());
                                }
                                if (!myReader.IsDBNull(myReader.GetOrdinal("Capacity")))
                                {
                                    capacity = (int)(myReader["Capacity"]);
                                }
                                else
                                {
                                    capacity = 0;
                                }
                                locations.Add(new LocationManager((long)(myReader["LocationID"]), (myReader["LocationName"].ToString()),
                                    (myReader["OwnerName"].ToString()), capacity, addressLine1
                                    , addressLine2, (myReader["LocationCity"].ToString()), (myReader["LocationZipOrPostcode"].ToString())
                                    , countyStateProvince, (myReader["LocationCountry"].ToString())
                                    , (myReader["LocationPhoneNo"].ToString()), (myReader["LocationEmailAddress"].ToString()), 0, 0, true, 0));

                                addresses.Add(new AddressManager());
                            }
                            phoneNos.Add(myReader["LocationPhoneNo"].ToString());
                            emailAddresses.Add(myReader["LocationEmailAddress"].ToString());
                            customerEmailAddresses.Add(myReader["CustomerEmailAddress"].ToString());

                            if (!myReader.IsDBNull(myReader.GetOrdinal("ImageLoc")))
                            {
                                imageLoc = (myReader["ImageLoc"].ToString());
                            }
                            else
                            {
                                imageLoc = "";
                            }
                            vehicles.Add(new VehicleManager((long)(myReader["VehicleID"]), (myReader["Manufacturer"].ToString()), (myReader["Model"].ToString())
                                , (myReader["SIPPCode"].ToString()), (double)(myReader["MPG"]), imageLoc, true, 0));
                            //active = (bool)(myReader["Active"]);
                            //userID = (long)(myReader["UserID"]);

                            totalDays = ((DateTime)(myReader["HireEnd"]) - (DateTime)(myReader["HireStart"])).TotalDays;
                            totalCost = totalDays * basePrice;
                            totalCosts.Add(Math.Round(totalCost, 2)); //Round to 2 dp
                            currencies.Add(myReader["Currency"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public static void SelectOrdersByCompany(ref List<VehicleManager> vehicles, ref List<LocationManager> locations, ref List<AddressManager> addresses,
            ref List<CustomerManager> customers, ref List<long> orderIDs, ref List<DateTime> hireStarts, ref List<DateTime> hireEnds, ref List<double> totalCosts, 
            ref List<string> orderStatuses, ref List<string> phoneNos, ref List<string> emailAddresses, ref List<string> currencies, long companyID)
        {
            try
            {
                double totalDays, basePrice, totalCost;
                string addressLine1 = "", addressLine2 = "", countyStateProvince = "", imageLoc = "";
                int capacity;

                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("SELECT_OrderInfo", myConnection))
                    {
                        myConnection.Open();

                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@CompanyID", SqlDbType.BigInt).Value = companyID;

                        SqlDataReader myReader = null;

                        myReader = myCommand.ExecuteReader();
                        while (myReader.Read())
                        {
                            orderIDs.Add((long)(myReader["OrderID"]));
                            //companyID = (long)(myReader["CompanyID"]);
                            hireStarts.Add((DateTime)(myReader["HireStart"]));
                            hireEnds.Add((DateTime)(myReader["HireEnd"]));
                            basePrice = (double)(myReader["BasePrice"]);
                            orderStatuses.Add((myReader["OrderStatus"].ToString()));
                            if (!myReader.IsDBNull(myReader.GetOrdinal("AddressID")))
                            {
                                if (!myReader.IsDBNull(myReader.GetOrdinal("Line1")))
                                {
                                    addressLine1 = (myReader["Line1"].ToString());
                                }
                                if (!myReader.IsDBNull(myReader.GetOrdinal("Line2")))
                                {
                                    addressLine2 = (myReader["Line2"].ToString());
                                }
                                if (!myReader.IsDBNull(myReader.GetOrdinal("CountyStateProvince")))
                                {
                                    countyStateProvince = (myReader["CountyStateProvince"].ToString());
                                }
                                addresses.Add(new AddressManager((long)(myReader["AddressID"]), (int)(myReader["AddressType"]), addressLine1
                                    , addressLine2, (myReader["City"].ToString()), (myReader["ZipOrPostcode"].ToString()),
                                    countyStateProvince, (myReader["Country"].ToString())));
                            }
                            else
                            {
                                addresses.Add(new AddressManager());
                            }

                            if (!myReader.IsDBNull(myReader.GetOrdinal("LocationAddressLine1")))
                            {
                                addressLine1 = (myReader["LocationAddressLine1"].ToString());
                            }
                            if (!myReader.IsDBNull(myReader.GetOrdinal("LocationAddressLine2")))
                            {
                                addressLine2 = (myReader["LocationAddressLine2"].ToString());
                            }
                            if (!myReader.IsDBNull(myReader.GetOrdinal("LocationCountyStateProvince")))
                            {
                                countyStateProvince = (myReader["LocationCountyStateProvince"].ToString());
                            }
                            if (!myReader.IsDBNull(myReader.GetOrdinal("Capacity")))
                            {
                                capacity = (int)(myReader["Capacity"]);
                            }
                            else
                            {
                                capacity = 0;
                            }
                            locations.Add(new LocationManager((long)(myReader["LocationID"]), (myReader["LocationName"].ToString()),
                                (myReader["OwnerName"].ToString()), capacity, addressLine1
                                , addressLine2, (myReader["LocationCity"].ToString()), (myReader["LocationZipOrPostcode"].ToString())
                                , countyStateProvince, (myReader["LocationCountry"].ToString())
                                , (myReader["LocationPhoneNo"].ToString()), (myReader["LocationEmailAddress"].ToString()), 0, 0, true, 0));
                            phoneNos.Add(myReader["LocationPhoneNo"].ToString());
                            emailAddresses.Add(myReader["LocationEmailAddress"].ToString());

                            if (!myReader.IsDBNull(myReader.GetOrdinal("ImageLoc")))
                            {
                                imageLoc = (myReader["ImageLoc"].ToString());
                            }
                            vehicles.Add(new VehicleManager((long)(myReader["VehicleID"]), (myReader["Manufacturer"].ToString()), (myReader["Model"].ToString())
                                , (myReader["SIPPCode"].ToString()), (double)(myReader["MPG"]), imageLoc, true, 0));
                            //active = (bool)(myReader["Active"]);
                            //userID = (long)(myReader["UserID"]);

                            totalDays = ((DateTime)(myReader["HireEnd"]) - (DateTime)(myReader["HireStart"])).TotalDays;
                            totalCost = totalDays * basePrice;
                            totalCosts.Add(Math.Round(totalCost, 2)); //Round to 2 dp
                            currencies.Add(myReader["Currency"].ToString());

                            customers.Add(new CustomerManager((long)(myReader["CustomerID"]),
                                companyID,
                                (myReader["CustomerUserName"].ToString()),
                                (myReader["Surname"].ToString()),
                                (myReader["Forename"].ToString()),
                                (myReader["CompanyName"].ToString()),
                                (myReader["Title"].ToString()),
                                (myReader["LicenseNo"].ToString()),
                                (DateTime)(myReader["IssueDate"]),
                                (DateTime)(myReader["ExpirationDate"]),
                                (DateTime)(myReader["DateOfBirth"]),
                                (myReader["CustomerPhoneNo"].ToString()),
                                (myReader["MobileNo"].ToString()),
                                (myReader["CustomerEmailAddress"].ToString()),
                                ""));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        /// <summary>
        /// Update order status.
        /// </summary>
        public static void UpdateOrderStatus(long orderID, string orderStatus)
        {
            try
            {
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("UPDATE_OrderStatus", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@OrderID", SqlDbType.BigInt).Value = orderID;
                        myCommand.Parameters.Add("@OrderStatus", SqlDbType.VarChar).Value = orderStatus;

                        myConnection.Open();
                        myCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        ///// <summary>
        ///// Inserting new vehicle order.
        ///// </summary>
        //public static void InsertNewVehicleOrder(long vehicleAvailableID, long orderID)
        //{
        //    try
        //    {
        //        using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
        //        {
        //            using (SqlCommand myCommand = new SqlCommand("INSERT_VehicleOrder", myConnection))
        //            {
        //                myCommand.CommandType = CommandType.StoredProcedure;

        //                myCommand.Parameters.Add("@VehicleAvailableID", SqlDbType.BigInt).Value = vehicleAvailableID;
        //                myCommand.Parameters.Add("@OrderID", SqlDbType.BigInt).Value = orderID;

        //                myConnection.Open();
        //                myCommand.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApplicationException(ex.Message);
        //    }
        //}

        ///// <summary>
        ///// Update available vehicles.
        ///// </summary>
        //public static void UpdateAvailableVehicles(long vehicleID, long locationID, long orderID)
        //{
        //    try
        //    {
        //        using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
        //        {
        //            using (SqlCommand myCommand = new SqlCommand("UPDATE_AvailableVehicles", myConnection))
        //            {
        //                myCommand.CommandType = CommandType.StoredProcedure;

        //                myCommand.Parameters.Add("@VehicleID", SqlDbType.BigInt).Value = vehicleID;
        //                myCommand.Parameters.Add("@LocationID", SqlDbType.BigInt).Value = locationID;
        //                myCommand.Parameters.Add("@OrderID", SqlDbType.BigInt).Value = orderID;

        //                myConnection.Open();
        //                myCommand.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApplicationException(ex.Message);
        //    }
        //}

        public static void GetLastAddedOrder(ref long orderID, ref string PayPalPayerID)
        {
            try
            {
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("SELECT_LastInsertedOrder", myConnection))
                    {
                        myConnection.Open();

                        SqlDataReader myReader = null;

                        myCommand.CommandType = CommandType.StoredProcedure;

                        myReader = myCommand.ExecuteReader();
                        while (myReader.Read())
                        {
                            orderID = (long)(myReader["OrderID"]);
                            PayPalPayerID = myReader["PayPalPayerID"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}
