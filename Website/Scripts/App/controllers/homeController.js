/*global window*/
(function (window, angular) {
    'use strict';

     angular.module('Home')
            .controller('HomeController',
                ['$scope', 'userService', 'auth', 'store','$http',
                function ($scope, userService, auth, store, $http) {

                    $scope.UsersProfile = [];

                    userService.GetUsersProfile(function (response, usersProfile) {

                        if (response.success) {
                            $scope.authStatus = "You are logged";
                            $scope.UsersProfile.push(usersProfile);
                        } else {
                            $scope.authStatus = response.message + " Not authorized";
                            $scope.dataLoading = false;
                        }
                    });
 
                    $scope.GetUsersProfile = function() {
                       
                        $.ajax({
                            url: '/SinlePage/GetUserProfile',
                            dataType: 'json',
                            contentType: 'application/json',
                            type: 'POST',
                            success: function (usersProfile) {

                                $scope.$apply(function() {
                                    $scope.UsersProfile.push(usersProfile);
                                });

                            },
                            error: function (xhr, text, error) {

                            }
                        });

                    }

                    $scope.GetUsersProfileStatic = function () {

                        $.ajax({
                            url: '/SinlePage/GetUserProfileStatic',
                            dataType: 'json',
                            contentType: 'application/json',
                            type: 'POST',
                            success: function (usersProfile) {

                                $scope.$apply(function () {
                                    $scope.UsersProfile.push(usersProfile);
                                });

                            },
                            error: function (xhr, text, error) {

                            }
                        });
                    }

                    $scope.login = function() {
                        
                        $.ajax({
                            url: '/SinlePage/Login',
                            dataType: 'json',
                            contentType: 'application/json',
                            type: 'Get',
                            success: function (response) {

                            },
                            error: function (xhr, text, error) {

                            }
                        });
                    }

                    $scope.logout = function() {
                        auth.signout();
                       
                        $.ajax({
                            url: '/SinlePage/Logout',
                            dataType: 'json',
                            contentType: 'application/json',
                            type: 'POST',
                            success: function (response) {
                               
                            },
                            error: function (xhr, text, error) {
                                
                            }
                        });
                    }
                }]);
}(window, window.angular));