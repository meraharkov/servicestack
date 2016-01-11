/*global window*/
(function (window, angular) {
    'use strict';

     angular.module('Home')
            .controller('HomeController',
                ['$scope', 'userService', 'auth', 'store', '$http', '$location',
                function ($scope, userService, auth, store, $http, $location) {

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


                    $scope.logout = function() {
                         
                        $.get("auth/logout", function(data) {
                            $location.path('/login');
                        });

                      //  auth.signout();
                    }
 
                }]);
}(window, window.angular));