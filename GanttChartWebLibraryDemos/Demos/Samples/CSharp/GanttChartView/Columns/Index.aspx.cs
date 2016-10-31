using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DlhSoft.Web.UI.WebControls;
using System.Drawing;
using DlhSoft.Windows.Data;

namespace Demos.Samples.CSharp.GanttChartView.Columns
{ 
    public partial class Index : System.Web.UI.Page
    {
        private static readonly DateTime date = DateTime.Today;
        private static readonly int year = date.Year, month = date.Month;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

                GanttChartView.DisplayedTime = new DateTime(year, month, 1);
                GanttChartView.CurrentTime = new DateTime(year, month, 2, 12, 0, 0);
                
                // Configure existing columns.
                GanttChartView.Columns[(int)ColumnType.Content].Header = "Work items";
                GanttChartView.Columns[(int)ColumnType.Content].Width = 240;
                GanttChartView.Columns[(int)ColumnType.Start].Header = "Beginning";
                GanttChartView.Columns[(int)ColumnType.Finish].Header = "End";
                GanttChartView.Columns[(int)ColumnType.Milestone].Header = "Is milestone";
                GanttChartView.Columns[(int)ColumnType.Completed].Header = "Is completed";
                GanttChartView.Columns[(int)ColumnType.Assignments].Header = "Workers";
                GanttChartView.Columns[(int)ColumnType.RowHeader].IsVisible = false;

                // Add supplemental columns. Note that columns obtained using get*Template client side method calls are using invariant culture, tough.
                GanttChartView.Columns.Insert(2, new Column { Header = string.Empty, Width = 40, CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getIndexColumnTemplate()(item);", AllowUserToResize = false });
                GanttChartView.Columns.Insert(5, new Column { Header = "Effort (h)", Width = 80, CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getTotalEffortColumnTemplate(64)(item);" });
                GanttChartView.Columns.Insert(6, new Column { Header = "Duration (d)", Width = 80, CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getDurationColumnTemplate(64, 8)(item);" });
                GanttChartView.Columns.Insert(10, new Column { Header = "%", Width = 80, CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getCompletionColumnTemplate(64)(item);" });
                GanttChartView.Columns.Insert(11, new Column { Header = "Predecessors", Width = 100, CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getPredecessorsColumnTemplate(84)(item);" });
                GanttChartView.Columns.Add(new Column { Header = "Cost ($)", Width = 100, CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getCostColumnTemplate(84)(item);" });
                GanttChartView.Columns.Add(new Column { Header = "Est. start", Width = 140, CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getBaselineStartColumnTemplate(124, true, true, 8 * 60 * 60 * 1000)(item);" }); // 8 AM
                GanttChartView.Columns.Add(new Column { Header = "Est. finish", Width = 140, CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getBaselineFinishColumnTemplate(124, true, true, 16 * 60 * 60 * 1000)(item);" }); // 4 PM

                // Initialize extra custom item values and associated columns.
                GanttChartView.Items[7].CustomValues["Property1"] = "A1";
                GanttChartView.Items[8].CustomValues["Property1"] = "B1";
                GanttChartView.Items[8].CustomValues["Property2"] = "B2";
                GanttChartView.Columns.Add(
                    new Column
                    {
                        Header = "My value 1", Width = 80,
                        CellTemplateClientCode = @"
                            var input = DlhSoft.Controls.GanttChartView.textInputColumnTemplateBase(document, 64, function () { return item.customProperty1Value; }, function (value) { item.customProperty1Value = value; });
                            input.setAttribute('Name', 'Item' + item.index + 'Property1'); // Ensure posting the (possibly updated) value back as a Request.Form field.
                            return input;"
                    });
                GanttChartView.Columns.Add(
                    new Column
                    {
                        Header = "My value 2", Width = 80,
                        CellTemplateClientCode = @"
                            var input = DlhSoft.Controls.GanttChartView.textInputColumnTemplateBase(document, 64, function () { return item.customProperty2Value; }, function (value) { item.customProperty2Value = value; });
                            input.setAttribute('Name', 'Item' + item.index + 'Property2'); // Ensure posting the (possibly updated) value back as a Request.Form field.
                            return input;"
                    });

                // Set custom item tag objects, properties, append read only custom columns bound to their values, and/or set up custom cell template code statements to be executed on the client side.
                GanttChartView.Items[7].Tag = 70;
                GanttChartView.Items[7].CustomValues["Description"] = "Custom description";
                GanttChartView.Columns.Add(new Column { Header = "Description", Width = 200, PropertyName = "Description" });
                GanttChartView.Columns.Add(new Column { Header = "Status", Width = 40, CellTemplateClientCode = "return control.ownerDocument.createTextNode(item.content == 'Task 2.1' ? '!' : '');" });
                GanttChartView.Columns[(int)ColumnType.Assignments].CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getAssignmentSelectorColumnTemplate(184, function (item) { return ['Resource 1', 'Resource 2']; })(item);"; // Resource assignment selector.

                GanttChartView.AreTaskDependencyConstraintsEnabled = true;

                // Optionally, initialize custom theme and templates (themes.js, templates.js).
                GanttChartView.InitializingClientCode = @"
                    if (initializeGanttChartTheme)
                        initializeGanttChartTheme(control.settings, theme);
                    if (initializeGanttChartTemplates)
                        initializeGanttChartTemplates(control.settings, theme);";
            }
            else
            {
                // Reinitialize custom item property values upon post backs, using the (possibly updated) Request.Form fields.
                for (var i = 0; i < GanttChartView.Items.Count; i++)
                {
                    var item = GanttChartView.Items[i];
                    item.CustomValues["Property1"] = Request.Form["Item" + i + "Property1"];
                    item.CustomValues["Property2"] = Request.Form["Item" + i + "Property2"];
                }
            }
        }

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
    }
}
