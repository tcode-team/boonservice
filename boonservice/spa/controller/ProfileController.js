(function () {
    'use strict';

    angular
        .module('app')
        .controller('ProfileController', ProfileController)

    ProfileController.$inject = ['$scope', '$http'];
    function ProfileController($scope, $http) {        
        $scope.title = 'Profile';

        $scope.user = JSON.parse(sessionStorage.getItem('userDetail'));
    };

})();