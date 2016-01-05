module app.vehicles {
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

        static $inject = ['data', 'app.shared.VehicleService'];
        constructor(public data, private vehicleSvc: app.shared.VehicleService) {
            vehicleSvc.VehiclesData = data;
        }
    }

    class ManageVehicleController {

        static $inject = ['data'];
        constructor(public data) {
        }

    }
    class TopicsController {

        private PlateNumber: any;
        private VehicleStatus: any;

        static $inject = ['data', 'app.shared.VehicleService'];
        constructor(public data, private vehicleSvc: app.shared.VehicleService) {
            this.PlateNumber = vehicleSvc.VehiclesData.PlateNumber;
            this.VehicleStatus = vehicleSvc.VehiclesData.VehicleStatus;
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