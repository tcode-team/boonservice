﻿(function () {
    'use strict';

    angular
        .module('app')
        .controller('RepairListController', RepairListController)

    RepairListController.$inject = ['$scope', '$location', 'RepairService'];
    function RepairListController($scope, $location, RepairService) {
        $scope.title = 'รายงานแจ้งซ่อม';

        $scope.user = JSON.parse(sessionStorage.getItem('userDetail'));

        $scope.selection = {
            status: 'ALL'
        };

        $scope.fn_search_list = function () {
            $scope.loading = true;
            var val = _.clone($scope.selection, true);
            if (val == undefined) {
                $scope.alert('โปรดระบุเงื่อนไขการค้นหา');
                $scope.loading = false;
                return;
            };
            if (val.repair_date_from !== undefined) val.repair_date_from = getFormattedDate(val.repair_date_from);
            if (val.repair_date_to !== undefined) val.repair_date_to = getFormattedDate(val.repair_date_to);
            RepairService.search_list(val).then(function (response) {
                if (response.status == '200') {
                    $scope.repairs = response.data;
                } else {
                    $scope.repairs = []
                    console.log(response);
                    $scope.alert(response.data.Message);
                };
                $scope.loading = false;
            });
        };

        $scope.LinkRepairForm = function (repair_code) {
            $location.path('/repairform/' + repair_code);
        };

        $scope.RepairStatusText = function (status) {
            return RepairService.RepairStatusText(status);
        }

        // alert function 
        $scope.alert = function (message) {
            new PNotify({
                text: message,
                confirm: {
                    confirm: true,
                    buttons: [
                        {
                            text: 'DISMISS',
                            addClass: 'btn btn-link',
                            click: function (notice) {
                                notice.remove();
                            }
                        },
                        null
                    ]
                },
                buttons: {
                    closer: false,
                    sticker: false
                },
                animate: {
                    animate: true,
                    in_class: 'slideInDown',
                    out_class: 'slideOutUp'
                },
                addclass: 'md multiline'
            });
        };

        function getFormattedDate(date) {
            try {
                var year = date.getFullYear();

                var month = (1 + date.getMonth()).toString();
                month = month.length > 1 ? month : '0' + month;

                var day = date.getDate().toString();
                day = day.length > 1 ? day : '0' + day;

                return day + '/' + month + '/' + year;
            }
            catch
            { return undefined; }
        };
    };

})();