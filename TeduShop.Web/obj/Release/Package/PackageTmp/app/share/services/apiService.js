/// <reference path="../../../assets/admin/libs/angular/angular.js" />
(function (app) {
    app.factory('apiService', apiService)
    apiService.$inject = ['$http', 'notificationService','authenticationService']
    function apiService($http, notificationService, authenticationService) {
        return {
            get: get,
            post: post,
            put: put,
            del:del
        }
        function post(url, data, success, failure)
        {
            authenticationService.setHeader(); // đây thôi
            $http.post(url, data).then(function (result) { success(result); },
                function (error) {
                    console.log(error.status); // n ra là do cái này?????????????????? tự thằng http n biết show ra lỗi
                    if (error.status === 401) { // ngoặc đung thì n nhảy vào ham failure
                        notificationService.displayError('Autheticate is requied');
                    }
                    else if (failure != null) {
                        failure(error);
                    }
                });
        }
        function get(url, params, succ, fail) {
            authenticationService.setHeader();
            $http.get(url, params).then(function (result) {
                succ(result);
            }, function (error) {

                fail(error);
            }
            );
        }
        function put(url, data, success, failure)
        {
            authenticationService.setHeader();
            $http.put(url, data).then(function (result)
            { success(result); },
                function (error)
                {
                    console.log(error.status); 
                    if (error.status === 401)
                    { 
                        notificationService.displayError('Autheticate is requied');
                    }
                    else if (failure != null)
                    {
                        failure(error);
                    }
                })
        }
        function del(url, data, success, failure) {
            authenticationService.setHeader();
            $http.delete(url, data).then(function (result) { success(result); },
                function (error) {
                    console.log(error.status); // n ra là do cái này?????????????????? tự thằng http n biết show ra lỗi
                    if (error.status === 401) { // ngoặc đung thì n nhảy vào ham failure
                        notificationService.displayError('Autheticate is requied');
                    }
                    else if (failure != null) {
                        failure(error);
                    }
                })
        }
    }
})(angular.module('tedushop.common'))
//get là một hàm trog apiService nhận 4 tham số truyên vào là url, param nhưng cái api kia k cân param nên là null,
// tiếpes là success : tham số này là dạng hàm, hàm này truyêfn vao biên result
// cuối cùng là hàm failure : nhận truyên vào là error
// e muốn sưa đi thành gì cx được
//đâý
// miễn là lúc e gọi apiService.get thi n phải đung cái truyên vaof như kia
// 2 cái đầu là tham số thường 2 cái sau là dạng hàm
//// .data là để nso lấy ra dữ liệu ở cái tham số trong hàm đó ạ 
// cái response la cái result
// còn cái responseData là result.data
