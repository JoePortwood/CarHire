using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarHireDBLibrary
{
    public class UserAccess
    {
        private UserType m_Type;
        private long m_TypeID;
        private string m_Password;

        public UserType Type
        {
            get { return m_Type; }
        }

        public long TypeID
        {
            get { return m_TypeID; }
        }

        public string Password
        {
            get { return m_Password; }
        }

        public enum UserType
        {
            admin = 1,
            company = 2,
            customer = 4
        };

        public enum ActionType
        {
            vehicle = 1,
            location = 2,
            vehiclesAvailable = 3
        };

        /// <summary>
        /// Constructor for UserAccess.
        /// </summary>
        public UserAccess(UserType type, long typeID, string password)
        {
            m_Type = type;
            m_TypeID = typeID;
            m_Password = password;
        }

        public static void InsertAccess(long userType, long typeID, string password)
        {
            try
            {
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("INSERT_UserAccess", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@UserType", SqlDbType.BigInt).Value = userType;
                        myCommand.Parameters.Add("@TypeID", SqlDbType.BigInt).Value = typeID;
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

        public static void UpdateAccess(long userType, long typeID, string password)
        {
            try
            {
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("UPDATE_UserAccess", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@UserType", SqlDbType.BigInt).Value = userType;
                        myCommand.Parameters.Add("@TypeID", SqlDbType.BigInt).Value = typeID;
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

        public static string GetCompanyPassword(string userName)
        {
            try
            {
                string password = "";

                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("SELECT_CompanyAccessInfo", myConnection))
                    {
                        myConnection.Open();

                        SqlDataReader myReader = null;

                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;

                        myReader = myCommand.ExecuteReader();
                        while (myReader.Read())
                        {
                            password = myReader["Pwd"].ToString();
                        }
                        return password;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public static long GetCompanyID(string userName)
        {
            try
            {
                long id = 0;

                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("SELECT_CompanyAccessInfo", myConnection))
                    {
                        myConnection.Open();

                        SqlDataReader myReader = null;

                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;

                        myReader = myCommand.ExecuteReader();
                        while (myReader.Read())
                        {
                            id = (long)myReader["TypeID"];
                        }
                        return id;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public static long GetCompanyByEmailAndUserName(string userName, string email)
        {
            try
            {
                long id = 0;

                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("SELECT_CompanyAccessInfo", myConnection))
                    {
                        myConnection.Open();

                        SqlDataReader myReader = null;

                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;
                        myCommand.Parameters.Add("@EmailAddress", SqlDbType.VarChar).Value = email;

                        myReader = myCommand.ExecuteReader();
                        while (myReader.Read())
                        {
                            id = (long)myReader["TypeID"];
                        }
                        return id;
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
