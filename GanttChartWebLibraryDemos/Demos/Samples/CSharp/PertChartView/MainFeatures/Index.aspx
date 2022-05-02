<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Demos.Samples.CSharp.PertChartView.MainFeatures.Index" %>
<%@ Register TagPrefix="pdpcc" Namespace="DlhSoft.Web.UI.WebControls.Pert" Assembly="DlhSoft.ProjectData.PertChart.ASP.Controls" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>PertChartView Main Features Sample</title>
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
            <div class="ribbonContainer">
                <div class="ribbonPanel">
                    <div class="ribbonHeader">Items</div>
                    <div class="ribbonCommandsArea">
                        <div class="ribbonCommand"><asp:ImageButton ID="SetCustomShapeColorToItemButton" runat="server" ImageUrl="Images/SetColor.png" ToolTip="Set custom shape color to Task event 2" AlternateText="Set color" OnClick="SetCustomShapeColorToItemButton_Click"/></div>
                        <div class="ribbonCommand"><asp:ImageButton ID="SetCustomDependencyLineColorToPredecessorItemButton" runat="server" ImageUrl="Images/SetDependencyColor.png" ToolTip="Set custom dependency line color to Task B" AlternateText="Set dependency color" OnClick="SetCustomDependencyLineColorToPredecessorItemButton_Click"/></div>
                    </div>
                </div>
                <div class="ribbonPanel">
                    <div class="ribbonHeader">Printing</div>
                    <div class="ribbonCommandsArea">
                        <div class="ribbonCommand"><asp:ImageButton ID="PrintButton" runat="server" ImageUrl="Images/Print.png" ToolTip="Print" AlternateText="Print" OnClick="PrintButton_Click"/></div>
                    </div>
                </div>
                <div class="ribbonPanel">
                    <div class="ribbonHeader">Project tools</div>
                    <div class="ribbonCommandsArea">
                        <div class="ribbonCommand"><asp:ImageButton ID="HighlightCriticalPathButton" runat="server" ImageUrl="Images/CriticalPath.png" ToolTip="Highlight critical path" AlternateText="Highlight critical path" OnClick="HighlightCriticalPathButton_Click"/></div>
                    </div>
                </div>
            </div>
            <pdpcc:PertChartView ID="PertChartView" runat="server" Height="388px" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
