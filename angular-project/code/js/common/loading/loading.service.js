

(function () {
    'use strict';

    angular
        .module('common.loading')
        .service('httpLoadingInterceptorService', httpLoadingInterceptorService);

    httpLoadingInterceptorService.$inject = ['$q', '$rootScope'];
    function httpLoadingInterceptorService($q, $rootScope) {
        return {
            request: function (config) {
                // Shows the loader
                if (config.showLoading) {
                    $rootScope.$broadcast("httpLoadingInterceptor:show");
                }
                return config || $q.when(config)
            },
            response: function (response) {
                // Hides the loader
                if (response.config.showLoading) {
                    $rootScope.$broadcast("httpLoadingInterceptor:hide");
                }
                return response || $q.when(response);
            },
            responseError: function (response) {
                // Hides the loader
                if (response.config.showLoading) {
                    $rootScope.$broadcast("httpLoadingInterceptor:hide");
                }

                return $q.reject(response);
            }
        };
    }
})();
