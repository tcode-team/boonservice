(function () {
    'use strict';

    angular
        .module('app')
        .controller('ShipmentSummaryController', ShipmentSummaryController)

    ShipmentSummaryController.$inject = ['$scope', '$location', 'ShipmentService'];
    function ShipmentSummaryController($scope, $location, ShipmentService) {
        $scope.title = 'รายงานเอกสารค่าเที่ยวสรุปบัญชี';
        $scope.selection = {};
        $scope.shipmentsum = [];
        $scope.user = JSON.parse(sessionStorage.getItem('userDetail'));

        $scope.showModal = false;
        $scope.buttonClicked = "";
        $scope.toggleModal = function (btnClicked) {
            $scope.buttonClicked = btnClicked;
            $scope.showModal = !$scope.showModal;
        }; 

        ShipmentService.identity().then(function(response) {
            $scope.identities = response.data;
        });

        $scope.selected_identity = function (identity) {
            $scope.selection.identity_id = identity.PEOPLE_ID;  
            $scope.selection.identity_name = identity.NAME;
        }

        $scope.fn_preview = function () {
            $scope.loading = true;
            var val = _.clone($scope.selection, true);
            if (val == undefined) {
                $scope.alert('โปรดระบุเงื่อนไขการค้นหา');
                $scope.loading = false;
                return;
            }
            if (val.identity_id === undefined || val.identity_id === '' || val.identity_id === null) {
                $scope.alert('โปรดระบุ พนักงาน');
                $scope.loading = false;
                return;
            };
            if (val.transport_month === undefined || val.transport_month === '' || val.transport_month === null) {
                $scope.alert('โปรดระบุ เดือน/ปี');
                $scope.loading = false;
                return;
            };
            val.transport_month = getFormattedDate(val.transport_month);
            ShipmentService.search_shipmentsum(val).then(function (response) {
                if (response.status == '200') {
                    $scope.shipmentsum = response.data;
                } else {
                    $scope.shipmentsum = []
                    $scope.alert(response.data.Message);
                };
                $scope.loading = false;
            });
        }

        $scope.fn_clear = function() {
            $scope.selection = {};
            $scope.shipmentsum = [];
        };

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