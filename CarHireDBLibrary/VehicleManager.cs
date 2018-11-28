using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace CarHireDBLibrary
{
    public class VehicleManager
    {
        private long m_VehicleID;
        private long m_VehicleAvailableID;
        private string m_Manufacturer;
        private string m_Model;
        private string m_SIPPCode;
        private double m_MPG;
        private string m_ImageLoc;
        private int m_TotalVehicles;
        private string m_Currency;
        private double m_BasePrice;
        private bool m_Active;
        private long m_UserID;

        public long VehicleID
        {
            get { return m_VehicleID; }
        }

        public long VehicleAvailableID
        {
            get { return m_VehicleAvailableID; }
        }

        public string Manufacturer
        {
            get { return m_Manufacturer; }
        }

        public string Model
        {
            get { return m_Model; }
        }

        public string SIPPCode
        {
            get { return m_SIPPCode; }
        }

        public double MPG
        {
            get { return m_MPG; }
        }

        public string ImageLoc
        {
            get { return m_ImageLoc; }
        }

        public int TotalVehicles
        {
            get { return m_TotalVehicles; }
        }

        public string Currency
        {
            get { return m_Currency; }
        }

        public double BasePrice
        {
            get { return m_BasePrice; }
        }

        public bool Active
        {
            get { return m_Active; }
        }

        public long UserID
        {
            get { return m_UserID; }
        }

        /// <summary>
        /// Constructor for vehicle.
        /// </summary>
        public VehicleManager(long vehicleID, string manufacturer, string model, string SIPPCodeStr, double MPG, string imageLoc, 
            bool active, long userID)
        {
            m_VehicleID = vehicleID;
            m_Manufacturer = manufacturer;
            m_Model = model;
            m_SIPPCode = SIPPCodeStr;
            m_MPG = MPG;
            m_ImageLoc = imageLoc;
            m_Active = active;
            m_UserID = userID;
        }

        /// <summary>
        /// Constructor for vehicles available.
        /// </summary>
        public VehicleManager(long vehicleAvailableID, long vehicleID, string manufacturer, string model, string SIPPCodeStr, double MPG, string imageLoc,
            int totalVehicles, string currency, double basePrice, bool active, long userID)
        {
            m_VehicleAvailableID = vehicleAvailableID;
            m_VehicleID = vehicleID;
            m_Manufacturer = manufacturer;
            m_Model = model;
            m_SIPPCode = SIPPCodeStr;
            m_MPG = MPG;
            m_ImageLoc = imageLoc;
            m_TotalVehicles = totalVehicles;
            m_Currency = currency;
            m_BasePrice = basePrice;
            m_Active = active;
            m_UserID = userID;
        }

        /// <summary>
        /// Gets all vehicles.
        /// </summary>
        public static List<VehicleManager> GetVehicles()
        {
            try
            {
                List<VehicleManager> vehicles = new List<VehicleManager>();
                VehicleManager vehicle;
                string manufacturer, model, SIPPCodeStr, imageLoc;
                long vehicleID;
                double MPG;
                bool active;
                long userID;

                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    myConnection.Open();
                    using (SqlCommand myCommand = new SqlCommand("SELECT_Vehicles", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        SqlDataReader myReader = null;

                        myReader = myCommand.ExecuteReader();
                        while (myReader.Read())
                        {
                            vehicleID = (long)(myReader["VehicleID"]);
                            manufacturer = (myReader["Manufacturer"].ToString());
                            model = (myReader["Model"].ToString());
                            SIPPCodeStr = (myReader["SIPPCode"].ToString());
                            MPG = (double)(myReader["MPG"]);
                            imageLoc = (myReader["ImageLoc"].ToString());
                            active = (bool)(myReader["Active"]);
                            userID = (long)(myReader["UserID"]);

                            vehicle = new VehicleManager(vehicleID, manufacturer, model, SIPPCodeStr, MPG, imageLoc, active, userID);
                            vehicles.Add(vehicle);
                        }
                        return vehicles;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

        }

        /// <summary>
        /// Gets all models from a specific manufacturer.
        /// </summary>
        public static List<string> GetModels(string manufacturer)
        {
            try
            {
                List<string> models = new List<string>();

                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    myConnection.Open();
                
                        using (SqlCommand myCommand = new SqlCommand("SELECT_Models", myConnection))
                        {
                            myCommand.CommandType = CommandType.StoredProcedure;

                            myCommand.Parameters.Add("@Manufacturer", SqlDbType.VarChar).Value = manufacturer;

                            SqlDataReader myReader = null;

                            myReader = myCommand.ExecuteReader();
                            while (myReader.Read())
                            {
                                models.Add(myReader["Model"].ToString());
                            }

                            return models;
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw new ApplicationException(ex.Message);
                }
            }

        /// <summary>
        /// Get all manufacturers.
        /// </summary>
        public static List<string> GetManufacturers()
        {
            try
            {
                List<string> manufacturers = new List<string>();
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    myConnection.Open();

                    SqlDataReader myReader = null;

                    SqlCommand myCommand = new SqlCommand("SELECT DISTINCT Manufacturer FROM Vehicles WHERE Active = 1", myConnection);

                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        manufacturers.Add(myReader["Manufacturer"].ToString());
                    }
                    return manufacturers;
                    
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        /// <summary>
        /// Inserting new vehicle.
        /// </summary>
        public static void InsertNewVehicle(string manufacturer, string model, string SIPPCodeStr, double mpg, string imageLoc,
            int userID, int userType)
        {
            try
            { 
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("INSERT_Vehicle", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@Manufacturer", SqlDbType.VarChar).Value = manufacturer;
                        myCommand.Parameters.Add("@Model", SqlDbType.VarChar).Value = model;
                        myCommand.Parameters.Add("@SIPPCode", SqlDbType.VarChar).Value = SIPPCodeStr;
                        myCommand.Parameters.Add("@MPG", SqlDbType.Float).Value = mpg;
                        if (imageLoc != null)
                        {
                            myCommand.Parameters.Add("@ImageLoc", SqlDbType.VarChar).Value = imageLoc;
                        }
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

        /// <summary>
        /// Update vehicle.
        /// </summary>
        public static void UpdateVehicle(string vehicleID, string manufacturer, string model, string SIPPCodeStr, double mpg,
            string imageLoc, bool active, int userID, int userType)
        {
            try
            { 
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("UPDATE_Vehicle", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@VehicleID", SqlDbType.VarChar).Value = vehicleID;
                        myCommand.Parameters.Add("@Manufacturer", SqlDbType.VarChar).Value = manufacturer;
                        myCommand.Parameters.Add("@Model", SqlDbType.VarChar).Value = model;
                        myCommand.Parameters.Add("@SIPPCode", SqlDbType.VarChar).Value = SIPPCodeStr;
                        myCommand.Parameters.Add("@MPG", SqlDbType.Float).Value = mpg;
                        if (imageLoc != null)
                        {
                            myCommand.Parameters.Add("@ImageLoc", SqlDbType.VarChar).Value = imageLoc;
                        }
                        myCommand.Parameters.Add("@Active", SqlDbType.BigInt).Value = active;
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

        /// <summary>
        /// Gets all available vehicles in certain location.
        /// </summary>
        public static List<VehicleManager> GetAvailableVehicles(long locationID)
        {
            try
            {
                List<VehicleManager> vehiclesAvailable = new List<VehicleManager>();
                VehicleManager vehicle;
                string manufacturer, model, SIPPCodeStr, imageLoc, currency;
                long vehicleAvailableID, vehicleID, userID;
                double MPG, basePrice;
                int totalVehicles;
                bool active;

                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    myConnection.Open();

                    using (SqlCommand myCommand = new SqlCommand("SELECT_AvailableVehicles", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@LocationID", SqlDbType.VarChar).Value = locationID;

                        SqlDataReader myReader = null;

                        myReader = myCommand.ExecuteReader();
                        while (myReader.Read())
                        {
                            vehicleAvailableID = (long)(myReader["VehiclesAvailableID"]);
                            vehicleID = (long)(myReader["VehicleID"]);
                            manufacturer = (myReader["Manufacturer"].ToString());
                            model = (myReader["Model"].ToString());
                            SIPPCodeStr = (myReader["SIPPCode"].ToString());
                            MPG = (double)(myReader["MPG"]);
                            imageLoc = (myReader["ImageLoc"].ToString());
                            totalVehicles = (int)(myReader["TotalVehicles"]);
                            currency = (myReader["Currency"].ToString());
                            basePrice = (double)(myReader["BasePrice"]);
                            active = (bool)(myReader["Active"]);
                            userID = (long)(myReader["UserID"]);

                            vehicle = new VehicleManager(vehicleAvailableID, vehicleID, manufacturer, model, SIPPCodeStr, MPG, 
                                imageLoc, totalVehicles, currency, basePrice, active, userID);
                            vehiclesAvailable.Add(vehicle);
                        }

                        return vehiclesAvailable;
                    }
                }
            }

            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        /// <summary>
        /// Inserting new available vehicle.
        /// </summary>
        public static void InsertNewAvailableVehicle(long vehicleID, long locationID, int totalVehicles, string currency, 
            double basePrice, int userID, int userType)
        {
            try
            {
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("INSERT_AvailableVehicle", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@VehicleID", SqlDbType.BigInt).Value = vehicleID;
                        myCommand.Parameters.Add("@LocationID", SqlDbType.BigInt).Value = locationID;
                        myCommand.Parameters.Add("@TotalVehicles", SqlDbType.Int).Value = totalVehicles;
                        myCommand.Parameters.Add("@Currency", SqlDbType.VarChar).Value = currency;
                        myCommand.Parameters.Add("@BasePrice", SqlDbType.Float).Value = basePrice;
                        myCommand.Parameters.Add("@UserID", SqlDbType.BigInt).Value = userID;
                        myCommand.Parameters.Add("@UserType", SqlDbType.BigInt).Value = userType;
                        myCommand.Parameters.Add("@Active", SqlDbType.Bit).Value = 1;

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

        /// <summary>
        /// Updating available vehicle.
        /// </summary>
        public static void UpdateAvailableVehicle(long vehicleID, long locationID, int totalVehicles, string currency,
            double basePrice, bool active, int userID, int userType)
        {
            try
            {
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    using (SqlCommand myCommand = new SqlCommand("UPDATE_AvailableVehicle", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@VehicleID", SqlDbType.BigInt).Value = vehicleID;
                        myCommand.Parameters.Add("@LocationID", SqlDbType.BigInt).Value = locationID;
                        myCommand.Parameters.Add("@TotalVehicles", SqlDbType.Int).Value = totalVehicles;
                        myCommand.Parameters.Add("@Currency", SqlDbType.VarChar).Value = currency;
                        myCommand.Parameters.Add("@BasePrice", SqlDbType.Float).Value = basePrice;
                        myCommand.Parameters.Add("@UserID", SqlDbType.BigInt).Value = userID;
                        myCommand.Parameters.Add("@UserType", SqlDbType.BigInt).Value = userType;
                        myCommand.Parameters.Add("@Active", SqlDbType.Bit).Value = active;

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
        ///// Checks to see if the MPG textbox is a decimal number.
        ///// </summary>
        //public static bool CheckMPGValid(string MPGStr)
        //{
        //    decimal value;

        //    if (!Regex.IsMatch(MPGStr, @"[0-9]+(\.[0-9][0-9]?)?"))
        //    {
        //        return false;
        //    }
        //    else if (!decimal.TryParse(MPGStr, out value))
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
    }
}
