using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarHireDBLibrary
{
    //For admin purposes of viewing companies who are hiring cars
    public class CompanyManager
    {
        private long m_CompanyID;
        private string m_UserName;
        private string m_CompanyName;
        private string m_CompanyDescription;
        private string m_LicensingDetails;
        private string m_PhoneNo;
        private string m_EmailAddress;
        private UserAccess m_Access;

        public long CompanyID
        {
            get { return m_CompanyID; }
        }

        public string UserName
        {
            get { return m_UserName; }
        }

        public string CompanyName
        {
            get { return m_CompanyName; }
        }

        public string CompanyDescription
        {
            get { return m_CompanyDescription; }
        }

        public string LicensingDetails
        {
            get { return m_LicensingDetails; }
        }

        public string PhoneNo
        {
            get { return m_PhoneNo; }
        }

        public string EmailAddress
        {
            get { return m_EmailAddress; }
        }

        public UserAccess Access
        {
            get { return m_Access; }
        }

        public CompanyManager(long companyID, string userName, string companyName, string companyDescription, string licensingDetails,
            string phoneNo, string emailAddress, string password)
        {
            m_CompanyID = companyID;
            m_UserName = userName;
            m_CompanyName = companyName;
            m_CompanyDescription = companyDescription;
            m_LicensingDetails = licensingDetails;
            m_PhoneNo = phoneNo;
            m_EmailAddress = emailAddress;
            m_Access = new UserAccess(UserAccess.UserType.company, companyID, password);
        }

        /// <summary>
        /// Gets all companies.
        /// </summary>
        public static List<CompanyManager> GetCompanies()
        {
            try
            {
                List<CompanyManager> companies = new List<CompanyManager>();
                CompanyManager company;
                string userName, companyName, companyDescription, licensingDetails, phoneNo, emailAddress, password;
                long companyID;

                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    myConnection.Open();

                    SqlDataReader myReader = null;

                    SqlCommand myCommand = new SqlCommand("SELECT * FROM v_Companies", myConnection);

                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        companyID = (long)(myReader["CompanyID"]);
                        userName = (myReader["UserName"].ToString());
                        companyName = (myReader["CompanyName"].ToString());
                        companyDescription = (myReader["CompanyDescription"].ToString());
                        licensingDetails = (myReader["LicensingDetails"].ToString());
                        phoneNo = (myReader["PhoneNo"].ToString());
                        emailAddress = (myReader["EmailAddress"].ToString());
                        password = (myReader["Pwd"].ToString());

                        company = new CompanyManager(companyID, userName, companyName, companyDescription, licensingDetails,
                        phoneNo, emailAddress, password);
                        companies.Add(company);
                    }
                    return companies;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        /// <summary>
        /// Gets company by location id
        /// </summary>
        public static string GetCompanyByLocation(long locationID)
        {
            try
            {
                string emailAddress = "";
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("SELECT_CompanyEmailByLocation", myConnection))
                    {
                        myConnection.Open();

                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@LocationID", SqlDbType.BigInt).Value = locationID;

                        SqlDataReader myReader = null;
                        myReader = myCommand.ExecuteReader();
                        while (myReader.Read())
                        {
                            emailAddress = (myReader["EmailAddress"].ToString());
                        }
                        return emailAddress;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public static void AddNewCompany(string userName, string companyName, string companyDescription, string licensingDetails,
            string phoneNo, string emailAddress, string password)
        {
            try
            {
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("INSERT_Company", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;
                        myCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = companyName;
                        myCommand.Parameters.Add("@CompanyDescription", SqlDbType.VarChar).Value = companyDescription;
                        myCommand.Parameters.Add("@LicensingDetails", SqlDbType.VarChar).Value = licensingDetails;
                        myCommand.Parameters.Add("@PhoneNo", SqlDbType.VarChar).Value = phoneNo;
                        myCommand.Parameters.Add("@EmailAddress", SqlDbType.VarChar).Value = emailAddress;
                        myCommand.Parameters.Add("@UserType", SqlDbType.BigInt).Value = UserAccess.UserType.company;
                        myCommand.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;

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

        public static void UpdateCompany(long companyID, string companyName, string companyDescription, string licensingDetails,
            string phoneNo, string emailAddress)
        {
            try
            {
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("UPDATE_Company", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@CompanyID", SqlDbType.BigInt).Value = companyID;
                        myCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = companyName;
                        myCommand.Parameters.Add("@CompanyDescription", SqlDbType.VarChar).Value = companyDescription;
                        myCommand.Parameters.Add("@LicensingDetails", SqlDbType.VarChar).Value = licensingDetails;
                        myCommand.Parameters.Add("@PhoneNo", SqlDbType.VarChar).Value = phoneNo;
                        myCommand.Parameters.Add("@EmailAddress", SqlDbType.VarChar).Value = emailAddress;

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

        public static long GetLastAddedCompany()
        {
            try
            {
                long companyID = 0;

                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("SELECT_LastInsertedCompany", myConnection))
                    {
                        myConnection.Open();

                        SqlDataReader myReader = null;

                        myCommand.CommandType = CommandType.StoredProcedure;

                        myReader = myCommand.ExecuteReader();
                        while (myReader.Read())
                        {
                            companyID = (long)(myReader["CompanyID"]);
                        }
                        return companyID;
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
