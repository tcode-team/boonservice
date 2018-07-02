(function () {
    'use strict';

    angular
        .module('app', ['ngRoute', 'ngCookies', 'ngSanitize', 'ngTouch', 'ngMask',
            'ngAnimate', 'ngIdle', 'ui.router', 'angular-loading-bar', 'oc.lazyLoad',
            'naif.base64'])
        .config(config);
        //.run(run);

    /************************** config **************************/
    config.$inject = ['$stateProvider', '$routeProvider', '$urlRouterProvider', '$locationProvider', '$httpProvider', '$qProvider', 'cfpLoadingBarProvider'];
    function config($stateProvider, $routeProvider, $urlRouterProvider, $locationProvider, $httpProvider, $qProvider, cfpLoadingBarProvider) {
        // Resolver to load controller, service, directive
        var resolveController = function (dependencies) {
            return {
                loadMyCtrl: ['$ocLazyLoad', 'config', function ($ocLazyLoad, config) {
                    return $ocLazyLoad.load('spa/controller/' + dependencies + '.js?v=' + config.version);
                }]
            }
        };

        $routeProvider
            .when('/home', {
                templateUrl: 'spa/view/home.html?v=' + config.version,
                controller: 'HomeController'
            })
            .when('/login', {
                templateUrl: 'spa/view/login.html?v=' + config.version,
                controller: 'LoginController',
                resolve: resolveController('LoginController')
            })
            .when('/blank', {
                templateUrl: 'spa/view/blank.html?v=' + config.version,
                controller: 'BlankController',
                resolve: resolveController('BlankController')
            })
            .when('/profile', {
                templateUrl: 'spa/view/profile.html?v=' + config.version,
                controller: 'ProfileController',
                resolve: resolveController('ProfileController')
            })
            .when('/shipmentlist', {
                templateUrl: 'spa/view/shipmentlist.html?v=' + config.version,
                controller: 'ShipmentListController',
                resolve: resolveController('ShipmentListController')
            })
            .when('/shipmentlist/:param1', {
                templateUrl: 'spa/view/shipmentlist.html?v=' + config.version,
                controller: 'ShipmentListController',
                resolve: resolveController('ShipmentListController')
            })
            .when('/shipmentedit/:param1', {
                templateUrl: 'spa/view/shipmentedit.html?v=' + config.version,
                controller: 'ShipmentEditController',
                resolve: resolveController('ShipmentEditController')
            })
            .when('/repairlist', {
                templateUrl: 'spa/view/repairlist.html?v=' + config.version,
                controller: 'RepairListController',
                resolve: resolveController('RepairListController')
            })
            .when('/repairform', {
                templateUrl: 'spa/view/repairform.html?v=' + config.version,
                controller: 'RepairFormController',
                resolve: resolveController('RepairFormController')
            })
            .when('/repairform/:param1', {
                templateUrl: 'spa/view/repairform.html?v=' + config.version,
                controller: 'RepairFormController',
                resolve: resolveController('RepairFormController')
            })
            .when('/shipmentplan', {
                templateUrl: 'spa/view/shipmentplan.html?v=' + config.version,
                controller: 'ShipmentPlanController',
                resolve: resolveController('ShipmentPlanController')
            })
            .when('/shipmentsummary', {
                templateUrl: 'spa/view/shipmentsummary.html?v=' + config.version,
                controller: 'ShipmentSummaryController',
                resolve: resolveController('ShipmentSummaryController')
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