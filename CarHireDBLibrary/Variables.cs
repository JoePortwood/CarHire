using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarHireDBLibrary
{
    /// <summary>
    /// Global variables.
    /// </summary>
    public static class Variables
    {
        public const string CONNSTRING = "Data Source='LAPTOP-1JSJ8FOU\\SQLEXPRESS';Initial Catalog=CarHire;Integrated Security=True";
        public const string ENCRYPTIONPASS = "Sa84GPS)#.}bx's";
        public const string REDIRECT = "~/Account/InformUser.aspx?InfoString=Unauthorised+access.+Please+login+to+an+account+with+correct+privileges.";
        public const string URL = "http://localhost:2443/";
        
        //Days Of The Week
        public const int MONDAY = 1;
        public const int TUESDAY = 2;
        public const int WEDNESDAY = 3;
        public const int THURSDAY = 4;
        public const int FRIDAY = 5;
        public const int SATURDAY = 6;
        public const int SUNDAY = 7;

        //SIPP Codes
        public const int SIZEOFVEHICLE = 1;
        public const int NOOFDOORS = 2;
        public const int TRANSMISSIONANDDRIVE = 3;
        public const int FUELANDAC = 4;

        //Standard size for images
        public const int STANDARDWIDTH = 270;
        public const int STANDARDHEIGHT = 150;

        //EcoFriendly in miles
        public const int ECOFRIENDLY = 50;

        //Returns next occurance of day specified
        public static DateTime Next(this DateTime from, DayOfWeek dayOfWeek)
        {
            int start = (int)from.DayOfWeek;
            int target = (int)dayOfWeek;
            if (target <= start)
                target += 7;
            return from.AddDays(target - start);
        }

        public static DateTime? GetNullableDateTime(this SqlDataReader reader, string name)
        {
            var col = reader.GetOrdinal(name);
            return reader.IsDBNull(col) ?
                        (DateTime?)null :
                        (DateTime?)reader.GetDateTime(col);
        }

        //Case insenitive contains
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        public static bool CheckAlphaNumericCharacters(string checkStr)
        {
            //Only allow alphanumeric characters
            if (!Regex.IsMatch(checkStr, "^[a-zA-Z0-9- ]*$"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool CheckAlphabetCharacters(string checkStr)
        {
            //Only allow alphabet characters
            if (!Regex.IsMatch(checkStr, "^[a-zA-Z ]*$"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Checks to see if the string is a decimal number to 2 decimal places.
        /// </summary>
        public static bool CheckDecimal(string checkStr)
        {
            decimal value;

            if (!Regex.IsMatch(checkStr, @"^[0-9]+(\.[0-9][0-9]?)?"))
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

        public static bool CheckPasswordValid(string checkStr)
        {
            //Only allow alphanumeric characters
            if (!Regex.IsMatch(checkStr, @"(?=^.{6,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void Email(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("portwoodcarhireassignment@gmail.com");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("portwoodcarhireassignment@gmail.com", "lolmate1");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        public static string GetCountryByCode(string countryCode)
        {
            string country = "";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    myConnection.Open();

                    using (SqlCommand myCommand = new SqlCommand("SELECT_CountryByCode", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@CountryCode", SqlDbType.VarChar).Value = countryCode;

                        SqlDataReader myReader = null;

                        myReader = myCommand.ExecuteReader();
                        while (myReader.Read())
                        {
                            country = myReader["Country"].ToString();
                        }
                        return country;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public static string GetCodeByCountry(string country)
        {
            string countryCode = "";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    myConnection.Open();

                    using (SqlCommand myCommand = new SqlCommand("SELECT_CodeByCountry", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@Country", SqlDbType.VarChar).Value = country;

                        SqlDataReader myReader = null;

                        myReader = myCommand.ExecuteReader();
                        while (myReader.Read())
                        {
                            countryCode = myReader["CountryCode"].ToString();
                        }
                        return countryCode;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        //Gets the user id from session variable
        public static int GetUser(string userIDSessionStr)
        {
            int userID = 0;
            if (userIDSessionStr != null)
            {
                userID = Convert.ToInt32(userIDSessionStr);
            }
            return userID;
        }
    }
}
