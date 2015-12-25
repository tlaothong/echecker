module app.noti {
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
		.module('app.noti')
		.controller('app.noti.MyController', MyController);
}