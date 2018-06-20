(function () {
    'use strict';

    angular
        .module('app')
        .factory('RepairService', RepairService);

    RepairService.$inject = ['$http', '$q', 'config'];
    function RepairService($http, $q, config) {
        var service = {};

        service.get_repair_item_type = get_repair_item_type;
        service.search_list = search_list;
        service.searchso = searchso;
        service.save = save; 

        return service;

        function search_list(selection) {
            var response = $http({
                url: config.api.url + 'repair/search_list',
                method: 'POST',
                data: selection
            });
            return response;  
        }

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

        function get_repair_item_type() {
            var response = $http({
                url: config.api.url + 'afs_repair_item_type/get',
                method: 'GET'
            });
            return response;  
        }

        function save(header, items) {
            var response = $http({
                url: config.api.url + 'repair/save',
                method: 'POST',
                data: {
                    header: header,
                    items: items
                }
            });
            return response;  
        }
       
    }
})();