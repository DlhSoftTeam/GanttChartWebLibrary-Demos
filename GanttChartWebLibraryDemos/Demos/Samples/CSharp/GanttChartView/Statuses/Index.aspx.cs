﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DlhSoft.Web.UI.WebControls;
using System.Drawing;
using DlhSoft.Windows.Data;

namespace Demos.Samples.CSharp.GanttChartView.Statuses
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

                // Set up item statuses and bar colors.
                InitializeItemStatuses();

                // Display status values using a custom grid column.
                Column statusColumn = new Column
                {
                    ColumnType = ColumnType.Custom,
                    Header = "Status",
                    Width = 120,
                    PropertyName = "Status" // Use values from item.CustomValues["Status"]
                };
                GanttChartView.Columns.Insert(3, statusColumn);

                // Optionally, initialize custom theme and templates (themes.js, templates.js).
                GanttChartView.InitializingClientCode += @";
                if (initializeGanttChartTheme)
                    initializeGanttChartTheme(control.settings, theme);
                if (initializeGanttChartTemplates)
                    initializeGanttChartTemplates(control.settings, theme);";
            }
        }

        private void InitializeItemStatuses()
        {
            foreach (GanttChartItem item in GanttChartView.Items)
            {
                var status = GetStatus(item);
                item.CustomValues["Status"] = status;
                if (!string.IsNullOrEmpty(status))
                    item.BarFill = GetStatusColor(status);
            }
        }

        protected void RefreshStatusesButton_Click(object sender, EventArgs e)
        {
            // Update item statuses and bar colors upon request.
            InitializeItemStatuses();
        }

        protected void IncreaseCurrentTimeButton_Click(object sender, EventArgs e)
        {
            GanttChartView.CurrentTime += TimeSpan.FromDays(7); // 1 week
            // Also update item statuses and bar colors.
            InitializeItemStatuses();
        }

        private string GetStatus(GanttChartItem item)
        {
            if (GanttChartView.HasChildren(item) || item.IsMilestone)
                return string.Empty;
            var itemStart = GanttChartView.GetNextWorkingTime(item.Start);
            var itemFinish = GanttChartView.GetPreviousNonworkingTime(item.Finish);
            if (itemFinish < itemStart)
                return string.Empty;
            var itemCompletedFinish = item.CompletedFinish;
            if (itemCompletedFinish < itemStart)
                itemCompletedFinish = itemStart;
            if (itemCompletedFinish >= itemFinish)
                return "Completed";
            DateTime now = GanttChartView.CurrentTime;
            if (itemCompletedFinish < now)
                return "Behind schedule";
            if (itemCompletedFinish > itemStart)
                return "In progress";
            return "To do";
        }

        private static Color GetStatusColor(string status)
        {
            switch (status) {
                case "Completed":
                    return Color.Green;
                case "To do":
                    return Color.Gray;
                case "Behind schedule":
                    return Color.Red;
                case "In progress":
                    return Color.Orange;
                default:
                    return Color.Transparent;
            }
        }
    }
}
