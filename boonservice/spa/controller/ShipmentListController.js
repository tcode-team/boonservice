//import { valueOf } from "../../Content/vendor/moment/src/lib/duration/as";

(function () {
    'use strict';

    angular
        .module('app')
        .controller('ShipmentListController', ShipmentListController)

    ShipmentListController.$inject = ['$scope', '$http', '$q', 'config'];
    function ShipmentListController($scope, $http, $q, config) {          
        $scope.title = 'คำนวณค่าขนส่ง BLF';         


        $scope.user = JSON.parse(sessionStorage.getItem('userDetail'));

        $scope.init = function () {
            $scope.Get_ShipmentType();
            $scope.Get_CarGroup();
            $scope.Get_Status();
            // $scope.Get_List();
        }

        $scope.Get_ShipmentType = function () {
            $http({
                url: config.api.url + 'shipmenttype/getall',
                method: 'GET'
            }).then(function (response) {
                console.log(response);
                $scope.ShipmentType = response.data;
            });
        }

        $scope.Get_CarGroup = function () {
            $http({
                url: config.api.url + 'afs_car_group/get',
                method: 'GET'
            }).then(function (response) {
                console.log(response);
                $scope.CarGroup = response.data;
            });
        }


        $scope.Get_Status = function () {
            $http({
                url: config.api.url + 'afs_shipment_status/get',
                method: 'GET'
            }).then(function (response) {
                console.log(response);
                $scope.DocStatus = response.data;
            });
        }

        $scope.Get_List = function (SearchData) {
            console.log(typeof SearchData);
            //  console.log(SearchData.Status);
            if (SearchData !== undefined) {
                try {
                    console.log("all " + SearchData); 
                    console.log("xx " + JSON.stringify(SearchData));  

                    var res = {};
                    var textObject = JSON.stringify(SearchData);

                    console.log("type " + typeof textObject); 
                    if (textObject === '{}') {
                        res = JSON.parse('{"forwarding":"1910"}');
                    } else {

                        console.log("text " + textObject.substring(1, textObject.length - 1)); 
                        textObject =  '{' + textObject.substring(1, textObject.length - 1) + ',"forwarding":"1910"}';
                        console.log("textObjectX " + textObject); 
                    }
                     

                    $http({
                        url: config.api.url + 'shipment/search',
                        method: 'POST',
                        data: textObject
                        //  grant_type: SearchData.grant_type,
                        //   forwarding: "1910",
                        // ShipmentDateFrom: "01/01/2018",
                        //ShipmentDateTo: "17/01/2018"//,
                        // ShipmentNo: SearchData.ShipmentNo,
                        // ShipmentType: SearchData.CarType,
                        // CarGroup: SearchData.CarGroup,
                        // CarLicense: SearchData.CarNo,
                        //ShipmentStatus: SearchData.Status 
                    }).then(function (response) {
                        console.log(response);
                        $scope.DocList = response.data;
                    });
                }
                catch{ }
            } // if
        }

    };

})();