(function() {
    'use strict';

    angular.module('demo', [
        'ui.router',

        'demo.home',

        'common.loading',
        'common.exception'
    ]);
})();