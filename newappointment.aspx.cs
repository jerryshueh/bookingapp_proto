using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class NewAppointment : System.Web.UI.Page
{
    //connection string for database
    string connectionString = WebConfigurationManager.ConnectionStrings["TCDentalSchedulerDB"].ConnectionString;

    string patientID;

    //run on page load
    protected void Page_Load(object sender, EventArgs e) {
        //execute on first load
        if (!this.IsPostBack) {
            //populate birthdate dropdown menu
            for (int i = 1; i <= 31; i++) {
                lstDateDay.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            for (int i = 1; i <= 12; i++) {
                lstDateMonth.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            int currentYear = System.DateTime.Now.Year;
            for (int i = currentYear; i <= currentYear + 50; i++) {
                lstDateYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            //populate time dropdown menu
            for (int i = 0; i <= 23; i++) {
                lstHour.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            for (int i = 0; i <= 55; i+=5) {
                lstMinute.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            //populate duration menu
            for (int i = 0; i <= 20; i++) {
                lstDuration.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            //populate doctor menu
            loadDoctorMenu();
        }

        //get patient info
        if (Request.QueryString["patient"] != null)
            patientID = Request.QueryString["patient"];
        else
            patientID = "";

        if (String.IsNullOrEmpty(patientID) || Int32.Parse(patientID) <= 0) {
            litName.Text = "Invalid Patient";
            btnBook.Enabled = false;
        }
        else {
            loadPatientInfo();
        }
    }

    //handler for custom validate birthdate
    protected void vldDate_Validate(object source, ServerValidateEventArgs args) {
        try {
            int Day = int.Parse(lstDateDay.SelectedValue);
            int Month = int.Parse(lstDateMonth.SelectedValue);
            int Year = int.Parse(lstDateYear.SelectedValue);

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

    private void loadDoctorMenu() {
        string selectSQL;
        selectSQL = "SELECT * FROM Doctor";

        //Define ADO.Net objects
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataReader reader;

        try {
            using (con) {
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read()) {
                    lstDoctor.Items.Add(new ListItem("Dr. " + reader["FirstName"].ToString() +
                                                        ", " +
                                                        reader["LastName"].ToString() +
                                                        " (" + reader["DoctorID"].ToString().PadLeft(3, '0') + ")",
                                                        reader["DoctorID"].ToString()));
                }
                reader.Close();
            }
        }
        catch (Exception err) {

        }
    }

    private void loadPatientInfo() {
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
                }
                reader.Close();
            }
        }
        catch (Exception err) {

        }
    }

    //create a new appointment
    protected void createNewAppointment() {
        //Insert string statement
        string insertSQL;
        insertSQL = "INSERT INTO Appointment";
        insertSQL += " (PatientID, DoctorID, Date, StartHour, StartMinute, Duration)";
        insertSQL += " VALUES (";
        insertSQL += patientID + ", ";
        insertSQL += lstDoctor.SelectedValue + ", '";
        insertSQL += lstDateYear.SelectedValue +
                     lstDateMonth.SelectedValue.PadLeft(2, '0') +
                     lstDateDay.SelectedValue.PadLeft(2, '0') + "', ";
        insertSQL += lstHour.SelectedValue + ", ";
        insertSQL += lstMinute.SelectedValue + ", ";
        insertSQL += lstDuration.SelectedValue + ")";

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
                System.Diagnostics.Debug.WriteLine("Executing insert");
            }
        }
        catch (Exception err) {

        }
    }

    //clear all fields in specified control
    protected void clearControl(ControlCollection ctrls) {
        foreach (Control ctrl in ctrls) {
            if (ctrl is TextBox) {
                ((TextBox)ctrl).Text = string.Empty;
            }
            else if (ctrl is DropDownList) {
                ((DropDownList)ctrl).ClearSelection();
                ((DropDownList)ctrl).SelectedIndex = 0;
            }
            else if (ctrl is RadioButtonList) {
                ((RadioButtonList)ctrl).ClearSelection();
                ((RadioButtonList)ctrl).SelectedIndex = 0;
            }

            clearControl(ctrl.Controls);
        }
    }

    protected void btnBook_Click(object sender, EventArgs e) {
        if (Page.IsValid)
            createNewAppointment();
        clearControl(Page.Controls);
    }
}