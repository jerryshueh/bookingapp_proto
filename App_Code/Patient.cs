using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Base patient profile class
/// </summary>
public class Patient
{
    //ID
    protected string patientID;

    //Mandatory fields
    protected string fName;         //Patient's first name
    protected string lName;         //Patient's last name
    protected char sex;             //Patient's sex
    protected string birthDate;     //Patient's date of birth

    //Address & contact
    protected Contact contactInfo;

    //Family
    protected int familyID;

	public Patient(string fName, string lName, char sex, string birthDate) {
        //Must have these properties
        this.fName = fName;
        this.lName = lName;
        this.sex = sex;
        this.birthDate = birthDate;
        this.contactInfo = new Contact();

        System.Diagnostics.Debug.WriteLine("NEW PATIENT CREATED");
	}

    //Mandatory properties

    public string PatientID {
        get { return this.patientID; }
        set { this.patientID = value; }
    }

    public string FName {
        get { return this.fName; }
        set { this.fName = value; }
    }

    public string LName {
        get { return this.lName; }
        set { this.lName = value; }
    }

    public char Sex {
        get { return this.sex; }
        set { this.sex = value; }
    }

    public string BirthDate {
        get { return this.birthDate; }
        set { this.birthDate = value; }
    }

    //Address properties

    public string Address1 {
        get { return this.contactInfo.Address1; }
        set { this.contactInfo.Address1 = value; }
    }

    public string Address2 {
        get { return this.contactInfo.Address2; }
        set { this.contactInfo.Address2 = value; }
    }

    public string City {
        get { return this.contactInfo.City; }
        set { this.contactInfo.City = value; }
    }

    public string Country {
        get { return this.contactInfo.Country; }
        set { this.contactInfo.Country = value; }
    }

    public string PostalCode {
        get { return this.contactInfo.PostalCode; }
        set { this.contactInfo.PostalCode = value; }
    }

    //Comm. properties

    public string HomePhone {
        get { return this.contactInfo.HomePhone; }
        set { this.contactInfo.HomePhone = value; }
    }

    public string WorkPhone {
        get { return this.contactInfo.WorkPhone; }
        set { this.contactInfo.WorkPhone = value; }
    }

    public string MobilePhone {
        get { return this.contactInfo.MobilePhone; }
        set { this.contactInfo.MobilePhone = value; }
    }

    public int BestPhone {
        get { return this.contactInfo.BestPhone; }
        set { this.contactInfo.BestPhone = value; }
    }

    public string Email {
        get { return this.contactInfo.Email; }
        set { this.contactInfo.Email = value; }
    }
}