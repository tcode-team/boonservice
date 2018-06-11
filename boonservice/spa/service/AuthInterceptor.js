(function () {
    'use strict';

    angular
        .module('app')
        .factory('AuthInterceptor', AuthInterceptor);

    AuthInterceptor.$inject = ['$location', '$q'];
    function AuthInterceptor($location, $q) {
        var authInterceptor = {
            request: function (config) {
                var accessToken = sessionStorage.getItem('accessToken');
                if (accessToken === null || accessToken === "undefined") {
                    $location.path('/login');
                }
                else {
                    config.headers["Authorization"] = "bearer " + accessToken;
                }
                return config;
            },

            requestError: function (config) {
                $location.path('/login');
                return config;
            },

            response: function (res) {
                return res;
            },

            responseError: function (res) {
                if (res.status === "401") {
                    $location.path('/login');
                }
                if (res.status === "400") {
                    $location.path('/login');
                }
                if (res.status === "403") {
                    $location.path('/login');
                }
                if (res.status === "404") {
                    $location.path('/login');
                }
                $q.reject(res)
                return res;
            }
        };

        return authInterceptor;
    };  

})();