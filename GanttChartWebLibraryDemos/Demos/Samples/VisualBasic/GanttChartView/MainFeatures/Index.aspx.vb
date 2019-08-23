Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DlhSoft.Web.UI.WebControls
Imports DlhSoft.Windows.Data
Imports System.Drawing

Namespace Demos.Samples.VisualBasic.GanttChartView.MainFeatures

    Partial Public Class Index
        Inherits System.Web.UI.Page

        Private Shared ReadOnly [date] As Date = Date.Today
        Private Shared ReadOnly year As Integer = [date].Year, month As Integer = [date].Month

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            If Not IsPostBack Then
                ' Optionally, set up client side HTML content to be displayed while initializing the component.
                GanttChartView.InitializingContent = "..."

                ' Prepare data items.
                Dim items = New List(Of GanttChartItem) From {
                    New GanttChartItem With {.Content = "Task 1", .IsExpanded = False},
                    New GanttChartItem With {.Content = "Task 1.1", .Indentation = 1, .Start = New Date(year, month, 2, 8, 0, 0), .Finish = New Date(year, month, 4, 16, 0, 0)},
                    New GanttChartItem With {.Content = "Task 1.2", .Indentation = 1, .Start = New Date(year, month, 3, 8, 0, 0), .Finish = New Date(year, month, 5, 12, 0, 0)},
                    New GanttChartItem With {.Content = "Task 2", .IsExpanded = True},
                    New GanttChartItem With {.Content = "Task 2.1", .Indentation = 1, .Start = New Date(year, month, 2, 8, 0, 0), .Finish = New Date(year, month, 8, 16, 0, 0), .CompletedFinish = New Date(year, month, 5, 16, 0, 0), .AssignmentsContent = "Resource 1, Resource 2 [50%]"},
                    New GanttChartItem With {.Content = "Task 2.2", .Indentation = 1},
                    New GanttChartItem With {.Content = "Task 2.2.1", .Indentation = 2, .Start = New Date(year, month, 11, 8, 0, 0), .Finish = New Date(year, month, 14, 16, 0, 0), .CompletedFinish = New Date(year, month, 14, 16, 0, 0), .AssignmentsContent = "Resource 2"},
                    New GanttChartItem With {.Content = "Task 2.2.2", .Indentation = 2, .Start = New Date(year, month, 12, 12, 0, 0), .Finish = New Date(year, month, 14, 16, 0, 0), .AssignmentsContent = "Resource 2"},
                    New GanttChartItem With {.Content = "Task 3", .Indentation = 1, .Start = New Date(year, month, 15, 16, 0, 0), .IsMilestone = True}
                }
                items(3).Predecessors = New List(Of PredecessorItem) From {New PredecessorItem With {.Item = items(0), .DependencyType = DependencyType.StartStart}}
                items(7).Predecessors = New List(Of PredecessorItem) From {New PredecessorItem With {.Item = items(6), .Lag = TimeSpan.FromHours(2)}}
                items(8).Predecessors = New List(Of PredecessorItem) From {
                    New PredecessorItem With {.Item = items(4)},
                    New PredecessorItem With {.Item = items(5)}
                }
                For i As Integer = 4 To 16
                    items.Add(New GanttChartItem With {.Content = "Task " & i, .Indentation = IIf(i >= 8 And i Mod 3 = 2, 0, 1), .Start = New Date(year, month, 2 + IIf(i <= 8, (i - 4) * 3, i - 8), 8, 0, 0), .Finish = New Date(year, month, 2 + IIf(i <= 8, (i - 4) * 3 + IIf(i > 8, 6, 1), i - 2), 16, 0, 0)})
                Next i
                items(9).Finish = items(9).Finish + TimeSpan.FromDays(2)
                items(9).AssignmentsContent = "Resource 1"
                items(10).Predecessors = New List(Of PredecessorItem) From {New PredecessorItem With {.Item = items(9)}}
                GanttChartView.Items = items

                ' Optionally, set baseline properties.
                GanttChartView.Items(6).BaselineStart = New Date(year, month, 10, 8, 0, 0)
                GanttChartView.Items(6).BaselineFinish = New Date(year, month, 11, 16, 0, 0)
                GanttChartView.Items(7).BaselineStart = New Date(year, month, 8, 8, 0, 0)
                GanttChartView.Items(7).BaselineFinish = New Date(year, month, 11, 16, 0, 0)
                GanttChartView.Items(8).BaselineStart = New Date(year, month, 12, 8, 0, 0)

                ' Optionally, define and assign default and specific task item schedules (working week and day intervals, and nonworking days).
                ' GanttChartView.Schedule = New Schedule With
                '     {
                '         .WorkingWeekStart = DayOfWeek.Monday,
                '         .WorkingWeekFinish = DayOfWeek.Friday,
                '         .WorkingDayStart = TimeOfDay.Parse("08:00:00"),
                '         .WorkingDayFinish = TimeOfDay.Parse("16:00:00")
                '     }
                ' ' GanttChartView.Schedule.SpecialNonworkingDays = New List(Of DlhSoft.Windows.Data.Date) From {New Date(year, month, 18), New Date(year, month, 21), New Date(year, month, 22)}
                ' Dim specialSchedule As New Schedule With
                '     {
                '         .WorkingWeekStart = DayOfWeek.Sunday,
                '         .WorkingWeekFinish = DayOfWeek.Wednesday,
                '         .WorkingDayStart = TimeOfDay.Parse("09:00:00"),
                '         .WorkingDayFinish = TimeOfDay.Parse("18:00:00")
                '     }
                ' ' specialSchedule.SpecialNonworkingDays = New List(Of DlhSoft.Windows.Data.Date) From {New Date(year, month, 18), New Date(year, month, 21), New Date(year, month, 22)}
                ' GanttChartView.Items(9).Schedule = specialSchedule
                ' GanttChartView.Items(10).Schedule = specialSchedule
                ' GanttChartView.IsIndividualItemNonworkingTimeHighlighted = True

                ' Optionally, preseve task effort when start value changes in the grid.
                ' GanttChartView.IsTaskEffortPreservedWhenStartChangesInGrid = True

                ' Optionally, hide data grid or set grid and chart widths, set read only settings, and/or disable virtualization.
                ' GanttChartView.IsGridVisible = False
                ' GanttChartView.GridWidth = New Unit("30%")
                ' GanttChartView.ChartWidth = New Unit("70%")
                ' GanttChartView.IsGridReadOnly = True
                ' GanttChartView.IsChartReadOnly = True
                ' GanttChartView.IsVirtualizing = False

                ' Optionally, set the scrollable timeline to present.
                ' GanttChartView.TimelineStart = New DateTime(year, month, 1)
                ' GanttChartView.TimelineFinish = New DateTime(year + 2, month, 1)

                ' Set the displayed and current time values to automatically scroll to a specific chart coordinate, and display a vertical bar highlighter at the specified point.
                GanttChartView.DisplayedTime = New Date(year, month, 1)
                GanttChartView.CurrentTime = New Date(year, month, 2, 12, 0, 0)

                ' Optionally, set theme and/or custom styles.
                ' GanttChartView.Theme = DlhSoft.Web.UI.WebControls.PresentationTheme.Aero
                ' GanttChartView.BorderColor = Color.Gray
                ' GanttChartView.GridLinesColor = Color.LightGray
                ' GanttChartView.StandardBarStroke = Color.Green
                ' GanttChartView.StandardBarFill = Color.LightGreen
                ' GanttChartView.StandardCompletedBarFill = Color.DarkGreen
                ' GanttChartView.StandardCompletedBarStroke = Color.DarkGreen
                ' GanttChartView.DependencyLineStroke = Color.Green

                ' Optionally, display alternative row background.
                ' GanttChartView.AlternativeRowBackColor = Color.FromArgb(&HF9, &HF9, &HF9)

                ' Optionally, configure selection.
                ' GanttChartView.SelectionMode = SelectionMode.Extended
                ' GanttChartView.SelectedItemBackColor = Color.LightCyan

                ' Optionally, initialize item selection.
                ' GanttChartView.Items(6).IsSelected = True

                ' Optionally, configure existing columns.
                ' GanttChartView.Columns(CInt(ColumnType.Content)).Header = "Work items"
                ' GanttChartView.Columns(CInt(ColumnType.Content)).Width = 240
                ' GanttChartView.Columns(CInt(ColumnType.Start)).Header = "Beginning"
                ' GanttChartView.Columns(CInt(ColumnType.Finish)).Header = "End"
                ' GanttChartView.Columns(CInt(ColumnType.Milestone)).Header = "Is milestone"
                ' GanttChartView.Columns(CInt(ColumnType.Completed)).Header = "Is completed"
                ' GanttChartView.Columns(CInt(ColumnType.Assignments)).Header = "Workers"
                ' GanttChartView.Columns(CInt(ColumnType.RowHeader)).IsVisible = False

                ' Optionally, add supplemental columns. Note that columns obtained using get*Template client side method calls are using invariant culture, tough.
                GanttChartView.Columns.Insert(2, New Column With {.Header = String.Empty, .Width = 40, .CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getIndexColumnTemplate()(item);", .AllowUserToResize = False})
                GanttChartView.Columns.Insert(5, New Column With {.Header = "Effort (h)", .Width = 80, .CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getTotalEffortColumnTemplate(64)(item);"})
                GanttChartView.Columns.Insert(6, New Column With {.Header = "Duration (d)", .Width = 80, .CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getDurationColumnTemplate(64, 8)(item);"})
                GanttChartView.Columns.Insert(10, New Column With {.Header = "%", .Width = 80, .CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getCompletionColumnTemplate(64)(item);"})
                GanttChartView.Columns.Insert(11, New Column With {.Header = "Predecessors", .Width = 100, .CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getPredecessorsColumnTemplate(84)(item);"})
                GanttChartView.Columns.Add(New Column With {.Header = "Cost ($)", .Width = 100, .CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getCostColumnTemplate(84)(item);"})
                GanttChartView.Columns.Add(New Column With {.Header = "Est. start", .Width = 140, .CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getBaselineStartColumnTemplate(124, true, true, 8 * 60 * 60 * 1000)(item);"}) ' 8 AM
                GanttChartView.Columns.Add(New Column With {.Header = "Est. finish", .Width = 140, .CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getBaselineFinishColumnTemplate(124, true, true, 16 * 60 * 60 * 1000)(item);"}) ' 4 PM

                ' Optionally, set custom item tag objects, properties, append read only custom columns bound to their values, and/or set up custom cell template code statements to be executed on the client side.
                ' GanttChartView.Items(7).Tag = 70
                ' GanttChartView.Items(7).CustomValues("Description") = "Custom description"
                ' GanttChartView.Columns.Add(New Column With {.Header = "Description", .Width = 200, .PropertyName = "Description"})
                ' GanttChartView.Columns.Add(New Column With {.Header = "Status", .Width = 40, .CellTemplateClientCode = "return control.ownerDocument.createTextNode(item.content == 'Task 2.1' ? '!' : '');"})
                ' GanttChartView.Columns(CInt(ColumnType.Assignments)).CellTemplateClientCode = "return DlhSoft.Controls.GanttChartView.getAssignmentSelectorColumnTemplate(184, function (item) { return ['Resource 1', 'Resource 2']; })(item);" ' Resource assignment selector.

                ' Optionally, define assignable resources.
                GanttChartView.AssignableResources = New List(Of String) From {"Resource 1", "Resource 2", "Resource 3",
                                                                           "Material 1", "Material 2"}
                GanttChartView.AutoAppendAssignableResources = True

                ' Optionally, define the quantity values to consider when leveling resources, indicating maximum material amounts available for use at the same time.
                GanttChartView.ResourceQuantities = New Dictionary(Of String, Double) From {{"Material 1", 4}, {"Material 2", Double.PositiveInfinity}}
                items(10).AssignmentsContent = "Material 1 [300%], Material 2"
                items(11).AssignmentsContent = "Material 1 [50%], Material 2 [200%]"
                items(12).AssignmentsContent = "Material 1 [250%]"

                ' Optionally, define task and resource costs.
                ' GanttChartView.TaskInitiationCost = 5
                items(4).ExecutionCost = 50
                ' GanttChartView.DefaultResourceUsageCost = 1
                ' GanttChartView.SpecificResourceUsageCosts = New Dictionary(Of String, Double) From {{"Resource 1", 2}, {"Material 1", 7}}
                GanttChartView.DefaultResourceHourCost = 10
                GanttChartView.SpecificResourceHourCosts = New Dictionary(Of String, Double) From {{"Resource 1", 20}, {"Material 2", 0.5}}

                ' Optionally, display multiple item parts on a single chart line.
                ' GanttChartView.Items(13).Parts = New List(Of GanttChartItem) From
                ' {
                '     New GanttChartItem With {.Content = "Task 8 (Part 1)", .Start = New DateTime(year, month, 2, 8, 0, 0), .Finish = New DateTime(year, month, 4, 16, 0, 0)},
                '     New GanttChartItem With {.Content = "Task 8 (Part 2)", .Start = New DateTime(year, month, 8, 8, 0, 0), .Finish = New DateTime(year, month, 10, 12, 0, 0), .AssignmentsContent = "Resource 1"}
                ' }

                ' Optionally, set up item template code statements, and standard, summary, milestone, and/or extra task template code statements to be executed on the client side.
                ' GanttChartView.ItemTemplateClientCode = "var toolTip = document.createElementNS('http://www.w3.org/2000/svg', 'title');" + _
                '     "var toolTipContent = item.content + ' • ' + 'Start: ' + item.start.toLocaleString();" + _
                '     "if (!item.isMilestone)" + _
                '     "toolTipContent += ' • ' + 'Finish: ' + item.finish.toLocaleString();" + _
                '     "toolTip.appendChild(document.createTextNode(toolTipContent));" + _
                '     "return toolTip;"
                'GanttChartView.StandardTaskTemplateClientCode = "var document = control.ownerDocument, svgns = 'http://www.w3.org/2000/svg';" + _
                '     "var itemLeft = control.getChartPosition(item.start, control.settings), itemRight = control.getChartPosition(item.finish, control.settings);" + _
                '     "var containerGroup = document.createElementNS(svgns, 'g');" + _
                '     "var rect = document.createElementNS(svgns, 'rect');" + _
                '     "rect.setAttribute('x', itemLeft); rect.setAttribute('width', Math.max(0, itemRight - itemLeft - 1));" + _
                '     "rect.setAttribute('y', control.settings.barMargin); rect.setAttribute('height', control.settings.barHeight);" + _
                '     "rect.setAttribute('style', 'stroke: Red; fill: LightYellow');" + _
                '     "containerGroup.appendChild(rect);" + _
                '     "var thumb = document.createElementNS(svgns, 'rect');" + _
                '     "thumb.setAttribute('x', itemLeft); thumb.setAttribute('width', Math.max(0, itemRight - itemLeft - 1));" + _
                '     "thumb.setAttribute('y', control.settings.barMargin); thumb.setAttribute('height', control.settings.barHeight);" + _
                '     "thumb.setAttribute('style', 'fill: Transparent; cursor: move');" + _
                '     "DlhSoft.Controls.GanttChartView.initializeTaskDraggingThumbs(thumb, null, null, null, item, itemLeft, itemRight, null);" + _
                '     "containerGroup.appendChild(thumb);" + _
                '     "return containerGroup;"
                ' GanttChartView.ExtraTaskTemplateClientCode = "var rect = control.ownerDocument.createElementNS('http://www.w3.org/2000/svg', 'rect'); var itemLeft = control.getChartPosition(item.start, control.settings); rect.setAttribute('x', itemLeft - 20 + (!item.hasChildren && !item.isMilestone ? 5 : 0)); rect.setAttribute('y', control.settings.barMargin); rect.setAttribute('width', 12); rect.setAttribute('height', control.settings.barHeight); rect.setAttribute('style', 'stroke: Blue; fill: Blue; fill-opacity: 0.1'); return rect;"
                ' GanttChartView.Items(7).TaskTemplateClientCode = "var rect = control.ownerDocument.createElementNS('http://www.w3.org/2000/svg', 'rect'); var itemLeft = control.getChartPosition(item.start, control.settings); var itemRight = control.getChartPosition(item.finish, control.settings); rect.setAttribute('x', itemLeft); rect.setAttribute('y', control.settings.barMargin); rect.setAttribute('width', Math.max(0, itemRight - itemLeft - 1)); rect.setAttribute('height', control.settings.barHeight); rect.setAttribute('style', 'stroke: DarkGreen; fill: LightGreen'); return rect;"

                ' Optionally, apply visibility filter to display only specific items in the view.
                ' GanttChartView.VisibilityFilterClientCode = "return item.content.indexOf('Task 2') >= 0;"

                ' Optionally, set HasFixedEffort to true to automatically update item assignment allocation units rather than effort upon duration changes.
                ' GanttChartView.Items(4).HasFixedEffort = True

                ' Optionally, set up auto-scheduling behavior for dependent tasks based on predecessor information, supplementary disallowing circular dependencies.
                GanttChartView.AreTaskDependencyConstraintsEnabled = True

                ' Optionally, disable auto-scheduling for specific items (turning on manual scheduling back for them.)
                ' GanttChartView.Items(7).AreDependencyConstraintsEnabled = False

                ' Optionally, specify the application target in order for the component to adapt to the screen size.
                ' GanttChartView.Target = DlhSoft.Web.UI.WebControls.PresentationTarget.Phone

                ' Optionally, set up custom initialization, and item property and selection change handlers.
                ' GanttChartView.InitializingClientCode = "control.settings.dateTimeFormatter = control.settings.dateFormatter; if (typeof console !== 'undefined') console.log('The component is about to be initialized using simple date formatting.');"
                ' GanttChartView.InitializedClientCode = "if (typeof console !== 'undefined') console.log('The component has been successfully initialized.');"
                ' GanttChartView.ItemPropertyChangeHandlerClientCode = "if (isDirect && isFinal && typeof console !== 'undefined') console.log(item.content + '.' + propertyName + ' has changed.');"
                ' GanttChartView.ItemSelectionChangeHandlerClientCode = "if (isSelected && isDirect && typeof console !== 'undefined') console.log(item.content + ' has been selected.');"
            End If

            ' Optionally, initialize custom theme And templates (themes.js, templates.js).
            Dim initializingClientCodeGetter As Func(Of String, String) =
                Function(Type As String)
                    Return "initialize" + Type + "Theme(control.settings, theme);" +
                            IIf(Type = "GanttChart" Or Type = "ScheduleChart" Or Type = "PertChart", "
                            initialize" + Type + "Templates(control.settings, theme);", String.Empty)
                End Function
            If Not IsPostBack Then GanttChartView.InitializingClientCode += initializingClientCodeGetter("GanttChart")
            ScheduleChartView.InitializingClientCode = initializingClientCodeGetter("ScheduleChart")
            LoadChartView.InitializingClientCode = initializingClientCodeGetter("LoadChart")
            PertChartView.InitializingClientCode = initializingClientCodeGetter("PertChart")
            NetworkDiagramView.InitializingClientCode = initializingClientCodeGetter("PertChart")

            ' Optionally, receive server side notifications when selection changes have occured on the client side by handling the SelectionChanged event.
            ' AddHandler GanttChartView.SelectionChanged, Sub() Console.WriteLine("Selected item index {0}.", GanttChartView.SelectedIndex)

            ' Receive server side notifications for the item property changes that have occured on the client side by handling the ItemPropertyChanged event.
            AddHandler GanttChartView.ItemPropertyChanged, AddressOf GanttChartView_ItemPropertyChanged
        End Sub

        ' Handle the individual item property change retreived as event argument.
        Private Sub GanttChartView_ItemPropertyChanged(ByVal sender As Object, ByVal e As ItemPropertyChangedEventArgs)
            ' Optionally or alternatively, record the item property change in a temporary storage collection easily accessible in the application code later.
            ' Changes.Add(e)
        End Sub

        ' ReadOnly Property Changes As List(Of ItemPropertyChangedEventArgs)
        '     Get
        '         If (Session("Changes") Is Nothing) Then Session("Changes") = New List(Of ItemPropertyChangedEventArgs)
        '         Return TryCast(Session("Changes"), List(Of ItemPropertyChangedEventArgs))
        '     End Get
        ' End Property

        ' Define user command methods.
        Public Sub AddNewItemButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim item = New GanttChartItem With {.Content = "New task", .Start = New Date(year, month, 2, 8, 0, 0), .Finish = New Date(year, month, 4, 16, 0, 0)}
            GanttChartView.AddItem(item)
            GanttChartView.SelectedItem = item
            GanttChartView.ScrollTo(item)
            GanttChartView.ScrollTo(New Date(year, month, 1))
        End Sub
        Public Sub InsertNewItemButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            If GanttChartView.SelectedItem Is Nothing Then Return
            Dim item = New GanttChartItem With {.Content = "New task", .Start = New Date(year, month, 2, 8, 0, 0), .Finish = New Date(year, month, 4, 16, 0, 0)}
            GanttChartView.InsertItem(GanttChartView.SelectedIndex, item)
            GanttChartView.SelectedItem = item
            GanttChartView.ScrollTo(item)
            GanttChartView.ScrollTo(New Date(year, month, 1))
        End Sub
        Public Sub IncreaseItemIndentationButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim item = GanttChartView.SelectedItem
            If item Is Nothing Then Return
            GanttChartView.IncreaseItemIndentation(item)
            GanttChartView.ScrollTo(item)
        End Sub
        Public Sub DecreaseItemIndentationButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim item = GanttChartView.SelectedItem
            If item Is Nothing Then Return
            GanttChartView.DecreaseItemIndentation(item)
            GanttChartView.ScrollTo(item)
        End Sub
        Public Sub DeleteItemButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim item = GanttChartView.SelectedItem
            If item Is Nothing Then
                Return
            End If
            GanttChartView.RemoveItem(item)
        End Sub
        Public Sub SetCustomBarColorToItemButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim item = GanttChartView.SelectedItem
            If item Is Nothing Then Return
            item.BarStroke = Color.Red
            item.BarFill = Color.Yellow
            GanttChartView.ScrollTo(item)
        End Sub
        Public Sub CopyItemButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim item = GanttChartView.SelectedItem
            If item Is Nothing Then Return
            ViewState("CopiedItem") = item
        End Sub
        Public Sub PasteItemButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            If ViewState("CopiedItem") Is Nothing Or GanttChartView.SelectedItem Is Nothing Then Return
            Dim copiedItem As GanttChartItem = ViewState("CopiedItem")
            Dim item = New GanttChartItem With {.Content = copiedItem.Content, .Start = copiedItem.Start, .Finish = copiedItem.Finish, .CompletedFinish = copiedItem.CompletedFinish, .IsMilestone = copiedItem.IsMilestone, .AssignmentsContent = copiedItem.AssignmentsContent}
            GanttChartView.InsertItem(GanttChartView.SelectedIndex + 1, item)
            GanttChartView.SelectedItem = item
            GanttChartView.ScrollTo(item)
            GanttChartView.ScrollTo(item.Start)
        End Sub
        Public Sub MoveItemUpButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim item = GanttChartView.SelectedItem
            If item Is Nothing Then Return
            GanttChartView.MoveItemUp(item, True, True)
        End Sub
        Public Sub MoveItemDownButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim item = GanttChartView.SelectedItem
            If item Is Nothing Then Return
            GanttChartView.MoveItemDown(item, True, True)
        End Sub
        Public Sub IncreaseTimelinePageButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            GanttChartView.IncreaseTimelinePage(TimeSpan.FromDays(4 * 7)) ' 4 weeks
        End Sub
        Public Sub DecreaseTimelinePageButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            GanttChartView.DecreaseTimelinePage(TimeSpan.FromDays(4 * 7)) ' 4 weeks
        End Sub
        Public Sub SetCustomScalesButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            GanttChartView.HeaderHeight = 21 * 3
            GanttChartView.Scales = New List(Of Scale) From {
            New Scale With {.ScaleType = ScaleType.Months, .HeaderTextFormat = ScaleHeaderTextFormat.Month, .IsSeparatorVisible = True},
            New Scale With {.ScaleType = ScaleType.Weeks, .HeaderTextFormat = ScaleHeaderTextFormat.Date, .IsSeparatorVisible = True},
            New Scale With {.ScaleType = ScaleType.Days, .HeaderTextFormat = ScaleHeaderTextFormat.Day}
        }
            GanttChartView.CurrentTimeLineColor = Color.Red
            GanttChartView.UpdateScaleInterval = TimeSpan.FromHours(1)
            GanttChartView.HourWidth = 5
            GanttChartView.VisibleWeekStart = DayOfWeek.Monday
            GanttChartView.VisibleWeekFinish = DayOfWeek.Friday
            GanttChartView.WorkingWeekStart = DayOfWeek.Monday
            GanttChartView.WorkingWeekFinish = DayOfWeek.Thursday
            GanttChartView.VisibleDayStart = TimeOfDay.Parse("1000:00") ' 10 AM
            GanttChartView.VisibleDayFinish = TimeOfDay.Parse("2000:00") ' 8 PM
            GanttChartView.SpecialNonworkingDays = New List(Of DlhSoft.Windows.Data.Date) From {
            New DlhSoft.Windows.Data.Date(year, month, 24),
            New DlhSoft.Windows.Data.Date(year, month, 25)
        }
        End Sub
        Public Sub ZoomInButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            GanttChartView.HourWidth *= 2
        End Sub
        Public Sub ToggleBaselineButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            GanttChartView.IsBaselineVisible = Not GanttChartView.IsBaselineVisible
            ToggleBaselineButton.CssClass = IIf(GanttChartView.IsBaselineVisible, "toggle pressed", "toggle")
        End Sub
        Public Sub HighlightCriticalPathButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            HighlightCriticalPathButton.CssClass = "toggle pressed"
            ' Reset the view.
            For Each item As GanttChartItem In GanttChartView.Items
                item.BarStroke = Nothing
                item.BarFill = Nothing
            Next item
            ' Set up red as bar stroke and fill properties for the critical items.
            For Each item As GanttChartItem In GanttChartView.GetCriticalItems()
                item.BarStroke = Color.Red
                item.BarFill = Color.Red
            Next item
        End Sub
        Public Sub SplitRemainingWorkButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            If GanttChartView.SelectedItem Is Nothing Then Return
            Dim remainingWorkItem = GanttChartView.SplitRemainingWork(GanttChartView.SelectedItem, " (rem. work)", " (compl. work)")
            If remainingWorkItem Is Nothing Then Return
                                                                                          GanttChartView.ScrollTo(remainingWorkItem)
        End Sub
        Public Sub ToggleDependencyConstraintsButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            GanttChartView.AreTaskDependencyConstraintsEnabled = Not GanttChartView.AreTaskDependencyConstraintsEnabled
            ToggleDependencyConstraintsButton.CssClass = IIf(GanttChartView.AreTaskDependencyConstraintsEnabled, "toggle pressed", "toggle")
        End Sub
        Public Sub LevelResourcesButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            ' Level the assigned resources for all tasks, including the already started ones, considering the current time displayed in the chart.
            GanttChartView.LevelResources(True, GanttChartView.CurrentTime)
            ' Alternatively, optimize work to obtain the minimum project finish date and time assuming unlimited resource availability:
            ' GanttChartView.OptimizeWork(False, True, GanttChartView.CurrentTime)
        End Sub
        Public Sub ScheduleChartButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            ScheduleChartPanel.Visible = True
        End Sub
        Public Sub CloseScheduleChartViewButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            ScheduleChartPanel.Visible = False
        End Sub
        Public Sub LoadChartButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            LoadChartPanel.Visible = True
        End Sub
        Public Sub CloseLoadChartViewButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            LoadChartPanel.Visible = False
        End Sub
        Public Sub PertChartButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            PertChartPanel.Visible = True
        End Sub
        Public Sub ClosePertChartViewButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            PertChartPanel.Visible = False
        End Sub
        Public Sub NetworkDiagramButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            NetworkDiagramPanel.Visible = True
        End Sub
        Public Sub CloseNetworkDiagramViewButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            NetworkDiagramPanel.Visible = False
        End Sub
        Public Sub ProjectStatisticsButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim startOutput = GanttChartView.GetProjectStart().ToShortDateString()
            Dim finishOutput = GanttChartView.GetProjectFinish().ToShortDateString()
            Dim effortOutput = GanttChartView.GetProjectTotalEffort().TotalHours.ToString("0.##")
            Dim completionOutput = GanttChartView.GetProjectCompletion().ToString("0.##%")
            Dim costOutput = GanttChartView.GetProjectCost().ToString("$0.##")
            Dim output = "Project statistics:\nStart:\t" + startOutput + "\nFinish:\t" + finishOutput + "\nEffort:\t" + effortOutput + "h\nCompl.:\t" + completionOutput + "\nCost:\t" + costOutput
            ScriptManager.RegisterStartupScript(Me, GetType(Index), "ProjectStatistics", "setTimeout(function() { alert('" + output + "'); }, 1000);", True)
        End Sub
        Public Sub SaveProjectXmlButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Session("DownloadContent") = GanttChartView.GetProjectXml()
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Index), "Download", "window.open('Download.aspx?ContentType=text/xml&Filename=Project.xml', '_blank');", True)
        End Sub
        Public Sub LoadProjectXmlButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            LoadProjectXmlPanel.Visible = True
        End Sub
        Public Sub LoadProjectXmlSubmitButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            GanttChartView.LoadProjectXml(LoadProjectXmlFileUpload.FileContent)
            LoadProjectXmlPanel.Visible = False
        End Sub
        Public Sub CloseLoadProjectXmlButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            LoadProjectXmlPanel.Visible = False
        End Sub
        Public Sub PrintButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            ' Print the task hierarchy column and a selected timeline page of 5 weeks (timeline end week extensions would be added automatically, if necessary).
            ' Optionally, to rotate the print output and simmulate Landscape printing mode (when the end user keeps Portrait selection in the Print dialog), append the rotate parameter set to true to the method call: rotate:=True.
            GanttChartView.Print(title:="Gantt Chart (printable)", isGridVisible:=True, columnIndexes:=New Integer() {2}, timelineStart:=New Date(year, month, 1), timelineFinish:=(New Date(year, month, 1)).AddDays(5 * 7), preparingMessage:="...")
        End Sub

        Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs)
            ' Optionally, synchronize other displayed views at initialization time and upon standard Gantt Chart item changes on the client side.
            Dim initalizationCode = String.Format("var ganttChartView = document.querySelector('#{0}');", GanttChartView.ClientID)
            GanttChartView.InitializedClientCode &= initalizationCode
            GanttChartView.ItemPropertyChangeHandlerClientCode &= initalizationCode
            GanttChartView.DisplayedTimeChangeHandlerClientCode &= initalizationCode
            GanttChartView.SplitterPositionChangeHandlerClientCode &= initalizationCode
            If ScheduleChartPanel.Visible Then
                ScheduleChartView.Items = GanttChartView.GetScheduleChartItems()
                ScheduleChartView.CopyCommonSettings(GanttChartView)
                GanttChartView.InitializedClientCode &= String.Format(ControlChars.CrLf & "ganttChartView.refreshScheduleChartView = function() {{" & ControlChars.CrLf & "if (!ganttChartView.isWaitingToRefreshScheduleChartView) {{" & ControlChars.CrLf & "ganttChartView.isWaitingToRefreshScheduleChartView = true;" & ControlChars.CrLf & "setTimeout(function() {{" & ControlChars.CrLf & "var scheduleChartView = document.querySelector('#{0}');" & ControlChars.CrLf & "if (scheduleChartView == null || typeof scheduleChartView.isScheduleChartInitialized === 'undefined' || !scheduleChartView.isScheduleChartInitialized)" & ControlChars.CrLf & "return;" & ControlChars.CrLf & "scheduleChartView.scheduleChartItems = ganttChartView.getScheduleChartItems();" & ControlChars.CrLf & "ganttChartView.copyCommonSettings(scheduleChartView.settings);" & ControlChars.CrLf & "scheduleChartView.refresh();" & ControlChars.CrLf & "scheduleChartView.settings.displayedTimeChangeHandler = function(displayedTime) {{ ganttChartView.refreshDisplayedTime(displayedTime); if (typeof ganttChartView.refreshLoadChartViewDisplayedTime !== 'undefined') ganttChartView.refreshLoadChartViewDisplayedTime(displayedTime); }}" & ControlChars.CrLf & "scheduleChartView.settings.splitterPositionChangeHandler = function(gridWidth, chartWidth) {{ ganttChartView.refreshSplitterPosition(gridWidth, chartWidth); if (typeof ganttChartView.refreshLoadChartViewSplitterPosition !== 'undefined') ganttChartView.refreshLoadChartViewSplitterPosition(gridWidth, chartWidth); }}" & ControlChars.CrLf & "ganttChartView.isWaitingToRefreshScheduleChartView = false;" & ControlChars.CrLf & "}}, 0);" & ControlChars.CrLf & "}}" & ControlChars.CrLf & "}};" & ControlChars.CrLf & "setTimeout(function() {{" & ControlChars.CrLf & "var scheduleChartView = document.querySelector('#{0}');" & ControlChars.CrLf & "if (scheduleChartView == null)" & ControlChars.CrLf & "return;" & ControlChars.CrLf & "ganttChartView.copyCommonSettings(scheduleChartView.settings);" & ControlChars.CrLf & "scheduleChartView.settings.displayedTimeChangeHandler = function(displayedTime) {{ ganttChartView.refreshDisplayedTime(displayedTime); if (typeof ganttChartView.refreshLoadChartViewDisplayedTime !== 'undefined') ganttChartView.refreshLoadChartViewDisplayedTime(displayedTime); }}" & ControlChars.CrLf & "scheduleChartView.settings.splitterPositionChangeHandler = function(gridWidth, chartWidth) {{ ganttChartView.refreshSplitterPosition(gridWidth, chartWidth); if (typeof ganttChartView.refreshLoadChartViewSplitterPosition !== 'undefined') ganttChartView.refreshLoadChartViewSplitterPosition(gridWidth, chartWidth); }}" & ControlChars.CrLf & "}}, 0);" & ControlChars.CrLf & "ganttChartView.refreshScheduleChartViewDisplayedTime = function(displayedTime) {{" & ControlChars.CrLf & "if (!ganttChartView.isWaitingToRefreshScheduleChartViewDisplayedTime) {{" & ControlChars.CrLf & "ganttChartView.isWaitingToRefreshScheduleChartViewDisplayedTime = true;" & ControlChars.CrLf & "setTimeout(function() {{" & ControlChars.CrLf & "var scheduleChartView = document.querySelector('#{0}');" & ControlChars.CrLf & "if (scheduleChartView == null || typeof scheduleChartView.isScheduleChartInitialized === 'undefined' || !scheduleChartView.isScheduleChartInitialized)" & ControlChars.CrLf & "return;" & ControlChars.CrLf & "scheduleChartView.scrollToDateTime(displayedTime);" & ControlChars.CrLf & "ganttChartView.isWaitingToRefreshScheduleChartViewDisplayedTime = false;" & ControlChars.CrLf & "}}, 0);" & ControlChars.CrLf & "}}" & ControlChars.CrLf & "}};" & ControlChars.CrLf & "ganttChartView.refreshScheduleChartViewSplitterPosition = function(gridWidth, chartWidth) {{" & ControlChars.CrLf & "if (!ganttChartView.isWaitingToRefreshScheduleChartViewSplitterPosition) {{" & ControlChars.CrLf & "ganttChartView.isWaitingToRefreshScheduleChartViewSplitterPosition = true;" & ControlChars.CrLf & "setTimeout(function() {{" & ControlChars.CrLf & "var scheduleChartView = document.querySelector('#{0}');" & ControlChars.CrLf & "if (scheduleChartView == null || typeof scheduleChartView.isScheduleChartInitialized === 'undefined' || !scheduleChartView.isScheduleChartInitialized)" & ControlChars.CrLf & "return;" & ControlChars.CrLf & "scheduleChartView.setSplitterPosition(gridWidth, chartWidth);" & ControlChars.CrLf & "ganttChartView.isWaitingToRefreshScheduleChartViewSplitterPosition = false;" & ControlChars.CrLf & "}}, 0);" & ControlChars.CrLf & "}}" & ControlChars.CrLf & "}};", ScheduleChartView.ClientID)
                GanttChartView.ItemPropertyChangeHandlerClientCode &= String.Format(ControlChars.CrLf & "if (isDirect && isFinal && ((!item.hasChildren && (propertyName == 'content' || propertyName == 'start' || propertyName == 'finish' || propertyName == 'completedFinish' || propertyName == 'isMilestone' || propertyName == 'assignmentsContent')) || propertyName == 'indentation')) {{" & ControlChars.CrLf & "ganttChartView.refreshScheduleChartView();" & ControlChars.CrLf & "}}")
                GanttChartView.DisplayedTimeChangeHandlerClientCode &= String.Format(ControlChars.CrLf & "if (typeof ganttChartView.refreshScheduleChartViewDisplayedTime !== 'undefined')" & ControlChars.CrLf & "ganttChartView.refreshScheduleChartViewDisplayedTime(displayedTime);")
                GanttChartView.SplitterPositionChangeHandlerClientCode &= String.Format(ControlChars.CrLf & "if (typeof ganttChartView.refreshScheduleChartViewSplitterPosition !== 'undefined')" & ControlChars.CrLf & "ganttChartView.refreshScheduleChartViewSplitterPosition(gridWidth, chartWidth);")
                GanttChartView.HourWidthChangeHandlerClientCode &= ControlChars.CrLf & "ganttChartView.refreshScheduleChartView();"
            End If
            If LoadChartPanel.Visible Then
                LoadChartView.Items = GanttChartView.GetLoadChartItems()
                LoadChartView.CopyCommonSettings(GanttChartView)
                GanttChartView.InitializedClientCode &= String.Format(ControlChars.CrLf & "ganttChartView.refreshLoadChartResourceSelector = function() {{" & ControlChars.CrLf & "var loadChartResourceFilter = document.querySelector('#loadChartResourceFilter'), i;" & ControlChars.CrLf & "if (loadChartResourceFilter == null)" & ControlChars.CrLf & "return;" & ControlChars.CrLf & "var previouslySelectedResource = loadChartResourceFilter.value;" & ControlChars.CrLf & "for (i = loadChartResourceFilter.childNodes.length; i-- > 2; )" & ControlChars.CrLf & "loadChartResourceFilter.removeChild(loadChartResourceFilter.childNodes[i]);" & ControlChars.CrLf & "var resources = ganttChartView.getAssignedResources();" & ControlChars.CrLf & "for (i = 0; i < resources.length; i++) {{" & ControlChars.CrLf & "var resource = resources[i];" & ControlChars.CrLf & "var option = document.createElement('option');" & ControlChars.CrLf & "option.appendChild(document.createTextNode(resource));" & ControlChars.CrLf & "if (resource == previouslySelectedResource)" & ControlChars.CrLf & "option.setAttribute('selected', 'true');" & ControlChars.CrLf & "loadChartResourceFilter.appendChild(option);" & ControlChars.CrLf & "}}" & ControlChars.CrLf & "}}" & ControlChars.CrLf & "setTimeout(ganttChartView.refreshLoadChartResourceSelector, 0);" & ControlChars.CrLf & "ganttChartView.refreshLoadChartView = function() {{" & ControlChars.CrLf & "if (!ganttChartView.isWaitingToRefreshLoadChartView) {{" & ControlChars.CrLf & "ganttChartView.isWaitingToRefreshLoadChartView = true;" & ControlChars.CrLf & "setTimeout(function() {{" & ControlChars.CrLf & "var loadChartView = document.querySelector('#{0}');" & ControlChars.CrLf & "if (loadChartView == null || typeof loadChartView.isLoadChartInitialized === 'undefined' || !loadChartView.isLoadChartInitialized)" & ControlChars.CrLf & "return;" & ControlChars.CrLf & "var loadChartResourceFilter = document.querySelector('#loadChartResourceFilter');" & ControlChars.CrLf & "if (loadChartResourceFilter == null)" & ControlChars.CrLf & "return;" & ControlChars.CrLf & "var resourceFilterValue = loadChartResourceFilter.value;" & ControlChars.CrLf & "if (resourceFilterValue == '') {{" & ControlChars.CrLf & "loadChartView.loadChartItems = ganttChartView.getLoadChartItems();" & ControlChars.CrLf & "loadChartView.settings.itemHeight = 21;" & ControlChars.CrLf & "loadChartView.settings.barHeight = 10.5;" & ControlChars.CrLf & "}}" & ControlChars.CrLf & "else {{" & ControlChars.CrLf & "loadChartView.loadChartItems = ganttChartView.getLoadChartItems([resourceFilterValue]);" & ControlChars.CrLf & "loadChartView.settings.itemHeight = 63;" & ControlChars.CrLf & "loadChartView.settings.barHeight = 52.5;" & ControlChars.CrLf & "}}" & ControlChars.CrLf & "ganttChartView.copyCommonSettings(loadChartView.settings);" & ControlChars.CrLf & "loadChartView.refresh();" & ControlChars.CrLf & "loadChartView.settings.displayedTimeChangeHandler = function(displayedTime) {{ ganttChartView.refreshDisplayedTime(displayedTime); if (typeof ganttChartView.refreshScheduleChartViewDisplayedTime !== 'undefined') ganttChartView.refreshScheduleChartViewDisplayedTime(displayedTime); }}" & ControlChars.CrLf & "loadChartView.settings.splitterPositionChangeHandler = function(gridWidth, chartWidth) {{ ganttChartView.refreshSplitterPosition(gridWidth, chartWidth); if (typeof ganttChartView.refreshScheduleChartViewSplitterPosition !== 'undefined') ganttChartView.refreshScheduleChartViewSplitterPosition(gridWidth, chartWidth); }}" & ControlChars.CrLf & "ganttChartView.isWaitingToRefreshLoadChartView = false;" & ControlChars.CrLf & "}}, 0);" & ControlChars.CrLf & "}}" & ControlChars.CrLf & "}};" & ControlChars.CrLf & "setTimeout(function() {{" & ControlChars.CrLf & "var loadChartView = document.querySelector('#{0}');" & ControlChars.CrLf & "if (loadChartView == null)" & ControlChars.CrLf & "return;" & ControlChars.CrLf & "ganttChartView.copyCommonSettings(loadChartView.settings);" & ControlChars.CrLf & "loadChartView.settings.displayedTimeChangeHandler = function(displayedTime) {{ ganttChartView.refreshDisplayedTime(displayedTime); if (typeof ganttChartView.refreshLoadChartViewDisplayedTime !== 'undefined') ganttChartView.refreshLoadChartViewDisplayedTime(displayedTime); }}" & ControlChars.CrLf & "loadChartView.settings.splitterPositionChangeHandler = function(gridWidth, chartWidth) {{ ganttChartView.refreshSplitterPosition(gridWidth, chartWidth); if (typeof ganttChartView.refreshScheduleChartViewSplitterPosition !== 'undefined') ganttChartView.refreshScheduleChartViewSplitterPosition(gridWidth, chartWidth); }}" & ControlChars.CrLf & "}}, 0);" & ControlChars.CrLf & "ganttChartView.refreshLoadChartViewDisplayedTime = function(displayedTime) {{" & ControlChars.CrLf & "if (!ganttChartView.isWaitingToRefreshLoadChartViewDisplayedTime) {{" & ControlChars.CrLf & "ganttChartView.isWaitingToRefreshLoadChartViewDisplayedTime = true;" & ControlChars.CrLf & "setTimeout(function() {{" & ControlChars.CrLf & "var loadChartView = document.querySelector('#{0}');" & ControlChars.CrLf & "if (loadChartView == null || typeof loadChartView.isLoadChartInitialized === 'undefined' || !loadChartView.isLoadChartInitialized)" & ControlChars.CrLf & "return;" & ControlChars.CrLf & "loadChartView.scrollToDateTime(displayedTime);" & ControlChars.CrLf & "ganttChartView.isWaitingToRefreshLoadChartViewDisplayedTime = false;" & ControlChars.CrLf & "}}, 0);" & ControlChars.CrLf & "}}" & ControlChars.CrLf & "}};" & ControlChars.CrLf & "ganttChartView.refreshLoadChartViewSplitterPosition = function(gridWidth, chartWidth) {{" & ControlChars.CrLf & "if (!ganttChartView.isWaitingToRefreshLoadChartViewSplitterPosition) {{" & ControlChars.CrLf & "ganttChartView.isWaitingToRefreshLoadChartViewSplitterPosition = true;" & ControlChars.CrLf & "setTimeout(function() {{" & ControlChars.CrLf & "var loadChartView = document.querySelector('#{0}');" & ControlChars.CrLf & "if (loadChartView == null || typeof loadChartView.isLoadChartInitialized === 'undefined' || !loadChartView.isLoadChartInitialized)" & ControlChars.CrLf & "return;" & ControlChars.CrLf & "loadChartView.setSplitterPosition(gridWidth, chartWidth);" & ControlChars.CrLf & "ganttChartView.isWaitingToRefreshLoadChartViewSplitterPosition = false;" & ControlChars.CrLf & "}}, 0);" & ControlChars.CrLf & "}}" & ControlChars.CrLf & "}};", LoadChartView.ClientID)
                GanttChartView.ItemPropertyChangeHandlerClientCode &= String.Format(ControlChars.CrLf & "if (isDirect && isFinal && ((!item.hasChildren && (propertyName == 'content' || propertyName == 'start' || propertyName == 'finish' || propertyName == 'isMilestone' || propertyName == 'assignmentsContent')) || propertyName == 'indentation')) {{" & ControlChars.CrLf & "ganttChartView.refreshLoadChartResourceSelector();" & ControlChars.CrLf & "ganttChartView.refreshLoadChartView();" & ControlChars.CrLf & "}}")
                GanttChartView.DisplayedTimeChangeHandlerClientCode &= String.Format(ControlChars.CrLf & "if (typeof ganttChartView.refreshLoadChartViewDisplayedTime !== 'undefined')" & ControlChars.CrLf & "ganttChartView.refreshLoadChartViewDisplayedTime(displayedTime);")
                GanttChartView.SplitterPositionChangeHandlerClientCode &= String.Format(ControlChars.CrLf & "if (typeof ganttChartView.refreshLoadChartViewSplitterPosition !== 'undefined')" & ControlChars.CrLf & "ganttChartView.refreshLoadChartViewSplitterPosition(gridWidth, chartWidth);")
                GanttChartView.HourWidthChangeHandlerClientCode &= ControlChars.CrLf & "ganttChartView.refreshLoadChartView();"
            End If
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Index), "LoadChartResourceFilterChanged", String.Format(ControlChars.CrLf & "function loadChartResourceFilterChanged()" & ControlChars.CrLf & "{{" & ControlChars.CrLf & "var ganttChartView = document.querySelector('#{0}');" & ControlChars.CrLf & "if (typeof ganttChartView.refreshLoadChartView !== 'undefined')" & ControlChars.CrLf & "ganttChartView.refreshLoadChartView();" & ControlChars.CrLf & "}}", GanttChartView.ClientID), True)
            If PertChartPanel.Visible Then
                PertChartView.Items = GanttChartView.GetPertChartItems()
                For Each predecessorItem As Pert.PredecessorItem In PertChartView.GetCriticalDependencies()
                    predecessorItem.Item.ShapeStroke = Color.Red
                    predecessorItem.DependencyLineStroke = Color.Red
                Next predecessorItem
                Dim finish = PertChartView.GetFinish()
                finish.ShapeStroke = Color.Red
                ' Optionally, reposition start and finish milestones to get better diagram layout.
                ' PertChartView.RepositionEnds()
                GanttChartView.InitializedClientCode &= String.Format(ControlChars.CrLf & "ganttChartView.hidePertChartView = function() {{" & ControlChars.CrLf & "var pertChartPanel = document.querySelector('#{0}');" & ControlChars.CrLf & "if (pertChartPanel == null)" & ControlChars.CrLf & "return;" & ControlChars.CrLf & "pertChartPanel.style.display = 'none';" & ControlChars.CrLf & "}};", PertChartPanel.ClientID)
                GanttChartView.ItemPropertyChangeHandlerClientCode &= String.Format(ControlChars.CrLf & "if (isDirect && isFinal && ((!item.hasChildren && (propertyName == 'content' || propertyName == 'start' || propertyName == 'finish' || propertyName == 'completedFinish' || propertyName == 'isMilestone' || propertyName == 'assignmentsContent')) || propertyName == 'indentation')) {{" & ControlChars.CrLf & "ganttChartView.hidePertChartView();" & ControlChars.CrLf & "}}")
            End If
            If NetworkDiagramPanel.Visible Then
                NetworkDiagramView.Items = GanttChartView.GetNetworkDiagramItems()
                For Each item As Pert.NetworkDiagramItem In NetworkDiagramView.GetCriticalItems()
                    item.ShapeStroke = Color.Red
                Next item
                ' Optionally, reposition start and finish milestones to get better diagram layout.
                ' NetworkDiagramView.RepositionEnds()
                GanttChartView.InitializedClientCode &= String.Format(ControlChars.CrLf & "ganttChartView.hideNetworkDiagramView = function() {{" & ControlChars.CrLf & "var networkDiagramPanel = document.querySelector('#{0}');" & ControlChars.CrLf & "if (networkDiagramPanel == null)" & ControlChars.CrLf & "return;" & ControlChars.CrLf & "networkDiagramPanel.style.display = 'none';" & ControlChars.CrLf & "}};", NetworkDiagramPanel.ClientID)
                GanttChartView.ItemPropertyChangeHandlerClientCode &= String.Format(ControlChars.CrLf & "if (isDirect && isFinal && ((!item.hasChildren && (propertyName == 'content' || propertyName == 'start' || propertyName == 'finish' || propertyName == 'completedFinish' || propertyName == 'isMilestone' || propertyName == 'assignmentsContent')) || propertyName == 'indentation')) {{" & ControlChars.CrLf & "ganttChartView.hideNetworkDiagramView();" & ControlChars.CrLf & "}}")
            End If
            GanttChartView.InitializedClientCode &= ControlChars.CrLf & "ganttChartView.refreshDisplayedTime = function(displayedTime) {{" & ControlChars.CrLf & "if (!ganttChartView.isWaitingToRefreshDisplayedTime) {{" & ControlChars.CrLf & "ganttChartView.isWaitingToRefreshDisplayedTime = true;" & ControlChars.CrLf & "setTimeout(function() {{" & ControlChars.CrLf & "ganttChartView.scrollToDateTime(displayedTime);" & ControlChars.CrLf & "ganttChartView.isWaitingToRefreshDisplayedTime = false;" & ControlChars.CrLf & "}}, 0);" & ControlChars.CrLf & "}}" & ControlChars.CrLf & "}};" & ControlChars.CrLf & "ganttChartView.refreshSplitterPosition = function(gridWidth, chartWidth) {{" & ControlChars.CrLf & "if (!ganttChartView.isWaitingToRefreshSplitterPosition) {{" & ControlChars.CrLf & "ganttChartView.isWaitingToRefreshSplitterPosition = true;" & ControlChars.CrLf & "setTimeout(function() {{" & ControlChars.CrLf & "ganttChartView.setSplitterPosition(gridWidth, chartWidth);" & ControlChars.CrLf & "ganttChartView.isWaitingToRefreshSplitterPosition = false;" & ControlChars.CrLf & "}}, 0);" & ControlChars.CrLf & "}}" & ControlChars.CrLf & "}};"
        End Sub
    End Class

End Namespace
