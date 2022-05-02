using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DlhSoft.Web.UI.WebControls;
using System.Drawing;
using DlhSoft.Windows.Data;

namespace Demos.Samples.CSharp.ScheduleChartView.Columns
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

                // Configure existing columns.
                ScheduleChartView.Columns[(int)ColumnType.Content].Header = "Workers";
                ScheduleChartView.Columns[(int)ColumnType.Content].Width = 120;

                // Initialize extra custom item values and associated columns.
                ScheduleChartView.Items[2].CustomValues["Property1"] = "A1";
                ScheduleChartView.Items[3].CustomValues["Property1"] = "B1";
                ScheduleChartView.Items[3].CustomValues["Property2"] = "B2";
                ScheduleChartView.Columns.Add(new Column
                {
                    Header = "My value 1", Width = 80,
                    CellTemplateClientCode = @"
                        var input = DlhSoft.Controls.ScheduleChartView.textInputColumnTemplateBase(document, 64, function () { return item.customProperty1Value; }, function (value) { item.customProperty1Value = value; });
                        input.setAttribute('Name', 'Item' + item.scheduleChartIndex + 'Property1'); // Ensure posting the (possibly updated) value back as a Request.Form field.
                        return input;"
                });
                ScheduleChartView.Columns.Add(new Column
                {
                    Header = "My value 2", Width = 80,
                    CellTemplateClientCode = @"
                        var input = DlhSoft.Controls.ScheduleChartView.textInputColumnTemplateBase(document, 64, function () { return item.customProperty2Value; }, function (value) { item.customProperty2Value = value; });
                        input.setAttribute('Name', 'Item' + item.scheduleChartIndex + 'Property2'); // Ensure posting the (possibly updated) value back as a Request.Form field.
                        return input;"
                });

                // Optionally, initialize custom themes (themes.js).
                ScheduleChartView.InitializingClientCode = @"initializeScheduleChartTheme(control.settings, theme);";
            }
            else
            {
                // Reinitialize custom item property values upon post backs, using the (possibly updated) Request.Form fields.
                for (var i = 0; i < ScheduleChartView.Items.Count; i++)
                {
                    var item = ScheduleChartView.Items[i];
                    item.CustomValues["Property1"] = Request.Form["Item" + i + "Property1"];
                    item.CustomValues["Property2"] = Request.Form["Item" + i + "Property2"];
                }
            }
        }

        public void AddNewItemButton_Click(object sender, EventArgs e)
        {
            var item = new ScheduleChartItem
            {
                Content = "New resource",
                GanttChartItems = new List<GanttChartItem>
                {
                    new GanttChartItem { Content = "Task X (New resource)", Start = new DateTime(year, month, 2, 8, 0, 0), Finish = new DateTime(year, month, 5, 16, 0, 0) },
                    new GanttChartItem { Content = "Task Y (New resource)", Start = new DateTime(year, month, 7, 8, 0, 0), Finish = new DateTime(year, month, 8, 16, 0, 0) }
                }
            };
            ScheduleChartView.AddItem(item);
            ScheduleChartView.SelectedItem = item;
            ScheduleChartView.ScrollTo(item);
            ScheduleChartView.ScrollTo(new DateTime(year, month, 1));
        }
        public void InsertNewItemButton_Click(object sender, EventArgs e)
        {
            if (ScheduleChartView.SelectedItem == null)
                return;
            var item = new ScheduleChartItem
            {
                Content = "New resource",
                GanttChartItems = new List<GanttChartItem>
                {
                    new GanttChartItem { Content = "Task X (New resource)", Start = new DateTime(year, month, 2, 8, 0, 0), Finish = new DateTime(year, month, 5, 16, 0, 0) },
                    new GanttChartItem { Content = "Task Y (New resource)", Start = new DateTime(year, month, 7, 8, 0, 0), Finish = new DateTime(year, month, 8, 16, 0, 0) }
                }
            };
            ScheduleChartView.InsertItem(ScheduleChartView.SelectedIndex, item);
            ScheduleChartView.SelectedItem = item;
            ScheduleChartView.ScrollTo(item);
            ScheduleChartView.ScrollTo(new DateTime(year, month, 1));
        }
        public void DeleteItemButton_Click(object sender, EventArgs e)
        {
            var item = ScheduleChartView.SelectedItem;
            if (item == null)
                return;
            ScheduleChartView.RemoveItem(item);
        }
        public void MoveItemUpButton_Click(object sender, EventArgs e)
        {
            var item = ScheduleChartView.SelectedItem;
            if (item == null)
                return;
            ScheduleChartView.MoveItemUp(item);
        }
        public void MoveItemDownButton_Click(object sender, EventArgs e)
        {
            var item = ScheduleChartView.SelectedItem;
            if (item == null)
                return;
            ScheduleChartView.MoveItemDown(item);
        }
    }
}
