using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DlhSoft.Web.UI.WebControls;
using System.Drawing;
using DlhSoft.Windows.Data;
using System.Threading;

namespace Demos.Samples.CSharp.GanttChartView.MinuteScale
{ 
    public partial class Index : System.Web.UI.Page
    {
        private static readonly DateTime date = DateTime.Today;
        private static readonly int year = date.Year, month = date.Month, day = date.Day;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var items = new List<GanttChartItem>
                {
                    new GanttChartItem { Content = "Task 1", IsExpanded = false },
                    new GanttChartItem { Content = "Task 1.1", Indentation = 1, Start = new DateTime(year, month, day, 8, 0, 0), Finish = new DateTime(year, month, day, 8, 5, 0) },
                    new GanttChartItem { Content = "Task 1.2", Indentation = 1, Start = new DateTime(year, month, day, 8, 5, 0), Finish = new DateTime(year, month, day, 8, 15, 0) },
                    new GanttChartItem { Content = "Task 2", IsExpanded = true },
                    new GanttChartItem { Content = "Task 2.1", Indentation = 1, Start = new DateTime(year, month, day, 8, 0, 0), Finish = new DateTime(year, month, day, 8, 5, 0), CompletedFinish = new DateTime(year, month, day, 8, 3, 0), AssignmentsContent = "Resource 1, Resource 2 [50%]" },
                    new GanttChartItem { Content = "Task 2.2", Indentation = 1 },
                    new GanttChartItem { Content = "Task 2.2.1", Indentation = 2, Start = new DateTime(year, month, day, 8, 0, 0), Finish = new DateTime(year, month, day, 8, 10, 0), CompletedFinish = new DateTime(year, month, day, 8, 20, 0), AssignmentsContent = "Resource 2" },
                    new GanttChartItem { Content = "Task 2.2.2", Indentation = 2, Start = new DateTime(year, month, day, 8, 10, 0), Finish = new DateTime(year, month, day, 8, 20, 0), AssignmentsContent = "Resource 2" },
                    new GanttChartItem { Content = "Task 3", Indentation = 1, Start = new DateTime(year, month, day, 8, 22, 0), IsMilestone = true },
                };
                items[3].Predecessors = new List<PredecessorItem> { new PredecessorItem { Item = items[0], DependencyType = DependencyType.StartStart } };
                items[7].Predecessors = new List<PredecessorItem> { new PredecessorItem { Item = items[6], Lag = TimeSpan.FromHours(2) } };
                items[8].Predecessors = new List<PredecessorItem> { new PredecessorItem { Item = items[4] }, new PredecessorItem { Item = items[5] } };
                for (int i = 4; i <= 16; i++)
                    items.Add(new GanttChartItem { Content = "Task " + i, Start = new DateTime(year, month, day, 8, 0, 0), Finish = new DateTime(year, month, day, 8, 5, 0) });
                GanttChartView.Items = items;

                // Set up timeline related settings to support the minute level scale.
                GanttChartView.Schedule = new Schedule
                {
                    WorkingWeekStart = DayOfWeek.Sunday,
                    WorkingWeekFinish = DayOfWeek.Saturday,
                    WorkingDayStart = TimeOfDay.Parse("00:00:00"),
                    WorkingDayFinish = TimeOfDay.Parse("24:00:00"),
                };
                Func<DateTime, DateTime> weekStartProvider = (dateTime) => dateTime.Date.AddDays(-(int)date.DayOfWeek + (int)Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
                GanttChartView.TimelineStart = weekStartProvider(DateTime.Now);
                GanttChartView.TimelineFinish = weekStartProvider(DateTime.Now.AddDays(7));
                GanttChartView.DisplayedTime = new DateTime(year, month, day, 7, 58, 0);
                GanttChartView.CurrentTime = new DateTime(year, month, day, 7, 59, 0);
                GanttChartView.HourWidth = 750;
                GanttChartView.IsMouseWheelZoomEnabled = false;
                // Set up scales to hours and minutes.
                GanttChartView.Scales = new List<Scale>
                {
                    new Scale
                    {
                        ScaleType = ScaleType.Custom, HeaderTextFormat = ScaleHeaderTextFormat.Custom, IsSeparatorVisible = true,
                        Intervals = GetIntervals(new TimeSpan(0, 15, 0), GanttChartView.TimelineStart, GanttChartView.TimelineFinish, (d) => d.ToShortDateString() + " " + d.Hour.ToString("00") + ":" + d.Minute.ToString("00"))
                    },
                    new Scale
                    {
                        ScaleType = ScaleType.Custom, HeaderTextFormat = ScaleHeaderTextFormat.Custom,
                        Intervals = GetIntervals(new TimeSpan(0, 3, 0), GanttChartView.TimelineStart, GanttChartView.TimelineFinish, (d) => d.Minute.ToString("00") + "'")
                    }
                };
                // Set up minute update scale as well.
                GanttChartView.UpdateScaleInterval = TimeSpan.FromMinutes(1);

                // Optionally, initialize custom themes (themes.js).
                GanttChartView.InitializingClientCode = @"initializeGanttChartTheme(control.settings, theme);";
            }
        }

        // Helper method that generates intervals of a specified duration between timeline start and finish times, and using the specified formatter for header texts.
        private static SortedDictionary<TimeInterval, string> GetIntervals(TimeSpan intervalDuration, DateTime timelineStart, DateTime timelineFinish, Func<DateTime, string> headerFormatter)
        {
            SortedDictionary<TimeInterval, string> intervals = new SortedDictionary<TimeInterval, string>();
            for (DateTime d = timelineStart; d < timelineFinish; d = d.AddMinutes(intervalDuration.Minutes))
                intervals.Add(new TimeInterval(d, intervalDuration), headerFormatter(d));
            return intervals;
        }
    }
}
