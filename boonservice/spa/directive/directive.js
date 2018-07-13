(function () {
    'use strict';

    angular.module("app").directive('dlEnterKey', ['$timeout', function ($timeout) {
        return function (scope, element, attrs) {

            element.bind("keydown keypress", function (event) {
                var keyCode = event.which || event.keyCode;

                // If enter key is pressed
                if (keyCode === 13) {
                    scope.$apply(function () {
                        $timeout(function () {
                            // Evaluate the expression
                            scope.$eval(attrs.dlEnterKey, { '$event': event });
                        }, 200);
                    });
                    event.preventDefault();
                }
            });
        };
    }]);

    angular.module("app").filter('removeleadingzero', function () {
        // Create the return function
        // set the required parameter name to **number**
        return function (number) {
            if (isNaN(number)) {
                return number;
            } else {
                return number.replace(/^0+/, '');
            }
        }
    });

    angular.module("app").directive('numericOnly', function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, modelCtrl) {

                modelCtrl.$parsers.push(function (inputValue) {

                    var transformedInput = inputValue ? inputValue.replace(/[^0-9.+-]/g, '') : 0;
                    var max = angular.isUndefined(attrs.ngMax) ? 0 : attrs.ngMax;
                    //console.log(transformedInput + '|' + attrs.ngMax);
                    if (transformedInput != inputValue) {
                        modelCtrl.$setViewValue(transformedInput);
                        modelCtrl.$render();
                    }
                    //clear beginning 0
                    //if (transformedInput < 0) {
                    //    modelCtrl.$setViewValue(0);
                    //    modelCtrl.$render();
                    //}

                    if (max > 0 && transformedInput > max) {
                        transformedInput = max.toString();
                        modelCtrl.$setViewValue(transformedInput);
                        modelCtrl.$render();
                    }
                    return transformedInput;
                });
            }
        };
    }); 

    angular.module("app").factory('Excel', function ($window) {
        var uri = 'data:application/vnd.ms-excel;base64,',
            template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>',
            base64 = function (s) { return $window.btoa(unescape(encodeURIComponent(s))); },
            format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) };
        return {
            tableToExcel: function (tableId, worksheetName) {
                var table = $(tableId);
                var ctx = { worksheet: worksheetName, table: table.html() },
                    href = uri + base64(format(template, ctx));
                return href;
            }
        };
    });

    angular.module("app").directive('modalSelect', [function () {
        return {
            template: '<div class="modal fade" tabindex="-1" role="dialog" aria-hidden="true">' +
                '<div class="modal-dialog" role="document">' +
                '<div class="modal-content">' +
                '<div class="modal-header" style="padding-bottom:7px;">' +
                '<h4 class="modal-title">{{ buttonClicked }}</h4>' +
                '<button type="button" class="close" data-dismiss="modal" aria-label="Close">' +
                '<span aria-hidden="true">×</span>' +
                '</button>' +
                '</div>' +
                '<div class="modal-body" style="padding-top:0px;" ng-transclude></div>' +
                '<div class="modal-footer">' +
                '<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>' +
                '</div>' +
                '</div>' +
                '</div>' +
                '</div>',
            restrict: 'E',
            transclude: true,
            replace: true,
            scope: true,
            link: function postLink(scope, element, attrs) {
                scope.$watch(attrs.visible, function (value) {
                    if (value == true)
                        $(element).modal('show');
                    else
                        $(element).modal('hide');
                });

                $(element).on('shown.bs.modal', function () {
                    scope.$apply(function () {
                        scope.$parent[attrs.visible] = true;
                    });
                });

                $(element).on('hidden.bs.modal', function () {
                    scope.$apply(function () {
                        scope.$parent[attrs.visible] = false;
                    });
                });
            }
        };
    }]);
})();