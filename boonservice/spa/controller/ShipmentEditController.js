(function () {
    'use strict';

    angular
        .module('app')
        .controller('ShipmentEditController', ShipmentEditController)

    ShipmentEditController.$inject = ['$scope', '$http', '$location', '$q', 'config'];
    function ShipmentEditController($scope, $http, $location, $q, config) {          
        $scope.title = 'ปรับปรุงค่าขนส่ง BLF';         

        $scope.user = JSON.parse(sessionStorage.getItem('userDetail'));

        $scope.init = function () { 
            $scope.ShipEditErr = ""; 
        }

        $scope.back = function () {
            $location.path('/shipmentlist');
        }


    };

})();