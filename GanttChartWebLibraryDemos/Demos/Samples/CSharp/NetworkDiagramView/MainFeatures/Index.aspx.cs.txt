using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using DlhSoft.Windows.Data;
using DlhSoft.Web.UI.WebControls.Pert;

namespace Demos.Samples.CSharp.NetworkDiagramView.MainFeatures
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
                NetworkDiagramView.InitializingContent = "...";

                // Prepare data items.
                var items = new List<NetworkDiagramItem>
                {
                    new NetworkDiagramItem { Content = "Start milestone", DisplayedText = "Start", IsMilestone = true, EarlyStart = new DateTime(year, month, 2, 8, 0, 0), EarlyFinish = new DateTime(year, month, 2, 8, 0, 0), LateStart = new DateTime(year, month, 2, 8, 0, 0), LateFinish = new DateTime(year, month, 2, 8, 0, 0) },
                    new NetworkDiagramItem { Content = "First task", DisplayedText = "Task 1", Effort = TimeSpan.FromHours(8), EarlyStart = new DateTime(year, month, 2, 8, 0, 0), EarlyFinish = new DateTime(year, month, 2, 16, 0, 0), LateStart = new DateTime(year, month, 2, 8, 0, 0), LateFinish = new DateTime(year, month, 2, 8, 0, 0), Slack = TimeSpan.Zero },
                    new NetworkDiagramItem { Content = "Second task", DisplayedText = "Task 2", Effort = TimeSpan.FromHours(4), EarlyStart = new DateTime(year, month, 2, 8, 0, 0), EarlyFinish = new DateTime(year, month, 2, 12, 0, 0), LateStart = new DateTime(year, month, 2, 12, 0, 0), LateFinish = new DateTime(year, month, 2, 8, 0, 0), Slack = TimeSpan.FromHours(4) },
                    new NetworkDiagramItem { Content = "Third task", DisplayedText = "Task 3", Effort = TimeSpan.FromHours(16), EarlyStart = new DateTime(year, month, 3, 8, 0, 0), EarlyFinish = new DateTime(year, month, 4, 16, 0, 0), LateStart = new DateTime(year, month, 3, 8, 0, 0), LateFinish = new DateTime(year, month, 4, 16, 0, 0), Slack = TimeSpan.Zero },
                    new NetworkDiagramItem { Content = "Fourth task", DisplayedText = "Task 4", Effort = TimeSpan.FromHours(4), EarlyStart = new DateTime(year, month, 3, 8, 0, 0), EarlyFinish = new DateTime(year, month, 3, 12, 0, 0), LateStart = new DateTime(year, month, 4, 12, 0, 0), LateFinish = new DateTime(year, month, 4, 16, 0, 0), Slack = TimeSpan.FromHours(12) },
                    new NetworkDiagramItem { Content = "Fifth task (middle milestone)", DisplayedText = "Task 5", IsMilestone = true, Effort = TimeSpan.FromHours(12), EarlyStart = new DateTime(year, month, 5, 8, 0, 0), EarlyFinish = new DateTime(year, month, 6, 12, 0, 0), LateStart = new DateTime(year, month, 5, 8, 0, 0), LateFinish = new DateTime(year, month, 6, 12, 0, 0), Slack = TimeSpan.Zero },
                    new NetworkDiagramItem { Content = "Sixth task", DisplayedText = "Task 6", Effort = TimeSpan.FromHours(48), EarlyStart = new DateTime(year, month, 6, 12, 0, 0), EarlyFinish = new DateTime(year, month, 12, 12, 0, 0), LateStart = new DateTime(year, month, 6, 12, 0, 0), LateFinish = new DateTime(year, month, 12, 12, 0, 0), Slack = TimeSpan.Zero },
                    new NetworkDiagramItem { Content = "Seventh task", DisplayedText = "Task 7", Effort = TimeSpan.FromHours(20), EarlyStart = new DateTime(year, month, 6, 12, 0, 0), EarlyFinish = new DateTime(year, month, 8, 16, 0, 0), LateStart = new DateTime(year, month, 10, 8, 0, 0), LateFinish = new DateTime(year, month, 12, 12, 0, 0), Slack = TimeSpan.FromHours(28) },
                    new NetworkDiagramItem { Content = "Finish milestone", DisplayedText = "Finish", IsMilestone = true, EarlyStart = new DateTime(year, month, 12, 12, 0, 0), EarlyFinish = new DateTime(year, month, 12, 12, 0, 0), LateStart = new DateTime(year, month, 12, 12, 0, 0), LateFinish = new DateTime(year, month, 12, 12, 0, 0) }
                };
                items[1].Predecessors.Add(new NetworkPredecessorItem { Item = items[0] });
                items[2].Predecessors.Add(new NetworkPredecessorItem { Item = items[0] });
                items[3].Predecessors.Add(new NetworkPredecessorItem { Item = items[1] });
                items[3].Predecessors.Add(new NetworkPredecessorItem { Item = items[2] });
                items[4].Predecessors.Add(new NetworkPredecessorItem { Item = items[1] });
                items[5].Predecessors.Add(new NetworkPredecessorItem { Item = items[3] });
                items[5].Predecessors.Add(new NetworkPredecessorItem { Item = items[4] });
                items[6].Predecessors.Add(new NetworkPredecessorItem { Item = items[5] });
                items[7].Predecessors.Add(new NetworkPredecessorItem { Item = items[5] });
                items[8].Predecessors.Add(new NetworkPredecessorItem { Item = items[6] });
                items[8].Predecessors.Add(new NetworkPredecessorItem { Item = items[7] });
                NetworkDiagramView.Items = items;

                // Optionally, clear user rearranging settings.
                // NetworkDiagramView.CanUserRearrangeItems = false;
                // NetworkDiagramView.SnapRearrangedItemsToGuidelines = false;

                // Optionally, set theme and/or custom styles.
                // NetworkDiagramView.Theme = PresentationTheme.Aero;
                // NetworkDiagramView.BorderColor = Color.Gray;
                // NetworkDiagramView.ShapeStroke = Color.Green;
                // NetworkDiagramView.MilestoneStroke = Color.DarkGreen;
                // NetworkDiagramView.DependencyLineStroke = Color.Green;

                // Optionally, set custom item tag objects and properties.
                // NetworkDiagramView.Items[1].Tag = 70;
                // NetworkDiagramView.Items[1].CustomValues["Description"] = "Custom description";

                // Optionally, specify the application target in order for the component to adapt to the screen size.
                // NetworkDiagramView.Target = PresentationTarget.Phone;

                // Optionally, set up custom initialization.
                // NetworkDiagramView.InitializingClientCode = "if (typeof console !== 'undefined') console.log('The component is about to be initialized.')";
                // NetworkDiagramView.InitializedClientCode = "if (typeof console !== 'undefined') console.log('The component has been successfully initialized.');";

                // Optionally, initialize custom theme and templates (themes.js, templates.js).
                NetworkDiagramView.InitializingClientCode += @";
                    initializePertChartTheme(control.settings, theme);
                    initializePertChartTemplates(control.settings, theme);";
            }
        }

        // Define user command methods.
        public void SetCustomShapeColorToItem2Button_Click(object sender, EventArgs e)
        {
            var item = NetworkDiagramView.Items[2];
            item.ShapeStroke = Color.DarkMagenta;
            item.ShapeFill = Color.LightYellow;
        }
        public void HighlightCriticalPathButton_Click(object sender, EventArgs e)
        {
            // Reset the view.
            foreach (NetworkDiagramItem item in NetworkDiagramView.Items)
                item.ShapeStroke = null;
            // Set up red as shape stroke properties for the critical items.
            foreach (NetworkDiagramItem item in NetworkDiagramView.GetCriticalItems())
                item.ShapeStroke = Color.Red;
        }
        public void PrintButton_Click(object sender, EventArgs e)
        {
            NetworkDiagramView.Print(title: "Network Diagram (printable)", preparingMessage: "...");
        }
    }
}
