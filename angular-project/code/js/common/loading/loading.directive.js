

(function () {
    'use strict';

    angular
        .module('common.loading')
        .directive('httpLoadingInterceptorSpinner', httpLoadingInterceptorSpinner);

    httpLoadingInterceptorSpinner.$inject = ['$timeout'];
    function httpLoadingInterceptorSpinner($timeout) {
        // Usage:
        //
        // Creates:
        //
        return {
            restrict: 'A',
            template: '',
            link: function ($scope, element, attrs) {
                var loadingTimeout = null;
                $scope.$on("httpLoadingInterceptor:show", function () {
                    // console.log('show');
                    loadingTimeout = $timeout(function () {
                        element.addClass('loading-visible')
                    }, 300);
                });

                $scope.$on("httpLoadingInterceptor:hide", function () {
                    if (loadingTimeout !== null) $timeout.cancel(loadingTimeout);
                    // console.log('hide');
                    element.removeClass('loading-visible');
                });

            }
        };
    }
})();

