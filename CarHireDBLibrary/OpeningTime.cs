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
    public class OpeningTime
    {
        private long m_LocationID;
        private int m_DayOfWeek;
        private DateTime m_OpenTime;
        private DateTime m_CloseTime;
        private DateTime? m_HolidayOpenTime;
        private DateTime? m_HolidayCloseTime;
        private DateTime m_HolidayStartDate;
        private DateTime m_HolidayEndDate;
        private bool m_Closed;

        public long LocationID
        {
            get { return m_LocationID; }
        }

        public int DayOfWeekNum
        {
            get { return m_DayOfWeek; }
        }

        public string DayOfWeek
        {
            get
            {
                if (m_DayOfWeek == 1)
                    return "Monday";
                else if (m_DayOfWeek == 2)
                    return "Tuesday";
                else if (m_DayOfWeek == 3)
                    return "Wednesday";
                else if (m_DayOfWeek == 4)
                    return "Thursday";
                else if (m_DayOfWeek == 5)
                    return "Friday";
                else if (m_DayOfWeek == 6)
                    return "Saturday";
                else if (m_DayOfWeek == 7)
                    return "Sunday";
                else
                    return "";
                }
        }
        
        public DateTime OpenTime
        {
            get { return m_OpenTime; }
        }

        public DateTime CloseTime
        {
            get { return m_CloseTime; }
        }

        public DateTime HolidayStartDate
        {
            get { return m_HolidayStartDate; }
        }

        public DateTime? HolidayOpenTime
        {
            get { return m_HolidayOpenTime; }
        }

        public DateTime? HolidayCloseTime
        {
            get { return m_HolidayCloseTime; }
        }

        public string HolidayOpenTimeStr
        {
            get { return m_HolidayOpenTime != null ? m_HolidayOpenTime.Value.ToString("HH:mm") : "n/a"; }
        }

        public string HolidayCloseTimeStr
        {
            get { return m_HolidayCloseTime != null ? m_HolidayCloseTime.Value.ToString("HH:mm") : "n/a"; }
        }

        public DateTime HolidayEndDate
        {
            get { return m_HolidayEndDate; }
        }

        public string HolidayStartDateStr
        {
            get
            {
                if (m_HolidayStartDate <= Variables.Next(DateTime.Now.Date, System.DayOfWeek.Sunday) && m_HolidayStartDate >= DateTime.Now.Date)
                {
                    return m_HolidayStartDate.ToString("dd/MM/yyyy");
                }
                else
                {
                    return "";
                }
            }
        }

        public string HolidayEndDateStr
        {
            get { return m_HolidayEndDate.ToString("dd/MM/yyyy"); }
        }

        public string OpenTimeStr
        {
            get { return m_OpenTime.ToString("HH:mm"); }
        }

        public string CloseTimeStr
        {
            get { return m_CloseTime.ToString("HH:mm"); }
        }

        public bool Closed
        {
            get { return m_Closed; }
        }

        //public enum DaysOfWeek
        //{
        //    Monday = 1,
        //    Tuesday = 2,
        //    Wednesday = 3,
        //    Thursday = 4,
        //    Friday = 5,
        //    Saturday = 6,
        //    Sunday = 7
        //}

        public OpeningTime(long locationID, int dayOfWeek, DateTime openingTime, DateTime closingTime, bool closed)
        {
            m_LocationID = locationID;
            m_DayOfWeek = dayOfWeek;
            m_OpenTime = openingTime;
            m_CloseTime = closingTime;
            m_Closed = closed;
        }

        //For holiday times
        public OpeningTime(long locationID, DateTime holidayStartDate, DateTime? openingTime, DateTime? closingTime, bool closed)
        {
            m_LocationID = locationID;
            m_HolidayStartDate = holidayStartDate;
            m_DayOfWeek = (int)holidayStartDate.DayOfWeek;
            m_HolidayOpenTime = openingTime;
            m_HolidayCloseTime = closingTime;
            m_Closed = closed;
        }

        public static List<OpeningTime> GetOpeningTimesByLocationID(long locationID)
        {            
            DateTime openTime, closeTime;
            int dayOfWeek;
            bool closed;
            try
            {
                List<OpeningTime> openingTimes = new List<OpeningTime>();
                OpeningTime openingTime;

                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    myConnection.Open();

                    using (SqlCommand myCommand = new SqlCommand("SELECT_OpeningTimesByLocation", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@LocationID", SqlDbType.VarChar).Value = locationID;

                        SqlDataReader myReader = null;

                        myReader = myCommand.ExecuteReader();
                        while (myReader.Read())
                        {
                            dayOfWeek = (int)myReader["DayOfTheWeek"];
                            openTime = (DateTime)myReader["OpenTime"];
                            closeTime = (DateTime)myReader["CloseTime"];
                            closed = (bool)myReader["Closed"];
                            openingTime = new OpeningTime(locationID, dayOfWeek, openTime, closeTime, closed);
                            openingTimes.Add(openingTime);
                        }
                        return openingTimes;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public static List<OpeningTime> GetOpeningTimes()
        {
            long locationID;
            DateTime openTime, closeTime;
            int dayOfWeek;
            bool closed;
            try
            {
                List<OpeningTime> openingTimes = new List<OpeningTime>();
                OpeningTime openingTime;

                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    myConnection.Open();

                    SqlDataReader myReader = null;

                    SqlCommand myCommand = new SqlCommand("SELECT * FROM OpeningTimes", myConnection);

                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        locationID = (long)myReader["LocationID"];
                        dayOfWeek = (int)myReader["DayOfTheWeek"];
                        openTime = (DateTime)myReader["OpenTime"];
                        closeTime = (DateTime)myReader["CloseTime"];
                        closed = (bool)myReader["Closed"];
                        openingTime = new OpeningTime(locationID, dayOfWeek, openTime, closeTime, closed);
                        openingTimes.Add(openingTime);
                    }
                    return openingTimes;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public static List<OpeningTime> GetHolidayOpeningTimes()
        {
            long locationID;
            DateTime holidayStartDate;
            DateTime? altOpenTime = null, altCloseTime = null;
            bool closed;
            try
            {
                List<OpeningTime> openingTimes = new List<OpeningTime>();
                OpeningTime openingTime;

                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    myConnection.Open();

                    SqlDataReader myReader = null;

                    SqlCommand myCommand = new SqlCommand("SELECT * FROM HolidayOpeningTimes", myConnection);

                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        locationID = (long)myReader["LocationID"];
                        holidayStartDate = (DateTime)myReader["HolidayStartDate"];
                        altOpenTime = Variables.GetNullableDateTime(myReader, "AltOpenTime");
                        altCloseTime = Variables.GetNullableDateTime(myReader, "AltCloseTime");
                        //if (myReader["AltOpenTime"] != null)
                        //{
                        //    altOpenTime = (DateTime?)myReader["AltOpenTime"];
                        //}
                        //if (myReader["AltCloseTime"] != null)
                        //{
                        //    altCloseTime = (DateTime?)myReader["AltCloseTime"];
                        //}
                        closed = (bool)myReader["Closed"];
                        openingTime = new OpeningTime(locationID, holidayStartDate, altOpenTime, altCloseTime, closed);
                        openingTimes.Add(openingTime);
                    }
                    return openingTimes;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public static List<OpeningTime> GetHolidayOpeningTimesByLocationID(long locationID)
        {
            DateTime holidayStartDate;
            DateTime? altOpenTime = null, altCloseTime = null;
            bool closed;
            try
            {
                List<OpeningTime> openingTimes = new List<OpeningTime>();
                OpeningTime openingTime;

                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    myConnection.Open();

                    using (SqlCommand myCommand = new SqlCommand("SELECT_HolidayOpeningTimesByLocation", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@LocationID", SqlDbType.VarChar).Value = locationID;

                        SqlDataReader myReader = null;

                        myReader = myCommand.ExecuteReader();
                        while (myReader.Read())
                        {
                            //locationID = (long)myReader["LocationID"];
                            holidayStartDate = (DateTime)myReader["HolidayStartDate"];
                            altOpenTime = Variables.GetNullableDateTime(myReader, "AltOpenTime");
                            altCloseTime = Variables.GetNullableDateTime(myReader, "AltCloseTime");
                            //sqlDateTime = myReader["AltOpenTime"];
                            //altOpenTime = (sqlDateTime == System.DBNull.Value)
                            //    ? (DateTime?)null
                            //    : Convert.ToDateTime(sqlDateTime);
                            //sqlDateTime = myReader["AltCloseTime"];
                            //altCloseTime = (sqlDateTime == System.DBNull.Value)
                            //    ? (DateTime?)null
                            //    : Convert.ToDateTime(sqlDateTime);
                            closed = (bool)myReader["Closed"];
                            openingTime = new OpeningTime(locationID, holidayStartDate, altOpenTime, altCloseTime, closed);
                            openingTimes.Add(openingTime);
                        }
                        return openingTimes;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        //Inserts opening times will all closed values
        public static void InsertDefaultOpeningTimes(long locationID)
        {
            try
            {
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    myConnection.Open();
                    for (int dayOfWeek = 1; dayOfWeek <= 7; dayOfWeek++)
                    {
                        using (SqlCommand myCommand = new SqlCommand("INSERT_DefaultOpeningTime", myConnection))
                        {
                            myCommand.CommandType = CommandType.StoredProcedure;
                        
                            myCommand.Parameters.Add("@LocationID", SqlDbType.BigInt).Value = locationID;
                            myCommand.Parameters.Add("@DayOfTheWeek", SqlDbType.Int).Value = dayOfWeek;
                            
                            myCommand.ExecuteNonQuery();
                        
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

        }

        public static void UpdateOpeningTimes(long locationID, int dayOfWeek, DateTime openTime, DateTime closeTime, bool closed)
        {
            try
            {
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    myConnection.Open();

                    using (SqlCommand myCommand = new SqlCommand("UPDATE_OpeningTime", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@LocationID", SqlDbType.BigInt).Value = locationID;
                        myCommand.Parameters.Add("@DayOfTheWeek", SqlDbType.Int).Value = dayOfWeek;
                        myCommand.Parameters.Add("@OpenTime", SqlDbType.DateTime).Value = openTime;
                        myCommand.Parameters.Add("@CloseTime", SqlDbType.DateTime).Value = closeTime;
                        myCommand.Parameters.Add("@Closed", SqlDbType.Bit).Value = closed;

                        myCommand.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public static void InsertHolidayOpeningTimes(long locationID, DateTime holidayStartDate, DateTime? altOpenTime, DateTime? altCloseTime, bool closed)
        {
            try
            {
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    myConnection.Open();
                    using (SqlCommand myCommand = new SqlCommand("INSERT_HolidayOpeningTime", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@LocationID", SqlDbType.BigInt).Value = locationID;
                        myCommand.Parameters.Add("@HolidayStartDate", SqlDbType.DateTime).Value = holidayStartDate;
                        if (altOpenTime != null)
                        {
                            myCommand.Parameters.Add("@AltOpenTime", SqlDbType.DateTime).Value = altOpenTime;
                        }
                        if (altCloseTime != null)
                        {
                            myCommand.Parameters.Add("@AltCloseTime", SqlDbType.DateTime).Value = altCloseTime;
                        }
                        myCommand.Parameters.Add("@Closed", SqlDbType.Bit).Value = closed;

                        myCommand.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

        }

        public static void DeleteHolidayOpeningTime(long locationID, DateTime holidayStartDate)
        {
            try
            {
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    myConnection.Open();
                    using (SqlCommand myCommand = new SqlCommand("DELETE_HolidayOpeningTime", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@LocationID", SqlDbType.BigInt).Value = locationID;
                        myCommand.Parameters.Add("@HolidayStartDate", SqlDbType.DateTime).Value = holidayStartDate;

                        myCommand.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

        }

        public static bool CheckTimeValid(string checkStr)
        {
            DateTime value;

            //Only accept times in am/pm format
            if (!Regex.IsMatch(checkStr, @"^(0?[1-9]|1[0-2]):[0-5][0-9]\s[ap]m$", RegexOptions.IgnoreCase))//@"^[-+]?[0-9]*\.?[0-9]+$"))
            {
                return false;
            }
            else if (!DateTime.TryParse(checkStr, out value))
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
