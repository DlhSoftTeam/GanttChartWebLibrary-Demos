﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Demos.Samples.CSharp.GanttChartView.MoveUpDown.Index" %>
<%@ Register TagPrefix="pdgcc" Namespace="DlhSoft.Web.UI.WebControls" Assembly="DlhSoft.ProjectData.GanttChart.ASP.Controls" %>
<%@ Register TagPrefix="pdpcc" Namespace="DlhSoft.Web.UI.WebControls.Pert" Assembly="DlhSoft.ProjectData.PertChart.ASP.Controls" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>GanttChartView Move Up-Down Sample</title>
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
                    <div class="ribbonHeader">Commands</div>
                    <div class="ribbonCommandsArea">
                        <div class="ribbonCommand"><asp:ImageButton ID="MoveItemUpImageButon" runat="server" AlternateText="Move item up" ImageUrl="Images/MoveUp.png" OnClick="MoveItemUpImageButon_Click" /></div>
                        <div class="ribbonCommand"><asp:ImageButton ID="MoveItemDownImageButon" runat="server" AlternateText="Move item down" ImageUrl="Images/MoveDown.png" OnClick="MoveItemDownImageButon_Click" /></div>
                    </div>
                </div>
            </div>
            <pdgcc:GanttChartView ID="GanttChartView" runat="server" Height="388px"/>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
