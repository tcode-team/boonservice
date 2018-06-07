(function () {
    'use strict';

    angular
        .module('app', ['ngCookies', 'ngSanitize', 'ngTouch', 'ngAnimate', 'ngIdle', 'ui.router', 'angular-loading-bar'])
        .config(config)
        .factory('AuthInterceptor', AuthInterceptor);
        //.run(run);    

    AuthInterceptor.$inject = ['$log', '$state', '$q'];
    function AuthInterceptor($log, $state, $q) {
        $log.debug('$log is here to show you that this is a regular factory with injection');

        var authInterceptor = {
            request: function (config) {
                var accessToken = sessionStorage.getItem('accessToken');
                if (accessToken === null || accessToken === "undefined") {
                    $state.go("login");
                }
                else {
                    config.headers["Authorization"] = "bearer " + accessToken;
                }
                return config;
            },

            requestError: function (config) {
                $state.go("login");
                return config;
            },

            response: function (res) {
                return res;
            },

            responseError: function (res) {
                if (res.status === "401") {
                    $state.go("login");
                }
                if (res.status === "400") {
                    $state.go("login");
                }
                if (res.status === "403") {
                    $state.go("login");
                }
                if (res.status === "404") {
                    $state.go("login");
                }
                $q.reject(res)
                return res;
            }
        };

        return authInterceptor;
    };  

    /************************** config **************************/
    config.$inject = ['$stateProvider', '$urlRouterProvider', '$locationProvider', '$httpProvider', '$qProvider', 'cfpLoadingBarProvider'];
    function config($stateProvider, $urlRouterProvider, $locationProvider, $httpProvider, $qProvider, cfpLoadingBarProvider) {
        $urlRouterProvider.otherwise('/home');
        $locationProvider.hashPrefix('');  

        $stateProvider
            .state('default', {
                url: '/',
                templateUrl: 'spa/view/home.html',
                controller: 'HomeController'
            })
            .state('home', {
                url: '/home',
                templateUrl: 'spa/view/home.html',
                controller: 'HomeController'
            })
            .state('login', {
                url: '/login',
                templateUrl: 'spa/view/login.html',
                controller: 'LoginController'
            });  

        //Authorize check
        $httpProvider.interceptors.push('AuthInterceptor');

        $qProvider.errorOnUnhandledRejections(false);   

        //loading bar
        cfpLoadingBarProvider.includeSpinner = true;
    }
    /************************** config **************************/

    /************************** run **************************/
    //run.$inject = ['$rootScope', '$location', '$cookies', '$http', '$window', 'Idle'];
    //function run($rootScope, $location, $cookies, $http, $window, Idle) { 
    //    //// keep user logged in after page refresh
    //    //$rootScope.globals = $cookies.getObject('globals') || {};
    //    //if ($rootScope.globals.currentUser) {
    //    //    $http.defaults.headers.common['Authorization'] = 'Basic ' + $rootScope.globals.currentUser.authdata;
    //    //}

    //    //$rootScope.$on('$locationChangeStart', function (event, next, current) {
    //    //    // redirect to login page if not logged in and trying to access a restricted page    
    //    //    var restrictedPage = $.inArray($location.path(), ['/login']) === -1;
    //    //    var loggedIn = $rootScope.globals.currentUser;
    //    //    console.log(loggedIn);
    //    //    //console.log(restrictedPage + ' ' + !loggedIn);
    //    //    if (restrictedPage && !loggedIn) {                
    //    //        //$window.location.href = '/login';
    //    //        $location.path('/login');
    //    //    }
    //    //});   

    //    // start watching when the app runs. also starts the Keepalive service by default.
    //    Idle.watch();

    //    //check online/offline
    //    $rootScope.online = navigator.onLine;
    //    $window.addEventListener("offline", function () {
    //        $rootScope.$apply(function () {
    //            $rootScope.online = false;
    //        });
    //    }, false);
    //    $window.addEventListener("online", function () {
    //        $rootScope.$apply(function () {
    //            $rootScope.online = true;
    //        });
    //    }, false);
    //}
    /************************** run **************************/    

})();