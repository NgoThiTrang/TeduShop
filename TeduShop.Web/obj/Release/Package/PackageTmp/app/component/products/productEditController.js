/// <reference path="../../../assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productEditController', productEditController);
    productEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService','$stateParams']//state là thuộc tính của service đế dẫn dữ liệu
    function productEditController(apiService, $scope, notificationService, $state, commonService, $stateParams) {
        $scope.product =
            {
                CreatedDate: new Date(),
                Status: true
            } //đối tượng này dùng để binding dữ liệu ứng với mỗi control trong các thẻ của html.
        //$scope.productCategories = productCategories;
        $scope.UpdateProduct = UpdateProduct;
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }
        function UpdateProduct() {
            apiService.put('/api/product/update', $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + 'đã được update');
                $state.go('products');// quay trở về link ban đầu sau khi đươjc thêm mới
            },
                function (error) {
                    notificationService.displayError("Thêm không thành công")
                }
            );
        }
        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }
        function loadProductDetail() {
            apiService.get('/api/product/getbyid/' + $stateParams.id, null, function (result) {
                $scope.product = result.data;
                $scope.moreImages = JSON.parse($scope.product.MoreImages);
            }, function (error) {
                notificationService.displayError(error.data)
            }
            )
        }
        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.product.Image = fileUrl;
            }
            finder.popup();
        }
        $scope.moreImages = [];
        $scope.ChooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                })
            }
            finder.popup();
        }
        function loadProductCategory() {
            apiService.get('/api/productCategory/getallparents', null, function (result) {
                $scope.productCategories = result.data;
            }, function () {
                console.log('can not get list parent')
            });
        }
        loadProductDetail();
        loadProductCategory();
    }

})(angular.module('tedushop.products'));
//productCategory gọi dược về đối tượng trong model vì có productCategory=reslt.data chihs là responsedata