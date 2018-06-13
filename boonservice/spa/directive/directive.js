(function () {
    'use strict';

    angular
        .module('app')
        .directive('modalSelect', modalSelect);

    function modalSelect() {
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
          replace:true,
          scope:true,
          link: function postLink(scope, element, attrs) {
              scope.$watch(attrs.visible, function(value){
              if (value == true)
                 $(element).modal('show');
              else
                 $(element).modal('hide');
            });

            $(element).on('shown.bs.modal', function(){
              scope.$apply(function(){
                scope.$parent[attrs.visible] = true;
              });
            });

            $(element).on('hidden.bs.modal', function(){
              scope.$apply(function(){
                scope.$parent[attrs.visible] = false;
              });
            });
          }
        };
    }
})();