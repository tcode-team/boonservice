(function () {
    'use strict';

    angular
        .module('app')
        .controller('BlankController', BlankController)

    BlankController.$inject = ['$scope', '$http', 'config'];
    function BlankController($scope, $http, config) {        
        $scope.title = 'Blank Page';

        $scope.user = JSON.parse(sessionStorage.getItem('userDetail'));

        $http.get(config.api.url + '/afs_car_identity_card/get').then(function (response) {
            console.log(response);
            $scope.drivers = response.data;
        });

        //Modal //////////////////////////////////////////
        $scope.showModal = false; //ต้องมี
        $scope.driver_id = "";
        $scope.buttonClicked = ""; //ต้องมี
        $scope.toggleModal = function (btnClicked) { //ต้องมี
            $scope.buttonClicked = btnClicked;
            $scope.showModal = !$scope.showModal;
        };
        $scope.selected_driver = function (driver_id) { //ต้องมี
            $scope.driver_id = driver_id;
        }
        //Modal //////////////////////////////////////////

    };

})();