using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DlhSoft.Web.UI.WebControls;
using System.Drawing;
using DlhSoft.Windows.Data;

namespace Demos.Samples.CSharp.ScheduleChartView.ResourceStatus
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
                            new GanttChartItem { Content = "Resource 1: started", Start = new DateTime(year, month, 2, 8, 0, 0), Finish = new DateTime(year, month, 8, 16, 0, 0), BarCssClass = "startedStatusBar" },
                            new GanttChartItem { Content = "Resource 1: maintenance", Start = new DateTime(year, month, 9, 8, 0, 0), Finish = new DateTime(year, month, 9, 16, 0, 0), BarCssClass = "maintenanceStatusBar" },
                            new GanttChartItem { Content = "Resource 1: started", Start = new DateTime(year, month, 10, 8, 0, 0), Finish = new DateTime(year, month, 13, 16, 0, 0), BarCssClass = "startedStatusBar" },
                            new GanttChartItem { Content = "Resource 1: issues", Start = new DateTime(year, month, 14, 8, 0, 0), Finish = new DateTime(year, month, 14, 16, 0, 0), BarCssClass = "issuesStatusBar" },
                            new GanttChartItem { Content = "Resource 1: maintenance", Start = new DateTime(year, month, 15, 8, 0, 0), Finish = new DateTime(year, month, 15, 16, 0, 0), BarCssClass = "maintenanceStatusBar" },
                            new GanttChartItem { Content = "Resource 1: started", Start = new DateTime(year, month, 16, 8, 0, 0), Finish = new DateTime(year, month, 22, 16, 0, 0), BarCssClass = "startedStatusBar" }
                        }
                    },
                    new ScheduleChartItem
                    {
                        Content = "Resource 2",
                        GanttChartItems = new List<GanttChartItem>
                        {
                            new GanttChartItem { Content = "Resource 2: started", Start = new DateTime(year, month, 2, 8, 0, 0), Finish = new DateTime(year, month, 8, 16, 0, 0), BarCssClass = "startedStatusBar" },
                            new GanttChartItem { Content = "Resource 2: issues", Start = new DateTime(year, month, 9, 8, 0, 0), Finish = new DateTime(year, month, 12, 16, 0, 0), BarCssClass = "issuesStatusBar" },
                            new GanttChartItem { Content = "Resource 2: maintenance", Start = new DateTime(year, month, 13, 8, 0, 0), Finish = new DateTime(year, month, 14, 16, 0, 0), BarCssClass = "maintenanceStatusBar" },
                            new GanttChartItem { Content = "Resource 2: started", Start = new DateTime(year, month, 15, 8, 0, 0), Finish = new DateTime(year, month, 22, 16, 0, 0), BarCssClass = "startedStatusBar" }
                        }
                    },
                    new ScheduleChartItem
                    {
                        Content  = "Resource 3",
                        GanttChartItems = new List<GanttChartItem>
                        {
                            new GanttChartItem { Content = "Resource 3: started", Start = new DateTime(year, month, 2, 8, 0, 0), Finish = new DateTime(year, month, 22, 16, 0, 0), BarCssClass = "startedStatusBar" }
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
                            new GanttChartItem { Content = "Resource " + i + ": started", Start = new DateTime(year, month, 2, 8, 0, 0), Finish = new DateTime(year, month, 5, 16, 0, 0), BarCssClass = "startedStatusBar" },
                            new GanttChartItem { Content = "Resource " + i + ": issues", Start = new DateTime(year, month, 6, 8, 0, 0), Finish = new DateTime(year, month, 6, 16, 0, 0), BarCssClass = "issuesStatusBar" },
                            new GanttChartItem { Content = "Resource " + i + ": maintenance", Start = new DateTime(year, month, 7, 8, 0, 0), Finish = new DateTime(year, month, 7, 16, 0, 0), BarCssClass = "maintenanceStatusBar" },
                            new GanttChartItem { Content = "Resource " + i + ": started", Start = new DateTime(year, month, 8, 8, 0, 0), Finish = new DateTime(year, month, 15, 16, 0, 0), BarCssClass = "startedStatusBar" },
                            new GanttChartItem { Content = "Resource " + i + ": issues", Start = new DateTime(year, month, 16, 8, 0, 0), Finish = new DateTime(year, month, 16, 16, 0, 0), BarCssClass = "issuesStatusBar" },
                            new GanttChartItem { Content = "Resource " + i + ": started", Start = new DateTime(year, month, 17, 8, 0, 0), Finish = new DateTime(year, month, 22, 16, 0, 0), BarCssClass = "startedStatusBar" }
                        }
                    });
                }
                ScheduleChartView.Items = items;

                ScheduleChartView.DisplayedTime = new DateTime(year, month, 1);
                ScheduleChartView.CurrentTime = new DateTime(year, month, 2, 12, 0, 0);

                ScheduleChartView.IsReadOnly = true;
                ScheduleChartView.IsTaskCompletedEffortVisible = false;
                ScheduleChartView.WorkingWeekStart = DayOfWeek.Sunday;
                ScheduleChartView.WorkingWeekFinish = DayOfWeek.Saturday;

                // Optionally, initialize custom theme and templates (themes.js, templates.js).
                ScheduleChartView.InitializingClientCode = @"
                    initializeScheduleChartTheme(control.settings, theme);
                    initializeScheduleChartTemplates(control.settings, theme);";
            }
        }
    }
}
