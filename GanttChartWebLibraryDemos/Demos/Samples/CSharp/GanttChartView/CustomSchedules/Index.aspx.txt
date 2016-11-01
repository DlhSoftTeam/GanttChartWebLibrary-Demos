<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Demos.Samples.CSharp.GanttChartView.CustomSchedules.Index" %>
<%@ Register TagPrefix="pdgcc" Namespace="DlhSoft.Web.UI.WebControls" Assembly="DlhSoft.ProjectData.GanttChart.ASP.Controls" %>
<%@ Register TagPrefix="pdpcc" Namespace="DlhSoft.Web.UI.WebControls.Pert" Assembly="DlhSoft.ProjectData.PertChart.ASP.Controls" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>GanttChartView Custom Schedules Sample</title>
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
            <div class="info-area">
                <table>
                    <tr>
                        <td></td>
                        <td>Working week</td>
                        <td>Working day</td>
                        <td>Holidays</td>
                    </tr>
                    <tr>
                        <td>Main schedule</td>
                        <td>Tuesday-Thursday</td>
                        <td>06:00-13:30</td>
                        <td>15, 16, 19</td>
                    </tr>
                    <tr>
                        <td>Task 2.2.1</td>
                        <td>Wednesday-Saturday</td>
                        <td>11:30-19:45</td>
                        <td>18, 19, 22, 23</td>
                    </tr>
                    <tr>
                        <td>Task 2.2.2</td>
                        <td>Sunday-Saturday</td>
                        <td>Main schedule</td>
                        <td>Main schedule</td>
                    </tr>
                </table>
            </div>
            <pdgcc:GanttChartView ID="GanttChartView" runat="server" Height="388px"/>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
