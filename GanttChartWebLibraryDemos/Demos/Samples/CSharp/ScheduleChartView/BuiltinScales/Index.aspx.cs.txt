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

namespace Demos.Samples.CSharp.ScheduleChartView.BuiltinScales
{ 
    public partial class Index : System.Web.UI.Page
    {
        private static readonly DateTime date = DateTime.Today;
        private static readonly int year = date.Year, month = date.Month;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

                ScheduleChartView.DisplayedTime = new DateTime(year, month, 1);
                ScheduleChartView.CurrentTime = new DateTime(year, month, 2, 12, 0, 0);

                // Optionally, initialize custom theme and templates (themes.js, templates.js).
                ScheduleChartView.InitializingClientCode = @"
                    initializeScheduleChartTheme(control.settings, theme);
                    initializeScheduleChartTemplates(control.settings, theme);";
            }
        }

        public void MajorScaleTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScheduleChartView.Scales[0].ScaleType = (ScaleType)Enum.Parse(typeof(ScaleType), MajorScaleTypeDropDownList.SelectedValue);
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
            ScheduleChartView.Scales[0].HeaderTextFormat = (ScaleHeaderTextFormat)Enum.Parse(typeof(ScaleHeaderTextFormat), MajorScaleHeaderFormatDropDownList.SelectedValue);
        }
        public void MinorScaleTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMinorScaleType();
        }
        private void UpdateMinorScaleType()
        {
            ScheduleChartView.Scales[1].ScaleType = (ScaleType)Enum.Parse(typeof(ScaleType), MinorScaleTypeDropDownList.SelectedValue);
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
            ScheduleChartView.Scales[1].HeaderTextFormat = (ScaleHeaderTextFormat)Enum.Parse(typeof(ScaleHeaderTextFormat), MinorScaleHeaderFormatDropDownList.SelectedValue);
        }
        public void MinorScaleSeparatorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ScheduleChartView.Scales[1].IsSeparatorVisible = MinorScaleSeparatorCheckBox.Checked;
        }
        public void NonworkingDaysCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ScheduleChartView.IsNonworkingTimeHighlighted = NonworkingDaysCheckBox.Checked;
        }
        public void CurrentTimeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ScheduleChartView.IsCurrentTimeLineVisible = CurrentTimeCheckBox.Checked;
        }
        public void ZoomLevelTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateZoomLevel();
        }
        private void UpdateZoomLevel()
        {
            double hourWidth;
            if (double.TryParse(ZoomLevelTextBox.Text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, CultureInfo.InvariantCulture, out hourWidth) && hourWidth > 0)
                ScheduleChartView.HourWidth = hourWidth;
        }
        public void UpdateScaleDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (UpdateScaleDropDownList.SelectedValue)
            {
                case "Free":
                    ScheduleChartView.UpdateScaleInterval = TimeSpan.FromTicks(1);
                    return;
                case "15 min":
                    ScheduleChartView.UpdateScaleInterval = TimeSpan.FromMinutes(15);
                    return;
                case "Hour":
                    ScheduleChartView.UpdateScaleInterval = TimeSpan.FromHours(1);
                    return;
                case "Day":
                    ScheduleChartView.UpdateScaleInterval = TimeSpan.FromDays(1);
                    return;
            }
        }
    }
}
