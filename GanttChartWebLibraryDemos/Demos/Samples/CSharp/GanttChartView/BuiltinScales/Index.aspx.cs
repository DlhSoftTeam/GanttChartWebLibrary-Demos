using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DlhSoft.Web.UI.WebControls;
using System.Drawing;
using DlhSoft.Windows.Data;
using System.Globalization;

namespace Demos.Samples.CSharp.GanttChartView.BuiltinScales
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

                // Always display separators between intervals defined at the major scale level.
                GanttChartView.Scales[0].IsSeparatorVisible = true;

                // Optionally, initialize custom themes (themes.js).
                GanttChartView.InitializingClientCode = @"initializeGanttChartTheme(control.settings, theme);";
            }
        }

        public void MajorScaleTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GanttChartView.Scales[0].ScaleType = (ScaleType)Enum.Parse(typeof(ScaleType), MajorScaleTypeDropDownList.SelectedValue);
            switch (MajorScaleTypeDropDownList.SelectedValue)
            {
                case "Years":
                    MajorScaleHeaderFormatDropDownList.SelectedIndex = 8;
                    break;
                case "Months":
                    MajorScaleHeaderFormatDropDownList.SelectedIndex = 7;
                    break;
                case "Weeks":
                    MajorScaleHeaderFormatDropDownList.SelectedIndex = 5;
                    break;
                case "Days":
                    MajorScaleHeaderFormatDropDownList.SelectedIndex = 4;
                    break;
                case "Hours":
                    MajorScaleHeaderFormatDropDownList.SelectedIndex = 2;
                    break;
            }
            UpdateMajorScaleHeaderFormat();
            MinorScaleTypeDropDownList.SelectedIndex = MajorScaleTypeDropDownList.SelectedIndex < MinorScaleTypeDropDownList.Items.Count - 1 ? MajorScaleTypeDropDownList.SelectedIndex + 1 : MajorScaleTypeDropDownList.SelectedIndex - 1;
            UpdateMinorScaleType();
        }
        public void MajorScaleHeaderFormatDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMajorScaleHeaderFormat();
        }
        private void UpdateMajorScaleHeaderFormat()
        {
            GanttChartView.Scales[0].HeaderTextFormat = (ScaleHeaderTextFormat)Enum.Parse(typeof(ScaleHeaderTextFormat), MajorScaleHeaderFormatDropDownList.SelectedValue);
        }
        public void MinorScaleTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMinorScaleType();
        }
        private void UpdateMinorScaleType()
        {
            GanttChartView.Scales[1].ScaleType = (ScaleType)Enum.Parse(typeof(ScaleType), MinorScaleTypeDropDownList.SelectedValue);
            switch (MinorScaleTypeDropDownList.SelectedValue)
            {
                case "Years":
                    MinorScaleHeaderFormatDropDownList.SelectedIndex = 8;
                    ZoomLevelTextBox.Text = "0.5";
                    break;
                case "Months":
                    MinorScaleHeaderFormatDropDownList.SelectedIndex = 7;
                    ZoomLevelTextBox.Text = "0.5";
                    break;
                case "Weeks":
                    MinorScaleHeaderFormatDropDownList.SelectedIndex = 5;
                    ZoomLevelTextBox.Text = "2";
                    break;
                case "Days":
                    MinorScaleHeaderFormatDropDownList.SelectedIndex = 4;
                    ZoomLevelTextBox.Text = "5";
                    break;
                case "Hours":
                    MinorScaleHeaderFormatDropDownList.SelectedIndex = 2;
                    ZoomLevelTextBox.Text = "25";
                    break;
            }
            UpdateMinorScaleHeaderFormat();
            UpdateZoomLevel();
        }
        public void MinorScaleHeaderFormatDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMinorScaleHeaderFormat();
        }
        private void UpdateMinorScaleHeaderFormat()
        {
            GanttChartView.Scales[1].HeaderTextFormat = (ScaleHeaderTextFormat)Enum.Parse(typeof(ScaleHeaderTextFormat), MinorScaleHeaderFormatDropDownList.SelectedValue);
        }
        public void MinorScaleSeparatorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GanttChartView.Scales[1].IsSeparatorVisible = MinorScaleSeparatorCheckBox.Checked;
        }
        public void NonworkingDaysCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GanttChartView.IsNonworkingTimeHighlighted = NonworkingDaysCheckBox.Checked;
        }
        public void CurrentTimeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GanttChartView.IsCurrentTimeLineVisible = CurrentTimeCheckBox.Checked;
        }
        public void ZoomLevelTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateZoomLevel();
        }
        private void UpdateZoomLevel()
        {
            double hourWidth;
            if (double.TryParse(ZoomLevelTextBox.Text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, CultureInfo.InvariantCulture, out hourWidth) && hourWidth > 0)
                GanttChartView.HourWidth = hourWidth;
        }
        public void UpdateScaleDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (UpdateScaleDropDownList.SelectedValue)
            {
                case "Free":
                    GanttChartView.UpdateScaleInterval = TimeSpan.FromTicks(1);
                    return;
                case "15 min":
                    GanttChartView.UpdateScaleInterval = TimeSpan.FromMinutes(15);
                    return;
                case "Hour":
                    GanttChartView.UpdateScaleInterval = TimeSpan.FromHours(1);
                    return;
                case "Day":
                    GanttChartView.UpdateScaleInterval = TimeSpan.FromDays(1);
                    return;
            }
        }
    }
}
