<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Demos.Samples.CSharp.ScheduleChartView.MainFeatures.Index" %>
<%@ Register TagPrefix="pdgcc" Namespace="DlhSoft.Web.UI.WebControls" Assembly="DlhSoft.ProjectData.GanttChart.ASP.Controls" %>
<%@ Register TagPrefix="pdpcc" Namespace="DlhSoft.Web.UI.WebControls.Pert" Assembly="DlhSoft.ProjectData.PertChart.ASP.Controls" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>ScheduleChartView Main Features Sample</title>
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
            <div class="ribbonContainer">
                <div class="ribbonPanel">
                    <div class="ribbonHeader">Items</div>
                    <div class="ribbonCommandsArea">
                        <div class="ribbonCommand"><asp:ImageButton ID="AddNewItemButton" runat="server" ImageUrl="Images/AddNew.png" ToolTip="Add new item" AlternateText="Add new" OnClick="AddNewItemButton_Click"/></div>
                        <div class="ribbonCommand"><asp:ImageButton ID="InsertNewItemButton" runat="server" ImageUrl="Images/InsertNew.png" ToolTip="Insert new item before selection" AlternateText="Insert new" OnClick="InsertNewItemButton_Click"/></div>
                        <div class="ribbonCommand"><asp:ImageButton ID="DeleteItemButton" runat="server" ImageUrl="Images/Delete.png" ToolTip="Delete selected item" AlternateText="Delete" OnClick="DeleteItemButton_Click"/></div>
                        <div class="ribbonCommand"><asp:ImageButton ID="SetCustomBarColorToItemButton" runat="server" ImageUrl="Images/SetColor.png" ToolTip="Set custom bar color to selected item" AlternateText="Set color" OnClick="SetCustomBarColorToItemButton_Click"/></div>
                        <div class="ribbonCommand"><asp:ImageButton ID="MoveItemUpButton" runat="server" ImageUrl="Images/MoveUp.png" ToolTip="Move selected item up" AlternateText="Move up" OnClick="MoveItemUpButton_Click"/></div>
                        <div class="ribbonCommand"><asp:ImageButton ID="MoveItemDownButton" runat="server" ImageUrl="Images/MoveDown.png" ToolTip="Move selected item down" AlternateText="Move down" OnClick="MoveItemDownButton_Click"/></div>
                    </div>
                </div>
                <div class="ribbonPanel">
                    <div class="ribbonHeader">Timeline/Schedule</div>
                    <div class="ribbonCommandsArea">
                        <div class="ribbonCommand"><asp:ImageButton ID="SetCustomScalesButton" runat="server" ImageUrl="Images/CustomScales.png" ToolTip="Set custom scales" AlternateText="Custom scales" OnClick="SetCustomScalesButton_Click"/></div>
                        <div class="ribbonCommand"><asp:ImageButton ID="ZoomInButton" runat="server" ImageUrl="Images/ZoomIn.png" ToolTip="Zoom in" AlternateText="Zoom in" OnClick="ZoomInButton_Click"/></div>
                        <div class="ribbonCommand"><asp:ImageButton ID="DecreaseTimelinePageButton" runat="server" ImageUrl="Images/DecreaseTimelinePage.png" ToolTip="Move towards past" AlternateText="Decrease timeline page" OnClick="DecreaseTimelinePageButton_Click"/></div>
                        <div class="ribbonCommand"><asp:ImageButton ID="IncreaseTimelinePageButton" runat="server" ImageUrl="Images/IncreaseTimelinePage.png" ToolTip="Move towards future" AlternateText="Increase timeline page" OnClick="IncreaseTimelinePageButton_Click"/></div>
                    </div>
                </div>
                <div class="ribbonPanel">
                    <div class="ribbonHeader">Printing</div>
                    <div class="ribbonCommandsArea">
                        <div class="ribbonCommand"><asp:ImageButton ID="PrintButton" runat="server" ImageUrl="Images/Print.png" ToolTip="Print" AlternateText="Print" OnClick="PrintButton_Click"/></div>
                    </div>
                </div>
            </div>
            <pdgcc:ScheduleChartView ID="ScheduleChartView" runat="server" Height="388px" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="AddNewItemButton" />
            <asp:PostBackTrigger ControlID="InsertNewItemButton" />
            <asp:PostBackTrigger ControlID="DeleteItemButton" />
            <asp:PostBackTrigger ControlID="SetCustomBarColorToItemButton" />
            <asp:PostBackTrigger ControlID="MoveItemUpButton" />
            <asp:PostBackTrigger ControlID="MoveItemDownButton" />
            <asp:PostBackTrigger ControlID="SetCustomScalesButton" />
            <asp:PostBackTrigger ControlID="ZoomInButton" />
            <asp:PostBackTrigger ControlID="DecreaseTimelinePageButton" />
            <asp:PostBackTrigger ControlID="IncreaseTimelinePageButton" />
            <asp:PostBackTrigger ControlID="PrintButton" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
