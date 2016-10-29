using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DlhSoft.Web.UI.WebControls;
using System.Drawing;
using DlhSoft.Windows.Data;

namespace Demos.Samples.CSharp.GanttChartView.AssigningResources
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

                // Set the displayed and current time values to automatically scroll to a specific chart coordinate, and display a vertical bar highlighter at the specified point.
                GanttChartView.DisplayedTime = new DateTime(year, month, 1);
                GanttChartView.CurrentTime = new DateTime(year, month, 2, 12, 0, 0);

                //Optionally, set some columns invisible.
                GanttChartView.Columns[3].IsVisible = false; // Start column
                GanttChartView.Columns[4].IsVisible = false; // Finish column
                GanttChartView.Columns[5].IsVisible = false; // Milestone column
                GanttChartView.Columns[6].IsVisible = false; // Completed column

                // Optionally, add supplemental columns. Note that columns obtained using get*Template client side method calls are using invariant culture, tough.
                GanttChartView.Columns.Insert(2, new Column { Header = string.Empty, Width = 40, CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getIndexColumnTemplate()(item);", AllowUserToResize = false });
                GanttChartView.Columns.Add(new Column { Header = "Cost ($)", Width = 100, CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getCostColumnTemplate(84)(item);" });

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
                GanttChartView.TaskInitiationCost = 5;
                items[4].ExecutionCost = 50;
                GanttChartView.DefaultResourceUsageCost = 1;
                GanttChartView.SpecificResourceUsageCosts = new Dictionary<string, double> { { "Resource 1", 2 }, { "Material 1", 7 } };
                GanttChartView.DefaultResourceHourCost = 10;
                GanttChartView.SpecificResourceHourCosts = new Dictionary<string, double> { { "Resource 1", 20 }, { "Material 2", 0.5 } };

                // Optionally, set up auto-scheduling behavior for dependent tasks based on predecessor information, supplementary disallowing circular dependencies.
                GanttChartView.AreTaskDependencyConstraintsEnabled = true;
            }

            // Optionally, initialize custom theme and templates (themes.js, templates.js).
            Func<string, string> initializingClientCodeGetter = (string type) => @";
                if (initialize" + type + @"Theme)
                    initialize" + type + @"Theme(control.settings, theme);" +
                (type == "GanttChart" ? @"
                if (initialize" + type + @"Templates)
                    initialize" + type + @"Templates(control.settings, theme);" : string.Empty);
            if (!IsPostBack)
                GanttChartView.InitializingClientCode += initializingClientCodeGetter("GanttChart");
            LoadChartView.InitializingClientCode = initializingClientCodeGetter("LoadChart");
        }

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
        public void LoadChartButton_Click(object sender, EventArgs e)
        {
            LoadChartPanel.Visible = true;
        }
        public void CloseLoadChartViewButton_Click(object sender, EventArgs e)
        {
            LoadChartPanel.Visible = false;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Optionally, synchronize other displayed views at initialization time and upon standard Gantt Chart item changes on the client side.
            var initalizationCode = string.Format(@"var ganttChartView = document.querySelector('#{0}');", GanttChartView.ClientID);
            GanttChartView.InitializedClientCode += initalizationCode;
            GanttChartView.ItemPropertyChangeHandlerClientCode += initalizationCode;
            GanttChartView.DisplayedTimeChangeHandlerClientCode += initalizationCode;
            GanttChartView.SplitterPositionChangeHandlerClientCode += initalizationCode;
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