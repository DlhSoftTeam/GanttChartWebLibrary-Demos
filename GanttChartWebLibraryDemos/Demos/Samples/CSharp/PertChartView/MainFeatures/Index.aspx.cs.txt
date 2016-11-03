using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using DlhSoft.Windows.Data;
using DlhSoft.Web.UI.WebControls.Pert;

namespace Demos.Samples.CSharp.PertChartView.MainFeatures
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
                PertChartView.InitializingContent = "...";

                // Prepare data items.
                var items = new List<PertChartItem>
                {
                    new PertChartItem { DisplayedText = "0", Content = "Start" },
                    new PertChartItem { DisplayedText = "1", Content = "Task event 1" },
                    new PertChartItem { DisplayedText = "2", Content = "Task event 2" },
                    new PertChartItem { DisplayedText = "3", Content = "Task event 3" },
                    new PertChartItem { DisplayedText = "4", Content = "Finish", DisplayedRowIndex = 0 }
                };
                items[1].Predecessors.Add(new PredecessorItem { Item = items[0], DisplayedText = "A", Content = "Task A", Effort = TimeSpan.FromHours(4) });
                items[2].Predecessors.Add(new PredecessorItem { Item = items[0], DisplayedText = "B", Content = "Task B", Effort = TimeSpan.FromHours(2) });
                items[3].Predecessors.Add(new PredecessorItem { Item = items[2], DisplayedText = "C", Content = "Task C", Effort = TimeSpan.FromHours(1) });
                items[4].Predecessors.Add(new PredecessorItem { Item = items[1], DisplayedText = "D", Content = "Task D", Effort = TimeSpan.FromHours(5) });
                items[4].Predecessors.Add(new PredecessorItem { Item = items[2], DisplayedText = "E", Content = "Task E", Effort = TimeSpan.FromHours(3) });
                items[4].Predecessors.Add(new PredecessorItem { Item = items[3], DisplayedText = "F", Content = "Task F", Effort = TimeSpan.FromHours(2) });
                PertChartView.Items = items;

                // Optionally, clear user rearranging settings.
                // PertChartView.CanUserRearrangeItems = false;
                // PertChartView.SnapRearrangedItemsToGuidelines = false;

                // Optionally, set theme and/or custom styles.
                // PertChartView.Theme = PresentationTheme.Aero;
                // PertChartView.BorderColor = Color.Gray;
                // PertChartView.ShapeStroke = Color.Green;
                // PertChartView.DependencyLineStroke = Color.Green;

                // Optionally, set custom item tag objects and properties.
                // PertChartView.Items[1].Tag = 70;
                // PertChartView.Items[1].CustomValues["Description"] = "Custom description";

                // Optionally, specify the application target in order for the component to adapt to the screen size.
                // PertChartView.Target = PresentationTarget.Phone;

                // Optionally, set up custom initialization.
                // PertChartView.InitializingClientCode = "if (typeof console !== 'undefined') console.log('The component is about to be initialized.')";
                // PertChartView.InitializedClientCode = "if (typeof console !== 'undefined') console.log('The component has been successfully initialized.');";

                // Optionally, initialize custom theme and templates (themes.js, templates.js).
                PertChartView.InitializingClientCode += @";
                    initializePertChartTheme(control.settings, theme);
                    initializePertChartTemplates(control.settings, theme);";
            }
        }

        // Define user command methods.
        public void SetCustomShapeColorToItemButton_Click(object sender, EventArgs e)
        {
            var item = PertChartView.Items[2];
            item.ShapeStroke = Color.DarkMagenta;
            item.ShapeFill = Color.LightYellow;
        }
        public void SetCustomDependencyLineColorToPredecessorItemButton_Click(object sender, EventArgs e)
        {
            var item = PertChartView.Items[2];
            var predecessorItem = item.Predecessors[0];
            predecessorItem.DependencyLineStroke = Color.DarkMagenta;
        }
        public void HighlightCriticalPathButton_Click(object sender, EventArgs e)
        {
            // Reset the view.
            foreach (PertChartItem item in PertChartView.Items)
            {
                item.ShapeStroke = null;
                foreach (PredecessorItem predecessorItem in item.Predecessors)
                    predecessorItem.DependencyLineStroke = null;
            }
            // Set up red as shape stroke properties for the critical items.
            foreach (PertChartItem item in PertChartView.GetCriticalItems())
                item.ShapeStroke = Color.Red;
            foreach (PredecessorItem predecessorItem in PertChartView.GetCriticalDependencies())
                predecessorItem.DependencyLineStroke = Color.Red;
        }
        public void PrintButton_Click(object sender, EventArgs e)
        {
            PertChartView.Print(title: "PERT Chart (printable)", preparingMessage: "...");
        }
    }
}
