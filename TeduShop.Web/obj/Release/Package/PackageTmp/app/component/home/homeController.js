var f = function (app) {
    app.controller('homeController', homeController);
    
    function homeController($scope) {
        $scope.test = "test";
    }  
}
f(angular.module('tedushop'))