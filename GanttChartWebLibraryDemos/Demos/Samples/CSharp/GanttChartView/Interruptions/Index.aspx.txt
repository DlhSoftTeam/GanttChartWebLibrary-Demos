<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Demos.Samples.CSharp.GanttChartView.Interruptions.Index" %>
<%@ Register TagPrefix="pdgcc" Namespace="DlhSoft.Web.UI.WebControls" Assembly="DlhSoft.ProjectData.GanttChart.ASP.Controls" %>
<%@ Register TagPrefix="pdpcc" Namespace="DlhSoft.Web.UI.WebControls.Pert" Assembly="DlhSoft.ProjectData.PertChart.ASP.Controls" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>GanttChartView Interruptions Sample</title>
    <link rel="Stylesheet" href="app.css" type="text/css" />
    <script src="templates.js" type="text/javascript"></script>
    <script src="themes.js" type="text/javascript"></script>
    <script type="text/javascript">
        // Query string syntax: ?theme
        // Supported themes: Default, Generic-bright, Generic-blue, DlhSoft-gray, Purple-green, Steel-blue, Dark-black, Cyan-green, Blue-navy, Orange-brown, Teal-green, Purple-beige, Gray-blue, Aero.
        var queryString = window.location.search;
        var theme = queryString ? queryString.substr(1) : null;
        // Support for interruption: we return drawing to be placed on top of existing bars.
        function addInterruptionElements(document, ganttChartView, extraArea, item) {
            if (!item.customInterruptionsValue || item.isMilestone || item.hasChildren)
                return;
            var interruptionStrings = item.customInterruptionsValue.split(';');
            for (var i = 0; i < interruptionStrings.length; i++) {
                var interruptionString = interruptionStrings[i];
                var interruptionEndStrings = interruptionString.split('-');
                var interruption = { start: new Date(parseInt(interruptionEndStrings[0])), finish: new Date(parseInt(interruptionEndStrings[1])) };
                interruption = { start: item.start > interruption.start ? item.start : interruption.start, finish: item.finish < interruption.finish ? item.finish : interruption.finish };
                if (interruption.finish <= item.start || interruption.start >= item.finish)
                    continue;
                extraArea.append(getInterruptionElement(document, ganttChartView, item, interruption));
            }
        }
        function getInterruptionElement(document, ganttChartView, item, interruption) {
            var svgns = 'http://www.w3.org/2000/svg';
            var group = document.createElementNS(svgns, 'g');
            var barMargin = 4;
            var barHeight = ganttChartView.settings.itemHeight - 2 * barMargin;
            var startX = ganttChartView.getChartPosition(interruption.start), finishX = ganttChartView.getChartPosition(interruption.finish);
            if (finishX - 0.5 > startX + 0.5) {
                startX += 0.5;
                finishX -= 0.5;
                var background = document.createElementNS(svgns, 'rect');
                background.setAttribute('x', startX.toString());
                background.setAttribute('y', (barMargin - 1).toString());
                background.setAttribute('width', Math.max(0, finishX - startX - 1).toString());
                background.setAttribute('height', (barHeight + 2).toString());
                background.setAttribute('style', 'fill: ' + (theme == "Dark-black" ? "#282828" : theme == "Steel-blue" ? "#bfcfda" : "White"));
                group.appendChild(background);
                var topLine = document.createElementNS(svgns, 'line');
                topLine.setAttribute('x1', startX.toString());
                topLine.setAttribute('x2', finishX.toString());
                topLine.setAttribute('y1', (barMargin + (barHeight < 20 ? 1 : 0)).toString());
                topLine.setAttribute('y2', (barMargin + (barHeight < 20 ? 1 : 0)).toString());
                topLine.setAttribute('style', 'stroke: #0050a0; stroke-opacity: 0.75; stroke-dasharray: 2 2');
                group.appendChild(topLine);
                var bottomLine = document.createElementNS(svgns, 'line');
                bottomLine.setAttribute('x1', startX.toString());
                bottomLine.setAttribute('x2', finishX.toString());
                bottomLine.setAttribute('y1', (barMargin + barHeight - (barHeight < 20 ? 1 : 0)).toString());
                bottomLine.setAttribute('y2', (barMargin + barHeight - (barHeight < 20 ? 1 : 0)).toString());
                bottomLine.setAttribute('style', 'stroke: #0050a0; stroke-opacity: 0.75; stroke-dasharray: 2 2');
                group.appendChild(bottomLine);
            }
            return group;
        }
    </script>
</head>
<body>
    <form id="form" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server"/>
    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
            <pdgcc:GanttChartView ID="GanttChartView" runat="server" Height="388px"/>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
