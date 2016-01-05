module app.checking {
    'use strict';

    class MyController {

        //public model: any = null;

        static $inject = ['$scope'];
        constructor(private $scope: ng.IScope) {
        }

    }

    class CheckAmissController {
        static $inject = ['data'];
        constructor(public data) {
        }

    }

    angular
        .module('app.checking')
        .controller('app.checking.MyController', MyController)
        .controller('app.checking.CheckAmissController', CheckAmissController);
}