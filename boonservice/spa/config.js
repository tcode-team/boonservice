(function () {
    'use strict';

    angular
        .module('app')
        .factory('config', config);

    config.$inject = [];
    function config() {

        var config = {};

        config.rootpath = 'qas';
        config.api = {
            url: 'http://bss.boonthavorn.com:8080/ords/bsssan'
        }
        
        return config;
        
    };

})();