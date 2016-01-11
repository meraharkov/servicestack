/*global window*/
(function (window, angular) {
    'use strict';

    angular
        .module('Authentication')
        .controller('LoginController',
            ['$scope', '$rootScope', '$location', 'authenticationService', 'auth', 'store', '$http', '$window', 'userService',
            function ($scope, $rootScope, $location, authenticationService, auth, store, $http, $window, userService) {
                // reset login status
                authenticationService.ClearCredentials();
                var lock = new Auth0Lock('VJxksG8qRBAyp6SuE58jGWAMQMYgZQca', 'authhornet.eu.auth0.com');

                $scope.auth = auth;
                $scope.loggedIn = auth.isAuthenticated;
                $scope.userProfile = null;

                $scope.hashtag = "football";
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

                $scope.Auth0 = function () {

                    //lock.show(function (err, profile, token) {
                    //    if (err) {
                    //        // Error callback
                    //        alert('There was an error');
                    //    } else {

                    //        $location.path('/home');
                    //        // Save the JWT token.
                    //       // localStorage.setItem('userToken', token);

                    //        // Save the profile
                    //       // $scope.userProfile = profile;
                    //    }
                    //});

                   //link with options  https://auth0.com/docs/identityproviders
                    lock.show({
                        callbackURL: 'http://localhost:10050/auth/auth0',
                        responseType: 'code',
                        connections: ['facebook', 'twitter', 'google-oauth2', 'vkontakte', 'github'],
                        icon: 'icon.png',
                        authParams: {
                            scope: 'openid name email',
                            state: "http://localhost:10050/home.html#/home",
                        },
                        //focusInput: false,
                        //popup: true,
                    }
                    //function (err, profile, token) {
                    //    alert(err);
                    //}
                    );


                    //lock.showSignin({
                    //    callbackURL: 'http://localhost:10050/auth/auth0',
                    //    responseType: 'code',
                    //    authParams: {
                    //        scope: 'openid offline_access name email'
                    //    }
                    //}, function (err, profile, id_token, access_token, state, refresh_token) {
                    //    // store refresh_token
                    //});


                    //lock.show({
                    //    callbackURL: 'http://localhost:10050/auth/auth0',
                    //    responseType: 'code',
                    //    ReferrerUrl:'http://localhost:10050/home.html#/home',
                    //    authParams: {
                    //        scope: 'openid name email',
                    //        state: 'http://localhost:10050/home.html#/home'
                    //    },
                    //});

                    //lock.showSignup({
                    //    callbackURL: 'http://localhost:10050/auth/auth0',
                    //    responseType: 'code',

                    //}, function (err, profile, token) {
                    //    if (err) {
                    //        // Error callback
                    //        console.log("There was an error");
                    //        alert("There was an error logging in");
                    //    } else {
                    //        localStorage.setItem('myToken', token);

                    //        $('#login_funct').hide();
                    //    }
                    //});

                    // $location.path('/home');

                    //$scope.$apply(function () {
                    //     $location.path("/home");
                    //});


                    //lock.show({
                    //    callbackURL: 'http://localhost:10050/auth/auth0',
                    //    responseType: 'code',
                    //    authParams: {
                    //        scope: 'openid name email',
                    //   }
                    //});
                }

                $scope.loginAuth0 = function () {

                    auth.signin({
                        callbackURL: 'http://localhost:10050/auth/auth0',
                        signupLink: "http://localhost:10050/home.html#/home",
                        responseType: 'code',
                        authParams: {
                            scope: 'openid name email',
                        }
                    }
                        //, function (profile, token) {
                        //    // Success callback
                        //    store.set('profile', profile);
                        //    store.set('token', token);
                        //    $location.path('/home');
                        //}, function() {
                        //    // Error callback
                        //    alert("error");
                        //}
                    );
                }
            }]);

}(window, window.angular));

