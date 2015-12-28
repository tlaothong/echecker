module app.regis {
	'use strict';

	class MyController {

		//public model: any = null;

		static $inject = ['$scope', 'myService'];
		constructor(private $scope: ng.IScope, public myService: any) {
		}

		// public myMethod(): void {
		// }

	}

	angular
		.module('app.regis')
		.controller('app.regis.MyController', MyController);
}