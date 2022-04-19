<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Demos.Samples.CSharp.GanttChartView.MainFeatures.Index" %>
<%@ Register TagPrefix="pdgcc" Namespace="DlhSoft.Web.UI.WebControls" Assembly="DlhSoft.ProjectData.GanttChart.ASP.Controls" %>
<%@ Register TagPrefix="pdpcc" Namespace="DlhSoft.Web.UI.WebControls.Pert" Assembly="DlhSoft.ProjectData.PertChart.ASP.Controls" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>GanttChartView Sample</title>
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
    <form id="form" runat="server" enctype="multipart/form-data">
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
                    <div class="ribbonCommand"><asp:ImageButton ID="SetCustomBarColorToItemButton" runat="server" ImageUrl="Images/SetColor.png" ToolTip="Set custom bar color to selected item" AlternateText="Set color" OnClick="SetCustomBarColorToItemButton_Click"/></div>
                    <div class="ribbonCommand"><asp:ImageButton ID="CopyImageButton" runat="server" ImageUrl="Images/Copy.png" ToolTip="Copy selected item" AlternateText="Copy" OnClick="CopyItemButton_Click"/></div>
                    <div class="ribbonCommand"><asp:ImageButton ID="PasteImageButton" runat="server" ImageUrl="Images/Paste.png" ToolTip="Paste after selected item" AlternateText="Paste" OnClick="PasteItemButton_Click"/></div>
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
                <div class="ribbonHeader">Project tools</div>
                <div class="ribbonCommandsArea">
                    <div class="ribbonCommand"><asp:ImageButton ID="ToggleBaselineButton" runat="server" CssClass="toggle pressed" ImageUrl="Images/Baseline.png" ToolTip="Hide/display baseline" AlternateText="Baseline" OnClick="ToggleBaselineButton_Click"/></div>
                    <div class="ribbonCommand"><asp:ImageButton ID="HighlightCriticalPathButton" runat="server" CssClass="toggle" ImageUrl="Images/CriticalPath.png" ToolTip="Highlight/refresh critical path" AlternateText="Highlight critical path" OnClick="HighlightCriticalPathButton_Click"/></div>
                    <div class="ribbonCommand"><asp:ImageButton ID="SplitRemainingWorkButton" runat="server" ImageUrl="Images/SplitRemainingWork.png" ToolTip="Split work upon completion point in order to be able to reschedule remaining work separately" AlternateText="Split remaining work" OnClick="SplitRemainingWorkButton_Click"/></div>
                    <div class="ribbonCommand"><asp:ImageButton ID="ToggleDependencyConstraintsButton" runat="server" CssClass="toggle pressed" ImageUrl="Images/DependencyConstraints.png" ToolTip="Disable/enable automatic scheduling" AlternateText="Dependency constraints" OnClick="ToggleDependencyConstraintsButton_Click"/></div>
                    <div class="ribbonCommand"><asp:ImageButton ID="LevelResourcesButton" runat="server" ImageUrl="Images/LevelResources.png" ToolTip="Level resources" AlternateText="Level resources" OnClick="LevelResourcesButton_Click"/></div>
                </div>
            </div>
            <div class="ribbonPanel">
                <div class="ribbonHeader">Other views</div>
                <div class="ribbonCommandsArea">
                    <div class="ribbonCommand"><asp:ImageButton ID="ScheduleChartButton" runat="server" ImageUrl="Images/ScheduleChart.png" ToolTip="Schedule Chart" AlternateText="Schedule Chart" OnClick="ScheduleChartButton_Click"/></div>
                    <div class="ribbonCommand"><asp:ImageButton ID="LoadChartButton" runat="server" ImageUrl="Images/LoadChart.png" ToolTip="Load Chart" AlternateText="Load Chart" OnClick="LoadChartButton_Click"/></div>
                    <div class="ribbonCommand"><asp:ImageButton ID="PertChartButton" runat="server" ImageUrl="Images/PertChart.png" ToolTip="PERT Chart" AlternateText="PERT Chart" OnClick="PertChartButton_Click"/></div>
                    <div class="ribbonCommand"><asp:ImageButton ID="NetworkDiagramButton" runat="server" ImageUrl="Images/NetworkDiagram.png" ToolTip="Network Diagram" AlternateText="Network Diagram" OnClick="NetworkDiagramButton_Click"/></div>
                    <div class="ribbonCommand"><asp:ImageButton ID="ProjectStatisticsButton" runat="server" ImageUrl="Images/ProjectStatistics.png" ToolTip="Project Statistics" AlternateText="Project statistics" OnClick="ProjectStatisticsButton_Click"/></div>
                </div>
            </div>
            <div class="ribbonPanel">
                <div class="ribbonHeader">Files and printing</div>
                <div class="ribbonCommandsArea">
                    <div class="ribbonCommand"><asp:ImageButton ID="LoadProjectXmlButton" runat="server" ImageUrl="Images/LoadProjectXml.png" ToolTip="Load existing Project XML file" AlternateText="Load Project XML" OnClick="LoadProjectXmlButton_Click"/></div>
                    <div class="ribbonCommand"><asp:ImageButton ID="SaveProjectXmlButton" runat="server" ImageUrl="Images/SaveProjectXml.png" ToolTip="Export as Project XML file" AlternateText="Save Project XML" OnClick="SaveProjectXmlButton_Click"/></div>
                    <div class="ribbonCommand"><asp:ImageButton ID="PrintButton" runat="server" ImageUrl="Images/Print.png" ToolTip="Print" AlternateText="Print" OnClick="PrintButton_Click"/></div>
                </div>
            </div>
        </div>
        <!-- Items and settings are set in code behind. -->
        <!-- Optionally, in order to improve performance by leveraging browser caching features on the client side, you may add DlhSoft.ProjectData.GanttChart.HTML.Controls.js file (from the DlhSoft product installation folder) as an item within your Web application, and set JavaScriptLibraryUrl property of the component to a value indicating its relative path. -->
        <pdgcc:GanttChartView ID="GanttChartView" runat="server" Height="388px"/>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="LoadProjectXmlUpdatePanel" runat="server" RenderMode="Inline">
    <ContentTemplate>
        <asp:Panel ID="LoadProjectXmlPanel" runat="server" CssClass="extraPanel" Visible="false">
            <div class="extraPanelHeader">Load Project XML</div>
            <div class="extraPanelContent">
                Select an existing Project XML file to load: <asp:FileUpload ID="LoadProjectXmlFileUpload" runat="server"/>
                <asp:Button ID="LoadProjectXmlSubmitButton" runat="server" Text="Submit" ToolTip="Submit the selected Project XML file" OnClick="LoadProjectXmlSubmitButton_Click"/>
                <asp:LinkButton ID="CloseLoadProjectXmlButton" runat="server" Text="Cancel" OnClick="CloseLoadProjectXmlButton_Click"/>
            </div>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="LoadProjectXmlSubmitButton"/>
    </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="ScheduleChartUpdatePanel" runat="server" RenderMode="Inline">
    <ContentTemplate>
        <asp:Panel ID="ScheduleChartPanel" runat="server" Visible="false">
            <div class="extraPanel">
                <div class="extraPanelCommandsArea">
                    <asp:LinkButton ID="CloseScheduleChartViewButton" runat="server" Text="Close" OnClick="CloseScheduleChartViewButton_Click"/>
                </div>
                <div class="extraPanelHeader">Schedule Chart</div>
            </div>
            <pdgcc:ScheduleChartView ID="ScheduleChartView" runat="server" IsReadOnly="true" SelectionMode="None" IsMouseWheelZoomEnabled="false" Height="190px"/>
        </asp:Panel>
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
    <asp:UpdatePanel ID="PertChartUpdatePanel" runat="server" RenderMode="Inline">
    <ContentTemplate>
        <asp:Panel ID="PertChartPanel" runat="server" Visible="false" EnableViewState="false">
            <div class="extraPanel">
                <div class="extraPanelCommandsArea">
                    <asp:LinkButton ID="ClosePertChartViewButton" runat="server" Text="Close" OnClick="ClosePertChartViewButton_Click"/>
                </div>
                <div class="extraPanelHeader">PERT Chart</div>
            </div>
            <pdpcc:PertChartView ID="PertChartView" runat="server" ChartMargin="2" SnapRearrangedItemsToGuidelines="false" Height="190px"/>
        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="NetworkDiagramUpdatePanel" runat="server" RenderMode="Inline">
    <ContentTemplate>
        <asp:Panel ID="NetworkDiagramPanel" runat="server" Visible="false" EnableViewState="false">
            <div class="extraPanel">
                <div class="extraPanelCommandsArea">
                    <asp:LinkButton ID="CloseNetworkDiagramViewButton" runat="server" Text="Close" OnClick="CloseNetworkDiagramViewButton_Click"/>
                </div>
                <div class="extraPanelHeader">Network Diagram</div>
            </div>
            <pdpcc:NetworkDiagramView ID="NetworkDiagramView" runat="server" DiagramMargin="2" SnapRearrangedItemsToGuidelines="false" Height="190px"/>
        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>