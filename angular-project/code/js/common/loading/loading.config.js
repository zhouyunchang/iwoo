
(function () {
    'use strict';

    angular.module('common.loading').config(["$httpProvider", function ($httpProvider) {
        $httpProvider.interceptors.push('httpLoadingInterceptorService');
    }]);
})();

