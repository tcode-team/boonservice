﻿(function () {
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

    HomeController.$inject = ['$scope', '$http', 'UserService'];
    function HomeController($scope, $http, UserService) {        

        //get user detail
        $scope.userName = sessionStorage.getItem('userName');
        UserService.userdetail($scope.userName).then(function (resp) {
            sessionStorage.setItem('userDetail', JSON.stringify(resp.data));
            $scope.user = resp.data;
        });
        //$scope.user = JSON.parse(sessionStorage.getItem('userDetail'));

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
            window.location.href = '/login';
        }
        
    };

})();