(function () {
    'use strict';

    angular
        .module('app')
        .controller('RepairFormController', RepairFormController)

    RepairFormController.$inject = ['$scope', '$http', '$routeParams', '$location', 'RepairService'];
    function RepairFormController($scope, $http, $routeParams, $location, RepairService) {        
        $scope.title = 'เพิ่มงานซ่อม SO';
        $scope.user = JSON.parse(sessionStorage.getItem('userDetail'));

        //Global variable
        $scope.repairh = {};
        $scope.repairi = [];

        // checkk route parameter 
        if ($routeParams.param1 === undefined || $routeParams.param1 === null) {
            //new แจ้งซ่อม
            $('#iSoNumber').focus();
        } else {
            //edit แจ้งซ่อม

        }

        //Select all items
        $scope.fn_select_all = function (val) {
            angular.forEach($scope.repairi, function (r) {
                r.select = val;
            });
        }

        //Search Sale Order Number
        $scope.fn_search_so = function () {
            $scope.loading = true;

            $scope.selectall = false;
            $scope.repairi = [];
            RepairService.searchso($scope.repairh.so_number).then(function (response) {
                $scope.loading = false;
                if (response.status == "200") {
                    $scope.repairh = response.data.header;
                    $scope.repairi = response.data.items;
                } else {
                    $scope.alert($scope.repairh.so_number + ': ' + response.data.Message);
                }
            }, function () {
                $scope.loading = false;
                $scope.alert("เกิดข้อผิดพลาด โปรดลองใหม่อีกครั้ง");
            });
        };

        //Upload images
        $scope.files = [];
        $scope.onLoad = function (e, reader, file, fileList, fileOjects, fileObj) {
            //console.log(fileObj);
        };
        $scope.onLoadend = function (e, reader, file, fileList, fileOjects, fileObj) {
            console.log(fileObj);
        };
        $scope.errorHandler = function (event, reader, file, fileList, fileObjs, object) {
            $scope.alert("เกิดข้อผิดพลาดกับไฟล์ที่เลือก: " + file.name);
            reader.abort();
        };

        //Save data
        $scope.fn_save = function () {
            var rows = _.filter($scope.repairi, function (v) { return v.select });
            if (rows.length === 0) {
                $scope.alert('โปรดเลือกรายการที่ต้องการบันทึก');
                return;
            }
        };

        //clear screen
        $scope.fn_clear = function () {
            $('#confirmNew').modal('hide');
            $scope.selectall = false;
            $scope.repairh = {};
            $scope.repairi = [];
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
    };

})();