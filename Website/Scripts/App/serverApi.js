/*global window*/
(function (window, angular) {
    'use strict';

    angular
        .module(window.appName)
        .factory('serverApi', ['$http', '$log',
            function ($http, $log) {
                var request = function (method, resource, entity, parameters) {
                    var response = {},
                        successFn = function () { },
                        errorFn = function () { };

                    response.success = function(fn) {
                        successFn = fn;

                        var callBack = {};
                        callBack.error = function(fnc) {
                            errorFn = fnc;
                        };
                        return callBack;
                    };


                    $http({
                        method: method,
                        url: window.domain + resource,
                        params: parameters,
                        data: JSON.stringify(entity),
                        headers: { 'Content-Type': 'application/json' }
                    })
                        .success(function (data, status, headers, config) {
                            successFn(data, status, headers, config);
                        })
                        .error(function (data, status, header, config) {
                           // var responceObj = JSON.parse(data.responseText);
                            var message = data.ResponseStatus.Message;
                            errorFn(message);

                            $log.warn(data, status, header, config);
                        });

                    return response;
                };

                return {
                    get: function (resource, parameters) {
                        return request('GET', resource, null, parameters);
                    },

                    post: function (resource, entity) {
                        return request('POST', resource, entity, null);
                    },

                    put: function (resource, entity) {
                        return request('PUT', resource, entity, null);
                    },

                    remove: function (resource, entity) {
                        return request('DELETE', resource, entity, null);
                    }
                };
            }]);

}(window, window.angular));