var app = angular.module("app", ['ngRoute'])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider.when('/', { templateUrl: '/templates/filmes.html', controller: 'FilmesController' });
        $routeProvider.when('/resultado', { templateUrl: '/templates/resultado.html', controller: 'ResultadoController' });
        $locationProvider.html5Mode(true);
    });
