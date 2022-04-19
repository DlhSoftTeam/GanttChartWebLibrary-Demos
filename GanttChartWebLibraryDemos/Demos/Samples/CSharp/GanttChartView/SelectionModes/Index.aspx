<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Demos.Samples.CSharp.GanttChartView.SelectionModes.Index" %>
<%@ Register TagPrefix="pdgcc" Namespace="DlhSoft.Web.UI.WebControls" Assembly="DlhSoft.ProjectData.GanttChart.ASP.Controls" %>
<%@ Register TagPrefix="pdpcc" Namespace="DlhSoft.Web.UI.WebControls.Pert" Assembly="DlhSoft.ProjectData.PertChart.ASP.Controls" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>GanttChartView Selection Modes Sample</title>
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
            <div class="command-area">
                <div>
                    Selection:
                    <asp:DropDownList ID="SelectionModeDropDownList" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="SelectionModeDropDownList_SelectedIndexChanged">
                        <asp:ListItem Text="Not allowed" Value="None" Selected="true" />
                        <asp:ListItem Text="Single item, by clicking" Value="Focus" />
                        <asp:ListItem Text="Single item, using checkboxes" Value="Single" />
                        <asp:ListItem Text="Multiple items, by checkboxes" Value="Extended" />
                        <asp:ListItem Text="Multiple items, by clicking or using checkboxes" Value="ExtendedFocus" />
                    </asp:DropDownList>
                </div>
            </div>
            <pdgcc:GanttChartView ID="GanttChartView" runat="server" Height="388px"/>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
