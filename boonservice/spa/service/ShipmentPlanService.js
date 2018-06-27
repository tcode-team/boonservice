(function () {
    'use strict';

    angular
        .module('app')
        .factory('ShipmentPlanService', ShipmentPlanService);

    ShipmentPlanService.$inject = ['$http', '$q', 'config'];
    function ShipmentPlanService($http, $q, config) {
        var service = {};

        service.search_shipmentplan = search_shipmentplan;
        
        return service;

        function search_shipmentplan(selection) {
            var response = $http({
                url: config.api.url + 'shipment/shipmentplan',
                method: 'POST',
                data: selection
            });
            return response;  
        }       
    }

})();