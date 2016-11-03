using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DlhSoft.Web.UI.WebControls;
using System.Drawing;
using DlhSoft.Windows.Data;

namespace Demos.Samples.CSharp.PertChartView.GanttChartIntegration
{ 
    public partial class Index : System.Web.UI.Page
    {
        private static readonly DateTime date = DateTime.Today;
        private static readonly int year = date.Year, month = date.Month;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initialize Gantt Chart.
                var ganttChartItems = new List<GanttChartItem>();
                for (var i = 1; i <= 8; i++)
                {
                    ganttChartItems.Add(new GanttChartItem
                    {
                        Content = "Task " + i,
                        Start = new DateTime(year, month, 2 + i - 1, 8, 0, 0),
                        Finish = new DateTime(year, month, 2 + i - 1 + 3, 16, 0, 0),
                        AssignmentsContent = "Resource " + (i % 3 > 0 ? i % 3 : 3)
                    });
                }
                ganttChartItems[2].Predecessors.Add(new PredecessorItem { Item = ganttChartItems[1] });
                GanttChartView.Items = ganttChartItems;

                GanttChartView.DisplayedTime = new DateTime(year, month, 1);
                GanttChartView.CurrentTime = new DateTime(year, month, 2, 12, 0, 0);

                GanttChartView.InitializingClientCode = @"
                    initializeGanttChartTheme(control.settings, theme);
                    initializeGanttChartTemplates(control.settings, theme);";
            }
        }

        public void AddNewGanttChartItemButton_Click(object sender, EventArgs e)
        {
            GanttChartItem item = new GanttChartItem {
                Content = "New task",
                Start = new DateTime(year, month, 2, 8, 0, 0),
                Finish = new DateTime(year, month, 4, 16, 0, 0),
                AssignmentsContent = "Resource 4"
            };
            GanttChartView.Items.Add(item);
        }
        public void ShowPertChartButton_Click(object sender, EventArgs e)
        {
            PertChartViewContainer.Visible = true;
            GanttChartViewContainer.Style[HtmlTextWriterStyle.Display] = "none";

            // Initialize PERT Chart.
            var items = GanttChartView.GetPertChartItems();
            PertChartView.Items = items;
            PertChartView.InitializingClientCode = @"
                initializePertChartTheme(control.settings, theme);
                initializePertChartTemplates(control.settings, theme)";
        }
        public void HidePertChartButton_Click(object sender, EventArgs e)
        {
            GanttChartViewContainer.Style[HtmlTextWriterStyle.Display] = null;
            PertChartViewContainer.Visible = false;
        }
    }
}
