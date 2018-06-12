(function () {
    'use strict';

    angular
        .module('app')
        .controller('ShipmentEditController', ShipmentEditController)

    ShipmentEditController.$inject = ['$scope', '$http', '$location', 'config','$routeParams'];
    function ShipmentEditController($scope, $http, $location, config, $routeParams) {          
        $scope.title = 'ปรับปรุงค่าขนส่ง BLF';         

        $scope.user = JSON.parse(sessionStorage.getItem('userDetail'));

        $scope.init = function () {
            $scope.Get_CarriesPoint();
            $scope.ShipEditErr = "";  
            console.log('routeParams.ShipmentNo ' + $routeParams.param1);
            $scope.ShipmentNo = $routeParams.param1;
            console.log('scope.ShipmentNo '+ $scope.ShipmentNo);
        }

        $scope.back = function () {
            $location.path('/shipmentlist');
        }

        $scope.Get_CarriesPoint = function () {
            $http({
                url: config.api.url + 'afs_carries_point/get',
                method: 'GET'
            }).then(function (response) {
                console.log(response);
                $scope.CarriesPoint = response.data;
            });
        }

    };

})();