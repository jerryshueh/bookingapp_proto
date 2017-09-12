<%@ Page Language="C#" AutoEventWireup="true" CodeFile="patientprofile.aspx.cs" Inherits="PatientProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Patient</title>
    <link href="Content/Styles/basicpagestyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmProfile" runat="server">
        <div class ="headerContainer">
            <div style="display:inline-block;">
                <h1>Patient Profile</h1>
            </div>
            <div style="display:inline-block; float: right; padding:10px; border: 1px solid #999; background-color:#333">
                <asp:HyperLink ID="lnkSchedule" runat="server" ForeColor="White" NavigateUrl="~/scheduler.aspx">Back to Schedule</asp:HyperLink>
            </div>

            <div style="display:inline-block; float: right; width:10px">&nbsp;</div>

            <div style="display:inline-block; float: right; padding:10px; border: 1px solid #999; background-color:#333">
                <asp:HyperLink ID="lnkSearch" runat="server" ForeColor="White" NavigateUrl="~/searchpatient.aspx">Search Patients</asp:HyperLink>
            </div>
        </div>

        <div class ="mainContainer" >
            <asp:Literal ID="litName" runat="server"/>
            <br />
            <asp:Image ID="patientPic" runat="server" Height="150px" Width="150px" />
            <br />
            <br />

            <div class="formFields">
                <div>
                    <asp:Label ID="lblPID" CssClass="formLabel" runat="server" Text="ID:" />
                    <asp:Label ID="lblPIDValue" CssClass="formLabel" runat="server" />
                </div>   

                <div>
                    <asp:Label ID="lblLName" CssClass="formLabel" runat="server" Text="Last Name:" />
                    <asp:TextBox ID="txtLName" runat="server" Enabled="False" />
                    <asp:RequiredFieldValidator ID="vldLName" CssClass="validationMessage" runat="server" ErrorMessage="Last name is required." ControlToValidate="txtLName" EnableClientScript="False" Display="Dynamic" >*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="vldLNameString" runat="server" CssClass="validationMessage" ErrorMessage="Last name cannot contain non-alphabetical characters." ControlToValidate="txtLName" ValidationExpression="[a-zA-Z]*" EnableClientScript="False" Display="Dynamic">*</asp:RegularExpressionValidator>
                </div>                
                
                <div>
                    <asp:Label ID="lblFName" CssClass="formLabel" runat="server" Text="First Name:" />
                    <asp:TextBox ID="txtFName" runat="server" Enabled="False" />
                    <asp:RequiredFieldValidator ID="vldFName" CssClass="validationMessage" runat="server" 
                        ErrorMessage="First name is required." 
                        ControlToValidate="txtFName" EnableClientScript="False" Display="Dynamic" >*</asp:RequiredFieldValidator> 
                    <asp:RegularExpressionValidator ID="vldFNameString0" runat="server" CssClass="validationMessage" ErrorMessage="First name cannot contain non-alphabetical characters." ControlToValidate="txtFName" ValidationExpression="[a-zA-Z]*" EnableClientScript="False" Display="Dynamic">*</asp:RegularExpressionValidator>
                </div>           
                
                <div>
                    <asp:Label ID="lbl_sex" CssClass="formLabel" runat="server" Text="Sex:" />
                    <asp:DropDownList ID="lstSex" runat="server" Enabled="False">
                        <asp:ListItem Text="Select" Selected="True" disabled="disabled" Value="invalid"/>
                        <asp:ListItem Text="Male" Value="M"/>
                        <asp:ListItem Text="Female" Value="F"/>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="vldSex" CssClass="validationMessage" runat="server" 
                        ErrorMessage="Sex field is required." 
                        ControlToValidate="lstSex" InitialValue="invalid" EnableClientScript="False" >*</asp:RequiredFieldValidator> 
                </div>              
                
                <div>
                    <asp:Label ID="lblDob" CssClass="formLabel" runat="server" Text="Date of Birth:" />
                    <asp:DropDownList ID="lstDOBYear" runat="server" Enabled="False">
                        <asp:ListItem Text="Year" Selected="True" disabled="disabled" Value="invalid"></asp:ListItem>
                    </asp:DropDownList>-
                    <asp:DropDownList ID="lstDOBMonth" runat="server" Enabled="False">
                        <asp:ListItem Text="Month" Selected="True" disabled="disabled" Value="invalid"></asp:ListItem>
                    </asp:DropDownList>-
                    <asp:DropDownList ID="lstDOBDay" runat="server" Enabled="False">
                        <asp:ListItem Text="Day" Selected="True" disabled="disabled" Value="invalid"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:CustomValidator ID="vldDob" CssClass="validationMessage" runat="server" 
                        OnServerValidate="vldDate_Validate" 
                        ErrorMessage="Date of birth is invalid." EnableClientScript="False">*</asp:CustomValidator>        
                </div>
                  
                <div>
                    <asp:Label ID="lblStreet" CssClass="formLabel" runat="server" Text="Address:" />
                    <asp:TextBox ID="txtAddress1" runat="server" Enabled="False" />                      
                </div>        
                
                <div>
                    <asp:Label CssClass="formLabel" runat="server"/>
                    <asp:TextBox ID="txtAddress2" runat="server" Enabled="False"/>                      
                </div>       
            
                <div>    
                    <asp:Label ID="lblCity" CssClass="formLabel" runat="server" Text="City:" />
                    <asp:TextBox ID="txtCity" runat="server" Enabled="False" />
                </div>
            
                <div>                            
                    <asp:Label ID="lblMailCode" CssClass="formLabel" runat="server" Text="ZIP Code:" />
                    <asp:TextBox ID="txtMailCode" runat="server" Enabled="False" />
                </div> 
            
                <div>                      
                    <asp:Label ID="lblCountry" CssClass="formLabel" runat="server" Text="Country:" />
                    <asp:TextBox ID="txtCountry" runat="server" Enabled="False" />       
                </div>         
            
                <div>    
                    <asp:Label ID="lblHomeNum" CssClass="formLabel" runat="server" Text="Home Phone:" />
                    <asp:TextBox ID="txtHomeNum" runat="server" Enabled="False" />   
                </div>  

                <div>    
                    <asp:Label ID="lblWorkNum" CssClass="formLabel" runat="server" Text="Work Phone:" />
                    <asp:TextBox ID="txtWorkNum" runat="server" Enabled="False" />   
                </div>  

                <div>    
                    <asp:Label ID="lblMobileNum" CssClass="formLabel" runat="server" Text="Mobile Phone:" />
                    <asp:TextBox ID="txtMobileNum" runat="server" Enabled="False" />   
                </div>  
            
                <div> 
                    <asp:Label ID="lblEmail" CssClass="formLabel" runat="server" Text="Email Address:" />
                    <asp:TextBox ID="txtEmail" runat="server" Enabled="False" />
                </div>
            </div>

            <br />

            <div class="buttonBar">
                <asp:Button ID="btnEdit" runat="server" Text="Edit Profile" OnClick="btnEdit_Click" CausesValidation="False"/>
                <asp:Button ID="btnSave" runat="server" Text="Save Changes" OnClick="btnSave_Click" Enabled="False" />
                <asp:Button ID="btnBook" runat="server" Text="Book Appointment" OnClick="btnBook_Click" CausesValidation="False" />
            </div>
            <asp:ValidationSummary ID="vldSummary" CssClass="validationSummaryList" runat="server" />
        </div>

        <div class ="footerContainer">
            
        </div>
    </form>
</body>
</html>
