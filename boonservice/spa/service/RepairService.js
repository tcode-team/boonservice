(function () {
    'use strict';

    angular
        .module('app')
        .factory('RepairService', RepairService);

    RepairService.$inject = ['$http', '$q', 'config'];
    function RepairService($http, $q, config) {
        var service = {};

        service.searchso = searchso;

        return service;

        function searchso(so_number) {
            var data = {
                sonumber: so_number
            }
            var response = $http({
                url: config.api.url + 'repair/searchso',
                method: 'POST',
                data: data
            });
            return response;  
        }        
       
    }
})();