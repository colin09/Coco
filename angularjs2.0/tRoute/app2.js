app.config(function($controllerProvider, $compileProvider, $filterProvider, $provide) {
  app.register = {
    controller: $controllerProvider.register,
    directive: $compileProvider.directive,
    filter: $filterProvider.register,
    factory: $provide.factory,
    service: $provide.service
  };
  app.asyncjs = function (js) {
        return ["$q", "$route", "$rootScope", function ($q, $route, $rootScope) {
            var deferred = $q.defer();
            var dependencies = angular.copy(js);
            if (Array.isArray(dependencies)) {
                for (var i = 0; i < dependencies.length; i++) {
                    dependencies[i] += "?v=" + v;
                }
            } else {
                dependencies += "?v=" + v;//v是版本号
            }
            $script(dependencies, function () {
                $rootScope.$apply(function () {
                    deferred.resolve();
                });
            });
            return deferred.promise;
        }];
    }
});

/**
	测试结果： jquery.min.js:2 Uncaught Error: [$injector:modulerr]
		angularjs 1.5 
		script.js 2.5.8
		jQuery v3.1.1

*/