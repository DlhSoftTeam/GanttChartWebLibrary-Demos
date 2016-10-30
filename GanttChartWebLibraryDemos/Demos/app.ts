declare var angular;

var queryString = window.location.search;
var initialSelection = queryString ? queryString.substr(1).replace('-', ' ') : null;

interface Sample {
    component: string;
    feature: string;
    title: string;
    description: string;
    sourceCodeFiles?: { [key: string]: string[] };
    sourceCodeUrls?: { [key: string]: string };
}

angular.module('Demos', [])
    .controller('MainController', ($scope, $http, $timeout) => {
        var components = ['GanttChartView'];
        var samples = <Sample[]>[
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
                component: 'GanttChartView', feature: 'AssignmentsTemplate', title: 'Assignments template (resource icons)', description: 'Shows how you can customize assignments template and show resource icons in the chart area',
                sourceCodeFiles: {
                    'CSharp': ['Index.aspx', 'Index.aspx.cs', 'app.css']
                }
            }
        ];
        var themes = ['Default', 'Generic-bright', 'Generic-blue', 'DlhSoft-gray', 'Purple-green', 'Steel-blue', 'Dark-black', 'Cyan-green', 'Blue-navy', 'Orange-brown', 'Teal-green', 'Purple-beige', 'Gray-blue', 'Aero'];
        $scope.themes = themes;
        $scope.selectedTheme = themes[1];
        $scope.selectTheme = (theme) => {
            if (theme == $scope.selectedTheme)
                return;
            $scope.applyingTheme = theme;
            $scope.selectedTheme = null;
            $timeout(() => {
                $scope.selectedTheme = theme;
                $scope.applyingTheme = null;
                $scope.run();
            });
        };
        var technologies = [{ name: 'CSharp', title: 'C#' }, { name: 'VisualBasic', title: 'Visual Basic®' }];
        $scope.technologies = technologies;
        $scope.selectedTechnology = technologies[0];
        var getSamples = (component, selectedTechnology) => {
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
        var getComponents = (selectedTechnology) => {
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
        var selectSample = (sample) => {
            $scope.selectedSample = sample;
            $scope.run();
        };
        var selectComponent = (component) => {
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
        $scope.selectTechnology = (technology) => {
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
        $scope.getSourceCodeFiles = (selectedSample, selectedTechnology) => {
            return selectedSample.sourceCodeFiles ? selectedSample.sourceCodeFiles[selectedTechnology.name] : null;
        };
        $scope.selectedSourceCodeFile = null;
        $scope.selectedSourceCodeFileContents = null;
        $scope.selectSourceCodeFile = (selectedSample, selectedTechnology, sourceCodeFile) => {
            $scope.selectedSourceCodeFile = sourceCodeFile;
            $scope.selectedSourceCodeFileContents = '…';
            var sourceCodeFileUrl = 'Samples/' + selectedTechnology.name + '/' + selectedSample.component + '/' + selectedSample.feature + '/' + sourceCodeFile.replace('.aspx', '.aspx.txt').replace('.aspx.txt.cs', '.aspx.cs.txt').replace('.aspx.txt.vb', '.aspx.vb.txt');
            $http.get(sourceCodeFileUrl).then((response) => {
                $scope.selectedSourceCodeFileContents = response.data;
            });
        };
        $scope.forceRun = false;
        $scope.run = (allowRefreshing) => {
            if (allowRefreshing && $scope.selectedSourceCodeFile == null) {
                var technology = $scope.selectedTechnology;
                $scope.selectedTechnology = null;
                $timeout(() => {
                    $scope.selectedTechnology = technology;
                });
            }
            $scope.selectedSourceCodeFile = null;
            $scope.selectedSourceCodeFileContents = null;
        };
        $scope.getSampleUrl = (selectedSample, selectedTechnology, selectedTheme) => {
            return 'Samples/' + (selectedTechnology ? selectedTechnology.name : '') + '/' + selectedSample.component + '/' + selectedSample.feature + '/index.aspx?' + (selectedTheme ? selectedTheme : $scope.applyingTheme);
        };
        if (initialSelection)
            selectComponent(initialSelection);
    })
    .directive('dsSample', ($timeout) => {
        return {
            restrict: 'E',
            replace: true,
            bindToController: {
                html: '='
            },
            controller: ($scope) => {
            },
            controllerAs: 'dss',
            templateUrl: 'Templates/Sample.html'
        };
    })
    .directive('dsSourceCode', ($timeout) => {
        return {
            restrict: 'E',
            replace: true,
            bindToController: {
                contents: '='
            },
            controller: ($scope) => {
            },
            controllerAs: 'dssc',
            templateUrl: 'Templates/SourceCode.html'
        };
    });
