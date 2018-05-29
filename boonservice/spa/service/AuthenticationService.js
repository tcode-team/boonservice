(function () {
    'use strict';

    angular
        .module('app')
        .factory('AuthenticationService', AuthenticationService);

    AuthenticationService.$inject = ['$http', '$q', '$window', '$cookies', '$rootScope', 'config'];
    function AuthenticationService($http,$q,$window,$cookies,$rootScope,config) {
        var service = {};

        service.Login = Login;
        service.SetCredentials = SetCredentials;
        service.GetCredentials = GetCredentials;
        service.ClearCredentials = ClearCredentials;

        return service;

        function Login(username, password, callback) {

            //console.log('AuthenticationService => Login');
            var user = {
                username: username,
                password: password
            }            
            $http.post(config.api.url + '/user/auth', user).then(function (response) {                
                //console.log(response);
                var result = response.data;
                if (result.type === 'S') {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify({ username: username })); // token: result                                      
                }
                callback(result);
            });
        }        

        function SetCredentials(user) {
            var deferred = $q.defer()
            var authdata = Base64.encode(user.username + ':' + user.password);

            var userdetail = {};
            $http.get(config.api.url + '/user/get/' + user.username).then(function (res) {
                userdetail = res.data;
                $window.sessionStorage.setItem("USERID", userdetail.userid);
                $window.sessionStorage.setItem("USERNAME", userdetail.username);
                $window.sessionStorage.setItem("POSITION", userdetail.position);
                $window.sessionStorage.setItem("VENDOR_CODE", userdetail.vendor_code);
                $window.sessionStorage.setItem("FIRST_NAME", userdetail.first_name);
                $window.sessionStorage.setItem("LAST_NAME", userdetail.last_name);

                $rootScope.globals = {
                    currentUser: {
                        userid: userdetail.userid,
                        username: userdetail.username,
                        authdata: authdata
                    },
                    UserDetail: userdetail
                };

                // set default auth header for http requests
                $http.defaults.headers.common['Authorization'] = 'Basic ' + authdata;

                // store user details in globals cookie that keeps user logged in for 1 week (or until they logout)
                var cookieExp = new Date();
                cookieExp.setDate(cookieExp.getDate() + 1);
                $cookies.putObject('globals', $rootScope.globals, { expires: cookieExp, path: '/' + config.rootpath });

                deferred.resolve(userdetail);
            }, function (err) {
                deferred.reject(err);
            });

            return deferred.promise;
        }

        function GetCredentials() {
            var deferred = $q.defer()

            $rootScope.globals = $cookies.getObject('globals') || {};
            deferred.resolve($rootScope.globals);

            return deferred.promise;
        }

        function ClearCredentials() {
            $rootScope.globals = {};
            $cookies.remove('globals');
            $http.defaults.headers.common.Authorization = 'Basic';
        }
       
    }

    // Base64 encoding service used by AuthenticationService
    var Base64 = {

        keyStr: 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=',

        encode: function (input) {
            var output = "";
            var chr1, chr2, chr3 = "";
            var enc1, enc2, enc3, enc4 = "";
            var i = 0;

            do {
                chr1 = input.charCodeAt(i++);
                chr2 = input.charCodeAt(i++);
                chr3 = input.charCodeAt(i++);

                enc1 = chr1 >> 2;
                enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
                enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
                enc4 = chr3 & 63;

                if (isNaN(chr2)) {
                    enc3 = enc4 = 64;
                } else if (isNaN(chr3)) {
                    enc4 = 64;
                }

                output = output +
                this.keyStr.charAt(enc1) +
                this.keyStr.charAt(enc2) +
                this.keyStr.charAt(enc3) +
                this.keyStr.charAt(enc4);
                chr1 = chr2 = chr3 = "";
                enc1 = enc2 = enc3 = enc4 = "";
            } while (i < input.length);

            return output;
        },

        decode: function (input) {
            var output = "";
            var chr1, chr2, chr3 = "";
            var enc1, enc2, enc3, enc4 = "";
            var i = 0;

            // remove all characters that are not A-Z, a-z, 0-9, +, /, or =
            var base64test = /[^A-Za-z0-9\+\/\=]/g;
            if (base64test.exec(input)) {
                window.alert("There were invalid base64 characters in the input text.\n" +
                "Valid base64 characters are A-Z, a-z, 0-9, '+', '/',and '='\n" +
                "Expect errors in decoding.");
            }
            input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");

            do {
                enc1 = this.keyStr.indexOf(input.charAt(i++));
                enc2 = this.keyStr.indexOf(input.charAt(i++));
                enc3 = this.keyStr.indexOf(input.charAt(i++));
                enc4 = this.keyStr.indexOf(input.charAt(i++));

                chr1 = (enc1 << 2) | (enc2 >> 4);
                chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
                chr3 = ((enc3 & 3) << 6) | enc4;

                output = output + String.fromCharCode(chr1);

                if (enc3 !== 64) {
                    output = output + String.fromCharCode(chr2);
                }
                if (enc4 !== 64) {
                    output = output + String.fromCharCode(chr3);
                }

                chr1 = chr2 = chr3 = "";
                enc1 = enc2 = enc3 = enc4 = "";

            } while (i < input.length);

            return output;
        }
    };

})();