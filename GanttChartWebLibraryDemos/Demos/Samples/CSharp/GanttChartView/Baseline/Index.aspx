<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Demos.Samples.CSharp.GanttChartView.Baseline.Index" %>
<%@ Register TagPrefix="pdgcc" Namespace="DlhSoft.Web.UI.WebControls" Assembly="DlhSoft.ProjectData.GanttChart.ASP.Controls" %>
<%@ Register TagPrefix="pdpcc" Namespace="DlhSoft.Web.UI.WebControls.Pert" Assembly="DlhSoft.ProjectData.PertChart.ASP.Controls" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>GanttChartView Baseline Sample</title>
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
            <pdgcc:GanttChartView ID="GanttChartView" runat="server" Height="388px"/>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
