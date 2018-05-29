(function () {
    'use strict';

    angular
        .module('app', ['ngRoute', 'ngCookies', 'ngSanitize', 'ngTouch', 'ngAnimate', 'ngIdle'])
        .config(config)
        .run(run);    

    /************************** config **************************/
    config.$inject = ['$routeProvider'];
    function config($routeProvider) {
        $routeProvider
            .when('/home', {
                templateUrl: 'spa/view/home.html',
                controller: 'HomeController',
            })
            .when('/login', {
                templateUrl: 'spa/view/login.html',
                controller: 'LoginController',
            })
            .otherwise({ redirectTo: '/home' });
    }
    /************************** config **************************/

    /************************** run **************************/
    run.$inject = ['$rootScope', '$location', '$cookies', '$http', '$window', 'Idle'];
    function run($rootScope, $location, $cookies, $http, $window, Idle) { 
        // keep user logged in after page refresh
        $rootScope.globals = $cookies.getObject('globals') || {};
        if ($rootScope.globals.currentUser) {
            $http.defaults.headers.common['Authorization'] = 'Basic ' + $rootScope.globals.currentUser.authdata;
        }

        $rootScope.$on('$locationChangeStart', function (event, next, current) {
            // redirect to login page if not logged in and trying to access a restricted page    
            var restrictedPage = $.inArray($location.path(), ['/login']) === -1;
            var loggedIn = $rootScope.globals.currentUser;
            console.log(loggedIn);
            //console.log(restrictedPage + ' ' + !loggedIn);
            if (restrictedPage && !loggedIn) {                
                //$window.location.href = '/login';
                $location.path('/login');
            }
        });   

        // start watching when the app runs. also starts the Keepalive service by default.
        Idle.watch();

        //check online/offline
        $rootScope.online = navigator.onLine;
        $window.addEventListener("offline", function () {
            $rootScope.$apply(function () {
                $rootScope.online = false;
            });
        }, false);
        $window.addEventListener("online", function () {
            $rootScope.$apply(function () {
                $rootScope.online = true;
            });
        }, false);
    }
    /************************** run **************************/    

})();