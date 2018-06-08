(function () {
    'use strict';

    angular
        .module('app')
        .controller('leftNavigatorController', leftNavigatorController)

    leftNavigatorController.$inject = ['$scope', '$http', '$rootScope'];
    function leftNavigatorController($scope, $http, $rootScope) {        
        
        $scope.navactive = $rootScope.navactive;
        $scope.nav_active = function(name) {
            $rootScope.navactive = name;
            console.log($rootScope.navactive);
        };
    };

})();