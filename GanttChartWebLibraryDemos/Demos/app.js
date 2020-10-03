var queryString = window.location.search;
var initialSelection = queryString ? queryString.substr(1).replace('-', ' ') : null;
angular.module('Demos', [])
    .controller('MainController', function ($scope, $http, $timeout, $window) {
    var components = ['GanttChartView', 'ScheduleChartView', 'LoadChartView', 'PertChartView', 'NetworkDiagramView'];
    var samples = [
        {
            component: 'GanttChartView', feature: 'MainFeatures', title: 'Main features', description: 'Complex sample application showing how to use the most important features of the component',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css', 'templates.js', 'themes.js'],
                'VisualBasic': ['Index.aspx', 'Index.aspx.vb', 'app.css', 'templates.js', 'themes.js']
            }
        },
        {
            component: 'GanttChartView', feature: 'AssigningResources', title: 'Assigning resources (with multi-selector combo box)', description: 'Shows how resource assignments work and includes code providing automatic Load Chart synchronization',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'AutomaticScheduling', title: 'Automatic scheduling (dependency constraints)', description: 'Shows how task dependency constraints can be enabled to automatically schedule tasks upon all changes',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'Columns', title: 'Grid columns (built-in and custom)', description: 'Shows how to add supplemental built-in and custom grid columns',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'ReadOnlySettings', title: 'Read only, visibility, and other behavioral settings', description: 'Shows how you can set up read only, visibility, and other settings on the component and on specific items',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'Filtering', title: 'Filtering and hiding items', description: 'Shows how you can set up an item visibility filter function and hide individual items when needed',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'Interruptions', title: 'Interruptions', description: 'Shows how you can set up task template extensions to draw interruptions',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'ChangeNotifications', title: 'Change notifications (item value update handling)', description: 'Shows how custom code can be executed when changes are post back on the data presented by the component',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'SelectionModes', title: 'Selection mode (single, extended, by clicking)', description: 'Shows how you can set up selection mode and handle item selection changes in the component',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'WBS', title: 'WBS path (work breakdown structure column)', description: 'Shows how you can easily insert a WBS column to the grid',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'MoveUpDown', title: 'Move up-down (hierarchical moving)', description: 'Shows how you can allow the end user to move items up and down without breaking the hierarchy',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'BuiltinScales', title: 'Built-in scales (from years to hours)', description: 'Shows how you can combine and use built-in scale types, text header formats, and related settings',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'ZoomLevel', title: 'Zoom level (and disabling mouse wheel zooming)', description: 'Shows how you can set up zoom level settings for the chart area',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'CustomScale', title: 'Custom scale (time intervals and header texts)', description: 'Shows how to define a fully custom chart scale with special time intervals and text headers',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'MinuteScale', title: 'Minute scale (zoom in to hour quaters and minutes)', description: 'Shows how to zoom in and display hour quarters and minutes in the chart area',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'SpecialDays', title: 'Special days (vertically highlight specific time intervals)', description: 'Shows how you can highlight special time intervals in the chart area',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'Baseline', title: 'Baseline (estimation time bars vs. actual task bars)', description: 'Shows how you can define and display estimation bars for tasks (i.e. project baseline)',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'Statuses', title: 'Status columns (including color indicator)', description: 'Shows how to add supplemental custom columns for showing task statuses, such as To do, In progress, Behind schedule, and Completed',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'CriticalPath', title: 'Critical path (tasks that affect project finish)', description: 'Shows how you can determine and highlight critical tasks in your project (i.e. those that would affect the project finish date if their duration would increase)',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'ContinuousSchedule', title: 'Continuous schedule (non-stop working time)', description: 'Shows how to define continuous working time for tasks (24/7)',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'CustomSchedules', title: 'Custom schedules (general and for individual tasks)', description: 'Shows how to define custom working time and special nonworking days for all tasks with individual exceptions',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'DateTimeFormats', title: 'Date and time formats (simple or fully customized)', description: 'Shows how to set up custom formatting for dates, times, and durations',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'Styling', title: 'Styling (with CSS classes)', description: 'Shows how to style up elements defined by the component using CSS classes',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'CustomTemplate', title: 'Custom template (drawing item bars using custom SVG)', description: 'Shows how you can write code to customize drawing stanadard item bars in the chart area using SVG elements',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'AssignmentsTemplate', title: 'Assignments template (resource icons)', description: 'Shows how you can customize assignments template and show resource icons in the chart area',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'MultipleBarsPerItem', title: 'Multiple bars per item (parts)', description: 'Shows how you can define and display multiple bars for each task (i.e. item parts)',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'WorkOptimizations', title: 'Work optimizations (reschedule project, level resources)', description: 'Shows how you can optimize project timeline and avoiding resource over-allocation',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'MaterialResources', title: 'Material resources (quantities and costs)', description: 'Shows how you can assign material resources having limited or unlimited available quantities and compute task costs based on the allocations',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'TimeConstraints', title: 'Time constraints (minimum-maximum start and finish)', description: 'Shows how you can set up constraints on item date and times using minimum and/or maximum values',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'ProjectXml', title: 'Importing and exporting Microsoft® Project XML', description: 'Shows how you can import and export Microsoft® Project XML schema based content, providing maximum compatibility with other applications',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css', 'Download.aspx.cs']
            }
        },
        {
            component: 'GanttChartView', feature: 'Printing', title: 'Printing (virtual printers, e.g. Print to PDF, supported)', description: 'Includes code that initiates a print operation; end user can select the printer to use (virtual printers such as Print to PDF are supported as well)',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'ExportPngImage-PMF', title: 'Export image (using Project Management Framework)', description: 'Shows how to generate PNG images for the current Gantt Chart – using TaskManager component from DlhSoft Project Management Framework, available separately for free to Gantt Chart Hyper Library licensees',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css', 'GetPng.aspx.cs']
            }
        },
        {
            component: 'GanttChartView', feature: 'Performance', title: 'Performance (large data set)', description: 'Shows app responsiveness and other runtime performance features when loading large sets of hierarchical data',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'ScheduleChartView', feature: 'MainFeatures', title: 'Main features', description: 'Complex sample application showing how to use the most important features of the component',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css', 'templates.js', 'themes.js']
            }
        },
        {
            component: 'ScheduleChartView', feature: 'GanttChartIntegration', title: 'Gantt Chart integration', description: 'Sample application showing how to generate a Schedule Chart view from Gantt Chart data',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'ScheduleChartView', feature: 'Filtering', title: 'Filtering and hiding items', description: 'Shows how you can set up an item visibility filter function and hide individual items when needed',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'ScheduleChartView', feature: 'Hierarchy', title: 'Hierarchy (resource groups)', description: 'Sample application showing how to display expandable groups of resources in a hierarchical fashion',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'ScheduleChartView', feature: 'Columns', title: 'Grid columns (custom)', description: 'Shows how to add supplemental custom grid columns',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'ScheduleChartView', feature: 'ReadOnlySettings', title: 'Read only, visibility, and other behavioral settings', description: 'Shows how you can set up read only, visibility, and other settings on the component and on specific items',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'ScheduleChartView', feature: 'ChangeNotifications', title: 'Change notifications (item value update handling)', description: 'Shows how custom code can be executed when changes occur on the data presented by the component',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'ScheduleChartView', feature: 'BuiltinScales', title: 'Built-in scales (from years to hours)', description: 'Shows how you can combine and use built-in scale types, text header formats, and related settings',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'ScheduleChartView', feature: 'ZoomLevel', title: 'Zoom level (and disabling mouse wheel zooming)', description: 'Shows how you can set up zoom level settings for the chart area',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'ScheduleChartView', feature: 'CustomScale', title: 'Custom scale (time intervals and header texts)', description: 'Shows how to define a fully custom chart scale with special time intervals and text headers',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'ScheduleChartView', feature: 'SpecialDays', title: 'Special days (vertically highlight specific time intervals)', description: 'Shows how you can highlight special time intervals in the chart area',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'ScheduleChartView', feature: 'ContinuousSchedule', title: 'Continuous schedule (non-stop working time)', description: 'Shows how to define continuous working time for tasks (24/7)',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'ScheduleChartView', feature: 'Styling', title: 'Styling (with CSS classes)', description: 'Shows how to style up elements defined by the component using CSS classes',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'ScheduleChartView', feature: 'ResourceStatus', title: 'Status displaying (resource timeline)', description: 'Sample application showing how you can display multiple resources and their status at different times using chart bars of different colors',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'ScheduleChartView', feature: 'ShiftScheduling', title: 'Shift scheduling (assigning employees on time shifts)', description: 'Shows how you can define shifts as resource assignments so that the end user can drag and drop them vertically to change shifts as needed',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'ScheduleChartView', feature: 'CustomTemplate', title: 'Custom template (drawing item bars using custom SVG)', description: 'Shows how you can write code to customize drawing stanadard item bars in the chart area using SVG elements',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'ScheduleChartView', feature: 'Printing', title: 'Printing (virtual printers, e.g. Print to PDF, supported)', description: 'Includes code that initiates a print operation; end user can select the printer to use (virtual printers such as Print to PDF are supported as well)',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'LoadChartView', feature: 'MainFeatures', title: 'Main features', description: 'Complex sample application showing how to use the most important features of the component',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css', 'themes.js']
            }
        },
        {
            component: 'LoadChartView', feature: 'SingleItem', title: 'Single item', description: 'Sample application showing how to display a single item with multiple allocations',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'LoadChartView', feature: 'GanttChartIntegration', title: 'Gantt Chart integration', description: 'Sample application showing how to generate a Load Chart view from Gantt Chart data',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'PertChartView', feature: 'MainFeatures', title: 'Main features', description: 'Complex sample application showing how to use the most important features of the component',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css', 'templates.js', 'themes.js']
            }
        },
        {
            component: 'PertChartView', feature: 'GanttChartIntegration', title: 'Gantt Chart integration', description: 'Sample application showing how to generate a PERT Chart view from Gantt Chart data',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'NetworkDiagramView', feature: 'MainFeatures', title: 'Main features', description: 'Complex sample application showing how to use the most important features of the component',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css', 'templates.js', 'themes.js']
            }
        },
        {
            component: 'NetworkDiagramView', feature: 'GanttChartIntegration', title: 'Gantt Chart integration', description: 'Sample application showing how to generate a Network Diagram view from Gantt Chart data',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'Database', title: 'SQL Server®', description: 'App accessing data from a SQL Server® database',
            sourceCodeUrls: {
                'CSharp': 'http://DlhSoft.com/GanttChartWebLibrary/Documentation/Samples/ASP .NET/GanttChartViewDatabaseSample.zip'
            }
        }
    ];
    var themes = ['Default', 'Generic-bright', 'Generic-blue', 'DlhSoft-gray', 'Purple-green', 'Steel-blue', 'Dark-black', 'Cyan-green', 'Blue-navy', 'Orange-brown', 'Teal-green', 'Purple-beige', 'Gray-blue', 'Aero'];
    $scope.themes = themes;
    $scope.selectedTheme = themes[1];
    $scope.selectTheme = function (theme) {
        if (theme == $scope.selectedTheme)
            return;
        $scope.applyingTheme = theme;
        $scope.selectedTheme = null;
        $timeout(function () {
            $scope.selectedTheme = theme;
            $scope.applyingTheme = null;
            $scope.run();
        });
    };
    var technologies = [{ name: 'CSharp', title: 'C# + WebForms' }, { name: 'VisualBasic', title: 'Visual Basic®' }, { title: 'Classic MVC', url: 'https://github.com/DlhSoftTeam/GanttChartWebLibrary-Mvc-Demos/tree/master/GanttChartWebLibraryMvcDemos/Demos' }, { title: 'MVC + .NET Core', url: 'https://github.com/DlhSoftTeam/GanttChartWebLibrary-NetCore-Demos/tree/master/GanttChartWebLibrary-NetCore-Demos' }];
    $scope.technologies = technologies;
    $scope.selectedTechnology = technologies[0];
    var getSamples = function (component, selectedTechnology) {
        var componentSamples = [];
        for (var i = 0; i < samples.length; i++) {
            var sample = samples[i];
            if (sample.component == component &&
                ((sample.sourceCodeFiles && sample.sourceCodeFiles[selectedTechnology.name]) ||
                    (sample.sourceCodeUrls && sample.sourceCodeUrls[selectedTechnology.name])))
                componentSamples.push(sample);
        }
        return componentSamples;
    };
    var getComponents = function (selectedTechnology) {
        var components = [];
        for (var i = 0; i < samples.length; i++) {
            var sample = samples[i];
            var component = sample.component;
            if (components.indexOf(component) < 0 &&
                ((sample.sourceCodeFiles && sample.sourceCodeFiles[selectedTechnology.name]) ||
                    (sample.sourceCodeUrls && sample.sourceCodeUrls[selectedTechnology.name])))
                components.push(component);
        }
        return components;
    };
    var selectSample = function (sample) {
        $scope.selectedSample = sample;
        $scope.run();
    };
    var selectComponent = function (component) {
        var firstComponentSample;
        for (var i = 0; i < samples.length; i++) {
            var sample = samples[i];
            if (sample.component == component) {
                if (sample.feature == $scope.selectedSample.feature && sample.sourceCodeFiles[$scope.selectedTechnology.name]) {
                    selectSample(sample);
                    return;
                }
                if (!firstComponentSample && sample.sourceCodeFiles[$scope.selectedTechnology.name])
                    firstComponentSample = sample;
            }
        }
        selectSample(firstComponentSample);
    };
    $scope.selectTechnology = function (technology) {
        if (technology == $scope.selectedTechnology)
            return;
        if (technology.url) {
            $window.open(technology.url, technology.name);
            return;
        }
        $scope.selectedTechnology = technology;
        var selectedSample = $scope.selectedSample;
        var selectedComponent = selectedSample.component;
        var selectedFeature = selectedSample.feature;
        if (getComponents(technology).indexOf(selectedComponent) < 0)
            selectComponent(selectedComponent = components[0]);
        var componentSamples = getSamples(selectedComponent, technology);
        var featureSampleFound = false;
        for (var i = 0; i < componentSamples.length; i++) {
            var sample = componentSamples[i];
            if (sample.feature == selectedFeature && sample.sourceCodeFiles && sample.sourceCodeFiles[technology.name]) {
                featureSampleFound = true;
                selectSample(sample);
                break;
            }
        }
        if (!featureSampleFound)
            selectSample(componentSamples[0]);
        $scope.run();
    };
    $scope.components = components;
    $scope.samples = samples;
    $scope.selectedSample = samples[0];
    $scope.getComponents = getComponents;
    $scope.getSamples = getSamples;
    $scope.selectSample = selectSample;
    $scope.selectComponent = selectComponent;
    $scope.getSourceCodeFiles = function (selectedSample, selectedTechnology) {
        return selectedSample.sourceCodeFiles ? selectedSample.sourceCodeFiles[selectedTechnology.name] : null;
    };
    $scope.selectedSourceCodeFile = null;
    $scope.selectedSourceCodeFileContents = null;
    $scope.selectSourceCodeFile = function (selectedSample, selectedTechnology, sourceCodeFile) {
        $scope.selectedSourceCodeFile = sourceCodeFile;
        $scope.selectedSourceCodeFileContents = '…';
        var sourceCodeFileUrl = 'Samples/' + selectedTechnology.name + '/' + selectedSample.component + '/' + selectedSample.feature + '/' + sourceCodeFile.replace('.aspx', '.aspx.txt').replace('.aspx.txt.cs', '.aspx.cs.txt').replace('.aspx.txt.vb', '.aspx.vb.txt');
        $http.get(sourceCodeFileUrl).then(function (response) {
            $scope.selectedSourceCodeFileContents = response.data;
        });
    };
    $scope.forceRun = false;
    $scope.run = function (allowRefreshing) {
        if (allowRefreshing && $scope.selectedSourceCodeFile == null) {
            var technology = $scope.selectedTechnology;
            $scope.selectedTechnology = null;
            $timeout(function () {
                $scope.selectedTechnology = technology;
            });
        }
        $scope.selectedSourceCodeFile = null;
        $scope.selectedSourceCodeFileContents = null;
    };
    $scope.getSampleUrl = function (selectedSample, selectedTechnology, selectedTheme) {
        return 'Samples/' + (selectedTechnology ? selectedTechnology.name : '') + '/' + selectedSample.component + '/' + selectedSample.feature + '/index.aspx?' + (selectedTheme ? selectedTheme : $scope.applyingTheme);
    };
    var pathIndex = initialSelection ? initialSelection.indexOf('/') : -1;
    if (pathIndex >= 0) {
        var selection1 = initialSelection.substr(0, pathIndex), selection2 = initialSelection.substr(pathIndex + 1);
        for (var i = 0; i < samples.length; i++) {
            var sample = samples[i];
            if (sample.component == selection1 && sample.feature == selection2) {
                selectSample(sample);
                break;
            }
        }
    }
    else if (initialSelection)
        selectComponent(initialSelection);
})
    .directive('dsSample', function ($timeout) {
    return {
        restrict: 'E',
        replace: true,
        bindToController: {
            html: '='
        },
        controller: function ($scope) {
        },
        controllerAs: 'dss',
        templateUrl: 'Templates/Sample.html'
    };
})
    .directive('dsSourceCode', function ($timeout) {
    return {
        restrict: 'E',
        replace: true,
        bindToController: {
            contents: '='
        },
        controller: function ($scope) {
        },
        controllerAs: 'dssc',
        templateUrl: 'Templates/SourceCode.html'
    };
});
$(document).ready(function () {
    var body = $(document).find('body');
    var shouldSyncSize = false;
    var syncSizeTimer = setInterval(function () {
        var sampleFrame = $('#sample-frame');
        var sampleBody = sampleFrame.contents().find('body');
        var bodyWidth = body.width(), sampleBodyWidth = sampleBody.width();
        if (sampleBodyWidth > bodyWidth)
            shouldSyncSize = true;
        if (shouldSyncSize && sampleBodyWidth != bodyWidth)
            sampleBody.width(bodyWidth + 'px');
        if (!shouldSyncSize)
            clearInterval(syncSizeTimer);
    }, 500);
});
