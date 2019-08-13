/// <reference path="../../assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('tedushop.products', ['tedushop.common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider          
            .state('products',
            {
                url: "/products",
                parent: 'base',
                templateUrl: "/app/component/products/productListView.html",
                controller: "productListController"
            }
        ).state('product_add',
            {
                url: "/product_add",
                parent: 'base',
                templateUrl: "/app/component/products/productAddView.html",
                controller: "productAddController"
            }
        ).state('edit_products',
            {
                url: "/edit_products/:id",
                parent: 'base',
                templateUrl: "/app/component/products/productEditView.html",
                controller: "productEditController"
            }

        );
    }
})();