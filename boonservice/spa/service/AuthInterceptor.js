(function () {
    'use strict';

    angular
        .module('app')
        .factory('AuthInterceptor', AuthInterceptor);

    AuthInterceptor.$inject = ['$log', '$state', '$location', '$q'];
    function AuthInterceptor($log, $state, $location, $q) {
        $log.debug('$log is here to show you that this is a regular factory with injection');

        var authInterceptor = {
            request: function (config) {
                var accessToken = sessionStorage.getItem('accessToken');
                if (accessToken === null || accessToken === "undefined") {
                    //$state.go("login");
                    $location.path('/login');
                    //window.location.href = 'login';
                }
                else {
                    config.headers["Authorization"] = "bearer " + accessToken;
                }
                return config;
            },

            requestError: function (config) {
                //$state.go("login");
                $location.path('/login');
                //window.location.href = 'login';
                return config;
            },

            response: function (res) {
                return res;
            },

            responseError: function (res) {
                if (res.status === "401") {
                    //$state.go("login");
                    $location.path('/login');
                    //window.location.href = 'login';
                }
                if (res.status === "400") {
                    //$state.go("login");
                    $location.path('/login');
                    //window.location.href = 'login';
                }
                if (res.status === "403") {
                    //$state.go("login");
                    $location.path('/login');
                    //window.location.href = 'login';
                }
                if (res.status === "404") {
                    //$state.go("login");
                    $location.path('/login');
                    //window.location.href = 'login';
                }
                $q.reject(res)
                return res;
            }
        };

        return authInterceptor;
    };  

})();