using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DlhSoft.Web.UI.WebControls;
using System.Drawing;
using DlhSoft.Windows.Data;

namespace Demos.Samples.CSharp.ScheduleChartView.MainFeatures
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
                ScheduleChartView.InitializingContent = "...";

                // Prepare data items.
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

                // Optionally, define and assign default task item schedule (working week and day intervals, and nonworking days).
                // ScheduleChartView.Schedule = new Schedule
                // {
                //     WorkingWeekStart = DayOfWeek.Monday,
                //     WorkingWeekFinish = DayOfWeek.Friday,
                //     WorkingDayStart = TimeOfDay.Parse("08:00:00"), // 8 AM
                //     WorkingDayFinish = TimeOfDay.Parse("16:00:00"), // 4 PM
                //     // SpecialNonworkingDays = new List<Date> { new Date(year, month, 19), new Date(year, month, 21) } // excluded
                // };

                // Optionally, hide data grid or set grid and chart widths, set read only settings, and/or disable virtualization.
                // ScheduleChartView.IsGridVisible = false;
                // ScheduleChartView.GridWidth = new Unit("20%");
                // ScheduleChartView.ChartWidth = new Unit("80%");
                // ScheduleChartView.IsGridReadOnly = true;
                // ScheduleChartView.IsChartReadOnly = true;
                // ScheduleChartView.IsVirtualizing = false;

                // Optionally, set the scrollable timeline to present.
                // ScheduleChartView.TimelineStart = new DateTime(year, month, 1);
                // ScheduleChartView.TimelineFinish = new DateTime(year + 2, month, 1);

                // Set the displayed and current time values to automatically scroll to a specific chart coordinate, and display a vertical bar highlighter at the specified point.
                ScheduleChartView.DisplayedTime = new DateTime(year, month, 1);
                ScheduleChartView.CurrentTime = new DateTime(year, month, 2, 12, 0, 0);

                // Optionally, set theme and/or custom styles.
                // ScheduleChartView.Theme = DlhSoft.Web.UI.WebControls.PresentationTheme.Aero;
                // ScheduleChartView.BorderColor = Color.Gray;
                // ScheduleChartView.GridLinesColor = Color.LightGray;
                // ScheduleChartView.StandardBarStroke = Color.Green;
                // ScheduleChartView.StandardBarFill = Color.LightGreen;
                // ScheduleChartView.StandardCompletedBarFill = Color.DarkGreen;
                // ScheduleChartView.StandardCompletedBarStroke = Color.DarkGreen;

                // Optionally, display alternative row background.
                // ScheduleChartView.AlternativeRowBackColor = Color.FromArgb(0xf9, 0xf9, 0xf9);

                // Optionally, configure selection.
                // ScheduleChartView.SelectionMode = SelectionMode.Extended;
                // ScheduleChartView.SelectedItemBackColor = Color.LightCyan;

                // Optionally, initialize item selection.
                // ScheduleChartView.Items[1].IsSelected = true;

                // Optionally, configure existing columns.
                // ScheduleChartView.Columns[(int)ColumnType.Content].Header = "Workers";
                // ScheduleChartView.Columns[(int)ColumnType.Content].Width = 240;
                // ScheduleChartView.Columns[(int)ColumnType.RowHeader].IsVisible = false;

                // Optionally, set custom item tag objects, properties, and/or append read only custom columns bound to their values.
                // ScheduleChartView.Items[1].Tag = 70;
                // ScheduleChartView.Items[1].CustomValues["Description"] = "Custom description";
                // ScheduleChartView.Columns.Add(new Column { Header = "Description", Width = 200, PropertyName = "Description" });
                // ScheduleChartView.Columns.Add(new Column { Header = "Status", Width = 40, CellTemplateClientCode = "return control.ownerDocument.createTextNode(item.content == 'Resource 2' ? '!' : '');" });

                // Optionally, set up item template code statements, and standard, milestone, and/or extra task template code statements to be executed on the client side.
                // ScheduleChartView.ItemTemplateClientCode = @"var toolTip = document.createElementNS('http://www.w3.org/2000/svg', 'title');
                //     var toolTipContent = item.content + ' • ' + 'Start: ' + item.start.toLocaleString();
                //     if (!item.isMilestone)
                //         toolTipContent += ' • ' + 'Finish: ' + item.finish.toLocaleString();
                //     toolTip.appendChild(document.createTextNode(toolTipContent));
                //     return toolTip;";
                // ScheduleChartView.StandardTaskTemplateClientCode = @"var document = control.ownerDocument, svgns = 'http://www.w3.org/2000/svg';
                //     var itemLeft = control.getChartPosition(item.start, control.settings), itemRight = control.getChartPosition(item.finish, control.settings);
                //     var containerGroup = document.createElementNS(svgns, 'g'); 
                //     var rect = document.createElementNS(svgns, 'rect'); 
                //     rect.setAttribute('x', itemLeft); rect.setAttribute('width', Math.max(0, itemRight - itemLeft - 1));
                //     rect.setAttribute('y', control.settings.barMargin); rect.setAttribute('height', control.settings.barHeight);
                //     rect.setAttribute('style', 'stroke: Red; fill: LightYellow');
                //     containerGroup.appendChild(rect);
                //     var thumb = document.createElementNS(svgns, 'rect');
                //     thumb.setAttribute('x', itemLeft); thumb.setAttribute('width', Math.max(0, itemRight - itemLeft - 1));
                //     thumb.setAttribute('y', control.settings.barMargin); thumb.setAttribute('height', control.settings.barHeight);
                //     thumb.setAttribute('style', 'fill: Transparent; cursor: move');
                //     DlhSoft.Controls.ScheduleChartView.initializeTaskDraggingThumbs(thumb, null, null, null, item, itemLeft, itemRight, null);
                //     containerGroup.appendChild(thumb);
                //     return containerGroup;";
                // ScheduleChartView.ExtraTaskTemplateClientCode = "var rect = control.ownerDocument.createElementNS('http://www.w3.org/2000/svg', 'rect'); var itemLeft = control.getChartPosition(item.start, control.settings); rect.setAttribute('x', itemLeft - 20 + (!item.isMilestone ? 5 : 0)); rect.setAttribute('y', control.settings.barMargin); rect.setAttribute('width', 12); rect.setAttribute('height', control.settings.barHeight); rect.setAttribute('style', 'stroke: Blue; fill: Blue; fill-opacity: 0.1'); return rect;";
                // ScheduleChartView.Items[1].GanttChartItems[2].TaskTemplateClientCode = "var rect = control.ownerDocument.createElementNS('http://www.w3.org/2000/svg', 'rect'); var itemLeft = control.getChartPosition(item.start, control.settings); var itemRight = control.getChartPosition(item.finish, control.settings); rect.setAttribute('x', itemLeft); rect.setAttribute('y', control.settings.barMargin); rect.setAttribute('width', Math.max(0, itemRight - itemLeft - 1)); rect.setAttribute('height', control.settings.barHeight); rect.setAttribute('style', 'stroke: DarkGreen; fill: LightGreen'); return rect;";

                // Optionally, specify the application target in order for the component to adapt to the screen size.
                // ScheduleChartView.Target = DlhSoft.Web.UI.WebControls.PresentationTarget.Phone;

                // Optionally, set up custom initialization, and item property and selection change handlers.
                // ScheduleChartView.InitializingClientCode = "if (typeof console !== 'undefined') console.log('The component is about to be initialized.');";
                // ScheduleChartView.InitializedClientCode = "if (typeof console !== 'undefined') console.log('The component has been successfully initialized.');";
                // ScheduleChartView.ItemPropertyChangeHanderClientCode = "if (isDirect && isFinal && typeof console !== 'undefined') console.log(item.content + '.' + propertyName + ' has changed.');";
                // ScheduleChartView.ItemSelectionChangeHanderClientCode = "if (isSelected && isDirect && typeof console !== 'undefined') console.log(item.content + ' has been selected.');";

                // Optionally, initialize custom theme and templates (themes.js, templates.js).
                ScheduleChartView.InitializingClientCode += @";
                    initializeScheduleChartTheme(control.settings, theme);
                    initializeScheduleChartTemplates(control.settings, theme);";
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
        public void SetCustomBarColorToItemButton_Click(object sender, EventArgs e)
        {
            var resource = ScheduleChartView.SelectedItem;
            if (resource == null)
                return;
            foreach (GanttChartItem item in resource.GanttChartItems)
            {
                item.BarStroke = Color.Green;
                item.BarFill = Color.YellowGreen;
            }
            ScheduleChartView.ScrollTo(resource);
        }
        public void IncreaseTimelinePageButton_Click(object sender, EventArgs e)
        {
            ScheduleChartView.IncreaseTimelinePage(TimeSpan.FromDays(4 * 7)); // 4 weeks
        }
        public void DecreaseTimelinePageButton_Click(object sender, EventArgs e)
        {
            ScheduleChartView.DecreaseTimelinePage(TimeSpan.FromDays(4 * 7)); // 4 weeks
        }
        public void SetCustomScalesButton_Click(object sender, EventArgs e)
        {
            ScheduleChartView.HeaderHeight = 21 * 3;
            ScheduleChartView.Scales = new List<Scale>
            {
                new Scale { ScaleType = ScaleType.Months, HeaderTextFormat = ScaleHeaderTextFormat.Month, IsSeparatorVisible = true },
                new Scale { ScaleType = ScaleType.Weeks, HeaderTextFormat = ScaleHeaderTextFormat.Date, IsSeparatorVisible = true },
                new Scale { ScaleType = ScaleType.Days, HeaderTextFormat = ScaleHeaderTextFormat.Day }
            };
            ScheduleChartView.CurrentTimeLineColor = Color.Red;
            ScheduleChartView.UpdateScaleInterval = TimeSpan.FromHours(1);
            ScheduleChartView.HourWidth = 5;
            ScheduleChartView.VisibleWeekStart = DayOfWeek.Monday;
            ScheduleChartView.VisibleWeekFinish = DayOfWeek.Friday;
            ScheduleChartView.WorkingWeekStart = DayOfWeek.Monday;
            ScheduleChartView.WorkingWeekFinish = DayOfWeek.Thursday;
            ScheduleChartView.VisibleDayStart = TimeOfDay.Parse("10:00:00"); // 10 AM
            ScheduleChartView.VisibleDayFinish = TimeOfDay.Parse("20:00:00"); // 8 PM
            ScheduleChartView.SpecialNonworkingDays = new List<Date> { new Date(year, month, 24), new Date(year, month, 25) };
        }
        public void ZoomInButton_Click(object sender, EventArgs e)
        {
            ScheduleChartView.HourWidth *= 2;
        }
        public void PrintButton_Click(object sender, EventArgs e)
        {
            // Print the task hierarchy column and a selected timeline page of 5 weeks (timeline end week extensions would be added automatically, if necessary).
            // Optionally, to rotate the print output and simulate Landscape printing mode (when the end user keeps Portrait selection in the Print dialog), append the rotate parameter set to true to the method call: rotate: true.
            ScheduleChartView.Print(title: "Schedule Chart (printable)", isGridVisible: true, columnIndexes: new[] { 1 }, timelineStart: new DateTime(year, month, 1), timelineFinish: new DateTime(year, month, 1).AddDays(5 * 7), preparingMessage: "...");
        }
    }
}
