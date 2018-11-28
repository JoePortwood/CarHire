using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CarHireDBLibrary;
using System.Drawing;
using System.Web.UI.HtmlControls;

namespace CarHireWebApp
{
    /// <summary>
    ///  This class allows for vehicles to be added to the database.
    /// </summary>
    public partial class AddVehicle : System.Web.UI.Page
    {
        /// <summary>
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["LoggedInType"] == null)
                {
                    Response.Redirect(Variables.REDIRECT, false);
                }
                else if (Session["LoggedInType"].ToString() == "")
                {
                    Response.Redirect(Variables.REDIRECT, false);
                }
                else if (Session["LoggedInType"].ToString() == "Customer")
                {
                    Response.Redirect(Variables.REDIRECT, false);
                }
                LoadSIPPCodes();
                pictureErrorLbl.Text = "";
                SIPPCodeFailLbl.Text = "";
                MPGFailLbl.Text = "";
                manufacturerFailLbl.Text = "";
                modelFailLbl.Text = "";
                addCompleteLbl.Text = "";
                generalErrorLbl.Text = "";

                imgViewFile.Width = Variables.STANDARDWIDTH;
                imgViewFile.Height = Variables.STANDARDHEIGHT;
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Checks fields entered are valid then adds vehicle to database.
        /// </summary>
        protected void VehicleAddBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string manufacturer, model, SIPPCodeStr, imageLoc;
                double mpg = 0;
                int userID = 0;

                //If any conditions fail then do not add vehicle
                bool insertVehicle = true;

                imageLoc = null;

                SIPPCodeStr = SIPPCodeTxt.Text.ToUpper();
                manufacturer = manufacturerTxt.Text;
                model = modelTxt.Text;

                #region checkValidity
                insertVehicle = PictureUpload();

                //Upload selected image (check further down if upload failed)
                if (noImageChk.Checked == false)
                {
                    //If no image is selected save null location in database
                    if (imgViewFile.ImageUrl != "")
                    {
                        imageLoc = imgViewFile.ImageUrl;
                    }
                    else
                    {
                        imageLoc = null;
                    }
                }
                //If no file is being uploaded then set there to be no file to look at
                else if (noImageChk.Checked == true)
                {
                    imageLoc = null;
                }

                if (Variables.CheckDecimal(MPGTxt.Text) == false)
                {
                    MPGFailLbl.Text = "MPG - Please enter either a number or decimal number";
                    insertVehicle = false;
                }
                else
                {
                    mpg = Convert.ToDouble(MPGTxt.Text);
                }
                if (SIPPCode.CheckSIPPCode(SIPPCodeStr) == false)
                {
                    SIPPCodeFailLbl.Text = "Please enter a valid 4 digit SIPP code";
                    insertVehicle = false;
                }
                if (model == "")
                {
                    modelFailLbl.Text = "Please enter a model";
                    insertVehicle = false;
                }
                if (manufacturer == "")
                {
                    manufacturerFailLbl.Text = "Please enter a manufacturer";
                    insertVehicle = false;
                }

                userID = Variables.GetUser(Session["UserID"].ToString());
                if (userID == 0)
                {
                    insertVehicle = false;
                    generalErrorLbl.Text = "Not logged in. Please login to continue";
                }
                #endregion

                if (insertVehicle == true)
                {
                    VehicleManager.InsertNewVehicle(manufacturer, model, SIPPCodeStr, mpg, imageLoc, userID, (int)UserAccess.UserType.company);

                    addCompleteLbl.Text = "Car Added Successfully";

                    //Send user to the list of all vehicles.
                    HtmlMeta meta = new HtmlMeta();
                    meta.HttpEquiv = "Refresh";
                    meta.Content = "2;url=" + Variables.URL + "Vehicles";
                    this.Page.Controls.Add(meta);
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Performs checks on picture to make sure it is a jpg and less than 10MB
        /// </summary>
        private bool PictureUpload()
        {
            bool pictureUploaded = true;
            try
            {
                if (fileUpload.PostedFile.FileName != "")
                {
                    if (fileUpload.PostedFile.ContentType == "image/jpeg")
                    {
                        if (fileUpload.PostedFile.ContentLength < 10240000)
                        {
                            fileUpload.PostedFile.SaveAs(MapPath("~/Images/" + fileUpload.Value));
                            imgViewFile.ImageUrl = "~/Images/" + fileUpload.Value;
                        }

                        else
                        {
                            pictureErrorLbl.Text = "Upload status: The file has to be less than 10MB!";
                            pictureUploaded = false;
                        }
                    }
                    else
                    {
                        pictureErrorLbl.Text = "Upload status: Only JPEG files are accepted!";
                        pictureUploaded = false;
                    }
                }
                return pictureUploaded;
            }
            catch (Exception ex)
            {
                pictureErrorLbl.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                pictureUploaded = false;
                return pictureUploaded;
            }

        }

        /// <summary>
        ///  Adds table header for SIPP codes modal.
        /// </summary>
        private void AddHeaderRow()
        {
            TableHeaderRow tr = new TableHeaderRow();

            TableCell cell = new TableCell();
            cell.Text = "First letter";
            cell.Font.Size = 10;
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Size of vehicle";
            cell.Font.Size = 10;
            tr.Cells.Add(cell);
            SIPPCodesSizeOfVehicleTbl.Rows.Add(tr);

            tr = new TableHeaderRow();
            cell = new TableCell();
            cell.Text = "Second letter";
            cell.Font.Size = 10;
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Number of doors";
            cell.Font.Size = 10;
            tr.Cells.Add(cell);
            SIPPCodesNoOfDoorsTbl.Rows.Add(tr);

            tr = new TableHeaderRow();
            cell = new TableCell();
            cell.Text = "Third letter";
            cell.Font.Size = 10;
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Transmission & drive";
            cell.Font.Size = 10;
            tr.Cells.Add(cell);
            SIPPCodesTransmissionAndDriveTbl.Rows.Add(tr);

            tr = new TableHeaderRow();
            cell = new TableCell();
            cell.Text = "Fourth letter";
            cell.Font.Size = 10;
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Fuel & A/C";
            cell.Font.Size = 10;
            tr.Cells.Add(cell);
            SIPPCodesFuelAndAirConTbl.Rows.Add(tr);
        }

        /// <summary>
        ///  Get SIPP Codes for modal.
        /// </summary>
        private void LoadSIPPCodes()
        {

            AddHeaderRow();
            
            List<SIPPCode> SIPPCodes;
            SIPPCodes = SIPPCode.GetSIPPCodes();

            TableRow row;
            TableCell letterCell, descriptionCell;

            foreach (SIPPCode code in SIPPCodes)
            {
                row = new TableRow();
                letterCell = new TableCell();
                descriptionCell = new TableCell();

                letterCell.Text = code.Letter;
                letterCell.Font.Size = 8;
                descriptionCell.Text = code.Description;
                descriptionCell.Font.Size = 8;
                row.Cells.Add(letterCell);
                row.Cells.Add(descriptionCell);

                if (code.Type == Variables.SIZEOFVEHICLE)
                {
                    SIPPCodesSizeOfVehicleTbl.Rows.Add(row);
                }
                else if (code.Type == Variables.NOOFDOORS)
                {
                    SIPPCodesNoOfDoorsTbl.Rows.Add(row);
                }
                else if (code.Type == Variables.TRANSMISSIONANDDRIVE)
                {
                    SIPPCodesTransmissionAndDriveTbl.Rows.Add(row);
                }
                else if (code.Type == Variables.FUELANDAC)
                {
                    SIPPCodesFuelAndAirConTbl.Rows.Add(row);
                }
                else
                {
                    //Code not currently on list
                }
            }
        }
    }
}