using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DlhSoft.Web.UI.WebControls;
using System.Drawing;
using DlhSoft.Windows.Data;

namespace Demos.Samples.CSharp.GanttChartView.CriticalPath
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
                    new GanttChartItem { Content = "Task 2.1", Indentation = 1, Start = new DateTime(year, month, 2, 8, 0, 0), Finish = new DateTime(year, month, 9, 16, 0, 0), CompletedFinish = new DateTime(year, month, 5, 16, 0, 0), AssignmentsContent = "Resource 1, Resource 2 [50%]" },
                    new GanttChartItem { Content = "Task 2.2", Indentation = 1 },
                    new GanttChartItem { Content = "Task 2.2.1", Indentation = 2, Start = new DateTime(year, month, 12, 8, 0, 0), Finish = new DateTime(year, month, 14, 16, 0, 0), CompletedFinish = new DateTime(year, month, 14, 16, 0, 0), AssignmentsContent = "Resource 2" },
                    new GanttChartItem { Content = "Task 2.2.2", Indentation = 2, Start = new DateTime(year, month, 12, 12, 0, 0), Finish = new DateTime(year, month, 14, 16, 0, 0), AssignmentsContent = "Resource 2" },
                    new GanttChartItem { Content = "Task 3", Indentation = 1, Start = new DateTime(year, month, 14, 16, 0, 0), IsMilestone = true },
                };
                items[3].Predecessors = new List<PredecessorItem> { new PredecessorItem { Item = items[0], DependencyType = DependencyType.StartStart } };
                items[5].Predecessors = new List<PredecessorItem> { new PredecessorItem { Item = items[4] } };
                items[7].Predecessors = new List<PredecessorItem> { new PredecessorItem { Item = items[6], Lag = TimeSpan.FromHours(2) } };
                items[8].Predecessors = new List<PredecessorItem> { new PredecessorItem { Item = items[4] }, new PredecessorItem { Item = items[5] } };
                for (int i = 4; i <= 16; i++)
                    items.Add(new GanttChartItem { Content = "Task " + i, Start = new DateTime(year, month, 2 , 8, 0, 0), Finish = new DateTime(year, month, 3, 16, 0, 0) });
                GanttChartView.Items = items;

                GanttChartView.DisplayedTime = new DateTime(year, month, 1);
                GanttChartView.CurrentTime = new DateTime(year, month, 2, 12, 0, 0);

                // Initialize critical path highlighting.
                InitializeCriticalPath();

                // Optionally, initialize custom theme and templates (themes.js, templates.js).
                GanttChartView.InitializingClientCode = @"
                    if (initializeGanttChartTheme)
                        initializeGanttChartTheme(control.settings, theme);
                    if (initializeGanttChartTemplates)
                        initializeGanttChartTemplates(control.settings, theme);";
            }
        }

        public void RefreshCritialPathButton_Click(object sender, EventArgs e)
        {
            UpdateCriticalPath();
        }

        // Critical path highlighting logic.
        private void InitializeCriticalPath()
        {
            // Set up red as bar stroke and fill properties for the critical items.
            foreach (var item in GanttChartView.GetCriticalItems())
            {
                item.BarStroke = Color.Red;
                item.BarFill = Color.Red;
                item.CompletedBarFill = Color.DarkRed;
            }
        }
        private void UpdateCriticalPath()
        {
            // Reset the view.
            foreach (GanttChartItem item in GanttChartView.Items)
            {
                item.BarStroke = null;
                item.BarFill = null;
                item.CompletedBarFill = null;
            }
            // Reinitialize critical path highlighting.
            InitializeCriticalPath();
        }
    }
}
