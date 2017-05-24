<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Demos.Samples.CSharp.GanttChartView.AssigningResources.Index" %>
<%@ Register TagPrefix="pdgcc" Namespace="DlhSoft.Web.UI.WebControls" Assembly="DlhSoft.ProjectData.GanttChart.ASP.Controls" %>
<%@ Register TagPrefix="pdpcc" Namespace="DlhSoft.Web.UI.WebControls.Pert" Assembly="DlhSoft.ProjectData.PertChart.ASP.Controls" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>GanttChartView Assigning Resources Sample</title>
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
                    <div class="ribbonCommand"><asp:ImageButton ID="DecreaseItemIndentationButton" runat="server" ImageUrl="Images/DecreaseIndentation.png" ToolTip="Decrease selected item indentation" AlternateText="Decrease indentation" OnClick="DecreaseItemIndentationButton_Click"/></div>
                    <div class="ribbonCommand"><asp:ImageButton ID="IncreaseItemIndentationButton" runat="server" ImageUrl="Images/IncreaseIndentation.png" ToolTip="Increase selected item indentation" AlternateText="Increase indentation" OnClick="IncreaseItemIndentationButton_Click"/></div>
                    <div class="ribbonCommand"><asp:ImageButton ID="DeleteItemButton" runat="server" ImageUrl="Images/Delete.png" ToolTip="Delete selected item" AlternateText="Delete" OnClick="DeleteItemButton_Click"/></div>
                </div>
            </div>
            <div class="ribbonPanel">
                <div class="ribbonHeader">Other views</div>
                <div class="ribbonCommandsArea">
                    <div class="ribbonCommand"><asp:ImageButton ID="LoadChartButton" runat="server" ImageUrl="Images/LoadChart.png" ToolTip="Load Chart" AlternateText="Load Chart" OnClick="LoadChartButton_Click"/></div>
                </div>
            </div>
        </div>
        <pdgcc:GanttChartView ID="GanttChartView" runat="server" Height="388px"/>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="LoadChartUpdatePanel" runat="server" RenderMode="Inline">
    <ContentTemplate>
        <asp:Panel ID="LoadChartPanel" runat="server" Visible="false">
            <div class="extraPanel">
                <div class="extraPanelCommandsArea">
                    <asp:LinkButton ID="CloseLoadChartViewButton" runat="server" Text="Close" OnClick="CloseLoadChartViewButton_Click"/>
                </div>
                <div class="extraPanelHeader">Load Chart</div>
                <div class="extraPanelContent">
                    Resource filter:
                    <select id="loadChartResourceFilter" onchange="loadChartResourceFilterChanged()">
                        <option value="">(All)</option>
                    </select>
                </div>
            </div>
            <pdgcc:LoadChartView ID="LoadChartView" runat="server" SelectionMode="None" IsMouseWheelZoomEnabled="false" Height="190px"/>
        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
