/// <reference path="../../../assets/admin/libs/angular/angular.js" />

(function (app) {
    app.filter('statusFilter', function () {
        return function (input)
        {
            if (input == true) 
                return 'kich hoạt';
            
            else
                return 'khoá';
        }
    });
})(angular.module('tedushop.common'));