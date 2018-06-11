(function () {
    'use strict';

    angular
        .module('app')
        .controller('HomeController', HomeController)
        .filter('trustAsResourceUrl', ['$sce', function ($sce) {
            return function (val) {
                var url = $sce.trustAsResourceUrl(val);
                return url;
            };
        }])

    HomeController.$inject = ['$scope', '$http', '$location', 'UserService'];
    function HomeController($scope, $http, $location, UserService) {        
        
        //get user detail
        $scope.user = JSON.parse(sessionStorage.getItem('userDetail'));
        if ($scope.user == undefined || $scope.user || null) {
            $scope.login = true;
        } else {
            $scope.login = false;
        }

        $scope.MenuClick = function (menuname) {
            $scope.navactive = menuname;
            $location.path('/' + menuname);
        }

        $scope.logout = function () {
            $scope.isAuthenticated = false;
            angular.forEach(sessionStorage, function (item, key) {
                sessionStorage.removeItem(key);
            });
            $location.path('/login');
        }
        
    };

})();