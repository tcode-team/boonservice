(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginController', LoginController)

    LoginController.$inject = ['$scope', '$rootScope','$cookies','$http','$location'];
    function LoginController($scope,$rootScope,$cookies,$http,$location) {        
        $scope.init = function () {

        };

        $scope.login = function() {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem('currentUser', JSON.stringify({ username: 'chalutpun' })); // token: result   
                       
            $rootScope.globals = {
                currentUser: {
                    userid: 1234,
                    username: 'CHALUTPUN'
                }
            };

            // store user details in globals cookie that keeps user logged in for 1 week (or until they logout)
            var cookieExp = new Date();
            cookieExp.setDate(cookieExp.getDate() + 1);
            $cookies.putObject('globals', $rootScope.globals, { expires: cookieExp, path: '/' });
            
            $location.path('/home');
        }
    };

})();