module app.vehicles {
	'use strict';

    class VehicleListController {
        static $inject = ['data', '$scope']
        constructor(public data, public scope: any) {
        }
    }

    class VehicleEditController {
        static $inject = ['data', '$scope', '$state']
        constructor(public data, public scope: any, private $state) {
        }

        public Submit(): void {
        
            //TODO: Process something...
            this.$state.go('app.manvehicles');
        }
    }

    class ManageVehicleController {

        static $inject = ['data', '$scope'];
        constructor(public data, private $scope: ng.IScope) {
        }

    }

    angular
        .module('app.vehicles')
        .controller('app.vehicles.VehicleListController', VehicleListController)
        .controller('app.vehicles.VehicleEditController', VehicleEditController)
        .controller('app.vehicles.ManageVehicleController', ManageVehicleController);
}