//(function (app) {
//    app.controller('rootController', rootController);
//    rootController.$inject =['$scope', '$state']
//    function rootController($scope, $state) {
//        $scope.logout = function () {
//            $state.go('login');
//        }

//    }
//}
//)(angular.module('tedushop'));
(function (app) {
    app.controller('rootController', rootController);

    rootController.$inject = ['$state', 'authData', 'loginService', '$scope', 'authenticationService'];

    function rootController($state, authData, loginService, $scope, authenticationService) {
        $scope.logout = function () {
            loginService.logOut();
            $state.go('login');
        }
        $scope.authentication = authData.authenticationData;

        //authenticationService.validateRequest(); //cái rootController là cái to đungf bao gồm cả login
        // luc nao n cung được goi ra cái hàm này dấy chưa đăng nhập nên n lỗi do cái login vẫn nằm trong rootController
        // hết lỗi nhé ừ chấm than.để tôi ngấm đã.
    }
})(angular.module('tedushop'));