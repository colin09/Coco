var ngApp = angular.module('ngApp',[]);

/*
app.config(['$interpolateProvider', function($interpolateProvider) {
    $interpolateProvider.startSymbol('{[');
    $interpolateProvider.endSymbol(']}');
}]);*/

ngApp.filter('subStr', function () {
    return function (value, max, tail) {
        if (!value) return '';

        max = parseInt(max, 10);
        if (!max) return value;
        if (value.length <= max) return value;

        value = value.substr(0, max);
        return value + (tail || ' …');
    };
});

ngApp.filter('number2', function () {
    return function (number) {
        if (number.length < 1)
            return "";
        return number.substr(0, number.indexOf('.') + 2);
    };
});

//在controller中 注入此单例服务，可用于在controller间通讯，不限制controller父子级
ngApp.factory('ShareData', function () {
    return {
        name: 'share-name'
    };
});

ngApp.controller('indexController',function($scope,$http,$filter,ShareData){

    $scope.roomUsers=[];
    $scope.roomMessages=[];
    
    $scope.txtMsg="";
    $scope.sendMsg = function(){
        if($scope.txtMsg.length<1)
            return;
        ws.send($scope.txtMsg);
        $scope.txtMsg="";
    };

    var ws = new WebSocket('ws://127.0.0.1:3000/ws/chat');

    ws.onmessage = function(event) {
        var data = event.data;
        console.log(data);
        var msg = JSON.parse(data);

        $scope.$apply(function () {
            if (msg.type === 'list') {
                $scope.roomUsers = msg.data;
            } else if (msg.type === 'join') {
                $scope.roomUsers.push(msg.user);
                $scope.roomMessages.push(msg);
            } else if (msg.type === 'left') {
                //移除用户
                $scope.roomMessages.push(msg);
            } else if (msg.type === 'chat') {
                $scope.roomMessages.push(msg);
            }
         });
    };
    

});