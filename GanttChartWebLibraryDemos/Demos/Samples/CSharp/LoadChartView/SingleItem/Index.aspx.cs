using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DlhSoft.Web.UI.WebControls;
using System.Drawing;
using DlhSoft.Windows.Data;

namespace Demos.Samples.CSharp.LoadChartView.SingleItem
{
    public partial class Index : System.Web.UI.Page
    {
        private static readonly DateTime date = DateTime.Today;
        private static readonly int year = date.Year, month = date.Month;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Prepare data items.
                var items = new List<LoadChartItem>
                {
                    new LoadChartItem
                    {
                        Content = "Resource 1",
                        GanttChartItems = new List<AllocationItem>
                        {
                            new AllocationItem { Content = "Task 1 (Resource 1)", Start = new DateTime(year, month, 2, 8, 0, 0), Finish = new DateTime(year, month, 2, 16, 0, 0) },
                            new AllocationItem { Content = "Task 1, Task 2 [50%] (Resource 1): 150%", Start = new DateTime(year, month, 3, 8, 0, 0), Finish = new DateTime(year, month, 3, 12, 0, 0), Units = 1.5 },
                            new AllocationItem { Content = "Task 2 [50%] (Resource 1)", Start = new DateTime(year, month, 3, 12, 0, 0), Finish = new DateTime(year, month, 4, 16, 0, 0), Units = 0.5 },
                            new AllocationItem { Content = "Task 3 (Resource 1)", Start = new DateTime(year, month, 6, 8, 0, 0), Finish = new DateTime(year, month, 6, 16, 0, 0) }
                        }
                    }
                };
                LoadChartView.Items = items;

                LoadChartView.DisplayedTime = new DateTime(year, month, 1);
                LoadChartView.CurrentTime = new DateTime(year, month, 2, 12, 0, 0);

                LoadChartView.IsGridVisible = false;
                LoadChartView.ItemHeight = 64;
                LoadChartView.BarMargin = 8;
                LoadChartView.BarHeight = 52;

                // Optionally, initialize custom theme (themes.js).
                LoadChartView.InitializingClientCode += @";
                    initializeLoadChartTheme(control.settings, theme);";
            }
        }
    }
}
