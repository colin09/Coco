/* loader.js */
define([], function() {
    return function(dependencies) {
        // 返回路由的 resolve 定义， 
        var definition = {
            // resolver 是一个函数， 返回一个 promise 对象；
            resolver: ['$q', '$rootScope', function($q, $rootScope) {
                // 创建一个延迟执行的 promise 对象
                var defered = $q.defer();
                // 使用 requirejs 的 require 方法加载的脚本
                require(dependencies, function() {
                    $rootScope.$apply(function() {
                        // 加载完脚本之后， 完成 promise 对象；
                        defered.resolve();
                    });
                });
                //返回延迟执行的 promise 对象， route 会等待 promise 对象完成
                return defered.promise;
            }]
        };
        return definition;
    }
});


/* route.js */
define([], function () {
    return {
        defaultRoute: '/welcome',
        routes: {
            '/welcome': {
                templateUrl: 'components/welcome/welcomeView.html',
                controller: 'WelcomeController',
                dependencies: ['components/welcome/welcomeController']
            },
            '/dialogs': {
                templateUrl: 'components/dialogs/dialogsView.html',
                controller: 'DialogsController',
                dependencies: ['components/dialogs/dialogsController']
            },
            '/list': {
                templateUrl: 'components/list/listView.html',
                controller: 'ListController',
                dependencies: ['components/list/listController']
            },
            '/user': {
                templateUrl: 'components/user/userView.html',
                controller: 'UserController',
                dependencies: ['components/user/userController']
            },
            '/help': {
                templateUrl: 'components/help/helpView.html',
                controller: 'HelpController',
                dependencies: ['components/help/helpController']
            }
        }
    };
});


/*************************************************************************************************/






define(['app.routes', 'app.loader', 'angular', 'angular-route'], function (config, loader) {
    'use strict';

    var app = angular.module('app', ['ngRoute', 'ngResource', 'ui.bootstrap']);
    app.config(configure);

    configure.$inject = ['$routeProvider', '$locationProvider', '$controllerProvider', '$compileProvider', '$filterProvider', '$provide'];

    return app;

    function configure($routeProvider, $locationProvider, $controllerProvider, $compileProvider, $filterProvider, $provide) {
        app.registerController = $controllerProvider.register;
        app.registerDirective = $compileProvider.directive;
        app.registerFilter = $filterProvider.register;
        app.registerFactory = $provide.factory;
        app.registerService = $provide.service;

        if (routeConfig.routes != undefined) {
            angular.forEach(routeConfig.routes, function(route, path) {
                $routeProvider.when(path, {
                    templateUrl: route.templateUrl,
                    controller: route.controller,
                    // 设置每个路由的 resolve ， 使用 requirejs 加载 controller 脚本
                    resolve: loader(route.dependencies)
                });
            });
        }

        if (routeConfig.defaultRoute != undefined) {
            $routeProvider.otherwise({ redirectTo: routeConfig.defaultRoute });
        }
    }
});


// 将 controller 定义为一个 AMD 模块， 依赖上面的 app
define(['app'], function(app) {
    'use strict';
    // 调用 app 暴露的 registerController 方法注册 controller
    app.registerController('HelpController', HelpController);
    // 定义 controller 的注入对象；
    HelpController.$inject = ['$scope'];
    // controller 具体实现
    function HelpController($scope) {
        $scope.greeting = 'Help Info';
    }
});