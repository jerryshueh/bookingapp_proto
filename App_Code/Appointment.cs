using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Base appointment block class
/// </summary>
public class Appointment
{
    enum BlockColor {
        DEFAULTGREY,
        BLUE,
        RED,
        YELLOW,
        GREEN
    }

    //ID
    string appointmentID;
    string patientID;      //Target patient's ID
    string doctorID;

    //Time
    DateTime date;      //Date of appointment
    int startHour;      //Starting hour
    int startMinute;    //Starting minute
    int duration;       //Duration of appointment *in minutes*
    int endHour;        //Ending hour
    int endMinute;      //Ending minute

    //Extra
    string note;        //Notes about appointment
    string status;      //Customizable appointment status
    int color;          //Color of block

    public Appointment(string appointmentID, string patientID, string doctorID, int startHour, int startMinute, int duration, int color) {
        this.appointmentID = appointmentID;
        this.patientID = patientID;
        this.doctorID = doctorID;
        this.startHour = startHour;
        this.startMinute = startMinute;
        this.duration = duration;
        this.color = color;
        this.note = "";
        this.status = "";

        calculateEnd();

        //System.Diagnostics.Debug.WriteLine("NEW APPOINTMENT CREATED: " + this.appointmentID);
    }

    private void calculateEnd() {
        endHour = startHour;
        endMinute = startMinute;
        int counter = duration;
        while (counter > 0) {
            endMinute += 5;

            if (endMinute >= 60) {
                endHour += 1;
                endMinute = 0;
            }

            counter -= 5;
        }
    }

    public string AppointmentID {
        get { return this.appointmentID; }
        set { this.appointmentID = value; }
    }

    public string PatientID {
        get { return this.patientID;}
        set { this.patientID = value; } 
    }

    public string DoctorID {
        get { return this.doctorID; }
        set { this.doctorID = value; }
    }

    public DateTime Date {
        get { return this.date; }
        set { this.date = value; }
    }

    public int StartHour {
        get { return this.startHour; }
        set { this.startHour = value; }
    }

    public int StartMinute {
        get { return this.startMinute; }
        set { this.startMinute = value; }
    }

    public int EndHour {
        get { return this.endHour; }
        set { this.endHour = value; }
    }

    public int EndMinute {
        get { return this.endMinute; }
        set { this.endMinute = value; }
    }

    public int Duration {
        get { return this.duration; }
        set { this.duration = value; }
    }

    public string Note {
        get { return this.note; }
        set { this.note = value; }
    }

    public string Status {
        get { return this.status; }
        set { this.status = value; }
    }

    public int Color {
        get { return this.color; }
        set { this.color = value; }
    }
}