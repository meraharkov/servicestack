/*global window*/
(function (window, angular) {
    'use strict';

    angular
        .module('Authentication')
        .controller('LoginController',
            ['$scope', '$rootScope', '$location', 'authenticationService', 'auth', 'store', '$http',
            function ($scope, $rootScope, $location, authenticationService, auth, store, $http) {
                // reset login status
                authenticationService.ClearCredentials();

                $scope.auth = auth;
                $scope.loggedIn = auth.isAuthenticated;

                var lock = new Auth0Lock('VJxksG8qRBAyp6SuE58jGWAMQMYgZQca', 'authhornet.eu.auth0.com');

                $scope.hashtag = "football";

                $scope.login = function () {
                    $scope.dataLoading = true;
                    authenticationService.Login($scope.username, $scope.password, function (response) {
                        if (response.success) {
                            authenticationService.SetCredentials($scope.username, $scope.password);
                            $location.path('/home');
                        } else {
                            $scope.error = response.message;
                            $scope.dataLoading = false;
                        }
                    });
                };



                $scope.loginAuth = function () {

                    auth.signin({
                        callbackURL: 'http://localhost:10050/auth/auth0',
                        signupLink: "http://localhost:10050/home.html#/home",
                        responseType: 'code',
                        connections: ['facebook', 'twitter', 'google-oauth2', 'vkontakte', 'github'],
                        authParams: {
                            scope: 'openid name email',
                        }
                    });
                }


            }]);

}(window, window.angular));

