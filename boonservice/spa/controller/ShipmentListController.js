(function () {
    'use strict';

    angular
        .module('app')
        .controller('ShipmentListController', ShipmentListController)

    ShipmentListController.$inject = ['$scope', '$http'];
    function ShipmentListController($scope, $http) {        
        $scope.title = 'คำนวณค่าขนส่ง BLF';          
    };

})();