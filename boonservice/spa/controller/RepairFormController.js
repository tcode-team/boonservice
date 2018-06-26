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
        $scope.repairapp = {};
        $scope.repairraw = [];

        //Get Authority group
        RepairService.authority($scope.user.userid).then(function (response) {
            if (response.status == "200") {
                if (response.data !== undefined) {
                    switch ('Y') {
                        case response.data.SALE_FLAG:
                            $scope.authority_group = "SALE";
                            break;
                        case response.data.AFTERSALE_FLAG:
                            $scope.authority_group = "AFTERSALE";
                            break;
                        case response.data.PURCHASE_FLAG:
                            $scope.authority_group = "PURCHASE";
                            break;
                        default:
                            $scope.authority_group = undefined;
                    };
                }
            } else {
                $scope.alert("เกิดข้อผิดพลาด โปรดลองใหม่อีกครั้ง");
            }; 
        });
        
        // checkk route parameter 
        if ($routeParams.param1 === undefined || $routeParams.param1 === null) {
            //new แจ้งซ่อม
            $('#iSoNumber').focus();
        } else {
            //edit แจ้งซ่อม
            $scope.loading = true;
            RepairService.repair_detail($routeParams.param1).then(function (response) {
                $scope.loading = false;
                if (response.status == "200") {
                    $scope.repairh = response.data.header;
                    $scope.repairi = response.data.items;
                    if (response.data.appoint !== undefined) {
                        response.data.appoint.appointment_date = new Date(response.data.appoint.appointment_date);
                        response.data.appoint.target_date = new Date(response.data.appoint.target_date);
                    };
                    $scope.repairapp = response.data.appoint;
                    $scope.repairraw = response.data.raws;
                } else {
                    $scope.alert($scope.repairh.so_number + ': ' + response.data.Message);
                }
            }, function () {
                $scope.loading = false;
                $scope.alert("เกิดข้อผิดพลาด โปรดลองใหม่อีกครั้ง");
            });
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
        };

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

        //Popup Image
        $scope.popImage = function (base) {
            $.magnificPopup.open({
                items: {
                    src: $('<div class="white-popup"><img src="' + base + '" style="width:100%" ></div>'), // Dynamically created element
                    type: 'inline'
                },
                type: 'image'
            });
        }

        //เพิ่มรายการ อะไหล่
        $scope.AddRawItem = function () {
            $scope.repairraw.push({
                raw_name: '',
                status: 'มีอะไหล่'
            })
        };

        //ลบรายการ อะไหล่
        $scope.RemoveRawItem = function (index) {
            $scope.repairraw.splice(index, 1);
        }

        //Before save data
        $scope.fn_before_save = function () {
            switch ($scope.authority_group) {
                case "SALE":
                    // valid data
                    var rows = _.filter($scope.repairi, function (v) { return v.select });
                    if (rows.length === 0) {
                        $scope.alert('โปรดเลือกรายการที่ต้องการบันทึก');
                        return;
                    }
                    $('#confirmSave').modal('show');
                    break;
                case "AFTERSALE":
                    // valid data
                    if ($scope.repairapp.appointment_date == undefined || $scope.repairapp.appointment_date == '') {
                        $scope.alert('โปรดระบุ วันที่นัดหมายลูกค้า');
                        return;
                    };
                    if ($scope.repairapp.appointment_time == undefined || $scope.repairapp.appointment_time == '') {
                        $scope.alert('โปรดระบุ เวลานัดหมายลูกค้า');
                        return;
                    };
                    if ($scope.repairapp.target_date == undefined || $scope.repairapp.target_date == '') {
                        $scope.alert('โปรดระบุ วันที่นัดซ่อมเสร็จ');
                        return;
                    }
                    var now = new Date();
                    if ($scope.repairapp.appointment_date <= now) {
                        $scope.alert('โปรดระบุ วันที่นัดหมายลูกค้า ต้องมากกว่าวันที่ปัจจุบัน');
                        return;
                    }
                    if ($scope.repairapp.target_date < $scope.repairapp.appointment_date) {
                        $scope.alert('โปรดระบุ วันที่นัดซ่อมเสร็จ ต้องมากกว่าหรือเท่ากับวันที่นัดหมายลูกค้า');
                        return;
                    }
                    //check raw item
                    angular.forEach($scope.repairraw, function (value, key) {
                        console.log(value);
                    });
                    console.log('after loop');

                    $('#confirmSave').modal('show');
                    break;
                case "PURCHASE":

                    $('#confirmSave').modal('show');
                    break;
            };
        };

        //Save data
        $scope.fn_save = function () {
            $('#confirmSave').modal('hide');
            $scope.loading = true;

            $scope.repairh.created_by = $scope.user.userid;
            $scope.repairh.update_by = $scope.user.userid;

            switch ($scope.authority_group) {
                case "SALE":
                    //บันทึกแจ้งซ่อม ข้อมูลลูกค้าและรายการ
                    RepairService.save($scope.repairh, _.filter($scope.repairi, function (i) { return i.select })).then(function (response) {
                        if (response.status == "200") {
                            $scope.repairh = response.data.header;
                            $scope.repairi = response.data.items;
                        } else {
                            $scope.alert(response.data.Message);
                        }
                        $scope.loading = false;
                    });
                    break;
                case "AFTERSALE":
                    //บันทึกแจ้งซ่อม นัดหมายและรายการอะไหล่
                    RepairService.saveaftersale($scope.repairh, $scope.repairapp, $scope.repairraw).then(function (response) {
                        if (response.status == "200") {
                            $scope.repairh = response.data.header;
                            if (response.data.appoint !== undefined) {
                                response.data.appoint.appointment_date = new Date(response.data.appoint.appointment_date);
                                response.data.appoint.target_date = new Date(response.data.appoint.target_date);
                            };
                            $scope.repairapp = response.data.appoint;
                            $scope.repairraw = response.data.raws;
                        } else {
                            $scope.alert(response.data.Message);
                        }
                        $scope.loading = false;
                    });
                    break;
                case "PURCHASE":

                    break;
            };
        }

        //Convert Status
        $scope.fn_status_desc = function (status) {
            if (status == "NEW") return "New"
            else if (status == "PREPARE") return "จัดเตรียมคิวงานและอะไหล่"
            else if (status == "PROCCESS") return "ทีมช่างดำเนินการ"
            else if (status == "COMPLETE") return "Complete"
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