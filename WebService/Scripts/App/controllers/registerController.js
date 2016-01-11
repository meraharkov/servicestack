/*global window*/
(function (window, angular) {
    'use strict';

    angular
        .module('Register')
        .controller('RegisterController',
            ['$scope', '$rootScope', '$location', 'AuthenticationService',
                function ($scope, $rootScope, $location, AuthenticationService) {
                    var vm = this;
                    $scope.userModel = {
                        UserName: "",
                        Email: "",
                        Password: ""
                    };

                    $scope.registerUser = function () {

                        vm.dataLoading = true;
                        AuthenticationService.RegisterUser($scope.userModel, function (resultRegistration, message) {
                            if (resultRegistration.success) {
                                alert(message);
                                $location.path("/login");
                            } else {
                                alert(message);
                                vm.dataLoading = false;
                            }
                        });
                    }
                }
            ]);

}(window, window.angular));
