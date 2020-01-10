
let college = angular.module('collegeApp', []);

college.controller('CourseController', function ($scope, $http) {

    $scope.statistics = {};
    // Obtain statistics for Courses  
    $http(
    {
        method: 'GET',
        url: '/Course/Statistics'
    }).then(function successCallback(response)
    {
        if (response.data) {
            $scope.statistics = response.data;
        }
    }, function errorCallback(response) {
        console.log(response);
    });

});

college.controller('SubjectsController', function ($scope, $http) {

    $scope.statistics = {};
    $scope.students = [];
    $scope.currentSubjectID = -1;
    $scope.currentSubjectName = '';
    // Obtain statistics for Subjects  
    $http(
        {
            method: 'GET',
            url: '/Subject/Statistics'
        }).then(function successCallback(response) {
            if (response.data) {
                $scope.statistics = response.data;
            }
        }, function errorCallback(response) {
            console.log(response);
        });

    var setStudents = function () {
        console.log("Settings students");
        for (var prop in $scope.statistics) {
            if ($scope.currentSubjectID == $scope.statistics[prop].ID) {
                $scope.currentSubjectName = $scope.statistics[prop].Name;
                $scope.students = $scope.statistics[prop].Students;
            }                     
        }
    }

    $scope.getGradeColor = function (grade) {
        if (grade < 10)
            return 'insufficient';
        else if (grade <= 12)
            return 'suficient';
        else if (grade <= 14)
            return 'suficient-plus';
        else if (grade <= 16)
            return 'average';
        else if (grade <= 18)
            return 'better-average';
        else
            return 'excellent';    
    }


    $scope.$watch('students');
    $scope.$watch('currentSubjectID', function () {
        setStudents();
    });
});


college.directive('showTeacherInfo', function ($http) {
    return {
        restrict: 'E',
        transclude: true,
        scope: {
            'subject': '@subjectId'
        },
        template: '<div> <label> {{teacher.Name}} </label> <h5> Salary: {{teacher.Salary}} </h5> </div>',
        link: function (scope, element, attrs) {
            console.log('Retrieving teacher information ...');
            scope.teacher = {}; 

            var getTeacherInfo = function () {
                if (!isNaN(scope.subject)) {
                    $http(
                        {
                            method: 'GET',
                            url: '/Subject/GetTeacherOfSubject',
                            params: { id: scope.subject }
                        }).then(function successCallback(response) {
                            if (response.data) {
                                scope.teacher = response.data;
                            }
                        }, function errorCallback(response) {
                            console.log(response);
                        });
                }
            };            

            attrs.$observe('subjectId', function () {
                getTeacherInfo();
            });

            scope.$watch('teacher', function () {
                console.log(scope.teacher);
            });
        }
    }
});
