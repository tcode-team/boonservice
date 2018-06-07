(function () {
    'use strict';

    angular
        .module('app')
        .factory('UserService', UserService);

    UserService.$inject = ['$http', '$q', 'config'];
    function UserService($http, $q, config) {
        var service = {};

        service.userdetail = userdetail;

        return service;

        function userdetail(username) {
            var response = $http({
                url: config.api.url + 'user/detail?username=' + username,
                method: 'POST'
            });
            return response;  
        }        
       
    }
})();