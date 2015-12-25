module app.vehicles {
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
		.module('app.vehicles')
		.controller('app.vehicles.MyController', MyController);
}