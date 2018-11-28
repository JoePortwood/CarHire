using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarHireDBLibrary
{
    public class CustomerManager
    {
        private long m_CustomerID;
        private long m_CompanyID;
        private string m_UserName;
        private string m_Surname;
        private string m_Forename;
        private string m_CompanyName;
        private string m_Title;
        private string m_LicenseNo;
        private DateTime m_IssueDate;
        private DateTime m_ExpirationDate;
        private DateTime m_DateOfBirth;
        private string m_PhoneNo;
        private string m_MobileNo;
        private string m_EmailAddress;
        private UserAccess m_Access;

        public long CustomerID
        {
            get { return m_CustomerID; }
        }

        public long CompanyID
        {
            get { return m_CompanyID; }
        }

        public string UserName
        {
            get { return m_UserName; }
        }

        public string Surname
        {
            get { return m_Surname; }
        }

        public string Forename
        {
            get { return m_Forename; }
        }

        public string CompanyName
        {
            get { return m_CompanyName; }
        }

        public string Title
        {
            get { return m_Title; }
        }

        public string LicenseNo
        {
            get { return m_LicenseNo; }
        }

        public DateTime IssueDate
        {
            get { return m_IssueDate; }
        }

        public DateTime ExpirationDate
        {
            get { return m_ExpirationDate; }
        }

        public DateTime DateOfBirth
        {
            get { return m_DateOfBirth; }
        }

        public string PhoneNo
        {
            get { return m_PhoneNo; }
        }

        public string MobileNo
        {
            get { return m_MobileNo; }
        }

        public string EmailAddress
        {
            get { return m_EmailAddress; }
        }

        public UserAccess Access
        {
            get { return m_Access; }
        }

        public CustomerManager(long customerID, long companyID, string userName, string surname, string forename,
            string companyName, string title, string licenseNo, DateTime issueDate, DateTime expirationDate, DateTime dateOfBirth,
            string phoneNo, string mobileNo, string emailAddress, string password)
        {
            m_CustomerID = customerID;
            m_CompanyID = companyID;
            m_UserName = userName;
            m_Surname = surname;
            m_Forename = forename;
            m_CompanyName = companyName;
            m_Title = title;
            m_LicenseNo = licenseNo;
            m_IssueDate = issueDate;
            m_ExpirationDate = expirationDate;
            m_DateOfBirth = dateOfBirth;
            m_PhoneNo = phoneNo;
            m_MobileNo = mobileNo;
            m_EmailAddress = emailAddress;
            m_Access = new UserAccess(UserAccess.UserType.customer, customerID, password);
        }

        /// <summary>
        /// Gets all customers.
        /// </summary>
        public static List<CustomerManager> GetCustomers()
        {
            try
            {
                List<CustomerManager> customers = new List<CustomerManager>();
                CustomerManager customer;
                string userName, surname, forename, title, licenseNo, companyName, phoneNo, mobileNo, emailAddress, password;
                DateTime issueDate, expirationDate, dateOfBirth;
                long customerID, companyID;

                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    myConnection.Open();

                    SqlDataReader myReader = null;

                    SqlCommand myCommand = new SqlCommand("SELECT * FROM v_Customers", myConnection);

                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        customerID = (long)(myReader["CustomerID"]);
                        //CompanyID column
                        if (!myReader.IsDBNull(myReader.GetOrdinal("CompanyID")))
                        {
                            companyID = (long)(myReader["CompanyID"]);
                            companyName = (myReader["CompanyName"].ToString());
                        }
                        else
                        {
                            companyID = 0;
                            companyName = "";
                        }
                        userName = (myReader["UserName"].ToString());
                        surname = (myReader["Surname"].ToString());
                        forename = (myReader["Forename"].ToString());
                        title = (myReader["Title"].ToString());
                        licenseNo = (myReader["LicenseNo"].ToString());
                        issueDate = (DateTime)(myReader["IssueDate"]);
                        expirationDate = (DateTime)(myReader["ExpirationDate"]);
                        dateOfBirth = (DateTime)(myReader["DateOfBirth"]);
                        phoneNo = (myReader["PhoneNo"].ToString());
                        mobileNo = (myReader["MobileNo"].ToString());
                        emailAddress = (myReader["EmailAddress"].ToString());
                        password = (myReader["Pwd"].ToString());

                        customer = new CustomerManager(customerID, companyID, userName, surname, forename, 
                            companyName, title, licenseNo, issueDate, expirationDate, dateOfBirth, phoneNo, 
                            mobileNo, emailAddress, password);
                        customers.Add(customer);
                    }
                    return customers;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

        }

        public static void AddNewCustomer(long companyID, string userName, string surname, string forename,
            string title, string licenseNo, DateTime issueDate, DateTime expirationDate, DateTime dateOfBirth,
            string phoneNo, string mobileNo, string emailAddress, string password)
        {
            try
            {
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("INSERT_Customer", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        if (companyID != 0)
                        {
                            myCommand.Parameters.Add("@CompanyID", SqlDbType.BigInt).Value = companyID;
                        }
                        myCommand.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;
                        myCommand.Parameters.Add("@Surname", SqlDbType.VarChar).Value = surname;
                        myCommand.Parameters.Add("@Forename", SqlDbType.VarChar).Value = forename;
                        myCommand.Parameters.Add("@Title", SqlDbType.VarChar).Value = title;
                        myCommand.Parameters.Add("@LicenseNo", SqlDbType.VarChar).Value = licenseNo;
                        myCommand.Parameters.Add("@IssueDate", SqlDbType.DateTime).Value = issueDate;
                        myCommand.Parameters.Add("@ExpirationDate", SqlDbType.DateTime).Value = expirationDate;
                        myCommand.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = dateOfBirth;
                        myCommand.Parameters.Add("@PhoneNo", SqlDbType.VarChar).Value = phoneNo;
                        if (mobileNo != "")
                        {
                            myCommand.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = mobileNo;
                        }
                        myCommand.Parameters.Add("@EmailAddress", SqlDbType.VarChar).Value = emailAddress;
                        myCommand.Parameters.Add("@UserType", SqlDbType.BigInt).Value = UserAccess.UserType.customer;
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

        public static void UpdateCustomer(long customerID, long companyID, string surname, string forename,
            string title, string licenseNo, DateTime issueDate, DateTime expirationDate, DateTime dateOfBirth,
            string phoneNo, string mobileNo, string emailAddress)
        {
            try
            {
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("UPDATE_Customer", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@CustomerID", SqlDbType.BigInt).Value = customerID;
                        if (companyID != 0)
                        {
                            myCommand.Parameters.Add("@CompanyID", SqlDbType.BigInt).Value = companyID;
                        }
                        myCommand.Parameters.Add("@Surname", SqlDbType.VarChar).Value = surname;
                        myCommand.Parameters.Add("@Forename", SqlDbType.VarChar).Value = forename;
                        myCommand.Parameters.Add("@Title", SqlDbType.VarChar).Value = title;
                        myCommand.Parameters.Add("@LicenseNo", SqlDbType.VarChar).Value = licenseNo;
                        myCommand.Parameters.Add("@IssueDate", SqlDbType.DateTime).Value = issueDate;
                        myCommand.Parameters.Add("@ExpirationDate", SqlDbType.DateTime).Value = expirationDate;
                        myCommand.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = dateOfBirth;
                        myCommand.Parameters.Add("@PhoneNo", SqlDbType.VarChar).Value = phoneNo;
                        if (mobileNo != "")
                        {
                            myCommand.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = mobileNo;
                        }
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
    }
}
