var vmPosition = avalon.define({
    $id: "positionController",

    indexList: [],
    modelList: [],
    detailList: [],


    relationlist: [],
    topItems: "",
    bottomItems: "",



    getList00: function (id) {
        console.log(id);
        $.getJSON("/mgr/System/PosotionItems", { "code": id }).done(function (json) {
            if (json != "empty") {
                $.each(json, function (key, item) {

                    if (item.Type == 1)
                        vmPosition.indexList.push(item);
                    if (item.Type == 2)
                        vmPosition.modelList.push(item);
                    if (item.Type == 3) {
                        if (item.RelationType == 1)
                            vmPosition.detailList.push(item);
                        if (item.RelationType == 2)
                            vmPosition.topItem = item.Desc;
                        if (item.RelationType == 3)
                            vmPosition.bodyItem = item.Desc;
                    }
                });

                alert();
            }
        });
    }
});


























angular.module('positionApp', ['ngSanitize'])
 .controller("positionController", function ($scope, $http) {

     $scope.getList = function (id) {
         console.log(id);

         $scope.indexList = [];
         $scope.modelList = [];
         $scope.detailList = [];
         $scope.relationlist = [];

         $scope.topItems = [];
         $scope.bottomItems = [];

         $http.get("/mgr/System/PosotionItems?code=" + id).success(function (json) {
             if (json != "empty") {
                 var rIds = "";
                 $.each(json, function (key, item) {

                     if (item.Type == 1)
                         $scope.indexList.push(item);
                     if (item.Type == 2)
                         $scope.modelList.push(item);
                     if (item.Type == 3) {
                         if (rIds.indexOf(item.RelationId) < 0)
                             rIds += "," + item.RelationId;

                         if (item.RelationType == 1)
                             $scope.detailList.push(item);
                         if (item.RelationType == 2)
                             $scope.topItems.push(item);
                         if (item.RelationType == 3)
                             $scope.bottomItems.push(item);
                     }
                 });
                 if (rIds.startsWith(','))
                     rIds = rIds.substring(1);
                 $scope.relationlist = rIds.split(',');
             }
         });
     }

 })