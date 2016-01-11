/*global window*/
(function (window, angular) {
    'use strict';

    angular.module('Authentication')
        .factory('userService',
        [  'userRepository',
            function (userRepository) {

                var service = {};

                service.GetUsersProfile = function (callback) {

                    userRepository.GetUsersProfile(function (responce, userProfile) {

                        callback(responce, userProfile);
                    });

                    // $.support.cors = true;
                    //$.ajax({
                    //    type: 'Post',
                    //    contentType: 'application/json',
                    //    cache: false,
                    //    async: false,
                    //    // crossDomain: true,
                    //    data: JSON.stringify({ Id: userRequest.Id, Email: userRequest.Email }),
                    //    dataType: "json",
                    //    url: "http://localhost:63957/user",
                    //    success: function(data, textStatus, jqXHR) {
                    //        alert(data.Result);
                    //    },
                    //    error: function(xhr, textStatus, errorThrown) {
                    //        alert("False");
                    //    }

                    //});
                };

                return service;
            }
        ]);


}(window, window.angular));