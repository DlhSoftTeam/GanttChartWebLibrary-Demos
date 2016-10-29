using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DlhSoft.Web.UI.WebControls;
using System.Drawing;
using DlhSoft.Windows.Data;

namespace Demos.Samples.CSharp.GanttChartView.AssignmentsTemplate
{ 
    public partial class Index : System.Web.UI.Page
    {
        private static readonly DateTime date = DateTime.Today;
        private static readonly int year = date.Year, month = date.Month;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Optionally, set up client side HTML content to be displayed while initializing the component.
                GanttChartView.InitializingContent = "...";

                // Prepare data items.
                var items = new List<GanttChartItem>
                {
                    new GanttChartItem { Content = "Task 1", IsExpanded = false },
                    new GanttChartItem { Content = "Task 1.1", Indentation = 1, Start = new DateTime(year, month, 2, 8, 0, 0), Finish = new DateTime(year, month, 4, 16, 0, 0) },
                    new GanttChartItem { Content = "Task 1.2", Indentation = 1, Start = new DateTime(year, month, 3, 8, 0, 0), Finish = new DateTime(year, month, 5, 12, 0, 0) },
                    new GanttChartItem { Content = "Task 2", IsExpanded = true },
                    new GanttChartItem { Content = "Task 2.1", Indentation = 1, Start = new DateTime(year, month, 2, 8, 0, 0), Finish = new DateTime(year, month, 8, 16, 0, 0), CompletedFinish = new DateTime(year, month, 5, 16, 0, 0), AssignmentsContent = "Resource 1, Resource 2 [50%]" },
                    new GanttChartItem { Content = "Task 2.2", Indentation = 1 },
                    new GanttChartItem { Content = "Task 2.2.1", Indentation = 2, Start = new DateTime(year, month, 11, 8, 0, 0), Finish = new DateTime(year, month, 14, 16, 0, 0), CompletedFinish = new DateTime(year, month, 14, 16, 0, 0), AssignmentsContent = "Resource 2" },
                    new GanttChartItem { Content = "Task 2.2.2", Indentation = 2, Start = new DateTime(year, month, 12, 12, 0, 0), Finish = new DateTime(year, month, 14, 16, 0, 0), AssignmentsContent = "Resource 2" },
                    new GanttChartItem { Content = "Task 3", Indentation = 1, Start = new DateTime(year, month, 15, 16, 0, 0), IsMilestone = true },
                };
                items[3].Predecessors = new List<PredecessorItem> { new PredecessorItem { Item = items[0], DependencyType = DependencyType.StartStart } };
                items[7].Predecessors = new List<PredecessorItem> { new PredecessorItem { Item = items[6], Lag = TimeSpan.FromHours(2) } };
                items[8].Predecessors = new List<PredecessorItem> { new PredecessorItem { Item = items[4] }, new PredecessorItem { Item = items[5] } };
                for (int i = 4; i <= 16; i++)
                    items.Add(new GanttChartItem { Content = "Task " + i, Indentation = i >= 8 && i % 3 == 2 ? 0 : 1, Start = new DateTime(year, month, 2 + (i <= 8 ? (i - 4) * 3 : i - 8), 8, 0, 0), Finish = new DateTime(year, month, 2 + (i <= 8 ? (i - 4) * 3 + (i > 8 ? 6 : 1) : i - 2), 16, 0, 0) });
                items[9].Finish = items[9].Finish + TimeSpan.FromDays(2);
                items[9].AssignmentsContent = "Resource 1";
                items[10].Predecessors = new List<PredecessorItem> { new PredecessorItem { Item = items[9] } };
                GanttChartView.Items = items;

                // Set the displayed and current time values to automatically scroll to a specific chart coordinate, and display a vertical bar highlighter at the specified point.
                GanttChartView.DisplayedTime = new DateTime(year, month, 1);
                GanttChartView.CurrentTime = new DateTime(year, month, 2, 12, 0, 0);

                GanttChartView.AssignmentsTemplateClientCode = @"
                    var undefinedType = 'undefined', svgns = 'http://www.w3.org/2000/svg', hourDuration = 60 * 60 * 1000;
                    var ganttChartView = item.ganttChartView;
                    var settings = ganttChartView.settings;
                    var document = ganttChartView.ownerDocument;
                    var group = document.createElementNS(svgns, 'g');
                    var icon = document.createElementNS(svgns, 'image');
                    var text = document.createElementNS(svgns, 'text');
                    var itemRight = ganttChartView.getChartPosition(item.finish);
                    if (item.isMilestone || (item.hasChildren && (typeof item.isSummaryEnabled === undefinedType || item.isSummaryEnabled)))
                        itemRight += settings.barHeight / 2;
                    icon.setAttribute('x', itemRight + 7);
                    icon.setAttribute('y', settings.barMargin);
                    icon.setAttribute('width', '16px');
                    icon.setAttribute('height', '16px');
                    text.setAttribute('x', itemRight + 7 + 16 + 2);
                    text.setAttribute('y', settings.barMargin + settings.barHeight - 1);
                    var content = item.assignmentsContent;
                    if (typeof content === undefinedType)
                        content = '';
                    var resource = content;
                    var resourceCommaIndex = resource.indexOf(',');
                    if (resourceCommaIndex >= 0)
                        resource = resource.substr(0, resourceCommaIndex);
                    if (resource && resource != 'Resource 1' && resource != 'Resource 2')
                        resource = 'Other';
                    icon.setAttribute('href', 'Images/' + resource + '.png');
                    text.appendChild(document.createTextNode(content));
                    if (typeof settings.assignmentsClass !== undefinedType)
                        text.setAttribute('class', settings.assignmentsClass);
                    else if (typeof settings.assignmentsStyle !== undefinedType)
                        text.setAttribute('style', settings.assignmentsStyle);
                    if (resource)
                        group.appendChild(icon);
                    group.appendChild(text);
                    return group;";

                // Optionally, initialize custom theme and templates (themes.js, templates.js).
                GanttChartView.InitializingClientCode += @";
                if (initializeGanttChartTheme)
                    initializeGanttChartTheme(control.settings, theme);
                if (initializeGanttChartTemplates)
                    initializeGanttChartTemplates(control.settings, theme);";
            }
        }
    }
}