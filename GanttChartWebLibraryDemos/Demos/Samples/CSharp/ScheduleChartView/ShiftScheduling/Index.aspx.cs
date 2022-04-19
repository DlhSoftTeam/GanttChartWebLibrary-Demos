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

namespace Demos.Samples.CSharp.ScheduleChartView.ShiftScheduling
{ 
    public partial class Index : System.Web.UI.Page
    {
        private static readonly DateTime date = DateTime.Today;
        private static readonly int year = date.Year, month = date.Month;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var scheduleChartItems = new List<ScheduleChartItem>();
                int engineerCount = 8, managerCount = 3;
                for (int i = 1; i <= engineerCount; i++)
                    scheduleChartItems.Add(new ScheduleChartItem { Content = "Engineer #" + i, Start = new DateTime(), GanttChartItems = new List<GanttChartItem>() });
                for (int i = 1; i <= managerCount; i++)
                    scheduleChartItems.Add(new ScheduleChartItem { Content = "Manager #" + i, Start = new DateTime(), GanttChartItems = new List<GanttChartItem>() });

                ScheduleChartView.Items = scheduleChartItems;

                Func<DateTime, DateTime> weekStartProvider = (dateTime) => dateTime.Date.AddDays(-(int)date.DayOfWeek + (int)Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
                ScheduleChartView.TimelineStart = weekStartProvider(new DateTime(year, month, 1));
                ScheduleChartView.TimelineFinish = weekStartProvider(ScheduleChartView.TimelineStart.AddMonths(1).AddDays(7));
                ScheduleChartView.DisplayedTime = new DateTime(year, month, 1);
                ScheduleChartView.CurrentTime = new DateTime(year, month, 2, 12, 0, 0);

                ScheduleChartView.IsTaskStartReadOnly = true;
                ScheduleChartView.IsTaskEffortReadOnly = true;
                ScheduleChartView.IsTaskCompletedEffortVisible = false;
                ScheduleChartView.IsTaskCompletionReadOnly = true;

                // Set up a continuous schedule to enable shifts also during the night and in weekend days.
                ScheduleChartView.WorkingWeekStart = DayOfWeek.Sunday;           
                ScheduleChartView.WorkingWeekFinish = DayOfWeek.Saturday;          
                ScheduleChartView.VisibleDayStart = TimeOfDay.Parse("00:00:00"); // From midnight
                ScheduleChartView.VisibleDayFinish = TimeOfDay.FromHours(24);    // To next midnight

                // Increase zoom level as well.
                ScheduleChartView.HourWidth = 3.75;

                // Generate time intervals for work shifts.
                int dayDuration = 24;
                int shiftDuration = dayDuration / 3; // 3 shifts per day, i.e. 8 hours per shift; shifts start at: 11 PM, 7 AM, and 3 PM.
                int shiftStartHour = 23;
                var customIntervals = GetIntervals(TimeSpan.FromHours(8), ScheduleChartView.TimelineStart.Date.AddDays(-7-1).AddHours(shiftStartHour), ScheduleChartView.TimelineFinish.Date.AddDays(7), (d) => d.Hour.ToString("00"));

                // Define a fully custom scale item using Custom scale type and Custom header text format, providing the time intervals to be displayed using an inline function.
                Scale customScale = new Scale { ScaleType = ScaleType.Custom, HeaderTextFormat = ScaleHeaderTextFormat.Custom, Intervals = customIntervals, HeaderCssClass = "customScaleHeader", IsSeparatorVisible = true, SeparatorCssClass = "customScaleSeparator" };
                ScheduleChartView.Scales = new List<Scale>
                {
                    new Scale { ScaleType = ScaleType.Days, HeaderTextFormat = ScaleHeaderTextFormat.Date, HeaderCssClass = "daysDateScaleHeader" },
                    new Scale { ScaleType = ScaleType.Days, HeaderTextFormat = ScaleHeaderTextFormat.DayOfWeek, HeaderCssClass = "daysDayOfWeekScaleHeader" },
                    customScale
                };
                // Ensure space for 3 scales with visible headers.
                ScheduleChartView.HeaderHeight = 21 * 3;

                // Set up the actual shifts for engineers and managers (resource assignments).
                for (var i = 0; i < engineerCount; i++)
                {
                    var scheduleChartItem = scheduleChartItems[i];
                    var ganttChartItems = scheduleChartItem.GanttChartItems;
                    for (var d = ScheduleChartView.TimelineStart.AddHours(-1).AddHours((i % 4) * shiftDuration); d < ScheduleChartView.TimelineFinish; d = d.AddHours(shiftDuration * 4))
                    {
                        var shiftType = d.Hour <= 8 ? "morning" : (d.Hour <= 16 ? "afternoon" : "night");
                        ganttChartItems.Add(new GanttChartItem {
                            Content = "Engineering " + shiftType + " shift",
                            Start = d.AddMinutes(15),
                            Finish = d.AddHours(shiftDuration).AddMinutes(-15),
                            BarCssClass = "engineer " + shiftType
                        });
                    }
                }
                for (var i = 0; i < managerCount; i++)
                {
                    var scheduleChartItem = scheduleChartItems[engineerCount + i];
                    var ganttChartItems = scheduleChartItem.GanttChartItems;
                    for (var d = ScheduleChartView.TimelineStart.AddHours(-1).AddHours((i % 3) * shiftDuration); d < ScheduleChartView.TimelineFinish; d = d.AddHours(shiftDuration * 3))
                    {
                        var shiftType = d.Hour <= 8 ? "morning" : (d.Hour <= 16 ? "afternoon" : "night");
                        ganttChartItems.Add(new GanttChartItem
                        {
                            Content = "Management " + shiftType + " shift",
                            Start = d.AddMinutes(15),
                            Finish = d.AddHours(shiftDuration).AddMinutes(-15),
                            BarCssClass = "manager " + shiftType
                        });
                    }
                }

                // Optionally, initialize custom themes (themes.js).
                ScheduleChartView.InitializingClientCode = @"initializeScheduleChartTheme(control.settings, theme);";
            }
        }

        // Helper method that generates intervals of a specified duration between timeline start and finish times, and using the specified formatter for header texts.
        private static SortedDictionary<TimeInterval, string> GetIntervals(TimeSpan intervalDuration, DateTime timelineStart, DateTime timelineFinish, Func<DateTime, string> headerFormatter)
        {
            SortedDictionary<TimeInterval, string> intervals = new SortedDictionary<TimeInterval, string>();
            for (DateTime d = timelineStart; d < timelineFinish; d = d.AddHours(intervalDuration.Hours))
                intervals.Add(new TimeInterval(d, intervalDuration), headerFormatter(d));
            return intervals;
        }
    }
}
