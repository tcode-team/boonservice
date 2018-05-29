(function () {
    'use strict';

    angular
        .module('app')
        .controller('ShipmentListController', ShipmentListController)

    BlankController.$inject = ['$scope', '$http'];
    function ShipmentListController($scope, $http) {        
        $scope.title = 'Shipment List Page'; 

    };

})();