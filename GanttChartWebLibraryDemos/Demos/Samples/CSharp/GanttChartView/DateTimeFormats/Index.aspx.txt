<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Demos.Samples.CSharp.GanttChartView.DateTimeFormats.Index" %>
<%@ Register TagPrefix="pdgcc" Namespace="DlhSoft.Web.UI.WebControls" Assembly="DlhSoft.ProjectData.GanttChart.ASP.Controls" %>
<%@ Register TagPrefix="pdpcc" Namespace="DlhSoft.Web.UI.WebControls.Pert" Assembly="DlhSoft.ProjectData.PertChart.ASP.Controls" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>GanttChartView DateTime Formats Sample</title>
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
                <table>
                    <tr>
                        <td>Date format</td>
                        <td>Hide time of day</td>
                        <td>Duration format</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DateFormatDropDownList" runat="server" AutoPostBack="true">
                                <asp:ListItem Text="English (United States)" Value="en-US" Selected="True" />
                                <asp:ListItem Text="English (United Kingdom)" Value="en-GB" />
                                <asp:ListItem Text="German (Germany)" Value="de-DE" />
                                <asp:ListItem Text="Dutch (The Netherlands)" Value="nl-NL" />
                                <asp:ListItem Text="Dutch (Belgium)" Value="nl-BE" />
                                <asp:ListItem Text="French (France)" Value="fr-FR" />
                                <asp:ListItem Text="French (Belgium)" Value="fr-BE" />
                                <asp:ListItem Text="Japanese (Japan)" Value="jp-JP" />
                           </asp:DropDownList>
                        </td>
                        <td><asp:CheckBox ID="HideTimeOfDayCheckBox" runat="server" AutoPostBack="true" OnCheckedChanged="HideTimeOfDayCheckBox_CheckedChanged" /></td>
                        <td>
                            <asp:DropDownList ID="DurationFormatDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DurationFormatDropDownList_SelectedIndexChanged">
                                <asp:ListItem Text="Hours" Value="h" />
                                <asp:ListItem Text="Days" Value="d" Selected="True" />
                                <asp:ListItem Text="Weeks" Value="w" />
                            </asp:DropDownList>
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
