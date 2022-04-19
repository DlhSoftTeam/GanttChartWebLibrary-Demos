<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Demos.Samples.CSharp.ScheduleChartView.ChangeNotifications.Index" %>
<%@ Register TagPrefix="pdgcc" Namespace="DlhSoft.Web.UI.WebControls" Assembly="DlhSoft.ProjectData.GanttChart.ASP.Controls" %>
<%@ Register TagPrefix="pdpcc" Namespace="DlhSoft.Web.UI.WebControls.Pert" Assembly="DlhSoft.ProjectData.PertChart.ASP.Controls" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>ScheduleChartView Change Notifications Sample</title>
    <link rel="Stylesheet" href="app.css" type="text/css" />
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
            <pdgcc:ScheduleChartView ID="ScheduleChartView" runat="server" Height="388px" />
            <div class="extraPanel">
                <div class="extraPanelHeader">Changes and server side notifications</div>
                <div class="extraPanelContent" style="margin: 4px 0px 4px 0px">
                    <div style="margin: 4px 0px 4px 0px">
                        <asp:Button ID="SubmitButton" runat="server" Text="Submit to server"/><br />
                        <asp:TextBox ID="NotificationsTextBox" runat="server" TextMode="MultiLine" ReadOnly="true" Columns="120" Rows="12" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
