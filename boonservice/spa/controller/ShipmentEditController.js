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
            $scope.ShipEditErr = "";   
            $scope.ShipmentNo = $routeParams.param1; 
             
            $scope.ShipmentHead();
            $scope.Get_CarriesPoint();
        }

        $scope.back = function () {
            $location.path('/shipmentlist');
        }


        //// Get Head
        $scope.ShipmentHead = function ()  {
            //$scope.loading = true;
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
                //$scope.loading = false;
                $scope.DocList = response.data;

                console.log(response.status );
                if (response.status !== 200) {
                    //$scope.loading = false;
                    $scope.ShipEditErr = response.statusText;
                    //return undefined;
                }
                });
            console.log("GetHead " + $scope.DocList);
            if ($scope.DocList !== undefined) {
                $scope.ShipmentNumber = $scope.DocList[0].shipment_number;
            }
            //console.log("GetHead " + $scope.DocList);
            return;
        }

        /// Get Head End -------------------------------------------------------------------------------------------

        /// Get Point


        /// Get Point End-------------------------------------------------------------------------------------------
         
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
        $scope.selected_driver = function (driver_id,driver_name) { //ต้องมี
            $scope.DocList[0].driver_id = driver_id; 
            $scope.Driver_Name = driver_name;
        }

        $scope.toggleModalS1 = function (btnClicked) { //ต้องมี
            $scope.buttonClicked = btnClicked;
            $scope.showModalS1 = !$scope.showModalS1;
        };
        $scope.selected_Staff1 = function (Staff_id, Staff_name) { //ต้องมี
            $scope.DocList[0].staff1_id = Staff_id;
            $scope.Staff_Name1 = Staff_name;
        }

        $scope.toggleModalS2 = function (btnClicked) { //ต้องมี
            $scope.buttonClicked = btnClicked;
            $scope.showModalS2 = !$scope.showModalS2;
        };
        $scope.selected_Staff2 = function (Staff_id, Staff_name) { //ต้องมี
            $scope.DocList[0].staff2_id = Staff_id;
            $scope.Staff_Name2 = Staff_name;
        }
        //Modal //////////////////////////////////////////-----------------------------------------------------


        $scope.Get_CarriesPoint = function () {
            $http({
                url: config.api.url + 'afs_carries_point/get',
                method: 'GET'
            }).then(function (response) {
                console.log(response);
                $scope.CarriesPoint = response.data;
            });
        }

        $scope.ShowTBL = function (DataList) {
//   console.log("ShowTBL DataList " + DataList);
            if (DataList === undefined) // if your going to return true
                return true;

  //          console.log(DataList.length);
            if (DataList.length > 0)
                return false;

            return true;
        }

        

        ///  Save Data

        $scope.put_Data_Flag = function Put_Data() {

            console.log($scope.DocList[0]);
            console.log($scope.DocList[0].driver_id);

            console.log($scope.DocList[0].staff1_id);
            console.log($scope.DocList[0].staff2_id);
            //console.log(Driver_Name.value);
            //$scope.DocList[0].driver_id = $scope.Driver_ID;

            var ChkPointList = $scope.ShowTBL($scope.PointList);
            console.log('ChkPointList ' + ChkPointList);
            if (ChkPointList)
            {
                $scope.ShipEditErr = "กรุณาระบุข้อมูลส่วน จุดส่งของ ";  
                return;
            }


            console.log($scope.DocList);
            var textObject = '{ }'; 

            textObject = '{"shipment_number":"' + $scope.DocList[0].shipment_number + '"}'; 
            if (DocList[0].driver_id.length > 0) {
                    textObject = '{' + textObject.substring(1, textObject.length - 1) + ',"driver_id":' + Driver_ID.value + '}'; 
                }
            if (DocList[0].staff1_id.length > 0) {
                    textObject = '{' + textObject.substring(1, textObject.length - 1) + ',"staff1_id":' + Staff_ID1.value + '}';
                }
            if (DocList[0].staff2_id.length > 0) {
                    textObject = '{' + textObject.substring(1, textObject.length - 1) + ',"staff2_id":' + Staff_ID2.value + '}';
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
                textObject = '{' + textObject.substring(1, textObject.length - 1) + ',"point_id":' + $scope.DocList[0].point_id + '}';

                //console.log( CarriesPoint_ID);
                //if (CarriesPoint_ID.value.length > 0) {
                //    //var CarPoint_ID = '"' + CarriesPoint_ID.value + '"';
                //    var CarPoint_ID = JSON.stringify(CarriesPoint_ID);
                //    console.log('Carries  ' + CarriesPoint_ID);
                //    CarPoint_ID = CarPoint_ID.substring(CarPoint_ID.indexof(":"));
                //    console.log('CarriesPoint_ID ' + CarPoint_ID );
                //    textObject = '{' + textObject.substring(1, textObject.length - 1) + ',"point_id":' + CarPoint_ID + '}';
                //}

               
                console.log(typeof $scope.DocList[0].transport_date );
                if ($scope.DocList[0].transport_date > 0) {
                    var Df = getFormattedDate($scope.DocList[0].transport_date);

                    if (Df != undefined && Df.length > 0) {
                        textObject = '{' + textObject.substring(1, textObject.length - 1) + ',"transport_date":"' + Df + '"}';
                    }
                }
                else {
                  $scope.ShipEditErr = "กรุณาระบุปข้อมูลให้ครบ (วันที่ส่ง)";
                  return undefined;
                }


            /////////   ExpenseList   Detail
            var ChkExpenseListt = $scope.ShowTBL($scope.ExpenseList);
            console.log('ChkExpenseListt ' + ChkExpenseListt);
            if (ChkExpenseListt) {
                $scope.ShipEditErr = "กรุณาระบุข้อมูลส่วน จุดส่งของ ";
                return;
            }
            /////////   ExpenseList   Detail -------------------------------------------------------------------------------


                //transport_amount
                var transport_amount = 0;
                textObject = '{' + textObject.substring(1, textObject.length - 1) + ',"transport_amount":'+transport_amount + '}';


            try {
                textObject = '{' + textObject.substring(1, textObject.length - 1) + ',"car_license":"' + $scope.DocList[0].car_license + '"}';
                textObject = '{' + textObject.substring(1, textObject.length - 1) + ',"container_id":"' + $scope.DocList[0].container_id + '"}';
                textObject = '{' + textObject.substring(1, textObject.length - 1) + ',"cargroup_code":"' + $scope.DocList[0].cargroup_code + '"}';
                textObject = '{' + textObject.substring(1, textObject.length - 1) + ',"shipmenttype_code":"' + $scope.DocList[0].shipmenttype_code + '"}';
                textObject = '{' + textObject.substring(1, textObject.length - 1) + ',"route":"' + $scope.DocList[0].route + '"}';
                textObject = '{' + textObject.substring(1, textObject.length - 1) + ',"status_code":"' + $scope.DocList[0].status_code + '"}';
                textObject = '{' + textObject.substring(1, textObject.length - 1) + ',"forwarding":"' + $scope.DocList[0].forwarding + '"}';
                textObject = '{' + textObject.substring(1, textObject.length - 1) + ',"client":"' + $scope.DocList[0].client + '"}';
            }
            catch{ }


            console.log('textObject ' + textObject);

            //$http({
            //    url: config.api.url + 'shipment/save',
            //    method: 'POST',
            //    data: textObject
            //            //    "shipment_number": "string",
            //            //   "transport_date": "string",
            //            //   "driver_id": 0,
            //            //     "staff1_id": 0,   "staff2_id": 0,
            //            //   "point_id": 0,
            //            //  "transport_amount": 0,
            //            // ShipmentNo: SearchData.ShipmentNo,
            //            //   "status_code": "string",  "status_desc": "string",
            //            //   "remark": "string",
            //            //ShipmentStatus: SearchData.Status 
            //        }).then(function (response) {
            //        console.log(response);
            //        $scope.loading = false;

            //            // $scope.DocList = response.data;

            //            $scope.ShipmentHead();

            //        if (response.status !== 200) {
            //            $scope.loading = false;
            //            $scope.ShipEditErr = response.statusText;
            //        }
            //    });

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
        }



        //clear screen
        $scope.fn_clear = function () {
            $('#CancelEdit').modal('hide');
            $scope.ShipmentHead();
        }

    };

})();