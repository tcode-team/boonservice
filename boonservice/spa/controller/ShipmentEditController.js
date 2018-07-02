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
            $scope.Staff_Count = 1;


           // $scope.PointList = [];
            $scope.ShipmentNo = $routeParams.param1;  
            $scope.Get_CarriesPoint();
            $scope.Get_TimeList();
            $scope.Get_ExpenseList(); 

            $scope.Get_identity();

            $scope.loading = false;
            $scope.ShipmentHead(); 
        }

        $scope.back = function () {
            $location.path('/shipmentlist/back');
        }


        //// Get Head
        $scope.ShipmentHead = function ()  {
            $scope.loading = true;
            console.log('loading' + $scope.loading);
            $scope.ShipEditErr = ""; 

            var textObject = JSON.stringify(""); 

            // console.log("text in " + textObject.indexOf("ShipmentNo")); 
            textObject = '{"forwarding":"5910"}';  
                         
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

                $scope.drivers = _.cloneDeep($scope.identity, true);
                $scope.staff1_list = _.cloneDeep($scope.identity, true);
                $scope.staff2_list = _.cloneDeep($scope.identity, true);  
                //console.log('drivers');
                //console.log($scope.drivers);
                //console.log('staff1_list');
                //console.log($scope.staff1_list);
                //console.log('staff2_list');
                //console.log($scope.staff2_list);

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
                if ($scope.DocList[0].shipment_carries.length > 0) {
                  $scope.RowPoint = $scope.DocList[0].shipment_carries.length  ;
                }
                if ($scope.DocList[0].shipment_expense.length > 0) {
                    $scope.RowExpense = $scope.DocList[0].shipment_expense.length  ;
                }
                $scope.Get_Staff_Amt();
                $scope.CalDocAmt();
                // Remove driver_id  ที่ถูกเลือกในช่องอื่นแล้ว  
                $scope.Remove_driver_list(); 
                $scope.Remove_Staff1_list(); 
                $scope.Remove_Staff2_list();
                // Remove driver_id  ที่ถูกเลือกในช่องอื่นแล้ว    End 
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
                            if ($scope.DocList[0].shipment_expense[v].itemno > $scope.ExpenseMax) {
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

        $scope.Get_identity = function () { 
            $http({
                url: config.api.url + 'afs_car_identity_card/get',
                method: 'GET'
            }).then(function (response) {
                console.log(response);
                $scope.identity = response.data;
                });
             
            //$http.get(config.api.url + '/afs_car_identity_card/get').then(function (response) {
            //    console.log(response);
            //    $scope.identity = response.data;

            //    console.log('Get_identity');
            //});
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
              
            $scope.RowPoint++;
            $scope.PointMax++;

           // console.log($scope.DocList[0].shipment_carries);
            if ($scope.DocList[0].shipment_carries == null)
            {
                $scope.DocList[0].shipment_carries = [];
            }
            $scope.DocList[0].shipment_carries.push({
                client: $scope.DocList[0].client,
                shipment_number: $scope.DocList[0].shipment_number,
                itemno: $scope.PointMax ,
                point_desc: "จุดส่งที่ " + $scope.RowPoint,
                //time_range: "",
                so_number: "",
                remark: "",
                driver_amount : Driver_Amt,
                staff_amount: Staff_Amt,
                created_by: $scope.user.userid,
                update_by: $scope.user.userid
            });
            //$scope.DocList[0].transport_amount = $scope.DocList[0].transport_amount + Driver_Amt + Staff_Amt;
            $scope.CalDocAmt();
        };

        $scope.removeRowPoint = function (rowPoint) {
            var removeLocal = function () {
                //// Start -- find index and remove 
                var index = -1;
                var comArr = eval($scope.DocList[0].shipment_carries);
                for (var i = 0; i < comArr.length; i++) {
                    if (comArr[i].itemno === rowPoint.itemno) {
                        index = i;
                        break;
                    }
                }
                if (index === -1) {
                    alert("Something gone wrong");
                    return undefined;
                }
                $scope.DocList[0].shipment_carries.splice(index, 1);
                //// End -- find index and remove 
            }

            //// Post delete 
            $http({
                url: config.api.url + 'shipment/removeCarries',
                method: 'POST',
                data: rowPoint
            }).then(function (response) {
                if (response.status === 200) {
                    removeLocal();
                };
            });

            if ($scope.RowPoint > 0) {
                $scope.RowPoint--;
            }
            
            $scope.CalDocAmt();
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
                client: $scope.DocList[0].client,
                shipment_number: $scope.ShipmentNo,
                itemno: $scope.ExpenseMax,
                expense_id :0,  
                remark: "อื่นๆที่ " + $scope.RowExpense, 
                expense_amount: 0,
                created_by: $scope.user.userid,
                update_by: $scope.user.userid
            });
            $scope.CalDocAmt();
            console.log($scope.DocList[0].shipment_expense);
            console.log('ExpenseMax ' + $scope.ExpenseMax);
        };

        $scope.removeRowExpense = function (rowExpense) {
            var removeLocal = function () {
                //// Start -- find index and remove 
                var index = -1;
                var comArr = eval($scope.DocList[0].shipment_expense);
                for (var i = 0; i < comArr.length; i++) {
                    if (comArr[i].itemno === rowExpense.itemno) {
                        index = i;
                        break;
                    }
                }
                if (index === -1) {
                    alert("Something gone wrong");
                    return undefined;
                }
                $scope.DocList[0].shipment_expense.splice(index, 1);
                //// End -- find index and remove 
            }

            //// Post delete 
            $http({
                url: config.api.url + 'shipment/removeExpense',
                method: 'POST',
                data: rowExpense
            }).then(function (response) {
                if (response.status === 200) {
                    removeLocal();
                };
            });

            if ($scope.RowPoint > 0) {
                $scope.RowPoint--;
            }


            if ($scope.RowExpense > 0) {
                $scope.RowExpense--;
            }
            $scope.CalDocAmt();
            console.log($scope.DocList[0].shipment_expense);
            console.log('RowExpense - ' + $scope.RowExpense);

        };

        $scope.changeRowExpense = function (rowExpense) {
            var expense = _.find($scope.Expense_List, function (v) { return v.EXPENSE_ID === rowExpense.expense_id });
            rowExpense.expense_amount = expense.EXPENSE_AMOUNT;
        }
        /// Add-Remove row Table End ---------------------------------------------------------------------------------------
         

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

            $scope.CalDocAmt();
            $scope.Remove_Staff1_list();
            $scope.Remove_Staff2_list();
            $scope.showModal = !$scope.showModal;
        }

        $scope.toggleModalS1 = function (btnClicked) { //ต้องมี
            $scope.buttonClicked = btnClicked;
            $scope.showModalS1 = !$scope.showModalS1;
        };
        $scope.selected_Staff1 = function (Staff_id) { //ต้องมี
            $scope.DocList[0].staff1_id = Staff_id; 

            $scope.CalDocAmt();
            $scope.Remove_driver_list();
            $scope.Remove_Staff2_list();
            $scope.showModalS1 = !$scope.showModalS1;
        }

        $scope.toggleModalS2 = function (btnClicked) { //ต้องมี
            $scope.buttonClicked = btnClicked;
            $scope.showModalS2 = !$scope.showModalS2;
        };
        $scope.selected_Staff2 = function (Staff_id ) { //ต้องมี
            $scope.DocList[0].staff2_id = Staff_id; 

            $scope.CalDocAmt();
            $scope.Remove_driver_list();
            $scope.Remove_Staff1_list();
            $scope.showModalS2 = !$scope.showModalS2;
        }

        $scope.Remove_driver_list = function () {// Remove driver_id  ที่ถูกเลือกในช่องอื่นแล้ว   
            $scope.drivers = _.cloneDeep($scope.identity, true);
            console.log('Remove Drive ' )
            //// Start -- find index and remove 
            if ($scope.DocList[0].staff1_id !== 0) {
                var index = -1;
                var comArr = eval($scope.drivers);
                for (var i = 0; i < comArr.length; i++) {
                    if (comArr[i].PEOPLE_ID === $scope.DocList[0].staff1_id) {
                        index = i;
                        break;
                    }
                }
                if (index === -1) {
                    //  alert("Something gone wrong");
                    return undefined;
                }
                $scope.drivers.splice(index, 1);
            }
            if ($scope.DocList[0].staff2_id !== 0) {
                var index = -1;
                var comArr = eval($scope.drivers);
                for (var i = 0; i < comArr.length; i++) {
                    if (comArr[i].PEOPLE_ID === $scope.DocList[0].staff2_id) {
                        index = i;
                        break;
                    }
                }
                if (index === -1) {
                    //  alert("Something gone wrong");
                    return undefined;
                }
                $scope.drivers.splice(index, 1);
            }
                //// End -- find index and remov 
        }

        $scope.Remove_Staff1_list = function () {// Remove driver_id  ที่ถูกเลือกในช่องอื่นแล้ว  
            console.log('Remove Staff1 ');
            $scope.staff1_list = _.cloneDeep($scope.identity, true);
            //// Start -- find index and remove  
            if ($scope.DocList[0].driver_id !== 0) {
                var index = -1;
                var comArr = eval($scope.staff1_list);
                for (var i = 0; i < comArr.length; i++) {
                    if (comArr[i].PEOPLE_ID === $scope.DocList[0].driver_id) {
                        index = i;
                        break;
                    }
                }
                if (index === -1) {
                    //  alert("Something gone wrong");
                    return undefined;
                }
                $scope.staff1_list.splice(index, 1);
            }
            if ($scope.DocList[0].staff2_id !== 0) {
                var index = -1;
                var comArr = eval($scope.staff1_list);
                for (var i = 0; i < comArr.length; i++) {
                    if (comArr[i].PEOPLE_ID === $scope.DocList[0].staff2_id) {
                        index = i;
                        break;
                    }
                }
                if (index === -1) {
                    //  alert("Something gone wrong");
                    return undefined;
                }
                $scope.staff1_list.splice(index, 1);
            }
            //// End -- find index and remov 
        }

        $scope.Remove_Staff2_list = function () {// Remove driver_id  ที่ถูกเลือกในช่องอื่นแล้ว  
            console.log('Remove Staff2 ');
            $scope.staff2_list = _.cloneDeep($scope.identity, true);
            //// Start -- find index and remove 

            if ($scope.DocList[0].driver_id !== 0) {
                var index = -1;
                var comArr = eval($scope.staff2_list);
                for (var i = 0; i < comArr.length; i++) {
                    if (comArr[i].PEOPLE_ID === $scope.DocList[0].driver_id) {
                        index = i;
                        break;
                    }
                }
                if (index === -1) {
                    //  alert("Something gone wrong");
                    return undefined;
                }
                $scope.staff2_list.splice(index, 1);
            }
            if ($scope.DocList[0].staff1_id !== 0) {
                var index = -1;
                var comArr = eval($scope.staff2_list);
                for (var i = 0; i < comArr.length; i++) {
                    if (comArr[i].PEOPLE_ID === $scope.DocList[0].staff1_id) {
                        index = i;
                        break;
                    }
                }
                if (index === -1) {
                    //  alert("Something gone wrong");
                    return undefined;
                }
                $scope.staff2_list.splice(index, 1);
            }
            //// End -- find index and remov 
        }
        //Modal //////////////////////////////////////////-----------------------------------------------------


        ///  Get Description 
        $scope.getDriverName = function (KeyID) {
           // var NameEmp = $filter('EmpName')(TblDataLIst, KeyID);
            var comArr = eval($scope.identity);
            if (comArr && KeyID) {
                for (var i = 0; i < comArr.length; i++) {
                    if (comArr[i].PEOPLE_ID === KeyID) {
                        var NameEmp = comArr[i].NAME;
                       // console.log('EmName ' + NameEmp);
                        return NameEmp;
                        break;
                    }
                }
            }
            return null;
        };

        $scope.CalStaffCount = function () {
            $scope.Staff_Count = 0; 
            if ($scope.DocList[0].staff1_id !== undefined) { 
                var staff1 = $scope.DocList[0].staff1_id; 
                console.log('staff1 ' + staff1);
                if (staff1 !== 0) {
                    $scope.Staff_Count = 1;
                }
            }
            if ($scope.DocList[0].staff2_id !== undefined) {
                var staff2 = $scope.DocList[0].staff2_id;
                console.log('staff2 ' + staff2);
                if ( staff2 !== 0) {
                    $scope.Staff_Count++;
                }
            }

            console.log('CalStaffCount ' + $scope.Staff_Count );
        }

        $scope.Get_Staff_Amt = function () { 
           // console.log('Driver_Amt1 ' + $scope.Driver_Amt1 + ' Staff_Amt1 ' + $scope.Staff_Amt1  + ' RowPoint ' + $scope.RowPoint);
            if ($scope.Driver_Amt1 === 0 || $scope.RowPoint < 2 ) {
                console.log($scope.CarriesPoint);
                for (var i = 0; i < $scope.CarriesPoint.length; i++) {
                    if ($scope.CarriesPoint[i].CARGROUP_CODE === $scope.DocList[0].cargroup_code && $scope.CarriesPoint[i].POINT_ID === $scope.DocList[0].point_id) {
                        $scope.Driver_Amt1 = $scope.CarriesPoint[i].DPOINT1_AMOUNT;
                        $scope.Staff_Amt1 = $scope.CarriesPoint[i].SPOINT1_AMOUNT;
                        $scope.Driver_Amt2 = $scope.CarriesPoint[i].DPOINT2_AMOUNT;
                        $scope.Staff_Amt2 = $scope.CarriesPoint[i].SPOINT2_AMOUNT;
                        //console.log('Add Driver_Amt1 ' + $scope.Driver_Amt1 + ' Staff_Amt1 ' + $scope.Staff_Amt1);
                        break;
                    }
                }
            }
        }

        $scope.CalDocAmt = function () {
            console.log('CalDocAmt');
            $scope.CalStaffCount();
            $scope.Get_Staff_Amt();
            $scope.Driver_Doc_Amt = 0;
            $scope.Staff_Doc_Amt = 0;
            try {
                if ($scope.DocList[0].shipment_carries.length > 0) {
                    //console.log($scope.Staff_Amt2 + ' * ' + $scope.Staff_Count);
                    $scope.DocList[0].shipment_carries[0].driver_amount = $scope.Driver_Amt1;
                    $scope.DocList[0].shipment_carries[0].staff_amount = $scope.Staff_Amt1 * $scope.Staff_Count;

                    $scope.Driver_Doc_Amt = $scope.Driver_Doc_Amt + $scope.DocList[0].shipment_carries[0].driver_amount
                    $scope.Staff_Doc_Amt = $scope.Staff_Doc_Amt + $scope.DocList[0].shipment_carries[0].staff_amount;
                    for (var i = 0; i < $scope.DocList[0].shipment_carries.length; i++) {
                        if (i !== 0) {
                            $scope.DocList[0].shipment_carries[i].driver_amount = $scope.Driver_Amt2;
                            $scope.DocList[0].shipment_carries[i].staff_amount = $scope.Staff_Amt2 * $scope.Staff_Count;

                            $scope.Driver_Doc_Amt = $scope.Driver_Doc_Amt + $scope.DocList[0].shipment_carries[i].driver_amount;
                            $scope.Staff_Doc_Amt = $scope.Staff_Doc_Amt + $scope.DocList[0].shipment_carries[i].staff_amount;
                        }
                    }
                }
            }
            catch (errPoint)
            { }
            try {
                if ($scope.DocList[0].shipment_expense.length > 0) { 
                    for (var v = 0; v < $scope.DocList[0].shipment_expense.length; v++) { 
                        $scope.Driver_Doc_Amt = $scope.Driver_Doc_Amt + ($scope.DocList[0].shipment_expense[v].expense_amount * 1);
                        $scope.Staff_Doc_Amt = $scope.Staff_Doc_Amt + ($scope.DocList[0].shipment_expense[v].expense_amount * $scope.Staff_Count);
                    }
                }
            }
            catch (errExpen) { }

            $scope.DocList[0].transport_amount = $scope.Driver_Doc_Amt + $scope.Staff_Doc_Amt;
            $scope.changeExpenseAmount();
        }
        ////  Get Description  End ----------------------------------------------------

        $scope.ClearStaff = function (Staff) {
            console.log(Staff);
            if (Staff === 'staff1') {
                $scope.DocList[0].staff1_id = 0;
            }
            if (Staff === 'staff2') {
                $scope.DocList[0].staff2_id = 0;
            }
            $scope.changeStaff();
        }

        $scope.changeStaff = function () {
            console.log('changeStaff ');
            $scope.Remove_driver_list();
            $scope.Remove_Staff1_list();
            $scope.Remove_Staff2_list();
            $scope.CalDocAmt();
        }

        $scope.changeExpenseAmount = function () {
            $scope.TotalDriverAmount();
            $scope.TotalStaffAmount();
        }

        /// รวมค่าขนส่ง คนขับ
        $scope.TotalDriverAmount = function () {
            var result = 0;
            if ($scope.DocList === undefined) return result;
            angular.forEach($scope.DocList[0].shipment_carries, function (value, key) {
                result += value.driver_amount;
            })
            angular.forEach($scope.DocList[0].shipment_expense, function (value, key) {
                result += parseFloat(value.expense_amount);
            })
            return result;
        }

        /// รวมค่าขนส่ง เด็กรถ
        $scope.TotalStaffAmount = function () {
            var result = 0;
            if ($scope.DocList === undefined) return result;
            angular.forEach($scope.DocList[0].shipment_carries, function (value, key) {
                result += value.staff_amount;
            })
            angular.forEach($scope.DocList[0].shipment_expense, function (value, key) {
                result += parseFloat(value.expense_amount);
            })
            return result;
        }

        $scope.ShowTBL = function (DataList) {
            //console.log("ShowTBL DataList " );
            //console.log(DataList);
            if (DataList === undefined) // if your going to return true
                return true;

            if (DataList == undefined) // if your going to return true
                return true;
             
            if (DataList.length > 0)
                return false;

            return true;
        }

        

        ///  Save Data
        $scope.put_Data_Flag = function Put_Data() {

            $scope.DocList[0].update_by = $scope.user.userid;
            console.log($scope.DocList[0]); 

            var ChkPointList = $scope.ShowTBL($scope.DocList[0].shipment_carries);
            console.log('ChkPointList ' + ChkPointList);
            if (ChkPointList)
            {
                $scope.ShipEditErr = "กรุณาระบุข้อมูลส่วน รายละเอียดจุดส่งของ ";  

                $scope.alert($scope.ShipEditErr);
                return;
            }


            console.log($scope.DocList); 
             
            if ($scope.DocList[0].driver_id.length == 0 || $scope.DocList[0].driver_id==0) {
                $scope.ShipEditErr = "กรุณาระบุข้อมูลส่วน คนขับรถ ";

                $scope.alert($scope.ShipEditErr);
                   return;
                }
            //if ($scope.DocList[0].staff1_id.length == 0 || $scope.DocList[0].staff1_id == 0 ) {
            //    $scope.ShipEditErr = "กรุณาระบุข้อมูลส่วน เด็กรถคนที่ 1 ";

            //    $scope.alert($scope.ShipEditErr);
            //    return; 
            //}

            console.log($scope.DocList[0].point_id);
            if ($scope.DocList[0].point_id === undefined) {
                $scope.ShipEditErr = "กรุณาระบุปข้อมูลให้ครบ (จุดส่ง)";

                $scope.alert($scope.ShipEditErr);
                return undefined;
            }
            if ($scope.DocList[0].point_id == 0) {
                $scope.ShipEditErr = "กรุณาระบุปข้อมูลให้ครบ (จุดส่ง)";

                $scope.alert($scope.ShipEditErr);
                return undefined;
            }  
               
            console.log(typeof $scope.DocList[0].transport_date);
            console.log('transport_date ' + $scope.DocList[0].transport_date);
            if ($scope.DocList[0].transport_date == undefined || $scope.DocList[0].transport_date == null || $scope.DocList[0].transport_date == '') 
            {
                $scope.ShipEditErr = "กรุณาระบุปข้อมูลให้ครบ (วันที่ส่ง)";
                $scope.alert($scope.ShipEditErr);
                return undefined;
            }

            $scope.loading = true;

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

            var val = _.clone($scope.DocList[0], true);
            if (val.transport_date !== undefined) val.transport_date = getFormattedDate(val.transport_date);

            //var textObject = JSON.stringify($scope.DocList);
            //textObject = textObject.substring(1, textObject.length - 1);
            //console.log('textObject ' + textObject);

            $http({
                url: config.api.url + 'shipment/save',
                method: 'POST',
                data: val //textObject
            }).then(function (response) {
                console.log(response);
                $scope.loading = false;

                // $scope.DocList = response.data;

                //$scope.ShipmentHead();

                if (response.status !== 200) {
                    $scope.loading = false;
                    $scope.ShipEditErr = response.statusText;
                    $scope.alert($scope.ShipEditErr);
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