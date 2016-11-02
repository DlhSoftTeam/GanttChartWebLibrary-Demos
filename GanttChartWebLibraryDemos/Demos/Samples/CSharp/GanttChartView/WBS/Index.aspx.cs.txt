using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DlhSoft.Web.UI.WebControls;
using System.Drawing;
using DlhSoft.Windows.Data;

namespace Demos.Samples.CSharp.GanttChartView.WBS
{ 
    public partial class Index : System.Web.UI.Page
    {
        private static readonly DateTime date = DateTime.Today;
        private static readonly int year = date.Year, month = date.Month;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

                GanttChartView.DisplayedTime = new DateTime(year, month, 1);
                GanttChartView.CurrentTime = new DateTime(year, month, 2, 12, 0, 0);

                // Add WBS column with index paths computed on the client side.
                GanttChartView.Columns.Insert(0, new Column { Header = "WBS", Width = 50, CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getWbsColumnTemplate()(item);" });

                // Optionally, initialize custom theme and templates (themes.js, templates.js).
                GanttChartView.InitializingClientCode = @"
                    initializeGanttChartTheme(control.settings, theme);
                    initializeGanttChartTemplates(control.settings, theme);";
            }
        }
    }
}
