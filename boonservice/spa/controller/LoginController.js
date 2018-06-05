(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginController', LoginController)

    LoginController.$inject = ['$scope', '$state','LoginService'];
    function LoginController($scope, $state, LoginService) {        

        $scope.login = function() {
            $scope.user = {
                grant_type: 'password',
                username: $scope.userName,
                password: $scope.password
            };
            if ($scope.userName === "" || $scope.password === "") {
                $scope.isAuthenticated = false;
                $state.go("login");
            }
            else {
                var promise = LoginService.Login($scope.user);

                promise.then(function (resp) {
                    if (resp.data !== null) {
                        if (resp.data.error === "invalid_grant") {
                            $scope.isAuthenticated = false;
                            $state.go("login");
                        }
                        else {
                            $scope.isAuthenticated = true;
                            sessionStorage.setItem('userName', $scope.userName)
                            sessionStorage.setItem('accessToken', resp.data.access_token)
                            sessionStorage.setItem('userName', $scope.userName)
                            $state.go("home");
                        }
                    } else {
                        $scope.isAuthenticated = false;
                        $state.go("login");
                    }
                }, function () {
                    $scope.isAuthenticated = false;
                    $state.go("login");
                }, function () {
                    console.log("C");
                    $scope.isAuthenticated = false;
                    $state.go("login");
                });

            } 
        }

    };

})();