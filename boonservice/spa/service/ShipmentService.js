(function () {
    'use strict';

    angular
        .module('app')
        .factory('ShipmentService', ShipmentService);

    ShipmentService.$inject = ['$http', '$q', 'config'];
    function ShipmentService($http, $q, config) {
        var service = {};

        service.identity = identity;
        service.search_shipmentplan = search_shipmentplan;
        service.search_shipmentsum = search_shipmentsum;
        
        return service;

        function search_shipmentplan(selection) {
            var response = $http({
                url: config.api.url + 'shipment/shipmentplan',
                method: 'POST',
                data: selection
            });
            return response;  
        }       

        function search_shipmentsum(selection) {
            var response = $http({
                url: config.api.url + 'shipment/shipmentsummary',
                method: 'POST',
                data: selection
            });
            return response;  
        }

        function identity() {
            var response = $http({
                url: config.api.url + '/afs_car_identity_card/get',
                method: 'GET'
            });
            return response;  
        };
    }

})();