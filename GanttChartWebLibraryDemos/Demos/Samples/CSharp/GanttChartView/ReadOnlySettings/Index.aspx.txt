<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Demos.Samples.CSharp.GanttChartView.ReadOnlySettings.Index" %>
<%@ Register TagPrefix="pdgcc" Namespace="DlhSoft.Web.UI.WebControls" Assembly="DlhSoft.ProjectData.GanttChart.ASP.Controls" %>
<%@ Register TagPrefix="pdpcc" Namespace="DlhSoft.Web.UI.WebControls.Pert" Assembly="DlhSoft.ProjectData.PertChart.ASP.Controls" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>GanttChartView Read Only Settings Sample</title>
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
                        <td>
                            <div><asp:CheckBox ID="IsReadOnlyCheckBox" runat="server" Text="Read only (override)" AutoPostBack="true" OnCheckedChanged="IsReadOnlyCheckBox_CheckedChanged" /></div>
                            <div><asp:CheckBox ID="IsGridReadOnlyCheckBox" runat="server" Text="Grid read only" AutoPostBack="true" OnCheckedChanged="IsGridReadOnlyCheckBox_CheckedChanged" /></div>
                            <div><asp:CheckBox ID="IsChartReadOnlyCheckBox" runat="server" Text="Chart read only" AutoPostBack="true" OnCheckedChanged="IsChartReadOnlyCheckBox_CheckedChanged" /></div>
                            <div><asp:CheckBox ID="HideGridCheckBox" runat="server" Text="Hide grid" AutoPostBack="true" OnCheckedChanged="HideGridCheckBox_CheckedChanged" /></div>
                        </td>
                        <td>
                            <div><asp:CheckBox ID="IsContentReadOnlyCheckBox" runat="server" Text="Content column read only" AutoPostBack="true" OnCheckedChanged="IsContentReadOnlyCheckBox_CheckedChanged" /></div>
                            <div><asp:CheckBox ID="AreSchedulingColumnsReadOnlyCheckBox" runat="server" Text="Scheduling columns read only" AutoPostBack="true" OnCheckedChanged="AreSchedulingColumnsReadOnlyCheckBox_CheckedChanged" /></div>
                            <div><asp:CheckBox ID="AreAssignmentsReadOnlyCheckBox" runat="server" Text="Assignments column read only" AutoPostBack="true" OnCheckedChanged="AreAssignmentsReadOnlyCheckBox_CheckedChanged" /></div>
                            <div><asp:CheckBox ID="IsEffortPreservedWhenStartChangesInGridCheckBox" runat="server" Text="Effort is preserved when changing start from grid" AutoPostBack="true" OnCheckedChanged="IsEffortPreservedWhenStartChangesInGridCheckBox_CheckedChanged" /></div>
                        </td>
                        <td>
                            <div><asp:CheckBox ID="IsStartReadOnlyCheckBox" runat="server" Text="Start read only in chart" AutoPostBack="true" OnCheckedChanged="IsStartReadOnlyCheckBox_CheckedChanged" /></div>
                            <div><asp:CheckBox ID="IsCompletionReadOnlyCheckBox" runat="server" Text="Completion read only in chart" AutoPostBack="true" OnCheckedChanged="IsCompletionReadOnlyCheckBox_CheckedChanged" /></div>
                            <div><asp:CheckBox ID="IsEffortReadOnlyCheckBox" runat="server" Text="Duration read only in chart" AutoPostBack="true" OnCheckedChanged="IsEffortReadOnlyCheckBox_CheckedChanged" /></div>
                            <div><asp:CheckBox ID="DisableStartEndDraggingCheckBox" runat="server" Text="Disable updating duration by start dragging" AutoPostBack="true" OnCheckedChanged="DisableStartEndDraggingCheckBox_CheckedChanged" /></div>
                        </td>
                        <td>
                            <div><asp:CheckBox ID="HideDependenciesCheckBox" runat="server" Text="Hide dependencies" AutoPostBack="true" OnCheckedChanged="HideDependenciesCheckBox_CheckedChanged" /></div>
                            <div><asp:CheckBox ID="DisableCreatingStartDependenciesCheckBox" runat="server" Text="Disable creating start dependencies" AutoPostBack="true" OnCheckedChanged="DisableCreatingStartDependenciesCheckBox_CheckedChanged" /></div>
                            <div><asp:CheckBox ID="AreDependenciesReadOnlyCheckBox" runat="server" Text="Dependencies read only" AutoPostBack="true" OnCheckedChanged="AreDependenciesReadOnlyCheckBox_CheckedChanged" /></div>
                            <div><asp:CheckBox ID="DisableScrollingOnTaskClickCheckBox" runat="server" Text="Disable chart scrolling on grid row clicking" AutoPostBack="true" OnCheckedChanged="DisableScrollingOnTaskClickCheckBox_CheckedChanged" /></div>
                        </td>
                        <td>
                            <p>Selected item commands:</p>
                            <div><asp:Button ID="SelectedItemAsReadOnlyButton" runat="server" Text="Set as read only (override)" OnClick="SelectedItemAsReadOnlyButton_Click" /></div>
                            <div><asp:Button ID="SelectedItemBarAsReadOnlyButton" runat="server" Text="Set chart bar as read only" OnClick="SelectedItemBarAsReadOnlyButton_Click" /></div>
                        </td>
                    </tr>
                </table>
            </div>
            <pdgcc:GanttChartView ID="GanttChartView" runat="server" Height="388px"/>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
