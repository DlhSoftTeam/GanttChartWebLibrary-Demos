using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DlhSoft.Web.UI.WebControls;
using System.Drawing;
using DlhSoft.Windows.Data;

namespace Demos.Samples.CSharp.GanttChartView.MultipleBarsPerItem
{ 
    public partial class Index : System.Web.UI.Page
    {
        private static readonly DateTime date = DateTime.Today;
        private static readonly int year = date.Year, month = date.Month;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var items = new List<GanttChartItem>();
                for (int i = 1; i <= 16; i++)
                    items.Add(new GanttChartItem { Content = "Task " + i, Indentation = i % 3 == 2 ? 0 : 1 });
                GanttChartView.Items = items;

                // Generate multiple parts for each item.
                for (int i = 0; i < items.Count; i++)
                {
                    var parts = new List<GanttChartItem>();
                    for (int j = 0; j < (i + 1) / (double)2; j++)
                    {
                        parts.Add(new GanttChartItem
                        {
                            Content = "Bar " + (i + 1) + '.' + (j + 1),
                            Start = new DateTime(year, month, 1).AddDays(1 + (i + 1) + (j + 1) * i).Add(TimeSpan.Parse("08:00:00")),
                            Finish = new DateTime(year, month, 1).AddDays(1 + (i + 1) + (j + 1) * i + (i / 2)).Add(TimeSpan.Parse("16:00:00")),
                            AssignmentsContent = (i + 1).ToString() + '.' + (j + 1).ToString()
                        });
                    }
                    items[i].Parts = parts;
                }

                GanttChartView.DisplayedTime = new DateTime(year, month, 1);
                GanttChartView.CurrentTime = new DateTime(year, month, 2, 12, 0, 0);

                // Simplify grid.
                GanttChartView.GridWidth = new Unit("15%");
                GanttChartView.ChartWidth = new Unit("85%");
                GanttChartView.IsGridRowClickTimeScrollingEnabled = false;
                for (int i = 3; i < GanttChartView.Columns.Count; i++)
                    GanttChartView.Columns[i].IsVisible = false;

                // Optionally, initialize custom theme and templates (themes.js, templates.js).
                GanttChartView.InitializingClientCode = @"
                    if (initializeGanttChartTheme)
                        initializeGanttChartTheme(control.settings, theme);
                    if (initializeGanttChartTemplates)
                        initializeGanttChartTemplates(control.settings, theme);";
            }
        }
    }
}
