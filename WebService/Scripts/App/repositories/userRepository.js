/*global window*/
(function (window, angular) {
    'use strict';

    angular
        .module(window.appName)
        .factory('userRepository', ['serverApi', function (serverApi) {
            return {
                GetUsersProfile: function (callback) {

                    serverApi
                        .get("/userProfile")
                        .success(function (userProfile) {

                            callback({ success: true }, userProfile);
                        })
                        .error(function (message) {

                            callback({ success: false, message: message });
                        });
                }
            };
        }]);

}(window, window.angular));