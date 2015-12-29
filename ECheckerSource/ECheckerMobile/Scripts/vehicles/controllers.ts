﻿module app.vehicles {
	'use strict';

    class VehicleListController {
        static $inject = ['data']
        constructor(public data) {
        }
    }

    class VehicleEditController {
        static $inject = ['data', '$state']
        constructor(private data, private $state) {
        }

        private Submit(): void {
        
            //TODO: Process something...
            this.$state.go('app.manvehicles');
        }
    }
    
    class VehicleAddController {
        static $inject = ['$state']
        constructor(private $state) {
        }

        private Submit(): void {
        
            //TODO: Process something...
            this.$state.go('app.manvehicles');
        }
    }

    class VehicleStatusController {
        static $inject = ['data'];
        constructor(public data) {
        }
    }

    class ManageVehicleController {

        static $inject = ['data'];
        constructor(public data) {
        }

    }
    class TopicsController {

        static $inject = ['data'];
        constructor(public data) {
        }

        public IsPass(topic: any): boolean {
            return topic.IsPass == "TRUE";
        }
        public IsFalse(topic: any): boolean {
            return topic.IsPass == "FALSE";
        }

    }

    angular
        .module('app.vehicles')
        .controller('app.vehicles.VehicleListController', VehicleListController)
        .controller('app.vehicles.VehicleEditController', VehicleEditController)
        .controller('app.vehicles.VehicleAddController', VehicleAddController)
        .controller('app.vehicles.VehicleStatusController', VehicleStatusController)
        .controller('app.vehicles.TopicsController', TopicsController)
        .controller('app.vehicles.ManageVehicleController', ManageVehicleController);
}