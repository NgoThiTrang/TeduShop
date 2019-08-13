/// <reference path="../../../assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productCategoryAddcontroller', productCategoryAddController);
    productCategoryAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService']//state là thuộc tính của service đế dẫn dữ liệu
    function productCategoryAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        } //đối tượng này dùng để binding dữ liệu ứng với mỗi control trong các thẻ của html.

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }
        $scope.AddProductCategory = AddProductCategory;
        function AddProductCategory() {
            apiService.post('/api/productCategory/create', $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + 'đã được thêm mới');
                $state.go('product_categories');// quay trở về link ban đầu sau khi đươjc thêm mới
            },
                function (error) {
                    notificationService.displayError("Thêm không thành công")
                }
            );
        }
        function loadParentCategory() {
            apiService.get('/api/productCategory/getallparents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log('can not get list parent')
            });
        } loadParentCategory();
    }

})(angular.module('tedushop.product_categories'));
//productCategory gọi dược về đối tượng trong model vì có productCategory=reslt.data chihs là responsedata