/// <reference path="../../../assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productAddController', productAddController);
    productAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService']//state là thuộc tính của service đế dẫn dữ liệu
    function productAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.product =
            {
                CreatedDate: new Date(),
                Status: true
            } //đối tượng này dùng để binding dữ liệu ứng với mỗi control trong các thẻ của html.
        //$scope.productCategories = productCategories;
        $scope.AddProduct = AddProduct;
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }
        function AddProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages)
            apiService.post('/api/product/create', $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + 'đã được thêm mới');
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

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                })
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


    function loadParentCategory() {
        apiService.get('/api/productCategory/getallparents', null, function (result) {
            $scope.productCategories = result.data;
        }, function () {
            console.log('can not get list parent')
        });
    }
    loadParentCategory(); // cái hanmf này n ở ngoài rồi còn gì cho nó vào bên trong hàm productAddController
    }



})(angular.module('tedushop.products'));
//productCategory gọi dược về đối tượng trong model vì có productCategory=reslt.data chihs là responsedata