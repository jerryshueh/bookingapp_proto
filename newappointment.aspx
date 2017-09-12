<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newappointment.aspx.cs" Inherits="NewAppointment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Appointment</title>
    <link href="Content/Styles/basicpagestyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmProfile" runat="server">
        <div class="headerContainer">
            <div style="display:inline-block;">
                <h1>New Appointment</h1>
            </div>
            <div style="display:inline-block; float: right; padding:10px; border: 1px solid #999; background-color:#333">
                <asp:HyperLink ID="lnkSchedule" runat="server" ForeColor="White" NavigateUrl="~/scheduler.aspx">Back to Schedule</asp:HyperLink>
            </div>
        </div>

        <div class ="mainContainer" >
            Booking appointment for: <asp:Literal ID="litName" runat="server"></asp:Literal>
            <br />
            <br />

            <div class="formFields"> 
                <div>
                    <asp:Label ID="lblDate" CssClass="formLabel" runat="server" Text="Date of Birth:" />
                    <asp:DropDownList ID="lstDateYear" runat="server">
                        <asp:ListItem Text="Year" Selected="True" disabled="disabled" Value="invalid"></asp:ListItem>
                    </asp:DropDownList>-
                    <asp:DropDownList ID="lstDateMonth" runat="server">
                        <asp:ListItem Text="Month" Selected="True" disabled="disabled" Value="invalid"></asp:ListItem>
                    </asp:DropDownList>-
                    <asp:DropDownList ID="lstDateDay" runat="server">
                        <asp:ListItem Text="Day" Selected="True" disabled="disabled" Value="invalid"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:CustomValidator ID="vldDate" CssClass="validationMessage" runat="server" 
                        OnServerValidate="vldDate_Validate" 
                        ErrorMessage="Date is invalid." EnableClientScript="False">*</asp:CustomValidator>                
                </div>
                           
                <div>    
                    <asp:Label ID="lblTime" CssClass="formLabel" runat="server" Text="Time:" />
                    <asp:DropDownList ID="lstHour" runat="server">
                        <asp:ListItem Text="Hour" Selected="True" disabled="disabled" Value="invalid"></asp:ListItem>
                    </asp:DropDownList>&nbsp;:&nbsp; 
                    <asp:DropDownList ID="lstMinute" runat="server">
                        <asp:ListItem Text="Minute(s)" Selected="True" disabled="disabled" Value="invalid"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="vldHour" CssClass="validationMessage" runat="server" 
                        ErrorMessage="Hour field is required." 
                        ControlToValidate="lstHour" InitialValue="invalid" EnableClientScript="False" >*</asp:RequiredFieldValidator> 
                    <asp:RequiredFieldValidator ID="vldMinute" CssClass="validationMessage" runat="server" 
                        ErrorMessage="Minute(s) field required." 
                        ControlToValidate="lstMinute" InitialValue="invalid" EnableClientScript="False" >*</asp:RequiredFieldValidator> 
                </div>

                <div>                            
                    <asp:Label ID="lblDuration" CssClass="formLabel" runat="server" Text="Duration:" />
                    <asp:DropDownList ID="lstDuration" runat="server">
                        <asp:ListItem Text="Select" Selected="True" disabled="disabled" Value="invalid"/>
                    </asp:DropDownList>
                    (units)
                    <asp:RequiredFieldValidator ID="vldDuration" CssClass="validationMessage" runat="server" 
                        ErrorMessage="Duration field is required." 
                        ControlToValidate="lstDuration" InitialValue="invalid" EnableClientScript="False" >*</asp:RequiredFieldValidator> 
                </div> 

                <div>
                    <asp:Label ID="lblDoctor" CssClass="formLabel" runat="server" Text="Doctor:" />
                    <asp:DropDownList ID="lstDoctor" runat="server">
                        <asp:ListItem Text="Select" Selected="True" disabled="disabled" Value="invalid"/>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="vldDoctor" CssClass="validationMessage" runat="server" 
                        ErrorMessage="Select a doctor for the appointment." 
                        ControlToValidate="lstDoctor" InitialValue="invalid" EnableClientScript="False" >*</asp:RequiredFieldValidator>                   
                </div>   
            
                <div style="vertical-align: top;">                      
                    <asp:Label ID="lblNote" CssClass="formLabel" runat="server" Text="Notes:" />
                    <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" />       
                </div>         
            
                <div>    
                    <asp:Label ID="lblColor" CssClass="formLabel" runat="server" Text="Block Color:" />
                    <asp:DropDownList ID="lstColor" runat="server">
                        <asp:ListItem Text="Grey" Selected="True" Value="0"/>
                        <asp:ListItem Text="Blue" Value="1"/>
                        <asp:ListItem Text="Red" Value="2"/>
                        <asp:ListItem Text="Yellow" Value="3"/>
                        <asp:ListItem Text="Green" Value="4"/>
                    </asp:DropDownList>
                </div>  
            </div>

            <br />

            <div class="buttonBar">
                <asp:Button ID="btnBook" runat="server" Text="Book Appointment" OnClick="btnBook_Click"/>
            </div>
            <asp:ValidationSummary ID="vldSummary" CssClass="validationSummaryList" runat="server" />
        </div>

        <div class ="footerContainer">
            
        </div>
    </form>
</body>
</html>
