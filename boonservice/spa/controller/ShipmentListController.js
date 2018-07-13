(function () {
    'use strict';

    angular
        .module('app')
        .controller('ShipmentListController', ShipmentListController)

    ShipmentListController.$inject = ['$scope', '$http', '$q', 'config', '$rootScope', '$location','$routeParams'];
    function ShipmentListController($scope, $http, $q, config, $rootScope, $location, $routeParams) {          
        $scope.title = 'คำนวณค่าขนส่ง BLF';         
         
        $scope.user = JSON.parse(sessionStorage.getItem('userDetail'));

        $scope.init = function () {
            $scope.Get_ShipmentType();
            $scope.Get_CarGroup();
            $scope.Get_Status();
            // $scope.Get_List();
            $scope.ShipSearchErr = ""; 
            $scope.ConfirmList = '';
            $scope.ConfirmRow = 0;
            console.log($scope.user);
           // try {
                var postBack = $routeParams.param1;
                if (postBack === 'back')
                {
                    console.log(typeof sessionStorage.getItem('SearchShipment'));
                    var TextSearch = JSON.parse(sessionStorage.getItem('SearchShipment'));
                    console.log(TextSearch);
                    if (TextSearch !== undefined) { 
                        if (TextSearch.ShipmentNo !== undefined) {
                            $scope.ShipmentNo = TextSearch.ShipmentNo;
                        } 
                        if (TextSearch.CarLicense !== undefined) {
                            $scope.CarLicense = TextSearch.CarLicense;  // DataSearch.ShipmentNo 
                        }
                        if (TextSearch.ShipmentType !== undefined) {
                            $scope.ShipmentType = TextSearch.ShipmentType;  // DataSearch.ShipmentNo 
                        }
                        if (TextSearch.CarGroup !== undefined) {
                            $scope.CarGroup = TextSearch.CarGroup;  // DataSearch.ShipmentNo 
                        }
                        if (TextSearch.ShipmentStatus !== undefined) {
                            $scope.ShipmentStatus = TextSearch.ShipmentStatus;  // DataSearch.ShipmentNo 
                        }
                    } 
                    var Df;
                    if (TextSearch.ShipmentDateFrom !== undefined) {
                        Df = TextSearch.ShipmentDateFrom;
                    }
                    else {
                        Df ="";
                    }
                    var Dt;
                    if (TextSearch.ShipmentDateTo !== undefined) {
                        Dt = TextSearch.ShipmentDateTo;
                    }
                    else {
                        Dt = "";
                    }
                    console.log(TextSearch);
                    console.log('Df ' + Df);
                    console.log('Dt ' + Dt);
                    if (Df !== undefined && Df !== null) { 
                        //delete TextSearch.ShipmentDateFrom;
                        var s = Df.split('/');
                        $scope.ShipmentDateFrom = new Date(s[2], s[1] - 1, s[0]);
                    }
                    if (Dt !== undefined && Dt !== null) { 
                       // delete TextSearch.ShipmentDateTo;
                        var s2 = Dt.split('/');
                        $scope.ShipmentDateTo = new Date(s2[2], s2[1] - 1, s2[0]);
                    } 
                   // delete TextSearch.forwarding; 
                    console.log(TextSearch); 
                    $scope.Get_List(Df,Dt);
                }
            //}
            //catch (errInit) {
            //    return;
            //}
        }

        $scope.Get_ShipmentType = function () {
            $http({
                url: config.api.url + 'shipmenttype/getall',
                method: 'GET'
            }).then(function (response) {
                console.log(response);
                $scope.ShipmentTypeList = response.data;
            });
        }

        $scope.Get_CarGroup = function () {
            $http({
                url: config.api.url + 'afs_car_group/get',
                method: 'GET'
            }).then(function (response) {
                console.log(response);
                $scope.CarGroupList = response.data;
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

        $scope.Get_List = function (DateFrom, DateTo) {
            $scope.loading = true;
            $scope.ShipSearchErr = "";
            //console.log(typeof SearchData);
            console.log(typeof DateFrom + " Fromtext " +  DateFrom);
            console.log(typeof DateTo + " Totext " + DateTo);
             
            var textObject = '{}'; 
            var SearchData = [];
            if ($scope.ShipmentNo !== undefined && $scope.ShipmentNo.length > 0) {
                textObject = '{"forwarding":"5910","ShipmentNo":"' + $scope.ShipmentNo + '"}';
                SearchData.push({
                    forwarding: "5910",
                    ShipmentNo: $scope.ShipmentNo
                });
            }
            else {
                textObject = '{"forwarding":"5910"}';
                SearchData.push({
                    forwarding: "5910" 
                });
            }
            if (DateFrom === undefined || DateTo === undefined) {
                
                if (textObject.indexOf("ShipmentNo") === - 1) {
                    $scope.ShipSearchErr = "กรุณาระบุเงื่อนไขในการค้นหา (วันที่ Shipment/ Shipment No)";

                    console.log($scope.ShipSearchErr);
                    $scope.loading = false;

                    $scope.alert($scope.ShipSearchErr);
                    return;
                }
            }

            var Df;
            var Dt ;
            if (typeof DateFrom === 'string') { Df = DateFrom; }
            else Df   = $scope.getFormattedDate(DateFrom);
            if (typeof DateTo === 'string') { Dt = DateTo; }
            else Dt = $scope.getFormattedDate(DateTo); 

            sessionStorage.removeItem('SearchShipment'); 

            while (textObject.indexOf(':""') !== - 1) { textObject = textObject.replace(':""', ':'); } 
            console.log("text in " + textObject);
             
            console.log("textObjectX " + textObject); 
                if (Df !== undefined && Df.length > 0) {
                    textObject = '{' + textObject.substring(1, textObject.length - 1) + ',"ShipmentDateFrom":"' + Df + '"}'; 
                    SearchData.ShipmentDateFrom = Df;
                    //sessionStorage.setItem('SearchShipmentDF', Df);
                }
                if (Dt !== undefined && Dt.length > 0) {
                    textObject = '{' + textObject.substring(1, textObject.length - 1) + ',"ShipmentDateTo":"' + Dt + '"}'; 
                    SearchData.ShipmentDateTo = Dt;
                   // sessionStorage.setItem('SearchShipmentDT', Dt);
                } 


            if ($scope.ShipmentType !== undefined && $scope.ShipmentType.length > 0) {
                textObject = '{' + textObject.substring(1, textObject.length - 1) + ',"ShipmentType":"' + $scope.ShipmentType + '"}';
                SearchData.ShipmentType = $scope.ShipmentType;
            }
            if ($scope.CarGroup !== undefined && $scope.CarGroup.length > 0) {
                textObject = '{' + textObject.substring(1, textObject.length - 1) + ',"CarGroup":"' + $scope.CarGroup + '"}';
                SearchData.CarGroup = $scope.CarGroup;
            }
            if ($scope.CarLicense !== undefined && $scope.CarLicense.length > 0) {
                textObject = '{' + textObject.substring(1, textObject.length - 1) + ',"CarLicense":"' + $scope.CarLicense + '"}';
                SearchData.CarLicense = $scope.CarLicense;
            }
            if ($scope.ShipmentStatus !== undefined && $scope.ShipmentStatus.length > 0) {
                textObject = '{' + textObject.substring(1, textObject.length - 1) + ',"ShipmentStatus":"' + $scope.ShipmentStatus + '"}';
                SearchData.ShipmentStatus = $scope.ShipmentStatus;
            }

            console.log(SearchData); 
            if (textObject !== undefined) {
                //sessionStorage.setItem('SearchShipment', JSON.stringify(SearchData));
                sessionStorage.setItem('SearchShipment', textObject);
            }
                   $http({
                        url: config.api.url + 'shipment/search',
                        method: 'POST',
                        data: textObject
                        //  grant_type: SearchData.grant_type,
                        //   forwarding: "5910",
                        // ShipmentDateFrom: "01/01/2018",
                        //ShipmentDateTo: "17/01/2018"//,
                        // ShipmentNo: SearchData.ShipmentNo,
                        // ShipmentType: SearchData.CarType,
                        // CarGroup: SearchData.CarGroup,
                        // CarLicense: SearchData.CarNo,
                        //ShipmentStatus: SearchData.Status 
                    }).then(function (response) {
                        console.log(response);
                        $scope.loading = false;
                        $scope.DocList = response.data; 
                        if (response.status !== '200') {
                            $scope.loading = false;
                            $scope.ShipSearchErr = response.statusText;
                        }
                });
            return;
        }

        $scope.getFormattedDate = function (date) {
            try {
                var year = date.getFullYear();

                var month = (1 + date.getMonth()).toString();
                month = month.length > 1 ? month : '0' + month;

                var day = date.getDate().toString();
                day = day.length > 1 ? day : '0' + day;

                return day + '/' + month + '/' + year;
            }
            catch (err) {
                return undefined;
            }

            //return undefined;
        };


        $scope.disabledChkBox = function (itemStatus) { 
            if (itemStatus === undefined) // if your going to return true
                return true;

            if (itemStatus ==='02' || itemStatus === 2)
            return false;

            //console.log('itemStatus chkBox ' + itemStatus);
            return true;
        }

        $scope.disabledChkBoxAll = function () { 
            var index = -1;
            var comArr = eval($scope.DataList);
            for (var i = 0; i < comArr.length; i++) {
                if (comArr[i].status_code === '02' || omArr[i].status_code  === 2) {
                    return false;
                }
            } 

            //console.log('itemStatus chkBox ' + itemStatus);
            return true;
        }

        $scope.disabledEditBT = function (itemStatus) { 
            if (itemStatus === undefined) // if your going to return true
                return true;

            if (itemStatus !== '03' && itemStatus !== 3)
                return false;

            return true;
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


        $scope.Show_Status = function (StatusCode) {
           // console.log("StatusCode " + StatusCode);
            if (StatusCode === undefined) // if your going to return true
                return 'badge ';

            //          console.log(DataList.length);
            if (StatusCode.length === 0  )
                return 'badge ';
            if (StatusCode === 1 || StatusCode === '01')
                return 'badge badge-warning';
            if (StatusCode === 2 || StatusCode === '02')
                return 'badge badge-success';
            if (StatusCode === 3 || StatusCode === '03')
                return 'badge badge-info';

            return 'badge ';
        }

        ////   Confirm  Head

        $scope.addOrRemoveConfirm = function (item, isMultiple) {

            console.log('addOrRemoveConfirm '); 
            var comArr = eval($scope.DocList);
            var itemData = '';
            for (var i = 0; i < comArr.length; i++) {
                if (comArr[i].shipment_number === item) {
                    itemData = '{ "client" : "' + comArr[i].client +'", "shipment_number": "' + item + '", "confirm_by": ' + $scope.user.userid + '}'
                    break;
                }
            }

            var itemIndex = $scope.ConfirmList.indexOf(itemData);//(item);
            var isPresent = (itemIndex > -1);

            console.log('itemdata ' + itemData);
            console.log('itemIndex ' + itemIndex + isMultiple);
            console.log('isPresent ' + isPresent);
            if (isMultiple) {
                if (isPresent) {
                   // $scope.ConfirmList.splice(itemIndex, 1)
                    if ($scope.ConfirmList.indexOf(',' + itemData) > -1) {
                        $scope.ConfirmRow--;
                        $scope.ConfirmList = $scope.ConfirmList.replace(',' + itemData, '');
                    }
                    if ($scope.ConfirmList.indexOf(  itemData + ',') > -1) {
                        $scope.ConfirmRow--;
                        $scope.ConfirmList = $scope.ConfirmList.replace(itemData + ',', '');
                    }
                    if ($scope.ConfirmList.indexOf(itemData  ) > -1) {
                        $scope.ConfirmRow--;
                        $scope.ConfirmList = $scope.ConfirmList.replace(itemData , '');
                    }
                } else {
                    // $scope.ConfirmList.push(item)
                      $scope.ConfirmRow++
                    if ($scope.ConfirmList.length === 0) {
                        $scope.ConfirmList = itemData;
                    }
                    else {
                        $scope.ConfirmList = $scope.ConfirmList + ',' + itemData;
                    }
                }
            } else { // UnClick 
                $scope.ConfirmRow--;
                console.log('Delete  ConfirmList');
                if (isPresent) {
                    $scope.ConfirmList = '';
                } else {
                    $scope.ConfirmList = $scope.ConfirmList.replace(itemData, '');
                }
                console.log($scope.ConfirmList);
            }
            console.log($scope.ConfirmList);
        }


        $scope.addOrRemoveConfirmALL = function (  isMultiple) {

            console.log('addOrRemoveConfirm ALL ');
            var comArr = eval($scope.DocList);
            var itemData = '';
            for (var i = 0; i < comArr.length; i++) {
                if (comArr[i].status_code === 2 || comArr[i].status_code === '02' ) {
                    itemData = '{ "client" : "' + comArr[i].client + '", "shipment_number": "' + comArr[i].shipment_number + '", "confirm_by": ' + $scope.user.userid + '}'
                 //   break;


                    var itemIndex = $scope.ConfirmList.indexOf(itemData);//(item);
                    var isPresent = (itemIndex > -1);

                    console.log('itemdata ' + itemData);
                    console.log('itemIndex ' + itemIndex + isMultiple);
                    console.log('isPresent ' + isPresent);
                    if (isMultiple) {
                        if (isPresent) {
                            // $scope.ConfirmList.splice(itemIndex, 1)
                            if ($scope.ConfirmList.indexOf(',' + itemData) > -1) {
                                $scope.ConfirmRow--;
                                $scope.ConfirmList = $scope.ConfirmList.replace(',' + itemData, '');
                            }
                            if ($scope.ConfirmList.indexOf(itemData + ',') > -1) {
                                $scope.ConfirmRow--;
                                $scope.ConfirmList = $scope.ConfirmList.replace(itemData + ',', '');
                            }
                            if ($scope.ConfirmList.indexOf(itemData) > -1) {
                                $scope.ConfirmRow--;
                                $scope.ConfirmList = $scope.ConfirmList.replace(itemData, '');
                            }
                        } else {
                            // $scope.ConfirmList.push(item)
                            $scope.ConfirmRow++
                            if ($scope.ConfirmList.length === 0) {
                                $scope.ConfirmList = itemData;
                            }
                            else {
                                $scope.ConfirmList = $scope.ConfirmList + ',' + itemData;
                            }
                        }
                    } else { // UnClick 
                        $scope.ConfirmRow--;
                        console.log('Delete  ConfirmList');
                        if (isPresent) {
                            $scope.ConfirmList = '';
                        } else {
                            $scope.ConfirmList = $scope.ConfirmList.replace(itemData, '');
                        }
                        console.log($scope.ConfirmList);
                    }

                }
            }

           
            console.log($scope.ConfirmList);
        }

        $scope.Confirm_Data = function () {

            $scope.loading = true;
            console.log($scope.ConfirmList); 
            $http({
                url: config.api.url + 'shipment/confirm',
                method: 'POST',
                data: '['+ $scope.ConfirmList +']'
            }).then(function (response) {
                console.log(response);
                $scope.loading = false;

                // $scope.DocList = response.data;
                $scope.ConfirmList = ''; 
                //$scope.Get_List($scope.DataSearch, $scope.ShipmentDateFrom, $scope.ShipmentDateTo);
                $scope.Get_List($scope.ShipmentDateFrom, $scope.ShipmentDateTo);

                if (response.status !== 200) {
                    $scope.loading = false;
                    $scope.ShipSearchErr = response.statusText;
                }
            });

            return;
        }
         ////   Confirm  Head  End -------------------------------------------------------------------------

        $scope.ShipmentForm = function (shipment_number) { 

            $location.path('/shipmentedit/' + shipment_number );
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

        
    };

})();