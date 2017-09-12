<%@ Page Language="C#" AutoEventWireup="true" CodeFile="scheduler.aspx.cs" Inherits="Scheduler" %>

<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Scheduler</title>
    <link href="Content/Styles/basicpagestyle.css" rel="stylesheet" type="text/css" />
    <link href="Content/Styles/schedulepagestyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmSearchPatient" runat="server">
        <div class="headerContainer">
            <div style="display:inline-block;">
                <h1>Scheduler</h1>
            </div>
            <div style="display:inline-block; float: right; padding:10px; border: 1px solid #999; background-color:#333">
                <asp:HyperLink ID="lnkSearch" runat="server" ForeColor="White" NavigateUrl="~/searchpatient.aspx">Search Patients</asp:HyperLink>
            </div>
        </div>

        <div class="mainContainer">
            <br />
            <div style="text-align: center;">
                <asp:Button ID="btnBackDate" runat="server" Text="<" OnClick="btnBackDate_Click" />
                <asp:Label ID="litCurrentDate" ForeColor="#333" runat="server" Font-Bold="True" Font-Size="X-Large" style="margin:0;"/>
                <asp:Button ID="btnForwardDate" runat="server" Text=">" OnClick="btnForwardDate_Click" />
            </div>
            <br />
            <div class="schedWrap">
                <asp:Table ID="tblTimeline" CssClass="tblTimeColumn" runat="server">
                    <asp:TableRow ID="rowTimelineHeader" runat="server">
                        <asp:TableCell CssClass="cellHeader" ColumnSpan="2">Time</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <asp:Table ID="tblSched1" CssClass="tblSchedColumn" runat="server" ViewStateMode="Disabled">
                    <asp:TableRow ID="rowSched1Header" runat="server">
                        <asp:TableCell CssClass="cellHeader">Header 1</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <asp:Table ID="tblSched2" CssClass="tblSchedColumn" runat="server" ViewStateMode="Disabled">
                    <asp:TableRow ID="rowSched2Header" runat="server">
                        <asp:TableCell CssClass="cellHeader">Header 2</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <asp:Table ID="tblSched3" CssClass="tblSchedColumn" runat="server" ViewStateMode="Disabled">
                    <asp:TableRow ID="rowSched3Header" runat="server">
                        <asp:TableCell CssClass="cellHeader">Header 3</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>

            <div class="rightBar">
                <div>
                    <asp:Calendar ID="cldPickDate" CssClass="calendar" runat="server" Height="180px" Width="200px" OnSelectionChanged="cldPickDate_SelectionChanged" BackColor="White" BorderColor="#999999" CellPadding="0" DayNameFormat="FirstTwoLetters" Font-Names="Segoe UI" Font-Size="9pt" ForeColor="Black" BorderStyle="Solid" Font-Overline="False" Font-Strikeout="False" NextMonthText="&amp;#9654;" PrevMonthText="&amp;#9664;">
                        <DayHeaderStyle BackColor="#555555" ForeColor="#FFFFFF" Font-Size="Small" />
                        <NextPrevStyle  ForeColor="#333333" VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#808080" />
                        <SelectedDayStyle BackColor="#999999" ForeColor="White" />
                        <SelectorStyle BackColor="#CCCCCC" />
                        <TitleStyle BackColor="#009966" ForeColor="White" BorderColor="Black" Font-Bold="True" />
                        <TodayDayStyle ForeColor="#006666" Font-Bold="True" />
                        <WeekendDayStyle BackColor="#EEEEEE" ForeColor="#CC6666 " />
                    </asp:Calendar>
                </div>
                <br />
                <div>
                    <asp:Label runat="server" Text="Jump to Date:"/>
                </div>
                <br />
                <div style="text-align:center;">
                    <asp:Button ID="btnJumpBackM" runat="server" Text=" < " OnClick="btnJumpBackM_Click" />
                    <asp:TextBox ID="txtJumpMonth" runat="server" Width="3em" />
                    <asp:Label Text="Month(s)" Width="5em" runat="server" />
                    <asp:Button ID="btnJumpForwardM" runat="server" Text=" > " OnClick="btnJumpForwardM_Click" />
                </div>
                <div  style="text-align:center;">
                    <asp:Button ID="btnJumpBackW" runat="server" Text=" < " OnClick="btnJumpBackW_Click" />
                    <asp:TextBox ID="txtJumpWeek" runat="server" Width="3em" />
                    <asp:Label Text="Week(s)" Width="5em" runat="server" />
                    <asp:Button ID="btnJumpForwardW" runat="server" Text=" > " OnClick="btnJumpForwardW_Click" />
                </div>
                <div  style="text-align:center;">
                    <asp:Button ID="btnJumpBackD" runat="server" Text=" < " OnClick="btnJumpBackD_Click" />
                    <asp:TextBox ID="txtJumpDay" runat="server" Width="3em" />
                    <asp:Label Text="Day(s)" Width="5em" runat="server" />
                    <asp:Button ID="btnJumpForwardD" runat="server" Text=" > " OnClick="btnJumpForwardD_Click" />
                </div>
                <div style="text-align:center; margin-top: 10px;">
                    <asp:Button ID="btnJumpToday" runat="server" Text="Jump to Today" OnClick="btnJumpToday_Click" />
                </div>
            </div>
            <br />
            <asp:ValidationSummary ID="vldSummary" CssClass="validationSummaryList" runat="server" />
            <br />
            <asp:Label ID="lblTest1" runat="server" Width="200px" Text=""></asp:Label>
            <asp:Label ID="lblTest2" runat="server" Width="200px" Text=""></asp:Label>
            <asp:Label ID="lblTest3" runat="server" Width="200px" Text=""></asp:Label>
            <br />
        </div>

        <div class ="footerContainer">

        </div>
    </form>
</body>
</html>
