var appList = angular.module("AppItemList", ['ngSanitize'])
    .controller("listCtl", function ($scope, $http) {
        $http.get("ItemData?code=" + $("#hdCode").val() + "&type=" + $("#hdType").val() + "&sourceId=" + $("#hdSource").val())
            .success(function (json) {
                if (json.length > 0) {
                    $scope.list = [];

                    $scope.title = json[0].Name;
                    //$scope.top = 0;
                    //$scope.bottom = 0;
                    $.each(json, function (key, item) {
                        if (item.RelationType == 1)
                            $scope.list.push(item);
                        if (item.RelationType == 2)
                            $scope.top = item.Desc;
                        if (item.RelationType == 3)
                            $scope.bottom = item.Desc;
                    });
                }
            });
    });

angular.module('angularWaterfallApp', ["ngWaterfall", "ui.router"])
 .controller("listCtl1", function ($scope, $http) {
     $http.get("ItemData?code=" + $("#hdCode").val() + "&type=" + $("#hdType").val() + "&sourceId=" + $("#hdSource").val())
         .success(function (json) {
             if (json.length > 0) {
                 $scope.list = json;

                 $scope.title = json[0].Name;
                 $scope.description = json[0].Desc;
             }
         });
     $scope.$on("waterfall:loadMore", function () {//滚动自动填充事件
         $scope.loadMoreData();
     })
 });

angular.module('quotationApp', ['ngSanitize'])
 .controller("DetailCtl1", function ($scope, $http) {
     $http.get("../scripts/js/quotations.json")
     //$http.get("ItemData?code=0")
         .success(function (json) {
             if (json.length > 0) {
                 var code = $("#hdCode").val();
                 $.each(json, function (key, item) {
                     if (item.id == code) {
                         $scope.body = item.body;
                     }
                 });
             }
         });
 })
.directive('repeatFinish', function () {
    return {
        link: function (scope, element, attr) {
            console.log(scope.$index);
            if (scope.$last == true) {
                console.log('ng-repeat执行完毕');
                var code = $("#hdCode").val();
                if (code == 6 || code == 7) {
                    $("#divPrice").css("margin", "0");
                    $("#ulItems li").css("padding", "0 5px");
                }
            }
        }
    }
})

