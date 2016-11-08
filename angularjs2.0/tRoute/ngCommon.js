(function (angular) {'use strict';
    var CommonApp = angular.module('ngCommon');

    /* 记录已加载的js */
	var loaded = {};
	/* 检测是否加载 */
	var checkLoaded = function (url) {
	    return !url || !angular.isString(url) || loaded[url];
	};
	CommonApp.factory('$require', ['$document', '$q', '$rootScope', function ($document, $q, $rootScope) {
	    return function (url) {
	        var script = null;
	        var onload = null;
	        var doc = $document[0];
	        var body = doc.body;
	        var deferred = $q.defer();
	        if (checkLoaded(url)) {
	            deferred.resolve();
	        } else {
	            script = doc.createElement('script');
	            onload = function (info) {
	                if (info === 1) {
	                    deferred.reject();
	                } else {
	                    loaded[url] = 1;
	                    /* AngularJS < 1.2.x 请使用$timeout */
	                    $rootScope.$evalAsync(function () {
	                        deferred.resolve();
	                    });
	                }
	                script.onload = script.onerror = null;
	                body.removeChild(script);
	                script = null;
	            };
	            script.onload = onload;
	            script.onerror = function () {
	                onload(1);
	            };
	            script.async = true;
	            script.src = url;
	            body.appendChild(script);
	        }
	        return deferred.promise;
	    };
	}]);

	CommonApp.provider('$routeResolver', function () {
	    this.$get = function () {
	        return this;
	    };
	    this.route = function (routeCnf) {
	        var controller = routeCnf.controller;
	        var controllerUrl = routeCnf.controllerUrl;
	        if (controllerUrl) {
	            routeCnf.reloadOnSearch = routeCnf.reloadOnSearch || false;
	            routeCnf.resolve = {
	                load: ['$route', '$require', 'ControllerChecker',
	                    function ($route, $require, ControllerChecker) {
	                        var controllerName = angular.isFunction(controller) ? controller($route.current.params) : controller;
	                        var url = angular.isFunction(controllerUrl) ? controllerUrl($route.current.params) : controllerUrl;
	                        if (checkLoaded(url) || (controllerName && ControllerChecker.exists(controllerName))) {
	                            loaded[url] = true;
	                            return;
	                        }
	                        return $require(url);
	                }]
	            };
	        }
	        return routeCnf;
	    };
	});



















})(angular);