using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class NewPatientProfile : System.Web.UI.Page {
    //connection string for database
    string connectionString = WebConfigurationManager.ConnectionStrings["TCDentalSchedulerDB"].ConnectionString;

    //run on page load
    protected void Page_Load(object sender, EventArgs e) {
        //execute on first load
        if (!this.IsPostBack) {
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
    protected void vldDate_Validate(object source, ServerValidateEventArgs args) {
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

    //create a new patient profile
    protected void createNewPatient() {
        //Insert string statement
        string insertSQL;
        insertSQL = "INSERT INTO Patient";
        insertSQL += " (LastName, FirstName, BirthDate, Sex)";
        insertSQL += "VALUES ('";
        insertSQL += txtLName.Text + "', '";
        insertSQL += txtFName.Text + "', '";
        insertSQL += lstDOBYear.SelectedValue +
                     lstDOBMonth.SelectedValue.PadLeft(2, '0') +
                     lstDOBDay.SelectedValue.PadLeft(2, '0') + "', '";
        insertSQL += lstSex.SelectedValue + "')";
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

    //handler for clicking save button
    protected void btnSave_Click(object sender, EventArgs e) {
        if(Page.IsValid)
            createNewPatient();
        clearControl(Page.Controls);
    }

    //handler for clicking reset button 
    protected void btnReset_Click(object sender, EventArgs e) {
        clearControl(Page.Controls);
    }

    protected void btnCancel_Click(object sender, EventArgs e) {
        Response.Redirect("~/searchpatient.aspx");
    }
}