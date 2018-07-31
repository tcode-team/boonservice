(function () {
    'use strict';

    angular
        .module('app')
        .controller('ShipmentPlanController', ShipmentPlanController)

    ShipmentPlanController.$inject = ['$scope', '$location', '$timeout', 'Excel', 'ShipmentService'];
    function ShipmentPlanController($scope, $location, $timeout, Excel, ShipmentService) {
        $scope.title = 'ตารางงานจัดส่ง';
        $scope.transports = [];
        $scope.user = JSON.parse(sessionStorage.getItem('userDetail'));

        $scope.fn_search_list = function () {
            $scope.loading = true;
            //console.log($scope.selection);    
            var val = _.clone($scope.selection, true);
            if (val == undefined) {
                $scope.alert('โปรดระบุเงื่อนไขการค้นหา');
                $scope.loading = false;
                return;
            }
            if ((val.transport_date === undefined || val.transport_date === null || val.transport_date === '') &&
                (val.SONumber === undefined || val.SONumber === null || val.SONumber === '')){
                $scope.alert('โปรดระบุวันที่ส่ง หรือ SO No');
                $scope.loading = false;
                return;
            }
            val.transport_date = getFormattedDate(val.transport_date);
            ShipmentService.search_shipmentplan(val).then(function (response) {
                if (response.status == '200') {
                    $scope.transports = response.data;
                } else {
                    $scope.transports = []
                    $scope.alert(response.data.Message);
                };
                $scope.loading = false;
            });
        }

        //Clear screen
        $scope.fn_clear = function () {
            $scope.selection = {};
            $scope.transports = [];
        }

        //Export excel
        $scope.fn_exportToExcel = function (tableId) { // ex: '#my-table'

            var table = $(tableId).clone();
            //console.log(table.html());
            table.find('[style*="display: none"]').remove();
            //console.log(table.html());

            $scope.exportHref = Excel.tableToExcel(tableId, 'sheet name');
            $timeout(function () { location.href = $scope.exportHref; }, 100); // trigger download
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
        }

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
        }

    };

})();