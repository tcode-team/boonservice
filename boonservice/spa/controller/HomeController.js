(function () {
    'use strict';

    angular
        .module('app')
        .controller('HomeController', HomeController)

    HomeController.$inject = ['$scope', '$http', '$rootScope', '$location', 'Idle'];
    function HomeController($scope, $http, $rootScope, $location, Idle) {        
        $scope.init = function () {

        };

        $scope.UrlChange = function (url) {
            
        };

        $scope.logout = function () {
            // reset login status
            //AuthenticationService.ClearCredentials();
            $location.path('/login');
        };
        
    };

})();