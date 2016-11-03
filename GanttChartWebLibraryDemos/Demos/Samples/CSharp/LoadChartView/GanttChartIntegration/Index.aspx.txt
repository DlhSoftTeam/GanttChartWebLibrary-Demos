<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Demos.Samples.CSharp.LoadChartView.GanttChartIntegration.Index" %>
<%@ Register TagPrefix="pdgcc" Namespace="DlhSoft.Web.UI.WebControls" Assembly="DlhSoft.ProjectData.GanttChart.ASP.Controls" %>
<%@ Register TagPrefix="pdpcc" Namespace="DlhSoft.Web.UI.WebControls.Pert" Assembly="DlhSoft.ProjectData.PertChart.ASP.Controls" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>LoadChartView Gantt Chart Integration Sample</title>
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
            <asp:Panel ID="GanttChartViewContainer" runat="server">
                <!-- Gantt Chart component. -->
                <pdgcc:GanttChartView ID="GanttChartView" runat="server" Height="388px" />
                <asp:Button ID="AddNewGanttChartItemButton" runat="server" Text="Add new task" OnClick="AddNewGanttChartItemButton_Click" />
                <asp:Button ID="ShowLoadChartButton" runat="server" Text="Generate Load Chart" OnClick="ShowLoadChartButton_Click" />
            </asp:Panel>
            <asp:Panel ID="LoadChartViewContainer" runat="server" Visible="false">
                <!-- Load Chart component. -->
                <pdgcc:LoadChartView ID="LoadChartView" runat="server" Height="388px" />
                <asp:Button ID="HideLoadChartButton" runat="server" Text="Back to Gantt Chart source" OnClick="HideLoadChartButton_Click"></asp:Button>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="AddNewGanttChartItemButton" />
            <asp:PostBackTrigger ControlID="ShowLoadChartButton" />
            <asp:PostBackTrigger ControlID="HideLoadChartButton" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
