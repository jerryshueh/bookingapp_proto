using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class PatientProfile : System.Web.UI.Page {
    //connection string for database
    string connectionString = WebConfigurationManager.ConnectionStrings["TCDentalSchedulerDB"].ConnectionString;

    //Profile properties
    string patientID;

    //run on page load
    protected void Page_Load(object sender, EventArgs e) {
        //execute on first load
        if(!this.IsPostBack) {
            //populate birthdate dropdown menu
            for (int i = 1; i <= 31; i++) {
                lstDOBDay.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            for (int i = 1; i <= 12; i++) {
                lstDOBMonth.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            int currentYear = System.DateTime.Now.Year;
            for (int i = currentYear; i >= currentYear - 100; i--) {
                lstDOBYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            if (Request.QueryString["patient"] != null)
                patientID = Request.QueryString["patient"];
            else
                patientID = "";

            patientPic.ImageUrl = "~/Content/Assets/Images/defaultuserpic.png";

            if (String.IsNullOrEmpty(patientID) || Int32.Parse(patientID) <= 0) {
                litName.Text = "Invalid or Empty Profile";
                btnEdit.Enabled = false;
            }
            else {
                loadProfile();
            }
        }
    }

    //handler for custom validate birthdate
    protected void vldDate_Validate(object source, ServerValidateEventArgs args) {
        try {
            int Day = int.Parse(lstDOBDay.SelectedValue);
            int Month = int.Parse(lstDOBMonth.SelectedValue);
            int Year = int.Parse(lstDOBYear.SelectedValue);

            DateTime Test = new DateTime(Year, Month, Day);
            //System.Diagnostics.Debug.WriteLine(Test.Date.ToString());
            args.IsValid = true;
        }
        catch (FormatException Arg) {
            // item not selected (couldn't convert int)
            args.IsValid = false;
        }
        catch (ArgumentOutOfRangeException Arg) {
            // invalid date (31 days in February)
            args.IsValid = false;
        }
    }

    //load information from patient db
    private void loadProfile() {
        //Find Patient Names
        string selectSQL;
        selectSQL = "SELECT * FROM Patient";
        selectSQL += " WHERE (PatientID = @pid)";

        //Define ADO.Net objects
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataReader reader;
        cmd.Parameters.AddWithValue("@pid", Int32.Parse(patientID));

        try {
            using (con) {
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read()) {
                    litName.Text = reader["FirstName"].ToString() + " " + reader["LastName"].ToString();
                    lblPIDValue.Text = reader["PatientID"].ToString().PadLeft(9, '0');
                    txtLName.Text += reader["LastName"];
                    txtFName.Text += reader["FirstName"];
                    lstSex.SelectedValue = reader["Sex"].ToString();
                    lstDOBYear.SelectedValue = Convert.ToDateTime(reader["BirthDate"].ToString()).Year.ToString();
                    lstDOBMonth.SelectedValue = Convert.ToDateTime(reader["BirthDate"].ToString()).Month.ToString();
                    lstDOBDay.SelectedValue = Convert.ToDateTime(reader["BirthDate"].ToString()).Day.ToString();
                }
                reader.Close();
            }
        }
        catch (Exception err) {

        }
    }

    //create a new appointment
    protected void updatePatient() {
        //Insert string statement
        string insertSQL;
        insertSQL = "UPDATE Patient";
        insertSQL += " SET ";
        insertSQL += "LastName = '" + txtLName.Text + "', ";
        insertSQL += "FirstName = '" + txtFName.Text + "', ";
        insertSQL += "BirthDate = '" + lstDOBYear.SelectedValue +
                     lstDOBMonth.SelectedValue.PadLeft(2, '0') +
                     lstDOBDay.SelectedValue.PadLeft(2, '0') + "', ";
        insertSQL += "Sex = '" + lstSex.SelectedValue + "'";
        insertSQL += " WHERE PatientID = " + Int32.Parse(lblPIDValue.Text);

        System.Diagnostics.Debug.WriteLine(insertSQL);

        //Define ADO.net objects
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(insertSQL, con);

        //Try to open database and execute SQL insert
        int added = 0;
        try {
            using (con) {
                con.Open();
                System.Diagnostics.Debug.WriteLine("Connect Opened");
                added = cmd.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine("Executing update");
            }
        }
        catch (Exception err) {

        }
    }

    private void toggleEditable(bool state) {
        txtLName.Enabled = state;
        txtFName.Enabled = state;
        lstSex.Enabled = state;
        lstDOBYear.Enabled = state;
        lstDOBMonth.Enabled = state;
        lstDOBDay.Enabled = state;
        txtAddress1.Enabled = state;
        txtAddress2.Enabled = state;
        txtCity.Enabled = state;
        txtCountry.Enabled = state;
        txtHomeNum.Enabled = state;
        txtWorkNum.Enabled = state;
        txtMailCode.Enabled = state;
    }

    protected void btnEdit_Click(object sender, EventArgs e) {
        toggleEditable(true);
        btnSave.Enabled = true;
        btnEdit.Enabled = false;
    }

    protected void btnSave_Click(object sender, EventArgs e) {
        if (Page.IsValid) {
            updatePatient();
            toggleEditable(false);
            btnEdit.Enabled = true;
            btnSave.Enabled = false;
            Response.Redirect("~/patientprofile.aspx?patient=" + Int32.Parse(lblPIDValue.Text));
        }
    }

    protected void btnBook_Click(object sender, EventArgs e) {
        Response.Redirect("~/newappointment.aspx?patient=" + Int32.Parse(lblPIDValue.Text));
    }
}