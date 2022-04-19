using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DlhSoft.Web.UI.WebControls;
using System.Drawing;
using DlhSoft.Windows.Data;

namespace Demos.Samples.CSharp.ScheduleChartView.CustomTemplate
{ 
    public partial class Index : System.Web.UI.Page
    {
        private static readonly DateTime date = DateTime.Today;
        private static readonly int year = date.Year, month = date.Month;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var items = new List<ScheduleChartItem>
                {
                    new ScheduleChartItem
                    {
                        Content = "Resource 1",
                        GanttChartItems = new List<GanttChartItem>
                        {
                            new GanttChartItem { Content = "Task A (Resource 1)", Start = new DateTime(year, month, 2, 8, 0, 0), Finish = new DateTime(year, month, 8, 16, 0, 0), CompletedFinish = new DateTime(year, month, 5, 16, 0, 0) }
                        }
                    },
                    new ScheduleChartItem
                    {
                        Content = "Resource 2",
                        GanttChartItems = new List<GanttChartItem>
                        {
                            new GanttChartItem { Content  = "Task A (Resource 2)", Start = new DateTime(year, month, 2, 8, 0, 0), Finish = new DateTime(year, month, 8, 16, 0, 0), CompletedFinish = new DateTime(year, month, 5, 16, 0, 0), AssignmentsContent = "50%" },
                            new GanttChartItem { Content  = "Task B (Resource 2)", Start = new DateTime(year, month, 11, 8, 0, 0), Finish = new DateTime(year, month, 12, 16, 0, 0), CompletedFinish = new DateTime(year, month, 12, 16, 0, 0) },
                            new GanttChartItem { Content  = "Task C (Resource 2)", Start = new DateTime(year, month, 14, 8, 0, 0), Finish = new DateTime(year, month, 14, 16, 0, 0) }
                        }
                    },
                    new ScheduleChartItem
                    {
                        Content  = "Resource 3",
                        GanttChartItems = new List<GanttChartItem>
                        {
                            new GanttChartItem { Content  = "Task D (Resource 3)", Start = new DateTime(year, month, 12, 12, 0, 0), Finish = new DateTime(year, month, 14, 16, 0, 0) }
                        }
                    }
                };
                for (int i = 4; i <= 16; i++)
                {
                    items.Add(new ScheduleChartItem
                    {
                        Content = "Resource " + i,
                        GanttChartItems = new List<GanttChartItem>
                        {
                            new GanttChartItem { Content = "Task X (Resource " + i + ")", Start = new DateTime(year, month, 2, 8, 0, 0), Finish = new DateTime(year, month, 5, 16, 0, 0) },
                            new GanttChartItem { Content = "Task Y (Resource " + i + ")", Start = new DateTime(year, month, 7, 8, 0, 0), Finish = new DateTime(year, month, 8, 16, 0, 0) }
                        }
                    });
                }
                ScheduleChartView.Items = items;

                ScheduleChartView.DisplayedTime = new DateTime(year, month, 1);
                ScheduleChartView.CurrentTime = new DateTime(year, month, 2, 12, 0, 0);

                // Initialize custom field values (HasMilestoneAtFinish, NumberOfLinesToDisplayInsteadOfRectangle, Label) for specific items.
                items[1].GanttChartItems[0].CustomValues["HasMilestoneAtFinish"] = true;
                items[1].GanttChartItems[1].CustomValues["HasMilestoneAtFinish"] = true;
                items[1].GanttChartItems[1].CustomValues["NumberOfLinesToDisplayInsteadOfRectangle"] = 50;
                items[2].GanttChartItems[0].CustomValues["NumberOfLinesToDisplayInsteadOfRectangle"] = 16;
                items[2].GanttChartItems[0].CustomValues["Label"] = "X";
                items[4].GanttChartItems[0].CustomValues["Label"] = "Task X (Resource 5)";
                items[5].GanttChartItems[0].CustomValues["Label"] = "X (6)";

                // Set standard task template as JavaScript function statements using custom fields prepared for items and 
                // returning SVG content (and optionally similar settings for milestone tasks).
                ScheduleChartView.StandardTaskTemplateClientCode = @"
                    var settings = control.settings;
                    var originalStandardTaskTemplate = settings.standardTaskTemplateForTheme ? settings.standardTaskTemplateForTheme : DlhSoft.Controls.GanttChartView.getDefaultStandardTaskTemplate(undefined, undefined, undefined);
                    var group = item.customNumberOfLinesToDisplayInsteadOfRectangleValue ? linesTemplate(item) : originalStandardTaskTemplate(item);
                    if (item.customHasMilestoneAtFinishValue)
                    {
                        var finishDiamond = getFinishDiamond(item);
                        var lastChildIndex = group.childNodes.length - 1; // Dependency creation thumb.
                        group.insertBefore(finishDiamond, group.childNodes[lastChildIndex]);
                    }
                    if (item.customLabelValue) {
                        var label = getLabel(item);
                        var index = group.childNodes.length - 4; // Drag thumb.
                        group.insertBefore(label, group.childNodes[index]);
                    }
                    return group;
                    // Custom template helpers.
                    function getChartItemArea(item) {
                        var undefinedType = 'undefined', svgns = 'http://www.w3.org/2000/svg';
                        var document = item.ganttChartView.ownerDocument;
                        if (typeof item.chartItemArea === undefinedType)
                            item.chartItemArea = document.createElementNS(svgns, 'g');
                        for (var i = item.chartItemArea.childNodes.length; i-- > 0;)
                            item.chartItemArea.removeChild(item.chartItemArea.childNodes[i]);
                        return item.chartItemArea;
                    }
                    function applyStyle(line, item, settings) {
                        var undefinedType = 'undefined';
                        var barClass = settings.standardBarClass;
                        if (typeof item.standardBarClass !== undefinedType)
                            barClass = item.standardBarClass;
                        if (typeof item.barClass !== undefinedType)
                            barClass = item.barClass;
                        if (typeof barClass !== undefinedType)
                            line.setAttribute('class', barClass);
                        else
                        {
                            var barStyle = settings.standardBarStyle;
                            if (typeof item.standardBarStyle !== undefinedType)
                                barStyle = item.standardBarStyle;
                            if (typeof item.barStyle !== undefinedType)
                                barStyle = item.barStyle;
                            if (typeof barStyle !== undefinedType)
                                line.setAttribute('style', barStyle);
                        }
                    }

                    function linesTemplate(item) {
                        var ganttChartView = item.ganttChartView;
                        var settings = ganttChartView.settings;
                        var items = ganttChartView.items;
                        var document = ganttChartView.ownerDocument;
                        var undefinedType = 'undefined', svgns = 'http://www.w3.org/2000/svg';
                        var barMargin = 4;
                        var barHeight = settings.itemHeight - 2 * barMargin;
                        var group = getChartItemArea(item);
                        var itemLeft = ganttChartView.getChartPosition(item.start);
                        var itemRight = Math.max(ganttChartView.getChartPosition(item.finish) - 1, itemLeft + 4);
                        var itemWidth = itemRight - itemLeft;
                        var lineCount = parseInt(item.customNumberOfLinesToDisplayInsteadOfRectangleValue);
                        for (var i = 0; i < lineCount; i++)
                        {
                            var line = document.createElementNS(svgns, 'line');
                            var y = i % 2 == 0 ? barMargin + i * 1.5 : barMargin + barHeight - i * 1.5;
                            line.setAttribute('x1', itemLeft + (i / lineCount) * itemWidth);
                            line.setAttribute('y1', barMargin);
                            line.setAttribute('x2', itemLeft + (i / lineCount) * itemWidth);
                            line.setAttribute('y2', barMargin + barHeight);
                            applyStyle(line, item, settings);
                            group.appendChild(line);
                            line = document.createElementNS(svgns, 'line');
                            var y = i % 2 == 0 ? barMargin + i * 1.5 : barMargin + barHeight - i * 1.5;
                            line.setAttribute('x1', itemLeft + (i / lineCount) * itemWidth);
                            line.setAttribute('y1', barMargin);
                            line.setAttribute('x2', itemLeft + ((i + 1) / lineCount) * itemWidth);
                            line.setAttribute('y2', barMargin + barHeight);
                            applyStyle(line, item, settings);
                            group.appendChild(line);
                        }
                        var finishLine = document.createElementNS(svgns, 'line');
                        finishLine.setAttribute('x1', itemRight);
                        finishLine.setAttribute('y1', barMargin);
                        finishLine.setAttribute('x2', itemRight);
                        finishLine.setAttribute('y2', barMargin + barHeight);
                        applyStyle(finishLine, item, settings);
                        group.appendChild(finishLine);
                        if (!settings.isReadOnly && !settings.isChartReadOnly && (typeof item.isReadOnly === undefinedType || !item.isReadOnly) && (typeof item.isBarReadOnly === undefinedType || !item.isBarReadOnly))
                        {
                            var thumb = document.createElementNS(svgns, 'rect');
                            thumb.setAttribute('x', itemLeft);
                            thumb.setAttribute('y', barMargin);
                            thumb.setAttribute('width', Math.max(0, itemRight - itemLeft - 1));
                            thumb.setAttribute('height', barHeight);
                            thumb.setAttribute('style', 'fill: White; fill-opacity: 0; cursor: move');
                            if (!settings.isTaskStartReadOnly)
                                group.appendChild(thumb);
                            var startThumb = document.createElementNS(svgns, 'rect');
                            startThumb.setAttribute('x', itemLeft - 4);
                            startThumb.setAttribute('y', barMargin);
                            startThumb.setAttribute('width', 4);
                            startThumb.setAttribute('height', barHeight);
                            startThumb.setAttribute('style', 'fill: White; fill-opacity: 0; cursor: e-resize');
                            if (settings.isDraggingTaskStartEndsEnabled && !settings.isTaskStartReadOnly && settings.interaction != 'TouchEnabled')
                                group.appendChild(startThumb);
                            var finishThumb = document.createElementNS(svgns, 'rect');
                            finishThumb.setAttribute('x', itemRight - 4);
                            finishThumb.setAttribute('y', barMargin);
                            finishThumb.setAttribute('width', 8);
                            finishThumb.setAttribute('height', barHeight);
                            finishThumb.setAttribute('style', 'fill: White; fill-opacity: 0; cursor: e-resize');
                            if (!settings.isTaskEffortReadOnly && settings.interaction != 'TouchEnabled')
                                group.appendChild(finishThumb);
                            ganttChartView.initializeTaskDraggingThumbs(thumb, startThumb, finishThumb, undefined, item, itemLeft, itemRight, undefined); // Without completion support: passing undefined for completion arguments.
                            if (settings.areTaskDependenciesVisible && !settings.areTaskPredecessorsReadOnly && !item.isPart)
                            {
                                var startDependencyThumb = null;
                                if (typeof settings.allowCreatingStartDependencies === undefinedType || settings.allowCreatingStartDependencies)
                                {
                                    startDependencyThumb = document.createElementNS(svgns, 'circle');
                                    startDependencyThumb.setAttribute('cx', itemLeft);
                                    startDependencyThumb.setAttribute('cy', barMargin + barHeight / 2);
                                    startDependencyThumb.setAttribute('r', barHeight / 4);
                                    startDependencyThumb.setAttribute('style', 'fill: White; fill-opacity: 0; cursor: pointer');
                                    group.appendChild(startDependencyThumb);
                                }
                                var dependencyThumb = document.createElementNS(svgns, 'circle');
                                dependencyThumb.setAttribute('cx', itemRight - 2);
                                dependencyThumb.setAttribute('cy', barMargin + barHeight / 2);
                                dependencyThumb.setAttribute('r', barHeight / 4);
                                dependencyThumb.setAttribute('style', 'fill: White; fill-opacity: 0; cursor: pointer');
                                group.appendChild(dependencyThumb);
                                ganttChartView.initializeDependencyDraggingThumbs(dependencyThumb, startDependencyThumb, group, item, barMargin + barHeight / 2, itemRight - 2, itemLeft);
                            }
                        }
                        return group;
                    }
                    function getFinishDiamond(item) {
                        var ganttChartView = item.ganttChartView;
                        var settings = ganttChartView.settings;
                        var document = ganttChartView.ownerDocument;
                        var undefinedType = 'undefined', svgns = 'http://www.w3.org/2000/svg';
                        var barMargin = 4;
                        var barHeight = settings.itemHeight * 1.38 - 2 * barMargin;
                        var group = document.createElementNS(svgns, 'g');
                        var itemLeft = ganttChartView.getChartPosition(item.finish);
                        var startDiamond = document.createElementNS(svgns, 'polygon');
                        var x = itemLeft - 8, y = settings.barMargin * 1.38 - 1, h = settings.barHeight * 1.38 + 1;
                        startDiamond.setAttribute('points', x + ',' + y + ' ' + (x - h / 2) + ',' + (y + h / 2) + ' ' + x + ',' + (y + h) + ' ' + (x + h / 2) + ',' + (y + h / 2));
                        var barClass = settings.milestoneBarClass;
                        if (typeof item.milestoneBarClass !== undefinedType)
                            barClass = item.milestoneBarClass;
                        if (typeof item.barClass !== undefinedType)
                            barClass = item.barClass;
                        if (typeof barClass !== undefinedType)
                            startDiamond.setAttribute('class', barClass);
                        else
                        {
                            var barStyle = settings.milestoneBarStyle;
                            if (typeof item.milestoneBarStyle !== undefinedType)
                                barStyle = item.milestoneBarStyle;
                            if (typeof item.barStyle !== undefinedType)
                                barStyle = item.barStyle;
                            if (typeof barStyle !== undefinedType)
                                startDiamond.setAttribute('style', barStyle);
                        }
                        group.appendChild(startDiamond);
                        return group;
                    }
                    function getLabel(item) {
                        var ganttChartView = item.ganttChartView;
                        var settings = ganttChartView.settings;
                        var document = ganttChartView.ownerDocument;
                        var svgns = 'http://www.w3.org/2000/svg';
                        var barMargin = 4;
                        var barHeight = settings.itemHeight - 2 * barMargin;
                        var itemLeft = ganttChartView.getChartPosition(item.start);
                        var content = document.createTextNode(item.customLabelValue);
                        var text = document.createElementNS(svgns, 'text');
                        text.setAttribute('x', itemLeft + 4);
                        text.setAttribute('y', barMargin + barHeight - barHeight / 4 - 1);
                        text.setAttribute('style', 'font-size: ' + (barHeight / 2 + 1) + 'px');
                        text.appendChild(content);
                        return text;
                    }";

                // Optionally, initialize custom themes (themes.js).
                ScheduleChartView.InitializingClientCode = @"initializeScheduleChartTheme(control.settings, theme);";
            }
        }
    }
}
