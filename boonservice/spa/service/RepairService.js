(function () {
    'use strict';

    angular
        .module('app')
        .factory('RepairService', RepairService);

    RepairService.$inject = ['$http', '$q', 'config'];
    function RepairService($http, $q, config) {
        var service = {};

        service.RepairStatusText = RepairStatusText;
        service.authority = authority;
        service.get_repair_item_type = get_repair_item_type;
        service.search_list = search_list;
        service.searchso = searchso;
        service.repair_detail = repair_detail;
        service.save = save; 
        service.saveaftersale = saveaftersale;
        service.removeraw = removeraw;

        return service;

        //Convert Status
        function RepairStatusText (status) {
            if (status == "NEW") return "New"
            else if (status == "PREPARE") return "จัดเตรียมคิวงานและอะไหล่"
            else if (status == "PROCESS") return "ทีมช่างดำเนินการ"
            else if (status == "COMPLETE") return "Complete"
            else return "New";
        }

        function authority(userid) {
            var response = $http({
                url: config.api.url + 'afs_authority/getid?id=' + userid,
                method: 'GET'
            });
            return response;  
        }

        function search_list(selection) {
            var response = $http({
                url: config.api.url + 'repair/search_list',
                method: 'POST',
                data: selection
            });
            return response;  
        }

        function searchso(so_number) {
            var data = {
                sonumber: so_number
            }
            var response = $http({
                url: config.api.url + 'repair/searchso',
                method: 'POST',
                data: data
            });
            return response;  
        }      

        function get_repair_item_type() {
            var response = $http({
                url: config.api.url + 'afs_repair_item_type/get',
                method: 'GET'
            });
            return response;  
        }

        function repair_detail(repair_code) {
            var response = $http({
                url: config.api.url + 'repair/detail?repair_code=' + repair_code,
                method: 'POST'
            });
            return response;  
        }

        function save(header, items) {
            var response = $http({
                url: config.api.url + 'repair/save',
                method: 'POST',
                data: {
                    header: header,
                    items: items
                }
            });
            return response;  
        }

        function saveaftersale(header, appoint, raws) {
            var response = $http({
                url: config.api.url + 'repair/save_appointment',
                method: 'POST',
                data: {
                    header: header,
                    appoint: appoint,
                    raws: raws
                }
            });
            return response;
        };

        function removeraw(raw_id) {
            var response = $http({
                url: config.api.url + 'repair/remove_raw',
                method: 'POST',
                data: {
                    raw_id: raw_id
                }
            });
            return response;  
        }
       
    }
})();