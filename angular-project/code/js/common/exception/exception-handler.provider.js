(function () {
    'use strict';

    angular.module("common.exception")
        .factory('http.interceptor', function ($q) {
            return {
                responseError: function (rejection) {
                    // 未授权的请求，跳转到登录页面
                    if (rejection.status === 401) {
                        var encodedUrl = decodeURI(location.pathname + location.hash);
                        window.location.href = '/Frame/NeedLogon.aspx?ReturnUrl=' + encodedUrl;
                    } else if (rejection.status === 400) {
                        swal("警告", rejection.data.Message);
                    } else if (rejection.status === 500) {
                        swal("服务器出错了", rejection.data.ExceptionMessage);
                    } else if (rejection.status === -1) {
                        swal("网络好像出问题了", '');
                    }
                    return $q.reject(rejection);
                }
            };
        })
        .config(["$httpProvider", function ($httpProvider) {
            $httpProvider.interceptors.push("http.interceptor");
        }]);

})();