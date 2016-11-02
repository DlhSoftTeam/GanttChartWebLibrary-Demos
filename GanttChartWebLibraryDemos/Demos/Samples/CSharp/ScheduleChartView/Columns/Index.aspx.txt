<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Demos.Samples.CSharp.ScheduleChartView.Columns.Index" %>
<%@ Register TagPrefix="pdgcc" Namespace="DlhSoft.Web.UI.WebControls" Assembly="DlhSoft.ProjectData.GanttChart.ASP.Controls" %>
<%@ Register TagPrefix="pdpcc" Namespace="DlhSoft.Web.UI.WebControls.Pert" Assembly="DlhSoft.ProjectData.PertChart.ASP.Controls" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>ScheduleChartView Columns Sample</title>
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
                        <div class="ribbonCommand"><asp:ImageButton ID="MoveItemUpButton" runat="server" ImageUrl="Images/MoveUp.png" ToolTip="Move selected item up" AlternateText="Move up" OnClick="MoveItemUpButton_Click"/></div>
                        <div class="ribbonCommand"><asp:ImageButton ID="MoveItemDownButton" runat="server" ImageUrl="Images/MoveDown.png" ToolTip="Move selected item down" AlternateText="Move down" OnClick="MoveItemDownButton_Click"/></div>
                    </div>
                </div>
            </div>
            <pdgcc:ScheduleChartView ID="ScheduleChartView" runat="server" Height="388px" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="AddNewItemButton"/>
            <asp:PostBackTrigger ControlID="InsertNewItemButton"/>
            <asp:PostBackTrigger ControlID="DeleteItemButton"/>
            <asp:PostBackTrigger ControlID="MoveItemUpButton"/>
            <asp:PostBackTrigger ControlID="MoveItemDownButton"/>
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
