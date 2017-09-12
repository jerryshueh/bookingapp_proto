using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Base scheduler settings class
/// </summary>
public class Schedule
{
    protected DateTime date;                    //date selected
    protected int unit;                         //time in minutes of single block unit
    protected int startHour, startMinute;       //Hour and Minute the work clock (daily schedule) begins
    protected int endHour, endMinute;          //Hour and Minute the work clock ends
    
    public Schedule(DateTime newDate) {
        this.date = newDate;
	}

    public Schedule(int newYear, int newMonth, int newDay) {
        this.date = new DateTime(newYear, newMonth, newDay);
    }

    public DateTime Date {
        get { return this.date;}
        set { this.date = value;}
    }

    public int Unit {
        get { return this.unit; }
        set { this.unit = value; }
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
}