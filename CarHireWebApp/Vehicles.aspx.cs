using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CarHireDBLibrary;
using System.IO;

namespace CarHireWebApp
{
    /// <summary>
    ///  This class shows vehicles in the database.
    /// </summary>
    public partial class Vehicles : System.Web.UI.Page
    {
        /// <summary>
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                AddHeaderRow();

                LoadManufacturers();
                LoadVehicles();
                LoadSIPPCodes();
                generalErrorLbl.Text = "";
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }            
        }

        /// <summary>
        ///  Load vehicles and put them into a table.
        /// </summary>
        private void LoadVehicles()
        {
            string manufacturer, model, SIPPCodeStr;
            double MPG;
            SIPPCode sizeOfVehicleSIPPCode, noOfDoorsSIPPCode, transmissionAndDriveSIPPCode, fuelAndACSIPPCode;
            TableRow row;
            List<VehicleManager> vehicles;
            List<CompanyManager> companies;
            CompanyManager company;

            vehicles = VehicleManager.GetVehicles().Where(x => x.Active == true).ToList();
            companies = CompanyManager.GetCompanies();

            //Only get cars with 50 MPG or more
            if (ecoFriendlyChk.Checked == true)
            {
                vehicles = vehicles.Where(x => x.MPG >= Variables.ECOFRIENDLY && x.Active == true).ToList();
            }

            if (manufacturerDdl.SelectedValue.Equals("All"))
            {
                //Get all the cars on the list
            }
            else if (modelDdl.SelectedValue.Equals("All"))
            {
                vehicles = vehicles.Where(x => x.Manufacturer == manufacturerDdl.SelectedValue && x.Active == true).ToList();
            }
            else
            {
                if (vehicles.Any(x => x.Manufacturer == manufacturerDdl.SelectedValue && x.Model == modelDdl.SelectedValue))
                {
                    vehicles = vehicles.Where(x => x.Manufacturer == manufacturerDdl.SelectedValue && x.Model == modelDdl.SelectedValue && x.Active == true).ToList();
                }
                //If selected another manufacturer while model of different manufacturer is selected the models are cleared
                else
                {
                    vehicles = vehicles.Where(x => x.Manufacturer == manufacturerDdl.SelectedValue && x.Active == true).ToList();
                }
            }

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

                company = companies.Where(x => x.CompanyID == vehicle.UserID).SingleOrDefault();
                AddCell(row, company.CompanyName);

                VehiclesTbl.Rows.Add(row);  
            }

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

            cell = new TableCell();
            cell.Controls.Add(revealCodeBtn);
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Vehicle Info";
            cell.CssClass = "h3";
            tr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Company";
            cell.CssClass = "h3";
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
        ///  Loads manufacturers.
        /// </summary>
        private void LoadManufacturers()
        {
            List<string> manufacturers;
            //add an empty value as long as there are no values empty values already to avoid duplicates
            if (!manufacturerDdl.Items.Contains(new ListItem("All", "All")))
            {
                manufacturerDdl.Items.Insert(0, new ListItem("All", "All"));
                modelDdl.Items.Insert(0, new ListItem("All", "All"));
            }

            manufacturers = VehicleManager.GetManufacturers();

            foreach (string manufacturer in manufacturers)
            {
                if (!manufacturerDdl.Items.Contains(new ListItem(manufacturer)))
                {
                    manufacturerDdl.Items.Add(manufacturer);
                }
            }

        }

        /// <summary>
        ///  Get header row for SIPP Codes modal.
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
        ///  When a manufacturer is loaded populate models ddl with related models.
        /// </summary>
        protected void ManufacturerDdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<string> models;

                //Clear values and add empty row to models dropdown everytime manufacturer dropdown is changed
                modelDdl.Items.Clear();
                modelDdl.Items.Add("All");

                models = VehicleManager.GetModels(manufacturerDdl.SelectedValue);

                foreach (string model in models)
                {
                    modelDdl.Items.Add(model);
                }
            }
            catch (Exception ex)
            {
                generalErrorLbl.Text = "An error has occured saying: " + ex.Message + " Please contact your system administrator.";
            }
        }
    }
}