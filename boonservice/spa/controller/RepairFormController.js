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
        $scope.ActiveTabName = 'itemtab'
        $scope.repairh = {};
        $scope.repairi = [];
        $scope.repairp = [];

        // checkk route parameter 
        if ($routeParams.param1 === undefined || $routeParams.param1 === null) {
            //new แจ้งซ่อม
            $('#iSoNumber').focus();
        } else {
            //edit แจ้งซ่อม

        }

        //Get master
        RepairService.get_repair_item_type().then(function (response) {
            $scope.repair_item_types = response.data;
        });

        //Select all items
        $scope.fn_select_all = function (val) {
            angular.forEach($scope.repairi, function (r) {
                r.select = val;
                if (val == false) {
                    r.images = [];
                }
            });
        }

        //Select item
        $scope.fn_item_select = function (item) {
            if (!item.select) {
                item.images = [];
            }
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
                    $scope.repairh.status = "NEW";
                    $scope.repairi = response.data.items;
                } else {
                    $scope.alert($scope.repairh.so_number + ': ' + response.data.Message);
                }
            }, function () {
                $scope.loading = false;
                $scope.alert("เกิดข้อผิดพลาด โปรดลองใหม่อีกครั้ง");
            });
        };

        //Browse Image
        $scope.currentSo = {};
        $scope.browseImg = function (item) {
            $scope.currentSo = item;
            $('#files').click();
        }

        //Upload images
        $scope.files = [];
        $scope.onLoadend = function (e, reader, file, fileList, fileOjects, fileObj) {
            if ($scope.currentSo.images === undefined || $scope.currentSo.images == null) {
                $scope.currentSo.images = [];
            }
            $scope.currentSo.images.push({
                mode: 'new',
                filename: fileObj.filename,
                base64: 'data:' + fileObj.filetype + ';base64,' + fileObj.base64
            });
        };
        $scope.errorHandler = function (event, reader, file, fileList, fileObjs, object) {
            $scope.alert("เกิดข้อผิดพลาดกับไฟล์ที่เลือก: " + file.name);
            reader.abort();
        };

        //Before save data
        $scope.fn_before_save = function () {
            var rows = _.filter($scope.repairi, function (v) { return v.select });
            if (rows.length === 0) {
                $scope.alert('โปรดเลือกรายการที่ต้องการบันทึก');
                return;
            }
            $('#confirmSave').modal('show');
        };

        //Save data
        $scope.fn_save = function () {
            $('#confirmSave').modal('hide');
            $scope.loading = true;

            $scope.repairh.created_by = $scope.user.userid;
            $scope.repairh.update_by = $scope.user.userid;

            RepairService.save($scope.repairh, _.filter($scope.repairi, function (i) { return i.select })).then(function (response) {
                if (response.status == "200") {
                    $scope.repairh = response.data.header;
                    $scope.repairi = response.data.items;
                } else {
                    $scope.alert(response.data.Message);
                }
                $scope.loading = false;
            });
        }

        //Convert Status
        $scope.fn_status_desc = function (status) {
            if (status == "NEW") return "สร้างใหม่"
            else if (status == "PREPARE") return "จัดเตรียมคิวงานและอะไหล่"
            else if (status == "PROCCESS") return "ทีมช่างดำเนินการ"
            else if (status == "COMPLETE") return "สำเร็จ"
            else return "สร้างใหม่";
        }

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