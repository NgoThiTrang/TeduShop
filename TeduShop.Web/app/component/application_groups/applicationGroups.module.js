/// <reference path="../../assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('tedushop.application_groups', ['tedushop.common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state
            ('application_groups',
            {
                url: "/application_groups",
                parent : "base",
                templateUrl: "/app/component/application_groups/applicationGroupListView.html",
                controller: "applicationGroupListController"
            })
            .state(
                'add_application_groups',
                {
                    url: "/add_application_groups",
                    parent: "base",
                    templateUrl: "/app/component/application_groups/applicationGroupAddView.html",
                    controller: "applicationGroupAddcontroller"
            }
        ).state(
            'edit_application_groups',
            {
                url: "/edit_application_groups/:id",
                parent: "base",
                templateUrl: "/app/component/application_groups/applicationGroupEditView.html",
                controller: "applicationGroupEditController"
            }
        );
    }
})();