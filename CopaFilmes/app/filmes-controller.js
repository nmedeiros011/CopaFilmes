'use strict';

app.controller("FilmesController", function ($scope, bootstrappedData) {
    $scope.filmes = bootstrappedData.filmes;
});