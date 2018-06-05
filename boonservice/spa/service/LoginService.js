(function () {
    'use strict';

    angular
        .module('app')
        .factory('LoginService', LoginService);

    LoginService.$inject = ['$http', '$q', 'config'];
    function LoginService($http, $q, config) {
        var service = {};

        service.Login = Login;

        return service;

        function Login(userData) {
            var response = $http({
                url: config.api.url + 'user/token',
                method: 'POST',
                data: $.param({
                    grant_type: userData.grant_type,
                    username: userData.username,
                    password: userData.password
                }),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                }
            });
            return response;  
        }        
       
    }
})();