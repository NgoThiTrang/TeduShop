/// <reference path="../../../assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productCategoryEditController', productCategoryEditController);
    productCategoryEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams','commonService']//state là thuộc tính của service đế dẫn dữ liệu
    function productCategoryEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        } //đối tượng này dùng để binding dữ liệu ứng với mỗi control trong các thẻ của html.

        $scope.UpdateProductCategory = UpdateProductCategory;
        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle()
        {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }
        function loadProductCategoryDetail()
        {
            apiService.get('/api/productCategory/getbyid/' + $stateParams.id, null, function (result)
            {
                $scope.productCategory = result.data;
            }, function (error) {
                notificationService.displayError(error.data)
            }
            )
        }
        function UpdateProductCategory() {
            apiService.put('/api/productCategory/update', $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + 'đã được cập nhật');
                $state.go('product_categories');// quay trở về link ban đầu sau khi đươjc thêm mới
            },
                function (error) {
                    notificationService.displayError("Cập nhật không thành công")
                }
            );
        }
        function loadParentCategory()
        {
            apiService.get('/api/productCategory/getallparents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log('can not get list parent')
            });
        }
        loadParentCategory();
        loadProductCategoryDetail();
    }

})(angular.module('tedushop.product_categories'));
//productCategory gọi dược về đối tượng trong model vì có productCategory=reslt.data chihs là responsedata