using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DlhSoft.Web.UI.WebControls;
using System.Drawing;
using DlhSoft.Windows.Data;

namespace Demos.Samples.CSharp.ScheduleChartView.ContinuousSchedule
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

                // Set up continuous schedule (24/7).
                ScheduleChartView.WorkingWeekStart = DayOfWeek.Sunday;                  
                ScheduleChartView.WorkingWeekFinish = DayOfWeek.Saturday;                 
                ScheduleChartView.VisibleDayStart = TimeOfDay.Parse("00:00:00");                    
                ScheduleChartView.VisibleDayFinish = TimeOfDay.Parse("24:00:00");

                // Optionally, initialize custom themes (themes.js).
                ScheduleChartView.InitializingClientCode = @"initializeScheduleChartTheme(control.settings, theme);";
            }
        }
    }
}
