using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Scheduler : System.Web.UI.Page {
    //connection string for database
    string connectionString = WebConfigurationManager.ConnectionStrings["TCDentalSchedulerDB"].ConnectionString;

    //Date store
    DataSet dsAppointments;         //dataset of retrieved appointments
    List<Appointment> schedList1;   
    List<Appointment> schedList2;
    List<Appointment> schedList3;

    //Scheduler properties
    DateTime currentDate;   //date to display
    int startHour;          //starting hour of day
    int unitTime;           //time interval of single unit
    int unitsPerHour;       //number of units for every hour
    int totalHours;         //total number of houw slots to display
    int totalUnits;         //total number of units to display
    double unitHeight;      //height of unit cell in em

    //Used to format table
    bool hour12;            //display AM/PM
    bool printTime;         //keep printing the time

    protected void Page_Init(object sender, EventArgs e) {
        //assign variables on load
        startHour = 8;
        unitTime = 15;
        totalHours = 9;
        unitsPerHour = 60 / unitTime;
        totalUnits = unitsPerHour * totalHours;
        unitHeight = 2.5;
        hour12 = true;
        printTime = true;
    }

    protected void Page_Load(object sender, EventArgs e) {

        if (Request.QueryString["date"] != null)
            currentDate = Convert.ToDateTime(Request.QueryString["date"]);
        else
            currentDate  = DateTime.Today;

        cldPickDate.SelectedDate = currentDate;
        litCurrentDate.Text = currentDate.ToString("MMM-dd-yyyy");

        //get db information
        dsAppointments = getAppointments();

        //populate timeline
        generateTimeline();

        //clear each schedule first
        clearSched(tblSched1);
        clearSched(tblSched2);
        clearSched(tblSched3);

        //create appointment List for each schedule
        createAppointmentsList(tblSched1);
        createAppointmentsList(tblSched2);
        createAppointmentsList(tblSched3);

        //mark appointment blocks
        markAppointments(tblSched1);
        markAppointments(tblSched2);
        markAppointments(tblSched3);

        //populateSchedule(tblSched1);
        //populateSchedule(tblSched1);
        //populateSchedule(tblSched2);
        //populateSchedule(tblSched3);
    }

    private void generateTimeline() {
        int currentHour = startHour;
        int currentMinute;

        for (int i = 0; i < totalHours; i++) {
            //populate timeline
            TableRow newRow = new TableRow();
            newRow.Height = new Unit(unitHeight * 2, UnitType.Em);
            tblTimeline.Rows.Add(newRow);

            TableCell tCell;
            tCell = new TableCell();
            tCell.CssClass = "cellHour";
            tCell.Height = new Unit(unitHeight * 2, UnitType.Em);
            tCell.Text = formatTime(currentHour);
            newRow.Cells.Add(tCell);

            tCell = new TableCell();
            newRow.Cells.Add(tCell);

            Table minDisplay = new Table();
            minDisplay.CssClass = "tblMinute";
            tCell.Controls.Add(minDisplay);

            currentMinute = 0;
            for (int j = 0; j < unitsPerHour; j++) {
                TableRow newMinuteRow = new TableRow();
                minDisplay.Rows.Add(newMinuteRow);

                TableCell tMinuteCell = new TableCell();
                if (printTime)
                    tMinuteCell.Text = " : " + currentMinute.ToString().PadLeft(2, '0');
                newMinuteRow.Cells.Add(tMinuteCell);
                tMinuteCell.Height = new Unit(unitHeight, UnitType.Em);

                currentMinute += unitTime;
            }

            currentHour++;
        }
    }

    //fill table with empty spaces
    private void clearSched(Table target) {
        string schedNum = target.ID.Remove(0, 8);
        int unitCounter = 0;
        int currentHour = startHour;
        int currentMinute;

        for (int i = 0; i < totalUnits; i++) {
            currentMinute = unitTime * unitCounter;

            TableRow newRow = new TableRow();
            target.Rows.Add(newRow);

            TableCell tCell = new TableCell();
            tCell.CssClass = "cellUnit";
            tCell.Height = new Unit(unitHeight, UnitType.Em);
            tCell.ID = schedNum + ":" + currentHour.ToString() + ":" + currentMinute.ToString();
            newRow.Cells.Add(tCell);

            unitCounter++;
            if (unitCounter >= unitsPerHour) {
                currentHour++;
                unitCounter = 0;
            }
        }
    }

    //retrieve current date's appointments from db
    private DataSet getAppointments() {
        //Select statement string
        string selectSQL;
        selectSQL = "SELECT * FROM Appointment";
        selectSQL += " WHERE (Date = @date)";

        //Define ADO.Net objects
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet dsAppts = new DataSet();

        cmd.Parameters.AddWithValue("@date", currentDate);
        System.Diagnostics.Debug.WriteLine(currentDate);

        //Try to open database and execute SQL query
        try {
            using (con) {
                con.Open();
                adapter.Fill(dsAppts, "Appointment");
            }
        }
        catch (Exception err) {

        }

        return dsAppts;
    }

    //store information from dataset into separate Lists
    private void createAppointmentsList(Table target) {
        string schedNum = target.ID.Remove(0, 8);
        List<Appointment> apptList = new List<Appointment>();

        //populate appointment List for current provider table
        foreach (DataRow row in dsAppointments.Tables["Appointment"].Rows) {
            if (row["DoctorID"].ToString() == schedNum) {
                Appointment appt = new Appointment(row["AppointmentID"].ToString(), row["PatientID"].ToString(), row["DoctorID"].ToString(), (int)row["StartHour"], (int)row["StartMinute"], (int)row["Duration"], (int)row["Color"]);
                if (row["Status"] != null)
                    appt.Status = row["Status"].ToString();
                if (row["Note"] != null)
                    appt.Note = row["Note"].ToString();

                apptList.Add(appt);
            }
        }

        if (target.ID == "tblSched1") {
            schedList1 = apptList;
            System.Diagnostics.Debug.WriteLine("sched 1 list stored: " + schedList1.Count() + " items");
        }
        else if (target.ID == "tblSched2") {
            schedList2 = apptList;
            System.Diagnostics.Debug.WriteLine("sched 2 list stored: " + schedList2.Count() + " items");
        }
        else if (target.ID == "tblSched3") {
            schedList3 = apptList;
            System.Diagnostics.Debug.WriteLine("sched 3 list stored: " + schedList3.Count() + " items");
        }
    }

    //Mark the units that correspond to existing appointments
    private void markAppointments(Table target) {
        string schedNum = target.ID.Remove(0, 8);
        List<Appointment> apptList = new List<Appointment>();

        if (schedNum == "1") {
            apptList = schedList1;
            System.Diagnostics.Debug.WriteLine("sched 1 list read: " + apptList.Count() + " items");
        }
        else if (schedNum == "2") {
            apptList = schedList2;
            System.Diagnostics.Debug.WriteLine("sched 2 list read: " + apptList.Count() + " items");
        }
        else if (schedNum == "3") {
            apptList = schedList3;
            System.Diagnostics.Debug.WriteLine("sched 3 list read: " + apptList.Count() + " items");
        }

        for (int i = 0; i < apptList.Count(); i++) {
            int absoluteStart = (apptList[i].StartHour * 60) + apptList[i].StartMinute;
            int absoluteEnd = (apptList[i].EndHour * 60) + apptList[i].EndMinute;

            foreach (Control c in target.Controls) {
                if (c is TableRow && c.ID == null) {
                    //int cellHour = Int32.Parse(c.ID.Remove(0, 2));
                    foreach (Control n in c.Controls) {
                        if (n is TableCell) {
                            string[] splitID = (n.ID.Remove(0, 2)).Split(':');
                            int cellHour = Int32.Parse(splitID[0]);
                            int cellMinute = Int32.Parse(splitID[1]);
                            int absoluteTime = (cellHour * 60) + cellMinute;
                            if (absoluteTime >= absoluteStart && absoluteTime < absoluteEnd) {
                                //Find Patient Names
                                string selectSQL;
                                selectSQL = "SELECT * FROM Patient";
                                selectSQL += " WHERE (PatientID = @pid)";

                                //Define ADO.Net objects
                                SqlConnection con = new SqlConnection(connectionString);
                                SqlCommand cmd = new SqlCommand(selectSQL, con);
                                SqlDataReader reader;
                                cmd.Parameters.AddWithValue("@pid", Int32.Parse(apptList[i].PatientID));

                                try {
                                    using (con) {
                                        con.Open();
                                        reader = cmd.ExecuteReader();

                                        while (reader.Read()) {
                                            ((TableCell)n).Text = apptList[i].PatientID + " - ";
                                            ((TableCell)n).Text += reader["LastName"] + ", " + reader["FirstName"];
                                            ((TableCell)n).BackColor = System.Drawing.ColorTranslator.FromHtml("#333");
                                        }
                                        reader.Close();
                                    }
                                }
                                catch (Exception err) {

                                }
                            }
                        }
                    }
                }
            }
        }
    }

    //populate table with appointments from dataset
    private void populateSchedule(Table target) {
        List<Appointment> apptList = new  List<Appointment>();

        //populate appointment List for current provider table
        foreach (DataRow row in dsAppointments.Tables["Appointment"].Rows) {
            if ("tblSched" + row["DoctorID"].ToString() == target.ID) {
                Appointment appt = new Appointment(row["AppointmentID"].ToString(), row["PatientID"].ToString(), row["DoctorID"].ToString(), (int)row["StartHour"], (int)row["StartMinute"], (int)row["Duration"], (int)row["Color"]);

                if (row["Status"] != null)
                    appt.Status = row["Status"].ToString();
                if (row["Note"] != null)
                    appt.Note = row["Note"].ToString();

                apptList.Add(appt);
            }
        }

        int currentHour = startHour;
        int currentMinute;
        int unitCounter = 0;
        int blockCounter = 0;
        string blockID = "";

        //create the table rows and cells
        for (int i = 0; i < totalUnits; i++) {
            currentMinute = unitTime * unitCounter;

            //insert new row
            TableRow newRow = new TableRow();
            target.Rows.Add(newRow);

            //insert cell into row
            TableCell tCell = new TableCell();
            tCell.CssClass = "cellUnit";
            tCell.Height = new Unit(unitHeight, UnitType.Em);

            //Try to find a appointment that begins at current time
            Appointment apptBlock = apptList.Find(x => (x.StartHour == currentHour) && (x.StartMinute == currentMinute));
            
            //If found, start counting down
            if (apptBlock != null) {
                blockCounter = apptBlock.Duration / unitTime;
                blockID = apptBlock.AppointmentID + "-" + apptBlock.PatientID;
                tCell.ID = blockID;
                tCell.Text += tCell.ID + ": ";
                tCell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#fff");
                tCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#444");

                //Find Patient Names
                string selectSQL;
                selectSQL = "SELECT * FROM Patient";
                selectSQL += " WHERE (PatientID = @pid)";

                //Define ADO.Net objects
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(selectSQL, con);
                SqlDataReader reader;
                cmd.Parameters.AddWithValue("@pid", Int32.Parse(apptBlock.PatientID));

                try {
                    using (con) {
                        con.Open();
                        reader = cmd.ExecuteReader();

                        while (reader.Read()) {
                            tCell.Text += reader["LastName"] + ", " + reader["FirstName"];
                        }
                        reader.Close();
                    }
                }
                catch (Exception err) {

                }
            }

            //color in block if we are counting down
            if (blockCounter > 1 && apptBlock == null) {
                tCell.ID = blockID;
                tCell.Text += tCell.ID;
                tCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#777");
                blockCounter--;
            }

            newRow.Cells.Add(tCell);

            if (unitCounter == unitsPerHour - 1) {
                currentHour++;
                unitCounter = 0;
            }
            else
                unitCounter++;
        }
    }

    //Returns a time formatted to 12-hour or 24-hour clock depending on if hour12 is true/false
    private string formatTime(int hour) {
        string formattedTime = "";
        string timeSuffix = "";

        if (hour <= 23 && hour >= 0) { 
            if (hour12) {
                if (hour == 0) {
                    formattedTime = (hour+12).ToString();
                }
                else if (hour <= 12 && hour > 0) {
                    formattedTime = hour.ToString();
                }
                else {
                    formattedTime = (hour-12).ToString();
                }

                if (hour < 12) {
                    timeSuffix = "am";
                }
                else {
                    timeSuffix = "pm";
                }
            }
            else {
                formattedTime = hour.ToString();
            }
        }
        else {
            printTime = false;
        }

        return (formattedTime + timeSuffix);
    }

    private void populateTest() {
        foreach (DataRow row in dsAppointments.Tables["Appointment"].Rows) {
            if ((int)row["DoctorID"] == 1) {
                lblTest1.Text += "<br /> +1 Patient";
            }
            else if ((int)row["DoctorID"] == 2) {
                lblTest2.Text += "<br /> +1 Patient";
            }
            else if ((int)row["DoctorID"] == 3) {
                lblTest3.Text += "<br /> +1 Patient";
            }
        }
    }

    //find controls recursively (helps search through tables)
    private Control findControlRecursive(Control root, string id) {
        if (root.ID == id) {
            return root;
        }

        foreach (Control c in root.Controls) {
            Control t = findControlRecursive(c, id);
            if (t != null) {
                return t;
            }
        }

        return null;
    }  

    protected void btnBook_Click(object sender, EventArgs e) {

    }

    protected void btnBackDate_Click(object sender, EventArgs e) {
        DateTime newDate = currentDate.AddDays(-1);
        Response.Redirect("~/scheduler.aspx?date=" + newDate.ToString("yyyy-MM-dd"));
    }

    protected void btnForwardDate_Click(object sender, EventArgs e) {
        DateTime newDate = currentDate.AddDays(1);
        Response.Redirect("~/scheduler.aspx?date=" + newDate.ToString("yyyy-MM-dd"));
    }

    protected void btnJumpBackM_Click(object sender, EventArgs e) {
        DateTime newDate = currentDate.AddMonths(-1 * Int32.Parse(txtJumpMonth.Text));
        Response.Redirect("~/scheduler.aspx?date=" + newDate.ToString("yyyy-MM-dd"));
    }

    protected void btnJumpForwardM_Click(object sender, EventArgs e) {
        DateTime newDate = currentDate.AddMonths(Int32.Parse(txtJumpMonth.Text));
        Response.Redirect("~/scheduler.aspx?date=" + newDate.ToString("yyyy-MM-dd"));
    }

    protected void btnJumpBackW_Click(object sender, EventArgs e) {
        DateTime newDate = currentDate.AddDays(-7 * Int32.Parse(txtJumpWeek.Text));
        Response.Redirect("~/scheduler.aspx?date=" + newDate.ToString("yyyy-MM-dd"));
    }

    protected void btnJumpForwardW_Click(object sender, EventArgs e) {
        DateTime newDate = currentDate.AddDays(7 * Int32.Parse(txtJumpWeek.Text));
        Response.Redirect("~/scheduler.aspx?date=" + newDate.ToString("yyyy-MM-dd"));
    }

    protected void btnJumpBackD_Click(object sender, EventArgs e) {
        DateTime newDate = currentDate.AddDays(-1 * Int32.Parse(txtJumpDay.Text));
        Response.Redirect("~/scheduler.aspx?date=" + newDate.ToString("yyyy-MM-dd"));
    }

    protected void btnJumpForwardD_Click(object sender, EventArgs e) {
        DateTime newDate = currentDate.AddDays(Int32.Parse(txtJumpDay.Text));
        Response.Redirect("~/scheduler.aspx?date=" + newDate.ToString("yyyy-MM-dd"));
    }

    protected void btnJumpToday_Click(object sender, EventArgs e) {
        DateTime newDate = DateTime.Today;
        Response.Redirect("~/scheduler.aspx?date=" + newDate.ToString("yyyy-MM-dd"));
    }

    protected void cldPickDate_SelectionChanged(object sender, EventArgs e) {
        DateTime newDate = cldPickDate.SelectedDate;
        Response.Redirect("~/scheduler.aspx?date=" + newDate.ToString("yyyy-MM-dd"));
    }
}