(function (app) {
    app.controller('productCategoryListController', productCategoryListController);
    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter']

    function productCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.productCategory = [];
        $scope.page = 0;
        $scope.pagescount = 0;
        $scope.totalCount = 0;
        $scope.keyword = '';
        $scope.search = search;
        $scope.getproductCategories = getproductCategories;
        $scope.delProductCategories = delProductCategories;
        $scope.selectAll = selectAll;
        $scope.deleteMultiple = deleteMultiple;
        function deleteMultiple() {
            var listID = [];
            $.each($scope.selected, function (i, item) {

                listID.push(item.ID) // hàm sẽ lấy ra chỉ số và giá trị của đối tượng $scope.selected , item chính là giá trj cua đói tượng

            })
            var config =
            {
                params:
                {
                    checkProductCategories: JSON.stringify(listID)  // chuyển dữ liệu thành một chuối để xử lí 
                }
            }
            apiService.del('api/productCategory/deleteMultiple', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                search();
            }, function (error) {
                    notificationService.displayError('Xóa không thành công');
                });
        }   
        
        function delProductCategories(id) {
            $ngBootbox.confirm('bạn có chắc chắn muốn xoá không').then(function () {
                var config =
                {
                    params:
                    {
                        id: id
                    }
                }
                apiService.del('api/productCategory/delete', config, function () {
                    notificationService.displaySuccess('Xoa thành công');
                    search();
                }, function () {
                    notificationService.displayError('Xoá không thành công ');
                }
                )

            })

        }
        $scope.$watch("productCategory", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;// 
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.productCategory, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.productCategory, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        function search() {
            getproductCategories();
        }

        function getproductCategories(page) {

            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 4
                }
            }
            apiService.get('/api/productCategory/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('khong tim thay ban ghi nao ');
                }
                else {
                    notificationService.displaySuccess('tim thay ' + result.data.TotalCount + ' ban ghi');
                }
                console.log(result);
                $scope.productCategory = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            },
                function () {
                    console.log('load productCategory fail');
                }
            );

        }
        $scope.getproductCategories();
    }
})(angular.module('tedushop.product_categories'));
// cai này n k cân biét cụ thể lỗi nên n k truyên vaof
// dâý result n là cả cai cục to kia