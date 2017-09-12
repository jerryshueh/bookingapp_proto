<%@ Page Language="C#" AutoEventWireup="true" CodeFile="searchpatient.aspx.cs" Inherits="SearchPatient" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search Patients</title>
    <link href="Content/Styles/basicpagestyle.css" rel="stylesheet" type="text/css" />
    <link href="Content/Styles/searchpagestyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmSearchPatient" runat="server">
        <div class="headerContainer">
            <div style="display:inline-block;">
                <h1>Search Patient</h1>
            </div>
            <div style="display:inline-block; float: right; padding:10px; border: 1px solid #999; background-color:#333">
                <asp:HyperLink ID="lnkSchedule" runat="server" ForeColor="White" NavigateUrl="~/scheduler.aspx">Back to Schedule</asp:HyperLink>
            </div>
        </div>

        <div class="mainContainer">
            Search patients using the fields below. Leave all blank to view entire list.
            <br />
            <br />

            <div class="formFields">
                <div>
                    <asp:Label ID="lblPID" CssClass="formLabel" runat="server" Text="Patient ID:"/>
                    <asp:TextBox ID="txtPID" runat="server" />
                    <asp:RegularExpressionValidator ID="vldPIDInt" runat="server" CssClass="validationMessage" ErrorMessage="Patient ID cannot contain non-numerical characters" ControlToValidate="txtPID" ValidationExpression="\d*" EnableClientScript="False" Display="Dynamic">*</asp:RegularExpressionValidator>
                </div>
                <div>
                    <asp:Label ID="lblLName" CssClass="formLabel" runat="server" Text="Last Name:" />
                    <asp:TextBox ID="txtLName" runat="server" />
                    <asp:RegularExpressionValidator ID="vldLNameString" runat="server" CssClass="validationMessage" ErrorMessage="Last name cannot contain non-alphabetical characters." ControlToValidate="txtLName" ValidationExpression="[a-zA-Z]*" EnableClientScript="False" Display="Dynamic">*</asp:RegularExpressionValidator>
                </div>
                <div>
                    <asp:Label ID="lblFName" CssClass="formLabel" runat="server" Text="First Name:" />
                    <asp:TextBox ID="txtFName" runat="server" />
                    <asp:RegularExpressionValidator ID="vldFNameString" runat="server" CssClass="validationMessage" ErrorMessage="First name cannot contain non-alphabetical characters." ControlToValidate="txtFName" ValidationExpression="[a-zA-Z]*" EnableClientScript="False" Display="Dynamic">*</asp:RegularExpressionValidator>
                </div>
                <div>
                    <asp:Label ID="lbl_sex" CssClass="formLabel" runat="server" Text="Sex:" />
                    <asp:DropDownList ID="lstSex" runat="server">
                        <asp:ListItem Text="Select" Selected="True" Value="invalid"/>
                        <asp:ListItem Text="Male" Value="M"/>
                        <asp:ListItem Text="Female" Value="F"/>
                    </asp:DropDownList>
                </div>
                <div>
                    <asp:Label ID="lblDob" CssClass="formLabel" runat="server" Text="Date of Birth:" />
                    <asp:DropDownList ID="lstDOBYear" runat="server">
                        <asp:ListItem Text="Year" Selected="True" Value="invalid"></asp:ListItem>
                    </asp:DropDownList>-
                    <asp:DropDownList ID="lstDOBMonth" runat="server">
                        <asp:ListItem Text="Month" Selected="True" Value="invalid"></asp:ListItem>
                    </asp:DropDownList>-
                    <asp:DropDownList ID="lstDOBDay" runat="server">
                        <asp:ListItem Text="Day" Selected="True" Value="invalid"></asp:ListItem>
                    </asp:DropDownList>  
                </div>
                <div>
                    <asp:Label ID="lblPhoneNum" CssClass="formLabel" runat="server" Text="Primary Phone:" />
                    <asp:TextBox ID="txtPhone" runat="server" />
                    <asp:RadioButton id="rdoHomePhone" Text="Home" Checked="True" GroupName="bestPhone" runat="server"/>
                    <asp:RadioButton id="rdoWorkPhone" Text="Work" GroupName="bestPhone" runat="server"/>
                    <asp:RadioButton id="rdoMobilePhone" Text="Mobile"
                    GroupName="bestPhone" runat="server"/>
                </div>
                <div>
                    <asp:Label ID="lblEmail" CssClass="formLabel" runat="server" Text="Primary Email:" />
                    <asp:TextBox ID="txtEmail" runat="server" />
                </div>
            </div>

            <br />

            <div class ="buttonBar">
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"/>
                <asp:Button ID="btnReset" runat="server" Text="Clear Fields" OnClick="btnReset_Click" CausesValidation="False"/>
                <asp:Button ID="btnNew" runat="server" Text="Create New" OnClick="btnNew_Click" CausesValidation="False"/>
            </div>
            <asp:ValidationSummary ID="vldSummary" CssClass="validationSummaryList" runat="server" />

            <br />
            Result(s):
            <br />
            <br />

            <asp:Table ID="tblDisplay" CssClass ="tableDisplay" runat="server">
                <asp:TableRow runat="server" CssClass="cellHeader">
                    <asp:TableCell CssClass="cellPID">
                        Patient ID
                    </asp:TableCell>  
                    <asp:TableCell CssClass="cellSex">
                        Sex
                    </asp:TableCell>        
                    <asp:TableCell CssClass="cellFName">
                        First Name
                    </asp:TableCell>        
                    <asp:TableCell CssClass="cellLName">
                        Last Name
                    </asp:TableCell>      
                    <asp:TableCell CssClass="cellDOB">
                        Date of Birth
                    </asp:TableCell>      
                    <asp:TableCell CssClass="cellPhone">
                        Primary Phone
                    </asp:TableCell>      
                    <asp:TableCell CssClass="cellEmail">
                        Email Address
                    </asp:TableCell>    
                </asp:TableRow>
            </asp:Table >
            <input type="hidden" id="selectedPatient" runat="server" />
            <br />
            <div class ="buttonBar">
                <asp:Button ID="btnView" runat="server" Text="View Full Profile" CausesValidation="False" OnClick="btnView_Click" Enabled="False"/>
            </div>
        </div>

        <div class ="footerContainer">

        </div>
    </form>

    <script>
        function rowClick(id) {
            var prev = document.getElementById("selectedPatient").value;
            if (document.getElementById(prev)) {
                document.getElementById(prev).removeAttribute("style");

                var c = document.getElementById(prev).children;
                for (i = 0; i < c.length; i++) {
                    c[i].removeAttribute("style");
                }
            }

            document.getElementById(id).style.backgroundColor = "#333";

            var c = document.getElementById(id).children;
            for (i = 0; i < c.length; i++) {
                c[i].style.color = "#fff";
            }

            document.getElementById("selectedPatient").value = id;
            document.getElementById("btnView").disabled = false;
        }
	</script>
</body>
</html>
