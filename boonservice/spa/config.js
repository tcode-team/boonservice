(function () {
    'use strict';

    angular
        .module('app')
        .factory('config', config);

    config.$inject = [];
    function config() {

        var config = {};

        config.version = '1';
        config.api = {
            url: 'http://dotnet64-test.boonthavorn.com/boon.api/'
        }
        
        return config;
        
    };

})();