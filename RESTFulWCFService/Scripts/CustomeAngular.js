/// <reference path="angular.min.js" />

//var myapp = angular.module("MyModule", []);
//var mycontroller = function ($scope) { $scope.message = "Hello Angular Js"; };
//myapp.controller("mycontroller", function ($scope) { $scope.message = "Hello Angular Js"; });

var myapp = angular.module("MyModule", []).controller("MyController", function ($scope) { $scope.message = "Hello Angular Js"; });