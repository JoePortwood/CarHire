using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarHireDBLibrary
{
    public class AddressManager
    {
        private long m_AddressID;
        private int m_AddressType;
        private string m_AddressLine1;
        private string m_AddressLine2;
        private string m_City;
        private string m_ZipOrPostcode;
        private string m_CountyStateProvince;
        private string m_Country;

        public long AddressID
        {
            get { return m_AddressID; }
        }

        public int AddressType
        {
            get { return m_AddressType; }
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

        public enum AddressTypeEnum
        {
            customer = 1,
            company = 2,
            location = 4
        }

        public AddressManager()
        {
            m_AddressID = 0;
            m_AddressType = 0;
            m_AddressLine1 = "";
            m_AddressLine2 = "";
            m_City = "";
            m_ZipOrPostcode = "";
            m_CountyStateProvince = "";
            m_Country = "";
        }

        public AddressManager(long addressID, int addressType, string addressLine1, string addressLine2,
            string city, string zipOrPostcode, string countyStateProvince, string country)
        {
            m_AddressID = addressID;
            m_AddressType = addressType;
            m_AddressLine1 = addressLine1;
            m_AddressLine2 = addressLine2;
            m_City = city;
            m_ZipOrPostcode = zipOrPostcode;
            m_CountyStateProvince = countyStateProvince;
            m_Country = country;
        }

        /// <summary>
        /// Gets all addresses.
        /// </summary>
        public static List<AddressManager> GetAddresses()
        {
            try
            {
                List<AddressManager> addresses = new List<AddressManager>();
                AddressManager address;
                long addressID;
                int addressType;
                string addressLine1 = "", addressLine2 = "", city, zipOrPostcode, countyStateProvince = "", country;

                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    myConnection.Open();

                    SqlDataReader myReader = null;

                    SqlCommand myCommand = new SqlCommand("SELECT * FROM Addresses", myConnection);

                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        addressID = (long)(myReader["AddressID"]);
                        addressType = (int)(myReader["AddressType"]);
                        if (!myReader.IsDBNull(myReader.GetOrdinal("Line1")))
                        {
                            addressLine1 = (myReader["Line1"].ToString());
                        }
                        if (!myReader.IsDBNull(myReader.GetOrdinal("Line2")))
                        {
                            addressLine2 = (myReader["Line2"].ToString());
                        }
                        city = (myReader["City"].ToString());
                        zipOrPostcode = (myReader["ZipOrPostcode"].ToString());
                        if (!myReader.IsDBNull(myReader.GetOrdinal("CountyStateProvince")))
                        {
                            countyStateProvince = (myReader["CountyStateProvince"].ToString());
                        }
                        country = (myReader["Country"].ToString());

                        address = new AddressManager(addressID, addressType, addressLine1, addressLine2, city, zipOrPostcode, countyStateProvince, country);
                        addresses.Add(address);
                    }
                    return addresses;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        /// <summary>
        /// Gets all addresses for particular customer.
        /// </summary>
        public static List<AddressManager> GetAddressesByCustomer(long customerID)
        {
            try
            {
                List<AddressManager> addresses = new List<AddressManager>();
                AddressManager address;
                long addressID;
                int addressType;
                string addressLine1 = "", addressLine2 = "", city, zipOrPostcode, countyStateProvince = "", country;

                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    myConnection.Open();
                
                    using (SqlCommand myCommand = new SqlCommand("SELECT_AddressesByCustomer", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@CustomerID", SqlDbType.BigInt).Value = customerID;

                        SqlDataReader myReader = null;

                        myReader = myCommand.ExecuteReader();
                        while (myReader.Read())
                        {
                            addressID = (long)(myReader["AddressID"]);
                            addressType = (int)(myReader["AddressType"]);
                            if (!myReader.IsDBNull(myReader.GetOrdinal("Line1")))
                            {
                                addressLine1 = (myReader["Line1"].ToString());
                            }
                            if (!myReader.IsDBNull(myReader.GetOrdinal("Line2")))
                            {
                                addressLine1 = (myReader["Line2"].ToString());
                            }
                            city = (myReader["City"].ToString());
                            zipOrPostcode = (myReader["ZipOrPostcode"].ToString());
                            if (!myReader.IsDBNull(myReader.GetOrdinal("CountyStateProvince")))
                            {
                                countyStateProvince = (myReader["CountyStateProvince"].ToString());
                            }
                            country = (myReader["Country"].ToString());

                            address = new AddressManager(addressID, addressType, addressLine1, addressLine2, city, zipOrPostcode, countyStateProvince, country);
                            addresses.Add(address);
                        }
                        return addresses;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public static void AddNewAddress(int addressType, string addressLine1, string addressLine2,
            string city, string zipOrPostcode, string countyStateProvince, string country)
        {
            try
            {
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("INSERT_Address", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@AddressType", SqlDbType.BigInt).Value = addressType;
                        if (addressLine1 != "")
                        {
                            myCommand.Parameters.Add("@AddressLine1", SqlDbType.VarChar).Value = addressLine1;
                        }
                        if (addressLine2 != "")
                        {
                            myCommand.Parameters.Add("@AddressLine2", SqlDbType.VarChar).Value = addressLine2;
                        }
                        myCommand.Parameters.Add("@City", SqlDbType.VarChar).Value = city;
                        myCommand.Parameters.Add("@ZipOrPostcode", SqlDbType.VarChar).Value = zipOrPostcode;
                        if (countyStateProvince != "")
                        {
                            myCommand.Parameters.Add("@CountyStateProvince", SqlDbType.VarChar).Value = countyStateProvince;
                        }
                        myCommand.Parameters.Add("@Country", SqlDbType.VarChar).Value = country;

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

        public static long GetLastAddedAddress()
        {
            try
            {
                long addressID = 0;

                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("SELECT_LastInsertedAddress", myConnection))
                    {
                        myConnection.Open();

                        SqlDataReader myReader = null;

                        myCommand.CommandType = CommandType.StoredProcedure;

                        myReader = myCommand.ExecuteReader();
                        while (myReader.Read())
                        {
                            addressID = (long)(myReader["AddressID"]);
                        }
                        return addressID;
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

        public string GetAddressStrWithoutBreaks()
        {
            string fullAddress;
            fullAddress = AddressLine1 + m_AddressLine2 + m_City + m_CountyStateProvince + m_ZipOrPostcode + m_Country;
            return fullAddress;
        }
    }
}
