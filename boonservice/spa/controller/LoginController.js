(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginController', LoginController)

    LoginController.$inject = ['$scope', '$location', 'LoginService', 'UserService'];
    function LoginController($scope, $location, LoginService, UserService) {        
        $scope.loading = false;
       
        $scope.login = function() {
            if ($scope.userName === "" || $scope.userName === undefined || $scope.password === "" || $scope.password === undefined) {
                $scope.isAuthenticated = false;
                $scope.AuthenMessage = "ต้องระบุชื่อผู้ใช้ระบบ และรหัสผ่าน";
                return;
            }
            $scope.loading = true;
            $scope.user = {
                grant_type: 'password',
                username: $scope.userName,
                password: $scope.password
            };

            var promise = LoginService.Login($scope.user);
            promise.then(function (resp) {
                if (resp.data !== null) {
                    if (resp.data.error === "invalid_grant" || resp.data.error === "invalid_user") {
                        $scope.AuthenMessage = "ชื่อผู้ใช้ระบบ และรหัสผ่านไม่ถูกต้อง หรือไม่มีในระบบ";
                        $scope.login_again();
                    } else if (resp.data.error === "inactive_user") {
                        $scope.AuthenMessage = "ชื่อผู้ใช้ระบบ ถูกยกเลิกใช้งาน";
                        $scope.login_again();
                    } else {
                        $scope.isAuthenticated = true;
                        sessionStorage.setItem('userName', $scope.userName);
                        sessionStorage.setItem('accessToken', resp.data.access_token);

                        ////get user detail
                        UserService.userdetail($scope.userName).then(function (resp) {
                            sessionStorage.setItem('userDetail', JSON.stringify(resp.data));
                            $location.path('/home');
                        });
                    }
                } else {
                    $scope.AuthenMessage = "เกิดข้อผิดพลาดในการเชื่อมต่อ โปรดลองใหม่อีกครั้ง";
                    $scope.login_again();
                }
            }, function () {
                $scope.AuthenMessage = "เกิดข้อผิดพลาดในการเชื่อมต่อ โปรดลองใหม่อีกครั้ง";
                $scope.login_again();
            }, function () {
                $scope.AuthenMessage = "เกิดข้อผิดพลาดในการเชื่อมต่อ โปรดลองใหม่อีกครั้ง";
                $scope.login_again();
            });
        }

        $scope.login_again = function () {
            $scope.loading = false;
            $scope.isAuthenticated = false;
        }

    };

})();