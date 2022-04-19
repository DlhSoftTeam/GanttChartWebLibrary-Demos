<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Demos.Samples.CSharp.GanttChartView.ProjectXml.Index" %>
<%@ Register TagPrefix="pdgcc" Namespace="DlhSoft.Web.UI.WebControls" Assembly="DlhSoft.ProjectData.GanttChart.ASP.Controls" %>
<%@ Register TagPrefix="pdpcc" Namespace="DlhSoft.Web.UI.WebControls.Pert" Assembly="DlhSoft.ProjectData.PertChart.ASP.Controls" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>GanttChartView Project XML Sample</title>
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
                    <div class="ribbonHeader">XML</div>
                    <div class="ribbonCommandsArea">
                        <div class="ribbonCommand"><asp:ImageButton ID="LoadProjectXmlButton" runat="server" ImageUrl="Images/LoadProjectXml.png" ToolTip="Load existing Project XML file" AlternateText="Load Project XML" OnClick="LoadProjectXmlButton_Click"/></div>
                        <div class="ribbonCommand"><asp:ImageButton ID="SaveProjectXmlButton" runat="server" ImageUrl="Images/SaveProjectXml.png" ToolTip="Export as Project XML file" AlternateText="Save Project XML" OnClick="SaveProjectXmlButton_Click"/></div>
                    </div>
                </div>
            </div>
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
    </form>
</body>
</html>
