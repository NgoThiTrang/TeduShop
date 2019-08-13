(function (app) {
    app.controller('loginController', ['$scope', 'loginService', '$injector', 'notificationService',
        function ($scope, loginService, $injector, notificationService) {

            $scope.loginData = {
                userName: "",
                password: ""
            };

            $scope.loginSubmit = function () {
                loginService.login($scope.loginData.userName, $scope.loginData.password).then(function (response) {
                    if (response != null && response.data.error != undefined) {
                        console.log(response.data);
                        notificationService.displayError(response.data.error_description);
                        // lỗi n nhảy vào đây nhưng e thiêu chũ data nên n k nhảy vaof đượcx
                    }
                    else {
                        var stateService = $injector.get('$state'); // lõi đấy mặc đinh show ra nếu đăng nhâp sai; e đăng nhập sai nhuwng thiếu chữ data nên lúc nào cũng nhay vào cái hàm else dưới này
                        stateService.go('home');
                    }
                });// nhưng mà lúc đầu sai cho dù có sai thì cũng k đăng nhập được chứ a nhở sao nó vẫn vào trang chủ bt mà rồ mới báo lỗi 
            }
        }]);
})(angular.module('tedushop'));