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
        };

        // Start Hub
        // Reference the auto-generated proxy for the hub.
        $scope.RepairHub = $.connection.RepairHub;
        $scope.initHub = function () {
            // Create a function that the hub can call back to display messages.
            $scope.RepairHub.client.updateUsersOnlineCount = function (count) {
                $scope.$apply(function () {
                    // Add the message to the page.
                    $('#usersCount').text(count);
                });
            };

            // List
            $scope.userActivate.client.UsersList = function (users) {
                $scope.$apply(function () {
                    $scope.users = users;
                })
            }
            
            $.connection.hub.start();
        }
        $scope.initHub();
        // End Hub

        $scope.MenuClick = function (menuname) {
            $scope.navactive = menuname;
            $location.path('/' + menuname);
        };

        $scope.logout = function () {
            $scope.isAuthenticated = false;
            angular.forEach(sessionStorage, function (item, key) {
                sessionStorage.removeItem(key);
            });
            $location.path('/login');
        };
        
    };

})();