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

namespace Demos.Samples.CSharp.GanttChartView.MaterialResources
{ 
    public partial class Index : System.Web.UI.Page
    {
        private static readonly DateTime date = DateTime.Today;
        private static readonly int year = date.Year, month = date.Month, day = date.Day;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var items = new List<GanttChartItem>();
                for (var i = 1; i <= 16; i++)
                    items.Add( new GanttChartItem { Content = "Print job #" + i, Start = new DateTime(year, month, day, 8, 0, 0) });
                GanttChartView.Items = items;

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
                GanttChartView.UpdateScaleInterval = TimeSpan.FromMinutes(1);

                // Define material and quantifiable assignable resource types, such as printers, sheets of paper, and printing supervisors.
                // Specify quantities of each resource type. We assume we have infinite sheets of paper, but limited printers and supervisors.
                GanttChartView.AssignableResources = new List<string> { "Printer", "Paper", "Supervisor"};
                GanttChartView.ResourceQuantities = new Dictionary<string, double> { { "Printer", 5 }, { "Paper", double.PositiveInfinity }, { "Supervisor", 2 } };

                // Define printing cost for 100 sheets of paper (default quantity used for cost by design).
                // Add a Cost column to the grid (hide unneeded columns as well).
                GanttChartView.SpecificResourceUsageCosts = new Dictionary<string, double> { { "Paper", 5 } };
                GanttChartView.Columns.Add(new Column { Header = "Cost ($)", Width = 100, CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getCostColumnTemplate(84)(item);" });

                // Hide all default columns except Task, Start, Finish, and Assignments.
                GanttChartView.Columns[5].IsVisible = false; // Milestone column
                GanttChartView.Columns[6].IsVisible = false; // Completed column

                // Assign a printer, the number of pages to pront on each print job, and part of the time of a supervisor needed to overview the printing jobs.
                // Update finish times of the task to based on their estimated durations, considering this ratio: 15 sheets of paper per minute.
                int[] sheetsOfPaperRequiredForPrintJobs = new int[] { 50, 20, 30, 60, 25, 10, 30, 50, 60, 80, 100, 25, 30, 30, 120, 80, 40 };
                for (var i = 0; i < items.Count; i++)
                {
                    int requiredSheetsOfPaper = sheetsOfPaperRequiredForPrintJobs[i];
                    items[i].AssignmentsContent = "Printer, Paper " + requiredSheetsOfPaper + "], Supervisor [50%]";
                    items[i].Finish = new DateTime(year, month, day, 8, (int)Math.Ceiling(requiredSheetsOfPaper / (double)15), 0);

                    GanttChartView.InitializingClientCode = @"initializeGanttChartTheme(control.settings, theme);";
                }
            }
        }

        public void LevelResourcesButton_Click(object sender, EventArgs e)
        {
            GanttChartView.LevelResources(true, GanttChartView.GetProjectStart());
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
