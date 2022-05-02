using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DlhSoft.Web.UI.WebControls;
using System.Drawing;
using DlhSoft.Windows.Data;

namespace Demos.Samples.CSharp.ScheduleChartView.Hierarchy
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
                    new ScheduleChartItem { Content = "Group 1" },
                    new ScheduleChartItem
                    {
                        Content = "Resource 1", Indentation = 1, GanttChartItems = new List<GanttChartItem>
                        {
                            new GanttChartItem { Content = "Task A (Resource 1)", Start = new DateTime(year, month, 2, 8, 0, 0), Finish = new DateTime(year, month, 8, 16, 0, 0), CompletedFinish = new DateTime(year, month, 5, 16, 0, 0) }
                        }
                    },
                    new ScheduleChartItem
                    {
                        Content = "Resource 2", Indentation = 1, GanttChartItems = new List<GanttChartItem>
                        {
                            new GanttChartItem { Content  = "Task A (Resource 2)", Start = new DateTime(year, month, 2, 8, 0, 0), Finish = new DateTime(year, month, 8, 16, 0, 0), CompletedFinish = new DateTime(year, month, 5, 16, 0, 0), AssignmentsContent = "50%" },
                            new GanttChartItem { Content  = "Task B (Resource 2)", Start = new DateTime(year, month, 11, 8, 0, 0), Finish = new DateTime(year, month, 12, 16, 0, 0), CompletedFinish = new DateTime(year, month, 12, 16, 0, 0) },
                            new GanttChartItem { Content  = "Task C (Resource 2)", Start = new DateTime(year, month, 14, 8, 0, 0), Finish = new DateTime(year, month, 14, 16, 0, 0) }
                        }
                    },
                    new ScheduleChartItem { Content = "Group 2" },
                    new ScheduleChartItem
                    {
                        Content  = "Resource 3", Indentation = 1, GanttChartItems = new List<GanttChartItem>
                        {
                            new GanttChartItem { Content  = "Task D (Resource 3)", Start = new DateTime(year, month, 12, 12, 0, 0), Finish = new DateTime(year, month, 14, 16, 0, 0) }
                        }
                    }
                };
                for (var g = 3; g <= 10; g++)
                {
                    items.Add( new ScheduleChartItem { Content = "Group " + g });
                    for (var i = 4; i < 10; i++)
                    {
                        var tasks = new List<GanttChartItem>();
                        for (var j = 0; j < 5; j++)
                            tasks.Add( new GanttChartItem { Content = "Task" + (j + 1) + " (Resource " + ((g - 3) * 6 + i) + ")", Start = new DateTime(year, month, 2 + j * 3, 8, 0, 0), Finish = new DateTime(year, month, 3 + j * 3, 16, 0, 0) });
                        items.Add( new ScheduleChartItem { Content = "Resource " + ((g - 3) * 6 + i), Indentation = 1, GanttChartItems = tasks });
                    }
                }
                ScheduleChartView.Items = items;

                ScheduleChartView.DisplayedTime = new DateTime(year, month, 1);
                ScheduleChartView.CurrentTime = new DateTime(year, month, 2, 12, 0, 0);

                ScheduleChartView.Columns[(int)ColumnType.Content].Width = 240;

                // Optionally, initialize custom themes (themes.js).
                ScheduleChartView.InitializingClientCode = @"initializeScheduleChartTheme(control.settings, theme);";
            }
        }
    }
}
