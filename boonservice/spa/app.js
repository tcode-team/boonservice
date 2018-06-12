(function () {
    'use strict';

    angular
        .module('app', ['ngRoute', 'ngCookies', 'ngSanitize', 'ngTouch', 'ngAnimate', 'ngIdle', 'ui.router', 'angular-loading-bar', 'oc.lazyLoad'])
        .config(config);
        //.run(run);    

    /************************** config **************************/
    config.$inject = ['$stateProvider', '$routeProvider', '$urlRouterProvider', '$locationProvider', '$httpProvider', '$qProvider', 'cfpLoadingBarProvider'];
    function config($stateProvider, $routeProvider, $urlRouterProvider, $locationProvider, $httpProvider, $qProvider, cfpLoadingBarProvider) {
        // Resolver to load controller, service, directive
        var resolveController = function (dependencies) {
            return {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load(dependencies);
                }]
            }
        };

        $routeProvider
            .when('/home', {
                templateUrl: 'spa/view/home.html',
                controller: 'HomeController'
            })
            .when('/login', {
                templateUrl: 'spa/view/login.html',
                controller: 'LoginController',
                resolve: resolveController('spa/controller/LoginController.js')
            })
            .when('/blank', {
                templateUrl: 'spa/view/blank.html',
                controller: 'BlankController',
                resolve: resolveController('spa/controller/BlankController.js')
            })
            .when('/profile', {
                templateUrl: 'spa/view/profile.html',
                controller: 'ProfileController',
                resolve: resolveController('spa/controller/ProfileController.js')
            })
            .when('/shipmentlist', {
                templateUrl: 'spa/view/shipmentlist.html',
                controller: 'ShipmentListController',
                resolve: resolveController('spa/controller/ShipmentListController.js')
            })
            .when('/shipmentedit', {
                templateUrl: 'spa/view/shipmentedit.html',
                controller: 'ShipmentEditController',
                resolve: resolveController('spa/controller/ShipmentEditController.js')
            })
            .when('/shipmentedit/:param1', { 
                templateUrl: 'spa/view/shipmentedit.html',
                controller: 'ShipmentEditController',
                resolve: resolveController('spa/controller/ShipmentEditController.js')
            })
            .when('/repairlist', {
                templateUrl: 'spa/view/repairlist.html',
                controller: 'RepairListController',
                resolve: resolveController('spa/controller/RepairListController.js')
            })
            .otherwise({ redirectTo: '/home' });

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