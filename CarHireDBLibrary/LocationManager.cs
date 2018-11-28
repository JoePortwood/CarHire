using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CarHireDBLibrary
{
    public class LocationManager
    {
        private long m_LocationID;
        private string m_LocationName;
        private string m_OwnerName;
        private int m_Capacity;
        private string m_AddressLine1;
        private string m_AddressLine2;
        private string m_City;
        private string m_ZipOrPostcode;
        private string m_CountyStateProvince;
        private string m_Country;
        private string m_PhoneNo;
        private string m_EmailAddress;
        private double m_Longitude;
        private double m_Latitude;
        private bool m_Active;
        private long m_UserID;

        public long LocationID
        {
            get { return m_LocationID; }
        }

        public string LocationName
        {
            get { return m_LocationName; }
        }

        public string OwnerName
        {
            get { return m_OwnerName; }
        }

        public int Capacity
        {
            get { return m_Capacity; }
        }

        public string AddressLine1
        {
            get { return m_AddressLine1; }
        }

        public string AddressLine2
        {
            get { return m_AddressLine2; }
        }

        public string City
        {
            get { return m_City; }
        }

        public string ZipOrPostcode
        {
            get { return m_ZipOrPostcode; }
        }

        public string CountyStateProvince
        {
            get { return m_CountyStateProvince; }
        }

        public string Country
        {
            get { return m_Country; }
        }

        public string PhoneNo
        {
            get { return m_PhoneNo; }
        }

        public string EmailAddress
        {
            get { return m_EmailAddress; }
        }

        public double Longitude
        {
            get { return m_Longitude; }
        }

        public double Latitude
        {
            get { return m_Latitude; }
        }

        public bool Active
        {
            get { return m_Active; }
        }

        public long UserID
        {
            get { return m_UserID; }
        }

        public LocationManager()
        {
            m_LocationID = 0;
            m_LocationName = "";
            m_OwnerName = "";
            m_Capacity = 0;
            m_AddressLine1 = "";
            m_AddressLine2 = "";
            m_City = "";
            m_ZipOrPostcode = "";
            m_CountyStateProvince = "";
            m_Country = "";
            m_PhoneNo = "";
            m_EmailAddress = "";
            m_Longitude = 0.0;
            m_Latitude = 0.0;
            m_Active = false;
            m_UserID = 0;
        }

        public LocationManager(long locationID, string locationName, string ownerName, int capacity, string addressLine1, 
            string addressLine2, string city, string zipOrPostcode, 
            string countyStateProvince, string country, string phoneNo, string emailAddress, 
            double longitude, double latitude, bool active, long userID)
        {
            m_LocationID = locationID;
            m_LocationName = locationName;
            m_OwnerName = ownerName;
            m_Capacity = capacity;
            m_AddressLine1 = addressLine1;
            m_AddressLine2 = addressLine2;
            m_City = city;
            m_ZipOrPostcode = zipOrPostcode;
            m_CountyStateProvince = countyStateProvince;
            m_Country = country;
            m_PhoneNo = phoneNo;
            m_EmailAddress = emailAddress;
            m_Longitude = longitude;
            m_Latitude = latitude;
            m_Active = active;
            m_UserID = userID;
        }
        /// <summary>
        /// Gets all locations.
        /// </summary>
        public static List<LocationManager> GetLocations()
        {
            try
            {
                List<LocationManager> locations = new List<LocationManager>();
                LocationManager location;
                string locationName, ownerName, addressLine1, addressLine2, city, zipOrPostcode,
                    countyStateProvince, country, phoneNo, emailAddress;
                long locationID, userID;
                double longitude, latitude;
                int capacity;
                bool active;

                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    myConnection.Open();
                    using (SqlCommand myCommand = new SqlCommand("SELECT_Locations", myConnection))
                    {

                        SqlDataReader myReader = null;

                        myReader = myCommand.ExecuteReader();
                        while (myReader.Read())
                        {
                            locationID = (long)(myReader["LocationID"]);
                            locationName = (myReader["LocationName"].ToString());
                            ownerName = (myReader["OwnerName"].ToString());
                            if (!myReader.IsDBNull(myReader.GetOrdinal("Capacity")))
                            {
                                capacity = (int)(myReader["Capacity"]);
                            }
                            else
                            {
                                capacity = 0;
                            }
                            addressLine1 = (myReader["AddressLine1"].ToString());
                            addressLine2 = (myReader["AddressLine2"].ToString());
                            city = (myReader["City"].ToString());
                            zipOrPostcode = (myReader["ZipOrPostcode"].ToString());
                            countyStateProvince = (myReader["CountyStateProvince"].ToString());
                            country = (myReader["Country"].ToString());
                            phoneNo = (myReader["PhoneNo"].ToString());
                            emailAddress = (myReader["EmailAddress"].ToString());
                            longitude = (double)(myReader["Longitude"]);
                            latitude = (double)(myReader["Latitude"]);
                            active = (bool)(myReader["Active"]);
                            userID = (long)(myReader["UserID"]);

                            location = new LocationManager(locationID, locationName, ownerName, capacity, addressLine1, addressLine2,
                                city, zipOrPostcode, countyStateProvince, country, phoneNo, emailAddress, longitude, latitude, active, userID);
                            locations.Add(location);
                        }
                        return locations;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        //RETURNS ALL RESULTS HERE BUT NOT IN SQL?
        public static List<LocationManager> GetLocationsByUser(long userID)
        {
            try
            {
                List<LocationManager> locations = new List<LocationManager>();
                LocationManager location;
                string locationName, ownerName, addressLine1, addressLine2, city, zipOrPostcode,
                    countyStateProvince, country, phoneNo, emailAddress;
                long locationID;
                double longitude, latitude;
                int capacity;
                bool active;

                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    myConnection.Open();
                    using (SqlCommand myCommand = new SqlCommand("SELECT_Locations", myConnection))
                    {

                        SqlDataReader myReader = null;

                        myCommand.Parameters.Add("@UserID", SqlDbType.BigInt).Value = userID;

                        myReader = myCommand.ExecuteReader();
                        while (myReader.Read())
                        {
                            locationID = (long)(myReader["LocationID"]);
                            locationName = (myReader["LocationName"].ToString());
                            ownerName = (myReader["OwnerName"].ToString());
                            if (!myReader.IsDBNull(myReader.GetOrdinal("Capacity")))
                            {
                                capacity = (int)(myReader["Capacity"]);
                            }
                            else
                            {
                                capacity = 0;
                            }
                            addressLine1 = (myReader["AddressLine1"].ToString());
                            addressLine2 = (myReader["AddressLine2"].ToString());
                            city = (myReader["City"].ToString());
                            zipOrPostcode = (myReader["ZipOrPostcode"].ToString());
                            countyStateProvince = (myReader["CountyStateProvince"].ToString());
                            country = (myReader["Country"].ToString());
                            phoneNo = (myReader["PhoneNo"].ToString());
                            emailAddress = (myReader["EmailAddress"].ToString());
                            longitude = (double)(myReader["Longitude"]);
                            latitude = (double)(myReader["Latitude"]);
                            active = (bool)(myReader["Active"]);

                            location = new LocationManager(locationID, locationName, ownerName, capacity, addressLine1, addressLine2,
                                city, zipOrPostcode, countyStateProvince, country, phoneNo, emailAddress, longitude, latitude, active, userID);
                            locations.Add(location);
                        }
                        return locations;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public static long GetLastAddedLocation()
        {
            try
            {
                long locationID = 0;

                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("SELECT_LastInsertedLocation", myConnection))
                    {
                        myConnection.Open();

                        SqlDataReader myReader = null;

                        myCommand.CommandType = CommandType.StoredProcedure;

                        myReader = myCommand.ExecuteReader();
                        while (myReader.Read())
                        {
                            locationID = (long)(myReader["LocationID"]);
                        }
                        return locationID;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public static void AddNewLocation(string locationName, string ownerName, int? capacity, string addressLine1, string addressLine2,
             string city, string zipOrPostcode, string countyStateProvince, string country,
            string phoneNo, string emailAddress, double longitude, double latitude, long userID, int userType)
        {
            try
            {
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("INSERT_Location", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@LocationName", SqlDbType.VarChar).Value = locationName;
                        myCommand.Parameters.Add("@OwnerName", SqlDbType.VarChar).Value = ownerName;
                        if (capacity != 0)
                        {
                            myCommand.Parameters.Add("@Capacity", SqlDbType.Int).Value = capacity;
                        }
                        if (addressLine1 != "")
                        {
                            myCommand.Parameters.Add("@AddressLine1", SqlDbType.VarChar).Value = addressLine1;
                        }
                        if (addressLine2 != "")
                        {
                            myCommand.Parameters.Add("@AddressLine2", SqlDbType.VarChar).Value = addressLine2;
                        }
                        myCommand.Parameters.Add("@City", SqlDbType.VarChar).Value = city;
                        myCommand.Parameters.Add("@ZipOrPostcode", SqlDbType.VarChar).Value = zipOrPostcode.ToUpper();
                        if (countyStateProvince != "")
                        {
                            myCommand.Parameters.Add("@CountyStateProvince", SqlDbType.VarChar).Value = countyStateProvince;
                        }
                        myCommand.Parameters.Add("@Country", SqlDbType.VarChar).Value = country;
                        myCommand.Parameters.Add("@PhoneNo", SqlDbType.VarChar).Value = phoneNo;
                        myCommand.Parameters.Add("@EmailAddress", SqlDbType.VarChar).Value = emailAddress;
                        myCommand.Parameters.Add("@Longitude", SqlDbType.Float).Value = longitude;
                        myCommand.Parameters.Add("@Latitude", SqlDbType.Float).Value = latitude;
                        myCommand.Parameters.Add("@Active", SqlDbType.Bit).Value = 1;
                        myCommand.Parameters.Add("@UserID", SqlDbType.BigInt).Value = userID;
                        myCommand.Parameters.Add("@UserType", SqlDbType.BigInt).Value = userType;

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

        public static void UpdateLocation(long locationID, string locationName, string ownerName, int? capacity, string addressLine1, 
            string addressLine2, string city, string zipOrPostcode, string countyStateProvince, string country,
            string phoneNo, string emailAddress, double longitude, double latitude, bool active, long userID, int userType)
        {
            try
            {
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("UPDATE_Location", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@LocationID", SqlDbType.BigInt).Value = locationID;
                        myCommand.Parameters.Add("@LocationName", SqlDbType.VarChar).Value = locationName;
                        myCommand.Parameters.Add("@OwnerName", SqlDbType.VarChar).Value = ownerName;
                        if (capacity != 0)
                        {
                            myCommand.Parameters.Add("@Capacity", SqlDbType.Int).Value = capacity;
                        }
                        if (addressLine1 != "")
                        {
                            myCommand.Parameters.Add("@AddressLine1", SqlDbType.VarChar).Value = addressLine1;
                        }
                        if (addressLine2 != "")
                        {
                            myCommand.Parameters.Add("@AddressLine2", SqlDbType.VarChar).Value = addressLine2;
                        }
                        myCommand.Parameters.Add("@City", SqlDbType.VarChar).Value = city;
                        myCommand.Parameters.Add("@ZipOrPostcode", SqlDbType.VarChar).Value = zipOrPostcode.ToUpper();
                        if (countyStateProvince != "")
                        {
                            myCommand.Parameters.Add("@CountyStateProvince", SqlDbType.VarChar).Value = countyStateProvince;
                        }
                        myCommand.Parameters.Add("@Country", SqlDbType.VarChar).Value = country;
                        myCommand.Parameters.Add("@PhoneNo", SqlDbType.VarChar).Value = phoneNo;
                        myCommand.Parameters.Add("@EmailAddress", SqlDbType.VarChar).Value = emailAddress;
                        myCommand.Parameters.Add("@Longitude", SqlDbType.Float).Value = longitude;
                        myCommand.Parameters.Add("@Latitude", SqlDbType.Float).Value = latitude;
                        myCommand.Parameters.Add("@Active", SqlDbType.Bit).Value = active;
                        myCommand.Parameters.Add("@UserID", SqlDbType.BigInt).Value = userID;
                        myCommand.Parameters.Add("@UserType", SqlDbType.BigInt).Value = userType;

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

        public string GetAddressStr()
        {
            string fullAddress = "";

            if (m_AddressLine1 != "")
            {
                fullAddress = fullAddress + AddressLine1;
            }
            if (m_AddressLine2 != "")
            {
                fullAddress = fullAddress + ", " + m_AddressLine2;
            }

            //If no address lines have been entered
            if (fullAddress != "")
            {
                fullAddress = fullAddress + ", " + m_City;
            }
            else
            {
                fullAddress = fullAddress + m_City;
            }

            if (m_CountyStateProvince != "")
            {
                fullAddress = fullAddress + ", " + m_CountyStateProvince;
            }
            fullAddress = fullAddress + ", " + m_ZipOrPostcode + ", " + m_Country;

            return fullAddress;
        }

        public static bool CheckLongitudeOrLatitudeValid(string checkStr)
        {
            decimal value;

            //Only accept 2 or 3 digits before a decimal point then any number of digits after dot e.g. 100.30405
            if (!Regex.IsMatch(checkStr, @"^[-+]?[0-9][0-9]?[0-9]?\.?[0-9]*"))//@"^[-+]?[0-9]*\.?[0-9]+$"))
            {
                return false;
            }
            else if (!decimal.TryParse(checkStr, out value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
