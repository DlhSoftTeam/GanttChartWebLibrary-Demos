<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Demos.Samples.CSharp.ScheduleChartView.BuiltinScales.Index" %>
<%@ Register TagPrefix="pdgcc" Namespace="DlhSoft.Web.UI.WebControls" Assembly="DlhSoft.ProjectData.GanttChart.ASP.Controls" %>
<%@ Register TagPrefix="pdpcc" Namespace="DlhSoft.Web.UI.WebControls.Pert" Assembly="DlhSoft.ProjectData.PertChart.ASP.Controls" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>ScheduleChartView Built-in Scales Sample</title>
    <link rel="Stylesheet" href="app.css" type="text/css" />
    <script src="templates.js" type="text/javascript"></script>
    <script src="themes.js" type="text/javascript"></script>
    <script type="text/javascript">
        // Query string syntax: ?theme
        // Supported themes: Default, Generic-bright, Generic-blue, DlhSoft-gray, Purple-green, Steel-blue, Dark-black, Cyan-green, Blue-navy, Orange-brown, Teal-green, Purple-beige, Gray-blue, Aero.
        var queryString = window.location.search;
        var theme = queryString ? queryString.substr(1) : null;
    </script>
</head>
<body>
    <form id="form" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server"/>
    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
                        <div class="command-area">
                <table>
                    <tr>
                        <td></td>
                        <td>Type</td>
                        <td>Header format</td>
                        <td>Separator</td>
                        <td>Monday-based</td>
                        <td>Nonworking days</td>
                        <td>Current time</td>
                        <td>Zoom level</td>
                        <td>Update scale</td>
                    </tr>
                    <tr>
                        <td>Major scale</td>
                        <td>
                            <asp:DropDownList ID="MajorScaleTypeDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="MajorScaleTypeDropDownList_SelectedIndexChanged">
                                <asp:ListItem Value="Years" Text="Years" />
                                <asp:ListItem Value="Months" Text="Months" />
                                <asp:ListItem Value="Weeks" Text="Weeks" Selected="True" />
                                <asp:ListItem Value="Days" Text="Days" />
                                <asp:ListItem Value="Hours" Text="Hours" />
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="MajorScaleHeaderFormatDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="MajorScaleHeaderFormatDropDownList_SelectedIndexChanged">
                                <asp:ListItem Value="DateTime" Text="DateTime" />
                                <asp:ListItem Value="Date" Text="Date" Selected="True" />
                                <asp:ListItem Value="Hour" Text="Hour" />
                                <asp:ListItem Value="DayOfWeek" Text="DayOfWeek" />
                                <asp:ListItem Value="DayOfWeekAbbreviation" Text="DayOfWeekAbbreviation" />
                                <asp:ListItem Value="Day" Text="Day" />
                                <asp:ListItem Value="Month" Text="Month" />
                                <asp:ListItem Value="MonthAbbreviation" Text="MonthAbbreviation" />
                                <asp:ListItem Value="Year" Text="Year" />
                                <asp:ListItem Value="MonthYear" Text="MonthYear" />
                                <asp:ListItem Value="Localized" Text="Localized" />
                            </asp:DropDownList>
                        </td>
                        <td><asp:CheckBox ID="MajorScaleSeparatorCheckBox" runat="server" AutoPostBack="true" OnCheckedChanged="MajorScaleSeparatorCheckBox_CheckedChanged" /></td>
                        <td><asp:CheckBox ID="MondayBasedCheckBox" runat="server" AutoPostBack="true" checked="true" OnCheckedChanged="MondayBasedCheckBox_CheckedChanged" /></td>
                        <td><asp:CheckBox ID="NonworkingDaysCheckBox" runat="server" AutoPostBack="true" checked="true" OnCheckedChanged="NonworkingDaysCheckBox_CheckedChanged" /></td>
                        <td><asp:CheckBox ID="CurrentTimeCheckBox" runat="server" AutoPostBack="true" checked="true" OnCheckedChanged="CurrentTimeCheckBox_CheckedChanged" /></td>
                        <td><asp:TextBox ID="ZoomLevelTextBox" runat="server" AutoPostBack="true" TextMode="Number" Text="5" OnTextChanged="ZoomLevelTextBox_TextChanged" /></td>
                        <td>
                            <asp:DropDownList ID="UpdateScaleDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="UpdateScaleDropDownList_SelectedIndexChanged">
                                <asp:ListItem Value="Free" Text="Free" />
                                <asp:ListItem Value="15 min" Text="15 min" Selected="True" />
                                <asp:ListItem Value="Hour" Text="Hour" />
                                <asp:ListItem Value="Day" Text="Day" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Minor scale</td>
                        <td>
                            <asp:DropDownList ID="MinorScaleTypeDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="MinorScaleTypeDropDownList_SelectedIndexChanged">
                                <asp:ListItem Value="Years" Text="Years" />
                                <asp:ListItem Value="Months" Text="Months" />
                                <asp:ListItem Value="Weeks" Text="Weeks" />
                                <asp:ListItem Value="Days" Text="Days" Selected="True" />
                                <asp:ListItem Value="Hours" Text="Hours" />
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="MinorScaleHeaderFormatDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="MinorScaleHeaderFormatDropDownList_SelectedIndexChanged">
                                <asp:ListItem Value="DateTime" Text="DateTime" />
                                <asp:ListItem Value="Date" Text="Date" />
                                <asp:ListItem Value="Hour" Text="Hour" />
                                <asp:ListItem Value="DayOfWeek" Text="DayOfWeek" />
                                <asp:ListItem Value="DayOfWeekAbbreviation" Text="DayOfWeekAbbreviation" Selected="True" />
                                <asp:ListItem Value="Day" Text="Day" />
                                <asp:ListItem Value="Month" Text="Month" />
                                <asp:ListItem Value="MonthAbbreviation" Text="MonthAbbreviation" />
                                <asp:ListItem Value="Year" Text="Year" />
                                <asp:ListItem Value="MonthYear" Text="MonthYear" />
                                <asp:ListItem Value="Localized" Text="Localized" />
                            </asp:DropDownList>
                        </td>
                        <td><asp:CheckBox ID="MinorScaleSeparatorCheckBox" runat="server" AutoPostBack="true" OnCheckedChanged="MinorScaleSeparatorCheckBox_CheckedChanged" /></td>
                    </tr>
                </table>
            </div>
            <pdgcc:ScheduleChartView ID="ScheduleChartView" runat="server" Height="388px" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
