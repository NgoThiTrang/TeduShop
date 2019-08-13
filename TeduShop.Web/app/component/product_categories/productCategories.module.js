/// <reference path="../../assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('tedushop.product_categories', ['tedushop.common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state
            ('product_categories',
            {
                url: "/product_categories",
                parent : "base",
                templateUrl: "/app/component/product_categories/productCategoryListView.html",
                controller: "productCategoryListController"
            })
            .state(
                'add_product_categories',
                {
                    url: "/add_product_categories",
                    parent: "base",
                    templateUrl: "/app/component/product_categories/productCategoryAddView.html",
                    controller: "productCategoryAddcontroller"
            }
        ).state(
            'edit_product_categories',
            {
                url: "/edit_product_categories/:id",
                parent: "base",
                templateUrl: "/app/component/product_categories/productCategoryEditView.html",
                controller: "productCategoryEditController"
            }
        );
    }
})();