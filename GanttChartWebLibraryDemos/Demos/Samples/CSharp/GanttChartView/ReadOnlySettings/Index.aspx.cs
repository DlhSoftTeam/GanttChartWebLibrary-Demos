using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DlhSoft.Web.UI.WebControls;
using System.Drawing;
using DlhSoft.Windows.Data;

namespace Demos.Samples.CSharp.GanttChartView.ReadOnlySettings
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

                // Do not allow creating to finish dependencies.
                GanttChartView.AllowCreatingToFinishDependencies = false;

                GanttChartView.InitializingClientCode = @"
                    if (initializeGanttChartTheme)
                        initializeGanttChartTheme(control.settings, theme);
                    if (initializeGanttChartTemplates)
                        initializeGanttChartTemplates(control.settings, theme);";
            }
        }

        public void IsReadOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GanttChartView.IsReadOnly = IsReadOnlyCheckBox.Checked;
        }
        public void IsGridReadOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GanttChartView.IsGridReadOnly = IsGridReadOnlyCheckBox.Checked;
        }
        public void IsChartReadOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GanttChartView.IsChartReadOnly = IsChartReadOnlyCheckBox.Checked;
        }
        public void HideGridCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GanttChartView.IsGridVisible = !HideGridCheckBox.Checked;
            GanttChartView.GridWidth = GanttChartView.IsGridVisible ? new Unit(35, UnitType.Percentage) : new Unit(0, UnitType.Percentage);
            GanttChartView.ChartWidth = GanttChartView.IsGridVisible ? new Unit(65, UnitType.Percentage) : new Unit(100, UnitType.Percentage);
        }
        public void IsContentReadOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GanttChartView.IsContentReadOnly = IsContentReadOnlyCheckBox.Checked;
        }
        public void AreSchedulingColumnsReadOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GanttChartView.Columns[3].IsReadOnly = AreSchedulingColumnsReadOnlyCheckBox.Checked; // Start date column
            GanttChartView.Columns[4].IsReadOnly = AreSchedulingColumnsReadOnlyCheckBox.Checked; // Finish date column
            GanttChartView.Columns[5].IsReadOnly = AreSchedulingColumnsReadOnlyCheckBox.Checked; // Milestone column
            GanttChartView.Columns[6].IsReadOnly = AreSchedulingColumnsReadOnlyCheckBox.Checked; // Completed column
        }
        public void AreAssignmentsReadOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GanttChartView.IsAssignmentsContentReadOnly = AreAssignmentsReadOnlyCheckBox.Checked;
        }
        public void IsEffortPreservedWhenStartChangesInGridCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GanttChartView.IsTaskEffortPreservedWhenStartChangesInGrid = IsEffortPreservedWhenStartChangesInGridCheckBox.Checked;
        }
        public void IsStartReadOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GanttChartView.IsTaskStartReadOnly = IsStartReadOnlyCheckBox.Checked;
        }
        public void IsCompletionReadOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GanttChartView.IsTaskCompletionReadOnly = IsCompletionReadOnlyCheckBox.Checked;
        }
        public void IsEffortReadOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GanttChartView.IsTaskEffortReadOnly = IsEffortReadOnlyCheckBox.Checked;
        }
        public void DisableStartEndDraggingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //... GanttChartView.IsDraggingTaskStartEndsEnabled = !DisableStartEndDraggingCheckBox.Checked;
        }
        public void HideDependenciesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GanttChartView.AreTaskDependenciesVisible = !HideDependenciesCheckBox.Checked;
        }
        public void DisableCreatingStartDependenciesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GanttChartView.AllowCreatingStartDependencies = !DisableCreatingStartDependenciesCheckBox.Checked;
        }
        public void AreDependenciesReadOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GanttChartView.AreTaskPredecessorsReadOnly = AreDependenciesReadOnlyCheckBox.Checked;
        }
        public void DisableScrollingOnTaskClickCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GanttChartView.IsGridRowClickTimeScrollingEnabled = !DisableScrollingOnTaskClickCheckBox.Checked;
        }
        public void SelectedItemAsReadOnlyButton_Click(object sender, EventArgs e)
        {
            GanttChartView.SelectedItem.IsReadOnly = true;
        }
        public void SelectedItemBarAsReadOnlyButton_Click(object sender, EventArgs e)
        {
            GanttChartView.SelectedItem.IsBarReadOnly = true;
        }
    }
}
