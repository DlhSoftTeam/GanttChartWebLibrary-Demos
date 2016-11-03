using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DlhSoft.Web.UI.WebControls;
using System.Drawing;
using DlhSoft.Windows.Data;

namespace Demos.Samples.CSharp.LoadChartView.MainFeatures
{
    public partial class Index : System.Web.UI.Page
    {
        private static readonly DateTime date = DateTime.Today;
        private static readonly int year = date.Year, month = date.Month;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Optionally, set up client side HTML content to be displayed while initializing the component.
                LoadChartView.InitializingContent = "...";

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
                            new AllocationItem { Content = "Task 2 [50%] (Resource 1)", Start = new DateTime(year, month, 3, 12, 0, 0), Finish = new DateTime(year, month, 4, 16, 0, 0), Units = 0.5 }
                        }
                    },
                    new LoadChartItem
                    {
                        Content = "Resource 2",
                        GanttChartItems = new List<AllocationItem>
                        {
                            new AllocationItem { Content = "Task 2 (Resource 2)", Start = new DateTime(year, month, 3, 8, 0, 0), Finish = new DateTime(year, month, 4, 16, 0, 0)}
                        }
                    }
                };
                for (int i = 3; i <= 16; i++)
                {
                    items.Add(new LoadChartItem
                    {
                        Content = "Resource " + i,
                        GanttChartItems = new List<AllocationItem> { new AllocationItem { Content = "Task X (Resource " + i + ")", Start = new DateTime(year, month, 2, 8, 0, 0), Finish = new DateTime(year, month, 5, 16, 0, 0) },
                                                                     new AllocationItem { Content = "Task Y (Resource " + i + ")", Start = new DateTime(year, month, 7, 8, 0, 0), Finish = new DateTime(year, month, 8, 16, 0, 0) } }
                    });
                }
                LoadChartView.Items = items;

                // Optionally, hide data grid or set grid and chart widths, clear read only settings, and/or disable virtualization.
                // LoadChartView.IsGridVisible = false;
                // LoadChartView.GridWidth = new Unit("20%");
                // LoadChartView.ChartWidth = new Unit("80%");
                // LoadChartView.IsReadOnly = false;
                // LoadChartView.IsVirtualizing = false;

                // Optionally, set the scrollable timeline to present.
                // LoadChartView.TimelineStart = new DateTime(year, month, 1);
                // LoadChartView.TimelineFinish = new DateTime(year + 2, month, 1);

                // Set the displayed and current time values to automatically scroll to a specific chart coordinate, and display a vertical bar highlighter at the specified point.
                LoadChartView.DisplayedTime = new DateTime(year, month, 1);
                LoadChartView.CurrentTime = new DateTime(year, month, 2, 12, 0, 0);

                // Optionally, set theme and/or custom styles.
                // LoadChartView.Theme = PresentationTheme.Aero;
                // LoadChartView.BorderColor = Color.Gray;
                // LoadChartView.NormalAllocationBarStroke = Color.Green;
                // LoadChartView.NormalAllocationBarFill = Color.Green;
                // LoadChartView.UnderAllocationBarStroke = Color.Yellow;
                // LoadChartView.UnderAllocationBarFill = Color.Yellow;

                // Optionally, display alternative row background.
                // LoadChartView.AlternativeRowBackColor = Color.FromArgb(0xf9, 0xf9, 0xf9);

                // Optionally, configure selection.
                // LoadChartView.SelectionMode = SelectionMode.Extended;
                // LoadChartView.SelectedItemBackColor = Color.LightCyan;

                // Optionally, initialize item selection.
                // LoadChartView.Items[1].IsSelected = true;

                // Optionally, configure columns.
                // LoadChartView.Columns[(int)ColumnType.Content].Header = "Workers";
                // LoadChartView.Columns[(int)ColumnType.Content].Width = 240;
                // LoadChartView.Columns[(int)ColumnType.RowHeader].IsVisible = false;

                // Optionally, set custom item tag objects, properties, append read only custom columns bound to their values, and/or set up custom cell template code statements to be executed on the client side.
                // LoadChartView.Items[1].Tag = 10;
                // LoadChartView.Items[1].CustomValues["Description"] = "Custom description";
                // LoadChartView.Columns.Add(new Column { Header = "Description", Width = 200, PropertyName = "Description" });
                // LoadChartView.Columns.Add(new Column { Header = "Status", Width = 40, CellTemplateClientCode = "return control.ownerDocument.createTextNode(item.content == 'Resource 2' ? '!' : '');" });

                // Optionally, specify the application target in order for the component to adapt to the screen size.
                // LoadChartView.Target = PresentationTarget.Phone;

                // Optionally, set up custom initialization, and item property and selection change handlers.
                // LoadChartView.InitializingClientCode = "if (typeof console !== 'undefined') console.log('The component is about to be initialized.');";
                // LoadChartView.InitializedClientCode = "if (typeof console !== 'undefined') console.log('The component has been successfully initialized.');";
                // LoadChartView.ItemPropertyChangeHanderClientCode = "if (isDirect && isFinal && typeof console !== 'undefined') console.log(item.content + '.' + propertyName + ' has changed.');";
                // LoadChartView.ItemSelectionChangeHanderClientCode = "if (isSelected && isDirect && typeof console !== 'undefined') console.log(item.content + ' has been selected.');";

                // Optionally, initialize custom theme (themes.js).
                LoadChartView.InitializingClientCode += @";
                    initializeLoadChartTheme(control.settings, theme);";
            }

            // Optionally, receive server side notifications when selection changes have occured on the client side by handling the SelectionChanged event.
            // LoadChartView.SelectionChanged += delegate { Console.WriteLine("Selected item index: {0}.", LoadChartView.SelectedIndex); };
        }

        // Define user command methods.
        public void MoveItemUpButton_Click(object sender, EventArgs e)
        {
            var item = LoadChartView.SelectedItem;
            if (item == null)
                return;
            LoadChartView.MoveItemUp(item);
        }
        public void MoveItemDownButton_Click(object sender, EventArgs e)
        {
            var item = LoadChartView.SelectedItem;
            if (item == null)
                return;
            LoadChartView.MoveItemDown(item);
        }
        public void IncreaseTimelinePageButton_Click(object sender, EventArgs e)
        {
            LoadChartView.IncreaseTimelinePage(TimeSpan.FromDays(4 * 7)); // 4 weeks
        }
        public void DecreaseTimelinePageButton_Click(object sender, EventArgs e)
        {
            LoadChartView.DecreaseTimelinePage(TimeSpan.FromDays(4 * 7)); // 4 weeks
        }
        public void SetCustomScalesButton_Click(object sender, EventArgs e)
        {
            LoadChartView.HeaderHeight = 21 * 3;
            LoadChartView.Scales = new List<Scale>
            {
                new Scale { ScaleType = ScaleType.Months, HeaderTextFormat = ScaleHeaderTextFormat.Month, IsSeparatorVisible = true },
                new Scale { ScaleType = ScaleType.Weeks, HeaderTextFormat = ScaleHeaderTextFormat.Date, IsSeparatorVisible = true },
                new Scale { ScaleType = ScaleType.Days, HeaderTextFormat = ScaleHeaderTextFormat.Day }
            };
            LoadChartView.CurrentTimeLineColor = Color.Red;
            LoadChartView.UpdateScaleInterval = TimeSpan.FromHours(1);
            LoadChartView.HourWidth = 5;
            LoadChartView.VisibleWeekStart = DayOfWeek.Monday;
            LoadChartView.VisibleWeekFinish = DayOfWeek.Friday;
            LoadChartView.WorkingWeekStart = DayOfWeek.Monday;
            LoadChartView.WorkingWeekFinish = DayOfWeek.Thursday;
            LoadChartView.VisibleDayStart = TimeOfDay.Parse("10:00:00"); // 10 AM
            LoadChartView.VisibleDayFinish = TimeOfDay.Parse("20:00:00");  // 8 PM
            LoadChartView.SpecialNonworkingDays = new List<Date> { new Date(year, month, 24), new Date(year, month, 25) };
        }
        public void ZoomInButton_Click(object sender, EventArgs e)
        {
            LoadChartView.HourWidth *= 2;
        }
        public void PrintButton_Click(object sender, EventArgs e)
        {
            // Print the resource list column and a selected timeline page of 5 weeks (timeline end week extensions would be added automatically, if necessary).
            LoadChartView.Print(title: "Load Chart (printable)", isGridVisible: true, columnIndexes: new int[] { 1 }, timelineStart: new DateTime(year, month, 1), timelineFinish: new DateTime(year, month, 1).AddDays(5 * 7), preparingMessage: "...");
        }
    }
}
