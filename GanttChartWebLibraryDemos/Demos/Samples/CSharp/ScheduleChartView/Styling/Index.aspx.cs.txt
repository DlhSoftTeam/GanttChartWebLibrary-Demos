using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DlhSoft.Web.UI.WebControls;
using System.Drawing;
using DlhSoft.Windows.Data;

namespace Demos.Samples.CSharp.ScheduleChartView.Styling
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

                // Initialize CSS classes to be used for rendering specific elements in the view.
                ScheduleChartView.ContainerCssClass = "container";
                ScheduleChartView.SelectedItemCssClass = "selectedItem";
                ScheduleChartView.CellCssClass = "cell";
                ScheduleChartView.ToggleButtonCssClass = "toggleButton";
                ScheduleChartView.ToggleButtonHoveringCssClass = "toggleButtonHovering";
                ScheduleChartView.StandardBarCssClass = "standardBar";
                ScheduleChartView.StandardCompletedBarCssClass = "darkBar";
                ScheduleChartView.SummaryBarCssClass = "darkBar";
                ScheduleChartView.MilestoneBarCssClass = "darkBar";
                ScheduleChartView.AssignmentsCssClass = "assignments";
                ScheduleChartView.DependencyLineCssClass = "dependencyLine";

                // CSS classes may also be initialized on the client side when needed.
                ScheduleChartView.InitializingClientCode = @"
                    control.settings.alternativeItemClass = theme == 'Dark-black' ? 'alternativeGridItem-dark' : (theme == 'Steel-blue' ? 'alternativeGridItem-steel' : 'alternativeGridItem');
                    control.settings.alternativeChartItemClass = theme == 'Dark-black' ? 'alternativeChartItem-dark' : 'alternativeChartItem';";

                // Optionally, initialize custom theme and templates (themes.js, templates.js).
                ScheduleChartView.InitializingClientCode += @";
                    initializeScheduleChartTheme(control.settings, theme);
                    initializeScheduleChartTemplates(control.settings, theme);";
            }
        }
    }
}
