(function () {
    'use strict';

    angular
        .module('demo')
        .service('Database', Database)
        .config(['$httpProvider', function ($httpProvider) {
            $httpProvider.interceptors.push('http.interceptor');
        }]);

    Database.$inject = ['$http'];

    function Database($http) {

    }
})();
