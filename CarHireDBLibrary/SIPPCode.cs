using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarHireDBLibrary
{
    public class SIPPCode
    {
        private int m_Type;
        private string m_Letter;
        private string m_Description;

        public int Type
        {
            get { return m_Type; }
        }

        public string Letter
        {
            get { return m_Letter; }
        }

        public string Description
        {
            get { return m_Description; }
        }

        /// <summary>
        /// Constructor for SIPPCode.
        /// </summary>
        public SIPPCode(int type, string letter, string description)
        {
            m_Type = type;
            m_Letter = letter;
            m_Description = description;
        }

        /// <summary>
        /// Get all SIPP Codes stored in database.
        /// </summary>
        public static List<SIPPCode> GetSIPPCodes()
        {
            try
            {
                List<SIPPCode> SIPPCodes = new List<SIPPCode>();
                SIPPCode SIPPCode;
                int type;
                string letter;
                string description;

                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    myConnection.Open();

                    SqlDataReader myReader = null;

                    SqlCommand myCommand = new SqlCommand("SELECT * FROM SIPPCodes", myConnection);

                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        type = (int)(myReader["Type"]);
                        letter = (string)(myReader["Letter"]);
                        description = (string)(myReader["Description"]);

                        SIPPCode = new SIPPCode(type, letter, description);
                        SIPPCodes.Add(SIPPCode);

                    }

                    return SIPPCodes;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No SIPP codes retrieved. " + ex + " .Please contact your system administrator");
            }
        }

        public static SIPPCode GetSIPPCodeDesc(int type, string letter)
        {
            try
            {
                SIPPCode thisSIPPCode = new SIPPCode(0, "", "");
                string description;
                using (SqlConnection myConnection = new SqlConnection(Variables.CONNSTRING))
                {
                    myConnection.Open();

                    using (SqlCommand myCommand = new SqlCommand("SELECT_SIPPCodeByTypeAndLetter", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.Add("@Type", SqlDbType.Int).Value = type;
                        myCommand.Parameters.Add("@Letter", SqlDbType.VarChar).Value = letter;

                        SqlDataReader myReader = null;

                        myReader = myCommand.ExecuteReader();
                        while (myReader.Read())
                        {
                            description = myReader["Description"].ToString();
                            thisSIPPCode = new SIPPCode(type, letter, description);
                        }

                        return thisSIPPCode;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex + " .Please contact your system administrator");
            }

        }

        /// <summary>
        /// Makes sure that the SIPP code is in database.
        /// </summary>
        /// <remarks>
        /// Invididually checks each letter in the SIPP code to check if it exists in the database.
        /// </remarks>
        public static bool CheckSIPPCode(string SIPPCodeStr)
        {
            bool foundLetter = false;
            List<SIPPCode> SIPPCodes;
            SIPPCodes = SIPPCode.GetSIPPCodes();

            if (SIPPCodeStr.Length < 5)
            {
                foreach (SIPPCode code in SIPPCodes)
                {
                    if (code.Type == Variables.SIZEOFVEHICLE && SIPPCodeStr.StartsWith(code.Letter))
                    {
                        foundLetter = true;
                        break;
                    }
                }
                if (foundLetter == false)
                {
                    return false;
                }
                foundLetter = false;

                foreach (SIPPCode code in SIPPCodes)
                {
                    if (code.Type == Variables.NOOFDOORS && SIPPCodeStr[1].ToString().Equals(code.Letter))
                    {
                        foundLetter = true;
                        break;
                    }
                }
                if (foundLetter == false)
                {
                    return false;
                }
                foundLetter = false;

                foreach (SIPPCode code in SIPPCodes)
                {
                    if (code.Type == Variables.TRANSMISSIONANDDRIVE && SIPPCodeStr[2].ToString().Equals(code.Letter))
                    {
                        foundLetter = true;
                        break;
                    }
                }
                if (foundLetter == false)
                {
                    return false;
                }
                foundLetter = false;

                foreach (SIPPCode code in SIPPCodes)
                {
                    if (code.Type == Variables.FUELANDAC && SIPPCodeStr[3].ToString().Equals(code.Letter))
                    {
                        foundLetter = true;
                        break;
                    }
                }
                if (foundLetter == false)
                {
                    return false;
                }

            }
            else
            {
                return false;
            }

            return true;

        }

        public static List<string> GetAllCombinations()
        {
            List<SIPPCode> SIPPCodes, doorsSIPPCode, tranmissionSIPPCode, fuelSIPPCode;
            SIPPCodes = SIPPCode.GetSIPPCodes();
            List<string> SIPPCodesStrs = new List<string>();

            doorsSIPPCode = SIPPCodes.Where(x => x.m_Type == Variables.NOOFDOORS).ToList();
            tranmissionSIPPCode = SIPPCodes.Where(x => x.m_Type == Variables.TRANSMISSIONANDDRIVE).ToList();
            fuelSIPPCode = SIPPCodes.Where(x => x.m_Type == Variables.FUELANDAC).ToList();

            foreach (SIPPCode code in SIPPCodes)
            {
                for (int i = 0; i < doorsSIPPCode.Count(); i++)
                {
                    for (int j = 0; j < tranmissionSIPPCode.Count(); j++)
                    {
                        for (int k = 0; k < fuelSIPPCode.Count(); k++)
                        {
                            SIPPCodesStrs.Add(code.Letter + doorsSIPPCode[i].Letter + tranmissionSIPPCode[j].Letter + fuelSIPPCode[k].Letter
                                + " : " + code.Description + "; " + doorsSIPPCode[i].Description + "; " + 
                                tranmissionSIPPCode[j].Description + "; " + fuelSIPPCode[k].Description);
                        }
                    }
                }
            }
            return SIPPCodesStrs;
        }
    }
}
