<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Demos.Samples.CSharp.GanttChartView.WorkOptimizations.Index" %>
<%@ Register TagPrefix="pdgcc" Namespace="DlhSoft.Web.UI.WebControls" Assembly="DlhSoft.ProjectData.GanttChart.ASP.Controls" %>
<%@ Register TagPrefix="pdpcc" Namespace="DlhSoft.Web.UI.WebControls.Pert" Assembly="DlhSoft.ProjectData.PertChart.ASP.Controls" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>GanttChartView Work Optimizations Sample</title>
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
                        <td class="columnStyle"><asp:Button ID="OptimizeWorkButton" runat="server" Text="Optimize work" OnClick="OptimizeWorkButton_Click" /></td>
                        <td class="columnStyle"><asp:CheckBox ID="DependenciesOnlyCheckBox" runat="server" Text="Dependencies only" /></td>
                        <td class="columnStyle">Start on <input id="startTextBox" name="startTextBox" type="date" title="date" /></td>
                    </tr>
                    <tr>
                        <td class="columnStyle"><asp:Button ID="LevelResourcesButton" runat="server" Text="Level resources" OnClick="LevelResourcesButton_Click" /></td>
                        <td class="columnStyle"><asp:CheckBox ID="IncludeStartedTasksCheckBox" runat="server" Text="Include started tasks" /></td>
                    </tr>
                </table>
            </div>
            <pdgcc:GanttChartView ID="GanttChartView" runat="server" Height="388px"/>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
