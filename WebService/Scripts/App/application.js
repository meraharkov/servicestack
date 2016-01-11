/*global window*/
(function (window, angular) {
    'use strict';

    window.appName = window.appName || 'WebsiteApp';
    window.domain = "http://localhost:10050/";

    // declare modules
    angular.module('Authentication', []);
    angular.module('Home', []);
    angular.module('Register', []);
    angular.module("Validation", []);

    angular.module(window.appName , [
        'Authentication',
        'Home',
        'Register',
        'Validation',
        'ngRoute',
        'ngCookies',
        'auth0',
        'angular-storage',
        'angular-jwt'
    ])
    .config(['$routeProvider', 'authProvider', 'jwtInterceptorProvider', '$httpProvider',
        function ($routeProvider, authProvider, jwtInterceptorProvider, $httpProvider) {

        $routeProvider
             .when('/register', {
                 controller: 'RegisterController',
                 templateUrl: '/Scripts/App/views/register.html'
             })
            .when('/login', {
                controller: 'LoginController',
                templateUrl: '/Scripts/App/views/login.html',
                hideMenus: true
            })
            .when('/home', {
                controller: 'HomeController',
                templateUrl: '/Scripts/App/views/home.html'
            })
           .otherwise({ redirectTo: '/login' });

            authProvider.init({
                domain: 'authhornet.eu.auth0.com',
                clientID: 'VJxksG8qRBAyp6SuE58jGWAMQMYgZQca',
               // callbackURL: 'http://localhost:10050/auth/auth0',
                callbackOnLocationHash: true
            });

        // We're annotating this function so that the `store` is injected correctly when this file is minified
        jwtInterceptorProvider.tokenGetter = ['store', function (store) {
            // Return the saved token
            return store.get('token');
        }];

        $httpProvider.interceptors.push('jwtInterceptor');

        authProvider.on('loginSuccess', function ($location, profilePromise, idToken, store) {
            profilePromise.then(function (profile) {
                store.set('profile', profile);
                store.set('token', idToken);
            });
            alert("loginSuccess");
            $location.path('/home');
        });

        authProvider.on('authenticated', function ($location) {
            alert("authenticated");
            $location.path('/home');
            // This is after a refresh of the page
            // If the user is still authenticated, you get this event
        });

        authProvider.on('loginFailure', function ($location, error) {
            alert("loginFailure");
            $location.path('/login');
        });

    }])
    .run(['$rootScope', '$location', '$cookieStore', '$http', 'auth', 'jwtHelper', 'store',
        function ($rootScope, $location, $cookieStore, $http, auth, jwtHelper, store) {
            // keep user logged in after page refresh
            $rootScope.globals = $cookieStore.get('globals') || {};
            if ($rootScope.globals.currentUser) {
                $http.defaults.headers.common['Authorization'] = 'Basic ' + $rootScope.globals.currentUser.authdata; // jshint ignore:line
            }

            auth.hookEvents();

            $rootScope.$on('$locationChangeStart', function () {
                var token = store.get('token');
                if (token) {
                    if (!jwtHelper.isTokenExpired(token)) {
                        if (!auth.isAuthenticated) {
                            auth.authenticate(store.get('profile'), token);
                        }
                    } else {
                        // Either show the login page or use the refresh token to get a new idToken
                        $location.path('/');
                    }
                }
            });
        }]);


}(window, window.angular));