using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarHireDBLibrary
{
    public class PasswordResetRequest
    {
        private long m_RequestID;
        private long m_AccountType;
        private long m_AccountID;
        private string m_UserName;
        private DateTime m_Created;

        public long RequestID
        {
            get { return m_RequestID; }
        }

        public long AccountType
        {
            get { return m_AccountType; }
        }

        public long AccountID
        {
            get { return m_AccountID; }
        }

        public string UserName
        {
            get { return m_UserName; }
        }

        public DateTime Created
        {
            get { return m_Created; }
        }

        /// <summary>
        /// Constructor for PasswordResetRequest.
        /// </summary>
        public PasswordResetRequest(long requestID, long accountType, long accountID, string userName, DateTime created)
        {
            m_RequestID = requestID;
            m_AccountType = accountType;
            m_AccountID = accountID;
            m_UserName = userName;
            m_Created = created;
        }

        /// <summary>
        /// Inserting new password request.
        /// </summary>
        public static void InsertNewRequest(long accountType, long accountID, string userName)
        {
            try
            {
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("INSERT_PasswordResetRequest", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@AccountType", SqlDbType.BigInt).Value = accountType;
                        myCommand.Parameters.Add("@AccountID", SqlDbType.BigInt).Value = accountID;
                        myCommand.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;

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

        //Gets the last request within the last 15 mins otherwise no result is returned
        public static DateTime? GetLastRequestedTime(long accountType, long accountID)
        {
            try
            {
                DateTime? lastRequested = null;

                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("SELECT_PasswordResetTime", myConnection))
                    {
                        myConnection.Open();

                        SqlDataReader myReader = null;

                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@AccountType", SqlDbType.BigInt).Value = accountType;
                        myCommand.Parameters.Add("@AccountID", SqlDbType.BigInt).Value = accountID;

                        myReader = myCommand.ExecuteReader();
                        while (myReader.Read())
                        {
                            lastRequested = Convert.ToDateTime(myReader["Created"]);
                        }
                        return lastRequested;
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
