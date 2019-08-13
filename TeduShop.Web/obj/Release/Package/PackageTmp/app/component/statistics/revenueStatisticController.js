/// <reference path="../../../assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('revenueStatisticController', revenueStatisticController);
    revenueStatisticController.$inject = ['apiService', '$scope','notificationService', '$filter']
    function revenueStatisticController(apiService, $scope, notificationService, $filter) {

        $scope.tabledata = [];
        $scope.labels = [];
        $scope.series = ['Doanh số', 'Lợi nhuận'];
        $scope.chartdata = [];
        function getStatistic() {
            var config = {
                param: {
                    fromDate: '01/01/2015',
                    toDate: '01/01/2020'
                }
            }
            apiService.get('api/statistic/getrevenue?fromDate=' + config.param.fromDate + "&toDate=" + config.param.toDate, null, function (response) { $scope.tabledata = response.data;
                var labels = [];
                var chartData = [];
                var revenues = [];
                var benerfits = [];
                $.each(response.data, function (i, item) {
                    labels.push($filter('date')(item.Date, 'dd/MM/yyyy'));
                    revenues.push(item.Revenues);
                    benerfits.push(item.Benerfit);
                });
                chartData.push(revenues);
                chartData.push(benerfits);

                $scope.chartdata = chartData;
                $scope.labels = labels;},
                function (response) {
                    notificationService.displayError('không thể tải dữ liệu');

                });
        }
        getStatistic();
    }

})(angular.module('tedushop.statistics'));