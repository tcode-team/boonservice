(function () {
    'use strict';

    angular
        .module('app')
        .controller('RepairFormController', RepairFormController)

    RepairFormController.$inject = ['$scope', '$http'];
    function RepairFormController($scope, $http) {        
        $scope.title = 'เพิ่มงานซ่อม SO';

        $scope.user = JSON.parse(sessionStorage.getItem('userDetail'));

    };

})();