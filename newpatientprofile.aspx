<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newpatientprofile.aspx.cs" Inherits="NewPatientProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Patient Profile</title>
    <link href="Content/Styles/basicpagestyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmProfile" runat="server">
        <div class="headerContainer">
            <div style="display:inline-block;">
                <h1>New Patient Profile</h1>
            </div>
            <div style="display:inline-block; float: right; padding:10px; border: 1px solid #999; background-color:#333">
                <asp:HyperLink ID="lnkSchedule" runat="server" ForeColor="White" NavigateUrl="~/scheduler.aspx">Back to Schedule</asp:HyperLink>
            </div>
        </div>

        <div class ="mainContainer" >
            Enter information for new patient profile.
            <br />
            <br />

            <div class="formFields">
                <div>
                    <asp:Label ID="lblLName" CssClass="formLabel" runat="server" Text="Last Name:" />
                    <asp:TextBox ID="txtLName" runat="server" />
                    <asp:RequiredFieldValidator ID="vldLName" CssClass="validationMessage" runat="server" ErrorMessage="Last name is required." ControlToValidate="txtLName" EnableClientScript="False" Display="Dynamic" >*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="vldLNameString" runat="server" CssClass="validationMessage" ErrorMessage="Last name cannot contain non-alphabetical characters." ControlToValidate="txtLName" ValidationExpression="[a-zA-Z]*" EnableClientScript="False" Display="Dynamic">*</asp:RegularExpressionValidator>
                </div>                
                
                <div>
                    <asp:Label ID="lblFName" CssClass="formLabel" runat="server" Text="First Name:" />
                    <asp:TextBox ID="txtFName" runat="server" />
                    <asp:RequiredFieldValidator ID="vldFName" CssClass="validationMessage" runat="server" 
                        ErrorMessage="First name is required." 
                        ControlToValidate="txtFName" EnableClientScript="False" Display="Dynamic" >*</asp:RequiredFieldValidator> 
                    <asp:RegularExpressionValidator ID="vldFNameString0" runat="server" CssClass="validationMessage" ErrorMessage="First name cannot contain non-alphabetical characters." ControlToValidate="txtFName" ValidationExpression="[a-zA-Z]*" EnableClientScript="False" Display="Dynamic">*</asp:RegularExpressionValidator>
                </div>           
                
                <div>
                    <asp:Label ID="lbl_sex" CssClass="formLabel" runat="server" Text="Sex:" />
                    <asp:DropDownList ID="lstSex" runat="server">
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
                    <asp:DropDownList ID="lstDOBYear" runat="server">
                        <asp:ListItem Text="Year" Selected="True" disabled="disabled" Value="invalid"></asp:ListItem>
                    </asp:DropDownList>-
                    <asp:DropDownList ID="lstDOBMonth" runat="server">
                        <asp:ListItem Text="Month" Selected="True" disabled="disabled" Value="invalid"></asp:ListItem>
                    </asp:DropDownList>-
                    <asp:DropDownList ID="lstDOBDay" runat="server">
                        <asp:ListItem Text="Day" Selected="True" disabled="disabled" Value="invalid"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:CustomValidator ID="vldDob" CssClass="validationMessage" runat="server" 
                        OnServerValidate="vldDate_Validate" 
                        ErrorMessage="Date of birth is invalid." EnableClientScript="False">*</asp:CustomValidator>                
                </div>
                  
                <div>
                    <asp:Label ID="lblStreet" CssClass="formLabel" runat="server" Text="Address:" />
                    <asp:TextBox ID="txtAddress1" runat="server" />                      
                </div>        
                
                <div>
                    <asp:Label CssClass="formLabel" runat="server"/>
                    <asp:TextBox ID="txtAddress2" runat="server"/>                      
                </div>       
            
                <div>    
                    <asp:Label ID="lblCity" CssClass="formLabel" runat="server" Text="City:" />
                    <asp:TextBox ID="txtCity" runat="server" />
                </div>
            
                <div>                            
                    <asp:Label ID="lblMailCode" CssClass="formLabel" runat="server" Text="ZIP Code:" />
                    <asp:TextBox ID="txtMailCode" runat="server" />
                </div> 
            
                <div>                      
                    <asp:Label ID="lblCountry" CssClass="formLabel" runat="server" Text="Country:" />
                    <asp:TextBox ID="txtCountry" runat="server" />       
                </div>         
            
                <div>    
                    <asp:Label ID="lblPhoneNum" CssClass="formLabel" runat="server" Text="Primary Phone:" />
                    <asp:TextBox ID="txtPhoneNum" runat="server" />   
                    <asp:RadioButtonList ID="rdoLstBestPhone" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                       <asp:ListItem selected="true">Home</asp:ListItem>
                       <asp:ListItem>Work</asp:ListItem>
                       <asp:ListItem>Mobile</asp:ListItem>
                    </asp:RadioButtonList>
                </div>  
            
                <div> 
                    <asp:Label ID="lblEmail" CssClass="formLabel" runat="server" Text="Email Address:" />
                    <asp:TextBox ID="txtEmail" runat="server" />
                </div>
            </div>

            <br />

            <div class="buttonBar">
                <asp:Button ID="btnSave" runat="server" Text="Save Patient" OnClick="btnSave_Click" />
                <asp:Button ID="btnReset" runat="server" Text="Clear Fields" OnClick="btnReset_Click" CausesValidation="False"/>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False"/>
            </div>
            <asp:ValidationSummary ID="vldSummary" CssClass="validationSummaryList" runat="server" />
        </div>

        <div class ="footerContainer">
            
        </div>
    </form>
</body>
</html>
