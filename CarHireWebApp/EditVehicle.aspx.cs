using CarHireDBLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarHireWebApp
{
    /// <summary>
    ///  This class allows for vehicles already in database to be edited.
    /// </summary>
    public partial class EditVehicle : System.Web.UI.Page
    {
        public List<string> SIPPCodes;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SIPPCodes = SIPPCode.GetAllCombinations();
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
                LoadManufacturers();
                LoadSIPPCodes();
                LoadVehicles();

                pictureErrorLbl.Text = "";
                SIPPCodeFailLbl.Text = "";
                MPGFailLbl.Text = "";
                manufacturerFailLbl.Text = "";
                modelFailLbl.Text = "";
                updateCompleteLbl.Text = "";
                generalErrorLbl.Text = "";
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Sets the user input update fields to visible.
        /// </summary>
        private void UpdateInputVisible()
        {
            pictureLbl.Visible = true;
            fileUpload.Visible = true;
            noImageChk.Visible = true;
            imgViewFile.Visible = true;
            vehicleDetailsLbl.Visible = true;
            manufacturerLbl.Visible = true;
            manufacturerTxt.Visible = true;
            modelLbl.Visible = true;
            modelTxt.Visible = true;
            SIPPCodeLbl.Visible = true;
            revealCodeBtn.Visible = true;
            SIPPCodeTxt.Visible = true;
            MPGLbl.Visible = true;
            MPGTxt.Visible = true;
            editVehicleBtn.Visible = true;
            activeChk.Visible = true;
        }

        /// <summary>
        ///  Adds table header for vehicle table.
        /// </summary>
        private void AddHeaderRow()
        {
            TableHeaderRow tr = new TableHeaderRow();

            TableCell cell = new TableCell();
            cell.Text = "Image";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Model";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            revealCodeBtnTbl.Visible = true;
            cell = new TableCell();
            cell.Controls.Add(revealCodeBtnTbl);
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Car Info";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            VehiclesTbl.Rows.Add(tr);

            cell = new TableCell();
            cell.Text = "Select";
            cell.CssClass = "h4";
            tr.Cells.Add(cell);
            VehiclesTbl.Rows.Add(tr);
        }

        /// <summary>
        ///  Adds cells with text to vehicle table.
        /// </summary>
        private void AddCell(TableRow row, string value)
        {
            TableCell cell = new TableCell();
            cell.Text = value;

            row.Cells.Add(cell);

            VehiclesTbl.Rows.Add(row);
        }

        /// <summary>
        ///  Adds header row to SIPP Codes modal.
        /// </summary>
        private void AddModalHeaderRow()
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
            AddModalHeaderRow();

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

        /// <summary>
        ///  Load vehicles and put into table but make table invisible until a manufacturer and model are selected.
        /// </summary>
        private void LoadVehicles()
        {
            string manufacturer, model, SIPPCodeStr;
            double MPG;
            SIPPCode sizeOfVehicleSIPPCode, noOfDoorsSIPPCode, transmissionAndDriveSIPPCode, fuelAndACSIPPCode;
            TableRow row;
            List<VehicleManager> vehicles;

            vehicles = VehicleManager.GetVehicles().Where(x => x.UserID == Variables.GetUser(Session["UserID"].ToString())).ToList();

            VehiclesTbl.Visible = false;

            //Only populate table when model and manufacturer selected
            if (vehicles.Any(x => x.Manufacturer == manufacturerDdl.SelectedValue && x.Model == modelDdl.SelectedValue))
            {
                vehicles = vehicles.Where(x => x.Manufacturer == manufacturerDdl.SelectedValue && x.Model == modelDdl.SelectedValue).ToList();
                AddHeaderRow();
                //Only make table visible when a model is selected
                //The car table always needs to exist otherwise update button from the table to text boxes WILL NOT WORK
                VehiclesTbl.Visible = true;

                foreach (VehicleManager vehicle in vehicles)
                {
                    row = new TableRow();
                    Image vehicleImg = new Image();

                    TableCell cell = new TableCell();

                    vehicleImg.ImageUrl = vehicle.ImageLoc;
                    vehicleImg.AlternateText = vehicle.Manufacturer + " " + vehicle.Model;

                    //make standard size of car images 270x150
                    vehicleImg.Width = Variables.STANDARDWIDTH;
                    vehicleImg.Height = Variables.STANDARDHEIGHT;

                    cell.Controls.Add(vehicleImg);

                    row.Cells.Add(cell);
                    VehiclesTbl.Rows.Add(row);

                    manufacturer = vehicle.Manufacturer;
                    model = vehicle.Model;
                    AddCell(row, manufacturer + " " + model);

                    SIPPCodeStr = vehicle.SIPPCode;
                    AddCell(row, SIPPCodeStr);

                    MPG = vehicle.MPG;

                    //Gets SIPP Code descriptions for all letters of SIPP code for this car
                    sizeOfVehicleSIPPCode = SIPPCode.GetSIPPCodeDesc(Variables.SIZEOFVEHICLE, vehicle.SIPPCode[0].ToString());
                    noOfDoorsSIPPCode = SIPPCode.GetSIPPCodeDesc(Variables.NOOFDOORS, vehicle.SIPPCode[1].ToString());
                    transmissionAndDriveSIPPCode = SIPPCode.GetSIPPCodeDesc(Variables.TRANSMISSIONANDDRIVE, vehicle.SIPPCode[2].ToString());
                    fuelAndACSIPPCode = SIPPCode.GetSIPPCodeDesc(Variables.FUELANDAC, vehicle.SIPPCode[3].ToString());

                    cell = new TableCell();
                    Label label = new Label();
                    label.Text = "MPG: " + MPG.ToString() + "<br />";
                    cell.Controls.Add(label);

                    label = new Label();
                    label.Text = sizeOfVehicleSIPPCode.Description + "<br />";
                    cell.Controls.Add(label);

                    label = new Label();
                    label.Text = noOfDoorsSIPPCode.Description + "<br />";
                    cell.Controls.Add(label);

                    label = new Label();
                    label.Text = transmissionAndDriveSIPPCode.Description + "<br />";
                    cell.Controls.Add(label);

                    label = new Label();
                    label.Text = fuelAndACSIPPCode.Description + "<br />";
                    cell.Controls.Add(label);

                    row.Cells.Add(cell);

                    VehiclesTbl.Rows.Add(row);

                    //Add an update button to each vehicle
                    Button updateBtn = new Button();
                    updateBtn.Click += new EventHandler(SelectToUpdateBtn_Click);
                    updateBtn.Text = "Select";
                    updateBtn.ID = vehicle.VehicleID.ToString() + vehicle.Model;
                    updateBtn.CssClass = "btn btn-primary";

                    cell = new TableCell();
                    cell.Controls.Add(updateBtn);
                    cell.CssClass = "h4";
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.ID = vehicle.VehicleID.ToString();
                    cell.Visible = false;
                    row.Cells.Add(cell);

                    VehiclesTbl.Rows.Add(row);
                }

            }
            //If selected another manufacturer while model of different manufacturer is selected the models are cleared
            else
            {
                vehicles = vehicles.Where(x => x.Manufacturer == manufacturerDdl.SelectedValue).ToList();
            }
        }

        /// <summary>
        ///  When vehicle to update button is selected.
        /// </summary>
        /// <remarks>
        /// Sessions variables of the car id and image location are saved as they are needed later.
        /// The car id is checked so if there are duplicate manufacturers and models the correct car is selected.
        /// The image location is saved if the image is going to be kept the same for the update.
        /// </remarks>
        protected void SelectToUpdateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateInputVisible();

                List<VehicleManager> vehicles = new List<VehicleManager>();

                vehicles = VehicleManager.GetVehicles().Where(x => x.UserID == Variables.GetUser(Session["UserID"].ToString())).ToList();

                //Find the ID of the button clicked
                Button updateBtn = (Button)sender;

                //Only gets numbers at the start of the button ID
                string btnID = Regex.Match(updateBtn.ID, @"\d+").Value;

                //Only get vehicles that are equal to the manufacturer and model
                if (vehicles.Any(x => x.Manufacturer == manufacturerDdl.SelectedValue && x.Model == modelDdl.SelectedValue))
                {
                    vehicles = vehicles.Where(x => x.Manufacturer == manufacturerDdl.SelectedValue && x.Model == modelDdl.SelectedValue).ToList();

                    foreach (VehicleManager vehicle in vehicles)
                    {
                        //Get ID of each cell and check the cell ID is the same as the start of the button ID
                        TableCell IDCell = (TableCell)VehiclesTbl.FindControl(vehicle.VehicleID.ToString());
                        if (btnID.ToString() == IDCell.ID)
                        {
                            manufacturerTxt.Text = vehicle.Manufacturer;
                            modelTxt.Text = vehicle.Model;
                            SIPPCodeTxt.Text = vehicle.SIPPCode;
                            MPGTxt.Text = vehicle.MPG.ToString();
                            imgViewFile.ImageUrl = vehicle.ImageLoc;
                            vehicleIDTxt.Text = vehicle.VehicleID.ToString();
                            activeChk.Checked = vehicle.Active;

                            //Get session variables for vehicleID and the ImageUrl for checking
                            //what variables are going to be updated
                            Session["VehicleID"] = vehicle.VehicleID.ToString();
                            Session["OldImageURL"] = vehicle.ImageLoc;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }

        /// <summary>
        ///  Checks fields entered are valid then updates vehicle in database.
        /// </summary>
        protected void VehicleUpdateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateInputVisible();

                string manufacturer, model, SIPPCodeStr, imageLoc, vehicleID;
                double mpg = 0;
                int userID = 0;
                bool active;

                //If any conditions fail then do not update vehicle
                bool updateCar = true;

                SIPPCodeStr = SIPPCodeTxt.Text.ToUpper();

                updateCar = PictureUpload();

                manufacturer = manufacturerTxt.Text;
                model = modelTxt.Text;
                vehicleID = (string)Session["VehicleID"];

                //If no image is selected and a file is going to be uploaded then
                //use the image currently in use for that vehicle
                if (imgViewFile.ImageUrl == "" && noImageChk.Checked == false)
                {
                    if ((string)Session["OldImageURL"] != "")
                    {
                        imageLoc = (string)Session["OldImageURL"];
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
                //Otherwise use image selected for vehicle
                else
                {
                    imageLoc = imgViewFile.ImageUrl;
                }


                if (Variables.CheckDecimal(MPGTxt.Text) == false)
                {
                    MPGFailLbl.Text = "MPG - Please enter either a number or decimal number";
                    updateCar = false;
                }
                else
                {
                    mpg = Convert.ToDouble(MPGTxt.Text);
                }
                if (SIPPCode.CheckSIPPCode(SIPPCodeStr) == false)
                {
                    SIPPCodeFailLbl.Text = "Please enter a valid 4 digit SIPP code";
                    updateCar = false;
                }
                if (model == "")
                {
                    modelFailLbl.Text = "Please enter a model";
                    updateCar = false;
                }
                if (manufacturer == "")
                {
                    manufacturerFailLbl.Text = "Please enter a manufacturer";
                    updateCar = false;
                }

                active = activeChk.Checked;

                userID = Variables.GetUser(Session["UserID"].ToString());
                if (userID == 0)
                {
                    updateCar = false;
                    generalErrorLbl.Text = "Not logged in. Please login to continue";
                }

                if (updateCar == true)
                {
                    VehicleManager.UpdateVehicle(vehicleID, manufacturer, model, SIPPCodeStr, mpg, imageLoc, active, userID, (int)UserAccess.UserType.company);

                    VehiclesTbl.Visible = false;

                    updateCompleteLbl.Text = "Car Updated Successfully";
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
        ///  Loads manufacturers.
        /// </summary>
        private void LoadManufacturers()
        {
            List<VehicleManager> vehicles;
            //add an empty value as long as there are no values empty values already to avoid duplicates
            if (!manufacturerDdl.Items.Contains(new ListItem("")))
            {
                manufacturerDdl.Items.Add("");
                modelDdl.Items.Add("");
            }

            //Only get vehicles the logged in user has created
            vehicles = VehicleManager.GetVehicles().Where(x => x.UserID == Variables.GetUser(Session["UserID"].ToString()) && x.Active == true).ToList();

            foreach (VehicleManager vehicle in vehicles)
            {
                if (!manufacturerDdl.Items.Contains(new ListItem(vehicle.Manufacturer)))
                {
                    manufacturerDdl.Items.Add(vehicle.Manufacturer);
                }
            }
        }

        /// <summary>
        ///  When a manufacturer is loaded populate models ddl with related models.
        /// </summary>
        protected void ManufacturerDdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<VehicleManager> vehicles;

                //Clear values and add empty row to models dropdown everytime manufacturer dropdown is changed.
                modelDdl.Items.Clear();
                modelDdl.Items.Add("");

                //Only load models if manufacturer is selected.
                if (manufacturerDdl.SelectedValue != "")
                {
                    //Only get vehicles the logged in user has created
                    vehicles = VehicleManager.GetVehicles().Where(x => x.UserID == Variables.GetUser(Session["UserID"].ToString()) && x.Active == true && x.Manufacturer == manufacturerDdl.SelectedValue).ToList();

                    foreach (VehicleManager vehicle in vehicles)
                    {
                        modelDdl.Items.Add(vehicle.Model);
                    }
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }
    }
}