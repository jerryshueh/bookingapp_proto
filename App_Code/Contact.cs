using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Base contact info class
/// </summary>
public class Contact
{
    //Contants
    enum PhoneType {
        HOME,
        WORK,
        MOBILE
    };

    //ID
    protected string contactID;

    //Address portion
    protected string address1;      //Address line 1
    protected string address2;      //Address line 2
    protected string city;          //Resident city
    protected string country;       //Resident country
    protected string postalCode;    //Postal code

    //Communication portion        
    protected string homePhone;     //Home phone number
    protected string workPhone;     //Workplace phone number
    protected string mobilePhone;   //Mobile/Cell phone number
    protected int bestPhone;        //Best or preferred phone number for user
    protected string email;         //Email address

    public Contact() {
        this.address1 = "";
        this.address2 = "";
        this.city = "";
        this.postalCode = "";
        this.country = "";

        System.Diagnostics.Debug.WriteLine("NEW CONTACTINFO CREATED");
    }

	public Contact(String address1, String address2, String city, String postalCode, String country) {
        this.address1 = "";
        this.address2 = "";
        this.city = city;
        this.postalCode = postalCode;
        this.country = country;
        this.bestPhone = (int)PhoneType.HOME;

        System.Diagnostics.Debug.WriteLine("NEW CONTACTINFO CREATED");
	}

    public string ContactID {
        get { return this.contactID; }
        set { this.contactID = value; }
    }

    public string Address1 {
        get { return this.address1; }
        set { this.address1 = value; }
    }

    public string Address2 {
        get { return this.address2; }
        set { this.address2 = value; }
    }

    public string City {
        get { return this.city; }
        set { this.city = value; }
    }

    public string Country {
        get { return this.country; }
        set { this.country = value; }
    }

    public string PostalCode {
        get { return this.postalCode; }
        set { this.postalCode = value; }
    }

    public string HomePhone { 
        get { return this.homePhone;}
        set { this.homePhone = value;} 
    }

    public string WorkPhone {
        get { return this.workPhone; }
        set { this.workPhone = value; }
    }

    public string MobilePhone {
        get { return this.mobilePhone; }
        set { this.mobilePhone = value; }
    }

    public int BestPhone {
        get { return this.bestPhone; }
        set { this.bestPhone = value; }
    }
    public string Email {
        get { return this.email; }
        set { this.email = value; }
    }
}