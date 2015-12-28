module app.checking {
	'use strict';

	class MyController {

		//public model: any = null;

		static $inject = ['$scope'];
		constructor(private $scope: ng.IScope) {
		}

		// public myMethod(): void {
		// }

	}

	angular
		.module('app.checking')
		.controller('app.checking.MyController', MyController);
}