using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DlhSoft.Web.UI.WebControls;
using System.Drawing;
using DlhSoft.Windows.Data;

namespace Demos.Samples.CSharp.ScheduleChartView.GanttChartIntegration
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
                        AssignmentsContent = "Resource " + (i % 4 > 0 ? i % 4 : 4)
                    });
                }
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
                AssignmentsContent = "Resource 5"
            };
            GanttChartView.Items.Add(item);
        }
        public void ShowScheduleChartButton_Click(object sender, EventArgs e)
        {
            ScheduleChartViewContainer.Visible = true;
            GanttChartViewContainer.Style[HtmlTextWriterStyle.Display] = "none";

            // Initialize Schedule Chart.
            var items = GanttChartView.GetScheduleChartItems();
            ScheduleChartView.Items = items;
            ScheduleChartView.CurrentTime = new DateTime(year, month, 2, 12, 0, 0);
            ScheduleChartView.InitializingClientCode = @"
                initializeScheduleChartTheme(control.settings, theme);
                initializeScheduleChartTemplates(control.settings, theme);";
        }
        public void HideScheduleChartButton_Click(object sender, EventArgs e)
        {
            GanttChartViewContainer.Style[HtmlTextWriterStyle.Display] = null;
            ScheduleChartViewContainer.Visible = false;
        }
    }
}
