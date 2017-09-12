using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class SearchPatient : System.Web.UI.Page {
    //connection string for database
    string connectionString = WebConfigurationManager.ConnectionStrings["TCDentalSchedulerDB"].ConnectionString;

    //run on page load
    protected void Page_Load(object sender, EventArgs e) {
        //execute on first load
        if(!this.IsPostBack) {
            //populate birthdate dropdown menu
            for(int i = 1; i <= 31; i++) {
                lstDOBDay.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            for(int i = 1; i <= 12; i++) {
                lstDOBMonth.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            int currentYear = System.DateTime.Now.Year;
            for(int i = currentYear; i >= currentYear - 100; i--) {
                lstDOBYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }
    }

    //handler for custom validate birthdate
    protected void vldDob_Validate(object source, ServerValidateEventArgs args) {
        try {
            int Day = int.Parse(lstDOBDay.SelectedValue);
            int Month = int.Parse(lstDOBMonth.SelectedValue);
            int Year = int.Parse(lstDOBYear.SelectedValue);

            DateTime Test = new DateTime(Year, Month, Day);
            //System.Diagnostics.Debug.WriteLine(Test.Date.ToString());
            args.IsValid = true;
        }
        catch(FormatException Arg) {
            // item not selected (couldn't convert int)
            args.IsValid = false;
        }
        catch(ArgumentOutOfRangeException Arg) {
            // invalid date (31 days in February)
            args.IsValid = false;
        }
    }

    //search for patients using fields
    private void searchPatientTable () {
        //Select statement string
        string selectSQL;
        selectSQL = "SELECT * FROM Patient";
        selectSQL += " WHERE (@pid IS NULL OR PatientID = @pid)";
        selectSQL += " AND (@fName IS NULL OR FirstName = @fName)";
        selectSQL += " AND (@lName is NULL OR LastName = @lName)";
        selectSQL += " AND (@sex is NULL OR Sex = @sex)";
        selectSQL += " AND (@year is NULL OR YEAR(BirthDate) = @year)";
        selectSQL += " AND (@month is NULL OR MONTH(BirthDate) = @month)";
        selectSQL += " AND (@day is NULL OR DAY(BirthDate) = @day)";

        //System.Diagnostics.Debug.WriteLine(selectSQL);

        //Define ADO.Net objects
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataReader reader;

        if (string.IsNullOrEmpty(txtPID.Text)) {
            cmd.Parameters.AddWithValue("@pid", DBNull.Value);
            //System.Diagnostics.Debug.WriteLine("PID is null");
        }
        else
            cmd.Parameters.AddWithValue("@pid", Int32.Parse(txtPID.Text));
        
        if (string.IsNullOrEmpty(txtFName.Text))
            cmd.Parameters.AddWithValue("@fName", DBNull.Value);
        else
            cmd.Parameters.AddWithValue("@fName", txtFName.Text);
        
        if (string.IsNullOrEmpty(txtLName.Text))
            cmd.Parameters.AddWithValue("@lName", DBNull.Value);
        else
            cmd.Parameters.AddWithValue("@lName", txtLName.Text);

        if(lstSex.SelectedValue == "invalid")
            cmd.Parameters.AddWithValue("@sex", DBNull.Value);
        else
            cmd.Parameters.AddWithValue("@sex", lstSex.SelectedItem.Value);

        if (lstDOBYear.SelectedValue == "invalid")
            cmd.Parameters.AddWithValue("@year", DBNull.Value);
        else
            cmd.Parameters.AddWithValue("@year", Int32.Parse(lstDOBYear.SelectedItem.Value));

        if (lstDOBMonth.SelectedValue == "invalid")
            cmd.Parameters.AddWithValue("@month", DBNull.Value);
        else
            cmd.Parameters.AddWithValue("@month", Int32.Parse(lstDOBMonth.SelectedItem.Value));

        if (lstDOBDay.SelectedValue == "invalid")
            cmd.Parameters.AddWithValue("@day", DBNull.Value);
        else
            cmd.Parameters.AddWithValue("@day", Int32.Parse(lstDOBDay.SelectedItem.Value));

        //Try to open database and execute SQL query
        try {
            using(con) {
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read()) {
                    TableRow newRow = new TableRow();
                    newRow.CssClass = "cellData";
                    newRow.ID = reader["PatientID"].ToString();
                    tblDisplay.Rows.Add(newRow);

                    TableCell tCell = new TableCell();
                    tCell.CssClass = "cellPID";
                    tCell.Text = reader["PatientID"].ToString().PadLeft(9, '0');
                    newRow.Cells.Add(tCell);

                    tCell = new TableCell();
                    tCell.CssClass = "cellSex";
                    tCell.Text = reader["Sex"].ToString();
                    newRow.Cells.Add(tCell);

                    tCell = new TableCell();
                    tCell.CssClass = "cellFName";
                    tCell.Text = reader["FirstName"].ToString();
                    newRow.Cells.Add(tCell);

                    tCell = new TableCell();
                    tCell.CssClass = "cellLName";
                    tCell.Text = reader["LastName"].ToString();
                    newRow.Cells.Add(tCell);

                    tCell = new TableCell();
                    tCell.CssClass = "cellDOB";
                    DateTime dateOnly = Convert.ToDateTime(reader["BirthDate"].ToString());
                    tCell.Text = dateOnly.ToString("yyyy/MM/dd");
                    newRow.Cells.Add(tCell);

                    tCell = new TableCell();
                    tCell.CssClass = "cellPhone";
                    newRow.Cells.Add(tCell);

                    tCell = new TableCell();
                    tCell.CssClass = "cellEmail";
                    newRow.Cells.Add(tCell);

                    //make row clickable
                    newRow.Attributes.Add("onclick", "rowClick(this.id);");
                }
                reader.Close();
            }
        }
        catch(Exception err) {

        }
    }

    //clear all fields in specified control
    private void clearControl(ControlCollection ctrls) {
        foreach(Control ctrl in ctrls) {
            if(ctrl is TextBox) {
                ((TextBox)ctrl).Text = string.Empty;
            }
            else if(ctrl is DropDownList) {
                ((DropDownList)ctrl).ClearSelection();
                ((DropDownList)ctrl).SelectedIndex = 0;
            }
            else if(ctrl is RadioButtonList) {
                ((RadioButtonList)ctrl).ClearSelection();
                ((RadioButtonList)ctrl).SelectedIndex = 0;
            }

            clearControl(ctrl.Controls);
        }
    }

    //handler for clicking search button
    protected void btnSearch_Click(object sender, EventArgs e) {
        if (Page.IsValid)
            searchPatientTable();
    }

    //handler for clicking reset button 
    protected void btnReset_Click(object sender, EventArgs e) {
        clearControl(Page.Controls);
    }

    //handler for clicking create new button
    protected void btnNew_Click(object sender, EventArgs e) {
        Response.Redirect("~/newpatientprofile.aspx");
    }
    protected void btnView_Click(object sender, EventArgs e) {
        Response.Redirect("~/patientprofile.aspx?patient=" + selectedPatient.Value);
    }
}