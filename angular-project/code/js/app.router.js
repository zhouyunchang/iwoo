(function () {
    'use strict';

    angular
        .module('demo')
        .config(function ($stateProvider, $urlRouterProvider) {
            $urlRouterProvider.otherwise('/home');

            $stateProvider
                .state('home', {
                    url: '/home',
                    templateUrl: 'home/home.html',
                    controller: 'HomeController',
                    controllerAs: 'vm'
                })
        });
})();