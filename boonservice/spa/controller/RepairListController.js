(function () {
    'use strict';

    angular
        .module('app')
        .controller('RepairListController', RepairListController)

    RepairListController.$inject = ['$scope', '$http'];
    function RepairListController($scope, $http) {        
        $scope.title = 'รายงานแจ้งซ่อม';

        $scope.user = JSON.parse(sessionStorage.getItem('userDetail'));
    };

})();