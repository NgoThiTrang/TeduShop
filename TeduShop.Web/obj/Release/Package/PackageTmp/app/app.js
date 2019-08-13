/// <reference path="../../assets/admin/libs/angular/angular.js" />*
(function () {
    angular.module('tedushop', ['tedushop.common','tedushop.products', 'tedushop.product_categories', 'tedushop.application_groups','tedushop.application_roles', 'tedushop.application_users','tedushop.statistics'])
        .config(config)
        .config(configAuthentication);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('base',
            {
                url: '',
                templateUrl: "/app/share/views/baseView.html",
                abstract: true // cho pheps các template khác kế thừa nó. 
                    
            } 
        )
            .state('login',
            {
                url: "/login",
                templateUrl: "/app/component/login/loginView.html ",
                controller: "loginController"
            }
            )
            .state('home',
            {
                url: "/admin",
                parent: 'base',// các template khác đều kế thừa cái html base ở trên .
                templateUrl: "/app/component/home/homeView.html",
                controller: "homeController"
            }
        );
        $urlRouterProvider.otherwise('/login');
    }
    function configAuthentication($httpProvider) {
        $httpProvider.interceptors.push(function ($q, $location) {
            return {
                request: function (config) {

                    return config;
                },
                requestError: function (rejection) {

                    return $q.reject(rejection);
                },
                response: function (response) {
                    if (response.status == "401") {
                        $location.path('/login');
                    }
                    //the same response/modified/or a new one need to be returned.
                    return response;
                },
                responseError: function (rejection) {

                    if (rejection.status == "401") {
                        $location.path('/login');
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }
})();