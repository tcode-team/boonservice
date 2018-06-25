(function () {
    'use strict';

    angular
        .module('app')
        .controller('ShipmentEditController', ShipmentEditController)

    ShipmentEditController.$inject = ['$scope', '$http', '$location', 'config','$routeParams'];
    function ShipmentEditController($scope, $http, $location, config, $routeParams) {          
        $scope.title = 'ปรับปรุงค่าขนส่ง BLF';         

        $scope.user = JSON.parse(sessionStorage.getItem('userDetail'));

        $scope.init = function () {
            $scope.loading = true;
            $scope.ShipEditErr = "";   
            $scope.RowPoint = 0;
            $scope.PointMax = 0;
            $scope.RowExpense = 0;
            $scope.ExpenseMax = 0;
            $scope.Driver_Amt1 = 0;
            $scope.Staff_Amt1 = 0;
            $scope.Driver_Amt2 = 0;
            $scope.Staff_Amt2 = 0;
            $scope.Staff_Count = 0;


           // $scope.PointList = [];
            $scope.ShipmentNo = $routeParams.param1; 
             
            $scope.Get_CarriesPoint();
            $scope.Get_TimeList();
            $scope.Get_ExpenseList();
            $scope.loading = false;
            $scope.ShipmentHead(); 
        }

        $scope.back = function () {
            $location.path('/shipmentlist');
        }


        //// Get Head
        $scope.ShipmentHead = function ()  {
            $scope.loading = true;
            console.log('loading' + $scope.loading);
            $scope.ShipEditErr = ""; 

            var textObject = JSON.stringify(""); 

            // console.log("text in " + textObject.indexOf("ShipmentNo")); 
            textObject = '{"forwarding":"1910"}';  
                         
            textObject = '{' + textObject.substring(1, textObject.length - 1) + ',"ShipmentNo":"' + $scope.ShipmentNo + '"}';
            //textObject = '{"ShipmentNo":"' + $scope.ShipmentNo + '"}';            

            console.log("textObjectX " + textObject);
            $http({
                url: config.api.url + 'shipment/search',
                method: 'POST',
                data: textObject 
            }).then(function (response) {
                console.log(response); 
                //$scope.ShipEditErr = response.statusText;
                $scope.loading = false;
                $scope.DocList = response.data;

                console.log(response.status); 
                    $scope.ShipmentNumber = $scope.DocList[0].shipment_number;
                    if ($scope.DocList[0].transport_date.length > 0) {
                        console.log("transport_date " + $scope.DocList[0].transport_date);
                        var s = $scope.DocList[0].transport_date.split('/');
                        //$scope.DocList[0].transport_date = new Date(s[1] + '/' + s[0] + '/' + s[2]);
                        $scope.DocList[0].transport_date = new Date(s[2] , s[1]-1 , s[0]);
                        console.log($scope.DocList[0].transport_date);
                         
                } 
                console.log('driver_id ' + $scope.DocList[0].driver_id);

                try {
                    if ($scope.DocList[0].shipment_carries.length > 0) {
                        for (var i = 0; i < $scope.DocList[0].shipment_carries.length; i++) {
                            if ($scope.DocList[0].shipment_carries[i].itemno > $scope.PointMax) {
                                $scope.PointMax = $scope.DocList[0].shipment_carries[i].itemno; 
                            }
                        }
                    }
                    if ($scope.DocList[0].shipment_expense.length > 0) {
                        for (var v = 0; v < $scope.DocList[0].shipment_expense.length; v++) {
                            if ($scope.DocList[0].shipment_expense[iv].itemno > $scope.ExpenseMax) {
                                $scope.ExpenseMax = $scope.DocList[0].shipment_expense[v].itemno;
                            }
                        }
                    }
                }
                catch{ }

                if (response.status !== 200) {
                    $scope.loading = false;
                    $scope.ShipEditErr = response.statusText;
                    //return undefined;
                }
                });
            console.log("GetHead " + $scope.DocList); 
            //console.log("GetHead " + $scope.DocList);
            return;
        }

        /// Get Head End -------------------------------------------------------------------------------------------

        /// Get Time List   CarriesPoint    ExpenseList
        $scope.Get_TimeList = function () {
            $scope.Time_List = [
                { time_desc: '8.00-9.00' },
                { time_desc: '9.00-10.00' },
                { time_desc: '10.00-11.00' },
                { time_desc: '11.00-12.00' },
                { time_desc: '12.00-13.00' },
                { time_desc: '13.00-14.00' },
                { time_desc: '14.00-15.00' },
                { time_desc: '15.00-16.00' },
                { time_desc: '16.00-17.00' }
            ];
        };


        $scope.Get_CarriesPoint = function () {
            $http({
                url: config.api.url + 'afs_carries_point/get',
                method: 'GET'
            }).then(function (response) {
                console.log(response);
                $scope.CarriesPoint = response.data;
            });
        }

        $scope.Get_ExpenseList = function () {
            $http({
                url: config.api.url + 'afs_expense/get',
                method: 'GET'
            }).then(function (response) {
                console.log(response);
                $scope.Expense_List = response.data;
            });
        }
        /// Get Time List   CarriesPoint    ExpenseList------------------------- ------------------------


        /// Add-Remove row  Table 
        $scope.addRowPoint = function () {
            var Driver_Amt = 0;
            var Staff_Amt = 0;

            console.log('point_id ' + $scope.DocList[0].point_id + ' status_code ' + $scope.DocList[0].status_code)
            if ($scope.Driver_Amt1 === 0) {
                console.log($scope.CarriesPoint);
                console.log('Driver_Amt1 ' + $scope.Driver_Amt1 + ' Staff_Amt1 ' + $scope.Staff_Amt1);
                for (var i = 0; i < $scope.CarriesPoint.length; i++) {
                    if ($scope.CarriesPoint[i].CARGROUP_CODE === $scope.DocList[0].cargroup_code && $scope.CarriesPoint[i].POINT_ID === $scope.DocList[0].point_id) {
                        $scope.Driver_Amt1 = $scope.CarriesPoint[i].DPOINT1_AMOUNT;
                        $scope.Staff_Amt1 = $scope.CarriesPoint[i].SPOINT1_AMOUNT;
                        $scope.Driver_Amt2 = $scope.CarriesPoint[i].DPOINT2_AMOUNT;
                        $scope.Staff_Amt2 = $scope.CarriesPoint[i].SPOINT2_AMOUNT; 
                        console.log('Add Driver_Amt1 ' + $scope.Driver_Amt1 + ' Staff_Amt1 ' + $scope.Staff_Amt1);
                        break;
                    }
                }
            }


            if ($scope.RowPoint === 0) {
                Driver_Amt = $scope.Driver_Amt1;
                Staff_Amt = $scope.Staff_Amt1 * $scope.Staff_Count;
            }
            else {
                Driver_Amt = $scope.Driver_Amt2;
                Staff_Amt = $scope.Staff_Amt2 * $scope.Staff_Count;
            }

            $scope.RowPoint++;
            $scope.PointMax++;

            console.log($scope.DocList[0].shipment_carries);
            if ($scope.DocList[0].shipment_carries == null)
            {
                $scope.DocList[0].shipment_carries = [];
            }
            $scope.DocList[0].shipment_carries.push({
                shipment_number: $scope.DocList[0].shipment_number,
                itemno: $scope.PointMax ,
                point_desc: "จุดส่งที่ " + $scope.RowPoint,
                //time_range: "",
                //so_number: "",
                //remark: "",
                driver_amount : Driver_Amt,
                staff_amount : Staff_Amt
            });
            $scope.DocList[0].transport_amount = $scope.DocList[0].transport_amount + Driver_Amt + Staff_Amt;
            console.log($scope.DocList[0].shipment_carries);
            console.log('PointMax ' + $scope.PointMax);
        };

        $scope.removeRowPoint = function (ItemNo) {
            var index = -1;
            var comArr = eval($scope.DocList[0].shipment_carries);
            for (var i = 0; i < comArr.length; i++) {
                if (comArr[i].item_no === ItemNo) {
                    index = i;
                    break;
                }
            }
            if (index === -1) {
                alert("Something gone wrong");
                return undefined;
            }
            $scope.DocList[0].shipment_carries.splice(index, 1);
            $scope.RowPoint--;
            console.log($scope.DocList[0].shipment_carries);
            console.log('RowPoint - ' + $scope.RowPoint);

        };

        $scope.addRowExpense = function () {
            var Expense_Amt = 0; 

            $scope.RowExpense++;
            $scope.ExpenseMax++;
            if ($scope.DocList[0].shipment_expense == null) {
                $scope.DocList[0].shipment_expense = [];
            }

            $scope.DocList[0].shipment_expense.push({
                shipment_number: $scope.ShipmentNo,
                itemno: $scope.ExpenseMax,
                expense_id :0,  
                remark: "จุดส่งที่ " + $scope.RowExpense,
                expense_amount: Expense_Amt,
                staff_amount: Staff_Ame
            });
            console.log($scope.DocList[0].shipment_expense);
            console.log('ExpenseMax ' + $scope.ExpenseMax);
        };

        $scope.removeRowExpense = function (ItemNo) {
            var index = -1;
            var comArr = eval($scope.DocList[0].shipment_expense);
            for (var i = 0; i < comArr.length; i++) {
                if (comArr[i].itemno === ItemNo) {
                    index = i;
                    break;
                }
            }
            if (index === -1) {
                alert("Something gone wrong");
                return undefined;
            }
            $scope.DocList[0].shipment_expense.splice(index, 1);
            $scope.RowExpense--;
            console.log($scope.DocList[0].shipment_expense);
            console.log('RowExpense - ' + $scope.RowExpense);

        };
        /// Add-Remove row Table End ---------------------------------------------------------------------------------------
         
        $http.get(config.api.url + '/afs_car_identity_card/get').then(function (response) {
            console.log(response);
            $scope.drivers = response.data;
        });

        //Modal //////////////////////////////////////////
        $scope.showModal = false; //ต้องมี
        $scope.Driver_ID = ""; 
        $scope.Driver_Name = "";

        $scope.showModalS1 = false; //ต้องมี
        $scope.Staff_ID1 = "";
        $scope.Staff_Name1 = "";

        $scope.showModalS2 = false; //ต้องมี
        $scope.Staff_ID2 = "";
        $scope.Staff_Name2 = "";

        $scope.buttonClicked = ""; //ต้องมี
        $scope.toggleModal = function (btnClicked) { //ต้องมี
            $scope.buttonClicked = btnClicked;
            $scope.showModal = !$scope.showModal;
        }; 

        $scope.selected_driver = function (driver_id ) { //ต้องมี
            $scope.DocList[0].driver_id = driver_id; 
        }

        $scope.toggleModalS1 = function (btnClicked) { //ต้องมี
            $scope.buttonClicked = btnClicked;
            $scope.showModalS1 = !$scope.showModalS1;
        };
        $scope.selected_Staff1 = function (Staff_id) { //ต้องมี
            $scope.DocList[0].staff1_id = Staff_id; 
        }

        $scope.toggleModalS2 = function (btnClicked) { //ต้องมี
            $scope.buttonClicked = btnClicked;
            $scope.showModalS2 = !$scope.showModalS2;
        };
        $scope.selected_Staff2 = function (Staff_id ) { //ต้องมี
            $scope.DocList[0].staff2_id = Staff_id; 
        }
        //Modal //////////////////////////////////////////-----------------------------------------------------


        ///  Get Description 
        $scope.getDriverName = function (TblDataList, KeyID) {
           // var NameEmp = $filter('EmpName')(TblDataLIst, KeyID);
            //console.log('KeyID ' + KeyID);
            if (TblDataList && KeyID) {
                for (var i = 0; i < TblDataList.length; i++) {
                    if (TblDataList[i].PEOPLE_ID === KeyID) {
                       var  NameEmp = TblDataList[i].NAME;
                        console.log('EmName ' + NameEmp);
                        return NameEmp;
                        break;
                    }
                }
            }
            return null;
        };

        ////  Get Description  End ----------------------------------------------------


        $scope.ShowTBL = function (DataList) {
            //console.log("ShowTBL DataList " );
            //console.log(DataList);
            if (DataList === undefined) // if your going to return true
                return true;

            if (DataList == undefined) // if your going to return true
                return true;

            console.log(DataList);
            if (DataList.length > 0)
                return false;

            return true;
        }

        

        ///  Save Data

        $scope.put_Data_Flag = function Put_Data() {

            console.log($scope.DocList[0]); 

            var ChkPointList = $scope.ShowTBL($scope.DocList[0].shipment_carries);
            console.log('ChkPointList ' + ChkPointList);
            if (ChkPointList)
            {
                $scope.ShipEditErr = "กรุณาระบุข้อมูลส่วน รายละเอียดจุดส่งของ ";  
                return;
            }


            console.log($scope.DocList); 
             
            if ($scope.DocList[0].driver_id.length == 0 || $scope.DocList[0].driver_id==0) {
                   $scope.ShipEditErr = "กรุณาระบุข้อมูลส่วน คนขับรถ ";
                   return;
                }
            if ($scope.DocList[0].staff1_id.length == 0 || $scope.DocList[0].staff1_id == 0 ) {
                   $scope.ShipEditErr = "กรุณาระบุข้อมูลส่วน เด็กรถคนที่ 1 ";
                return; 
                } 

                console.log($scope.DocList[0].point_id);
                if ($scope.DocList[0].point_id === undefined) {
                    $scope.ShipEditErr = "กรุณาระบุปข้อมูลให้ครบ (จุดส่ง)";
                    return undefined;
                }
                if ($scope.DocList[0].point_id == 0) {
                    $scope.ShipEditErr = "กรุณาระบุปข้อมูลให้ครบ (จุดส่ง)";
                    return undefined;
                }  
               
            console.log(typeof $scope.DocList[0].transport_date);
            console.log('transport_date ' + $scope.DocList[0].transport_date);
            if ($scope.DocList[0].transport_date != undefined) {
                var Df = getFormattedDate($scope.DocList[0].transport_date);

                console.log(Df);

                    if (Df != undefined && Df.length > 0) {
                        $scope.DocList[0].transport_date = Df;
                    }
                }
                else {
                  $scope.ShipEditErr = "กรุณาระบุปข้อมูลให้ครบ (วันที่ส่ง)";
                  return undefined;
                }


            /////////   ExpenseList   Detail
            var ChkExpenseListt = $scope.ShowTBL($scope.DocList[0].shipment_expense);
            console.log('ChkExpenseListt ' + ChkExpenseListt);
            //if (ChkExpenseListt) {
            //    $scope.ShipEditErr = "กรุณาระบุข้อมูลส่วน จุดส่งของ ";
            //    return;
            //}
            /////////   ExpenseList   Detail -------------------------------------------------------------------------------

            $scope.DocList[0].status_code = '02';

                //transport_amount 

            console.log($scope.DocList[0].shipment_carries);
            console.log($scope.DocList[0].shipment_expense);

            var textObject = JSON.stringify($scope.DocList);
            textObject = textObject.substring(1, textObject.length - 1);
            console.log('textObject ' + textObject);
            $http({
                url: config.api.url + 'shipment/save',
                method: 'POST',
                data: textObject
                    }).then(function (response) {
                    console.log(response);
                    $scope.loading = false;

                        // $scope.DocList = response.data;

                        $scope.ShipmentHead();

                    if (response.status !== 200) {
                        $scope.loading = false;
                        $scope.ShipEditErr = response.statusText;
                    }
                });

            return ;
        }

        ///  Save Data End--------------------------------------------------------------------------------

        function getFormattedDate(date) {

            try {
                var year = date.getFullYear();
                console.log(year)
                var month = (1 + date.getMonth()).toString();
                month = month.length > 1 ? month : '0' + month;

                console.log(month)
                var day = date.getDate().toString();
                day = day.length > 1 ? day : '0' + day;

                return day + '/' + month + '/' + year;
            }
            catch
            {
                console.log(date)
                return undefined;
            }
        };



        //clear screen
        $scope.fn_clear = function () {
            $('#CancelEdit').modal('hide');
            $scope.ShipmentHead();
        }

    };

})();