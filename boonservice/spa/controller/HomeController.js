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

    HomeController.$inject = ['$scope', '$http', '$state'];
    function HomeController($scope, $http, $state) {        

        $scope.prgurl = 'http://localhost:56468/blank';

        $scope.init = function () {

        };

        $scope.UrlChange = function (url) {
            //url += 'userid=' + $scope.user.userid + '&username=' + $scope.user.username + '&position=' + $scope.user.position;
            //url += '&vendor_code=' + $scope.user.vendor_code + '&first_name=' + $scope.user.first_name + '&last_name=' + $scope.user.last_name;
            $scope.prgurl = url;
        };

        $scope.logout = function () {
            $scope.isAuthenticated = false;
            angular.forEach(sessionStorage, function (item, key) {
                sessionStorage.removeItem(key);
            });
            $state.go("login");
        }
        
    };

})();