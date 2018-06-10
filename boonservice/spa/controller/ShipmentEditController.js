(function () {
    'use strict';

    angular
        .module('app')
        .controller('ShipmentEditController', ShipmentEditController)

    ShipmentListController.$inject = ['$scope', '$http', '$q', 'config'];
    function ShipmentListController($scope, $http, $q, config) {          
        $scope.title = 'ปรับปรุงค่าขนส่ง BLF';         


        $scope.user = JSON.parse(sessionStorage.getItem('userDetail'));

        $scope.init = function () { 
            $scope.ShipEditErr = ""; 
        }


    };

})();