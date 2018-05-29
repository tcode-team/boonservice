(function () {
    'use strict';

    angular
        .module('app')
        .controller('BlankController', BlankController)

    BlankController.$inject = ['$scope', '$http'];
    function BlankController($scope, $http) {        
        $scope.title = 'Blank Page';

        //
    };

})();