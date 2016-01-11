/*global window*/
(function (window, angular) {
    'use strict';

    angular
        .module(window.appName)
        .factory('authenticationRepository', ['serverApi', function (serverApi) {
            return {
                login: function (authdata, callback) {

                    $.ajax({
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader("Authorization", 'Basic ' + authdata);
                        },
                        type: 'Post',
                        contentType: 'application/json',
                        cache: false,
                        async: false,
                        dataType: "json",
                        url: window.domain + "/auth/basic",
                        success: function (data, textStatus, jqXHR) {
                            alert(textStatus);
                            callback({ success: true });
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            alert("False");
                            callback({ success: false });
                        }
                    });
                },

                register: function (registerModel, callback) {

                    serverApi
                        .post("/json/reply/Register", registerModel)
                        .success(function (users) {

                            callback({ success: true }, "Registration was successful");
                        })
                        .error(function (message) {

                            callback({ success: false }, message);
                        });

                    //$.ajax({
                    //    type: 'Post',
                    //    contentType: 'application/json',
                    //    data: JSON.stringify(userModel),
                    //    dataType: "json",
                    //    url: "http://localhost:63957/json/reply/Register",
                    //    success: function (data, textStatus, jqXHR) {

                    //        callback({ success: true }, "Registration was successful");
                    //    },
                    //    error: function (xhr, textStatus, errorThrown) {
                    //       var responceObj =  JSON.parse(xhr.responseText);
                    //       callback({ success: false }, responceObj.ResponseStatus.Message);
                    //    }
                    //});
                },

                loginWithFacebook: function (fbModel, callback) {

                    serverApi
                       .post("FacebookLogin", fbModel)
                       .success(function (users) {
                           callback({ success: true }, users);
                       })
                       .error(function (message) {

                           callback({ success: false }, message);
                       });
                }
            };
        }]);

}(window, window.angular));