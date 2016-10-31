var queryString = window.location.search;
var initialSelection = queryString ? queryString.substr(1).replace('-', ' ') : null;
angular.module('Demos', [])
    .controller('MainController', function ($scope, $http, $timeout) {
    var components = ['GanttChartView'];
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
            component: 'GanttChartView', feature: 'Statuses', title: 'Status columns (including color indicator)', description: 'Shows how to add supplemental custom columns for showing task statuses, such as To do, In progress, Behind schedule, and Completed',
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
            component: 'GanttChartView', feature: 'MoveUpDown', title: 'Move up-down (hierarchical moving)', description: 'Shows how you can allow the end user to move items up and down without breaking the hierarchy',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
            }
        },
        {
            component: 'GanttChartView', feature: 'AssignmentsTemplate', title: 'Assignments template (resource icons)', description: 'Shows how you can customize assignments template and show resource icons in the chart area',
            sourceCodeFiles: {
                'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
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
    var technologies = [{ name: 'CSharp', title: 'C#' }, { name: 'VisualBasic', title: 'Visual Basic®' }];
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
    if (initialSelection)
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
//# sourceMappingURL=app.js.map