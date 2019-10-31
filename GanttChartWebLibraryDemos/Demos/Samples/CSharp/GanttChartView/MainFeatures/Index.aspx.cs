using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DlhSoft.Web.UI.WebControls;
using System.Drawing;
using DlhSoft.Windows.Data;

namespace Demos.Samples.CSharp.GanttChartView.MainFeatures
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
                GanttChartView.InitializingContent = "...";

                // Prepare data items.
                var items = new List<GanttChartItem>
                {
                    new GanttChartItem { Content = "Task 1", IsExpanded = false },
                    new GanttChartItem { Content = "Task 1.1", Indentation = 1, Start = new DateTime(year, month, 2, 8, 0, 0), Finish = new DateTime(year, month, 4, 16, 0, 0) },
                    new GanttChartItem { Content = "Task 1.2", Indentation = 1, Start = new DateTime(year, month, 3, 8, 0, 0), Finish = new DateTime(year, month, 5, 12, 0, 0) },
                    new GanttChartItem { Content = "Task 2", IsExpanded = true },
                    new GanttChartItem { Content = "Task 2.1", Indentation = 1, Start = new DateTime(year, month, 2, 8, 0, 0), Finish = new DateTime(year, month, 8, 16, 0, 0), CompletedFinish = new DateTime(year, month, 5, 16, 0, 0), AssignmentsContent = "Resource 1, Resource 2 [50%]" },
                    new GanttChartItem { Content = "Task 2.2", Indentation = 1 },
                    new GanttChartItem { Content = "Task 2.2.1", Indentation = 2, Start = new DateTime(year, month, 11, 8, 0, 0), Finish = new DateTime(year, month, 14, 16, 0, 0), CompletedFinish = new DateTime(year, month, 14, 16, 0, 0), AssignmentsContent = "Resource 2" },
                    new GanttChartItem { Content = "Task 2.2.2", Indentation = 2, Start = new DateTime(year, month, 12, 12, 0, 0), Finish = new DateTime(year, month, 14, 16, 0, 0), AssignmentsContent = "Resource 2" },
                    new GanttChartItem { Content = "Task 3", Indentation = 1, Start = new DateTime(year, month, 15, 16, 0, 0), IsMilestone = true },
                };
                items[3].Predecessors = new List<PredecessorItem> { new PredecessorItem { Item = items[0], DependencyType = DependencyType.StartStart } };
                items[7].Predecessors = new List<PredecessorItem> { new PredecessorItem { Item = items[6], Lag = TimeSpan.FromHours(2) } };
                items[8].Predecessors = new List<PredecessorItem> { new PredecessorItem { Item = items[4] }, new PredecessorItem { Item = items[5] } };
                for (int i = 4; i <= 16; i++)
                    items.Add(new GanttChartItem { Content = "Task " + i, Indentation = i >= 8 && i % 3 == 2 ? 0 : 1, Start = new DateTime(year, month, 2 + (i <= 8 ? (i - 4) * 3 : i - 8), 8, 0, 0), Finish = new DateTime(year, month, 2 + (i <= 8 ? (i - 4) * 3 + (i > 8 ? 6 : 1) : i - 2), 16, 0, 0) });
                items[9].Finish = items[9].Finish + TimeSpan.FromDays(2);
                items[9].AssignmentsContent = "Resource 1";
                items[10].Predecessors = new List<PredecessorItem> { new PredecessorItem { Item = items[9] } };
                GanttChartView.Items = items;

                // Optionally, set baseline properties.
                GanttChartView.Items[6].BaselineStart = new DateTime(year, month, 10, 8, 0, 0);
                GanttChartView.Items[6].BaselineFinish = new DateTime(year, month, 11, 16, 0, 0);
                GanttChartView.Items[7].BaselineStart = new DateTime(year, month, 8, 8, 0, 0);
                GanttChartView.Items[7].BaselineFinish = new DateTime(year, month, 11, 16, 0, 0);
                GanttChartView.Items[8].BaselineStart = new DateTime(year, month, 12, 8, 0, 0);

                // Optionally, define and assign default and specific task item schedules (working week and day intervals, and nonworking days).
                // GanttChartView.Schedule = new Schedule
                // {
                //     WorkingWeekStart = DayOfWeek.Monday,
                //     WorkingWeekFinish = DayOfWeek.Friday,
                //     WorkingDayStart = TimeOfDay.Parse("08:00:00"), // 8 AM
                //     WorkingDayFinish = TimeOfDay.Parse("16:00:00"), // 4 PM
                //     // SpecialNonworkingDays = new List<Date> { new Date(year, month, 19), new Date(year, month, 21) } // excluded
                // };
                // Schedule specialSchedule = new Schedule
                // {
                //     WorkingWeekStart = DayOfWeek.Sunday,
                //     WorkingWeekFinish = DayOfWeek.Wednesday,
                //     WorkingDayStart = TimeOfDay.Parse("09:00:00"), // 9 AM
                //     WorkingDayFinish = TimeOfDay.Parse("18:00:00"), // 7 PM, exceeding visible 4 PM
                //     // SpecialNonworkingDays = new List<Date> { new Date(year, month, 18), new Date(year, month, 21), new Date(year, month, 22) } // partial replacement for excluded dates
                // };
                // GanttChartView.Items[9].Schedule = specialSchedule;
                // GanttChartView.Items[10].Schedule = specialSchedule;
                // GanttChartView.IsIndividualItemNonworkingTimeHighlighted = true;

                // Optionally, hide data grid or set grid and chart widths, set read only settings, and/or disable virtualization.
                // GanttChartView.IsGridVisible = false;
                // GanttChartView.GridWidth = new Unit("30%");
                // GanttChartView.ChartWidth = new Unit("70%");
                // GanttChartView.IsGridReadOnly = true;
                // GanttChartView.IsChartReadOnly = true;
                // GanttChartView.IsVirtualizing = false;

                // Optionally, preseve task effort when start value changes in the grid.
                // GanttChartView.IsTaskEffortPreservedWhenStartChangesInGrid = true;

                // Optionally, set the scrollable timeline to present.
                // GanttChartView.TimelineStart = new DateTime(year, month, 1);
                // GanttChartView.TimelineFinish = new DateTime(year + 2, month, 1);

                // Set the displayed and current time values to automatically scroll to a specific chart coordinate, and display a vertical bar highlighter at the specified point.
                GanttChartView.DisplayedTime = new DateTime(year, month, 1);
                GanttChartView.CurrentTime = new DateTime(year, month, 2, 12, 0, 0);

                // Optionally, set theme and/or custom styles.
                // GanttChartView.Theme = DlhSoft.Web.UI.WebControls.PresentationTheme.Aero;
                // GanttChartView.BorderColor = Color.Gray;
                // GanttChartView.GridLinesColor = Color.LightGray;
                // GanttChartView.StandardBarStroke = Color.Green;
                // GanttChartView.StandardBarFill = Color.LightGreen;
                // GanttChartView.StandardCompletedBarFill = Color.DarkGreen;
                // GanttChartView.StandardCompletedBarStroke = Color.DarkGreen;
                // GanttChartView.DependencyLineStroke = Color.Green;

                // Optionally, display alternative row background.
                // GanttChartView.AlternativeRowBackColor = Color.FromArgb(0xf9, 0xf9, 0xf9);

                // Optionally, configure selection.
                // GanttChartView.SelectionMode = SelectionMode.Extended;
                // GanttChartView.SelectedItemBackColor = Color.LightCyan;

                // Optionally, initialize item selection.
                // GanttChartView.Items[6].IsSelected = true;

                // Optionally, configure existing columns.
                // GanttChartView.Columns[(int)ColumnType.Content].Header = "Work items";
                // GanttChartView.Columns[(int)ColumnType.Content].Width = 240;
                // GanttChartView.Columns[(int)ColumnType.Start].Header = "Beginning";
                // GanttChartView.Columns[(int)ColumnType.Finish].Header = "End";
                // GanttChartView.Columns[(int)ColumnType.Milestone].Header = "Is milestone";
                // GanttChartView.Columns[(int)ColumnType.Completed].Header = "Is completed";
                // GanttChartView.Columns[(int)ColumnType.Assignments].Header = "Workers";
                // GanttChartView.Columns[(int)ColumnType.RowHeader].IsVisible = false;

                // Optionally, add supplemental columns. Note that columns obtained using get*Template client side method calls are using invariant culture, tough.
                GanttChartView.Columns.Insert(2, new Column { Header = string.Empty, Width = 40, CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getIndexColumnTemplate()(item);", AllowUserToResize = false });
                GanttChartView.Columns.Insert(5, new Column { Header = "Effort (h)", Width = 80, CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getTotalEffortColumnTemplate(64)(item);" });
                GanttChartView.Columns.Insert(6, new Column { Header = "Duration (d)", Width = 80, CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getDurationColumnTemplate(64, 8)(item);" });
                GanttChartView.Columns.Insert(10, new Column { Header = "%", Width = 80, CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getCompletionColumnTemplate(64)(item);" });
                GanttChartView.Columns.Insert(11, new Column { Header = "Predecessors", Width = 100, CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getPredecessorsColumnTemplate(84)(item);" });
                GanttChartView.Columns.Add(new Column { Header = "Cost ($)", Width = 100, CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getCostColumnTemplate(84)(item);" });
                GanttChartView.Columns.Add(new Column { Header = "Est. start", Width = 140, CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getBaselineStartColumnTemplate(124, true, true, 8 * 60 * 60 * 1000)(item);" }); // 8 AM
                GanttChartView.Columns.Add(new Column { Header = "Est. finish", Width = 140, CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getBaselineFinishColumnTemplate(124, true, true, 16 * 60 * 60 * 1000)(item);" }); // 4 PM

                // Optionally, set custom item tag objects, properties, append read only custom columns bound to their values, and/or set up custom cell template code statements to be executed on the client side.
                // GanttChartView.Items[7].Tag = 70;
                // GanttChartView.Items[7].CustomValues["Description"] = "Custom description";
                // GanttChartView.Columns.Add(new Column { Header = "Description", Width = 200, PropertyName = "Description" });
                // GanttChartView.Columns.Add(new Column { Header = "Status", Width = 40, CellTemplateClientCode = "return control.ownerDocument.createTextNode(item.content == 'Task 2.1' ? '!' : '');" });
                // GanttChartView.Columns[(int)ColumnType.Assignments].CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getAssignmentSelectorColumnTemplate(184, function (item) { return ['Resource 1', 'Resource 2']; })(item);"; // Resource assignment selector.

                // Optionally, define assignable resources.
                GanttChartView.AssignableResources = new List<string> { "Resource 1", "Resource 2", "Resource 3",
                                                                        "Material 1", "Material 2" };
                GanttChartView.AutoAppendAssignableResources = true;

                // Optionally, define the quantity values to consider when leveling resources, indicating maximum material amounts available for use at the same time.
                GanttChartView.ResourceQuantities = new Dictionary<string, double> { { "Material 1", 4 }, { "Material 2", double.PositiveInfinity } };
                items[10].AssignmentsContent = "Material 1 [300%], Material 2";
                items[11].AssignmentsContent = "Material 1 [50%], Material 2 [200%]";
                items[12].AssignmentsContent = "Material 1 [250%]";

                // Optionally, define task and resource costs.
                // GanttChartView.TaskInitiationCost = 5;
                items[4].ExecutionCost = 50;
                // GanttChartView.DefaultResourceUsageCost = 1;
                // GanttChartView.SpecificResourceUsageCosts = new Dictionary<string, double> { { "Resource 1", 2 }, { "Material 1", 7 } };
                GanttChartView.DefaultResourceHourCost = 10;
                GanttChartView.SpecificResourceHourCosts = new Dictionary<string, double> { { "Resource 1", 20 }, { "Material 2", 0.5 } };

                // Optionally, display multiple item parts on a single chart line.
                // GanttChartView.Items[13].Parts = new List<GanttChartItem>
                // {
                //     new GanttChartItem { Content = "Task 8 (Part 1)", Start = new DateTime(year, month, 2, 8, 0, 0), Finish = new DateTime(year, month, 4, 16, 0, 0) },
                //     new GanttChartItem { Content = "Task 8 (Part 2)", Start = new DateTime(year, month, 8, 8, 0, 0), Finish = new DateTime(year, month, 10, 12, 0, 0), AssignmentsContent = "Resource 1" }
                // };

                // Optionally, set up item template code statements, and standard, summary, milestone, and/or extra task template code statements to be executed on the client side.
                // GanttChartView.ItemTemplateClientCode = @"var toolTip = document.createElementNS('http://www.w3.org/2000/svg', 'title');
                //     var toolTipContent = item.content + '\n' + 'Start: ' + item.start.toLocaleString();
                //     if (!item.isMilestone)
                //         toolTipContent += '\n' + 'Finish: ' + item.finish.toLocaleString();
                //     toolTip.appendChild(document.createTextNode(toolTipContent));
                //     return toolTip;";
                // GanttChartView.StandardTaskTemplateClientCode = @"var document = control.ownerDocument, svgns = 'http://www.w3.org/2000/svg';
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
                //     DlhSoft.Controls.GanttChartView.initializeTaskDraggingThumbs(thumb, null, null, null, item, itemLeft, itemRight, null);
                //     containerGroup.appendChild(thumb);
                //     return containerGroup;";
                // GanttChartView.ExtraTaskTemplateClientCode = "var rect = control.ownerDocument.createElementNS('http://www.w3.org/2000/svg', 'rect'); var itemLeft = control.getChartPosition(item.start, control.settings); rect.setAttribute('x', itemLeft - 20 + (!item.hasChildren && !item.isMilestone ? 5 : 0)); rect.setAttribute('y', control.settings.barMargin); rect.setAttribute('width', 12); rect.setAttribute('height', control.settings.barHeight); rect.setAttribute('style', 'stroke: Blue; fill: Blue; fill-opacity: 0.1'); return rect;";
                // GanttChartView.Items[7].TaskTemplateClientCode = "var rect = control.ownerDocument.createElementNS('http://www.w3.org/2000/svg', 'rect'); var itemLeft = control.getChartPosition(item.start, control.settings); var itemRight = control.getChartPosition(item.finish, control.settings); rect.setAttribute('x', itemLeft); rect.setAttribute('y', control.settings.barMargin); rect.setAttribute('width', Math.max(0, itemRight - itemLeft - 1)); rect.setAttribute('height', control.settings.barHeight); rect.setAttribute('style', 'stroke: DarkGreen; fill: LightGreen'); return rect;";
                // Optionally, apply visibility filter to display only specific items in the view.
                // GanttChartView.VisibilityFilterClientCode = "return item.content.indexOf('Task 2') >= 0;";

                // Optionally, set HasFixedEffort to true to automatically update item assignment allocation units rather than effort upon duration changes.
                // GanttChartView.Items[4].HasFixedEffort = true;

                // Optionally, set up auto-scheduling behavior for dependent tasks based on predecessor information, supplementary disallowing circular dependencies.
                GanttChartView.AreTaskDependencyConstraintsEnabled = true;

                // Optionally, disable auto-scheduling for specific items (turning on manual scheduling back for them.)
                // GanttChartView.Items[7].AreDependencyConstraintsEnabled = false;

                // Optionally, specify the application target in order for the component to adapt to the screen size.
                // GanttChartView.Target = DlhSoft.Web.UI.WebControls.PresentationTarget.Phone;

                // Optionally, set up custom initialization, and item property and selection change handlers.
                // GanttChartView.InitializingClientCode = "control.settings.dateTimeFormatter = control.settings.dateFormatter; if (typeof console !== 'undefined') console.log('The component is about to be initialized using simple date formatting.');";
                // GanttChartView.InitializedClientCode = "if (typeof console !== 'undefined') console.log('The component has been successfully initialized.');";
                // GanttChartView.ItemPropertyChangeHandlerClientCode = "if (isDirect && isFinal && typeof console !== 'undefined') console.log(item.content + '.' + propertyName + ' has changed.');";
                // GanttChartView.ItemSelectionChangeHandlerClientCode = "if (isSelected && isDirect && typeof console !== 'undefined') console.log(item.content + ' has been selected.');";
            }

            // Optionally, initialize custom theme and templates (themes.js, templates.js).
            Func<string, string> initializingClientCodeGetter = (string type) => @";
                initialize" + type + @"Theme(control.settings, theme);" + 
                (type == "GanttChart" || type == "ScheduleChart" || type == "PertChart" ? @"
                initialize" + type + @"Templates(control.settings, theme);" : string.Empty);
            if (!IsPostBack)
                GanttChartView.InitializingClientCode += initializingClientCodeGetter("GanttChart");
            ScheduleChartView.InitializingClientCode = initializingClientCodeGetter("ScheduleChart");
            LoadChartView.InitializingClientCode = initializingClientCodeGetter("LoadChart");
            PertChartView.InitializingClientCode = initializingClientCodeGetter("PertChart");
            NetworkDiagramView.InitializingClientCode = initializingClientCodeGetter("PertChart");

            // Optionally, receive server side notifications when selection changes have occured on the client side by handling the SelectionChanged event.
            // GanttChartView.SelectionChanged += delegate { Console.WriteLine("Selected item index: {0}.", GanttChartView.SelectedIndex); };

            // Receive server side notifications for the item property changes that have occured on the client side by handling the ItemPropertyChanged event.
            GanttChartView.ItemPropertyChanged += GanttChartView_ItemPropertyChanged;
        }

        // Handle the individual item property change retreived as event argument.
        private void GanttChartView_ItemPropertyChanged(object sender, ItemPropertyChangedEventArgs e)
        {
            // Optionally or alternatively, record the item property change in a temporary storage collection easily accessible in the application code later.
            // Changes.Add(e);
        }

        //private List<ItemPropertyChangedEventArgs> Changes
        //{
        //    get
        //    {
        //        if (Session["Changes"] == null)
        //            Session["Changes"] = new List<ItemPropertyChangedEventArgs>();
        //        return Session["Changes"] as List<ItemPropertyChangedEventArgs>;
        //    }
        //}

        // Define user command methods.
        public void AddNewItemButton_Click(object sender, EventArgs e)
        {
            var item = new GanttChartItem { Content = "New task", Start = new DateTime(year, month, 2, 8, 0, 0), Finish = new DateTime(year, month, 4, 16, 0, 0) };
            GanttChartView.AddItem(item);
            GanttChartView.SelectedItem = item;
            GanttChartView.ScrollTo(item);
            GanttChartView.ScrollTo(new DateTime(year, month, 1));
        }
        public void InsertNewItemButton_Click(object sender, EventArgs e)
        {
            if (GanttChartView.SelectedItem == null)
                return;
            var item = new GanttChartItem { Content = "New task", Start = new DateTime(year, month, 2, 8, 0, 0), Finish = new DateTime(year, month, 4, 16, 0, 0) };
            GanttChartView.InsertItem(GanttChartView.SelectedIndex, item);
            GanttChartView.SelectedItem = item;
            GanttChartView.ScrollTo(item);
            GanttChartView.ScrollTo(new DateTime(year, month, 1));
        }
        public void IncreaseItemIndentationButton_Click(object sender, EventArgs e)
        {
            var item = GanttChartView.SelectedItem;
            if (item == null)
                return;
            GanttChartView.IncreaseItemIndentation(item);
            GanttChartView.ScrollTo(item);
        }
        public void DecreaseItemIndentationButton_Click(object sender, EventArgs e)
        {
            var item = GanttChartView.SelectedItem;
            if (item == null)
                return;
            GanttChartView.DecreaseItemIndentation(item);
            GanttChartView.ScrollTo(item);
        }
        public void DeleteItemButton_Click(object sender, EventArgs e)
        {
            var item = GanttChartView.SelectedItem;
            if (item == null)
                return;
            GanttChartView.RemoveItem(item);
        }
        public void CopyItemButton_Click(object sender, EventArgs e)
        {
            var item = GanttChartView.SelectedItem;
            if (item == null)
                return;
            ViewState["CopiedItem"] = item;
        }
        public void PasteItemButton_Click(object sender, EventArgs e)
        {
            if (ViewState["CopiedItem"] == null || GanttChartView.SelectedItem == null)
                return;
            var copiedItem = ViewState["CopiedItem"] as GanttChartItem;
            var item = new GanttChartItem { Content = copiedItem.Content, Start = copiedItem.Start, Finish = copiedItem.Finish, CompletedFinish = copiedItem.CompletedFinish, IsMilestone = copiedItem.IsMilestone, AssignmentsContent = copiedItem.AssignmentsContent };
            GanttChartView.InsertItem(GanttChartView.SelectedIndex + 1, item);
            GanttChartView.SelectedItem = item;
            GanttChartView.ScrollTo(item);
            GanttChartView.ScrollTo(item.Start);
        }
        public void MoveItemUpButton_Click(object sender, EventArgs e)
        {
            var item = GanttChartView.SelectedItem;
            if (item == null)
                return;
            GanttChartView.MoveItemUp(item, true, true);
        }
        public void MoveItemDownButton_Click(object sender, EventArgs e)
        {
            var item = GanttChartView.SelectedItem;
            if (item == null)
                return;
            GanttChartView.MoveItemDown(item, true, true);
        }
        public void SetCustomBarColorToItemButton_Click(object sender, EventArgs e)
        {
            var item = GanttChartView.SelectedItem;
            if (item == null)
                return;
            item.BarStroke = Color.Red;
            item.BarFill = Color.Yellow;
            GanttChartView.ScrollTo(item);
        }
        public void IncreaseTimelinePageButton_Click(object sender, EventArgs e)
        {
            GanttChartView.IncreaseTimelinePage(TimeSpan.FromDays(4 * 7)); // 4 weeks
        }
        public void DecreaseTimelinePageButton_Click(object sender, EventArgs e)
        {
            GanttChartView.DecreaseTimelinePage(TimeSpan.FromDays(4 * 7)); // 4 weeks
        }
        public void SetCustomScalesButton_Click(object sender, EventArgs e)
        {
            GanttChartView.HeaderHeight = 21 * 3;
            GanttChartView.Scales = new List<Scale>
            {
                new Scale { ScaleType = ScaleType.Months, HeaderTextFormat = ScaleHeaderTextFormat.Month, IsSeparatorVisible = true },
                new Scale { ScaleType = ScaleType.Weeks, HeaderTextFormat = ScaleHeaderTextFormat.Date, IsSeparatorVisible = true },
                new Scale { ScaleType = ScaleType.Days, HeaderTextFormat = ScaleHeaderTextFormat.Day }
            };
            GanttChartView.CurrentTimeLineColor = Color.Red;
            GanttChartView.UpdateScaleInterval = TimeSpan.FromHours(1);
            GanttChartView.HourWidth = 5;
            GanttChartView.VisibleWeekStart = DayOfWeek.Monday;
            GanttChartView.VisibleWeekFinish = DayOfWeek.Friday;
            GanttChartView.WorkingWeekStart = DayOfWeek.Monday;
            GanttChartView.WorkingWeekFinish = DayOfWeek.Thursday;
            GanttChartView.VisibleDayStart = TimeOfDay.Parse("10:00:00"); // 10 AM
            GanttChartView.VisibleDayFinish = TimeOfDay.Parse("20:00:00"); // 8 PM
            GanttChartView.SpecialNonworkingDays = new List<Date> { new Date(year, month, 24), new Date(year, month, 25) };
        }
        public void ZoomInButton_Click(object sender, EventArgs e)
        {
            GanttChartView.HourWidth *= 2;
        }
        public void ToggleBaselineButton_Click(object sender, EventArgs e)
        {
            GanttChartView.IsBaselineVisible = !GanttChartView.IsBaselineVisible;
            ToggleBaselineButton.CssClass = GanttChartView.IsBaselineVisible ? "toggle pressed" : "toggle";
        }
        public void HighlightCriticalPathButton_Click(object sender, EventArgs e)
        {
            HighlightCriticalPathButton.CssClass = "toggle pressed";
            // Reset the view.
            foreach (GanttChartItem item in GanttChartView.Items)
            {
                item.BarStroke = null;
                item.BarFill = null;
            }
            // Set up red as bar stroke and fill properties for the critical items.
            foreach (GanttChartItem item in GanttChartView.GetCriticalItems())
            {
                item.BarStroke = Color.Red;
                item.BarFill = Color.Red;
            }
        }
        public void SplitRemainingWorkButton_Click(object sender, EventArgs e)
        {
            if (GanttChartView.SelectedItem == null)
                return;
            var remainingWorkItem = GanttChartView.SplitRemainingWork(GanttChartView.SelectedItem, " (rem. work)", " (compl. work)");
            if (remainingWorkItem == null)
                return;
            GanttChartView.ScrollTo(remainingWorkItem);
        }
        public void ToggleDependencyConstraintsButton_Click(object sender, EventArgs e)
        {
            GanttChartView.AreTaskDependencyConstraintsEnabled = !GanttChartView.AreTaskDependencyConstraintsEnabled;
            ToggleDependencyConstraintsButton.CssClass = GanttChartView.AreTaskDependencyConstraintsEnabled ? "toggle pressed" : "toggle";
        }
        public void LevelResourcesButton_Click(object sender, EventArgs e)
        {
            // Level the assigned resources for all tasks, including the already started ones, considering the current time displayed in the chart.
            GanttChartView.LevelResources(true, GanttChartView.CurrentTime);
            // Alternatively, optimize work to obtain the minimum project finish date and time assuming unlimited resource availability:
            // GanttChartView.OptimizeWork(false, true, GanttChartView.CurrentTime);
        }
        public void ScheduleChartButton_Click(object sender, EventArgs e)
        {
            ScheduleChartPanel.Visible = true;
        }
        public void CloseScheduleChartViewButton_Click(object sender, EventArgs e)
        {
            ScheduleChartPanel.Visible = false;
        }
        public void LoadChartButton_Click(object sender, EventArgs e)
        {
            LoadChartPanel.Visible = true;
        }
        public void CloseLoadChartViewButton_Click(object sender, EventArgs e)
        {
            LoadChartPanel.Visible = false;
        }
        public void PertChartButton_Click(object sender, EventArgs e)
        {
            PertChartPanel.Visible = true;
        }
        public void ClosePertChartViewButton_Click(object sender, EventArgs e)
        {
            PertChartPanel.Visible = false;
        }
        public void NetworkDiagramButton_Click(object sender, EventArgs e)
        {
            NetworkDiagramPanel.Visible = true;
        }
        public void CloseNetworkDiagramViewButton_Click(object sender, EventArgs e)
        {
            NetworkDiagramPanel.Visible = false;
        }
        public void ProjectStatisticsButton_Click(object sender, EventArgs e)
        {
            var startOutput = GanttChartView.GetProjectStart().ToShortDateString();
            var finishOutput = GanttChartView.GetProjectFinish().ToShortDateString();
            var effortOutput = GanttChartView.GetProjectTotalEffort().TotalHours.ToString("0.##");
            var completionOutput = GanttChartView.GetProjectCompletion().ToString("0.##%");
            var costOutput = GanttChartView.GetProjectCost().ToString("$0.##");
            string output = "Project statistics:\\nStart:\\t" + startOutput + "\\nFinish:\\t" + finishOutput + "\\nEffort:\\t" + effortOutput + "h\\nCompl.:\\t" + completionOutput + "\\nCost:\\t" + costOutput;
            ScriptManager.RegisterStartupScript(this, typeof(Index), "ProjectStatistics", "setTimeout(function() { alert('" + output + "'); }, 1000);", true);
        }
        public void SaveProjectXmlButton_Click(object sender, EventArgs e)
        {
            Session["DownloadContent"] = GanttChartView.GetProjectXml();
            ScriptManager.RegisterClientScriptBlock(this, typeof(Index), "Download", "window.open('Download.aspx?ContentType=text/xml&Filename=Project.xml', '_blank');", true);
        }
        public void LoadProjectXmlButton_Click(object sender, EventArgs e)
        {
            LoadProjectXmlPanel.Visible = true;
        }
        public void LoadProjectXmlSubmitButton_Click(object sender, EventArgs e)
        {
            GanttChartView.LoadProjectXml(LoadProjectXmlFileUpload.FileContent);
            LoadProjectXmlPanel.Visible = false;
        }
        public void CloseLoadProjectXmlButton_Click(object sender, EventArgs e)
        {
            LoadProjectXmlPanel.Visible = false;
        }
        public void PrintButton_Click(object sender, EventArgs e)
        {
            // Print the task hierarchy column and a selected timeline page of 5 weeks (timeline end week extensions would be added automatically, if necessary).
            // Optionally, to rotate the print output and simulate Landscape printing mode (when the end user keeps Portrait selection in the Print dialog), append the rotate parameter set to true to the method call: rotate: true.
            GanttChartView.Print(title: "Gantt Chart (printable)", isGridVisible: true, columnIndexes: new int[] { 2 }, timelineStart: new DateTime(year, month, 1), timelineFinish: new DateTime(year, month, 1).AddDays(5 * 7), preparingMessage: "...");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Optionally, synchronize other displayed views at initialization time and upon standard Gantt Chart item changes on the client side.
            var initalizationCode = string.Format(@"var ganttChartView = document.querySelector('#{0}');", GanttChartView.ClientID);
            GanttChartView.InitializedClientCode += initalizationCode;
            GanttChartView.ItemPropertyChangeHandlerClientCode += initalizationCode;
            GanttChartView.DisplayedTimeChangeHandlerClientCode += initalizationCode;
            GanttChartView.SplitterPositionChangeHandlerClientCode += initalizationCode;
            if (ScheduleChartPanel.Visible)
            {
                ScheduleChartView.Items = GanttChartView.GetScheduleChartItems();
                ScheduleChartView.CopyCommonSettings(GanttChartView);
                GanttChartView.InitializedClientCode += string.Format(@"
                    ganttChartView.refreshScheduleChartView = function() {{
                        if (!ganttChartView.isWaitingToRefreshScheduleChartView) {{
                            ganttChartView.isWaitingToRefreshScheduleChartView = true;
                            setTimeout(function() {{
                                var scheduleChartView = document.querySelector('#{0}');
                                if (scheduleChartView == null || typeof scheduleChartView.isScheduleChartInitialized === 'undefined' || !scheduleChartView.isScheduleChartInitialized)
                                    return;
                                scheduleChartView.scheduleChartItems = ganttChartView.getScheduleChartItems();
                                ganttChartView.copyCommonSettings(scheduleChartView.settings);
                                scheduleChartView.refresh();
                                scheduleChartView.settings.displayedTimeChangeHandler = function(displayedTime) {{ ganttChartView.refreshDisplayedTime(displayedTime); if (typeof ganttChartView.refreshLoadChartViewDisplayedTime !== 'undefined') ganttChartView.refreshLoadChartViewDisplayedTime(displayedTime); }}
                                scheduleChartView.settings.splitterPositionChangeHandler = function(gridWidth, chartWidth) {{ ganttChartView.refreshSplitterPosition(gridWidth, chartWidth); if (typeof ganttChartView.refreshLoadChartViewSplitterPosition !== 'undefined') ganttChartView.refreshLoadChartViewSplitterPosition(gridWidth, chartWidth); }}
                                ganttChartView.isWaitingToRefreshScheduleChartView = false;
                            }}, 0);
                        }}
                    }};
                    setTimeout(function() {{
                        var scheduleChartView = document.querySelector('#{0}');
                        if (scheduleChartView == null)
                            return;
                        ganttChartView.copyCommonSettings(scheduleChartView.settings);
                        scheduleChartView.settings.displayedTimeChangeHandler = function(displayedTime) {{ ganttChartView.refreshDisplayedTime(displayedTime); if (typeof ganttChartView.refreshLoadChartViewDisplayedTime !== 'undefined') ganttChartView.refreshLoadChartViewDisplayedTime(displayedTime); }}
                        scheduleChartView.settings.splitterPositionChangeHandler = function(gridWidth, chartWidth) {{ ganttChartView.refreshSplitterPosition(gridWidth, chartWidth); if (typeof ganttChartView.refreshLoadChartViewSplitterPosition !== 'undefined') ganttChartView.refreshLoadChartViewSplitterPosition(gridWidth, chartWidth); }}
                    }}, 0);
                    ganttChartView.refreshScheduleChartViewDisplayedTime = function(displayedTime) {{
                        if (!ganttChartView.isWaitingToRefreshScheduleChartViewDisplayedTime) {{
                            ganttChartView.isWaitingToRefreshScheduleChartViewDisplayedTime = true;
                            setTimeout(function() {{
                                var scheduleChartView = document.querySelector('#{0}');
                                if (scheduleChartView == null || typeof scheduleChartView.isScheduleChartInitialized === 'undefined' || !scheduleChartView.isScheduleChartInitialized)
                                    return;
                                scheduleChartView.scrollToDateTime(displayedTime);
                                ganttChartView.isWaitingToRefreshScheduleChartViewDisplayedTime = false;
                            }}, 0);
                        }}
                    }};
                    ganttChartView.refreshScheduleChartViewSplitterPosition = function(gridWidth, chartWidth) {{
                        if (!ganttChartView.isWaitingToRefreshScheduleChartViewSplitterPosition) {{
                            ganttChartView.isWaitingToRefreshScheduleChartViewSplitterPosition = true;
                            setTimeout(function() {{
                                var scheduleChartView = document.querySelector('#{0}');
                                if (scheduleChartView == null || typeof scheduleChartView.isScheduleChartInitialized === 'undefined' || !scheduleChartView.isScheduleChartInitialized)
                                    return;
                                scheduleChartView.setSplitterPosition(gridWidth, chartWidth);
                                ganttChartView.isWaitingToRefreshScheduleChartViewSplitterPosition = false;
                            }}, 0);
                        }}
                    }};",
                    ScheduleChartView.ClientID);
                GanttChartView.ItemPropertyChangeHandlerClientCode += string.Format(@"
                    if (isDirect && isFinal && ((!item.hasChildren && (propertyName == 'content' || propertyName == 'start' || propertyName == 'finish' || propertyName == 'completedFinish' || propertyName == 'isMilestone' || propertyName == 'assignmentsContent')) || propertyName == 'indentation')) {{
                        ganttChartView.refreshScheduleChartView();
                    }}");
                GanttChartView.DisplayedTimeChangeHandlerClientCode += string.Format(@"
                    if (typeof ganttChartView.refreshScheduleChartViewDisplayedTime !== 'undefined')
                        ganttChartView.refreshScheduleChartViewDisplayedTime(displayedTime);");
                GanttChartView.SplitterPositionChangeHandlerClientCode += string.Format(@"
                    if (typeof ganttChartView.refreshScheduleChartViewSplitterPosition !== 'undefined')
                        ganttChartView.refreshScheduleChartViewSplitterPosition(gridWidth, chartWidth);");
                GanttChartView.HourWidthChangeHandlerClientCode += string.Format(@"
                    ganttChartView.refreshScheduleChartView();");
            }
            if (LoadChartPanel.Visible)
            {
                LoadChartView.Items = GanttChartView.GetLoadChartItems();
                LoadChartView.CopyCommonSettings(GanttChartView);
                GanttChartView.InitializedClientCode += string.Format(@"
                    ganttChartView.refreshLoadChartResourceSelector = function() {{
                        var loadChartResourceFilter = document.querySelector('#loadChartResourceFilter'), i;
                        if (loadChartResourceFilter == null)
                            return;
                        var previouslySelectedResource = loadChartResourceFilter.value;
                        for (i = loadChartResourceFilter.childNodes.length; i-- > 2; )
                            loadChartResourceFilter.removeChild(loadChartResourceFilter.childNodes[i]);
                        var resources = ganttChartView.getAssignedResources();
                        for (i = 0; i < resources.length; i++) {{
                            var resource = resources[i];
                            var option = document.createElement('option');
                            option.appendChild(document.createTextNode(resource));
                            if (resource == previouslySelectedResource)
                                option.setAttribute('selected', 'true');
                            loadChartResourceFilter.appendChild(option);
                        }}
                    }}
                    setTimeout(ganttChartView.refreshLoadChartResourceSelector, 0);
                    ganttChartView.refreshLoadChartView = function() {{
                        if (!ganttChartView.isWaitingToRefreshLoadChartView) {{
                            ganttChartView.isWaitingToRefreshLoadChartView = true;
                            setTimeout(function() {{
                                var loadChartView = document.querySelector('#{0}');
                                if (loadChartView == null || typeof loadChartView.isLoadChartInitialized === 'undefined' || !loadChartView.isLoadChartInitialized)
                                    return;
                                var loadChartResourceFilter = document.querySelector('#loadChartResourceFilter');
                                if (loadChartResourceFilter == null)
                                    return;
                                var resourceFilterValue = loadChartResourceFilter.value;
                                if (resourceFilterValue == '') {{
                                    loadChartView.loadChartItems = ganttChartView.getLoadChartItems();
                                    loadChartView.settings.itemHeight = 21;
                                    loadChartView.settings.barHeight = 10.5;
                                }}
                                else {{
                                    loadChartView.loadChartItems = ganttChartView.getLoadChartItems([resourceFilterValue]);
                                    loadChartView.settings.itemHeight = 63;
                                    loadChartView.settings.barHeight = 52.5;
                                }}
                                ganttChartView.copyCommonSettings(loadChartView.settings);
                                loadChartView.refresh();
                                loadChartView.settings.displayedTimeChangeHandler = function(displayedTime) {{ ganttChartView.refreshDisplayedTime(displayedTime); if (typeof ganttChartView.refreshScheduleChartViewDisplayedTime !== 'undefined') ganttChartView.refreshScheduleChartViewDisplayedTime(displayedTime); }}
                                loadChartView.settings.splitterPositionChangeHandler = function(gridWidth, chartWidth) {{ ganttChartView.refreshSplitterPosition(gridWidth, chartWidth); if (typeof ganttChartView.refreshScheduleChartViewSplitterPosition !== 'undefined') ganttChartView.refreshScheduleChartViewSplitterPosition(gridWidth, chartWidth); }}
                                ganttChartView.isWaitingToRefreshLoadChartView = false;
                            }}, 0);
                        }}
                    }};
                    setTimeout(function() {{
                        var loadChartView = document.querySelector('#{0}');
                        if (loadChartView == null)
                            return;
                        ganttChartView.copyCommonSettings(loadChartView.settings);
                        loadChartView.settings.displayedTimeChangeHandler = function(displayedTime) {{ ganttChartView.refreshDisplayedTime(displayedTime); if (typeof ganttChartView.refreshLoadChartViewDisplayedTime !== 'undefined') ganttChartView.refreshLoadChartViewDisplayedTime(displayedTime); }}
                        loadChartView.settings.splitterPositionChangeHandler = function(gridWidth, chartWidth) {{ ganttChartView.refreshSplitterPosition(gridWidth, chartWidth); if (typeof ganttChartView.refreshScheduleChartViewSplitterPosition !== 'undefined') ganttChartView.refreshScheduleChartViewSplitterPosition(gridWidth, chartWidth); }}
                    }}, 0);
                    ganttChartView.refreshLoadChartViewDisplayedTime = function(displayedTime) {{
                        if (!ganttChartView.isWaitingToRefreshLoadChartViewDisplayedTime) {{
                            ganttChartView.isWaitingToRefreshLoadChartViewDisplayedTime = true;
                            setTimeout(function() {{
                                var loadChartView = document.querySelector('#{0}');
                                if (loadChartView == null || typeof loadChartView.isLoadChartInitialized === 'undefined' || !loadChartView.isLoadChartInitialized)
                                    return;
                                loadChartView.scrollToDateTime(displayedTime);
                                ganttChartView.isWaitingToRefreshLoadChartViewDisplayedTime = false;
                            }}, 0);
                        }}
                    }};
                    ganttChartView.refreshLoadChartViewSplitterPosition = function(gridWidth, chartWidth) {{
                        if (!ganttChartView.isWaitingToRefreshLoadChartViewSplitterPosition) {{
                            ganttChartView.isWaitingToRefreshLoadChartViewSplitterPosition = true;
                            setTimeout(function() {{
                                var loadChartView = document.querySelector('#{0}');
                                if (loadChartView == null || typeof loadChartView.isLoadChartInitialized === 'undefined' || !loadChartView.isLoadChartInitialized)
                                    return;
                                loadChartView.setSplitterPosition(gridWidth, chartWidth);
                                ganttChartView.isWaitingToRefreshLoadChartViewSplitterPosition = false;
                            }}, 0);
                        }}
                    }};",
                    LoadChartView.ClientID);
                GanttChartView.ItemPropertyChangeHandlerClientCode += string.Format(@"
                    if (isDirect && isFinal && ((!item.hasChildren && (propertyName == 'content' || propertyName == 'start' || propertyName == 'finish' || propertyName == 'isMilestone' || propertyName == 'assignmentsContent')) || propertyName == 'indentation')) {{
                        ganttChartView.refreshLoadChartResourceSelector();
                        ganttChartView.refreshLoadChartView();
                    }}");
                GanttChartView.DisplayedTimeChangeHandlerClientCode += string.Format(@"
                    if (typeof ganttChartView.refreshLoadChartViewDisplayedTime !== 'undefined')
                        ganttChartView.refreshLoadChartViewDisplayedTime(displayedTime);");
                GanttChartView.SplitterPositionChangeHandlerClientCode += string.Format(@"
                    if (typeof ganttChartView.refreshLoadChartViewSplitterPosition !== 'undefined')
                        ganttChartView.refreshLoadChartViewSplitterPosition(gridWidth, chartWidth);");
                GanttChartView.HourWidthChangeHandlerClientCode += string.Format(@"
                    ganttChartView.refreshLoadChartView();");
            }
            ScriptManager.RegisterClientScriptBlock(this, typeof(Index), "LoadChartResourceFilterChanged", string.Format(@"
                function loadChartResourceFilterChanged()
                {{
                    var ganttChartView = document.querySelector('#{0}');
                    if (typeof ganttChartView.refreshLoadChartView !== 'undefined')
                        ganttChartView.refreshLoadChartView();
                }}", GanttChartView.ClientID), true);
            if (PertChartPanel.Visible)
            {
                PertChartView.Items = GanttChartView.GetPertChartItems();
                foreach (DlhSoft.Web.UI.WebControls.Pert.PredecessorItem predecessorItem in PertChartView.GetCriticalDependencies())
                {
                    predecessorItem.Item.ShapeStroke = Color.Red;
                    predecessorItem.DependencyLineStroke = Color.Red;
                }
                var finish = PertChartView.GetFinish();
                finish.ShapeStroke = Color.Red;
                // Optionally, reposition start and finish milestones to get better diagram layout.
                // PertChartView.RepositionEnds();
                GanttChartView.InitializedClientCode += string.Format(@"
                    ganttChartView.hidePertChartView = function() {{
                        var pertChartPanel = document.querySelector('#{0}');
                        if (pertChartPanel == null)
                            return;
                        pertChartPanel.style.display = 'none';
                    }};",
                    PertChartPanel.ClientID);
                GanttChartView.ItemPropertyChangeHandlerClientCode += string.Format(@"
                    if (isDirect && isFinal && ((!item.hasChildren && (propertyName == 'content' || propertyName == 'start' || propertyName == 'finish' || propertyName == 'completedFinish' || propertyName == 'isMilestone' || propertyName == 'assignmentsContent')) || propertyName == 'indentation')) {{
                        ganttChartView.hidePertChartView();
                    }}");
            }
            if (NetworkDiagramPanel.Visible)
            {
                NetworkDiagramView.Items = GanttChartView.GetNetworkDiagramItems();
                foreach (DlhSoft.Web.UI.WebControls.Pert.NetworkDiagramItem item in NetworkDiagramView.GetCriticalItems())
                    item.ShapeStroke = Color.Red;
                // Optionally, reposition start and finish milestones to get better diagram layout.
                // NetworkDiagramView.RepositionEnds();
                GanttChartView.InitializedClientCode += string.Format(@"
                    ganttChartView.hideNetworkDiagramView = function() {{
                        var networkDiagramPanel = document.querySelector('#{0}');
                        if (networkDiagramPanel == null)
                            return;
                        networkDiagramPanel.style.display = 'none';
                    }};",
                    NetworkDiagramPanel.ClientID);
                GanttChartView.ItemPropertyChangeHandlerClientCode += string.Format(@"
                    if (isDirect && isFinal && ((!item.hasChildren && (propertyName == 'content' || propertyName == 'start' || propertyName == 'finish' || propertyName == 'completedFinish' || propertyName == 'isMilestone' || propertyName == 'assignmentsContent')) || propertyName == 'indentation')) {{
                        ganttChartView.hideNetworkDiagramView();
                    }}");
            }
            GanttChartView.InitializedClientCode += @"
                ganttChartView.refreshDisplayedTime = function(displayedTime) {{
                    if (!ganttChartView.isWaitingToRefreshDisplayedTime) {{
                        ganttChartView.isWaitingToRefreshDisplayedTime = true;
                        setTimeout(function() {{
                            ganttChartView.scrollToDateTime(displayedTime);
                            ganttChartView.isWaitingToRefreshDisplayedTime = false;
                        }}, 0);
                    }}
                }};
                ganttChartView.refreshSplitterPosition = function(gridWidth, chartWidth) {{
                    if (!ganttChartView.isWaitingToRefreshSplitterPosition) {{
                        ganttChartView.isWaitingToRefreshSplitterPosition = true;
                        setTimeout(function() {{
                            ganttChartView.setSplitterPosition(gridWidth, chartWidth);
                            ganttChartView.isWaitingToRefreshSplitterPosition = false;
                        }}, 0);
                    }}
                }};";
        }
    }
}